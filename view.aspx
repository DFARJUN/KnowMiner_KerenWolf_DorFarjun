<%@ Page Language="C#" AutoEventWireup="true" CodeFile="view.aspx.cs" Inherits="Edit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>כורה המידע - צפייה במשחק</title>
    <link href="style/tabs.css" rel="stylesheet" />
    <link href="style/tabstyles.css" rel="stylesheet" />
    <link href="style/StyleSheet.css" rel="stylesheet" />
    <script src="jsScript/jquery-1.12.0.min.js"></script>
    <script src="jsScript/JavaScript.js"></script>
    <script src="https://use.fontawesome.com/releases/v5.13.0/js/all.js" crossorigin="anonymous"></script>
        <link rel="shortcut icon" href="styles/logo.png" type="image/x-icon">
    <link rel="icon" href="styles/logo.png" type="image/x-icon">
</head>
<body dir="rtl">
    <form id="form1" runat="server">
        <asp:ScriptManager runat="server" EnablePageMethods="true" />
        <div>
            <div id="maingamepage">
                                                <nav>
                    <ul>
                        <li class="notthistab"><a href="index.html">
                            <span>כורה המידע - המשחק</span>
                        </a></li>
                        <li class="notthistab"><a href="Default.aspx">
                            <span>המשחקים שלי</span>
                        </a>
                        </li>
                        <li class="thistab"><a>
                                <asp:Label ID="GameSubjectLbl" runat="server" Text="Label"></asp:Label></span>
                        </a>
                            <span></span>
                        </li>
                        <li class="newgamebtn">
                            <a href="newGame.aspx">
                                <span>+ יצירת משחק חדש</span>
                            </a>
                        </li>
                    </ul>
                    <div>
                        <asp:Label ID="Label1" runat="server" Text="Label">Zipora teacher</asp:Label>
                        <asp:Image ID="Image1" runat="server" src="style/153-512.png" />
                    </div>
                </nav>
                   <section id="topeditsec">
                    <asp:TextBox ID="GameSubjectTB" maxchart="40" runat="server" onkeyup="charcountupdate(this.value)"></asp:TextBox>
                </section>
                <input id="Button1" type="button" onclick="expand()" value="הרחב הכל" />
                <input id="Button2" type="button" onclick="Shrink()" value="כווץ הכל" />
                <div id="viewdiv">
                    <asp:Panel ID="Panel1" runat="server"></asp:Panel>
                </div>
            </div>
            <div id="blackwithimg">
                <span><i class='fas fa-times'></i></span>
                <img id="theimg" alt="" src="" />
            </div>
            </div>
    </form>
</body>
</html>
