<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>כורה המידע - המשחקים שלי</title>

    <link href="style/StyleSheet.css" rel="stylesheet" />
    <link href="style/tabs.css" rel="stylesheet" />
    <link href="style/tabstyles.css" rel="stylesheet" />
    <script src="jsScript/jquery-1.12.0.min.js"></script>
    <script src="jsScript/JavaScript.js"></script>
    <script src="https://unpkg.com/@lottiefiles/lottie-player@latest/dist/lottie-player.js"></script>
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
                        <li class="thistab"><a>
                            <span>המשחקים שלי</span>
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
                        <asp:Label ID="adminamelb" runat="server" Text="Label">Zipora teacher</asp:Label>
                        <asp:Image ID="ziporaavatar" runat="server" src="style/153-512.png" />
                    </div>
                </nav>
                <div id="gridblock">
                    <div id="serchtomove">
                        &nbsp;<asp:TextBox ID="addNameTB" placeholder="חיפוש על פי שם או קוד משחק" autocomplete="off" runat="server"></asp:TextBox>
                        &nbsp;<asp:Image ID="ImageButtonserch" runat="server" ImageUrl="~/images/add.png" />
                    </div>
                    <div id="nogame">
                        <h1>לא נמצאו משחקיים קיימים.</h1>
                        <h1>התחילו ביצירת משחק חדש</h1>
                        <lottie-player src="style/noresult.json" background="transparent" speed="1" style="width: 20%; height: 20%;" loop autoplay></lottie-player>
                    </div>
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="XmlDataSource1" BackColor="White" CellPadding="4" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" OnRowCommand="GridView1_RowCommand">
                        <Columns>
                            <asp:TemplateField HeaderText="שם המשחק">
                                <ItemTemplate>
                                    <span title="" data-original-title="לחץ להצגת המשחק" data-toggle="tooltip">
                                        <asp:Button Text='<%#Server.UrlDecode(XPathBinder.Eval(Container.DataItem, "GameSubject").ToString())%>' ToolTip="sfdhg fdshfd fsdhfd fdsh fsdhdsg" class="qbutton" CommandName="viewRow" theItemId='<%#XPathBinder.Eval(Container.DataItem,"@GameCode").ToString()%>' runat="server" />
                                    </span>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="GameCode" HeaderText="קוד המשחק" SortExpression="GameCode" />
                            <asp:TemplateField HeaderText="מספר שאלות">
                                <ItemTemplate>
                                    <asp:Label ID="GameQnumLbl" runat="server" Text='<%#XPathBinder.Eval(Container.DataItem, "questions/@Quantity").ToString()%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="עריכה">
                                <ItemTemplate>
                                    <asp:ImageButton ID="EditIMG" runat="server" ImageUrl="~/images/edit.png" CommandName="editRow" theItemId='<%#XPathBinder.Eval(Container.DataItem,"@GameCode").ToString()%>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="מחיקה">
                                <ItemTemplate>
                                    <input id="Button1" type="button" class="delebtnmain" onclick="showmodel(this)" />
                                    <div id="id01" onclick="document.getElementById('id01').style.display='none'">
                                        <div class="modal">
                                            <div class="container">
                                                <h1>האם ברצונך למחוק את המשחק: 
                                                    <asp:Label ID="Label2" runat="server" Text='<%#Server.UrlDecode(XPathBinder.Eval(Container.DataItem, "GameSubject").ToString())%>'></asp:Label><span id="gamename"></span>?</h1>
                                                <p>לא קיימת אפשרות לביטול מחיקת המשחק</p>
                                                <div class="clearfix">
                                                    <asp:Button ID="deleteIMG" runat="server" class="deletebtn" Text="מחק" CommandName="deleteRow" theItemId='<%#XPathBinder.Eval(Container.DataItem,"@GameCode").ToString()%>'></asp:Button>
                                                    <button type="button" onclick="hidemodel(this)" class="cancelbtn">אל תמחק את המשחק</button>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="המשחק פורסם?">
                                <ItemTemplate>
                                    <asp:CheckBox ID="ISpassCheckBox1" runat="server" CommandName="publishRow" Checked='<%#Convert.ToBoolean(XPathBinder.Eval(Container.DataItem,"@isPublish"))%>' theItemId='<%#XPathBinder.Eval(Container.DataItem,"@GameCode")%>'
                                        AutoPostBack="true" OnCheckedChanged="isPublish_CheckedChanged" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle BackColor="#99CCCC" />
                        <HeaderStyle />
                        <PagerStyle BackColor="#99CCCC" HorizontalAlign="Left" />
                        <RowStyle BackColor="White" />
                        <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                        <SortedAscendingCellStyle BackColor="#EDF6F6" />
                        <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                        <SortedDescendingCellStyle BackColor="#D6DFDF" />
                        <SortedDescendingHeaderStyle BackColor="#002876" />
                    </asp:GridView>

                </div>
                <div id="alldash">
                    <div class="dashboard">
                        <div class="dashicon">
                        </div>
                        <h2>מספר המשחקים שלי:</h2>
                        <asp:Label ID="dashgamenumber" runat="server" Text="Label"></asp:Label>
                    </div>

                    <div class="dashboard">
                        <div class="dashicon">
                        </div>
                        <h2>מספר הפעמים ששחקו במשחקים שיצרתי:</h2>
                        <asp:Label ID="Numofplayer" runat="server" Text="Label"></asp:Label>
                    </div>

                    <div class="dashboard">
                        <div class="dashicon">
                        </div>
                        <h2>משחקים שהייתי רוצה לפתח:</h2>
                        <asp:TextBox ID="TextBox1" runat="server" TextMode="MultiLine"></asp:TextBox>
                        <asp:ImageButton ID="ImageButton1" runat="server" OnClick="ImageButton1_Click1" />
                    </div>

                </div>

            </div>

            <div id="noresult">
                לא מצאנו את מה שחיפשת
            <lottie-player src="style/noresult.json" background="transparent" speed="1" style="width: 50px; height: 50px;" Loop autoplay></lottie-player>
            </div>

            <div id="id02" onclick="document.getElementById('id01').style.display='none'">
                <lottie-player src="style/lf20_30iie6.json" background="transparent" speed="1" style="width: 100%; height: 100%;" loop autoplay></lottie-player>
                <div class="modal">
                    <div class="container">
                        <h1>עשית את זה! המשחק ' 
                            <asp:Label ID="Label3" runat="server"></asp:Label>
                            ' פורסם.</h1>
                        <h1>קוד המשחק הינו:
                            <asp:Label ID="Label4" runat="server"></asp:Label>
                        </h1>
                        <div class="clearfix">
                            <button type="button" onclick="hidemodel(this)" class="cancelbtn">אישור</button>
                        </div>
                    </div>
                </div>
            </div>


            <asp:XmlDataSource ID="XmlDataSource1" runat="server" DataFile="~/tree/game.xml" XPath="/RootTree/game"></asp:XmlDataSource>
        </div>
    </form>
</body>
</html>
