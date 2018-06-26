$("#logincheck").click(function()
{
    if (emailvalidate() && passwordvalidate()) {
        var data =
            {
                UserName: $("#Email").val(),
                Password: $("#Password").val()
            }
        $.ajax({
            type: "POST",
            url: "/studentloginsubmit",
            data: data,
            success: function (result) {
                if (result != null && result == "dberror")
                {
                    $(".validationerrormessage").css("display", "none");
                    $(".validationmessage").css("display", "none");
                    $(".dberrormessage").css("display", "block");
                }

                if (result != null && result == "false")
                {
                    $(".validationerrormessage").css("display", "none");
                    $(".validationmessage").css("display", "block");
                    $(".dberrormessage").css("display", "none");
                }

                if (result != null && result == "true") {
                    setCookie('Atcstudentuser', $("#Email").val(), 1);
                    window.location = "/testschedule";
                }
            },

        });
    }
    else {
        $(".validationerrormessage").css("display","block");
        $(".validationmessage").css("display","none");
        $(".dberrormessage").css("display","none");
    }
})

function passwordvalidate()
{
    if($("#Password").val() != null && $("#Password").val() == "")
    {
        return false;
    }

    return true;
}
function emailvalidate()
{
    var reg = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/;
    if (reg.test($("#Email").val()) == false) {
        return false;
    }

    return true;
}

$(document).ready(function () {
    eraseCookie("Atcstudentuser");
    eraseCookie("UserCookie");
    eraseCookie("AdminCookie");
});