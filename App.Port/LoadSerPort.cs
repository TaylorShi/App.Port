namespace App.Port
{
    public class LoadSerPort
    {
        /// <summary>
        /// 关闭串口监听
        /// </summary>
        /// <returns></returns>
        public static bool ClosePort()
        {
            try
            {
                LoadStatic.SPort.Close();
                LoadStatic.Continue = false;
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 开启串口监听
        /// </summary>
        /// <returns></returns>
        public static bool OpenPort()
        {
            LoadStatic.SPort.PortName = LoadStatic.PortName.Text;
            int baudRate;
            if (int.TryParse(LoadStatic.BaudRate.Text, out baudRate))
            {
                LoadStatic.SPort.BaudRate = baudRate;
            }
            LoadStatic.SPort.ParityReplace = System.Text.Encoding.UTF8.GetBytes(LoadStatic.Parity.Text)[0];
            int dataBits;
            if (int.TryParse(LoadStatic.DataBits.Text, out dataBits))
            {
                LoadStatic.SPort.DataBits = dataBits;
            }
            switch (LoadStatic.StopBits.Text)
            {
                    // 默认值 One
                case @"One":
                    {
                        LoadStatic.SPort.StopBits = System.IO.Ports.StopBits.One;
                    }
                    break;
                case @"OnePointFive":
                    {
                        LoadStatic.SPort.StopBits = System.IO.Ports.StopBits.OnePointFive;
                    }
                    break;
                case @"Two":
                    {
                        LoadStatic.SPort.StopBits = System.IO.Ports.StopBits.Two;
                    }
                    break;
            }
            switch (LoadStatic.Handshake.Text)
            {
                    // 默认值 None
                case @"None":
                    {
                        LoadStatic.SPort.Handshake = System.IO.Ports.Handshake.None;
                    }
                    break;
                case @"RequestToSend":
                    {
                        LoadStatic.SPort.Handshake = System.IO.Ports.Handshake.RequestToSend;
                    }
                    break;
                case @"RequestToSendXOnXOff":
                    {
                        LoadStatic.SPort.Handshake = System.IO.Ports.Handshake.RequestToSendXOnXOff;
                    }
                    break;
                case @"XOnXOff":
                    {
                        LoadStatic.SPort.Handshake = System.IO.Ports.Handshake.XOnXOff;
                    }
                    break;
            }
            int readTimeout;
            if (int.TryParse(LoadStatic.ReadTimeout.Text, out readTimeout))
            {
                LoadStatic.SPort.ReadTimeout = readTimeout;
            }
            int writeTimeout;
            if (int.TryParse(LoadStatic.WriteTimeout.Text, out writeTimeout))
            {
                LoadStatic.SPort.WriteTimeout = writeTimeout;
            }
            // 开启数据截取和加载
            LoadStatic.SPort.DataReceived += SPort_DataReceived;
            LoadStatic.SPort.Encoding = System.Text.Encoding.UTF8;
            try
            {
                LoadStatic.SPort.Open();
                LoadStatic.Continue = true;
            }
            catch
            {
                LoadStatic.Continue = false;
                return false;
            }
            return true;
        }

        /// <summary>
        /// 接收串口数据并显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void SPort_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            if (!LoadStatic.Continue || !LoadStatic.SPort.IsOpen) return;
            var portLine = LoadStatic.SPort.ReadLine();
            Style.Helper.Ui.Invoke(
                new System.Windows.Forms.MethodInvoker
                    (() =>
                        {
                            LoadStatic.ReadData.Text += portLine + @"
";
                            LoadStatic.ReadData.Select(LoadStatic.ReadData.TextLength, 0); //光标定位到文本最后
                            LoadStatic.ReadData.ScrollToCaret(); //滚动到光标处
                            LoadStatic.WriteData.Focus();
                        }
                    ));
        }

        /// <summary>
        /// 程序初始化时复位
        /// </summary>
        public static void Reset()
        {
            if (LoadStatic.SPort != null && LoadStatic.SPort.IsOpen)
            {
                ClosePort();
                LoadStatic.SPort.Dispose();
            }
            LoadStatic.SPort = new System.IO.Ports.SerialPort();
            LoadStatic.PortNames = System.IO.Ports.SerialPort.GetPortNames();
            LoadStatic.PortName.Text = LoadStatic.PortNames[0];
            LoadStatic.BaudRate.Text =
                LoadStatic.SPort.BaudRate.ToString(System.Globalization.CultureInfo.InvariantCulture);
            LoadStatic.Parity.Text =
                LoadStatic.SPort.ParityReplace.ToString(System.Globalization.CultureInfo.InvariantCulture);
            LoadStatic.DataBits.Text =
                LoadStatic.SPort.DataBits.ToString(System.Globalization.CultureInfo.InvariantCulture);
            LoadStatic.StopBits.Text = LoadStatic.SPort.StopBits.ToString();
            LoadStatic.Handshake.Text = LoadStatic.SPort.Handshake.ToString();
            LoadStatic.ReadTimeout.Text =
                LoadStatic.SPort.ReadTimeout.ToString(System.Globalization.CultureInfo.InvariantCulture);
            LoadStatic.WriteTimeout.Text =
                LoadStatic.SPort.WriteTimeout.ToString(System.Globalization.CultureInfo.InvariantCulture);
            LoadStatic.WriteData.Text = "";
            LoadStatic.ReadData.Text = "";
            LoadStatic.OpenPort.Text = @"开启监听";
            LoadStatic.Continue = false;
            LoadStatic.IsAfterClear = true;
            LoadStatic.IsHexJinZhi = false;
            LoadStatic.IsAutoPost = false;
            LoadStatic.AutoTime.Text = "";
            if (LoadStatic.AutoPoster != null)
            {
                LoadStatic.AutoPoster.Dispose();
            }
            LoadStatic.AutoPoster = new System.Timers.Timer{ Enabled = false};
            LoadStatic.AutoPoster.Elapsed += AutoPoster_Elapsed;
            ChangeOpen();
        }
        /// <summary>
        /// 自动发送
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void AutoPoster_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            Style.Helper.Ui.Invoke(
                   new System.Windows.Forms.MethodInvoker
                       (() =>
                       {
                           LoadStatic.WritePost.Text = @"停止发送";
                       }));
            
            PostData();
        }
        /// <summary>
        ///  发送数据出去
        /// </summary>
        public static void PostData()
        {
            var wannaWrite = LoadStatic.WriteData.Text;
            if (wannaWrite.IsNullOrEmptyOrSpace())
            {
                LoadStatic.AutoPoster.Enabled = false;
                "尚未输入任何内容!".ToErrorMsgBox("发送内容输入框");
                return;
            }
            if (LoadStatic.IsHexJinZhi)
            {
                // 建立临时字节数组对象
                var bSendTemp = new System.Byte[1];
                // 由文本框读入想要发送的数据
                bSendTemp[0] = System.Byte.Parse(wannaWrite);
                // 发送数据
                LoadStatic.SPort.Write(bSendTemp, 0, 1);
            }
            else
            {
                LoadStatic.SPort.WriteLine(wannaWrite);
            }
            if (LoadStatic.IsAfterClear)
            {
                Style.Helper.Ui.Invoke(
                    new System.Windows.Forms.MethodInvoker
                        (() =>
                            {
                                LoadStatic.WriteData.Text = "";
                                LoadStatic.WriteData.Text = "";
                                LoadStatic.WriteData.ScrollToCaret(); //滚动到光标处
                                LoadStatic.WriteData.Select(LoadStatic.ReadData.TextLength, 0); //光标定位到文本最后
                                
                            }));
            }
        }
        /// <summary>
        /// 切换开启和关闭状态
        /// </summary>
        public static void ChangeOpen()
        {
            LoadStatic.WriteClear.Enabled =
                LoadStatic.ReadClear.Enabled =
                LoadStatic.WritePost.Enabled =
                LoadStatic.WriteData.Enabled =
                LoadStatic.ReadData.Enabled = 
                LoadStatic.AfterClear.Enabled =
                LoadStatic.HexJinZhi.Enabled =
                LoadStatic.AutoPost.Enabled =
                LoadStatic.AutoTime.Enabled
                = LoadStatic.Continue;

            LoadStatic.PortName.Enabled =
                LoadStatic.Parity.Enabled =
                LoadStatic.DataBits.Enabled =
                LoadStatic.StopBits.Enabled =
                LoadStatic.Handshake.Enabled =
                LoadStatic.ReadTimeout.Enabled =
                LoadStatic.WriteTimeout.Enabled =
                LoadStatic.BaudRate.Enabled 
                = !LoadStatic.Continue;

            if (LoadStatic.Continue)
            {
                LoadStatic.WriteData.TabIndex = 0;
                LoadStatic.WriteData.Focus();
            }
            else
            {
                LoadStatic.OpenPort.TabIndex = 0;
                LoadStatic.OpenPort.Focus();
            }
        }
    }
}