using System.Linq;

namespace App.Style
{
    public class StepPal : ELabel
    {
        private string Group { get; set; }
        public string Value { get; set; }
        private string[] Values { get; set; }

        public void SetStep(int i)
        {
            var ser = this;

            foreach (var vt in ser.Controls.OfType<Step>())
            {
                vt.GetLight();
                if (vt.Value == Values[i-1])
                {
                    break;
                }
            }
        }

        public StepPal
            (
            System.Windows.Forms.Control parentpal,
            string[] values,
            string group,
            int lightnum,
            bool independent,
            System.Drawing.Size size,
            System.Drawing.Point point,
            System.Drawing.Font font,
            System.Drawing.Color forecolor,
            System.Drawing.Color backcolor,
            System.Drawing.Color linecolor,
            System.Drawing.Color linelight,
            System.Windows.Forms.AnchorStyles anchorstyle
            )
        {
            Values = values;
            Group = group;
            Size = size;
            Location = point;
            Tag = Group;
            Value = values[lightnum];
            BackColor = backcolor;
            const int arrowwidth = 28;
            for (var i = 0; i < values.Length; i++)
            {
                new Step(
                        this,
                        values[i],
                        false,
                        new System.Drawing.Size(55, 33),
                        new System.Drawing.Point(55 * i + arrowwidth * i, 0),
                        font,
                        System.Drawing.Color.FromArgb(125, 125, 125),
                        System.Drawing.Color.FromArgb(25, 25, 25),
                        BackColor,
                        System.Drawing.Color.Transparent,
                        linelight,
                        anchorstyle
                        );
                if (independent)
                {
                    new Step(
                        this,
                        "à",
                        false,
                        new System.Drawing.Size(arrowwidth, 33),
                        new System.Drawing.Point(55 * (i + 1) + arrowwidth * i, 0),
                        new System.Drawing.Font("Wingdings", 15F),
                        System.Drawing.Color.FromArgb(177, 223, 244),
                        System.Drawing.Color.FromArgb(70, 180, 230),
                        System.Drawing.Color.Transparent,
                        System.Drawing.Color.Transparent,
                        System.Drawing.Color.Transparent,
                        anchorstyle
                        );
                }
                else
                {
                    new Step(
                     this,
                     "à",
                     false,
                     new System.Drawing.Size(arrowwidth, 33),
                     new System.Drawing.Point(55 * (i + 1) + arrowwidth * i, 0),
                     new System.Drawing.Font("Wingdings", 15F),
                     System.Drawing.Color.FromArgb(177, 223, 244),
                     System.Drawing.Color.FromArgb(70, 180, 230),
                     BackColor,
                     System.Drawing.Color.Transparent,
                     System.Drawing.Color.FromArgb(70, 180, 230),
                     anchorstyle
                     );
                }
            }
            MouseClick += StepPal_MouseClick;
            parentpal.Controls.Add(this);
        }

        private static void StepPal_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            var ser = (StepPal) sender;
            foreach (var vr in ser.Controls.OfType<Step>().Where(vr => vr.Value == "选项"))
            {
                vr.GetLight();
            }
        }
    }

    public class Step : EPanel
    {

        public string Value { get; set; }
        public System.Drawing.Color Linecolor { get; set; }
        public System.Drawing.Color Linelight { get; set; }
        public new System.Drawing.Color ForeColor { get; set; }
        public System.Drawing.Color ForeLight { get; set; }

        public void GetGray()
        {
            foreach (
                var hDarge in
                    from variable in Controls.OfType<HDarge>() let vt = variable where vt.Text == Value select variable)
            {
                (hDarge).ForeColor = ForeColor;
            }
            BackColor = Linecolor;
        }

        public void GetLight()
        {
            foreach (
                var hDarge in
                    from variable in Controls.OfType<HDarge>() let vt = variable where vt.Text == Value select variable)
            {
                (hDarge).ForeColor = ForeLight;
            }
            BackColor = Linelight;
        }

        public Step
            (
            System.Windows.Forms.Control parentpal,
            string rdotext,
            bool isChecked,
            System.Drawing.Size size,
            System.Drawing.Point point,
            System.Drawing.Font font,
            System.Drawing.Color forecolor,
            System.Drawing.Color forelight,
            System.Drawing.Color backcolor,
            System.Drawing.Color linecolor,
            System.Drawing.Color linelight,
            System.Windows.Forms.AnchorStyles anchorstyle
            )
        {
            Linecolor = linecolor;
            Linelight = linelight;
            ForeColor = forecolor;
            ForeLight = forelight;


            Value = rdotext;
            Size = size;
            Location = point;

            BackColor = isChecked ? Linelight : Linecolor;


            new HDarge
                (
                this,
                rdotext,
                font,
                new System.Drawing.Size(size.Width, size.Height - 2),
                new System.Drawing.Point(0, 0),
                isChecked ? ForeLight : ForeColor,
                backcolor,
                System.Drawing.ContentAlignment.MiddleLeft,
                anchorstyle
                );

            parentpal.Controls.Add(this);
        }
    }
}