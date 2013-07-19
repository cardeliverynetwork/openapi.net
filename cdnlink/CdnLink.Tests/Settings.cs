using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CdnLink.Tests
{
    public static class Settings
    {
        public const string DbDataSource = "localhost";
        public const string DbInitialCatalog = "CdnLinkTest";
        public const string DbUser = "CdnLinkTestUsr";
        public const string DbPass = "password";

        public static string GetConnetionString()
        {
            return GetConnetionString(DbDataSource, DbInitialCatalog, DbUser, DbPass);
        }

        public static string GetConnetionString(string dataSource, string initialCatalog, string user, string pass)
        {
            return string.Format(
                "Data Source={0};Initial Catalog={1};uid={2};pwd={3}", 
                dataSource, 
                initialCatalog, 
                user, 
                pass);
        }
    }
}
