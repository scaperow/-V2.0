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
    public partial class StringCompress : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnCompressed_Click(object sender, EventArgs e)
        {
            string strOriginal = txtOriginal.Text.Trim();
            if (!string.IsNullOrEmpty(strOriginal))
            {
                string strCompressed = ZipHelper.GZipCompressString(strOriginal);
                lblOriginalLength.Text = strOriginal.Length.ToString();
                txtCompressed.Text = strCompressed;
                lblCompressed.Text = strCompressed.Length.ToString();
                lblCompressRate.Text = (strCompressed.Length * 1.00 / strOriginal.Length * 100).ToString() + "%";
            }
        }

        protected void btnDeCompressed_Click(object sender, EventArgs e)
        {
            string strCompressed = txtCompressed.Text;
            if (!string.IsNullOrEmpty(strCompressed))
            {
                string strDeCompressed = ZipHelper.GZipDecompressString(strCompressed);
                lblDeCompressed.Text = strDeCompressed.Length.ToString();
                txtDeCompressed.Text = strDeCompressed;
            }
        }
    }
}
