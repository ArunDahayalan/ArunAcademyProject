
jQuery(document).ready(function () {

    /*
        Background slideshow
    */
    $('.testimonials-container').backstretch("assets/img/backgrounds/1.jpg");

    $('a[data-toggle="tab"]').on('shown.bs.tab', function () {
        $('.testimonials-container').backstretch("resize");
    });

});

$(".Testimonials").on('click',function(){
    $("#testimonial-section").css("display", "block");
    $("#album-section").css("display", "none");
    $("#statistics-section").css("display", "none");
});

$(".Album").on('click', function () {
    $("#album-section").css("display", "block");
    $("#testimonial-section").css("display", "none");
    $("#statistics-section").css("display", "none");
});

$(".Achievements").on('click', function () {
    $("#album-section").css("display", "none");
    $("#testimonial-section").css("display", "none");
    $("#statistics-section").css("display", "block");
});

$(".testimonial-list .nav-tabs li a").click(function()
{
    $(".testimonial-list .nav-tabs li").removeClass("active");
    $(this).parent().addClass("active");
})