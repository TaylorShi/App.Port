namespace App.FileIO
{
    public class ReadWriteHelper
    {

        #region Decrypt
        /// <summary>
        /// ImagePath -> Decrypt To Image
        /// </summary>
        /// <param name="imagePath"></param>
        /// <returns></returns>
        public static System.Drawing.Image DecryptImageFormFilePath(string imagePath)
        {
            #region ImagePath -> Decrypt To Image

            // Creat -> FileInfo
            var fileInfo = new System.IO.FileInfo(imagePath);
            // Creat -> Key && IV
            var enckey = EnDecryptHelper.StringMd5ShaToString(false,
                                                              string.Format("<{0}/>{1}</{2}>", ShareModel.Companyname,
                                                                            System.IO.Path.GetFileNameWithoutExtension(
                                                                                fileInfo.FullName), ShareModel.Companyname), 16,
                                                              false, 1,
                                                              System.Text.Encoding.UTF8);
            var enciv = EnDecryptHelper.StringMd5ShaToString(false,
                                                             string.Format("[{0}/]{1}[/{2}]", ShareModel.Companyname,
                                                                           System.IO.Path.GetFileNameWithoutExtension(
                                                                               fileInfo.FullName), ShareModel.Companyname), 16,
                                                             false, 1,
                                                             System.Text.Encoding.UTF8);
            // Encrypt -> Decrypt
            System.Drawing.Image img = null;
            try
            {
                var imgByte = EnDecryptHelper.ByteAesToByte(true, System.IO.File.ReadAllBytes(imagePath), enckey, enciv);
                using (var ms = new System.IO.MemoryStream(imgByte))
                {
                    ms.Position = 0;
                    img = System.Drawing.Image.FromStream(ms);
                }
            }
            catch
            {
                try
                {
                    var imgTemp = System.Drawing.Image.FromFile(imagePath);
                    img = new System.Drawing.Bitmap(imgTemp);
                    imgTemp.Dispose();
                }
                catch
                {
                    return img;
                }
            }
            return img;

            #endregion
        }

        /// <summary>
        /// XmlPath -> Decrypt To String
        /// </summary>
        /// <param name="xmlPath"></param>
        /// <returns></returns>
        public static string DecryptXmlFromFilePath(string xmlPath)
        {
            #region XmlPath -> Decrypt To String

            // Creat -> FileInfo
            var fileInfo = new System.IO.FileInfo(xmlPath);
            // Creat -> Key && IV
            var enckey = EnDecryptHelper.StringMd5ShaToString(false,
                                                              string.Format("<{0}/>{1}</{2}>", ShareModel.Companyname,
                                                                            System.IO.Path.GetFileNameWithoutExtension(
                                                                                fileInfo.FullName), ShareModel.Companyname), 16,
                                                              false, 1,
                                                              System.Text.Encoding.UTF8);
            var enciv = EnDecryptHelper.StringMd5ShaToString(false,
                                                             string.Format("[{0}/]{1}[/{2}]", ShareModel.Companyname,
                                                                           System.IO.Path.GetFileNameWithoutExtension(
                                                                               fileInfo.FullName), ShareModel.Companyname), 16,
                                                             false, 1,
                                                             System.Text.Encoding.UTF8);
            // Encrypt -> Decrypt
            string decresult;
            try
            {
                decresult = EnDecryptHelper.ByteAesDecToString(System.IO.File.ReadAllBytes(xmlPath), enckey, enciv);
                if (string.IsNullOrEmpty(decresult))
                {
                    decresult = System.IO.File.ReadAllText(xmlPath);
                }
            }
            catch
            {
                try
                {
                    decresult = System.IO.File.ReadAllText(xmlPath);
                }
                catch
                {
                    return null;
                }
            }
            return string.IsNullOrEmpty(decresult) ? null : decresult;

            #endregion
        }
        /// <summary>
        /// String -> Decrypt To String
        /// </summary>
        /// <param name="encryptstr"></param>
        /// <param name="key"></param>
        /// <param name="vi"></param>
        /// <returns></returns>
        public static string DecryptStringFromString(string encryptstr, string key, string vi)
        {
            #region String -> Decrypt To String
            if (encryptstr.IsNullOrEmptyOrSpace()) return null;
            if (key.IsNullOrEmptyOrSpace()) return null;
            if (vi.IsNullOrEmptyOrSpace()) return null;

            key = EnDecryptHelper.StringMd5ShaToString(false,
                                                       string.Format("<{0}/>{1}</{2}>", ShareModel.Companyname, key, ShareModel.Companyname),
                                                       16, false, 1, System.Text.Encoding.UTF8);
            vi = EnDecryptHelper.StringMd5ShaToString(false,
                                                      string.Format("[{0}/]{1}[/{2}]", ShareModel.Companyname, vi, ShareModel.Companyname), 16,
                                                      false, 1, System.Text.Encoding.UTF8);
            if (key.IsNullOrEmptyOrSpace()) return null;
            if (vi.IsNullOrEmptyOrSpace()) return null;

            string encryptedstr;
            try
            {
                encryptedstr = EnDecryptHelper.StringAesToString(true, encryptstr, key, vi);
            }
            catch
            {
                encryptedstr = encryptstr;
            }
            return encryptedstr.ToSafeValue();
            #endregion
        } 
        #endregion

        #region Encrypt

        /// <summary>
        /// ImagePath -> Encrypt To SavePath
        /// </summary>
        /// <param name="savePath"></param>
        /// <param name="originPath"></param>
        /// <returns></returns>
        public static bool EncryptImageFormiOriginPath(string savePath, string originPath)
        {
            #region ImagePath -> Encrypt To SavePath

            return EncryptFileFormPath(savePath, originPath, true, null);

            #endregion
        }

        /// <summary>
        /// XmlPath -> Encrypt To SavePath
        /// </summary>
        /// <param name="savePath"></param>
        /// <param name="originPath"></param>
        /// <returns></returns>
        public static bool EncryptXmlFormiOriginPath(string savePath, string originPath)
        {
            #region XmlPath -> Encrypt To SavePath

            return EncryptFileFormPath(savePath, originPath, false, null);

            #endregion
        }

        /// <summary>
        /// OriginPath -> SavePath With IsImage?
        /// </summary>
        /// <param name="savePath"></param>
        /// <param name="originPath"></param>
        /// <param name="isImage"></param>
        /// <param name="keyvi"></param>
        /// <returns></returns>
        private static bool EncryptFileFormPath(string savePath, string originPath, bool isImage, string keyvi)
        {
            #region OriginPath -> SavePath With IsImage?

            if (savePath.IsNullOrEmptyOrSpace() || originPath.IsNullOrEmptyOrSpace()) return false;
            // Creat -> FileInfo
            var fileInfo = new System.IO.FileInfo(originPath);
            // Creat -> Key && IV
            var enckey = EnDecryptHelper.StringMd5ShaToString(false,
                                                              string.Format("<{0}/>{1}</{2}>", ShareModel.Companyname,
                                                                            keyvi.IsNullOrEmptyOrSpace()
                                                                                ? System.IO.Path
                                                                                        .GetFileNameWithoutExtension(
                                                                                            fileInfo.FullName)
                                                                                : keyvi, ShareModel.Companyname), 16, false, 1,
                                                              System.Text.Encoding.UTF8);
            if (enckey.IsNullOrEmptyOrSpace()) return false;
            var enciv = EnDecryptHelper.StringMd5ShaToString(false,
                                                             string.Format("[{0}/]{1}[/{2}]", ShareModel.Companyname,
                                                                           keyvi.IsNullOrEmptyOrSpace()
                                                                               ? System.IO.Path
                                                                                       .GetFileNameWithoutExtension(
                                                                                           fileInfo.FullName)
                                                                               : keyvi, ShareModel.Companyname), 16, false, 1,
                                                             System.Text.Encoding.UTF8);
            if (enciv.IsNullOrEmptyOrSpace()) return false;
            // Origin -> Encrypt
            var decresult = isImage
                                ? EnDecryptHelper.ByteAesToByte(false, System.IO.File.ReadAllBytes(fileInfo.FullName),
                                                                enckey,
                                                                enciv)
                                : EnDecryptHelper.StringAesEncToByte(System.IO.File.ReadAllText(fileInfo.FullName),
                                                                     enckey,
                                                                     enciv);
            if (decresult.IsEmptyBytes()) return false;
            // Encrypt -> SaveFile
            try
            {
                System.IO.File.WriteAllBytes(savePath, decresult);
            }
            catch
            {
                return false;
            }
            return true;

            #endregion
        }

        /// <summary>
        /// String -> Encrypt To File
        /// </summary>
        /// <param name="savePath"></param>
        /// <param name="byEnString"></param>
        /// <param name="key"></param>
        /// <param name="vi"></param>
        /// <returns></returns>
        public static bool EncryptFileFromString(string savePath, string byEnString, string key, string vi)
        {
            #region String -> Encrypt To File

            if (savePath.IsNullOrEmptyOrSpace()) return false;
            if (byEnString.IsNullOrEmptyOrSpace()) return false;
            if (key.IsNullOrEmptyOrSpace()) return false;
            if (vi.IsNullOrEmptyOrSpace()) return false;

            key = EnDecryptHelper.StringMd5ShaToString(false,
                                                       string.Format("<{0}/>{1}</{2}>", ShareModel.Companyname, key, ShareModel.Companyname),
                                                       16, false, 1, System.Text.Encoding.UTF8);
            vi = EnDecryptHelper.StringMd5ShaToString(false,
                                                      string.Format("[{0}/]{1}[/{2}]", ShareModel.Companyname, vi, ShareModel.Companyname), 16,
                                                      false, 1, System.Text.Encoding.UTF8);
            if (key.IsNullOrEmptyOrSpace()) return false;
            if (vi.IsNullOrEmptyOrSpace()) return false;

            var encryptedstr = EnDecryptHelper.StringAesToString(false,byEnString, key, vi);
            if (encryptedstr.IsNullOrEmptyOrSpace()) return false;

            // Encrypt -> SaveFile
            try
            {
                System.IO.File.WriteAllText(savePath, encryptedstr,System.Text.Encoding.UTF8);
            }
            catch
            {
                return false;
            }
            return true;

            #endregion
        }

        /// <summary>
        /// UserImagePath -> Encrypt To SavePath
        /// </summary>
        /// <param name="savePath"></param>
        /// <param name="originPath"></param>
        /// <returns></returns>
        public static bool EncryptUserImageFormiOriginPath(string savePath, string originPath)
        {
            #region UserImagePath -> Encrypt To SavePath

            var savepaths = savePath.Split("\\".ToCharArray());
            return !savepaths.IsEmptyStrings() &&
                   EncryptFileFormPath(savePath, originPath, true, savepaths[savepaths.Length - 1].Split('.')[0]);

            #endregion
        }

        #endregion
    }
}