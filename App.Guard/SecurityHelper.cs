using System.Linq;

namespace App.Guard
{
    public class SecurityHelper
    {
        #region 静态量
        public static System.Collections.Generic.List<string> SafeList { get; set; }

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool EnumWindows(Wndenumproc lpEnumFunc, int lParam);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern int GetWindowTextW(System.IntPtr hWnd,
                                                 [System.Runtime.InteropServices.MarshalAs(
                                                     System.Runtime.InteropServices.UnmanagedType.LPWStr)] System.Text.StringBuilder lpString,
                                                 int nMaxCount);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern int GetClassNameW(System.IntPtr hWnd,
                                                [System.Runtime.InteropServices.MarshalAs(
                                                    System.Runtime.InteropServices.UnmanagedType.LPWStr)] System.Text.StringBuilder lpString,
                                                int nMaxCount);

        #endregion

        #region 公有化

        public static void BeginSafe()
        {
            while (true)
            {
                if (
                    GatAllDesktopWindows()
                        .Any(item => SafeList.Any(str => item.SzWindowName.ToLower().Contains(str))))
                {
                    foreach (System.Windows.Forms.Form frm in System.Windows.Forms.Application.OpenForms)
                    {
                        var frm1 = frm;
                        frm.Invoke(new System.Windows.Forms.MethodInvoker(delegate { frm1.Visible = false; }));
                    }
                    System.Diagnostics.Process.GetCurrentProcess().Kill();
                    return;
                }
                System.Threading.Thread.Sleep(1000);
            }
        }

        #endregion

        #region 私有化

        private static System.Collections.Generic.IEnumerable<WindowInfo> GatAllDesktopWindows()
        {
            var wndList = new System.Collections.Generic.List<WindowInfo>();

            //enum all desktop windows  
            EnumWindows(delegate(System.IntPtr hWnd, int lParam)
                {
                    var wnd = new WindowInfo();
                    var sb = new System.Text.StringBuilder(256);
                    //get hwnd  
                    //get window name  
                    GetWindowTextW(hWnd, sb, sb.Capacity);
                    wnd.SzWindowName = sb.ToString();
                    //get window class  
                    GetClassNameW(hWnd, sb, sb.Capacity);
                    //add it into list 你可以在这里修改 过滤你要的控件进入列表
                    wndList.Add(wnd);
                    return true;
                }, 0);
            return wndList.ToArray();
        }

        private delegate bool Wndenumproc(System.IntPtr hWnd, int lParam);

        private struct WindowInfo
        {
            public string SzWindowName;
        }

        #endregion
    }
}