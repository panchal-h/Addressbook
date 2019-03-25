$(function () {
    $("a[data-modal=mobile]").on("click", function () {
        $("#contactMobileContent").load(this.href, function () {
            $("#contactMobileModal").modal({ keyboard: true }, "show");
            $("#frmContactMobile").submit(function () {
                if($("#frmContactMobile").valid())
                {
                    $.ajax({
                        url: this.action,
                        type: this.method,
                        data: $(this).serialize(),
                        success: function (result) {
                            if (result.success) {
                                $("#contactMobileModal").modal("hide");
                                location.reload();
                            }
                        },
                        error:function()
                        {
                            alert('Error');
                        }
                    });
                    return false;
                }
            });
        });
        return false;
    });


    $("a[data-modal=email]").on("click", function () {
        $("#contactEmailContent").load(this.href, function () {
            $("#contactEmailModal").modal({ keyboard: true }, "show");
            $("#frmContactEmail").submit(function () {
                if ($("#frmContactEmail").valid()) {
                    $.ajax({
                        url: this.action,
                        type: this.method,
                        data: $(this).serialize(),
                        success: function (result) {
                            if (result.success) {
                                $("#contactEmailModal").modal("hide");
                                location.reload();
                            }
                        },
                        error: function () {
                            alert('Error');
                        }
                    });
                    return false;
                }
            });
        });
        return false;
    });

    $("a[data-modal=contact]").on("click", function () {
        $("#contactContent").load(this.href, function () {
            $("#contactModal").modal({ keyboard: true }, "show");
            $("#frmContact").submit(function () {
                if ($("#frmContact").valid()) {
                    $.ajax({
                        url: this.action,
                        type: this.method,
                        data: $(this).serialize(),
                        success: function (result) {
                            if (result.success) {
                                $("#contactModal").modal("hide");
                                location.reload();
                            }
                        },
                        error: function () {
                            alert('Error');
                        }
                    });
                    return false;
                }
            });
        });
        return false;
    });

    
});