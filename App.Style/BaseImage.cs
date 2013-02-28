namespace App.Style
{
    public static class BaseImage
    {
        /// <summary>
        /// Image -> IsEmptyImage
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public static bool IsEmptyImage(this System.Drawing.Image image)
        {
            #region Image -> IsEmptyImage

            return image == null;

            #endregion
        }

        /// <summary>
        /// Icon -> IsEmptyIcon
        /// </summary>
        /// <param name="icon"></param>
        /// <returns></returns>
        public static bool IsEmptyIcon(this System.Drawing.Icon icon)
        {
            #region Image -> IsEmptyImage

            return icon == null;

            #endregion
        }
		/// <summary>
		/// String -> Image
		/// </summary>
		/// <param name="s"></param>
		/// <returns></returns>
		public static System.Drawing.Image LoadLocalImage(this string s)
        {
            #region String -> Image
            if (s.IsExistFile())
            {
                try
                {
                    var img = System.Drawing.Image.FromFile(s);
                    System.Drawing.Image bmp = new System.Drawing.Bitmap(img);
                    img.Dispose();
                    return bmp;
                }
                catch
                {
                    return null;
                }
            }
            return null; 
            #endregion
        }
        /// <summary>
        /// WebPath -> Image
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
		public static System.Drawing.Image Webimage(this string s)
        {
            #region WebPath -> Image
            var wc = new System.Net.WebClient { Proxy = null };
            var filesavepath = System.Windows.Forms.Application.StartupPath + "\\" + System.Guid.NewGuid().ToString().Replace("-","");
            wc.DownloadFile(s, filesavepath);
            var img = System.Drawing.Image.FromFile(filesavepath);
            var bmp = new System.Drawing.Bitmap(img);
            filesavepath.ToDeleteFile();
            return bmp; 
            #endregion
        }
    }
}