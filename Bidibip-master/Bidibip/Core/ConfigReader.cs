using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Bidibip.Core
{
    /// <summary>
    /// Config reader for read INI files using Win32 API
    /// Created by Zino2201
    /// </summary>
    public static class ConfigReader
    {
        [DllImport("kernel32.dll", EntryPoint = "GetPrivateProfileString")]
        public static extern int GetPrivateProfileString(string SectionName, string KeyName, 
            string Default, StringBuilder Return_StringBuilder_Name, int Size, string FileName);

        public static string File;

        public static void Initialize(string fileName)
        {
            File = fileName;
        }

        public static string Read(string Key, string Section = null)
        {
            StringBuilder builder = new StringBuilder(255);
            GetPrivateProfileString(Section, Key, "null", builder, 255,
                AppDomain.CurrentDomain.BaseDirectory + File);
            return builder.ToString();
        }
    }
}
