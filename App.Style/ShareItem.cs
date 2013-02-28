using System.Linq;

namespace App.Style
{
    public class ShareItemBarFly : HFlyPal
    {
        public ShareItemBarFly()
        {
            Size = new System.Drawing.Size(694, 360);
        }
    }

    public class ShareItemBarPal : EPanel
    {
        public ShareItemBarPal()
        {
            Size = new System.Drawing.Size(525, 37);
        }
    }

    public class ShareItemBarBase : ELabel
    {
        public ShareItemBarBase(string key)
        {
            Text = key;
            Cursor = Helper.HCursors;
            ForeColor = Helper.All["ListNavBar"][0];
            BackColor = Helper.All["ListNavBar"][1];
            Font = new System.Drawing.Font(Helper.FontFang, 18F);
            TextAlign = Helper.Align;
            Dock = System.Windows.Forms.DockStyle.Fill;
            MouseDown += ListNavBarBase_MouseDown;
        }

        static void ListNavBarBase_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            foreach (var listNavBaritem in ((ShareItemBarBase)sender).Parent.Parent.Controls.OfType<ShareItemBarBase>().SelectMany(variable => (variable).Controls.OfType<ShareItemBarBase>()))
            {
                GetLeaveOrUp(listNavBaritem);
            }
            SetDownOrHover((ShareItemBarBase)sender);
        }


        private static void GetLeaveOrUp(System.Windows.Forms.Control control)
        {
            control.ForeColor = Helper.All["ListNavBar"][0];
            control.BackColor = Helper.All["ListNavBar"][1];
        }
        private static void SetDownOrHover(System.Windows.Forms.Control control)
        {
            control.ForeColor = Helper.All["ListNavBar"][2];
            control.BackColor = Helper.All["ListNavBar"][3];
        }
    }
    
}
