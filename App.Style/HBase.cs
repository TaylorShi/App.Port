namespace App.Style
{
    public class HFlyPal : EFlyPal
    {
        public HFlyPal()
        {
            AllowDrop = true;
            DragEnter += HFlyPal_DragEnter;
            MouseDown += HFlyPal_MouseDown;
        }

        private void HFlyPal_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            ((System.Windows.Forms.Control)sender).Capture = false;
            var msg = System.Windows.Forms.Message.Create(Helper.Ui.Handle, 0x00A1, (System.IntPtr)0x002,
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

    public class HTxtBox : ETextBox
    {
        public HTxtBox(string tip)
        {
            Multiline = false;
            _tbtip = Text = tip;
            MouseLeave += HTxtBox_MouseLeave;
            MouseDown += HTxtBox_MouseDown;
        }

        #region DownOrLeave
        private static string _tbtip;
        private static HTxtBox _hTxtBox;
        private static void HTxtBox_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            _hTxtBox = (HTxtBox)sender;
            if (_hTxtBox.Text == _tbtip)
            {
                _hTxtBox.Text = "";
            }
        }

        private static void HTxtBox_MouseLeave(object sender, System.EventArgs e)
        {
            _hTxtBox = (HTxtBox)sender;
            if (_hTxtBox.Text != "") return;
            System.Threading.Thread.Sleep(200);
            if (_hTxtBox.Text == "")
            {
                _hTxtBox.Text = _tbtip;
            }
        } 
        #endregion
    }

    public partial class HPicBox : EPicBox
    {
        public HPicBox(string key)
        {
            Image = new Resx.ResourcesHelper().GetImage(key);
            SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
        }
    }

    public class HPanel:EPanel
    {
        public HPanel()
        {
            AllowDrop = true;
            DragEnter += HPanel_DragEnter;
            MouseDown += HPanel_MouseDown;
        }

        private void HPanel_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            ((System.Windows.Forms.Control)sender).Capture = false;
            var msg = System.Windows.Forms.Message.Create(Helper.Ui.Handle, 0x00A1, (System.IntPtr)0x002,
                                                          System.IntPtr.Zero);
            base.WndProc(ref msg);
        }

        private static void HPanel_DragEnter(object sender, System.Windows.Forms.DragEventArgs e)
        {
            #region DragEnter

            if (e.Data.GetDataPresent(System.Windows.Forms.DataFormats.FileDrop))
                e.Effect = System.Windows.Forms.DragDropEffects.Copy | System.Windows.Forms.DragDropEffects.None;
            else
                e.Effect = System.Windows.Forms.DragDropEffects.None;

            #endregion
        }
    }

    public class HLaple:ELabel
    {
        public HLaple()
        {
            BackColor = Helper.Transparent;
            AllowDrop = true;
            DragEnter += HLaple_DragEnter;
            MouseDown += HLaple_MouseDown;
        }

        private void HLaple_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            ((System.Windows.Forms.Control)sender).Capture = false;
            var msg = System.Windows.Forms.Message.Create(Helper.Ui.Handle, 0x00A1, (System.IntPtr)0x002,
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

    public class HLable:ELabel
    {
        public HLable()
        {
            BackColor = Helper.Transparent;
            AllowDrop = true;
            DragEnter += HLable_DragEnter;
        }

        private static void HLable_DragEnter(object sender, System.Windows.Forms.DragEventArgs e)
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
