using System;
using System.IO;
using tipcalc.Droid;
using tipcalc_data.Interfaces;

using Xamarin.Forms;

[assembly: Dependency(typeof(FileHelper))]
namespace tipcalc.Droid
{
    public class FileHelper : IFileHelper
    {
        public string GetLocalFilePath(string filename)
        {
            string path = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            return Path.Combine(path, filename);
        }
    }
}