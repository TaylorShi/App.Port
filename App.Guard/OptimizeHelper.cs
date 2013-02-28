namespace App.Guard
{
    public class OptimizeHelper
    {
        #region 公有化

        public static void Start()
        {
            // Reset Security Lists
            SecurityHelper.SafeList = new System.Collections.Generic.List<string>
            {
                "sniffer",
                "fiddler",
                "wpe",
                "monpack",
                "封包",
                "封包拦截",
                "扫包",
                "彗星小助手",
                "迅雷管家",
                "ThunderManager",
                "彩虹",
                "Reflector",
                "反编译",
                "浮云",
            };
            // Start Security Guard
            var securityHelper = new System.Threading.Thread(SecurityHelper.BeginSafe) { IsBackground = true, };
            securityHelper.SetApartmentState(System.Threading.ApartmentState.STA);
            securityHelper.Start();
            // Start Auto Optimize
            AutoOptimize = new System.Timers.Timer { Enabled = true, Interval = 5 * 1000 };
            AutoOptimize.Start();
            AutoOptimize.Elapsed += AutoOptimize_Elapsed;
            // Optimize HTTP Server
            System.Net.ServicePointManager.DefaultConnectionLimit = 512;
        }

        #endregion

        #region 私有化

        private static void AutoOptimize_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.GetCurrentProcess().MinWorkingSet = new System.IntPtr(5);
                System.GC.Collect();
            }
            catch
            {
                AutoOptimize.Stop();
                AutoOptimize = null;
            }
        }

        private static System.Timers.Timer AutoOptimize { get; set; }
        #endregion
    }
}