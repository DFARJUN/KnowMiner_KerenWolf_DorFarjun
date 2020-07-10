using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Web.Services;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.IO;
using System.Drawing;



public partial class Edit : System.Web.UI.Page
{
    string imagesLibPath = "uploadedFiles/";

    protected void Page_Load(object sender, EventArgs e)
    {
        ifpublish();
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
        XmlDataSource2.XPath = "/RootTree/game[@GameCode=" + GameID + "]//question";
        GameSubjectTB.Text = Server.UrlDecode(mygame.InnerText);
        ViewState["quastionIDviewstate"] = "1";
        if (Page.IsPostBack == false)
        {
        Session["isgamepublish"] = myDoc.SelectSingleNode("/RootTree/game[@GameCode=" + GameID + "]/@isPublish").InnerText;
        }

        if (myDoc.SelectNodes("/RootTree/game[@GameCode=" + GameID + "]//question").Count == 0)
        {
        }
        else
        {
            refreshrow();
        }

        string gametime = myDoc.SelectSingleNode("//game[@GameCode=" + GameID + "]").Attributes["timePerQuest"].InnerText;
        RadioButtonListtime.SelectedValue = gametime;

    }


    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string GameID = Session["gameIDSession"].ToString();
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
        ViewState["quastionIDviewstate"] = theId;

        // עלינו לברר איזו פקודה צריכה להתבצע - הפקודה רשומה בכל כפתור             
        switch (e.CommandName)
        {
            case "deleteRow":
                deleteRow();
                break;

            case "editRow":
                refreshrow();
                break;
        }
    }

    protected void deleteRow()
    {
        //הסרת ענף של משחק קיים באמצעות זיהוי האיי דיי שניתן לו על ידי לחיצה עליו מתוך הטבלה
        //שמירה ועדכון לתוך העץ ולגריד ויו
        //במחיקה לא נוגעים באיי די קאונטר כדי שלא ייוצר כפילות 
        string GameID = Session["gameIDSession"].ToString();
        string qustionid = ViewState["quastionIDviewstate"].ToString();

        //לקיחת הקובץ 
        XmlDocument myDoc = new XmlDocument();
        myDoc.Load(Server.MapPath("/tree/game.xml"));

        XmlNode node = myDoc.SelectSingleNode("/RootTree/game[@GameCode=" + GameID + "]//question[@id=" + qustionid + "]");
          
        //פקודת המחיקה
        node.ParentNode.RemoveChild(node);


        ViewState["quastionIDviewstate"] = "1";

        int qcount = myDoc.SelectNodes("/RootTree/game[@GameCode=" + GameID + "]//question").Count;
        for (int q = 1; q <= qcount; q++)
        {
            XmlNode thisq = myDoc.SelectSingleNode("/RootTree/game[@GameCode=" + GameID + "]//question[" + q + "]");
            thisq.Attributes["id"].InnerText = q.ToString();
        }
                //שמירה
        myDoc.Save(Server.MapPath("/tree/game.xml"));
        GridView1edit.DataBind();
        refreshrow();

    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        XmlDocument myDoc = new XmlDocument();
        myDoc.Load(Server.MapPath("/tree/game.xml"));
        string GameID = Session["gameIDSession"].ToString();

        //יצירת שאילתה שתקבל לי את הסטודנט המסוים לפי האיי די
        //לזכור שצריך לפענח את התווים את המיוחדים כדי שנוכל לקרוא אותם.
        XmlNode titlegame = myDoc.SelectSingleNode("/RootTree/game[@GameCode=" + GameID + "]/GameSubject");

        // פנייה למאפיין אותו רוצים לשנות + שינוי
        titlegame.InnerXml = Server.UrlEncode(GameSubjectTB.Text);

        // שמירת העץ החדש
        myDoc.Save(Server.MapPath("/tree/game.xml"));
        XmlNode mygame = myDoc.SelectSingleNode("/RootTree/game[@GameCode=" + GameID + "]/GameSubject");
        GameSubjectLbl.Text = Server.UrlDecode(mygame.InnerText);
        GameSubjectTB.DataBind();
    }

    public void refreshrow()
    {
        string GameID = Session["gameIDSession"].ToString();
        string qustionid = ViewState["quastionIDviewstate"].ToString();
        XmlDocument myDoc = new XmlDocument();
        myDoc.Load(Server.MapPath("/tree/game.xml"));
        Qnum.Text= myDoc.SelectNodes("/RootTree/game[@GameCode=" + GameID + "]//question").Count.ToString();
        if(myDoc.SelectNodes("/RootTree/game[@GameCode=" + GameID + "]//question").Count >= 6)
        {
            ifqnum.Text = "<i class='fas fa-check'></i>";
        }
        else
        {
            ifqnum.Text = "<i class='fas fa-times'></i>";
        }

        int dl = myDoc.SelectNodes("/RootTree/game[@GameCode=" + GameID + "]//question[@id=" + qustionid + "]/answers/answer").Count;
        
        for (int l = 1; l <= 7; l++)
        {
            string d = Convert.ToString(l);
            string dd = Convert.ToString(l + 1);

            ((TextBox)FindControl("ATextBox" + d)).Text = "";
            ((CheckBox)FindControl("ACheckBox" + d)).Checked = false;
            ((ImageButton)FindControl("ImageforUpload" + dd)).ImageUrl = "~/Images/ImagePlaceholder.png";
            ((TextBox)FindControl("ATextBox" + d)).Enabled = true;
            ((ImageButton)FindControl("ImageforUpload" + dd)).Enabled = true;
            ((ImageButton)FindControl("ImageforUpload" + dd)).Style.Add("opacity", "1");
        }

        ((TextBox)FindControl("mainqtb")).Text = Server.UrlDecode(myDoc.SelectSingleNode("/RootTree/game[@GameCode=" + GameID + "]//question[@id=" + qustionid + "]/questionText").InnerText);
        if(myDoc.SelectSingleNode("/RootTree/game[@GameCode=" + GameID + "]//question[@id=" + qustionid + "]/img").InnerXml != "")
        {
            ((ImageButton)FindControl("ImageforUpload1")).ImageUrl = myDoc.SelectSingleNode("/RootTree/game[@GameCode=" + GameID + "]//question[@id=" + qustionid + "]/img").InnerXml;

        }
        else
        {
            ((ImageButton)FindControl("ImageforUpload1")).ImageUrl = "/Images/ImagePlaceholder.png";
        }

        for (int l = 1; l <= dl; l++)
        {
            string d = Convert.ToString(l);
            string dd = Convert.ToString(l + 1);

            if ((myDoc.SelectSingleNode("/RootTree/game[@GameCode=" + GameID + "]//question[@id=" + qustionid + "]/answers/answer[" + d + "]")).Attributes["AnsType"].InnerText == "text")
            {
                ((TextBox)FindControl("ATextBox" + d)).Text =Server.UrlDecode(myDoc.SelectSingleNode("/RootTree/game[@GameCode=" + GameID + "]//question[@id=" + qustionid + "]/answers/answer[" + d + "]").InnerXml);
                ((ImageButton)FindControl("ImageforUpload" + dd)).Enabled = false;
                ((ImageButton)FindControl("ImageforUpload" + dd)).Style.Add("opacity", "0.3");
                ((ImageButton)FindControl("ImageforUpload" + dd)).CssClass = "ImageButtongrid";
            }
            else
            {
                ((ImageButton)FindControl("ImageforUpload" + dd)).ImageUrl =myDoc.SelectSingleNode("/RootTree/game[@GameCode=" + GameID + "]//question[@id=" + qustionid + "]/answers/answer[" + d + "]").InnerXml;
                ((TextBox)FindControl("ATextBox" + d)).Enabled = false;
            }

            ((CheckBox)FindControl("ACheckBox" + d)).Checked = Convert.ToBoolean(myDoc.SelectSingleNode("/RootTree/game[@GameCode=" + GameID + "]//question[@id=" + qustionid + "]/answers/answer[" + d + "]/@feedback").InnerXml);
            Table1.DataBind();
        }


        int qcount = myDoc.SelectNodes("/RootTree/game[@GameCode=" + GameID + "]//question").Count;
        for (int q = 1; q <= qcount; q++)
        {
            XmlNode thisq = myDoc.SelectSingleNode("/RootTree/game[@GameCode=" + GameID + "]//question[" + q + "]");
            thisq.Attributes["id"].InnerText = q.ToString();
        }


        myDoc.SelectSingleNode("/RootTree/game[@GameCode=" + GameID + "]/questions").Attributes["Quantity"].InnerText = myDoc.SelectNodes("/RootTree/game[@GameCode=" + GameID + "]//question").Count.ToString();
        myDoc.Save(Server.MapPath("/tree/game.xml"));

    }



    protected void Button3_Click(object sender, EventArgs e)
    {
        string GameID = Session["gameIDSession"].ToString();
        string qustionid = ViewState["quastionIDviewstate"].ToString();
        XmlDocument myDoc = new XmlDocument();
        myDoc.Load(Server.MapPath("/tree/game.xml"));
        
        if(Convert.ToInt32(qustionid) > myDoc.SelectNodes("/RootTree/game[@GameCode=" + GameID + "]//question").Count)
        {
            XmlElement myNewaquestionNode = myDoc.CreateElement("question");
            myNewaquestionNode.SetAttribute("id", qustionid);

            XmlElement myNewaquestionTextNode = myDoc.CreateElement("questionText");
            myNewaquestionNode.AppendChild(myNewaquestionTextNode);

            XmlElement myNewimgNode = myDoc.CreateElement("img");
            myNewaquestionNode.AppendChild(myNewimgNode);

            XmlElement myNewanswerNode = myDoc.CreateElement("answers");
            myNewaquestionNode.AppendChild(myNewanswerNode);

            XmlNode myqthis = myDoc.SelectSingleNode("/RootTree/game[@GameCode=" + GameID + "]/questions");
            XmlNode Firstq = myDoc.SelectNodes("/RootTree/game[@GameCode=" + GameID + "]/questions/question").Item(0);
            myDoc.SelectSingleNode("/RootTree/game[@GameCode=" + GameID + "]/questions").InsertBefore(myNewaquestionNode, Firstq);
        }

        XmlNode titleq = myDoc.SelectSingleNode("/RootTree/game[@GameCode=" + GameID + "]//question[@id=" + qustionid + "]/questionText");
        titleq.InnerXml = Server.UrlEncode(((TextBox)FindControl("mainqtb")).Text);
        
        XmlNode titlimg = myDoc.SelectSingleNode("/RootTree/game[@GameCode=" + GameID + "]//question[@id=" + qustionid + "]/img");
        if ((((FileUpload)FindControl("FileUpload1")).PostedFile.ContentType.Contains("image")) || (((ImageButton)FindControl("ImageforUpload1")).ImageUrl.Contains("thisuplod") && (((HiddenField)FindControl("hdnfldVariable1")).Value != "false")))
        {

            if (((FileUpload)FindControl("FileUpload1")).PostedFile.ContentType.Contains("image"))
            {

                string fileType = ((FileUpload)FindControl("FileUpload1")).PostedFile.ContentType;

                if (fileType.Contains("image")) //בדיקה האם הקובץ שהוכנס הוא תמונה
                {
                    // הנתיב המלא של הקובץ עם שמו האמיתי של הקובץ
                    string fileName = ((FileUpload)FindControl("FileUpload1")).PostedFile.FileName;
                    // הסיומת של הקובץ
                    string endOfFileName = fileName.Substring(fileName.LastIndexOf("."));
                    //לקיחת הזמן האמיתי למניעת כפילות בתמונות
                    string myTime = DateTime.Now.ToString("dd_MM_yy-HH_mm_ss");
                    string mynamee = "thisuplod";
                    // חיבור השם החדש עם הסיומת של הקובץ
                    string imageNewName = mynamee + myTime + 1 + endOfFileName;
                    //שמירה של הקובץ לספרייה בשם החדש שלו
                    // Bitmap המרת הקובץ שיתקבל למשתנה מסוג
                    System.Drawing.Bitmap bmpPostedImage = new System.Drawing.Bitmap(((FileUpload)FindControl("FileUpload1")).PostedFile.InputStream);


                    //קריאה לפונקציה המקטינה את התמונה
                    //אנו שולחים לה את התמונה שלנו בגירסאת הביטמאפ ואת האורך והרוחב שאנו רוצים לתמונה החדשה
                    System.Drawing.Image objImage = FixedSize(bmpPostedImage, 150, 150);

                    //שמירה של הקובץ לספרייה בשם החדש שלו
                    objImage.Save(Server.MapPath(imagesLibPath) + imageNewName);

                    //הצגה של הקובץ החדש מהספרייה
                    ((ImageButton)FindControl("ImageforUpload1")).ImageUrl = imagesLibPath + imageNewName;

                    //XmlNode pic = myDoc.SelectSingleNode("/RootTree/game[@GameCode=" + GameID + "]//question[@id=" + qustionid + "]//answer[" + optionnum + "]");
                    titlimg.InnerXml = "/uploadedFiles" + "/" + imageNewName;
                }
            }

            if (((ImageButton)FindControl("ImageforUpload1")).ImageUrl.Contains("thisuplod"))
            {
                if (((HiddenField)FindControl("hdnfldVariable1")).Value != "false")
                {
                    titlimg.InnerXml = ((ImageButton)FindControl("ImageforUpload1")).ImageUrl;
                }
            }
        }
        else
        {
            titlimg.InnerXml = "";
        }




        XmlNode myqqthis = myDoc.SelectSingleNode("/RootTree/game[@GameCode=" + GameID + "]//question[@id=" + qustionid + "]");
        XmlNode node = myDoc.SelectSingleNode("/RootTree/game[@GameCode=" + GameID + "]//question[@id=" + qustionid + "]/answers");
        node.ParentNode.RemoveChild(node);
        XmlElement myNewanswersNode = myDoc.CreateElement("answers");
        myqqthis.AppendChild(myNewanswersNode);

        int optionnum = 1;
        for (int l = 1; l <= 7; l++){
            string d = Convert.ToString(l);
            string dd = Convert.ToString(l+1);
            if ((((TextBox)FindControl("ATextBox" + d)).Text!="") || (((FileUpload)FindControl("FileUpload" + dd)).PostedFile.ContentType.Contains("image")) || (((ImageButton)FindControl("ImageforUpload" + dd)).ImageUrl.Contains("thisuplod")&&(((HiddenField)FindControl("hdnfldVariable" + dd)).Value != "false")))
            {
                
                XmlElement myNewanswerNode = myDoc.CreateElement("answer");
                myNewanswerNode.SetAttribute("feedback", Convert.ToString(((CheckBox)FindControl("ACheckBox" + d)).Checked));
                myNewanswerNode.SetAttribute("AnsType", "text");
                
                
                if (((TextBox)FindControl("ATextBox" + d)).Text != "")
                {
                    myNewanswerNode.InnerXml = Server.UrlEncode(((TextBox)FindControl("ATextBox" + d)).Text);
                    myNewanswerNode.Attributes["AnsType"].InnerText = "text";
                }

                if (((FileUpload)FindControl("FileUpload" + dd)).PostedFile.ContentType.Contains("image"))
                {

                    string fileType = ((FileUpload)FindControl("FileUpload" + dd)).PostedFile.ContentType;

                    if (fileType.Contains("image")) //בדיקה האם הקובץ שהוכנס הוא תמונה
                    {
                        // הנתיב המלא של הקובץ עם שמו האמיתי של הקובץ
                        string fileName = ((FileUpload)FindControl("FileUpload" + dd)).PostedFile.FileName;
                        // הסיומת של הקובץ
                        string endOfFileName = fileName.Substring(fileName.LastIndexOf("."));
                        //לקיחת הזמן האמיתי למניעת כפילות בתמונות
                        string myTime = DateTime.Now.ToString("dd_MM_yy-HH_mm_ss");
                        string mynamee = "thisuplod";
                        // חיבור השם החדש עם הסיומת של הקובץ
                        string imageNewName = mynamee+myTime + dd + endOfFileName;

                        // Bitmap המרת הקובץ שיתקבל למשתנה מסוג
                        System.Drawing.Bitmap bmpPostedImage = new System.Drawing.Bitmap(((FileUpload)FindControl("FileUpload" + dd)).PostedFile.InputStream);


                        //קריאה לפונקציה המקטינה את התמונה
                        //אנו שולחים לה את התמונה שלנו בגירסאת הביטמאפ ואת האורך והרוחב שאנו רוצים לתמונה החדשה
                        System.Drawing.Image objImage = FixedSize(bmpPostedImage, 230, 230);

                        //שמירה של הקובץ לספרייה בשם החדש שלו
                        objImage.Save(Server.MapPath(imagesLibPath) + imageNewName);

                        //הצגה של הקובץ החדש מהספרייה
                        ((ImageButton)FindControl("ImageforUpload" + dd)).ImageUrl = imagesLibPath + imageNewName;

                            //XmlNode pic = myDoc.SelectSingleNode("/RootTree/game[@GameCode=" + GameID + "]//question[@id=" + qustionid + "]//answer[" + optionnum + "]");
                            myNewanswerNode.InnerXml = "/uploadedFiles" + "/" + imageNewName;
                            myNewanswerNode.Attributes["AnsType"].InnerText = "picture";
                        ((TextBox)FindControl("ATextBox" + d)).Enabled =false;

                    }
                }

                if (((ImageButton)FindControl("ImageforUpload" + dd)).ImageUrl.Contains("thisuplod"))
                {
                    if(((HiddenField)FindControl("hdnfldVariable"+dd)).Value != "false")
                    {
                    myNewanswerNode.InnerXml = ((ImageButton)FindControl("ImageforUpload" + dd)).ImageUrl;
                    myNewanswerNode.Attributes["AnsType"].InnerText = "picture";
                    ((TextBox)FindControl("ATextBox" + d)).Enabled = false;
                    }
                }
               

                XmlNode myqthis = myDoc.SelectSingleNode("/RootTree/game[@GameCode=" + GameID + "]//question[@id=" + qustionid + "]/answers");
                myqthis.AppendChild(myNewanswerNode);
                optionnum++;
            }
        }

        myDoc.Save(Server.MapPath("/tree/game.xml"));
        cleaneow();
        GridView1edit.DataBind();
        Table1.DataBind();
        refreshrow();
        newq();

    }


    protected void RadioButtonListtime_CheckedChanged(object sender, EventArgs e)
    {
        XmlDocument myDoc = new XmlDocument();
        myDoc.Load(Server.MapPath("/tree/game.xml"));
        string GameID = Session["gameIDSession"].ToString();
        XmlNode gametime = myDoc.SelectSingleNode("/RootTree/game[@GameCode=" + GameID + "]");
        gametime.Attributes["timePerQuest"].InnerText = RadioButtonListtime.SelectedItem.Value ;
        myDoc.Save(Server.MapPath("/tree/game.xml"));
    }



    protected void Button5_Click(object sender, EventArgs e)
    {
        newq();
    }

    protected void cleaneow()
    {
        mainqtb.Text = "";
        FileUpload1.Dispose();
        ImageforUpload1.ImageUrl = "/Images/ImagePlaceholder.png";

        for (int l = 1; l <= 7; l++)
        {
            string d = Convert.ToString(l);
            string dd = Convert.ToString(l + 1);
            ((TextBox)FindControl("ATextBox" + d)).Text = "";
            ((TextBox)FindControl("ATextBox" + d)).Enabled = true;
            ((CheckBox)FindControl("ACheckBox" + d)).Checked = false;
            ((FileUpload)FindControl("FileUpload" + dd)).Dispose();
            ((ImageButton)FindControl("ImageforUpload" + dd)).ImageUrl = "/Images/ImagePlaceholder.png";
            ((ImageButton)FindControl("ImageforUpload" + dd)).Style.Add("opacity", "1");
        }
    }


    protected void Button4_Click(object sender, EventArgs e)
    {
        Page.ClientScript.RegisterStartupScript(this.GetType(), "X", "endedit()", true);
    }

    static System.Drawing.Image FixedSize(System.Drawing.Image imgPhoto, int Width, int Height)
    {
        int sourceWidth = Convert.ToInt32(imgPhoto.Width);
        int sourceHeight = Convert.ToInt32(imgPhoto.Height);

        int sourceX = 0;
        int sourceY = 0;
        int destX = 0;
        int destY = 0;

        float nPercent = 0;
        float nPercentW = 0;
        float nPercentH = 0;

        nPercentW = ((float)Width / (float)sourceWidth);
        nPercentH = ((float)Height / (float)sourceHeight);
        if (nPercentH < nPercentW)
        {
            nPercent = nPercentH;
            destX = System.Convert.ToInt16((Width -
                          (sourceWidth * nPercent)) / 2);
        }
        else
        {
            nPercent = nPercentW;
            destY = System.Convert.ToInt16((Height -
                          (sourceHeight * nPercent)) / 2);
        }

        int destWidth = (int)(sourceWidth * nPercent);
        int destHeight = (int)(sourceHeight * nPercent);

        System.Drawing.Bitmap bmPhoto = new System.Drawing.Bitmap(Width, Height,
                          PixelFormat.Format24bppRgb);
        bmPhoto.SetResolution(imgPhoto.HorizontalResolution,
                         imgPhoto.VerticalResolution);

        System.Drawing.Graphics grPhoto = System.Drawing.Graphics.FromImage(bmPhoto);
        grPhoto.Clear(System.Drawing.Color.White);
        grPhoto.InterpolationMode =
                InterpolationMode.HighQualityBicubic;

        grPhoto.DrawImage(imgPhoto,
            new System.Drawing.Rectangle(destX, destY, destWidth, destHeight),
            new System.Drawing.Rectangle(sourceX, sourceY, sourceWidth, sourceHeight),
            System.Drawing.GraphicsUnit.Pixel);

        grPhoto.Dispose();
        return bmPhoto;
    }

    public void newq()
    {
        string GameID = Session["gameIDSession"].ToString();
        XmlDocument myDoc = new XmlDocument();
        myDoc.Load(Server.MapPath("/tree/game.xml"));
        int qcount = (myDoc.SelectNodes("/RootTree/game[@GameCode=" + GameID + "]//question").Count) + 1;
        ViewState["quastionIDviewstate"] = qcount;
        cleaneow();
        Table1.DataBind();
        GridView1edit.DataBind();
    }

    protected void Button6_Click(object sender, EventArgs e)
    {
        string GameID = Session["gameIDSession"].ToString();
        XmlDocument myDoc = new XmlDocument();
        myDoc.Load(Server.MapPath("/tree/game.xml"));
        int numofq = myDoc.SelectNodes("/RootTree/game[@GameCode=" + GameID + "]//question").Count;


        if (numofq > 5)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "X", "endeditandpublish()", true);
        }
        else
        {
            if (Session["isgamepublish"].ToString() == "True")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "X", "thegamewaspublish()", true);
            }
            else
            {
                Response.Redirect("Default.aspx");
            }
        }
    }


    protected void Button7_Click(object sender, EventArgs e)
    {
        Page.ClientScript.RegisterStartupScript(this.GetType(), "X", "publishgame()", true); 
        string GameID = Session["gameIDSession"].ToString();
        XmlDocument myDoc = new XmlDocument();
        myDoc.Load(Server.MapPath("/tree/game.xml"));
        myDoc.SelectSingleNode("/RootTree/game[@GameCode=" + GameID + "]/@isPublish").InnerText="True";
        Label3.Text = Server.UrlDecode(myDoc.SelectSingleNode("/RootTree/game[@GameCode=" + GameID + "]/GameSubject").InnerText);
        Label4.Text = myDoc.SelectSingleNode("/RootTree/game[@GameCode=" + GameID + "]/@GameCode").InnerText;
        myDoc.Save(Server.MapPath("/tree/game.xml"));

    }

    protected void Button8_Click(object sender, EventArgs e)
    {
        string GameID = Session["gameIDSession"].ToString();
        XmlDocument myDoc = new XmlDocument();
        myDoc.Load(Server.MapPath("/tree/game.xml"));
        myDoc.SelectSingleNode("/RootTree/game[@GameCode=" + GameID + "]/@isPublish").InnerText = "False";
        myDoc.Save(Server.MapPath("/tree/game.xml"));
        Response.Redirect("Default.aspx");

    }

    public void ifpublish()
    {
        string GameID = Session["gameIDSession"].ToString();
        XmlDocument myDoc = new XmlDocument();
        myDoc.Load(Server.MapPath("/tree/game.xml"));

            if (myDoc.SelectNodes("/RootTree/game[@GameCode="+GameID+"]//question").Count < 6)
            {
            myDoc.SelectSingleNode("/RootTree/game[@GameCode=" + GameID + "]/@isPublish").InnerText = "False";
            }
        myDoc.Save(Server.MapPath("/tree/game.xml"));
    }
}


