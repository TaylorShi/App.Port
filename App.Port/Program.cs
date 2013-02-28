using System.Linq;

namespace App.Port
{
    internal static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [System.STAThread]
        private static void Main()
        {
            #region 启动窗体

            var process = RuningInstance();
            if (process == null)
            {
                System.Windows.Forms.Application.EnableVisualStyles();
                System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);
                Style.Helper.Ui = new FrmMain();
                Style.Helper.Ui.Load += Load.Ui_Load;
                System.Windows.Forms.Application.Run(Style.Helper.Ui);
            }
            else
            {
                // 激活下这个窗体
                HandleRunningInstance(process);
            }

            #endregion
        }

        #region 调用Win32 API，并激活并程序的窗口，显示在最前端

        private const int SwShownomal = 1;

        [System.Runtime.InteropServices.DllImport("User32.dll")]
        private static extern bool ShowWindowAsync(System.IntPtr hWnd, int cmdShow);

        [System.Runtime.InteropServices.DllImport("User32.dll")]
        private static extern bool SetForegroundWindow(System.IntPtr hWnd);

        private static void HandleRunningInstance(System.Diagnostics.Process instance)
        {
            ShowWindowAsync(instance.MainWindowHandle, SwShownomal); //显示
            SetForegroundWindow(instance.MainWindowHandle); //当到最前端
        }

        private static System.Diagnostics.Process RuningInstance()
        {
            var currentProcess = System.Diagnostics.Process.GetCurrentProcess();
            var processes = System.Diagnostics.Process.GetProcessesByName(currentProcess.ProcessName);
            return
                processes.Where(process => process.Id != currentProcess.Id)
                         .FirstOrDefault(
                             process =>
                             System.Reflection.Assembly.GetExecutingAssembly().Location.Replace("/", "\\") ==
                             currentProcess.MainModule.FileName);
        }

        #endregion
    }
}