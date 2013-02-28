using System.Linq;

namespace App.Style
{
    public class NavCellFill : ELabel
    {
        private System.Drawing.Font FontModel { get; set; }
        private System.Drawing.Color ForeNormal { get; set; }
        private System.Drawing.Color BackNormal { get; set; }
        private System.Drawing.Color ForeLight { get; set; }
        private System.Drawing.Color BackLight { get; set; }

        public NavCellFill(string key, bool light, System.Drawing.Font font, System.Drawing.Color foreNormal,
                           System.Drawing.Color backNormal, System.Drawing.Color foreLight,
                           System.Drawing.Color backLight)
        {
            FontModel = font;
            ForeNormal = foreNormal;
            BackNormal = backNormal;
            ForeLight = foreLight;
            BackLight = backLight;

            Text = key;
            Font = FontModel;
            Cursor = Helper.HCursors;
            TextAlign = Helper.Align;
            ForeColor = light ? ForeLight : ForeNormal;
            BackColor = light ? BackLight : BackNormal;
            Dock = System.Windows.Forms.DockStyle.Fill;
            MouseDown += NavCellFill_MouseDown;
        }

        private void NavCellFill_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            foreach (
                var listNavBaritem in
                    ((NavCellFill) sender).Parent.Parent.Controls.OfType<EPanel>()
                                          .SelectMany(variable => (variable).Controls.OfType<NavCellFill>()))
            {
                GetLeaveOrUp(listNavBaritem);
            }
            SetDownOrHover((NavCellFill) sender);
        }

        private void GetLeaveOrUp(System.Windows.Forms.Control control)
        {
            control.ForeColor = ForeNormal;
            control.BackColor = BackNormal;
        }

        private void SetDownOrHover(System.Windows.Forms.Control control)
        {
            control.ForeColor = ForeLight;
            control.BackColor = BackLight;
        }
    }

    public class MLabel : HLable
    {
        private System.Drawing.Font FontModel { get; set; }
        private System.Drawing.Color ForeNormal { get; set; }
        private System.Drawing.Color BackNormal { get; set; }
        private System.Drawing.Color ForeLight { get; set; }
        private System.Drawing.Color BackLight { get; set; }

        public MLabel
            (
                string key,
                bool light, 
                System.Drawing.Font font,
                System.Drawing.Color foreNormal,
                System.Drawing.Color backNormal,
                System.Drawing.Color foreLight,
                System.Drawing.Color backLight,
                int sizewidth, 
                int sizehegiht
            )
        {
            FontModel = font;
            ForeNormal = foreNormal;
            BackNormal = backNormal;
            ForeLight = foreLight;
            BackLight = backLight;

            Text = key;
            Font = FontModel;
            Cursor = Helper.HCursors;
            TextAlign = Helper.Align;
            ForeColor = light ? ForeLight : ForeNormal;
            BackColor = light ? BackLight : BackNormal;
            Size = new System.Drawing.Size(sizewidth, sizehegiht);

            MouseHover += MLabel_MouseHover;
            MouseLeave += MLabel_MouseLeave;
        }

        private void MLabel_MouseLeave(object sender, System.EventArgs e)
        {
            GetLeaveOrUp((MLabel) sender);
        }

        private void MLabel_MouseHover(object sender, System.EventArgs e)
        {
            SetDownOrHover((MLabel) sender);
        }

        private void GetLeaveOrUp(System.Windows.Forms.Control control)
        {
            control.ForeColor = ForeNormal;
            control.BackColor = BackNormal;
        }

        private void SetDownOrHover(System.Windows.Forms.Control control)
        {
            control.ForeColor = ForeLight;
            control.BackColor = BackLight;
        }
    }

}