namespace App.Base
{
    public class Global
    {
        /// <summary>
        /// 是否是深色主题,默认是深色背景
        /// </summary>
        public static bool IsDeep { get; set; }
        /// <summary>
        /// 是否为管理员,默认是非管理员
        /// </summary>
        public static bool IsAdmin { get; set; }
        /// <summary>
        /// 是否为服务器版,默认是本地版
        /// </summary>
        public static bool IsServer { get; set; }
        /// <summary>
        /// 本地数据文件夹
        /// </summary>
        public static string DataFolderDirectory { get; set; }
        /// <summary>
        /// 服务器数据请求地址
        /// </summary>
        public static string ServerRequestPath { get; set; }
        /// <summary>
        /// 服务器检查升级地址
        /// </summary>
        public static string CheckUpdatePath { get; set; }
    }
}