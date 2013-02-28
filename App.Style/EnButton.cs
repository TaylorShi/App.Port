using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Timer = System.Threading.Timer;

namespace App.Style
{
    public class EnButton : Label
    {
        private readonly int[] _alphas = new[] {0, 0, 0};
        private bool _enable = true;

        private bool _isDown;
        private bool _isGradientEffects = true;

        private bool _isShowBorder = true;
        private Color _lColor = Color.White;
        private Timer _timerEnter;
        private Timer _timerLeave;

        public EnButton()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.UserPaint |
                     ControlStyles.SupportsTransparentBackColor, true);
            AutoSize = false;
        }

        public override sealed bool AutoSize
        {
            get { return base.AutoSize; }
            set { base.AutoSize = value; }
        }

        public Color LColor
        {
            get { return _lColor; }
            set { _lColor = value; }
        }

        public bool Enable
        {
            get { return _enable; }
            set
            {
                _enable = value;
                _alphas[0] = 0;
                _alphas[1] = 0;
                _alphas[2] = 0;
                Invalidate();
            }
        }

        public bool IsShowBorder
        {
            get { return _isShowBorder; }
            set
            {
                _isShowBorder = value;
                Invalidate();
            }
        }

        public bool IsGradientEffects
        {
            get { return _isGradientEffects; }
            set { _isGradientEffects = value; }
        }

        public bool IsEnter { get; set; }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            //base.OnPaint(pevent);
            Graphics g = pevent.Graphics;
            using (var pen = new Pen(Color.FromArgb(_alphas[1], 72, 123, 152), 1))
            {
                using (GraphicsPath path = CreateRoundedRectanglePath(ClientRectangle, 2))
                {
                    g.DrawPath(pen, path);
                }


                var nBorder = new Rectangle(
                    ClientRectangle.X + 1,
                    ClientRectangle.Y + 1,
                    ClientRectangle.Width - 2,
                    ClientRectangle.Height - 2);

                if (!IsShowBorder)
                {
                    nBorder = new Rectangle(
                        ClientRectangle.X,
                        ClientRectangle.Y,
                        ClientRectangle.Width,
                        ClientRectangle.Height);
                }
                else
                {
                    var nBorderPen = new Pen(Color.FromArgb(100, LColor), 1);
                    g.DrawRectangle(nBorderPen, nBorder);
                }

                if (Enable)
                {
                    if (IsGradientEffects)
                    {
                        var linear = new LinearGradientBrush(nBorder, Color.FromArgb(_alphas[2], LColor),
                                                             Color.FromArgb(0, LColor), 90);
                        if (_isDown)
                            linear = new LinearGradientBrush(nBorder, Color.FromArgb(0, LColor),
                                                             Color.FromArgb(_alphas[2], LColor), 90);
                        g.FillRectangle(linear, nBorder);
                        linear.Dispose();
                    }
                    else
                    {
                        g.FillRectangle(
                            new SolidBrush(
                                Color.FromArgb(_alphas[2], LColor)), nBorder);
                    }
                }

                if (Enable)
                {
                    if (Image != null)
                    {
                        g.DrawImage(Image, new Point(
                                               ClientRectangle.X + ((ClientRectangle.Width - Image.Width)/2),
                                               ClientRectangle.Y + ((ClientRectangle.Height - Image.Height)/2)));
                    }
                }

                using (var textPath = new GraphicsPath())
                {
                    Size textSize = TextRenderer.MeasureText(Text, Font);

                    int tY = (ClientRectangle.Height - textSize.Height)/2 - 1;
                    int tX = (ClientRectangle.Width - textSize.Width)/2 - 1;
                    float emSize = Font.Size/0.74f;
                    textPath.AddString(Text, Font.FontFamily, (int) Font.Style, emSize, new Point(tX, tY),
                                       StringFormat.GenericDefault);

                    Blur.DrawOuterGlow2(g, textPath, 2, Color.FromArgb(75, Color.Black), Enable ? ForeColor : Color.Gray);
                }
            }
        }

        internal static GraphicsPath CreateRoundedRectanglePath(Rectangle rect, int cornerRadius)
        {
            var roundedRect = new GraphicsPath();
            roundedRect.AddArc(rect.X, rect.Y, cornerRadius*2, cornerRadius*2, 180, 90);
            roundedRect.AddLine(rect.X + cornerRadius, rect.Y, rect.Right - cornerRadius*2, rect.Y);
            roundedRect.AddArc(rect.X + rect.Width - cornerRadius*2, rect.Y, cornerRadius*2, cornerRadius*2, 270, 90);
            roundedRect.AddLine(rect.Right, rect.Y + cornerRadius*2, rect.Right, rect.Y + rect.Height - cornerRadius*2);
            roundedRect.AddArc(rect.X + rect.Width - cornerRadius*2, rect.Y + rect.Height - cornerRadius*2,
                               cornerRadius*2, cornerRadius*2, 0, 90);
            roundedRect.AddLine(rect.Right - cornerRadius*2, rect.Bottom, rect.X + cornerRadius*2, rect.Bottom);
            roundedRect.AddArc(rect.X, rect.Bottom - cornerRadius*2, cornerRadius*2, cornerRadius*2, 90, 90);
            roundedRect.AddLine(rect.X, rect.Bottom - cornerRadius*2, rect.X, rect.Y + cornerRadius*2);
            roundedRect.CloseFigure();
            return roundedRect;
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            IsEnter = true;
            InEffect();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            IsEnter = false;
            _isDown = false;
            OutEffect();
        }

        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            base.OnMouseDown(mevent);
            _isDown = true;
            Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            base.OnMouseUp(mevent);
            _isDown = false;
            Invalidate();
        }

        private void InEffect()
        {
            if (!Enable) return;
            if (_timerLeave != null)
                _timerLeave.Dispose();
            _alphas[0] = 0;
            _alphas[1] = 0;
            _alphas[2] = 0;
            _timerEnter = new Timer(delegate
                {
                    _alphas[0] += 20;
                    _alphas[1] += 15;
                    _alphas[2] += 10;
                    if (_alphas[2] >= 70)
                    {
                        _alphas[0] = 255;
                        _alphas[1] = 100;
                        _alphas[2] = 70;
                        Invalidate();
                        _timerEnter.Dispose();
                    }
                    Invalidate();
                }, null, 0, 20);
        }

        private void OutEffect()
        {
            if (!Enable) return;
            _alphas[0] = 255;
            _alphas[1] = 100;
            _alphas[2] = 70;
            if (_timerEnter != null)
                _timerEnter.Dispose();
            _timerLeave = new Timer(delegate
                {
                    _alphas[0] -= 20;
                    _alphas[1] -= 15;
                    _alphas[2] -= 10;
                    if (_alphas[2] <= 0)
                    {
                        _alphas[0] = 0;
                        _alphas[1] = 0;
                        _alphas[2] = 0;
                        Invalidate();
                        _timerLeave.Dispose();
                    }
                    Invalidate();
                }, null, 0, 20);
        }
    }

    public static class Blur
    {
        public static void DrawOuterGlow(Graphics gps, GraphicsPath gp, Rectangle rect, float size,
                                         Color lightColor,
                                         Color bodyColor)
        {
            var bm = new Bitmap(rect.Width/5, rect.Height/5);
            var g = Graphics.FromImage(bm);
            var mx = new Matrix(
                1.0f/5,
                0,
                0,
                1.0f/5,
                -(1.0f/5),
                -(1.0f/5));
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.Transform = mx;
            var p = new Pen(lightColor, size);
            g.DrawPath(p, gp);
            //g.FillPath(new SolidBrush(lightColor), gp);
            g.Dispose();
            gps.Transform = new Matrix();

            gps.SmoothingMode = SmoothingMode.AntiAlias;
            gps.InterpolationMode = InterpolationMode.HighQualityBicubic;
            gps.DrawImage(bm, rect, 0, 0, bm.Width, bm.Height, GraphicsUnit.Pixel);
            gps.FillPath(new SolidBrush(bodyColor), gp);
        }

        public static void DrawOuterGlow2(Graphics gps, GraphicsPath gp, float size, Color lightColor, Color bodyColor)
        {
            var p = new Pen(lightColor, size);

            gps.SmoothingMode = SmoothingMode.AntiAlias;
            gps.InterpolationMode = InterpolationMode.HighQualityBicubic;
            gps.DrawPath(p, gp);
            gps.FillPath(new SolidBrush(bodyColor), gp);
            p.Dispose();
        }

        public static Bitmap DrawOuterGlowReturnBitmap(Rectangle rect, GraphicsPath gp, float size, Color lightColor,
                                                       Color bodyColor)
        {
            var bitm = new Bitmap(rect.Width, rect.Height);
            var g = Graphics.FromImage(bitm);
            var p = new Pen(lightColor, size);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.DrawPath(p, gp);
            g.FillPath(new SolidBrush(bodyColor), gp);
            p.Dispose();
            g.Dispose();
            return bitm;
        }

        /// <summary>
        ///     剪切图片
        /// </summary>
        /// <param name="sourceBitmap"></param>
        /// <param name="x">剪切位置的左上角x坐标</param>
        /// <param name="y">剪切位置的左上角y坐标</param>
        /// <param name="width">要剪切的宽度</param>
        /// <param name="height">要剪切的高度</param>
        public static Bitmap Cut(Bitmap sourceBitmap, int x, int y, int width, int height)
        {
            //加载底图
            var img = sourceBitmap;
            var w = img.Width;
            var h = img.Height;
            //设置画布
            width = width >= w ? w : width;
            height = height >= h ? h : height;
            var map = new Bitmap(width, height);
            //绘图
            var g = Graphics.FromImage(map);
            g.DrawImage(img, 0, 0, new Rectangle(x, y, width, height), GraphicsUnit.Pixel);
            //保存
            return map;
        }
    }
}