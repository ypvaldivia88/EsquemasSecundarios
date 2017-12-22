using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Web.Configuration;
namespace ConnectionString
{
    public class Connection
    {
        public string ConfigurnewConnectionString(string server, string database, string userid, string password, string ConnectionString)
        {
            System.Configuration.Configuration Config1 = WebConfigurationManager.OpenWebConfiguration("~");
            ConnectionStringsSection conSetting = (ConnectionStringsSection)Config1.GetSection("ESConnection");
            conSetting.ConnectionStrings[ConnectionString].ConnectionString = conSetting.ConnectionStrings[ConnectionString].ConnectionString.ToString().Replace("{0}", server);
            conSetting.ConnectionStrings[ConnectionString].ConnectionString = conSetting.ConnectionStrings[ConnectionString].ConnectionString.ToString().Replace("{1}", database);
            conSetting.ConnectionStrings[ConnectionString].ConnectionString = conSetting.ConnectionStrings[ConnectionString].ConnectionString.ToString().Replace("{2}", userid);
            conSetting.ConnectionStrings[ConnectionString].ConnectionString = conSetting.ConnectionStrings[ConnectionString].ConnectionString.ToString().Replace("{3}", password);
            return conSetting.ConnectionStrings[ConnectionString].ConnectionString;
        }
    }
}