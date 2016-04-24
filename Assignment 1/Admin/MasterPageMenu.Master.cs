using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Assignment_1
{
    public partial class MasterPageMenu : System.Web.UI.MasterPage
    {
        protected void Page_Load (object sender, EventArgs e)
        {
            if (Page.IsPostBack)
                return;

            if (Session ["breadcrumb"] == null)
                Session ["breadcrumb"] = new List<string> ();

            List<string> breadcrumb = (List<string>) Session ["breadcrumb"];

            string pageName = Path.GetFileName (Page.AppRelativeVirtualPath);
            string pagePath = HttpContext.Current.Request.Url.AbsolutePath;

            string pageLink = "<a href = \"" + pagePath + "\">" + pageName + "</a>";

            if (!breadcrumb.Contains (pageLink))
                breadcrumb.Add (pageLink);

            for (Int32 pageIndex = 0; pageIndex < breadcrumb.Count; ++pageIndex)
            {
                if (!(pageIndex == 0 || pageIndex == breadcrumb.Count))
                    visited.Text += " >> ";

                visited.Text += breadcrumb.ElementAt (pageIndex);
            }
        }
    }
}