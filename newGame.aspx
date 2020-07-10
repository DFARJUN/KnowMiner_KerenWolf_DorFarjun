<%@ Page Language="C#" AutoEventWireup="true" CodeFile="newGame.aspx.cs" Inherits="Edit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>כורה המידע - משחק חדש</title>
    <link href="style/StyleSheet.css" rel="stylesheet" />
    <link href="style/tabs.css" rel="stylesheet" />
    <link href="style/tabstyles.css" rel="stylesheet" />
    <script src="jsScript/jquery-1.12.0.min.js"></script>
    <script src="jsScript/JavaScript.js"></script>
        <link rel="shortcut icon" href="styles/logo.png" type="image/x-icon">
    <link rel="icon" href="styles/logo.png" type="image/x-icon">
</head>
<body dir="rtl">
    <form id="form1" runat="server">
        <div>

            <div class="tabs tabs-style-shape">
                <svg class="hidden" style="height: 0px">
			<defs>
				<path id="tabshape" d="M80,60C34,53.5,64.417,0,0,0v60H80z"/>
			</defs>
		</svg>
                <nav>
                    <ul>
                        <li class="notthistab"><a href="Default.aspx">
	 		<svg viewBox="0 0 80 60" preserveAspectRatio="none"><use xlink:href="#tabshape"></use></svg>
			<span>המשחקים שלי</span>
            </a></li>

               <li class="thistab"><a>
			<svg viewBox="0 0 80 60" preserveAspectRatio="none"><use xlink:href="#tabshape"></use></svg>
            <svg viewBox="0 0 80 60" preserveAspectRatio="none"><use xlink:href="#tabshape"></use></svg>
			<span>יצירת משחק חדש</span>
            </a></li> 
                    </ul>
                </nav>
            
            </div>
                    <div id="maingamepage">
            
                            <div id="id01" style="display:block">
                                <div class="modal">
                                        <div class="container">
                                            <h1>משחק חדש צריך שם</h1>
                                            <p>ניתן לשנות את שם המשחק בכל שלב</p>
                                                                <asp:TextBox ID="GameSubjectTB" MaxLength="40" runat="server" onkeyup="charcountupdatenew(this.value)"></asp:TextBox>
                    <div class="charcount"></div>
                                            <div class="clearfix">
                                                        <asp:Button ID="newgameIMG" runat="server" class="deletebtn" Text="עריכת המשחק החדש" CommandName="deleteRow" OnClick="deleteIMG_Click" Enabled="false"></asp:Button>
                                                        <button type="button" onclick="location.href='Default.aspx'" class="cancelbtn">חזור למסך המשחקים שלי</button>
                                            </div>
                                        </div>
                                </div>
                            </div>


        </div>




        </div>




    </form>
</body>
</html>
