namespace App.Port
{
    public class LoadNav
    {
        public static void LoadNavs()
        {
            if (LoadStatic.MainPal.IsEmpty()) return;

            var navmin = new Style.HLabel
                (
                LoadStatic.MainPal,
                @"-",
                new System.Drawing.Font("Symbol", 22F),
                new System.Drawing.Size(35, 39),
                new System.Drawing.Point(LoadStatic.MainPal.Width - 40-38, -3),
                System.Drawing.Color.FromArgb(80, 80, 80),
                Style.Helper.Transparent,
                System.Drawing.ContentAlignment.TopLeft,
                Style.Helper.AnchorTopRight
                ) {Cursor = Style.Helper.HCursors};

            navmin.MouseClick += navmin_MouseClick;
            var navoff = new Style.HLabel
                (
                LoadStatic.MainPal,
                @"´",
                new System.Drawing.Font("Symbol", 24F),
                new System.Drawing.Size(38, 43),
                new System.Drawing.Point(LoadStatic.MainPal.Width-40, -5),
                System.Drawing.Color.FromArgb(80, 80, 80),
                Style.Helper.Transparent,
                System.Drawing.ContentAlignment.TopLeft,
                Style.Helper.AnchorTopRight
                ) {Cursor = Style.Helper.HCursors};
            navoff.MouseClick += navoff_MouseClick;
        }

        private static void navmin_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button != System.Windows.Forms.MouseButtons.Left || e.Clicks < 0) return;
            Style.Helper.Ui.WindowState = System.Windows.Forms.FormWindowState.Minimized;
        }

        private static void navoff_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button != System.Windows.Forms.MouseButtons.Left || e.Clicks < 0) return;
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }
    }
}