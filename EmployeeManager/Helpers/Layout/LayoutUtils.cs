using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;

namespace Helpers.Layout
{
    public static class LayoutUtils
    {
        public static string GetAuthBackground(string p_imageName = null)
        {
            try
            {
                string l_RelativePath = @"\images\auth_bg";
                string l_BasePath = AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\') + l_RelativePath;
                string l_background = "/images/bg.jpg";
                if (Directory.Exists(l_BasePath))
                {
                    DirectoryInfo l_DirInfo = new DirectoryInfo(l_BasePath);
                    FileInfo[] l_Files = l_DirInfo.GetFiles();

                    if (string.IsNullOrEmpty(p_imageName))
                        l_background = l_RelativePath.Replace("\\", "/") + "/" + l_Files[((new Random()).Next(0, l_Files.Count()))].Name;
                    else
                    {
                        if (l_Files.Where(p => p.Name == p_imageName).Any())
                            l_background = l_RelativePath.Replace("\\", "/") + "/" + l_Files.Where(p => p.Name == p_imageName).Select(p => p.Name).FirstOrDefault();
                    }
                }

                return l_background;
            }
            catch (Exception Error)
            {
                throw;
            }
        }
    }

}