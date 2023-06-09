$(document).ready(function () {


    $(document).on("click", ".show-more-btn", function () {
        let parent = $("#parent-elem")

        let skipCount = $(parent).children().length;

        let productsCount = $("#products").attr("data-count")

        $.ajax({
            url: `shop/showmoreorless?skip=${skipCount}`,
            type: "Get",
            success: function (res) {
                $(parent).append(res)
                skipCount = $(parent).children().lenght;
                if (skipCount >= productsCount) {
                    $(".show-more-btn").addClass("d-none")
                }
            }
        })
    })
})