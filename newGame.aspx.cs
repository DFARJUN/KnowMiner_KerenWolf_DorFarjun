using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

public partial class Edit : System.Web.UI.Page
{
    protected void Page_init(object sender, EventArgs e)
    {

    }



    protected void deleteIMG_Click(object sender, EventArgs e)
    {

        XmlDocument myDoc = new XmlDocument();
        myDoc.Load(Server.MapPath("/tree/game.xml"));

        //--הקפצה של ה- ID--//
        int myId = Convert.ToInt16(myDoc.SelectSingleNode("//idCounter").InnerXml);
        myId++;
        string myNewId = myId.ToString(); //אם אני רוצה ליצור קוד של 106, נוסיף את 10 ידנית ואז את שם המשתנה
        myDoc.SelectSingleNode("//idCounter").InnerText = myNewId;
        Session["gameIDSession"] = myNewId; 

        // יצירת ענף משחק     
        XmlElement myNewStudentNode = myDoc.CreateElement("game");
        myNewStudentNode.SetAttribute("GameCode", myNewId);
        myNewStudentNode.SetAttribute("timePerQuest", "30");
        myNewStudentNode.SetAttribute("isPublish", "false");
        myNewStudentNode.SetAttribute("protected", "false");

        // יצירת ענף שם הסטודנט
        // במשחק שלנו לא להוסיף תוכן אלא רקליצור את התשתית עצמה
        XmlElement myNewNameNode = myDoc.CreateElement("GameSubject");
        //הוספת טקסט עם יכולת קידוד
        myNewNameNode.InnerXml = Server.UrlEncode(GameSubjectTB.Text);        //לא להוסיף את זה בהתחלה
        myNewStudentNode.AppendChild(myNewNameNode);

        // יצירת ענף ציונים ללא הציונים עצמם
        XmlElement myGradesNode = myDoc.CreateElement("questions");
        myGradesNode.SetAttribute("Quantity", "0");
        myNewStudentNode.AppendChild(myGradesNode);

        // הוספת ענף התלמיד לעץ כתלמיד הראשון
        XmlNode FirstStudent = myDoc.SelectNodes("/RootTree/game").Item(0);
        myDoc.SelectSingleNode("/RootTree").InsertBefore(myNewStudentNode, FirstStudent);
        myDoc.Save(Server.MapPath("/tree/game.xml"));


        //
        Response.Redirect("Edit.aspx");
    }

    protected void Button2_Click(object sender, EventArgs e)
    {

    }
}

