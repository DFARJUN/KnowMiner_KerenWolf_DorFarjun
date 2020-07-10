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
        removeempty();
        loadavatar();
    }

    protected void Page_init(object sender, EventArgs e)
    {
        XmlDocument myDoc = new XmlDocument();
        myDoc.Load(Server.MapPath("/tree/game.xml"));
        TextBox1.Text = Server.UrlDecode(myDoc.SelectSingleNode("//teachernote").InnerText);
        TextBox1.DataBind();
    }


        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        string nameserch = addNameTB.Text;
        XmlDataSource1.XPath = "/RootTree/game[GameSubject[contains(text(),'"+nameserch+"')]]";
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string theId = "000";
        if (e.CommandSource is ImageButton)
        {
            // תחילה אנו מבררים מהו ה -אי די- של הפריט בעץ ה אקס אם אל
            ImageButton i = (ImageButton)e.CommandSource; //הכנסת האימג' בטון לתוך משתנה ובירור על מי לחצנו
            theId = i.Attributes["theItemId"];
        }
        if (e.CommandSource is Button)
        {
            Button i = (Button)e.CommandSource;
            theId = i.Attributes["theItemId"];
        }

        Session["gameIDSession"] = theId;
        // עלינו לברר איזו פקודה צריכה להתבצע - הפקודה רשומה בכל כפתור             
        switch (e.CommandName)
        {
            //אם נלחץ על כפתור מחיקה יקרא לפונקציה של מחיקה                    
            case "deleteRow":
                deleteRow();
                break;
            
            case "viewRow":
                Response.Redirect("view.aspx");
                break;

            //אם נלחץ על כפתור עריכה (העפרון) נעבור לדף עריכה                    
            case "editRow":
                Response.Redirect("Edit.aspx");
                break;
        }
    }

    protected void deleteRow()
    {
        //הסרת ענף של משחק קיים באמצעות זיהוי האיי דיי שניתן לו על ידי לחיצה עליו מתוך הטבלה
        //שמירה ועדכון לתוך העץ ולגריד ויו
        //במחיקה לא נוגעים באיי די קאונטר כדי שלא ייוצר כפילות 
        string GameID = Session["gameIDSession"].ToString();

        //לקיחת הקובץ 
        XmlDocument Document = XmlDataSource1.GetXmlDocument();

            XmlNode node = Document.SelectSingleNode("/RootTree/game[@GameCode='" + GameID + "']");
            //פקודת המחיקה
            node.ParentNode.RemoveChild(node);
            //שמירה
            XmlDataSource1.Save();
            GridView1.DataBind();
              removeempty();

    }

    protected void isPublish_CheckedChanged(object sender, EventArgs e)
    {
        //בדיקה האם הסטודנט עבר או לא/ המשחק פורסם או לא. 
        //לוקח את הערך העדכני של מה שיש בגריד. אם זה לא מסומן אז הוא פולס ואם לוחצים עליו ניתן לראות בעץ שהערך ישתנה לטרו
        //אם הערך מסומן ואני לוחצת כדי לבטל את הסימון אנצ'קט אז הערך העץ ישתנה לפולס
        // טעינה של העץ
        XmlDocument xmlDoc = XmlDataSource1.GetXmlDocument();

        // תחילה אנו מבררים מהו ה -אי די- של הפריט בעץ ה אקס אם אל
        CheckBox myCheckBox = (CheckBox)sender;

        // מושכים את האי די של הפריט באמצעות המאפיין שהוספנו באופן ידני לתיבה
        string theId = myCheckBox.Attributes["theItemId"];

        //שאילתא למציאת הסטודנט שברצוננו לעדכן
        XmlNode theStudents = xmlDoc.SelectSingleNode("/RootTree/game[@GameCode='" + theId + "']");

        //קבלת הערך החדש של התיבה לאחר הלחיצה
        bool NewIsPass = myCheckBox.Checked;

        //עדכון של המאפיין בעץ
        theStudents.Attributes["isPublish"].InnerText = NewIsPass.ToString();

        //שמירה בעץ והצגה
        XmlDataSource1.Save();
        GridView1.DataBind();

        if (theStudents.Attributes["isPublish"].InnerText == "True")
        {
        Page.ClientScript.RegisterStartupScript(this.GetType(), "X", "showmodel2()", true);
        Label3.Text = Server.UrlDecode(xmlDoc.SelectSingleNode("/RootTree/game[@GameCode='" + theId + "']/GameSubject").InnerText.ToString());
        Label4.Text = theId;
        }
       

    }

    public void removeempty()
    {
        XmlDocument myDoc = new XmlDocument();
        myDoc.Load(Server.MapPath("/tree/game.xml"));

        Numofplayer.Text= myDoc.SelectSingleNode("//childcounter").InnerText;
        int numofgame = myDoc.SelectNodes("/RootTree/game").Count;
        dashgamenumber.Text= numofgame.ToString();

        Numofplayer.DataBind();
        dashgamenumber.DataBind();


    }
    
    public void loadavatar()
    {
        XmlDocument myDoc = new XmlDocument();
        myDoc.Load(Server.MapPath("/tree/avatar.xml"));
        string admin = "";
        if(Session["theadmin"] !=null)
        {
            admin = Session["theadmin"].ToString().ToLower();
        }
        if (myDoc.SelectSingleNode("//admin[@code='" + admin + "']/name") == null)
        {

        }
        else
        {
        adminamelb.Text = myDoc.SelectSingleNode("//admin[@code='"+ admin +"']/name").InnerText.ToString();
        ziporaavatar.Attributes["src"] = "/style/avatarimg/" + admin + ".png";
        }


    }


    protected void ImageButton1_Click1(object sender, ImageClickEventArgs e)
    {
        XmlDocument myDoc = new XmlDocument();
        myDoc.Load(Server.MapPath("/tree/game.xml"));
        myDoc.SelectSingleNode("//teachernote").InnerText = Server.UrlEncode(TextBox1.Text);
        myDoc.Save(Server.MapPath("/tree/game.xml"));
        TextBox1.DataBind();
    }
}