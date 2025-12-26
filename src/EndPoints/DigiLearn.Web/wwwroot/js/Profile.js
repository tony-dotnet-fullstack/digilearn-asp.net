$(".showMenu a").click(function () {
    $(".profile-menu ul").slideToggle();
    var currentClass = $(".showMenu i").hasClass("fa-chevron-down");
    if (currentClass) {
        $(".showMenu i").removeClass("fa-chevron-down");
        $(".showMenu i").addClass("fa-chevron-up");
        $(this).html("close منو");
    } else {
        $(".showMenu i").addClass("fa-chevron-down");
        $(".showMenu i").removeClass("fa-chevron-up");
        $(this).html("show منو");
    }
});
$(document).ready(function () {
    $('.ticket-Body').animate({
        scrollTop: $('.ticket-Body .chat:last-child').position().top
    }, 'slow');
});

$("#upImgAvatar").change(function() {
    $("#change_avatar").submit();
})