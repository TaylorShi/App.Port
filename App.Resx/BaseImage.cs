namespace App.Resx
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
    }
}