using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Web.Services;
using System.Web.UI.HtmlControls;

public partial class Edit : System.Web.UI.Page
{
    string imagesLibPath = "KnowMiner/uploadedFiles/";

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Page_Init(object sender, EventArgs e)
    {
        string GameID = Session["gameIDSession"].ToString();
        XmlDocument myDoc = new XmlDocument();
        myDoc.Load(Server.MapPath("/tree/game.xml"));

        //יצירת שאילתה שתקבל לי את הסטודנט המסוים לפי האיי די
        //לזכור שצריך לפענח את התווים את המיוחדים כדי שנוכל לקרוא אותם.
        XmlNode mygame = myDoc.SelectSingleNode("/RootTree/game[@GameCode=" + GameID + "]/GameSubject");
        GameSubjectLbl.Text = Server.UrlDecode(mygame.InnerText);
        ViewState["quastionIDviewstate"] = "1";
        string gametime = myDoc.SelectSingleNode("//game[@GameCode=" + GameID + "]").Attributes["timePerQuest"].InnerText;
        createacurd();
    }

    protected void createacurd()
    {
        string GameID = Session["gameIDSession"].ToString();
        XmlDocument myDoc = new XmlDocument();
        myDoc.Load(Server.MapPath("/tree/game.xml"));
        int qunum = myDoc.SelectNodes("//game[@GameCode=" + GameID + "]//question").Count;


        for(int i=1; i <= qunum; i++)
        {
            string d = i.ToString();
            HtmlInputButton mylbl1 = new HtmlInputButton();
            mylbl1.Attributes["class"] = "accordion";
            mylbl1.Value = Server.UrlDecode(myDoc.SelectSingleNode("/RootTree/game[@GameCode=" + GameID + "]//question[@id="+ d +"]/questionText").InnerText);
            Panel1.Controls.Add(mylbl1);

            int ansnum = myDoc.SelectNodes("/RootTree/game[@GameCode=" + GameID + "]//question[@id=" + d + "]//answer").Count;
            Table thetable = new Table();
            thetable.Attributes["class"] = "panel";
            TableRow trd = new TableRow();
            TableCell tdt = new TableCell();
            trd.Controls.Add(tdt);
            TableCell td = new TableCell();
            if (myDoc.SelectSingleNode("/RootTree/game[@GameCode=" + GameID + "]//question[@id=" + d + "]/img").InnerText != "")
            {
                Image tdi = new Image();
                tdi.ImageUrl = myDoc.SelectSingleNode("/RootTree/game[@GameCode=" + GameID + "]//question[@id=" + d + "]/img").InnerText;
                tdi.Attributes["class"] = "dordor Qimg";
                td.Attributes["class"] = "tdimg";
                td.Controls.Add(tdi);
            }
            trd.Controls.Add(td);
            thetable.Controls.Add(trd);

            for (int l = 1; l <= ansnum; l++)
            {
                TableRow tr = new TableRow();
                TableCell tct = new TableCell();
                if (myDoc.SelectSingleNode("/RootTree/game[@GameCode=" + GameID + "]//question[@id=" + d + "]//answer[" + l + "]/@feedback").InnerText == "True")
                {
                    tct.Text = "<i class='fas fa-check'></i>";
                }
                if (myDoc.SelectSingleNode("/RootTree/game[@GameCode=" + GameID + "]//question[@id=" + d + "]//answer[" + l + "]/@feedback").InnerText == "False")
                {
                    tct.Text = "<i class='fas fa-times'></i>";
                }
                tr.Controls.Add(tct);
                TableCell tc = new TableCell();

                if (myDoc.SelectSingleNode("/RootTree/game[@GameCode=" + GameID + "]//question[@id=" + d + "]//answer[" + l + "]/@AnsType").InnerText == "picture")
                {
                    Image tci = new Image();
                    tci.ImageUrl = myDoc.SelectSingleNode("/RootTree/game[@GameCode=" + GameID + "]//question[@id=" + d + "]//answer[" + l + "]").InnerText;
                    tci.Attributes["class"] = "dordor";
                    tc.Controls.Add(tci);
                }
                else
                {
                    tc.Text = Server.UrlDecode(myDoc.SelectSingleNode("/RootTree/game[@GameCode=" + GameID + "]//question[@id=" + d + "]//answer[" + l + "]").InnerText);
                }
                tr.Controls.Add(tc);
                thetable.Controls.Add(tr);

            }
            Panel1.Controls.Add(thetable);
        }
        Panel1.DataBind();
    }
}


