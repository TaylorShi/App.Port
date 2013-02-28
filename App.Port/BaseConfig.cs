namespace App.Port
{
    public static class BaseConfig
    {
        /// <summary>
        /// String -> Check Value -> GetBack -> String
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string ToConfigValue(this string key)
        {
            #region String -> Check Value -> GetBack -> String

            return System.Configuration.ConfigurationManager.AppSettings[key].ToSafeValue();

            #endregion
        }
    }
}