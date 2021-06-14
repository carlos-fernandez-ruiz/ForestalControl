using System;
using System.Configuration;
using System.IO;

namespace ControlForestal
{
    class Program
    {
        static void Main(string[] args)
        {
            string importDirectory = ConfigurationManager.AppSettings["ImportRoutesDirectory"];
            string exportDirectory = ConfigurationManager.AppSettings["ExportRoutesResultDirectory"];
            System.IO.Directory.CreateDirectory(exportDirectory);
            foreach (string file in Directory.EnumerateFiles(importDirectory))
            {
                string result = new DroneController().processFile(file);
                string resultFile = Path.GetFileNameWithoutExtension(file) + "_result.txt";
                File.WriteAllText(Path.Combine(exportDirectory, resultFile), result);
            }            
        }
    }
}
