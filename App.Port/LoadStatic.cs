namespace App.Port
{
    public class LoadStatic
    {
        /// <summary>
        /// 
        /// </summary>
        public static bool Continue { get; set; }
        public static System.IO.Ports.SerialPort SPort { get; set; }
        public static Style.InputBoxWithDesc PortName { get; set; }
        public static Style.InputBoxWithDesc Parity { get; set; }
        public static Style.InputBoxWithDesc BaudRate { get; set; }
        public static Style.InputBoxWithDesc DataBits { get; set; }
        public static Style.InputBoxWithDesc StopBits { get; set; }
        public static Style.InputBoxWithDesc Handshake { get; set; }
        public static Style.InputBoxWithDesc ReadTimeout { get; set; }
        public static Style.InputBoxWithDesc WriteTimeout { get; set; }
        public static Style.InputBoxWithDesc WriteData { get; set; }
        public static Style.InputBoxWithDesc ReadData { get; set; }
        public static Style.LButton WriteClear { get; set; }
        public static Style.LButton ReadClear { get; set; }
        public static Style.LButton WritePost { get; set; }
        public static Style.LButton OpenPort { get; set; }
        //public static Style.LButton RestPort { get; set; }
        public static Style.LButton RestPort { get; set; }
        public static Style.RadioSigleBox AfterClear { get; set; }
        public static Style.RadioSigleBox HexJinZhi { get; set; }
        public static Style.RadioSigleBox AutoPost { get; set; }
        public static bool IsAutoPost { get; set; }
        public static Style.InputBoxWithDesc AutoTime { get; set; }
        public static bool IsAfterClear { get; set; }
        public static bool IsHexJinZhi { get; set; }
        public static string[] PortNames { get; set; }
        public static System.Timers.Timer AutoPoster { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public const int FloatInt = 8;

        public const int LineInt = 1;

        /// <summary>
        /// 
        /// </summary>
        public static Style.LPanel MainPal { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public static Style.HDarge HeaderLine { get; set; }

        public static Style.HDarge HeaderTitle { get; set; }
        public static Style.HDarImg HeaderIco { get; set; }
        public static Style.StepPal HeaderNav { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public static Style.HDarge SectionPal { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public static Style.HDarge FooterPal { get; set; }
    }
}