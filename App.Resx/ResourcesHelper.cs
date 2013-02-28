namespace App.Resx
{
    public class ResourcesHelper
    {
        /// <summary>
        /// 返回Image
        /// </summary>
        /// <param name="resxname"></param>
        /// <returns></returns>
        public System.Drawing.Image GetImage(string resxname)
        {
            if (resxname.IsNullOrEmptyOrSpace()) return null;
            System.Drawing.Image image;
            var resxnames = resxname.Split('.');
            var localPath = "";
            switch (resxnames.Length)
            {
                case 3:
                    localPath = Base.Global.DataFolderDirectory + "\\Image\\" + resxnames[0] + "\\" + resxnames[1] + "." + resxnames[2];
                    break;
                case 2:
                    localPath = Base.Global.DataFolderDirectory + "\\Image\\" + resxname;
                    break;
            }
            if (localPath.IsExistFile())
            {
                image = System.Drawing.Image.FromFile(localPath);
            }
            else
            {
                var stream = GetType().Assembly.GetManifestResourceStream("App.Resx.Image." + resxname);
                if (stream == null)
                {
                    return null;
                }
                image = System.Drawing.Image.FromStream(stream);
            }
            return image.IsEmptyImage() ? null : image;
        }

        /// <summary>
        /// 返回ICO
        /// </summary>
        /// <param name="resxname"></param>
        /// <returns></returns>
        public System.Drawing.Icon GetIco(string resxname)
        {
            if (resxname.IsNullOrEmptyOrSpace()) return null;
            var stream = GetType().Assembly.GetManifestResourceStream("App.Resx.ICO." + resxname);
            if (stream == null)
            {
                return null;
            }
            var icon = new System.Drawing.Icon(stream);
            return icon.IsEmptyIcon() ? null : icon;
        }
    }
}
