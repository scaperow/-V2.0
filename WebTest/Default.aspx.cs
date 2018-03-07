using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace WebTest
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            MobileService.MobileService ms = new MobileService.MobileService();
            ltlMsg.Text= ms.GetBhzLineStatics("C4018EC0-8360-491A-B24F-B7E069349256", "2014-02-06", "2014-03-09", "", "AndroidUser", "zTyEXJHPzOy9iLq4eBLrcsXjBd5TX1r3tXKsRZDuRr4=");
        }
    }
}
