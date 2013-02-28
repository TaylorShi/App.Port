namespace App.FileIO
{
    public class EnDecryptHelper
    {
        private const string EnDecryptConst = @"CraigTaylor";
        ///////////////////////////////////////////////////////////////////////////////////////////////////
        //
        //  EnDecryptHelper .Net加密解密算法辅助类
        //  By CraigTaylor  LastModify At 2013.1.14
        //  http://msdn.microsoft.com/zh-cn/library/9eat8fht.aspx
        //
        ///////////////////////////////////////////////////////////////////////////////////////////////////
        //
        //  在.NET Framework中，可以使用System.Security.Cryptography命名空间中的类来加密。
        //  它实现了几个对称和非对称算法。从.NET 3.5开始，
        //  一些新类以Cng作为前缀或后缀，表示Cryptography Next Generation，
        //  用于采用Windows NT 6.0或更高内核版本的操作系统（Vista、Win7、Win2008、Win8）。
        //  这个API可以使用基于提供程序的模型，编写独立于算法的程序
        //  没有Cng、Managed或CryptoServiceProvider后缀的类是抽象基类，
        //  例如MD5。Managed后缀表示这个算法用托管代码实现，
        //  其他类可能封装了内部的Windows API调用。
        //  CryptoServiceProvider后缀用于实现了抽象基类的类，
        //  Cng后缀用于利用新Cryptography CNG API的类，
        //  它只能用于指定版本的操作系统。
        //  关于性能方面，就同一种算法，
        //  有CryptoServiceProvider、Managed、Cng三种实现方式，
        //  我现在测试了散列中的方法，其中MD5是没有Managed实现的
        //  Cng算法的速度是最差的，而Csp居中水平，
        //  Managed实现则速度非常快，另外，
        //  如果加大SHA算法的位数的话，当到384位时差别就不再明显，
        //  而且Csp算法所需时间成为了最少的方式，
        //  这个我认为是操作系统API调用所形成的优势
        //
        ///////////////////////////////////////////////////////////////////////////////////////////////////
        /// 

        #region 以字节形式读取并还原实例

        ////-----------------------以字节形式读取并还原实例-------------------------//
        //
        //  var fileInfo = new FileInfo(fileName);
        //  // Creat Key && IV
        //  var enckey = EnDecryptHelper.StringMd5ShaToString(false, string.Format("<{0}/>{1}</{2}>", _companyname, fileInfo.Name, _companyname), 16, false, 1, Encoding.UTF8);
        //  var enciv = EnDecryptHelper.StringMd5ShaToString(false, string.Format("[{0}/]{1}[/{2}]", _companyname, fileInfo.Name, _companyname), 16, false, 1, Encoding.UTF8);
        //  // File -> Origin 
        //  var filestring = File.ReadAllBytes(fileInfo.FullName);
        //  if (!Directory.Exists(_lastDirPath + @"\SafeDir\"))
        //  {
        //      Directory.CreateDirectory(_lastDirPath + @"\SafeDir\");
        //  }
        //  var orginsavepath = _lastDirPath + @"\SafeDir\" + fileInfo.Name + ".Ori";
        //  File.WriteAllBytes(orginsavepath, filestring);
        //  // Origin -> Encrypt 
        //  var encstring = EnDecryptHelper.ByteAesToByte(false, filestring, enckey, enciv);
        //  var encsavepath = _lastDirPath + @"\SafeDir\" + fileInfo.Name + ".Enc";
        //  File.WriteAllBytes(encsavepath, encstring);
        //  // Encrypt -> Decrypt
        //  var decresult = EnDecryptHelper.ByteAesToByte(true, File.ReadAllBytes(encsavepath), enckey, enciv);
        //  var decsavepath = _lastDirPath + @"\SafeDir\" + fileInfo.Name + ".Dec";
        //  File.WriteAllBytes(decsavepath, decresult);
        //  // SHA1 : Origin ==  Decrypt
        //  var orginsha = EnDecryptHelper.FileMd5ShaToString(true, orginsavepath, 1);
        //  var decsha = EnDecryptHelper.FileMd5ShaToString(true, decsavepath, 1);
        //  var shasavepath = _lastDirPath + @"\SafeDir\" + fileInfo.Name + ".Sha";
        //  if (orginsha == decsha)
        //  {
        //      File.WriteAllText(shasavepath, decsha);
        //  }
        //
        ////-----------------------以字节形式读取并还原实例-------------------------// 

        #endregion

        ///

        #region 以文本形式读取并还原实例

        ////-------------------以文本形式读取并还原实例------------------------//
        //
        //  // File -> Origin 
        //  var filestring = File.ReadAllText(fileInfo.FullName,Encoding.Default);
        //  if (!Directory.Exists(_lastDirPath + @"\SafeDir\"))
        //  {
        //      Directory.CreateDirectory(_lastDirPath + @"\SafeDir\");
        //  }
        //  var orginsavepath = _lastDirPath + @"\SafeDir\" + fileInfo.Name + ".Ori";
        //  File.WriteAllText(orginsavepath, filestring);
        //  // Origin -> Encrypt 
        //  var encstring = EnDecryptHelper.StringAesEncToByte(filestring, enckey, enciv);
        //  var encsavepath = _lastDirPath + @"\SafeDir\" + fileInfo.Name+ ".Enc";
        //  File.WriteAllBytes(encsavepath, encstring);
        //  // Encrypt -> Decrypt
        //  var decresult = EnDecryptHelper.ByteAesDecToString(File.ReadAllBytes(encsavepath), enckey, enciv);
        //  var decsavepath = _lastDirPath + @"\SafeDir\" + fileInfo.Name + ".Dec";
        //  File.WriteAllText(decsavepath, decresult);
        //  // SHA1 : Origin ==  Decrypt
        //  var orginsha = EnDecryptHelper.FileMd5ShaToString(true, orginsavepath, 1);
        //  var decsha = EnDecryptHelper.FileMd5ShaToString(true, decsavepath, 1);
        //  var shasavepath = _lastDirPath + @"\SafeDir\" + fileInfo.Name + ".Sha";
        //  if (orginsha == decsha )
        //  {
        //      File.WriteAllText(shasavepath, decsha);
        //  }
        //
        ////-------------------以文本形式读取并还原实例------------------------// 

        #endregion

        ///
        ///////////////////////////////////////////////////////////////////////////////////////////////////
        //
        //  公共调用代码
        //
        ///////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////
        //
        //  MD5、SHA 系列算法调用
        //
        ///////////////////////////////////////////////////////////////////////////////////////////////////
        /// 
        ///  <summary>
        ///  MD5 (16-32)位 / SHA (1-256-384-512)位 String -> String
        ///  </summary>
        /// <param name="isSha"></param>
        /// <param name="toEn"></param>
        ///  <param name="size"></param>
        ///  <param name="toLower"></param>
        ///  <param name="time"></param>
        ///  <param name="encoding"></param>
        ///  <returns>string / null</returns>
        public static string StringMd5ShaToString(bool isSha, string toEn, int size, bool toLower, int time,
                                                  System.Text.Encoding encoding)
        {
            #region MD5 (16-32)位 / SHA (1-256-384-512)位 String -> String

            // 判断输入正确
            if (toEn.IsNullOrEmptyOrSpace()) return null;
            // 得到Byte[]
            var stringbyte = encoding.GetBytes(toEn);
            if (stringbyte.IsEmptyBytes()) return null;
            // 得到Sting
            var stringstring = isSha == false
                                   ? ByteMd5ToString(stringbyte, size, toLower, time, encoding)
                                   : ByteShaToString(stringbyte, size, toLower, time, encoding);
            // 返回值
            return stringstring.ToSafeValue();
            #endregion
        }

        /// <summary>
        /// MD5 (16-32)位 / SHA (1-256-384-512)位 FilePath -> String
        /// </summary>
        /// <param name="isSha"></param>
        /// <param name="fileReadPath"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static string FileMd5ShaToString(bool isSha, string fileReadPath, int size)
        {
            #region MD5 (16-32)位 / SHA (1-256-384-512)位 FilePath -> String

            // 校验输入
            if (!fileReadPath.IsExistFile()) return null;
            // 读取输入
            var filebyte = System.IO.File.ReadAllBytes(fileReadPath);
            if (filebyte.IsEmptyBytes()) return null;
            // 得到摘要
            var fileString = isSha == false
                                 ? ByteMd5ToString(filebyte, size, false, 1, System.Text.Encoding.Default)
                                 : ByteShaToString(filebyte, size, false, 1, System.Text.Encoding.Default);
            // 返回摘要
            return fileString.ToSafeValue();
            #endregion
        }

        ///
        /// <summary>
        /// MD5 (16-32)位 Byte -> String
        /// </summary>
        /// <param name="toEn"></param>
        /// <param name="size"></param>
        /// <param name="toLower"></param>
        /// <param name="time"></param>
        /// <param name="encoding"></param>
        /// <returns>string / null </returns>
        public static string ByteMd5ToString(byte[] toEn, int size, bool toLower, int time,
                                             System.Text.Encoding encoding)
        {
            #region MD5 (16-32)位 Byte -> String

            // 执行多次
            for (var i = 1; i < time; i++)
            {
                toEn = encoding.GetBytes(ByteMd5ToString(toEn, size, false, i, encoding));
            }
            // 判断输入正确
            if (toEn.IsEmptyBytes()) return null;
            // 进行MD5计算
            var bytebyte = ByteMd5ToByte(toEn);
            if (bytebyte.IsEmptyBytes()) return null;
            // 核对MD5结果
            var result = string.Empty;
            // 16 位 / 32 位
            switch (size)
            {
                case 16:
                    {
                        // 转换大小写，默认值大写
                        result = toLower
                                   ? System.BitConverter.ToString(bytebyte, 4, 8).Replace("-", "").ToLower()
                                   : System.BitConverter.ToString(bytebyte, 4, 8).Replace("-", "");
                    }
                    break;
                case 32:
                    {
                        // 转换大小写，默认值大写
                        result = toLower
                                   ? System.BitConverter.ToString(bytebyte).Replace("-", "").ToLower()
                                   : System.BitConverter.ToString(bytebyte).Replace("-", "");
                    }
                    break;
            }
            return result.ToSafeValue();
            #endregion
        }

        ///
        /// <summary>
        /// SHA (1-256-384-512)位 Byte -> String
        /// </summary>
        /// <param name="toEn"></param>
        /// <param name="size"></param>
        /// <param name="toLower"></param>
        /// <param name="time"></param>
        /// <param name="encoding"></param>
        /// <returns>string / null </returns>
        public static string ByteShaToString(byte[] toEn, int size, bool toLower, int time,
                                             System.Text.Encoding encoding)
        {
            #region SHA (1-256-384-512)位 Byte -> String

            // 执行多次
            for (var i = 1; i < time; i++)
            {
                toEn = encoding.GetBytes(ByteShaToString(toEn, size, false, i, encoding));
            }
            // 判断输入正确
            if (toEn.IsEmptyBytes()) return null;
            // 进行MD5计算
            var bytebyte = ByteShaToByte(toEn, size);
            if (bytebyte.IsEmptyBytes()) return null;

            var bitstring = toLower
                                    ? System.BitConverter.ToString(bytebyte).Replace("-", "").ToLower()
                                    : System.BitConverter.ToString(bytebyte).Replace("-", "");
            return bitstring.ToSafeValue();

            #endregion
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////
        //
        //  散列算法具体执行
        //
        ///////////////////////////////////////////////////////////////////////////////////////////////////
        //
        //  MD5, MD5Cng
        //  SHA1, SHA1Managed, SHA1Cng
        //  SHA256, SHA256Managed, SHA256Cng
        //  SHA384, SHA384Managed, SHA384Cng
        //  SHA512, SHA512Managed, SHA512Cng
        //  散列算法的目标是从任意长度的二进制字符串中创建一个长度固定的散列值。
        //  这些算法和数字签名一起用于保证数据的完整性。
        //  如果再次散列相同的二进制字符串，会返回相同的散列结果。
        //  MD5(Message Digest Algorithm 5)是由RSA实验室开发的，比SHA1快。
        //  SHA1在抵御暴力攻击方面比较强大。SHA算法是由美国国家安全局(NSA)设计的。
        //  MD5使用128位的散列值，SHA1使用160位。其它SHA算法在其名称中包含了散列长度。
        //  SHA512是这些算法中最强大的，其散列长度为512位，也是最慢的
        //
        ///////////////////////////////////////////////////////////////////////////////////////////////////
        ///
        /// <summary>
        /// MD5 (16-32)位 Byte -> Byte
        /// </summary>
        /// <param name="toEn"></param>
        /// <returns>byte[] / null</returns>
        private static byte[] ByteMd5ToByte(byte[] toEn)
        {
            #region MD5 (16-32)位 Byte -> Byte

            try
            {
                // MD5没有Managed方法
                return new System.Security.Cryptography.MD5CryptoServiceProvider().ComputeHash(toEn);
            }
            catch
            {
                return null;
            }

            #endregion
        }

        ///
        /// <summary>
        /// SHA (1-256-384-512)位  Byte -> Byte
        /// </summary>
        /// <param name="toEn"></param>
        /// <param name="size"></param>
        /// <returns>byte[] / null </returns>
        private static byte[] ByteShaToByte(byte[] toEn, int size)
        {
            #region SHA (1-256-384-512)位  Byte -> Byte

            try
            {
                // 分算法型号处理
                switch (size)
                {
                    case 1:
                        {
                            // SHA1Managed 算法的哈希值大小为 160 位
                            return new System.Security.Cryptography.SHA1Managed().ComputeHash(toEn);
                        }
                    case 256:
                        {
                            // SHA256Managed 算法的哈希值大小为 256 位。
                            return new System.Security.Cryptography.SHA256Managed().ComputeHash(toEn);
                        }
                    case 384:
                        {
                            // SHA384Managed 算法的哈希值大小为 384 位。
                            return new System.Security.Cryptography.SHA384Managed().ComputeHash(toEn);
                        }
                    case 512:
                        {
                            // SHA512Managed 算法的哈希值大小为 512 位。
                            return new System.Security.Cryptography.SHA512Managed().ComputeHash(toEn);
                        }
                }
            }
            catch
            {
                return null;
            }
            return null;

            #endregion
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////
        //
        //  对称算法具体执行
        //
        ///////////////////////////////////////////////////////////////////////////////////////////////////
        //
        //  高级加密标准AES(Advanced Encryption Standard)
        //
        ///////////////////////////////////////////////////////////////////////////////////////////////////
        //
        //  DES, DESCryptoServiceProvider
        //  TripleDES, TripleDESCryptoServiceProvider
        //  AES, AESCryptoServiceProvider, AESManaged
        //  RC2, RC2CryptoServiceProvider
        //  Rijandel, RijandelManaged
        //  对称密钥算法使用相同的密钥进行数据的加密和解密。
        //  现在认为DES(Data Encryption Standard)是不安全的，
        //  因为它只使用56位的密钥，可以在不超过24小时的时间内破解。
        //  Triple DES是DES的继任者，其密钥长度是168位，
        //  但它提供的有效安全性只有112位。
        //  AES(Advanced Encrytion Standard)是美国政府采用的加密标准，
        //  其密钥长度是128、192或256位。
        //  Rijandel非常类似于AES，只是在密钥长度方面的选项较多。
        //  高级加密标准（Advanced Encryption Standard，AES），
        //  在密码学中又称Rijndael加密法，是美国联邦政府采用的一种区块加密标准
        //  该算法为比利时密码学家Joan Daemen和Vincent Rijmen所设计，结合两位作者的名字，
        //  以Rijndael之命名之，投稿高级加密标准的甄选流程
        //  AES和Rijndael加密法并不完全一样（虽然在实际应用中二者可以互换）
        //  Rijndael加密法可以支持更大范围的区块和密钥长度：
        //  AES的区块长度固定为128 比特，密钥长度则可以是128，192或256比特；
        //  而Rijndael使用的密钥和区块长度可以是32位的整数倍，以128位为下限，
        //  256比特为上限。加密过程中使用的密钥是由Rijndael密钥生成方案产生。
        //  截至2006年，针对AES唯一的成功攻击是旁道攻击
        //  旁道攻击(side-channelattacks,简称EC2)是一种针对密码设备的新型攻击技术，
        //  它并不是直接获取计算机里存储的信息，
        //  而是通过一种间接的方式，比如，
        //  通过对电脑屏幕和键盘所产生的电磁辐射信息进行分析，
        //  就可以知道这台计算机正在做什么。
        //
        ///////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// AES 加解密 Byte -> Byte
        /// </summary>
        /// <param name="isDec"></param>
        /// <param name="plainbyte"></param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <returns></returns>
        public static byte[] ByteAesToByte(bool isDec, byte[] plainbyte, string key, string iv)
        {
            #region AES 加解密 Byte -> Byte

            return isDec
                       ? System.Convert.FromBase64String(ByteAesDecToString(plainbyte, key, iv))
                       : StringAesEncToByte(System.Convert.ToBase64String(plainbyte), key, iv);

            #endregion
        }

        /// 
        ///  <summary>
        ///  AES 加解密 String -> String
        ///  </summary>
        /// <param name="isDec"></param>
        /// <param name="plainText"></param>
        ///  <param name="key"></param>
        ///  <param name="iv"></param>
        /// <returns></returns>
        public static string StringAesToString(bool isDec, string plainText, string key, string iv)
        {
            #region AES 加解密 String -> String

            return isDec
                       ? ByteAesDecToString(System.Convert.FromBase64String(plainText), key, iv)
                       : System.Convert.ToBase64String(StringAesEncToByte(plainText, key, iv));

            #endregion
        }

        /// 
        ///  <summary>
        ///  AES 加密 String -> Byte
        ///  </summary>
        ///  <param name="plainText"></param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <returns></returns>
        public static byte[] StringAesEncToByte(string plainText, string key, string iv)
        {
            #region AES 加密 String -> Byte

            // 检查参数
            if (string.IsNullOrEmpty(plainText))
                return null;
            if (string.IsNullOrEmpty(key))
                return null;
            if (string.IsNullOrEmpty(iv))
                return null;
            // 合成密钥
            var enckey = StringMd5ShaToString(false, string.Format("<{0}/>{1}</{2}>[{3}]", iv, key, iv, EnDecryptConst),
                                              16, false, 1, System.Text.Encoding.UTF8);
            var enciv = StringMd5ShaToString(false, string.Format("[{0}/]{1}[/{2}]<{3}>", iv, key, iv, EnDecryptConst),
                                             16, false, 1, System.Text.Encoding.UTF8);
            // 转换密钥
            var keybyte = System.Text.Encoding.UTF8.GetBytes(enckey);
            if (keybyte.Length <= 0)
                return null;
            var ivbyte = System.Text.Encoding.UTF8.GetBytes(enciv);
            if (ivbyte.Length <= 0)
                return null;
            byte[] encrypted;
            // 创建一个加密对象
            using (var aesAlg = new System.Security.Cryptography.AesManaged())
            {
                aesAlg.Key = keybyte;
                aesAlg.IV = ivbyte;
                // 创建一个加密来执行流的转换
                var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
                // 为加密创建一个内存流
                try
                {
                    using (var msEncrypt = new System.IO.MemoryStream())
                    {
                        using (
                            var csEncrypt = new System.Security.Cryptography.CryptoStream(msEncrypt, encryptor,
                                                                                          System.Security.Cryptography
                                                                                                .CryptoStreamMode.Write)
                            )
                        {
                            using (var swEncrypt = new System.IO.StreamWriter(csEncrypt))
                            {
                                // 把所有数据写进流
                                swEncrypt.Write(plainText);
                            }
                            // 把内存流转换为字节数组
                            encrypted = msEncrypt.ToArray();
                        }
                    }
                }
                catch
                {
                    return null;
                }
            }
            // 返回此加密字节
            return encrypted;

            #endregion
        }

        /// <summary>
        /// AES 解密 Byte -> String
        /// </summary>
        /// <param name="cipherText"></param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <returns></returns>
        public static string ByteAesDecToString(byte[] cipherText, string key, string iv)
        {
            #region AES 解密 Byte -> String

            // 检查参数
            if (cipherText.IsEmptyBytes()) return null;
            if (key.IsNullOrEmptyOrSpace()) return null;
            if (iv.IsNullOrEmptyOrSpace()) return null;

            // 合成密钥
            var deckey = StringMd5ShaToString(false, string.Format("<{0}/>{1}</{2}>[{3}]", iv, key, iv, EnDecryptConst),
                                              16, false, 1, System.Text.Encoding.UTF8);
            var deciv = StringMd5ShaToString(false, string.Format("[{0}/]{1}[/{2}]<{3}>", iv, key, iv, EnDecryptConst),
                                             16, false, 1, System.Text.Encoding.UTF8);
            // 转换参数
            var keybyte = System.Text.Encoding.UTF8.GetBytes(deckey);
            if (keybyte.Length <= 0)
                return null;
            var ivbyte = System.Text.Encoding.UTF8.GetBytes(deciv);
            if (ivbyte.Length <= 0)
                return null;
            // 存储解密结果
            string plaintext;
            // 创建一个解密对象
            using (var aesAlg = new System.Security.Cryptography.AesManaged())
            {
                aesAlg.Key = keybyte;
                aesAlg.IV = ivbyte;
                // 创建一个解密对象
                var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                // 创建一个解密内存流
                try
                {
                    using (var msDecrypt = new System.IO.MemoryStream(cipherText))
                    {
                        using (
                            var csDecrypt = new System.Security.Cryptography.CryptoStream(msDecrypt, decryptor,
                                                                                          System.Security.Cryptography
                                                                                                .CryptoStreamMode.Read))
                        {
                            using (var srDecrypt = new System.IO.StreamReader(csDecrypt))
                            {
                                // 得到String
                                plaintext = srDecrypt.ReadToEnd();
                            }
                        }
                    }
                }
                catch
                {
                    return null;
                }
            }
            // 返回String
            return plaintext;

            #endregion
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////
        //
        //  非对称算法具体执行
        //
        ///////////////////////////////////////////////////////////////////////////////////////////////////
        //
        //  DSA, DSACryptoServiceProvider
        //  ECDsa, ECDsaCng
        //  ECDiffieHellman, ECDiffieHellmanCng
        //  RSA, RSACryptoServiceProvider
        //  非对称算法使用不同的密钥进行加密和解密。
        //  RSA(Rivest, Shamir, Adleman)是第一个用于签名和加密的算法。
        //  这个算法广泛用于电子商务协议。
        //  DSA(Digital Signature Algorithm)是美国联邦数字签名的政府标准。
        //  ECDsa(Elliptic Curve DSA)和ECDiffieHellman使用基于椭圆曲线组的算法。
        //  这些算法比较安全，且使用较短的密钥长度。
        //  例如，DSA的密钥长度为1024位，其安全性类似于160位的ECDsa。
        //  因此，ECDsa比较快。ECDiffieHellman算法用于以安全的方式在公共信道中互换私钥。
        //
        ///////////////////////////////////////////////////////////////////////////////////////////////////

        ///////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////
    }
}