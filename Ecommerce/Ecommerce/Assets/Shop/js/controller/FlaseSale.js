var user = {
    init: function () {
        user.registerEvents();
    },
    registerEvents: function () {
        $('.btn-circle').off('click').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);
            var id = btn.data('id');
            $.ajax({
                url: "/Areas/Admin/ProductManage/JoinFlaseSale",
                data: { id: id },
                dataType: "json",
                type: "POST",
                success: function (response) {
                    console.log(response);
                    if (response.status == true) {
                        btn.text('Khoá');
                    }
                    else {
                        btn.text('Kích hoạt');
                    }
                }
            });
        });
    }
}
user.init();