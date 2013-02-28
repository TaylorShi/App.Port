namespace App.Port
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
            else
            {
                var realtivepath = System.Windows.Forms.Application.StartupPath + s;
                if (realtivepath.IsExistFile())
                {
                    try
                    {
                        var img = System.Drawing.Image.FromFile(realtivepath);
                        System.Drawing.Image bmp = new System.Drawing.Bitmap(img);
                        img.Dispose();
                        return bmp;
                    }
                    catch
                    {
                        return null;
                    }
                }
            }
            return null;

            #endregion
        }

        public static System.Drawing.Image Webimage(this string s)
        {
            var wc = new System.Net.WebClient {Proxy = null};
            var filesavepath = System.Windows.Forms.Application.StartupPath + "\\" + System.Guid.NewGuid();
            wc.DownloadFile(s, filesavepath);
            return System.Drawing.Image.FromFile(filesavepath);
        }
    }
}