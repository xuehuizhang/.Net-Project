using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Configuration;
using Dapper;
using System.Data;

namespace TestDataCommond
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine(Path.GetFullPath("../../Config"));
            //List<DataCommand> list = XmlHelper.XmlDeserializeFromFile<List<DataCommand>>(Path.GetFullPath("../../Config") + "/MyData.xml", Encoding.UTF8);
            //if (list.Count > 0)
            //    Console.WriteLine(list[0].CommandName + ": " + list[0].CommandText);
            var connStr = ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;
            using (var conn = new SqlConnection(connStr))
            {
                DataCommand dc = DataCommandManager.GetDataCommand("MyData.UpdateCustomer");
                var param = new DynamicParameters();
                param.Add("@Name", value: "张三", dbType: DbType.String, direction: ParameterDirection.Input);
                param.Add("@Age", value: "10", dbType: DbType.Int32, direction: ParameterDirection.Input);
                var u= conn.Query<Users>(dc.CommandText.ToString(), param);
                Console.ReadKey();
            }
        }

        /// <summary>  
        /// 列出指定目录下及所其有子目录及子目录里更深层目录里的文件（需要递归）  
        /// </summary>  
        /// <param name="path"></param>  
        public static string GetAllFiles(string path, string fileName)
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
                string filePath = GetAllFiles(d.FullName, fileName);
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
