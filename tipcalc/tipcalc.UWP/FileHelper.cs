using System.IO;
using tipcalc.UWP;
using tipcalc_data.Interfaces;

using Windows.Storage;

using Xamarin.Forms;

[assembly: Dependency(typeof(FileHelper))]
namespace tipcalc.UWP
{
    public class FileHelper : IFileHelper
    {
        public string GetLocalFilePath(string filename)
        {
            return Path.Combine(ApplicationData.Current.LocalFolder.Path, filename);
        }
    }
}