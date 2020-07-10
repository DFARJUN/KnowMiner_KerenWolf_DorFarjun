<%@ WebHandler Language="C#" Class="Handler" %>
using System;
using System.Web;
using System.Xml;
using Newtonsoft.Json;

public class Handler : IHttpHandler
{
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";

        string gameCode = context.Request["gameCode"]; //קוד המשחק שנשלח מאנימייט

        XmlDocument myDoc = new XmlDocument();
        myDoc.Load(context.Server.MapPath("tree/game.xml")); //טעינת העץ שלכם

        XmlNode gameNode = myDoc.SelectSingleNode("//game[@GameCode='" + gameCode + "']"); //שליפת הענף של המשחק המתאים

        if (gameNode != null) //אם קיים משחק שתואם לקוד
        {
            //כאן תתבצע הבדיקה לפרסום
            myDoc.SelectSingleNode("//childcounter").InnerXml = (Convert.ToInt32(myDoc.SelectSingleNode("//childcounter").InnerXml) + 1).ToString();
           myDoc.Save(context.Server.MapPath("/tree/game.xml"));
            //ההמרה לג'ייסון תתבצע רק אם המשחק קיים ומפורסם
            string jsonText = JsonConvert.SerializeXmlNode(gameNode); //המרת הענף מהעץ לטקסט במבנה של ג'ייסון
            context.Response.Write(jsonText); //שליחת המחרוזת אל אנימייט
        }
        else //אם המשחק לא קיים
        {
            context.Response.Write("noGameFound"); //שליחת תשובה שלא נמצא משחק
        }
    }

    public bool IsReusable
    {
        get
        {
            return true;
        }
    }
}
















