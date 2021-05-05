/*!
 * Global Technologic nopcommerce Theme (http://globaltechnologic.com/nopcommerce-themes.php)
 * Copyright 2019 Global Technologic.
 */
//jQuery to collapse the navbar on scroll
$(document).ready(function (){    
    var sticky = $('.masthead').offset().top + ($('.masthead').height());		
    $(window).scroll(function(){
        if($(this).scrollTop()>sticky){
            $('.masthead').addClass('sticky');           
        }
        else{
            $('.masthead').removeClass('sticky'); 
        }
    })
    var Menusticky = $('header').offset().top;
    $(window).scroll(function () {
        if ($(this).scrollTop() > Menusticky) {
            $('header').addClass('fixed');
        }
        else {
            $('header').removeClass('fixed');
        }
    })
    $('.scrollup').fadeOut();
    $(window).scroll(function () {        
        if ($(this).scrollTop() > 50) {
            $('.scrollup').fadeIn();
        } else {
            $('.scrollup').fadeOut();
        }
    });
    $('.scrollup').click(function () {
        $("html, body").animate({
            scrollTop: 0
        }, 600);
        return false;
    });    
    $('.currency-selector, .language-selector').click(function () {
        event.stopPropagation();
    });
    $(".searchbtn").click(function () {
        $(".search-box").toggleClass("search-display");
        $(".site-header").toggleClass("search-overlay-open");
        if ($('.searchbtn i').hasClass('fa-search')) {
            $('.searchbtn i').addClass('fa-times').removeClass('fa-search');
        }
        else {
            $('.searchbtn i').addClass('fa-search').removeClass('fa-times');
        }
    });
    $('.header-links li a.dropdown-nav').click(function () {      
        $('.header-links ul.dropdown-view').toggle();
        event.stopPropagation(); 
    });
    $('body').click(function () {
        $('.header-links ul.dropdown-view').hide();        
    });     
    $('footer h3, .side-nav h3').click(function () {
      $('.showHide-ft').slideUp();
      $(this).next('.showHide-ft').slideToggle();
    });
    $('.side-nav h3').click(function () {
       // $('.showHide-ft').slideUp();
        $(this).parents('.side-nav').find('.showHide-ft').slideToggle();
    });    
})
$(window).resize(function () {
    $(".navbar.navbar-inverse").width("100%");
});

$(document).ready(function () {
    $('#exit ').click(function (e) {
        $('.responsive').hide();
        $('.master-wrapper-page').css('margin-top', '0');
        $('.header-links').css('margin-top', '20px');
    });
});
/* Custom Style for Collapse Sidebar Box */
$(document).ready(function () {
    $('.block .title').click(function () {
        var e = window, a = 'inner';
        if (!('innerWidth' in window)) {
            a = 'client';
            e = document.documentElement || document.body;
        }
        var result = { width: e[a + 'Width'], height: e[a + 'Height'] };
        if (result.width < 992) {
            $(this).siblings('.listbox').slideToggle('slow');
            $(this).toggleClass("arrow-up-down");
        }
    });
});

