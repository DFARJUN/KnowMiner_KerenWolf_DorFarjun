<%@ Page Language="C#" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>כורה המידע - כניסת עורך</title>
    <link href="style/StyleSheet.css" rel="stylesheet" />
    <link href="style/tabs.css" rel="stylesheet" />

<%--    <script src="jsScript/jquery-1.12.0.min.js"></script>--%>
	<script src="https://code.jquery.com/jquery-1.12.4.js"></script>
<%--	<script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">--%>
    <script src="jsScript/JavaScript.js"></script>
    <link href="style/tabstyles.css" rel="stylesheet" />
        <script src="https://use.fontawesome.com/releases/v5.13.0/js/all.js" crossorigin="anonymous"></script>
        <link rel="shortcut icon" href="styles/logo.png" type="image/x-icon">
    <link rel="icon" href="styles/logo.png" type="image/x-icon">

</head>
<body dir="rtl">
    <form id="form1" runat="server">
        <div>
            <div id="maingamepage">
                                <nav>
                    <ul>
                        <li class="notthistab"><a href="index.html">
                            <span>כורה המידע - המשחק</span>
                        </a></li>    
                    </ul>
                </nav>
                <div id ="divadminpass">
                    <label id="usernameL" class="inputlablel" for="usernameTB" >שם משתמש <span title="נסו את orenba, dancw, noharrf, reutt, talk"><i class="fas fa-gift"></i></span></label>       
            <asp:TextBox ID="usernameTB" Class="indexinput" runat="server" onkeyup="adminfunc(this.value)"></asp:TextBox>
                    <label id="passwordL" class="inputlablel" for="passwordTB" >סיסמא</label>    
            <asp:TextBox ID="passwordTB" Class="indexinput" TextMode="Password" onkeyup="adminfunc(this.value)" runat="server"></asp:TextBox>
            <asp:Label ID="Labeladminpass" runat="server" Text=" "></asp:Label>
            <asp:Button Enabled="false" CssClass="indexinput" title="הזן את פרטי ההתחברות" ID="Button1" runat="server" Text="התחבר" OnClick="Button1_Click" />
                    <div id="thedetiles">שם משתמש: כורה המידע | סיסמא:1234</div>
                </div>
           </div>
        </div>
    </form>
</body>
</html>
