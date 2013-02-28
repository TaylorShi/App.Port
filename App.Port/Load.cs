namespace App.Port
{
    public class Load
    {
        public static void Ui_Load(object sender, System.EventArgs e)
        {
            Style.Helper.Start();
            LoadInstall.LoadInstalls();
        }
    }
}