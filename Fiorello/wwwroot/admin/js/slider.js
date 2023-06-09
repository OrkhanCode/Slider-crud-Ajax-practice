$(document).ready(function () {



    $(document).on("click", ".status-slider", function (e) {

        let clickedBtn = $(this);

        let productId = $(this).attr("data-id");
        let data = { id: productId };

        $.ajax({
            url: "slider/changestatus",
            type: "Post",
            data: data,
            success: function (res) {
                if (clickedBtn.hasClass("status-true")) {
                    clickedBtn.removeClass("status-true")
                    clickedBtn.addClass("status-false")
                }
                else {
                    clickedBtn.addClass("status-true")
                    clickedBtn.removeClass("status-false")
                }
            }
        })
    })



})