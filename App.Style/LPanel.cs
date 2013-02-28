namespace App.Style
{
    public class LButton : HLable
    {
        private static System.Drawing.Color Linecolor { get; set; }
        private static System.Drawing.Color Linelight { get; set; }
        private static System.Drawing.Color Backcolor { get; set; }
        private static System.Drawing.Color Backlight { get; set; }
        private static System.Drawing.Color Forecolor { get; set; }
        private static System.Drawing.Color Forelight { get; set; }

        public LButton
            (
            System.Windows.Forms.Control parentpal,
            int lineint,
            string text,
            System.Drawing.Font font,
            System.Drawing.Size size,
            System.Drawing.Point point,
            System.Drawing.Color linecolor,
            System.Drawing.Color linelight,
            System.Drawing.Color backcolor,
            System.Drawing.Color backlight,
            System.Drawing.Color forecolor,
            System.Drawing.Color forelight,
            System.Windows.Forms.AnchorStyles anchorstyle
            )
        {
            Linecolor = linecolor;
            Linelight = linelight;
            Backcolor = backcolor;
            Backlight = backlight;
            Forecolor = forecolor;
            Forelight = forelight;


            var panelSec = new HPanel
                {
                    Size = size,
                    Location = point,
                    Anchor = anchorstyle,
                    BackColor = linecolor,
                };

            Text = text;
            Font = font;
            TextAlign = Helper.Align;
            Size = new System.Drawing.Size(panelSec.Size.Width - 2*lineint, panelSec.Size.Height - 2*lineint);
            Location = new System.Drawing.Point(lineint, lineint);
            ForeColor = Forecolor;
            BackColor = Backcolor;
            Anchor = Helper.AnchorFill;
            Cursor = Helper.HCursors;

            panelSec.Controls.Add(this);
            parentpal.Controls.Add(panelSec);

            MouseUp += LButton_MouseUp;
            MouseDown += LButton_MouseDown;
            MouseHover += LButton_MouseHover;
            MouseLeave += LButton_MouseLeave;
        }

        private static void LButton_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            EnterOrLeave((LButton) sender, false);
        }

        private static void LButton_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            EnterOrLeave((LButton) sender, true);
        }

        private static void LButton_MouseLeave(object sender, System.EventArgs e)
        {
            // EnterOrLeave((LButton)sender, false);
        }

        private static void LButton_MouseHover(object sender, System.EventArgs e)
        {
            // EnterOrLeave((LButton)sender, true);
        }

        private static void EnterOrLeave(System.Windows.Forms.Control ser, bool isEnter)
        {
            ser.ForeColor = isEnter ? Forelight : Forecolor;
            ser.BackColor = isEnter ? Backlight : Backcolor;
            ser.Parent.BackColor = isEnter ? Linelight : Linecolor;
        }
    }

    public class LRichBox : ERichBox
    {
        private static System.Drawing.Color Linecolor { get; set; }
        private static System.Drawing.Color Linelight { get; set; }
        private static System.Drawing.Color Backcolor { get; set; }
        private static System.Drawing.Color Backlight { get; set; }
        private static System.Drawing.Color Forecolor { get; set; }
        private static System.Drawing.Color Forelight { get; set; }

        public LRichBox
            (
            System.Windows.Forms.Control parentpal,
            int lineint,
            string preword,
            System.Drawing.Font font,
            System.Drawing.Size size,
            System.Drawing.Point point,
            System.Drawing.Color linecolor,
            System.Drawing.Color linelight,
            System.Drawing.Color backcolor,
            System.Drawing.Color backlight,
            System.Drawing.Color forecolor,
            System.Drawing.Color forelight,
            System.Windows.Forms.AnchorStyles anchorstyle
            )
        {
            Linecolor = linecolor;
            Linelight = linelight;
            Backcolor = backcolor;
            Backlight = backlight;
            Forecolor = forecolor;
            Forelight = forelight;

            var inputPal = new LPanel
                (
                parentpal,
                lineint,
                size,
                point,
                Linecolor,
                Backcolor,
                anchorstyle
                );

            Text = preword.ToSafeValue();
            Size = new System.Drawing.Size(inputPal.Size.Width - 8*lineint, inputPal.Size.Height - 4*lineint);
            Location = new System.Drawing.Point(4*lineint, 3*lineint);
            BackColor = Backcolor;
            ForeColor = Forecolor;
            Font = font;
            inputPal.Controls.Add(this);
            Leave += LRichBox_Leave;
            Enter += LRichBox_Enter;
        }

        private static void LRichBox_Enter(object sender, System.EventArgs e)
        {
            EnterOrLeave((LRichBox) sender, true);
        }

        private static void LRichBox_Leave(object sender, System.EventArgs e)
        {
            EnterOrLeave((LRichBox) sender, false);
        }

        private static void EnterOrLeave(System.Windows.Forms.Control ser, bool isEnter)
        {
            ser.ForeColor = isEnter ? Forelight : Forecolor;
            ser.Parent.BackColor = ser.BackColor = isEnter ? Backlight : Backcolor;
            ser.Parent.Parent.BackColor = isEnter ? Linelight : Linecolor;
        }
    }

    public class LInput : ETextBox
    {
        private static System.Drawing.Color Linecolor { get; set; }
        private static System.Drawing.Color Linelight { get; set; }
        private static System.Drawing.Color Backcolor { get; set; }
        private static System.Drawing.Color Backlight { get; set; }
        private static System.Drawing.Color Forecolor { get; set; }
        private static System.Drawing.Color Forelight { get; set; }

        public LInput
            (
            System.Windows.Forms.Control parentpal,
            int lineint,
            string preword,
            System.Drawing.Font font,
            System.Drawing.Size size,
            System.Drawing.Point point,
            System.Drawing.Color linecolor,
            System.Drawing.Color linelight,
            System.Drawing.Color backcolor,
            System.Drawing.Color backlight,
            System.Drawing.Color forecolor,
            System.Drawing.Color forelight,
            System.Windows.Forms.AnchorStyles anchorstyle
            )
        {
            Linecolor = linecolor;
            Linelight = linelight;
            Backcolor = backcolor;
            Backlight = backlight;
            Forecolor = forecolor;
            Forelight = forelight;

            var inputPal = new LPanel
                (
                parentpal,
                lineint,
                size,
                point,
                Linecolor,
                Backcolor,
                anchorstyle
                );

            Text = preword.ToSafeValue();
            Size = new System.Drawing.Size(inputPal.Size.Width - 8*lineint, inputPal.Size.Height - 4*lineint);
            Location = new System.Drawing.Point(4*lineint, 3*lineint);
            BackColor = Backcolor;
            ForeColor = Forecolor;
            Font = font;
            inputPal.Controls.Add(this);
            Leave += LInput_Leave;
            Enter += LInput_Enter;
        }

        private static void LInput_Enter(object sender, System.EventArgs e)
        {
            EnterOrLeave((LInput)sender, true);
        }

        private static void LInput_Leave(object sender, System.EventArgs e)
        {
            EnterOrLeave((LInput) sender, false);
        }

        private static void EnterOrLeave(System.Windows.Forms.Control ser, bool isEnter)
        {
            ser.ForeColor = isEnter ? Forelight : Forecolor;
            ser.Parent.BackColor = ser.BackColor = isEnter ? Backlight : Backcolor;
            ser.Parent.Parent.BackColor = isEnter ? Linelight : Linecolor;
        }
    }

    public class LLabel : HLaple
    {
        public LLabel
            (
            System.Windows.Forms.Control parentpal,
            string text,
            System.Drawing.Font font,
            System.Drawing.Size size,
            System.Drawing.Point point,
            System.Drawing.Color forecolor,
            System.Drawing.ContentAlignment alignment
            )
        {
            Text = text;
            Font = font;
            Size = size;
            Location = point;
            ForeColor = forecolor;
            TextAlign = alignment;
            parentpal.Controls.Add(this);
        }
    }
}