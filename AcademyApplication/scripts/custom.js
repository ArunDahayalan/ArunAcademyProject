$(document).ready(function() {
    if (getCookie("AdminCookie").split('=')[1] == "True")
    {
        $(".adminregister").css("display", "block");
    }
    else
    {
        $(".adminregister").css("display", "none");
    }
  var lastScrollTop = 0;
$(window).scroll(function(event){
   var st = $(this).scrollTop();
   var scrollH =  $(window).scrollTop();
   if (scrollH > 50){
       // downscroll code
       //alert('down');
       $('.main-menu').addClass('sticky');
    $('.full-menu').addClass('sticky');
     
       
   } else {
      // upscroll code
    
    $('.main-menu').removeClass('sticky');
       $('.full-menu').removeClass('sticky');
   }
   lastScrollTop = st;
});


});



$('.owl-carousel').owlCarousel({
    loop:true,
    margin:30,
    nav:true,
    responsive:{
        0:{
            items:1
        },
        600:{
            items:3
        },
        1000:{
            items:4
        }
    }
});

function setCookie(name, value, days) {
    var expires = "";
    if (days) {
        var date = new Date();
        date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
        expires = "; expires=" + date.toUTCString();
    }
    document.cookie = name + "=" + (value || "") + expires + "; path=/";
}
function getCookie(name) {
    var nameEQ = name + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') c = c.substring(1, c.length);
        if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length, c.length);
    }
    return null;
}
function eraseCookie(name) {
    document.cookie = name + '=; Max-Age=-99999999;';
}

if (getCookie("Atcstudentuser") != null)
{
    $(".logout-section").css("display", "block")
    $(".login").css("display", "none")
    $(".username").text("Hi, " + getCookie("Atcstudentuser").split('@')[0]);
}
else
{
    $(".logout-section").css("display", "none")
    $(".login").css("display", "block")
}

$(".userlogout a").click(function () {
    eraseCookie("Atcstudentuser");
    eraseCookie("UserCookie");
    eraseCookie("AdminCookie");
    window.location = "/";
});

$body = $("body");

$(document).on({
    ajaxStart: function () { $body.addClass("loading"); },
    ajaxStop: function () { $body.removeClass("loading"); }
});