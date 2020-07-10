<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Edit.aspx.cs" Inherits="Edit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>כורה המידע - עריכת משחק</title>
    <link href="style/tabs.css" rel="stylesheet" />
    <link href="style/tabstyles.css" rel="stylesheet" />
    <link href="style/StyleSheet.css" rel="stylesheet" />
    <script src="jsScript/jquery-1.12.0.min.js"></script>
    <script src="jsScript/JavaScript.js"></script>
    <script src="https://use.fontawesome.com/releases/v5.13.0/js/all.js" crossorigin="anonymous"></script>
    <script src="https://unpkg.com/@lottiefiles/lottie-player@latest/dist/lottie-player.js"></script>
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
                    <asp:TextBox ID="GameSubjectTB" MaxLength="40" runat="server" onkeyup="charcountupdate(this.value)"></asp:TextBox>
                    <div class="charcount"></div>
                    <asp:Button ID="Button2" runat="server" Text="Button" OnClick="Button2_Click" />
                    <asp:RadioButtonList ID="RadioButtonListtime" runat="server" RepeatDirection="Horizontal" Width="320px" AutoPostBack="True" OnDataBound="RadioButtonListtime_CheckedChanged" OnSelectedIndexChanged="RadioButtonListtime_CheckedChanged">
                        <asp:ListItem Value="unlimited">ללא הגבלה</asp:ListItem>
                        <asp:ListItem Value="20">20</asp:ListItem>
                        <asp:ListItem Selected="True" Value="30">30</asp:ListItem>
                        <asp:ListItem Value="60">60</asp:ListItem>
                        <asp:ListItem Value="90">90</asp:ListItem>
                    </asp:RadioButtonList>
                </section>

                <section id="manineditsec">
                    <div id="editinright">
                        <div class="qnuminst">
                        <div>השאלות <span>(<asp:Label ID="Qnum" runat="server" Text="Label"></asp:Label>)</span> </div>
                        <div><asp:Label ID="ifqnum" runat="server" Text="Label"></asp:Label> לפחות 6 שאלות לפרסום</div>
                        </div>
                        <input type="button" ID="newqbtn" onclick="checkifhaveq()"  value="+ שאלה חדשה">
                        <div id="editdivscroll">
                    <asp:GridView ID="GridView1edit" class="Gridviewedit" runat="server" AutoGenerateColumns="False" DataSourceID="XmlDataSource2" OnRowCommand="GridView1_RowCommand">
                        <Columns>
                            <asp:TemplateField HeaderText="שאלה">
                                <ItemTemplate>
                                    <asp:Button CommandName="editRow" ID="GameSubjectLbl5" class="qbutton" runat="server" Text='<%#Server.UrlDecode(XPathBinder.Eval(Container.DataItem, "questionText").ToString())%>' theItemId='<%#XPathBinder.Eval(Container.DataItem,"@id").ToString()%>'></asp:Button>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="מחיקה">
                                <ItemTemplate>
                                    <input id="Button1" type="button" class="delebtnmain" onclick="showmodel(this)" />
                                    <div id="id01" onclick="document.getElementById('id01').style.display='none'">
                                        <div class="modal">
                                                <div class="container">
                                                    <h1>האם ברצונך למחוק את השאלה <span class="gamename"></span>?</h1>
                                                    <p>לא קיימת אפשרות לביטול מחיקת המשחק</p>

                                                    <div class="clearfix">
                                                        <asp:Button ID="deleteIMG" runat="server" class="deletebtn" Text="מחק" CommandName="deleteRow" theItemId='<%#XPathBinder.Eval(Container.DataItem,"@id").ToString()%>'></asp:Button>
                                                        <button type="button" onclick="hidemodel(this)" class="cancelbtn">אל תמחק את המשחק</button>
                                                    </div>
                                                </div> 
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                        </div>

                    <asp:XmlDataSource ID="XmlDataSource2" runat="server" DataFile="~/tree/game.xml" XPath="/RootTree/game[@GameCode=]//question"></asp:XmlDataSource>
                    </div>
                    <section id="editqsec" class="Gridviewedit">
                                                <div class="qnuminst">
                        <div>השאלה <span>(2-100 תווים ואפשרות להוספת תמונה)</span> </div>
                        </div>
                        <div>
                            <asp:TextBox ID="mainqtb" Maxchar="100" runat="server" TextMode="MultiLine" Width="78%" Height="40px"  onkeyup="charcountupdate(this.value)"></asp:TextBox>
                            <div class="charcount"></div>
                            <asp:FileUpload ID="FileUpload1" Class="FileUploadgrid" runat="server" />
                            <asp:ImageButton ID="ImageforUpload1" runat="server" ImageUrl="~/Images/ImagePlaceholder.png" OnClientClick="openFileUploader1(); return false;" />
                            <input id="IButton1" class="notdel" type="button" value="X" onclick="return false;"/>                                    
                            <asp:HiddenField ID="hdnfldVariable1" runat="server" />
                        </div>
                                              <div class="qnuminst">
                        <div>אפשרויות תשובה <span>(2-40 תווים או תמונה)</span> </div>

                        <div>
                            <i ID="threeans" class='fas fa-check'></i> לפחות 3 אפשרויות תשובה <span style="display:inline-block;width:6px;">   </span> <i ID="onecorrect" class='fas fa-times'></i> 1-5 תשובות נכונות</div>
                        </div>

                        <asp:Table ID="Table1" runat="server">
                            <asp:TableRow>
                                <asp:TableCell>
                                    <asp:CheckBox ID="ACheckBox1" runat="server" />
                                </asp:TableCell>
                                <asp:TableCell>
                                    <asp:TextBox ID="ATextBox1" MaxLength="40" runat="server" onkeyup="charcountupdate(this.value)"></asp:TextBox>
                                                        <div class="charcount"></div>
                                </asp:TableCell>
                                <asp:TableCell>
                                    או
                                </asp:TableCell>
                                <asp:TableCell>
                                    <asp:FileUpload ID="FileUpload2" Class="FileUploadgrid" runat="server" />
                                    <asp:ImageButton ID="ImageforUpload2" Class="ImageButtongrid" runat="server" ImageUrl="~/Images/ImagePlaceholder.png" OnClientClick="openFileUploader2(); return false;" />
                                    <input id="IButton2" class="del" type="button" value="X" onclick="return false;"/>                                    
                                    <asp:HiddenField ID="hdnfldVariable2" runat="server" />
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableCell>
                                    <asp:CheckBox ID="ACheckBox2" runat="server" />
                                </asp:TableCell>
                                <asp:TableCell>
                                    <asp:TextBox ID="ATextBox2" runat="server" MaxLength="40" onkeyup="charcountupdate(this.value)"></asp:TextBox>
                                                        <div class="charcount"></div>
                                </asp:TableCell>
                                                                <asp:TableCell>
                                    או
                                </asp:TableCell>
                                <asp:TableCell>
                                    <asp:FileUpload ID="FileUpload3" Class="FileUploadgrid" runat="server" />
                                    <asp:ImageButton ID="ImageforUpload3" Class="ImageButtongrid" runat="server" ImageUrl="~/Images/ImagePlaceholder.png" OnClientClick="openFileUploader3(); return false;" />
                                    <input id="IButton3" class="del" type="button" value="X" onclick="return false;"/>                                    
                                    <asp:HiddenField ID="hdnfldVariable3" runat="server" />
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableCell>
                                    <asp:CheckBox ID="ACheckBox3" runat="server"  />
                                </asp:TableCell>
                                <asp:TableCell>
                                    <asp:TextBox ID="ATextBox3" runat="server" MaxLength="40" onkeyup="charcountupdate(this.value)"></asp:TextBox>
                                                        <div class="charcount"></div>
                                </asp:TableCell>
                                                                <asp:TableCell>
                                    או
                                </asp:TableCell>
                                <asp:TableCell>
                                    <asp:FileUpload ID="FileUpload4" Class="FileUploadgrid" runat="server" />
                                    <asp:ImageButton ID="ImageforUpload4" Class="ImageButtongrid" runat="server" ImageUrl="~/Images/ImagePlaceholder.png" OnClientClick="openFileUploader4(); return false;" />
                                    <input id="IButton4" class="del" type="button" value="X" onclick="return false;"/>                                    
                                    <asp:HiddenField ID="hdnfldVariable4" runat="server" />
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableCell>
                                    <asp:CheckBox ID="ACheckBox4" runat="server" />
                                </asp:TableCell>
                                <asp:TableCell>
                                    <asp:TextBox ID="ATextBox4" runat="server" MaxLength="40" onkeyup="charcountupdate(this.value)"></asp:TextBox>
                                                        <div class="charcount"></div>
                                </asp:TableCell>
                                                                <asp:TableCell>
                                    או
                                </asp:TableCell>
                                <asp:TableCell>
                                    <asp:FileUpload ID="FileUpload5" Class="FileUploadgrid" runat="server" />
                                    <asp:ImageButton ID="ImageforUpload5" Class="ImageButtongrid" runat="server" ImageUrl="~/Images/ImagePlaceholder.png" OnClientClick="openFileUploader5(); return false;" />
                                    <input id="IButton5" class="del" type="button" value="X" onclick="return false;"/>                                    
                                    <asp:HiddenField ID="hdnfldVariable5" runat="server" />
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableCell>
                                    <asp:CheckBox ID="ACheckBox5" runat="server" />
                                </asp:TableCell>
                                <asp:TableCell>
                                    <asp:TextBox ID="ATextBox5" runat="server" MaxLength="40" onkeyup="charcountupdate(this.value)"></asp:TextBox>
                                                        <div class="charcount"></div>
                                </asp:TableCell>
                                                                <asp:TableCell>
                                    או
                                </asp:TableCell>
                                <asp:TableCell>
                                    <asp:FileUpload ID="FileUpload6" Class="FileUploadgrid" runat="server" />
                                    <asp:ImageButton ID="ImageforUpload6" Class="ImageButtongrid" runat="server" ImageUrl="~/Images/ImagePlaceholder.png" OnClientClick="openFileUploader6(); return false;" />
                                    <input id="IButton6" class="del" type="button" value="X" onclick="return false;"/>
                                    <asp:HiddenField ID="hdnfldVariable6" runat="server" />
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableCell>
                                    <asp:CheckBox ID="ACheckBox6" runat="server" />
                                </asp:TableCell>
                                <asp:TableCell>
                                    <asp:TextBox ID="ATextBox6" runat="server" MaxLength="40" onkeyup="charcountupdate(this.value)"></asp:TextBox>
                                                        <div class="charcount"></div>
                                </asp:TableCell>
                                                                <asp:TableCell>
                                    או
                                </asp:TableCell>
                                <asp:TableCell>
                                    <asp:FileUpload ID="FileUpload7" Class="FileUploadgrid" runat="server" />
                                    <asp:ImageButton ID="ImageforUpload7" Class="ImageButtongrid" runat="server" ImageUrl="~/Images/ImagePlaceholder.png" OnClientClick="openFileUploader7(); return false;" />
                                    <input id="IButton7" class="del" type="button" value="X" onclick="return false;"/>
                                    <asp:HiddenField ID="hdnfldVariable7" runat="server" />
                                 </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableCell>
                                    <asp:CheckBox ID="ACheckBox7" runat="server" />
                                </asp:TableCell>
                                <asp:TableCell>
                                    <asp:TextBox ID="ATextBox7" runat="server" MaxLength="40" onkeyup="charcountupdate(this.value)"></asp:TextBox>
                                                        <div class="charcount"></div>
                                </asp:TableCell>
                                                                <asp:TableCell>
                                    או
                                </asp:TableCell>
                                <asp:TableCell>
                                    <asp:FileUpload ID="FileUpload8" Class="FileUploadgrid" runat="server" />
                                    <asp:ImageButton ID="ImageforUpload8" Class="ImageButtongrid" runat="server" ImageUrl="~/Images/ImagePlaceholder.png" OnClientClick="openFileUploader8(); return false;" />
                                    <input id="IButton8" class="del" type="button" value="X" onclick="return false;"/>                                    
                                    <asp:HiddenField ID="hdnfldVariable8" runat="server" />
                                </asp:TableCell>
                            </asp:TableRow>
                        </asp:Table>
                        <asp:Button ID="addtoq" runat="server" ToolTip="השאלה לא עומדת בתנאי שמירה" disabled Text="הוספה למאגר השאלות" OnClick="Button3_Click" />
                    </section>
                    <asp:Button ID="Button4" runat="server" Text="סיים עריכה" OnClick="Button4_Click" />
                </section>
            </div>
                                  <div id="id02" onclick="document.getElementById('id02').style.display='none'">
                                <div class="modal">
                                        <div class="container">
                                            <h1>ביצירת שאלה חדשה תמחק השאלה שנמצאת בעריכה.</h1>
                                            <div class="clearfix">
                                                        <asp:Button ID="deleteIMG" OnClick="Button5_Click" runat="server" class="deletebtn" Text="יצירת שאלה חדשה"></asp:Button>
                                                        <button type="button" onclick="hidemodel(this)" class="cancelbtn">חזרה לשאלה בעריכה</button>
                                            </div>
                                        </div>
                                </div>
                            </div>

                             <div id="id03" onclick="document.getElementById('id03').style.display='none'">
                                <div class="modal">
                                        <div class="container">
                                            <h1>בסיום עריכה תמחק השאלה שנמצאת בעריכה.</h1>
                                            <div class="clearfix">
                                                        <asp:Button ID="Button3" OnClick="Button6_Click" runat="server" class="deletebtn" Text="סיום עריכה"></asp:Button>
                                                        <button type="button" onclick="hidemodel(this)" class="cancelbtn">חזרה לשאלה בעריכה</button>
                                            </div>
                                        </div>
                                </div>
                            </div>
                           <div id="id04" onclick="document.getElementById('id04').style.display='none'">
                                <div class="modal">
                                        <div class="container">
                                            <h1>המשחק עומד בתנאי הפרסום, האם ברצונך לפרסם?</h1>
                                            <div class="clearfix">
                                                        <asp:Button ID="Button5" OnClick="Button7_Click" runat="server" class="deletebtn" Text="פרסם"></asp:Button>
                                                        <button type="button" onclick="hidemodel(this)" class="cancelbtn">אל תפרסם</button>
                                            </div>
                                        </div>
                                </div>
                            </div>
                            <div id="id05" onclick="document.getElementById('id05').style.display='none'">
                                <lottie-player src="style/lf20_30iie6.json"  background="transparent" speed="1"  style="width: 100%; height: 100%;"  autoplay></lottie-player>
                                <div class="modal">
                                        <div class="container">
                                            <h1>עשית את זה! המשחק '  <asp:Label ID="Label3" runat="server"></asp:Label> ' פורסם.</h1>
                                            <h1>קוד המשחק הינו: <asp:Label ID="Label4" runat="server"></asp:Label> </h1>
                                            <div class="clearfix">
                                                        <button type="button" onclick="movetomain()" class="cancelbtn">אישור</button>
                                            </div>
                                        </div>
                                </div>
                            </div>
                                        <div id="id06" onclick="document.getElementById('id05').style.display='none'">
                                <div class="modal">
                                        <div class="container">
                                            <h1>המשחק אינו עומד כעת בתנאי הפרסום האם ברצונך להמשיך בעריכה?</h1>
                                            <div class="clearfix">
                                                        <asp:Button ID="Button6" OnClick="Button8_Click" runat="server" class="deletebtn" Text="סיים ללא פרסום"></asp:Button>
                                                        <button type="button" onclick="hidemodel(this)" class="cancelbtn">המשך בעריכה</button>
                                            </div>
                                        </div>
                                </div>
                            </div>

    </form>
</body>
</html>
