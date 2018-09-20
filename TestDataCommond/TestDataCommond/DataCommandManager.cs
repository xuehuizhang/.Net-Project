using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDataCommond
{
    public class DataCommandManager
    {
        private static string path=Path.GetFullPath("../../Config");
        public static DataCommand GetDataCommand(string fileName)
        {
            try
            {
                DataCommand data = null;
                var arrs = fileName.Split('.');
                string fullPath = GetFiles(path, arrs[0]);
                if (fullPath != "")
                {
                    List<DataCommand> list = XmlHelper.XmlDeserializeFromFile<List<DataCommand>>(fullPath, Encoding.UTF8);

                    if (list != null && list.Any())
                    {
                        foreach (var item in list)
                        {
                            if (item.CommandName == arrs[1])
                            {
                                return item;
                            }
                        }
                    }
                    return data;
                }
                return null;
            }catch(Exception ex)
            {
                throw ex;
            }
           
        }

        /// <summary>  
        /// 列出指定目录下及所其有子目录及子目录里更深层目录里的文件（需要递归）  
        /// </summary>  
        /// <param name="path"></param>  
        public static string GetFiles(string path, string fileName)
        {
            DirectoryInfo dir = new DirectoryInfo(path);

            //找到该目录下的文件  
            FileInfo[] fi = dir.GetFiles();
            foreach (FileInfo f in fi)
            {
                if ((fileName + ".xml").Equals(f.Name))
                {
                    return f.FullName.ToString();
                }
            }

            //找到该目录下的所有目录再递归  
            DirectoryInfo[] subDir = dir.GetDirectories();
            foreach (DirectoryInfo d in subDir)
            {
                string filePath = GetFiles(d.FullName, fileName);
                string name = Path.GetFileName(filePath);
                if ((fileName + ".xml").Equals(name))
                {
                    return filePath;
                }
            }
            return "";
        }
    }
}
