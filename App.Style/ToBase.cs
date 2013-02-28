namespace App.Style
{
    public class HDarImg:EPicBox
    {
        public HDarImg
        (
            System.Windows.Forms.Control parentpal,
            System.Drawing.Size size,
            System.Drawing.Point point,
            System.Drawing.Image image,
            System.Windows.Forms.PictureBoxSizeMode pictureBoxSizeMode
        )
        {
            Size = size;
            Location = point;
            Image = image;
            SizeMode = pictureBoxSizeMode;

            AllowDrop = true;
            DragEnter += HDarImg_DragEnter;
            MouseDown += HDarImg_MouseDown;
            parentpal.Controls.Add(this);
        }

        private void HDarImg_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            ((System.Windows.Forms.Control)sender).Capture = false;
            var msg = System.Windows.Forms.Message.Create(Helper.Ui.Handle, 0x00A1, (System.IntPtr)0x002,
                                                          System.IntPtr.Zero);
            base.WndProc(ref msg);
        }

        private static void HDarImg_DragEnter(object sender, System.Windows.Forms.DragEventArgs e)
        {
            #region DragEnter

            if (e.Data.GetDataPresent(System.Windows.Forms.DataFormats.FileDrop))
                e.Effect = System.Windows.Forms.DragDropEffects.Copy | System.Windows.Forms.DragDropEffects.None;
            else
                e.Effect = System.Windows.Forms.DragDropEffects.None;

            #endregion
        }
    }

    public class HDarge : ELabel
    {
        public HDarge
            (
            System.Windows.Forms.Control parentpal,
            string predesc,
            System.Drawing.Font font,
            System.Drawing.Size size,
            System.Drawing.Point point,
            System.Drawing.Color forecolor,
            System.Drawing.Color backcolor,
            System.Drawing.ContentAlignment alignment,
            System.Windows.Forms.AnchorStyles anchorstyle
            )
        {
            Text = predesc;
            Font = font;
            Size = size;
            Location = point;
            ForeColor = forecolor;
            TextAlign = alignment;
            BackColor = backcolor;
            Anchor = anchorstyle;

            AllowDrop = true;
            DragEnter += HLaple_DragEnter;
            MouseDown += HLaple_MouseDown;

            parentpal.Controls.Add(this);
        }

        private void HLaple_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            ((System.Windows.Forms.Control) sender).Capture = false;
            var msg = System.Windows.Forms.Message.Create(Helper.Ui.Handle, 0x00A1, (System.IntPtr) 0x002,
                                                          System.IntPtr.Zero);
            base.WndProc(ref msg);
        }

        private static void HLaple_DragEnter(object sender, System.Windows.Forms.DragEventArgs e)
        {
            #region DragEnter

            if (e.Data.GetDataPresent(System.Windows.Forms.DataFormats.FileDrop))
                e.Effect = System.Windows.Forms.DragDropEffects.Copy | System.Windows.Forms.DragDropEffects.None;
            else
                e.Effect = System.Windows.Forms.DragDropEffects.None;

            #endregion
        }
    }

    public class LPanel : HLaple
    {
        public LPanel
            (
            System.Windows.Forms.Control parentpal,
            int lineint,
            System.Drawing.Size size,
            System.Drawing.Point point,
            System.Drawing.Color linecolor,
            System.Drawing.Color backcolor,
            System.Windows.Forms.AnchorStyles anchorstyle
            )
        {
            var panelSec = new HPanel
                {
                    Size = size,
                    Location = point,
                    Anchor = anchorstyle,
                    BackColor = linecolor,
                };
            Size = new System.Drawing.Size(panelSec.Size.Width - 2*lineint, panelSec.Size.Height - 2*lineint);
            Location = new System.Drawing.Point(lineint, lineint);
            BackColor = backcolor;
            Anchor = Helper.AnchorFill;

            panelSec.Controls.Add(this);
            parentpal.Controls.Add(panelSec);
        }
    }

    public class HLink : ELabel
    {
        private string Link { get; set; }
        public HLink
            (
            System.Windows.Forms.Control parentpal,
            string predesc,
            string prelink,
            System.Drawing.Font font,
            System.Drawing.Size size,
            System.Drawing.Point point,
            System.Drawing.Color forecolor,
            System.Drawing.Color backcolor,
            System.Drawing.ContentAlignment alignment,
            System.Windows.Forms.AnchorStyles anchorstyle
            )
        {
            Link = prelink;
            Text = predesc;
            Font = font;
            Size = size;
            Location = point;
            ForeColor = forecolor;
            TextAlign = alignment;
            BackColor = backcolor;
            Anchor = anchorstyle;
            Cursor = Helper.HCursors;

            AllowDrop = true;
            DragEnter += HLabel_DragEnter;
            parentpal.Controls.Add(this);
            MouseClick += HLink_MouseClick;
        }

        private void HLink_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (Link.IsNullOrEmptyOrSpace()) return;
            if (!Link.StartsWith("http://") && Link.StartsWith("www."))
            {
                Link = "http://" + Link;
            }
            try
            {
                System.Diagnostics.Process.Start(Link);
            }
            // ReSharper disable EmptyGeneralCatchClause
            catch
            // ReSharper restore EmptyGeneralCatchClause
            {
            }
        }

        private static void HLabel_DragEnter(object sender, System.Windows.Forms.DragEventArgs e)
        {
            #region DragEnter

            if (e.Data.GetDataPresent(System.Windows.Forms.DataFormats.FileDrop))
                e.Effect = System.Windows.Forms.DragDropEffects.Copy | System.Windows.Forms.DragDropEffects.None;
            else
                e.Effect = System.Windows.Forms.DragDropEffects.None;

            #endregion
        }
    }

    public class HLabel : ELabel
    {
        public HLabel
            (
            System.Windows.Forms.Control parentpal,
            string predesc,
            System.Drawing.Font font,
            System.Drawing.Size size,
            System.Drawing.Point point,
            System.Drawing.Color forecolor,
            System.Drawing.Color backcolor,
            System.Drawing.ContentAlignment alignment,
            System.Windows.Forms.AnchorStyles anchorstyle
            )
        {
            Text = predesc;
            Font = font;
            Size = size;
            Location = point;
            ForeColor = forecolor;
            TextAlign = alignment;
            BackColor = backcolor;
            Anchor = anchorstyle;

            AllowDrop = true;
            DragEnter += HLabel_DragEnter;
            parentpal.Controls.Add(this);
        }

        private static void HLabel_DragEnter(object sender, System.Windows.Forms.DragEventArgs e)
        {
            #region DragEnter

            if (e.Data.GetDataPresent(System.Windows.Forms.DataFormats.FileDrop))
                e.Effect = System.Windows.Forms.DragDropEffects.Copy | System.Windows.Forms.DragDropEffects.None;
            else
                e.Effect = System.Windows.Forms.DragDropEffects.None;

            #endregion
        }
    }

    public class LFlyPal : EFlyPal
    {
        public LFlyPal
            (
            System.Windows.Forms.Control parentpal,
            System.Drawing.Size size,
            System.Drawing.Point point
            )
        {
            Size = size;
            Location = point;
            AllowDrop = true;

            DragEnter += HFlyPal_DragEnter;
            MouseDown += HFlyPal_MouseDown;
            parentpal.Controls.Add(this);
        }

        private void HFlyPal_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            ((System.Windows.Forms.Control) sender).Capture = false;
            var msg = System.Windows.Forms.Message.Create(Helper.Ui.Handle, 0x00A1, (System.IntPtr) 0x002,
                                                          System.IntPtr.Zero);
            base.WndProc(ref msg);
        }

        private static void HFlyPal_DragEnter(object sender, System.Windows.Forms.DragEventArgs e)
        {
            #region DragEnter

            if (e.Data.GetDataPresent(System.Windows.Forms.DataFormats.FileDrop))
                e.Effect = System.Windows.Forms.DragDropEffects.Copy | System.Windows.Forms.DragDropEffects.None;
            else
                e.Effect = System.Windows.Forms.DragDropEffects.None;

            #endregion
        }
    }
}