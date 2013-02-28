namespace App.Style
{

    #region ELabel
    
    public class ELabel : System.Windows.Forms.Label
    {
        public ELabel()
        {
            AutoSize = false;
            SetStyle(
                System.Windows.Forms.ControlStyles.UserPaint |
                System.Windows.Forms.ControlStyles.AllPaintingInWmPaint |
                System.Windows.Forms.ControlStyles.OptimizedDoubleBuffer |
                System.Windows.Forms.ControlStyles.ResizeRedraw |
                System.Windows.Forms.ControlStyles.Selectable |
                System.Windows.Forms.ControlStyles.SupportsTransparentBackColor, true
                );
        }
    }

    #endregion

    #region EFlyPal

    public class EFlyPal : System.Windows.Forms.FlowLayoutPanel
    {
        public EFlyPal()
        {
            BackColor = Helper.Transparent;
            SetStyle(
                System.Windows.Forms.ControlStyles.UserPaint |
                System.Windows.Forms.ControlStyles.AllPaintingInWmPaint |
                System.Windows.Forms.ControlStyles.OptimizedDoubleBuffer |
                System.Windows.Forms.ControlStyles.ResizeRedraw |
                System.Windows.Forms.ControlStyles.Selectable |
                System.Windows.Forms.ControlStyles.SupportsTransparentBackColor, true
                );
        }
    }

    #endregion

    #region EPanel

    public class EPanel : System.Windows.Forms.Panel
    {
        public EPanel()
        {
            BackColor = Helper.Transparent;
            SetStyle(
                System.Windows.Forms.ControlStyles.UserPaint |
                System.Windows.Forms.ControlStyles.AllPaintingInWmPaint |
                System.Windows.Forms.ControlStyles.OptimizedDoubleBuffer |
                System.Windows.Forms.ControlStyles.ResizeRedraw |
                System.Windows.Forms.ControlStyles.Selectable |
                System.Windows.Forms.ControlStyles.SupportsTransparentBackColor, true
                );
        }
    }

    #endregion

    #region EPicBox

    public class EPicBox : System.Windows.Forms.PictureBox
    {
        public EPicBox()
        {
            SetStyle(
                System.Windows.Forms.ControlStyles.UserPaint |
                System.Windows.Forms.ControlStyles.AllPaintingInWmPaint |
                System.Windows.Forms.ControlStyles.OptimizedDoubleBuffer |
                System.Windows.Forms.ControlStyles.ResizeRedraw |
                System.Windows.Forms.ControlStyles.Selectable |
                System.Windows.Forms.ControlStyles.SupportsTransparentBackColor, true
                );
        }
    }

    #endregion

    #region ERichBox

    public class ERichBox : System.Windows.Forms.RichTextBox
    {
        public ERichBox()
        {
            ImeMode = System.Windows.Forms.ImeMode.OnHalf;
            Multiline = true;
            BorderStyle = System.Windows.Forms.BorderStyle.None;
        }
    }

    #endregion

    #region ETxtBox

    public class ETextBox : System.Windows.Forms.TextBox
    {
        public ETextBox()
        {
            ImeMode = System.Windows.Forms.ImeMode.OnHalf;
            BorderStyle = System.Windows.Forms.BorderStyle.None;
        }
    }

    #endregion

    #region ECheckBox
    public class ECheckBox : System.Windows.Forms.CheckBox
    {
        public ECheckBox()
        {
            AutoSize = false;
            Cursor = Helper.HCursors;
        }
    } 
    #endregion

    #region ERadioButton
    public class ERadioButton : System.Windows.Forms.RadioButton
    {
        public ERadioButton()
        {
            AutoSize = false;
            Cursor = Helper.HCursors;
        }
    }
    #endregion
}