using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ServiceClient
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            ServiceLibrary.SyncServer.ContectServer("tcp://localhost:9901/SyncServer");
            ServiceLibrary.WcfServer.ContectServer();

            var time = DateTime.Now;
            ServiceLibrary.SyncServer.Server.Save("test", "remoting:" + time);


            ServiceLibrary.WcfServer.Server.Save("test2", "wcf:" + time);

            Button1.Text = time.ToString();
        }
    }
}