using tipcalc_data.Interfaces;

namespace tipcalc_standard.tests.Models
{
    public class FileHelperMock : IFileHelper
    {
        public string GetLocalFilePath(string filename)
        {
            throw new System.NotImplementedException();
        }
    }
}
