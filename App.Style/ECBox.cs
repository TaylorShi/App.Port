namespace App.Style
{
    public class EnCheckBox : System.Windows.Forms.CheckBox
    {
        readonly System.Windows.Forms.Timer _antiTimer = new System.Windows.Forms.Timer();

        int _offsetX;

        private System.Drawing.Size _scrollSize = new System.Drawing.Size(45, 15);

        public System.Drawing.Size ScrollSize
        {
            get { return _scrollSize; }
            set
            {
                _scrollSize = value;
                Invalidate();
            }
        }

        private bool _isShowBorder = true;

        public bool IsShowBorder
        {
            get { return _isShowBorder; }
            set
            {
                _isShowBorder = value;
                Invalidate();
            }
        }
        public EnCheckBox()
        {
            AutoSize = false;
            Size = new System.Drawing.Size(100, 39);
            SetStyle(
                System.Windows.Forms.ControlStyles.UserPaint |
                System.Windows.Forms.ControlStyles.AllPaintingInWmPaint |
                System.Windows.Forms.ControlStyles.OptimizedDoubleBuffer |
                System.Windows.Forms.ControlStyles.ResizeRedraw, true);
            UpdateStyles();
            _antiTimer.Tick += antiTimer_Tick;
            _antiTimer.Interval = 10;
        }

        void antiTimer_Tick(object sender, System.EventArgs e)
        {
            if (Checked)
            {
                _offsetX += _antiTimer.Interval;
                if (_antiTimer.Interval != 1)
                    _antiTimer.Interval--;
                if (_offsetX >= ScrollSize.Width - ScrollSize.Height)
                {
                    _offsetX = ScrollSize.Width - ScrollSize.Height;
                    _antiTimer.Enabled = false;
                }
            }
            else
            {
                _offsetX -= _antiTimer.Interval;
                if (_antiTimer.Interval != 1)
                    _antiTimer.Interval--;
                if (_offsetX <= 0)
                {
                    _offsetX = 0;
                    _antiTimer.Enabled = false;
                }
            }
            Invalidate();
        }

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs pevent)
        {
            base.OnPaint(pevent);
            base.OnPaintBackground(pevent);
            pevent.Graphics.FillRectangle(new System.Drawing.SolidBrush(BackColor), ClientRectangle);

            System.Windows.Forms.TextRenderer.DrawText(
                pevent.Graphics,
                Text,
                Font,
                new System.Drawing.Rectangle(
                    0, 0, Width - ScrollSize.Width - 2, Height),
                ForeColor,
                System.Windows.Forms.TextFormatFlags.VerticalCenter |
                System.Windows.Forms.TextFormatFlags.Left |
                System.Windows.Forms.TextFormatFlags.SingleLine |
                System.Windows.Forms.TextFormatFlags.WordEllipsis);

            var boxRect = new System.Drawing.Rectangle(
                new System.Drawing.Point(Width - ScrollSize.Width - 2, (Height - ScrollSize.Height) / 2),
                ScrollSize);
            pevent.Graphics.FillRectangle(
                new System.Drawing.SolidBrush(System.Drawing.Color.Silver),
                boxRect);

            System.Drawing.Rectangle sBoxRect;
            if (Checked)
            {
                sBoxRect = new System.Drawing.Rectangle(
                    new System.Drawing.Point(boxRect.X + _offsetX, (Height - (ScrollSize.Height + 6)) / 2),
                    new System.Drawing.Size(ScrollSize.Height, ScrollSize.Height + 6));
            }
            else
            {
                sBoxRect = new System.Drawing.Rectangle(
                    new System.Drawing.Point(boxRect.X + _offsetX, (Height - (ScrollSize.Height + 6)) / 2),
                    new System.Drawing.Size(ScrollSize.Height, ScrollSize.Height + 6));
            }
            if (IsShowBorder)
            {
                using (var pen = new System.Drawing.Pen(System.Drawing.Color.Black, 2))
                {
                    pevent.Graphics.DrawRectangle(pen, boxRect);
                    pevent.Graphics.DrawRectangle(pen, sBoxRect);
                }
            }

            var boxBackColor = System.Drawing.Color.YellowGreen;
            pevent.Graphics.FillRectangle(
                new System.Drawing.SolidBrush(System.Drawing.Color.YellowGreen),
                new System.Drawing.Rectangle(
                    sBoxRect.X + (IsShowBorder ? 1 : 0),
                    sBoxRect.Y + (IsShowBorder ? 1 : 0),
                    sBoxRect.Width - (IsShowBorder ? 2 : 0),
                    sBoxRect.Height - (IsShowBorder ? 2 : 0)));
            var sub = IsShowBorder ? boxRect.Left + 2 : boxRect.Left;
            pevent.Graphics.FillRectangle(
                new System.Drawing.SolidBrush(boxBackColor),
                new System.Drawing.Rectangle(
                    boxRect.X + (IsShowBorder ? 1 : 0),
                    boxRect.Y + (IsShowBorder ? 1 : 0),
                    sBoxRect.Left - sub,
                    boxRect.Height - (IsShowBorder ? 2 : 0)));
        }

        protected override void OnCheckedChanged(System.EventArgs e)
        {
            base.OnCheckedChanged(e);
            _antiTimer.Interval = 6;
            _antiTimer.Stop();
            _antiTimer.Start();
        }
    }
}
