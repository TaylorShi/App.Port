using System.Linq;
namespace App.FileIO
{
    public class ComPressHelper
    {

        #region 定义
        /// <summary>
        /// 压缩文件体积最小值(单位/K)
        /// </summary>
        private const int ComPressMin = 200; 
        #endregion

        #region 公开

        /// <summary>
        /// FileComPressToFile
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="savePath"></param>
        /// <returns>bool</returns>
        public bool FileComPressToFile(string filePath, string savePath)
        {
            var filebyte = FilePathComPressToByte(filePath);
            if (filebyte != null && filebyte.Count() > 0)
            {
                System.IO.File.WriteAllBytes(savePath, filebyte);
                return true;
            }
            return false;
        }

        /// <summary>
        /// FileComPressToString
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="encoding"></param>
        /// <returns>string / null</returns>
        public string FileComPressToString(string filePath, System.Text.Encoding encoding)
        {
            var filebyte = FilePathComPressToByte(filePath);
            if (filebyte != null && filebyte.Count() > 0)
            {
                var fileStr = encoding.GetString(filebyte);
                if (!string.IsNullOrEmpty(fileStr))
                {
                    return fileStr;
                }
            }
            return null;
        }

        /// <summary>
        /// StingComPressToString
        /// </summary>
        /// <param name="str"></param>
        /// <param name="encoding"></param>
        /// <returns>string / null</returns>
        public string StingComPressToString(string str, System.Text.Encoding encoding)
        {
            var stringbyte = encoding.GetBytes(str);
            if (stringbyte.Count() > 0)
            {
                var ms = new System.IO.MemoryStream(stringbyte);
                if (ms.Length > 0)
                {
                    var bytetobyte = FileStreamComPressToByte(ms);
                    if (bytetobyte != null && bytetobyte.Count() > 0)
                    {
                        var bytestr = encoding.GetString(bytetobyte);
                        if (!string.IsNullOrEmpty(bytestr))
                        {
                            return bytestr;
                        }
                    }
                }

            }
            return null;
        } 

        #endregion

        #region 私有
        /// <summary>
        /// FilePathComPressToByte
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns>byte[] / null</returns>
        private static byte[] FilePathComPressToByte(string filePath)
        {
            if (string.IsNullOrEmpty(filePath)) return null;
            var fileInfo = new System.IO.FileInfo(filePath);
            using (var originalFileStream = fileInfo.OpenRead())
            {
                if (originalFileStream.Length >= 1024 * ComPressMin)
                {
                    var fileStreamByte = FileStreamComPressToByte(originalFileStream);
                    if (fileStreamByte != null && fileStreamByte.Count() > 0)
                    {
                        return fileStreamByte;
                    }
                }
                else
                {
                    return System.IO.File.ReadAllBytes(filePath);
                }
            }
            return null;
        }
        /// <summary>
        /// FileStreamComPressToByte
        /// </summary>
        /// <param name="fileStream"></param>
        /// <returns>byte[] / null</returns>
        private static byte[] FileStreamComPressToByte(System.IO.Stream fileStream)
        {
            if (fileStream == null || fileStream.Length <= 0) return null;
            using (var ms = new System.IO.MemoryStream())
            {
                using (var compressionStream = new System.IO.Compression.GZipStream(ms, System.IO.Compression.CompressionMode.Compress))
                {
                    fileStream.CopyTo(compressionStream);
                    ms.Position = 0;
                    return ms.ToArray();
                }
            }
        } 
        #endregion
    }
}
