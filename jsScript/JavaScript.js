//////////////טולטיפ////////

//$(document).ready(function () {
//    $('[data-toggle="tooltip"]').tooltip();
//});

//////////דיב שעוטף את הגריווי//////////
$(document).ready(function () {
    if (window.location.pathname == '/Default.aspx') {
        $("#GridView1").parent().addClass("divgrid")
        //var grid = document.getElementsByClassName("divgrid").item(0);
        //var divmove = document.getElementById("serchtomove");
        //var Gridview = document.getElementById("GridView1");
        //grid.appendChild(divmove);
        //grid.insertBefore(divmove, Gridview)
    }
});



/////////////////חיפוש גרידווי///////////////
$(document).ready(function () {
    $("#addNameTB").keyup(function () {
        var result = 0;
        var tbody = document.getElementById("GridView1").getElementsByTagName("tbody").item(0).getElementsByTagName("tr");
        for (var i = 1; i < tbody.length; i++) {
            if ((tbody.item(i).getElementsByClassName("qbutton").item(0).value.includes(document.getElementById("addNameTB").value)) ||
                (tbody.item(i).getElementsByTagName("td").item(1).innerHTML.includes(document.getElementById("addNameTB").value))) {
                tbody.item(i).style.display = "table-row"
                result++;
                document.getElementById("noresult").style.display = "none";

            } else {
                tbody.item(i).style.display = "none"

            }
        }
        if (result == 0) {
            document.getElementById("noresult").style.display = "block";
            var grid = document.getElementsByClassName("divgrid").item(0);
            var divmove = document.getElementById("noresult");
            grid.appendChild(divmove);
        }
    });

    if (document.getElementById("GridView1") == null) {
        document.getElementById("serchtomove").style.display = "none";
    } else {
        document.getElementById("nogame").style.display = "none";
    }

});



//$(document).ready(function () {
//    $(window).unload(function () {
//        $("#Button2").click();
//    });
//});

//////////זום למסך/////////
$(window).resize(function () {
    resizewin();
});

$(document).ready(function () {
    resizewin();
});

function resizewin() {
    var win = $(window).width();
    var winzoom = win / 1200;
    $("body").css("zoom", winzoom);
}




//////אקורדיון////////////////////


$(document).ready(function () {
    var acc = document.getElementsByClassName("accordion");
    var i;
    for (i = 0; i < acc.length; i++) {
        acc[i].addEventListener("click", function () {
            console.log("test");
            this.classList.toggle("active");
            var panel = this.nextElementSibling;
            if (panel.style.display === "block") {
                panel.style.display = "none";
            } else {
                panel.style.display = "block";
            }
        });
    };

    $(".dordor").click(function () {
        $("#theimg").attr('src', this.src);
        $("#blackwithimg").css("display", "block")

    });

    $("#blackwithimg").click(function () {
        $("#blackwithimg").css("display", "none")
    });

});

var acc = document.getElementsByClassName("accordion");
function expand() {
    for (i = 0; i < acc.length; i++) {
        acc[i].classList.add("active");
        var panel = acc[i].nextElementSibling;
        panel.style.display = "block";
    }
}

function Shrink() {
    for (i = 0; i < acc.length; i++) {
        acc[i].classList.remove("active");
        var panel = acc[i].nextElementSibling;
        panel.style.display = "none";
    }
}



///////////צקבוקס פרסום - לא פעיל//////////


$(document).ready(function () {
    if (window.location.pathname == '/Default.aspx') {
        var gamenum = document.getElementById("dashgamenumber").innerText;
        for (i = 0; i < gamenum; i++) {
            if (document.getElementById("GridView1_GameQnumLbl_" + i).innerHTML < 6) {
                document.getElementById("GridView1_ISpassCheckBox1_" + i).disabled = true;
                document.getElementById("GridView1_ISpassCheckBox1_" + i).parentElement.parentElement.setAttribute("title", "");
                document.getElementById("GridView1_ISpassCheckBox1_" + i).parentElement.parentElement.setAttribute("data-original-title", "לא ניתן לפרסם משחק יש להוסיף שאלות למשחק");
                document.getElementById("GridView1_ISpassCheckBox1_" + i).parentElement.parentElement.setAttribute("data-toggle", "tooltip");
            }
            else {
                document.getElementById("GridView1_ISpassCheckBox1_" + i).disabled = false;
                document.getElementById("GridView1_ISpassCheckBox1_" + i).parentElement.parentElement.removeAttribute("title");
                document.getElementById("GridView1_ISpassCheckBox1_" + i).parentElement.parentElement.removeAttribute("data-original-title");
                document.getElementById("GridView1_ISpassCheckBox1_" + i).parentElement.parentElement.removeAttribute("data-toggle");
            }

        }
    }
}); 



////////////ספירת תווים////////////
function charcountupdate(str) {
    for (var i = 0; i < 9; i++) {
        var editqseccount = document.getElementsByClassName("charcount")[i].innerHTML = "";
    }
    var lng = str.length;
    var thisid = event.target.id
    var thismax = document.getElementById(thisid).getAttribute("MaxLength");
    if (thismax != null) {
        document.getElementById(thisid).nextElementSibling.innerHTML = lng + "/" + thismax;
        if (thisid == "GameSubjectTB" && lng >= 2) {
            document.getElementsByClassName("charcount")[0].style.color = "black";
            document.getElementsByClassName("charcount")[0].style.fontWeight = "normal";
        }
        if (thisid == "GameSubjectTB" && lng < 2) {
            document.getElementsByClassName("charcount")[0].style.color = "red";
            document.getElementsByClassName("charcount")[0].style.fontWeight = "bold";
        }
    } else {
        thismax = document.getElementById(thisid).getAttribute("Maxchar");
        document.getElementById(thisid).nextElementSibling.innerHTML = lng + "/" + thismax;
        document.getElementById(thisid).value = str.slice(0, thismax)
        if (lng >= 3) {
            document.getElementsByClassName("charcount")[1].style.color = "black";
            document.getElementsByClassName("charcount")[1].style.fontWeight = "normal";
        }
        if (lng < 3) {
            document.getElementsByClassName("charcount")[1].style.color = "red";
            document.getElementsByClassName("charcount")[1].style.fontWeight = "bold";
        }
    }
}

function charcountupdatenew(str) {
    var lng = str.length;
    var thisid = event.target.id
    var thismax = document.getElementById(thisid).getAttribute("MaxLength");
    if (thismax != null) {
        document.getElementById(thisid).nextElementSibling.innerHTML = lng + "/" + thismax;
        if (thisid == "GameSubjectTB" && lng >= 2) {
            document.getElementsByClassName("charcount")[0].style.color = "black";
            document.getElementsByClassName("charcount")[0].style.fontWeight = "normal";
            document.getElementById("newgameIMG").disabled = false;
            document.getElementById("newgameIMG").className = "deletebtn";

            
        }
        if (thisid == "GameSubjectTB" && lng < 2) {
            document.getElementsByClassName("charcount")[0].style.color = "red";
            document.getElementsByClassName("charcount")[0].style.fontWeight = "bold";
            document.getElementById("newgameIMG").disabled = true;
        }
    }
}


$(document).ready(function () {
    if ($("#GameSubjectTB").val().length < 2) {
        $(".charcount:first").css("color", "red")
        $(".charcount:first").css("font-weight", "bold")
        $(".charcount:first").text($("#GameSubjectTB").val().length + "/" + "40");
    }
});







//////////////דף כניסה עם סיסמא/////////
function adminfunc(str) {

    if (document.getElementById("usernameTB").value != "" && document.getElementById("passwordTB").value != "") {
        document.getElementById("Button1").disabled = false;
        document.getElementById("Button1").setAttribute('title', "");
        document.getElementById("Button1").style.cursor = "pointer"
    } else {
        document.getElementById("Button1").disabled = true;
        document.getElementById("Button1").setAttribute('title', "הזן את פרטי ההתחברות");
        document.getElementById("Button1").style.cursor = "help"
    }
};



////////////בדיקה כפולה של מחיקה//////////////

// Get the modal
var modal = document.getElementById('id01');

// When the user clicks anywhere outside of the modal, close it
window.onclick = function (event) {
    if (event.target == modal) {
        modal.style.display = "none";
    }
}

function showmodel(event) {
    event.parentElement.children[1].style.display = 'block';
    if (window.location.pathname == '/Edit.aspx') {
        event.parentElement.getElementsByClassName("gamename").item(0).innerHTML = "''" + event.parentElement.parentElement.getElementsByTagName("input").item(0).value + "''";
    }
}
function showmodel2(event) {
    document.getElementById("id02").style.display = 'block';
}

function hidemodel(event) {
    event.parentElement.parentElement.parentElement.parentElement.style.display = "none";
}



//פעולה שמתרחשת בלחיצה על התמונה הראשונה ופותחת את חלון בחירת התמונה הראשונה
function openFileUploader1() {
    $('#FileUpload1').click();
}
function openFileUploader2() {
    $('#FileUpload2').click();
}
function openFileUploader3() {
    $('#FileUpload3').click();
}
function openFileUploader4() {
    $('#FileUpload4').click();
}
function openFileUploader5() {
    $('#FileUpload5').click();
}
function openFileUploader6() {
    $('#FileUpload6').click();
}
function openFileUploader7() {
    $('#FileUpload7').click();
}
function openFileUploader8() {
    $('#FileUpload8').click();
}


$(document).ready(function () {

    //לאחר שלחצנו על התמונה שרצינו לבחור - תמונה מספר אחד
    $("#FileUpload1").change(function () {
        if (this.files && this.files[0]) {
            var reader = new FileReader();

            reader.onload = function (e) {
                $('#ImageforUpload1').attr('src', e.target.result);
            }
            reader.readAsDataURL(this.files[0]);
        }
        document.getElementById('IButton1').style.display = "inline-block"
    });




    //לאחר שלחצנו על התמונה שרצינו לבחור - תמונה מספר שתיים
    $("#FileUpload2").change(function () {
        if (this.files && this.files[0]) {
            var reader = new FileReader();

            reader.onload = function (e) {
                $('#ImageforUpload2').attr('src', e.target.result);
            }

            reader.readAsDataURL(this.files[0]);
        }
        document.getElementById("ATextBox1").disabled = true;
        document.getElementById('IButton2').style.display = "inline-block"
    });

    $("#FileUpload3").change(function () {
        if (this.files && this.files[0]) {
            var reader = new FileReader();

            reader.onload = function (e) {
                $('#ImageforUpload3').attr('src', e.target.result);
            }

            reader.readAsDataURL(this.files[0]);
        }
        document.getElementById("ATextBox2").disabled = true;
        document.getElementById('IButton3').style.display = "inline-block"


    });


    $("#FileUpload4").change(function () {
        if (this.files && this.files[0]) {
            var reader = new FileReader();

            reader.onload = function (e) {
                $('#ImageforUpload4').attr('src', e.target.result);
            }

            reader.readAsDataURL(this.files[0]);
        }
        document.getElementById("ATextBox3").disabled = true;
        document.getElementById('IButton4').style.display = "inline-block"
    });


    $("#FileUpload5").change(function () {
        if (this.files && this.files[0]) {
            var reader = new FileReader();

            reader.onload = function (e) {
                $('#ImageforUpload5').attr('src', e.target.result);
            }

            reader.readAsDataURL(this.files[0]);
        }
        document.getElementById("ATextBox4").disabled = true;
        document.getElementById('IButton5').style.display = "inline-block"


    });

    $("#FileUpload6").change(function () {
        if (this.files && this.files[0]) {
            var reader = new FileReader();

            reader.onload = function (e) {
                $('#ImageforUpload6').attr('src', e.target.result);
            }

            reader.readAsDataURL(this.files[0]);
        }
        document.getElementById("ATextBox5").disabled = true;
        document.getElementById('IButton6').style.display = "inline-block"

    });


    $("#FileUpload7").change(function () {
        if (this.files && this.files[0]) {
            var reader = new FileReader();

            reader.onload = function (e) {
                $('#ImageforUpload7').attr('src', e.target.result);
            }

            reader.readAsDataURL(this.files[0]);
        }
        document.getElementById("ATextBox6").disabled = true;
        document.getElementById('IButton7').style.display = "inline-block"

    });

    $("#FileUpload8").change(function () {
        if (this.files && this.files[0]) {
            var reader = new FileReader();

            reader.onload = function (e) {
                $('#ImageforUpload8').attr('src', e.target.result);
            }

            reader.readAsDataURL(this.files[0]);
        }
        document.getElementById("ATextBox7").disabled = true;
        document.getElementById('IButton8').style.display = "inline-block"

    });

});



/////////////תנאי שמירת תאורה//////////
$(document).ready(function () {
    setTimeout(function () { refsave(); }, 100);
         

    
    for (var i = 1; i < 8; i++) {
        $("#FileUpload" + i).change(function () {
            refsave();
        });
        $("#ACheckBox" + i).change(function () {
            refsave();
        });
        $("#ATextBox" + i).focusout(function () {
            refsave();
        });
        $("#mainqtb").focusout(function () {
            refsave();
        });
    }
    function refsave() {
        var numofopt = 0;
        var numofocheck = 0;
        for (var i = 1; i < 8; i++) {
            if (document.getElementById("ImageforUpload" + i).src.includes("ImagePlaceholder.png") != true) {
                numofopt++
            };
            if (document.getElementById("ATextBox" + i).value !="") {
                numofopt++
            };
            if (document.getElementById("ACheckBox" + i).checked == true) {
                numofocheck++
            };
        }
        if (numofopt > 2 && (document.getElementById("mainqtb").value[2] != undefined) && (numofocheck > 0) && (numofocheck < 6)) {
            document.getElementById("addtoq").disabled = false;
            document.getElementById("addtoq").removeAttribute("title");
            document.getElementById("threeans").firstChild.setAttribute("d","M173.898 439.404l-166.4-166.4c-9.997-9.997-9.997-26.206 0-36.204l36.203-36.204c9.997-9.998 26.207-9.998 36.204 0L192 312.69 432.095 72.596c9.997-9.997 26.207-9.997 36.204 0l36.203 36.204c9.997 9.997 9.997 26.206 0 36.204l-294.4 294.401c-9.998 9.997-26.207 9.997-36.204-.001z")
            document.getElementById("onecorrect").firstChild.setAttribute("d","M173.898 439.404l-166.4-166.4c-9.997-9.997-9.997-26.206 0-36.204l36.203-36.204c9.997-9.998 26.207-9.998 36.204 0L192 312.69 432.095 72.596c9.997-9.997 26.207-9.997 36.204 0l36.203 36.204c9.997 9.997 9.997 26.206 0 36.204l-294.4 294.401c-9.998 9.997-26.207 9.997-36.204-.001z")


        } else {
            document.getElementById("addtoq").disabled = true;
            document.getElementById("addtoq").setAttribute("title", "השאלה לא עומדת בתנאי שמירה");
            if (numofopt > 2 && (document.getElementById("mainqtb").value[2] != undefined)) {
                document.getElementById("onecorrect").firstChild.setAttribute("d", "M242.72 256l100.07-100.07c12.28-12.28 12.28-32.19 0-44.48l-22.24-22.24c-12.28-12.28-32.19-12.28-44.48 0L176 189.28 75.93 89.21c-12.28-12.28-32.19-12.28-44.48 0L9.21 111.45c-12.28 12.28-12.28 32.19 0 44.48L109.28 256 9.21 356.07c-12.28 12.28-12.28 32.19 0 44.48l22.24 22.24c12.28 12.28 32.2 12.28 44.48 0L176 322.72l100.07 100.07c12.28 12.28 32.2 12.28 44.48 0l22.24-22.24c12.28-12.28 12.28-32.19 0-44.48L242.72 256z")

            }
            if ((numofocheck > 0) && (numofocheck < 6)) {
                document.getElementById("threeans").firstChild.setAttribute("d", "M242.72 256l100.07-100.07c12.28-12.28 12.28-32.19 0-44.48l-22.24-22.24c-12.28-12.28-32.19-12.28-44.48 0L176 189.28 75.93 89.21c-12.28-12.28-32.19-12.28-44.48 0L9.21 111.45c-12.28 12.28-12.28 32.19 0 44.48L109.28 256 9.21 356.07c-12.28 12.28-12.28 32.19 0 44.48l22.24 22.24c12.28 12.28 32.2 12.28 44.48 0L176 322.72l100.07 100.07c12.28 12.28 32.2 12.28 44.48 0l22.24-22.24c12.28-12.28 12.28-32.19 0-44.48L242.72 256z")
            }

        }
    }

});








$(document).ready(function () {

    $(".del").click(function () {
        var curr = this.id;
        var curNum = parseInt(curr.substring(7, 8));
        var curNum1 = curNum - 1;
        document.getElementById('ImageforUpload' + curNum).src = '/Images/ImagePlaceholder.png';
        document.getElementById('ATextBox' + curNum1).disabled = false
        document.getElementById('IButton' + curNum).style.display = "none"
        var hdnfldVariable = document.getElementById('hdnfldVariable' + curNum);
        hdnfldVariable.value = false;

        $('#FileUpload' + curNum).val('');
    });

    $(".notdel").click(function () {
        document.getElementById('ImageforUpload1').src = '/Images/ImagePlaceholder.png';
        document.getElementById('IButton1').style.display = "none"
        var hdnfldVariable = document.getElementById('hdnfldVariable1');
        hdnfldVariable.value = false;

        $('#FileUpload1').val('');
    });


    $(".del").show(function () {
        var curr = this.id;
        var curNum = parseInt(curr.substring(7, 8));
        var sssrc = document.getElementById('ImageforUpload' + curNum).src
        if (sssrc.includes("ImagePlaceholder.png")) {
        document.getElementById('IButton' + curNum).style.display = "none"
        }
    });

    $(".notdel").show(function () {
        var sssrc = document.getElementById('ImageforUpload1').src
        if (sssrc.includes("ImagePlaceholder.png")) {
        document.getElementById('IButton1').style.display = "none"
        }
    });


    $("#ATextBox1").focusout(function () {
        if ($("#ATextBox1").val().length !=0) {
        $("#ImageforUpload2").prop("disabled", true);
        $("#ImageforUpload2").css("opacity", "0.5")
        $("#ImageforUpload2").CssClass = "ImageButtongrid";
        } else {
            $("#ImageforUpload2").prop("disabled", false);
            $("#ImageforUpload2").css("opacity", "1");
        }
    });
    $("#ATextBox2").focusout(function () {
        if ($("#ATextBox2").val().length != 0) {
            $("#ImageforUpload3").prop("disabled", true);
            $("#ImageforUpload3").css("opacity", "0.5");
            $("#ImageforUpload3").CssClass = "ImageButtongrid";
        }
        else {
            $("#ImageforUpload3").prop("disabled", false);
            $("#ImageforUpload3").css("opacity", "1");
        }
    }); $("#ATextBox3").focusout(function () {
        if ($("#ATextBox3").val().length != 0) {
            $("#ImageforUpload4").prop("disabled", true);
            $("#ImageforUpload4").css("opacity", "0.5")
            $("#ImageforUpload4").CssClass = "ImageButtongrid";
        } else {
            $("#ImageforUpload4").prop("disabled", false);
            $("#ImageforUpload4").css("opacity", "1");
        }
    }); $("#ATextBox4").focusout(function () {
        if ($("#ATextBox4").val().length != 0) {
            $("#ImageforUpload5").prop("disabled", true);
            $("#ImageforUpload5").css("opacity", "0.5")
            $("#ImageforUpload5").CssClass = "ImageButtongrid";
        } else {
            $("#ImageforUpload5").prop("disabled", false);
            $("#ImageforUpload5").css("opacity", "1");
        }
    }); $("#ATextBox5").focusout(function () {
        if ($("#ATextBox5").val().length != 0) {
            $("#ImageforUpload6").prop("disabled", true);
            $("#ImageforUpload6").css("opacity", "0.5")
            $("#ImageforUpload6").CssClass = "ImageButtongrid";
        } else {
            $("#ImageforUpload6").prop("disabled", false);
            $("#ImageforUpload6").css("opacity", "1");
        }
    }); $("#ATextBox6").focusout(function () {
        if ($("#ATextBox6").val().length !=0) {
            $("#ImageforUpload7").prop("disabled", true);
            $("#ImageforUpload7").css("opacity", "0.5")
            $("#ImageforUpload7").CssClass = "ImageButtongrid";
        } else {
            $("#ImageforUpload7").prop("disabled", false);
            $("#ImageforUpload7").css("opacity", "1");
        }
    }); $("#ATextBox7").focusout(function () {
        if ($("#ATextBox7").val().length != 0) {
            $("#ImageforUpload8").prop("disabled", true);
            $("#ImageforUpload8").css("opacity", "0.5")
            $("#ImageforUpload8").CssClass = "ImageButtongrid";
        } else {
            $("#ImageforUpload8").prop("disabled", false);
            $("#ImageforUpload8").css("opacity", "1");
        }
    });
});

function checkifhaveq() {
    var numofopt = 0;
    for (var i = 1; i < 8; i++) {
        if (document.getElementById("ImageforUpload" + i).src.includes("ImagePlaceholder.png") != true) {
            numofopt++
        };
        if (document.getElementById("ATextBox" + i).value != "") {
            numofopt++
        };
    }

    if (numofopt > 0 || (document.getElementById("mainqtb").value[1] != undefined)) {
        document.getElementById("id02").style.display = "block";
    } 

}

function movetomain() {
    console.log("dsg")
    window.location = '/Default.aspx';
}

function thegamewaspublish() {
    console.log("dsagds")
    document.getElementById("id03").style.display = "none";
    document.getElementById("id06").style.display = "block";
}

///////////////סיום עריכה ופרסום///////
function endeditandpublish() {
    document.getElementById("id03").style.display = "none";
    document.getElementById("id04").style.display = "block";
}

function publishgame() {
    document.getElementById("id04").style.display = "none";
    document.getElementById("id05").style.display = "block";
}

function endedit() {
    var numofopt = 0;
    for (var i = 1; i < 8; i++) {
        if (document.getElementById("ImageforUpload" + i).src.includes("ImagePlaceholder.png") != true) {
            numofopt++
        };
        if (document.getElementById("ATextBox" + i).value != "") {
            numofopt++
        };
    }

    if (numofopt > 0 || (document.getElementById("mainqtb").value[1] != undefined)) {
        document.getElementById("id03").style.display = "block";
    } else {
        document.getElementById("id03").style.display = "block";
        document.getElementById("Button3").click();
    }
}


