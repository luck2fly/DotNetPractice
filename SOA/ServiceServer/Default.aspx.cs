using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ServiceServer
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Write(System.Runtime.Caching.MemoryCache.Default.Get("test"));
            Response.Write("<br>\r\n");
            Response.Write(System.Runtime.Caching.MemoryCache.Default.Get("test2"));
        }
    }
}