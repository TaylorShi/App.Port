using System.Linq;

namespace App.Style
{
    /// <summary>
    /// ELabel -> NavBarBase
    /// </summary>
    public class NavBarBase : ELabel
    {
        /// <summary>
        /// 基本配置信息
        /// </summary>
        public NavBarBase()
        {
            Cursor = Helper.HCursors;
            ForeColor = Helper.All["NavBar"][0];
            BackColor = Helper.All["NavBar"][1];
            Size = Helper.NavSize;
            MouseUp += NavBarBase_MouseUp;
            MouseLeave += NavBarBase_MouseLeave;
        }

        /// <summary>
        /// 鼠标移开时恢复原始配色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void NavBarBase_MouseLeave(object sender, System.EventArgs e)
        {
            GetLeaveOrUp((NavBarBase) sender);
        }

        /// <summary>
        /// 鼠标抬起时恢复原始配色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void NavBarBase_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            GetLeaveOrUp((NavBarBase) sender);
        }

        /// <summary>
        /// 执行鼠标抬起或离开动作
        /// </summary>
        /// <param name="navBar"></param>
        private static void GetLeaveOrUp(System.Windows.Forms.Control navBar)
        {
            navBar.ForeColor = Helper.All["NavBar"][0];
            navBar.BackColor = Helper.All["NavBar"][1];
        }
    }

    /// <summary>
    /// NavBarBase -> NavBar
    /// </summary>
    public class NavBar : NavBarBase
    {
        /// <summary>
        /// 基本配置信息
        /// </summary>
        public NavBar()
        {
            MouseDown += NavBar_MouseDown;
            MouseHover += NavBar_MouseHover;
        }

        /// <summary>
        /// 鼠标按下时改变控件配色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void NavBar_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            GetDownOrHover((NavBar) sender);
        }

        /// <summary>
        /// 鼠标移上时改变控件配色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void NavBar_MouseHover(object sender, System.EventArgs e)
        {
            GetDownOrHover((NavBar) sender);
        }

        /// <summary>
        /// 执行鼠标移上或按下动作
        /// </summary>
        /// <param name="navBar"></param>
        private static void GetDownOrHover(System.Windows.Forms.Control navBar)
        {
            navBar.ForeColor = Helper.All["NavBar"][2];
            navBar.BackColor = Helper.All["NavBar"][3];
        }
    }

    /// <summary>
    /// NavBarBase -> NavBarCustom
    /// </summary>
    public class NavBarCustom : NavBarBase
    {
        /// <summary>
        /// 定制配置信息
        /// </summary>
        public NavBarCustom()
        {
            MouseDown += NavBarCustom_MouseDown;
            MouseHover += NavBarCustom_MouseHover;
        }

        /// <summary>
        /// 鼠标按下时定制控件配色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void NavBarCustom_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            CustomDownOrHover((NavBarCustom) sender);
        }

        /// <summary>
        /// 鼠标移上时定制控件配色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void NavBarCustom_MouseHover(object sender, System.EventArgs e)
        {
            CustomDownOrHover((NavBarCustom) sender);
        }

        /// <summary>
        /// 执行鼠标移上或按下动作
        /// </summary>
        /// <param name="navBar"></param>
        private static void CustomDownOrHover(System.Windows.Forms.Control navBar)
        {
            navBar.ForeColor = Helper.All["NavBar"][4];
            navBar.BackColor = Helper.All["NavBar"][5];
        }
    }

    /// <summary>
    /// NavBarBase -> NavBarCmDrag
    /// </summary>
    public class NavBarCmDrag : NavBarBase
    {
        public NavBarCmDrag()
        {
            Text = @"I";
            TextAlign = Helper.Blign;
            Font = new System.Drawing.Font(Helper.FontWing, Helper.NavFontSize, System.Drawing.FontStyle.Bold);
            MouseDown += NavBarCuDrag_MouseDown;
            MouseHover += NavBarCmDrag_MouseHover;
        }

        private static void NavBarCmDrag_MouseHover(object sender, System.EventArgs e)
        {
            var navBar = (NavBarCmDrag) sender;
            navBar.ForeColor = Helper.All["NavBar"][2];
            navBar.BackColor = Helper.All["NavBar"][3];
        }

        private void NavBarCuDrag_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            ((System.Windows.Forms.Control) sender).Capture = false;
            var msg = System.Windows.Forms.Message.Create(Helper.Ui.Handle, 0x00A1, (System.IntPtr) 0x002,
                                                          System.IntPtr.Zero);
            base.WndProc(ref msg);
        }
    }

    /// <summary>
    /// NavBar -> Nav
    /// </summary>
    public class Nav : NavBar
    {
        public Nav(string navkey)
        {
            switch (navkey)
            {
                case @"NavMax":
                    {
                        Text = @"1";
                        TextAlign = Helper.Align;
                        Font = new System.Drawing.Font(Helper.FontWeb, Helper.NavFontSize);
                        Helper.Ui.SizeChanged += Ui_SizeChanged;
                    }
                    break;
                case @"NaxMin":
                    {
                        Text = @"0";
                        TextAlign = Helper.Align;
                        Font = new System.Drawing.Font(Helper.FontWeb, Helper.NavFontSize);
                    }
                    break;
                case @"NavTop":
                    {
                        Text = @"å";
                        TextAlign = Helper.Blign;
                        Font = new System.Drawing.Font(Helper.FontWing, Helper.NavFontSize);
                        Helper.Tip.SetToolTip(this, @"切换置顶");
                    }
                    break;
                case @"NavTray":
                    {
                        Text = @"æ";
                        TextAlign = Helper.Blign;
                        Font = new System.Drawing.Font(Helper.FontWing, Helper.NavFontSize);
                        Helper.Tip.SetToolTip(this, @"最小托盘");
                    }
                    break;
                case @"NavBack":
                    {
                        Text = @"ß";
                        TextAlign = Helper.Blign;
                        Font = new System.Drawing.Font(Helper.FontWing, Helper.NavFontSize);
                    }
                    break;
            }
            MouseClick += Nav_MouseClick;
        }

        private static void Ui_SizeChanged(object sender, System.EventArgs e)
        {
            switch (Helper.Ui.WindowState)
            {
                case System.Windows.Forms.FormWindowState.Normal:
                    {
                        foreach (
                            var nav in
                                Helper.Ui.Controls.OfType<AppNav>()
                                      .SelectMany(
                                          navPal => (navPal).Controls.OfType<Nav>().Where(nav => (nav).Text == @"2")))
                        {
                            (nav).Text = @"1";
                        }
                    }
                    break;
                case System.Windows.Forms.FormWindowState.Maximized:
                    {
                        foreach (
                            var nav in
                                Helper.Ui.Controls.OfType<AppNav>()
                                      .SelectMany(
                                          navPal => (navPal).Controls.OfType<Nav>().Where(nav => (nav).Text == @"1")))
                        {
                            (nav).Text = @"2";
                        }
                    }
                    break;
            }
        }

        private static void Nav_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button != System.Windows.Forms.MouseButtons.Left || e.Clicks < 0) return;
            var nav = (Nav) sender;
            switch (nav.Text)
            {
                    // NavMax
                case @"2":
                    {
                        nav.Text = @"1";
                        Helper.Ui.WindowState = System.Windows.Forms.FormWindowState.Normal;
                    }
                    break;
                case @"1":
                    {
                        nav.Text = @"2";
                        Helper.Ui.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                    }
                    break;
                    // NaxMin
                case @"0":
                    {
                        Helper.Ui.WindowState = System.Windows.Forms.FormWindowState.Minimized;
                    }
                    break;
                    // NavTop
                case @"ä":
                    {
                        nav.Text = @"å";
                        Helper.Ui.TopMost = false;
                    }
                    break;
                case @"å":
                    {
                        nav.Text = @"ä";
                        Helper.Ui.TopMost = true;
                    }
                    break;
                    // NavTray
                case @"æ":
                    {
                        Helper.Ui.Hide();
                    }
                    break;
                    // NavBack
                case @"ß":
                    {
                    }
                    break;
            }
        }
    }

    /// <summary>
    /// NavBarCustom -> NavCsm
    /// </summary>
    public class NavCsm : NavBarCustom
    {
        public NavCsm(string csmkey)
        {
            switch (csmkey)
            {
                case @"NavOff":
                    {
                        Text = @"r";
                        TextAlign = Helper.Align;
                        Font = new System.Drawing.Font(Helper.FontWeb, Helper.NavFontSize);
                    }
                    break;
            }
            MouseClick += NavCsm_MouseClick;
        }

        private static void NavCsm_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button != System.Windows.Forms.MouseButtons.Left || e.Clicks < 0) return;
            switch (((NavCsm) sender).Text)
            {
                    // NavOff
                case @"r":
                    {
                        System.Diagnostics.Process.GetCurrentProcess().Kill();
                    }
                    break;
            }
        }
    }

    /// <summary>
    /// Nav -> NavPal
    /// </summary>
    public class AppNav : EPanel
    {
        public AppNav(int key, bool isColor)
        {
            switch (key)
            {
                case 3:
                    {
                        Controls.Add(new Nav(@"NaxMin") {Location = new System.Drawing.Point(0, 0)});
                        Controls.Add(new Nav(@"NavMax") {Location = new System.Drawing.Point(Helper.NavSize.Width, 0)});
                        Controls.Add(new NavCsm(@"NavOff")
                            {
                                Location = new System.Drawing.Point(Helper.NavSize.Width*2, 0)
                            });
                    }
                    break;
                case 5:
                    {
                        Controls.Add(new Nav(@"NavTray") {Location = new System.Drawing.Point(0, 0)});
                        Controls.Add(new Nav(@"NavTop") {Location = new System.Drawing.Point(Helper.NavSize.Width, 0)});
                        Controls.Add(new Nav(@"NaxMin") {Location = new System.Drawing.Point(Helper.NavSize.Width*2, 0)});
                        Controls.Add(new Nav(@"NavMax") {Location = new System.Drawing.Point(Helper.NavSize.Width*3, 0)});
                        Controls.Add(new NavCsm(@"NavOff")
                            {
                                Location = new System.Drawing.Point(Helper.NavSize.Width*4, 0)
                            });
                    }
                    break;
                case 7:
                    {
                        Controls.Add(new NavBarCmDrag {Location = new System.Drawing.Point(0, 0)});
                        Controls.Add(new Nav(@"NavBack") {Location = new System.Drawing.Point(Helper.NavSize.Width, 0)});
                        Controls.Add(new Nav(@"NavTray")
                            {
                                Location = new System.Drawing.Point(Helper.NavSize.Width*2, 0)
                            });
                        Controls.Add(new Nav(@"NavTop") {Location = new System.Drawing.Point(Helper.NavSize.Width*3, 0)});
                        Controls.Add(new Nav(@"NaxMin") {Location = new System.Drawing.Point(Helper.NavSize.Width*4, 0)});
                        Controls.Add(new Nav(@"NavMax") {Location = new System.Drawing.Point(Helper.NavSize.Width*5, 0)});
                        Controls.Add(new NavCsm(@"NavOff")
                            {
                                Location = new System.Drawing.Point(Helper.NavSize.Width*6, 0)
                            });
                    }
                    break;
            }
            if (isColor)
            {
                BackColor = System.Drawing.Color.YellowGreen;
            }
            Anchor = System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Top;
            Size = new System.Drawing.Size(Helper.NavSize.Width*key, Helper.NavSize.Height);
            Location = new System.Drawing.Point(Helper.Ui.Width - Helper.NavSize.Width*key, 0);
        }
    }
}