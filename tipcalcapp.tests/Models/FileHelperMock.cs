using tipcalc_data.Interfaces;

namespace tipcalcapp.tests.Models
{
    public class FileHelperMock : IFileHelper
    {
        public string GetLocalFilePath(string filename)
        {
            throw new System.NotImplementedException();
        }
    }
}
