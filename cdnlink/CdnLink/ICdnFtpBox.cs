using System.Collections.Generic;

namespace CdnLink
{
    public interface ICdnFtpBox
    {
        string Host { get; }
        string Root { get; }
        string User { get; }
        string Pass { get; }
        List<string> GetFileList();
        string GetFileContents(string filename);
        void DeleteFile(string filename);
        void MarkFileAsBad(string filename);
    }
}
