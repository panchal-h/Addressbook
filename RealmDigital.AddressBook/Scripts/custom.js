$(document).ready(function() {
    //Prevent Page Reload on all # links
    $("a[href='#']").click(function(e) {
        e.preventDefault();
    });

    // On scroll header small
    $(window).scroll(function(e) {
        if ($(window).scrollTop() > 200)
            $(".wrapper").addClass('small-header');
        else
            $(".wrapper").removeClass('small-header');
    });

    //placeholder
    $("[placeholder]").each(function() {
        $(this).attr("data-placeholder", this.placeholder);

        $(this).bind("focus", function() {
            this.placeholder = '';
        });
        $(this).bind("blur", function() {
            this.placeholder = $(this).attr("data-placeholder");
        });
    });

    //date time picker
    $("body").on("click", ".datepicker>span", function() {
        $(this).siblings('.form-control').trigger("focus");
    });
    /*datepicker*/
    $('.datepicker').append('<div class="backdrop"><a href="#"></a></div>');
    $(".datepicker").on("dp.show", function() {
        var backDrop = $(this);
        backDrop.addClass('active-datepicker');
        setTimeout(function() {
            backDrop.addClass('visible');
        }, 5);
        if ($(this).hasClass('timepicker') && !$(this).children('.bootstrap-datetimepicker-widget').children('.timepicker-close-btn')[0]) {
            $(this).children('input').attr("data-val", $(this).children('input').val());
            $(this).children('.bootstrap-datetimepicker-widget').append("<div class='btn-list text-center'><button type='button' class='btn btn-primary timepicker-close-btn'>Ok</button><button type='button' class='btn btn-outlined-primary timepicker-cancel-btn'>Cancel</button></div>");
        }
    });
    $(".datepicker").on("dp.hide", function() {
        var backDrop = $(this);
        backDrop.removeClass('visible');
        setTimeout(function() {
            backDrop.removeClass('active-datepicker');
        }, 305);
    });
    $(".datepicker .backdrop a").on("click", function() {
        if ($(this).closest('.datepicker').hasClass('input-group')) $(this).parent().parent().data("DateTimePicker").hide();
        else if ($(this).closest('.datepicker').hasClass('form-group')) $(this).parent().siblings('input').data("DateTimePicker").hide();
    });
    $(".datepicker").each(function() {
        if ($(this).hasClass('fixed')) {
            $(this).find("input").datetimepicker({
                format: "DD-MM-YYYY",
                ignoreReadonly: true,
                focusOnShow: false,
                widgetParent: $("body")
            });
        } else if ($(this).hasClass('no-edit')) {
            $(this).find("input").datetimepicker({
                format: "DD-MM-YYYY",
                ignoreReadonly: true,
                focusOnShow: false
            });
        }
    });
    $("body").on("dp.show", ".datepicker.fixed input", function() {
        var picker = $(".bootstrap-datetimepicker-widget");
        var me = $(this);
        var top = me.offset().top;
        var left = me.offset().left;
        var height = me.outerHeight();
        if (picker.hasClass('bottom')) {
            picker.css({
                "top": top + height,
                "left": left
            });
        } else if (picker.hasClass('top')) {
            picker.css({
                "top": top - picker.outerHeight() - 8,
                "left": left,
                "bottom": "auto"
            });
        }
    });

    /*nav toggle*/
    $("body").on("click", ".nav-toggle,.nav-backdrop>a", function() {
        $("html,body").toggleClass('nav-open');
    });
});