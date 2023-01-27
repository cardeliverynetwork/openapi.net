using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CarDeliveryNetwork.Api.ClientProxy;
using CarDeliveryNetwork.Api.Data;
using Moq;

namespace CdnLink.Tests
{
    public class TestBase
    {
        protected ICdnApi GetMockCdnApi(bool populateApiKey = true)
        {
            var mock = new Mock<ICdnApi>();

            if (populateApiKey)
                mock.Setup(s => s.ApiKey)
                    .Returns("123");

            mock.Setup(s => s.GetHomeFleet())
                .Returns(new Fleet { Scac = "THESCAC" });

            mock.Setup(s => s.Uri)
                .Returns("http://example.com/openapi");

            mock.Setup(s => s.CreateJob(It.IsAny<Job>(), It.IsAny<string>()))
                 .Returns((Job j) => j);

            return mock.Object;
        }

        protected ICdnFtpBox GetMockFtpBox(bool hasFiles)
        {
            var testFiles = hasFiles
                ? Directory.GetFiles("FtpFiles").ToList()
                : new List<string>();

            var files = testFiles.ToDictionary(f => Guid.NewGuid().ToString(), File.ReadAllText);
            var fileNames = files.Select(f => f.Key).ToList();

            var mock = new Mock<ICdnFtpBox>();

            mock.Setup(s => s.Host)
                .Returns("http://ftp.example.com");

            mock.Setup(s => s.User)
                .Returns("testusr");

            mock.Setup(s => s.Pass)
                .Returns("testPass");

            mock.Setup(s => s.GetFileList())
                .Returns(fileNames);

            mock.Setup(s => s.GetFileContents(It.IsAny<string>()))
                .Returns((string s) => files.ContainsKey(s) ? files[s] : null);

            mock.Setup(s => s.DeleteFile(It.IsAny<string>()))
                .Callback((string s) => fileNames.Remove(s));

            return mock.Object;
        }
    }
}
