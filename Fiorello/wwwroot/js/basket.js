$(document).ready(function () {

    // Add basket

    $(document).on("submit", "#basket-form", function (e) {
        e.preventDefault();

        let productId = $(this).attr("data-id");
        let data = { id: productId };

        $.ajax({
            url: "cart/addbasket",
            type: "Post",
            data: data,
            success: function (res) {
                $("sup.rounded-circle").text(res)
            }
        })
    })


    // Delete product from basket

    $(document).on("submit", "#basket-delete-form", function (e) {
        e.preventDefault();

        let productId = $(this).attr("data-id");

        $(this).parent().parent().remove();

        let data = { id: productId };

        $.ajax({
            url: "cart/delete",
            type: "Post",
            data: data,
            success: function (res) {
                $("sup.rounded-circle").text(res.count)
                if (res.count != 0) {
                    $("#basket-total-price").text("Total : " + res.total + " ₼")
                }
                else {
                    $("table").addClass("d-none")
                    $("#basket-total-price").addClass("d-none")
                }
            }
        })
    })


})