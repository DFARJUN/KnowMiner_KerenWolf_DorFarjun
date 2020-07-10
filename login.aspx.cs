using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;


public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void Button1_Click(object sender, EventArgs e)
    {

        XmlDocument myDoc = new XmlDocument();
        string adminn = usernameTB.Text.ToLower();
        string password = passwordTB.Text;
        myDoc.Load(Server.MapPath("/tree/avatar.xml"));
        if (myDoc.SelectSingleNode("//admin[@code='" + adminn + "']/name") == null)
        {
            if ((password == "1234") && (adminn == "כורה המידע"))
            {
                Session["theadmin"] = adminn;
                Response.Redirect("Default.aspx");
            }
            else
            {
                Labeladminpass.Text = "שם המשתמש או הסיסמא אינם נכונים";
            }

        }
        else
        {
if ( (password=="1234"))
        {
            Session["theadmin"] = adminn;
            Response.Redirect("Default.aspx");
        }
        else
        {
            Labeladminpass.Text = "שם המשתמש או הסיסמא אינם נכונים";
        }
        }
    
    }
}