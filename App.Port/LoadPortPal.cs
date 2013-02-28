
namespace App.Port
{
    public class LoadPortPal
    {
        public static void LoadPortPals()
        {
            LoadStatic.PortName = new Style.InputBoxWithDesc(
                LoadStatic.MainPal,
                1,
                2,
                @"串口名",
                "",
                new System.Drawing.Font(Style.Helper.FontSegoe, 12.5F),
                new System.Drawing.Size(84, 34),
                new System.Drawing.Font(Style.Helper.FontSegoe, 12F),
                new System.Drawing.Size(126, 34),
                new System.Drawing.Point(8, 8*1 + 34*0),
                System.Drawing.Color.FromArgb(248, 248, 248),
                System.Drawing.Color.FromArgb(241, 159, 70),
                System.Drawing.Color.FromArgb(220, 220, 220),
                System.Drawing.Color.FromArgb(241, 159, 70),
                System.Drawing.Color.FromArgb(248, 248, 248),
                System.Drawing.Color.FromArgb(248, 248, 248),
                System.Drawing.Color.FromArgb(75, 75, 75),
                System.Drawing.Color.FromArgb(25, 25, 25),
                Style.Helper.AnchorTopRight
                );
            LoadStatic.BaudRate = new Style.InputBoxWithDesc(
                LoadStatic.MainPal,
                1,
                2,
                @"波特率",
                "",
                new System.Drawing.Font(Style.Helper.FontSegoe, 12.5F),
                new System.Drawing.Size(84, 34),
                new System.Drawing.Font(Style.Helper.FontSegoe, 12F),
                new System.Drawing.Size(126, 34),
                new System.Drawing.Point(8, 8*2 + 34*1),
                System.Drawing.Color.FromArgb(248, 248, 248),
                System.Drawing.Color.FromArgb(241, 159, 70),
                System.Drawing.Color.FromArgb(220, 220, 220),
                System.Drawing.Color.FromArgb(241, 159, 70),
                System.Drawing.Color.FromArgb(248, 248, 248),
                System.Drawing.Color.FromArgb(248, 248, 248),
                System.Drawing.Color.FromArgb(75, 75, 75),
                System.Drawing.Color.FromArgb(25, 25, 25),
                Style.Helper.AnchorTopRight
                );
            LoadStatic.Parity = new Style.InputBoxWithDesc(
                LoadStatic.MainPal,
                1,
                2,
                @"校验位",
                "",
                new System.Drawing.Font(Style.Helper.FontSegoe, 12.5F),
                new System.Drawing.Size(84, 34),
                new System.Drawing.Font(Style.Helper.FontSegoe, 12F),
                new System.Drawing.Size(126, 34),
                new System.Drawing.Point(8, 8*3 + 34*2),
                System.Drawing.Color.FromArgb(248, 248, 248),
                System.Drawing.Color.FromArgb(241, 159, 70),
                System.Drawing.Color.FromArgb(220, 220, 220),
                System.Drawing.Color.FromArgb(241, 159, 70),
                System.Drawing.Color.FromArgb(248, 248, 248),
                System.Drawing.Color.FromArgb(248, 248, 248),
                System.Drawing.Color.FromArgb(75, 75, 75),
                System.Drawing.Color.FromArgb(25, 25, 25),
                Style.Helper.AnchorTopRight
                );
            LoadStatic.DataBits = new Style.InputBoxWithDesc(
                LoadStatic.MainPal,
                1,
                2,
                @"数据位",
                "",
                new System.Drawing.Font(Style.Helper.FontSegoe, 12.5F),
                new System.Drawing.Size(84, 34),
                new System.Drawing.Font(Style.Helper.FontSegoe, 12F),
                new System.Drawing.Size(126, 34),
                new System.Drawing.Point(8, 8*4 + 34*3),
                System.Drawing.Color.FromArgb(248, 248, 248),
                System.Drawing.Color.FromArgb(241, 159, 70),
                System.Drawing.Color.FromArgb(220, 220, 220),
                System.Drawing.Color.FromArgb(241, 159, 70),
                System.Drawing.Color.FromArgb(248, 248, 248),
                System.Drawing.Color.FromArgb(248, 248, 248),
                System.Drawing.Color.FromArgb(75, 75, 75),
                System.Drawing.Color.FromArgb(25, 25, 25),
                Style.Helper.AnchorTopRight
                );
            LoadStatic.StopBits = new Style.InputBoxWithDesc(
                LoadStatic.MainPal,
                1,
                2,
                @"停止位",
                "",
                new System.Drawing.Font(Style.Helper.FontSegoe, 12.5F),
                new System.Drawing.Size(84, 34),
                new System.Drawing.Font(Style.Helper.FontSegoe, 12F),
                new System.Drawing.Size(126, 34),
                new System.Drawing.Point(8, 8*5 + 34*4),
                System.Drawing.Color.FromArgb(248, 248, 248),
                System.Drawing.Color.FromArgb(241, 159, 70),
                System.Drawing.Color.FromArgb(220, 220, 220),
                System.Drawing.Color.FromArgb(241, 159, 70),
                System.Drawing.Color.FromArgb(248, 248, 248),
                System.Drawing.Color.FromArgb(248, 248, 248),
                System.Drawing.Color.FromArgb(75, 75, 75),
                System.Drawing.Color.FromArgb(25, 25, 25),
                Style.Helper.AnchorTopRight
                );
            LoadStatic.Handshake = new Style.InputBoxWithDesc(
                LoadStatic.MainPal,
                1,
                2,
                @"握手法",
                "",
                new System.Drawing.Font(Style.Helper.FontSegoe, 12.5F),
                new System.Drawing.Size(84, 34),
                new System.Drawing.Font(Style.Helper.FontSegoe, 12F),
                new System.Drawing.Size(126, 34),
                new System.Drawing.Point(8, 8*6 + 34*5),
                System.Drawing.Color.FromArgb(248, 248, 248),
                System.Drawing.Color.FromArgb(241, 159, 70),
                System.Drawing.Color.FromArgb(220, 220, 220),
                System.Drawing.Color.FromArgb(241, 159, 70),
                System.Drawing.Color.FromArgb(248, 248, 248),
                System.Drawing.Color.FromArgb(248, 248, 248),
                System.Drawing.Color.FromArgb(75, 75, 75),
                System.Drawing.Color.FromArgb(25, 25, 25),
                Style.Helper.AnchorTopRight
                );
            LoadStatic.ReadTimeout = new Style.InputBoxWithDesc(
                LoadStatic.MainPal,
                1,
                2,
                @"读超时",
                "",
                new System.Drawing.Font(Style.Helper.FontSegoe, 12.5F),
                new System.Drawing.Size(84, 34),
                new System.Drawing.Font(Style.Helper.FontSegoe, 12F),
                new System.Drawing.Size(126, 34),
                new System.Drawing.Point(8, 8*7 + 34*6),
                System.Drawing.Color.FromArgb(248, 248, 248),
                System.Drawing.Color.FromArgb(241, 159, 70),
                System.Drawing.Color.FromArgb(220, 220, 220),
                System.Drawing.Color.FromArgb(241, 159, 70),
                System.Drawing.Color.FromArgb(248, 248, 248),
                System.Drawing.Color.FromArgb(248, 248, 248),
                System.Drawing.Color.FromArgb(75, 75, 75),
                System.Drawing.Color.FromArgb(25, 25, 25),
                Style.Helper.AnchorTopRight
                );
            LoadStatic.WriteTimeout = new Style.InputBoxWithDesc(
                LoadStatic.MainPal,
                1,
                2,
                @"写超时",
                "",
                new System.Drawing.Font(Style.Helper.FontSegoe, 12.5F),
                new System.Drawing.Size(84, 34),
                new System.Drawing.Font(Style.Helper.FontSegoe, 12F),
                new System.Drawing.Size(126, 34),
                new System.Drawing.Point(8, 8*8 + 34*7),
                System.Drawing.Color.FromArgb(248, 248, 248),
                System.Drawing.Color.FromArgb(241, 159, 70),
                System.Drawing.Color.FromArgb(220, 220, 220),
                System.Drawing.Color.FromArgb(241, 159, 70),
                System.Drawing.Color.FromArgb(248, 248, 248),
                System.Drawing.Color.FromArgb(248, 248, 248),
                System.Drawing.Color.FromArgb(75, 75, 75),
                System.Drawing.Color.FromArgb(25, 25, 25),
                Style.Helper.AnchorTopRight
                );
            LoadStatic.OpenPort = new Style.LButton
                (
                LoadStatic.MainPal,
                1,
                @"开启监听",
                new System.Drawing.Font(Style.Helper.FontSegoe, 13F),
                new System.Drawing.Size(126 + 84, 34),
                new System.Drawing.Point(8, 8*9 + 34*8),
                System.Drawing.Color.FromArgb(241, 159, 70),
                System.Drawing.Color.FromArgb(248, 248, 248),
                System.Drawing.Color.FromArgb(248, 248, 248),
                System.Drawing.Color.FromArgb(241, 159, 70),
                System.Drawing.Color.FromArgb(241, 159, 70),
                System.Drawing.Color.FromArgb(248, 248, 248),
                Style.Helper.AnchorTopRight
                );
            LoadStatic.RestPort = new Style.LButton
                (
                LoadStatic.MainPal,
                1,
                @"恢复默认设置",
                new System.Drawing.Font(Style.Helper.FontSegoe, 13F),
                new System.Drawing.Size(126 + 84, 34),
                new System.Drawing.Point(8, 8*10 + 34*9),
                System.Drawing.Color.FromArgb(241, 159, 70),
                System.Drawing.Color.FromArgb(248, 248, 248),
                System.Drawing.Color.FromArgb(248, 248, 248),
                System.Drawing.Color.FromArgb(241, 159, 70),
                System.Drawing.Color.FromArgb(241, 159, 70),
                System.Drawing.Color.FromArgb(248, 248, 248),
                Style.Helper.AnchorTopRight
                );
            LoadStatic.ReadData = new Style.InputBoxWithDesc(
                LoadStatic.MainPal,
                1,
                2,
                "接\r\n收\r\n内\r\n容",
                "",
                new System.Drawing.Font(Style.Helper.FontSegoe, 12.5F),
                new System.Drawing.Size(84, 286),
                new System.Drawing.Font(Style.Helper.FontSegoe, 12F),
                new System.Drawing.Size(326, 286),
                new System.Drawing.Point(8*2 + 84 + 126, 8*1 + 34*0),
                System.Drawing.Color.FromArgb(248, 248, 248),
                System.Drawing.Color.FromArgb(241, 159, 70),
                System.Drawing.Color.FromArgb(220, 220, 220),
                System.Drawing.Color.FromArgb(241, 159, 70),
                System.Drawing.Color.FromArgb(248, 248, 248),
                System.Drawing.Color.FromArgb(248, 248, 248),
                System.Drawing.Color.FromArgb(75, 75, 75),
                System.Drawing.Color.FromArgb(25, 25, 25),
                Style.Helper.AnchorTopRight
                );
            LoadStatic.WriteData = new Style.InputBoxWithDesc(
                LoadStatic.MainPal,
                1,
                2,
                "发\r\n送\r\n内\r\n容",
                "",
                new System.Drawing.Font(Style.Helper.FontSegoe, 12.5F),
                new System.Drawing.Size(84, 118),
                new System.Drawing.Font(Style.Helper.FontSegoe, 12F),
                new System.Drawing.Size(326, 118),
                new System.Drawing.Point(8*2 + 84 + 126, 8*2 + 34*0 + 286),
                System.Drawing.Color.FromArgb(248, 248, 248),
                System.Drawing.Color.FromArgb(241, 159, 70),
                System.Drawing.Color.FromArgb(220, 220, 220),
                System.Drawing.Color.FromArgb(241, 159, 70),
                System.Drawing.Color.FromArgb(248, 248, 248),
                System.Drawing.Color.FromArgb(248, 248, 248),
                System.Drawing.Color.FromArgb(75, 75, 75),
                System.Drawing.Color.FromArgb(25, 25, 25),
                Style.Helper.AnchorTopRight
                );

            LoadStatic.ReadClear = new Style.LButton
                (
                LoadStatic.MainPal,
                1,
                @"清空接收",
                new System.Drawing.Font(Style.Helper.FontSegoe, 13F),
                new System.Drawing.Size(146, 34),
                new System.Drawing.Point(8*3 + 84*2 + 126 + 326, 8*0 + 286 - 34*2),
                System.Drawing.Color.FromArgb(241, 159, 70),
                System.Drawing.Color.FromArgb(248, 248, 248),
                System.Drawing.Color.FromArgb(248, 248, 248),
                System.Drawing.Color.FromArgb(241, 159, 70),
                System.Drawing.Color.FromArgb(241, 159, 70),
                System.Drawing.Color.FromArgb(248, 248, 248),
                Style.Helper.AnchorTopRight
                );
            LoadStatic.ReadClear.MouseClick += ReadClear_MouseClick;
            LoadStatic.WriteClear = new Style.LButton
                (
                LoadStatic.MainPal,
                1,
                @"清空发送",
                new System.Drawing.Font(Style.Helper.FontSegoe, 13F),
                new System.Drawing.Size(146, 34),
                new System.Drawing.Point(8*3 + 84*2 + 126 + 326, 8*1 + 286 - 34),
                System.Drawing.Color.FromArgb(241, 159, 70),
                System.Drawing.Color.FromArgb(248, 248, 248),
                System.Drawing.Color.FromArgb(248, 248, 248),
                System.Drawing.Color.FromArgb(241, 159, 70),
                System.Drawing.Color.FromArgb(241, 159, 70),
                System.Drawing.Color.FromArgb(248, 248, 248),
                Style.Helper.AnchorTopRight
                );
            LoadStatic.WriteClear.MouseClick += WriteClear_MouseClick;
            LoadStatic.WritePost = new Style.LButton
                (
                LoadStatic.MainPal,
                1,
                @"点击发送",
                new System.Drawing.Font(Style.Helper.FontSegoe, 13F),
                new System.Drawing.Size(146, 118),
                new System.Drawing.Point(8*3 + 84*2 + 126 + 326, 8*2 + 34*0 + 286),
                System.Drawing.Color.FromArgb(241, 159, 70),
                System.Drawing.Color.FromArgb(248, 248, 248),
                System.Drawing.Color.FromArgb(248, 248, 248),
                System.Drawing.Color.FromArgb(241, 159, 70),
                System.Drawing.Color.FromArgb(241, 159, 70),
                System.Drawing.Color.FromArgb(248, 248, 248),
                Style.Helper.AnchorTopRight
                );
            LoadStatic.WritePost.MouseClick += WritePost_MouseClick;


            LoadStatic.AfterClear = new Style.RadioSigleBox(
                LoadStatic.MainPal,
                "发送自动清空",
                true,
                false,
                "自动清空",
                16,
                0,
                new System.Drawing.Size(146, 25),
                new System.Drawing.Point(8*3 + 84*2 + 126 + 326, 8*4 + 34*3 + 50),
                new System.Drawing.Font(Style.Helper.FontSegoe, 12F),
                System.Drawing.Color.FromArgb(241, 159, 70),
                System.Drawing.Color.FromArgb(255, 255, 255),
                Style.Helper.AnchorTopRight,
                AfterClear_MouseDown
                );
            LoadStatic.HexJinZhi = new Style.RadioSigleBox(
                LoadStatic.MainPal,
                "发送十六进制",
                false,
                false,
                "十六进制",
                16,
                0,
                new System.Drawing.Size(146, 25),
                new System.Drawing.Point(8 * 3 + 84 * 2 + 126 + 326, 8 * 3 + 34 * 3 + 25),
                new System.Drawing.Font(Style.Helper.FontSegoe, 12F),
                System.Drawing.Color.FromArgb(241, 159, 70),
                System.Drawing.Color.FromArgb(255, 255, 255),
                Style.Helper.AnchorTopRight,
                HexJinZhi_MouseDown
                );
            LoadStatic.AutoPost = new Style.RadioSigleBox(
                LoadStatic.MainPal,
                "自动循环发送",
                false,
                false,
                "循环发送",
                16,
                0,
                new System.Drawing.Size(146, 25),
                new System.Drawing.Point(8 * 3 + 84 * 2 + 126 + 326, 8 * 2 + 34 * 3 ),
                new System.Drawing.Font(Style.Helper.FontSegoe, 12F),
                System.Drawing.Color.FromArgb(241, 159, 70),
                System.Drawing.Color.FromArgb(255, 255, 255),
                Style.Helper.AnchorTopRight,
                AutoPost_MouseDown
                );
            LoadStatic.AutoTime = new Style.InputBoxWithDesc(
                LoadStatic.MainPal,
                1,
                2,
                @"周期",
                "",
                new System.Drawing.Font(Style.Helper.FontSegoe, 11F),
                new System.Drawing.Size(46, 30),
                new System.Drawing.Font(Style.Helper.FontSegoe, 10F),
                new System.Drawing.Size(80, 30),
                new System.Drawing.Point(8 * 3 + 84 * 2 + 126 + 326, 8 * 1 + 34 * 3 - 35),
                System.Drawing.Color.FromArgb(248, 248, 248),
                System.Drawing.Color.FromArgb(241, 159, 70),
                System.Drawing.Color.FromArgb(220, 220, 220),
                System.Drawing.Color.FromArgb(241, 159, 70),
                System.Drawing.Color.FromArgb(248, 248, 248),
                System.Drawing.Color.FromArgb(248, 248, 248),
                System.Drawing.Color.FromArgb(75, 75, 75),
                System.Drawing.Color.FromArgb(25, 25, 25),
                Style.Helper.AnchorTopRight
                );
            LoadStatic.AutoPost.CheckStateChanged += AutoPost_CheckStateChanged;
            LoadStatic.OpenPort.MouseClick += OpenPort_MouseClick;
            LoadStatic.RestPort.MouseClick += RestPort_MouseClick;
            LoadStatic.WriteData.KeyDown += WriteData_KeyDown;
            Style.Helper.Ui.Activated += Ui_Activated;
        }

        private static void WriteData_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case System.Windows.Forms.Keys.Enter:
                    {
                        ReadyToPost();
                    }
                    break;
            }
        }

        private static void AutoPost_CheckStateChanged(object sender, System.EventArgs e)
        {
            LoadStatic.AutoTime.Enabled = LoadStatic.AutoPost.Checked;
            if (LoadStatic.AutoPost.Checked)
            {
                LoadStatic.AfterClear.Checked = false;
                LoadStatic.IsAfterClear = false;
            }
            if (LoadStatic.AutoTime.Text.IsNullOrEmptyOrSpace())
            {
                LoadStatic.AutoTime.Text = @"1000";
            }
        }

       
        /// <summary>
        /// 自动循环发送
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void AutoPost_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            LoadStatic.AutoPost.Checked = !LoadStatic.AutoPost.Checked;
            LoadStatic.IsAutoPost = LoadStatic.AutoPost.Checked;
        }
        /// <summary>
        /// 发送十六进制数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void HexJinZhi_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            LoadStatic.HexJinZhi.Checked = !LoadStatic.HexJinZhi.Checked;
            LoadStatic.IsHexJinZhi = LoadStatic.HexJinZhi.Checked;
        }
        /// <summary>
        /// 发送后自动清理输入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void AfterClear_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            LoadStatic.AfterClear.Checked = !LoadStatic.AfterClear.Checked;
            LoadStatic.IsAfterClear = LoadStatic.AfterClear.Checked;
        }

        /// <summary>
        /// 恢复默认设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void RestPort_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            LoadSerPort.Reset();
        }

        /// <summary>
        /// 打开串口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void OpenPort_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            switch (LoadStatic.OpenPort.Text)
            {
                case @"开启监听":
                    if (!LoadStatic.SPort.IsOpen)
                    {
                        LoadStatic.OpenPort.Text = LoadSerPort.OpenPort() ? @"关闭监听" : @"开启监听";
                    }
                    break;
                case @"关闭监听":
                    if (LoadStatic.SPort.IsOpen)
                    {
                        LoadStatic.OpenPort.Text = LoadSerPort.ClosePort() ? @"开启监听" : @"关闭监听";
                    }
                    break;
            }
            LoadSerPort.ChangeOpen();
        }

        /// <summary>
        /// 发送内容
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void WritePost_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            ReadyToPost();
        }
        private static void ReadyToPost()
        {
            if (LoadStatic.SPort == null || !LoadStatic.Continue) return;
            if (LoadStatic.WritePost.Text == @"停止发送")
            {
                LoadStatic.AutoPoster.Enabled = false;
                LoadStatic.WritePost.Text = @"点击发送";
            }
            else
            {
                if (LoadStatic.IsAutoPost)
                {
                    int autoTime;
                    if (int.TryParse(LoadStatic.AutoTime.Text, out autoTime))
                    {
                        LoadStatic.AutoPoster.Interval = autoTime;
                        LoadStatic.AutoPoster.Enabled = true;
                    }
                }
                else
                {
                    LoadSerPort.PostData();
                }
            }
        }
        /// <summary>
        /// 清空接收
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void ReadClear_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            LoadStatic.ReadData.Text = "";
        }

        /// <summary>
        /// 清空发送
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void WriteClear_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            LoadStatic.WriteData.Text = "";
        }

        /// <summary>
        /// 输入框聚焦
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Ui_Activated(object sender, System.EventArgs e)
        {
            LoadStatic.OpenPort.TabIndex = 0;
            LoadStatic.OpenPort.Focus();
        }
    }
}