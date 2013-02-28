

namespace App.Port
{
    public class LoadInstall
    {
        public static void LoadInstalls()
        {
            LoadStatic.MainPal = new Style.LPanel
                (
                Style.Helper.Ui,
                LoadStatic.LineInt,
                Style.Helper.Ui.Size,
                new System.Drawing.Point(0, 0),
                System.Drawing.Color.FromArgb(200, 200, 200),
                System.Drawing.Color.FromArgb(255, 255, 255),
                Style.Helper.AnchorFill
                );
            LoadNav.LoadNavs();
            LoadPortPal.LoadPortPals();
            LoadSerPort.Reset();
        }
    }
}