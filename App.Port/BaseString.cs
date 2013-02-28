namespace App.Port
{
    public static class BaseString
    {
        /// <summary>
        /// String -> Check Value -> GetBack -> String
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToSafeValue(this string value)
        {
            #region String -> Check Value -> GetBack -> String

            return value.IsNullOrEmptyOrSpace() ? "" : value.Trim();

            #endregion
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToClearHang(this string value)
        {
            return value.Replace("\r\n", "");
        }

        public static int ToInt(this string value)
        {
            int vkfl;
            return System.Int32.TryParse(value, out vkfl) ? vkfl : vkfl;
        }

        /// <summary>
        /// String -> Check Value -> IsNotNullOrEmpty
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNotNullOrEmpty(this string value)
        {
            #region String -> Check Value -> IsNotNullOrEmpty

            return !string.IsNullOrEmpty(value) && !string.IsNullOrWhiteSpace(value);

            #endregion
        }

        /// <summary>
        /// String -> Check Value -> IsNullOrEmptyOrSpace
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNullOrEmptyOrSpace(this string value)
        {
            #region String -> Check Value -> IsNullOrEmptyOrSpace

            return string.IsNullOrEmpty(value) && string.IsNullOrWhiteSpace(value);

            #endregion
        }
    }
}