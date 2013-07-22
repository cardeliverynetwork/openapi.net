
namespace CdnLink.Tests
{
    public static class Settings
    {
        public const string DbDataSource = "localhost";
        public const string DbInitialCatalog = "CdnLinkTest";
        public const string DbUser = "CdnLinkTestUsr";
        public const string DbPass = "password";

        public static string GetConnectionString(
            string dataSource = DbDataSource, 
            string initialCatalog = DbInitialCatalog, 
            string user = DbUser, 
            string pass = DbPass)
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
