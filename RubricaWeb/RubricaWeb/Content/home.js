var hh = $('#header').outerHeight();
var fh = $('footer').outerHeight();

// using https://rawgit.com/bassjobsen/affix/master/assets/js/affix.js
$('.navbar').affix({
    offset: {
        top: hh,
        bottom: hh - 200
    }
});

$('#sidemenu').affix({
    offset: {
        top: hh,
        bottom: fh + 50
    }
});