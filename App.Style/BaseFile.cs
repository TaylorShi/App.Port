namespace App.Style
{
    public static class BaseFile
    {
        /// <summary>
        /// String -> Check Path -> IsExistFile
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool IsExistFile(this string path)
        {
            #region String -> Check Path -> IsExistFile

            return !Resx.BaseString.IsNullOrEmptyOrSpace(path) && System.IO.File.Exists(path);

            #endregion
        }

        /// <summary>
        /// String -> Check Dir -> IsExistDir
        /// </summary>
        /// <param name="dir"></param>
        /// <returns></returns>
        public static bool IsExistDir(this string dir)
        {
            #region String -> Check Dir -> IsExistDir

            return !Resx.BaseString.IsNullOrEmptyOrSpace(dir) && System.IO.Directory.Exists(dir);

            #endregion
        }

        /// <summary>
        /// String -> File -> ToDelete
        /// </summary>
        /// <param name="path"></param>
        public static void ToDeleteFile(this string path)
        {
            #region String -> File -> ToDelete

            if (!path.IsExistFile()) return;
            try
            {
                System.IO.File.Delete(path);
            }
                // ReSharper disable EmptyGeneralCatchClause
            catch
                // ReSharper restore EmptyGeneralCatchClause
            {
            }

            #endregion
        }

        /// <summary>
        /// String -> Dir -> ToDelete
        /// </summary>
        /// <param name="dir"></param>
        public static void ToDeleteDir(this string dir)
        {
            #region String -> Dir -> ToDelete

            if (!dir.IsExistDir()) return;
            try
            {
                System.IO.Directory.Delete(dir);
            }
                // ReSharper disable EmptyGeneralCatchClause
            catch
                // ReSharper restore EmptyGeneralCatchClause
            {
            }

            #endregion
        }

        /// <summary>
        /// String -> Dir -> ToCreatDir
        /// </summary>
        /// <param name="dir"></param>
        public static void ToCreatDir(this string dir)
        {
            #region String -> Dir -> ToCreatDir

            if (dir.IsExistDir()) return;
            try
            {
                System.IO.Directory.CreateDirectory(dir);
            }
                // ReSharper disable EmptyGeneralCatchClause
            catch
                // ReSharper restore EmptyGeneralCatchClause
            {
            }

            #endregion
        }

        /// <summary>
        /// String -> Path -> Ext
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string ToExt(this string path)
        {
            #region String -> Path -> Ext

            if (!Resx.BaseString.IsNullOrEmptyOrSpace(path) && path.IsExistFile())
            {
                var ext = System.IO.Path.GetExtension(path);
                return !Resx.BaseString.IsNullOrEmptyOrSpace(ext) ? ext : null;
            }
            return null;

            #endregion
        }

        /// <summary>
        /// String -> Path -> NameWithExt
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string ToNameWithExt(this string path)
        {
            #region String -> Path -> NameWithExt

            if (!Resx.BaseString.IsNullOrEmptyOrSpace(path) && path.IsExistFile())
            {
                var name = System.IO.Path.GetFileName(path);
                return !Resx.BaseString.IsNullOrEmptyOrSpace(name) ? name : null;
            }
            return null;

            #endregion
        }

        /// <summary>
        /// String -> Path -> NameNoExt
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string ToNameNoExt(this string path)
        {
            #region String -> Path -> ToNameNoExt

            if (!Resx.BaseString.IsNullOrEmptyOrSpace(path) && path.IsExistFile())
            {
                var name = System.IO.Path.GetFileNameWithoutExtension(path);
                return !Resx.BaseString.IsNullOrEmptyOrSpace(name) ? name : null;
            }
            return null;

            #endregion
        }
    }
}