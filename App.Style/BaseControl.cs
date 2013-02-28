using System.Linq;

namespace App.Style
{
    public class InputBoxWithDesc : ETextBox
    {
        private System.Drawing.Color Linecolor { get; set; }
        private System.Drawing.Color Linelight { get; set; }
        private System.Drawing.Color Backcolor { get; set; }
        private System.Drawing.Color Backlight { get; set; }
        private System.Drawing.Color Forecolor { get; set; }
        private System.Drawing.Color Forelight { get; set; }

        public InputBoxWithDesc
            (
            System.Windows.Forms.Control parentpal,
            int lineint,
            int lineflo,
            string predesc,
            string preword,
            System.Drawing.Font descfont,
            System.Drawing.Size descsize,
            System.Drawing.Font font,
            System.Drawing.Size size,
            System.Drawing.Point point,
            System.Drawing.Color defrcolor,
            System.Drawing.Color debkcolor,
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

            var inputdesc = new HDarge
                (
                parentpal,
                predesc,
                descfont,
                descsize,
                point,
                defrcolor,
                debkcolor,
                Helper.Align,
                anchorstyle
                );

            var inputPal = new LPanel
                (
                parentpal,
                lineint,
                size,
                new System.Drawing.Point(inputdesc.Location.X + inputdesc.Width, inputdesc.Location.Y),
                Linecolor,
                Backcolor,
                anchorstyle
                );

            Text = preword.ToSafeValue();
            Multiline = true;
            Size = new System.Drawing.Size(inputPal.Size.Width - 8*lineint, inputPal.Size.Height - 4*lineint);
            Location = new System.Drawing.Point(4*lineint, (4 + lineflo)*lineint);
            BackColor = Backcolor;
            ForeColor = Forecolor;
            Font = font;
            Anchor = anchorstyle;

            Leave += LInput_Leave;
            Enter += LInput_Enter;

            inputPal.Controls.Add(this);
        }

        private void LInput_Enter(object sender, System.EventArgs e)
        {
            EnterOrLeave((InputBoxWithDesc) sender, true);
        }

        private void LInput_Leave(object sender, System.EventArgs e)
        {
            EnterOrLeave((InputBoxWithDesc) sender, false);
        }

        private void EnterOrLeave(System.Windows.Forms.Control ser, bool isEnter)
        {
            ser.ForeColor = isEnter ? Forelight : Forecolor;
            ser.Parent.BackColor = ser.BackColor = isEnter ? Backlight : Backcolor;
            ser.Parent.Parent.BackColor = isEnter ? Linelight : Linecolor;
        }
    }
    public class RadioSigleBox:ECheckBox
    {
        private HLabel Radiotap { get; set; }
        private bool Single { get; set; }
        private string Group { get; set; }
        public string Value { get; set; }
        
        public RadioSigleBox
        (
            System.Windows.Forms.Control parentpal,
            string rdbtext,
            bool isChecked,
            bool single,
            string group,
            float fla,
            int flo,
            System.Drawing.Size size,
            System.Drawing.Point point,
            System.Drawing.Font font,
            System.Drawing.Color forecolor,
            System.Drawing.Color backcolor,
            System.Windows.Forms.AnchorStyles anchorstyle,
            System.Windows.Forms.MouseEventHandler touchRadio
        )
        {
            Group = group;
            Single = single;
            Size = size;
            Location = point;
            Tag = Group;
            Value = rdbtext;
            Checked = isChecked;

            Radiotap = new HLabel
                (
                this,
                isChecked ? @"þ" : @"¨",
                new System.Drawing.Font(Helper.FontWing, fla),
                new System.Drawing.Size(size.Height - 6 - flo, size.Height),
                new System.Drawing.Point(0, 0),
                forecolor,
                backcolor,
                Helper.Align,
                anchorstyle
                );
            var radiodesc = new HLabel
                (
                this,
                rdbtext,
                font,
                new System.Drawing.Size(size.Width - Radiotap.Width, size.Height),
                new System.Drawing.Point(Radiotap.Location.X + Radiotap.Width, -1),
                forecolor,
                backcolor,
                System.Drawing.ContentAlignment.MiddleLeft,
                anchorstyle
                );
            Radiotap.MouseDown += touchRadio;
            radiodesc.MouseDown += touchRadio;
            CheckedChanged += RadioSigleBox_CheckedChanged;
            parentpal.Controls.Add(this);
        }

        private void RadioSigleBox_CheckedChanged(object sender, System.EventArgs e)
        {
            Radiotap.Text = ((RadioSigleBox)sender).Checked ? @"þ" : @"¨";
        }
    }
    public class CheckSingleBox : ECheckBox
    {
        private HLabel Radiotap { get; set; }
        private bool Single { get; set; }
        private string Group { get; set; }
        public string Value { get; set; }
        

        public CheckSingleBox
            (
            System.Windows.Forms.Control parentpal,
            string rdbtext,
            bool isChecked,
            bool single,
            string group,
            float fla,
            int flo,
            System.Drawing.Size size,
            System.Drawing.Point point,
            System.Drawing.Font font,
            System.Drawing.Color forecolor,
            System.Drawing.Color backcolor,
            System.Windows.Forms.AnchorStyles anchorstyle
            )
        {
            Group = group;
            Single = single;
            Size = size;
            Location = point;
            Tag = Group;
            Value = rdbtext;
            Checked = isChecked;

            Radiotap = new HLabel
                (
                this,
                isChecked ? @"þ" : @"¨",
                new System.Drawing.Font(Helper.FontWing, fla),
                new System.Drawing.Size(size.Height - 6 - flo, size.Height),
                new System.Drawing.Point(0, 0),
                forecolor,
                backcolor,
                Helper.Align,
                anchorstyle
                );
            var radiodesc = new HLabel
                (
                this,
                rdbtext,
                font,
                new System.Drawing.Size(size.Width - Radiotap.Width, size.Height),
                new System.Drawing.Point(Radiotap.Location.X + Radiotap.Width, -1),
                forecolor,
                backcolor,
                System.Drawing.ContentAlignment.MiddleLeft,
                anchorstyle
                );
            Radiotap.MouseDown += RadioButton_MouseDown;
            radiodesc.MouseDown += RadioButton_MouseDown;
            CheckedChanged += CheckSingleBox_CheckedChanged;
            parentpal.Controls.Add(this);
        }

        private void CheckSingleBox_CheckedChanged(object sender, System.EventArgs e)
        {
            Radiotap.Text = ((CheckSingleBox) sender).Checked ? @"þ" : @"¨";
            var ser = (CheckSingleBox) sender;
            var parevalue = ((CheckMultiBox) ser.Parent.Parent.Parent.Parent).CheckValue;
            switch (((CheckSingleBox) sender).Checked)
            {
                case true:
                    {
                        if (!Single)
                        {
                            if (!parevalue.Contains(Value))
                            {
                                parevalue.Add(Value);
                            }
                        }
                        else
                        {
                            parevalue.Clear();
                            parevalue.Add(Value);
                        }
                    }
                    break;
                case false:
                    {
                        if (parevalue.Contains(Value))
                        {
                            parevalue.Remove(Value);
                        }
                    }
                    break;
            }
        }

        private void RadioButton_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            var ser = (HLabel) sender;
            switch (Radiotap.Text)
            {
                case @"þ":
                    {
                        ((CheckSingleBox) ser.Parent).Checked = false;
                    }
                    break;
                case @"¨":
                    {
                        if (Single)
                        {
                            foreach (
                                var variable in
                                    ser.Parent.Parent.Controls.OfType<CheckSingleBox>()
                                       .Where(variable => (string) variable.Tag == Group))
                            {
                                (variable).Checked = false;
                            }
                        }
                        ((CheckSingleBox) ser.Parent).Checked = true;
                    }
                    break;
            }
        }
    }

    public class CheckMultiBox : HPanel
    {
        public System.Collections.Generic.List<string> CheckValue { get; set; }

        public void GetThisClear(bool all)
        {
            foreach (
                var m in
                    Controls.OfType<HPanel>()
                            .SelectMany(
                                t =>
                                (t).Controls.OfType<LPanel>()
                                   .SelectMany(
                                       k =>
                                       (k).Controls.OfType<LFlyPal>()
                                          .SelectMany(n => (n).Controls.OfType<CheckSingleBox>()))))
            {
                (m).Checked = all;
            }
        }

        public void GetThisCheck(string bychecked,bool check)
        {
            foreach (
                var m in
                    Controls.OfType<HPanel>()
                            .SelectMany(
                                t =>
                                (t).Controls.OfType<LPanel>()
                                   .SelectMany(
                                       k =>
                                       (k).Controls.OfType<LFlyPal>()
                                          .SelectMany(
                                              n =>
                                              (n).Controls.OfType<CheckSingleBox>().Where(m => (m).Value == bychecked))))
                )
            {
                (m).Checked = check;
            }
        }

        public CheckMultiBox
            (
            System.Windows.Forms.Control parentpal,
            string[] values,
            string group,
            bool single,
            float celldesc,
            float cellfit,
            int lineint,
            int lineflo,
            int descsize,
            System.Drawing.Size cellsize,
            System.Drawing.Size allsize,
            System.Drawing.Point point,
            System.Drawing.Font font,
            System.Drawing.Color pabgcolor,
            System.Drawing.Color linecolor,
            System.Drawing.Color foreColor,
            System.Drawing.Color fodecolor,
            System.Windows.Forms.AnchorStyles anchorstyle
            )
        {
            Size = allsize;
            Location = point;
            Anchor = anchorstyle;
            CheckValue = new System.Collections.Generic.List<string>();
            if (!single)
            {
                CheckValue.Add(values[0]);
            }

            new HDarge
                (
                this,
                group,
                font,
                new System.Drawing.Size(descsize, allsize.Height),
                new System.Drawing.Point(0, 0),
                pabgcolor,
                foreColor,
                Helper.Align,
                anchorstyle
                );
            var checkMultiPal = new LPanel
                (
                this,
                lineint,
                new System.Drawing.Size(allsize.Width - descsize, allsize.Height),
                new System.Drawing.Point(descsize, 0),
                linecolor,
                fodecolor,
                anchorstyle
                );
            var flyPal = new LFlyPal
                (
                checkMultiPal,
                new System.Drawing.Size(checkMultiPal.Width - 10, allsize.Height),
                new System.Drawing.Point(10, lineflo)
                );
            foreach (var value in values)
            {
                if (single)
                {
                    new CheckSingleBox
                        (
                        flyPal,
                        value,
                        false,
                        true,
                        group,
                        celldesc,
                        1,
                        cellsize,
                        new System.Drawing.Point(0, 0),
                        new System.Drawing.Font(Helper.FontSegoe, cellfit),
                        foreColor,
                        fodecolor,
                        Helper.AnchorTopRight
                        );
                }
                else
                {
                    if (value == values[0])
                    {
                        new CheckSingleBox
                            (
                            flyPal,
                            value,
                            true,
                            false,
                            group,
                            celldesc,
                            1,
                            cellsize,
                            new System.Drawing.Point(0, 0),
                            new System.Drawing.Font(Helper.FontSegoe, cellfit),
                            foreColor,
                            fodecolor,
                            Helper.AnchorTopRight
                            );
                    }
                    else
                    {
                        new CheckSingleBox
                            (
                            flyPal,
                            value,
                            false,
                            false,
                            group,
                            celldesc,
                            1,
                            cellsize,
                            new System.Drawing.Point(0, 0),
                            new System.Drawing.Font(Helper.FontSegoe, cellfit),
                            foreColor,
                            fodecolor,
                            Helper.AnchorTopRight
                            );
                    }
                }
            }
            parentpal.Controls.Add(this);
        }
    }

}