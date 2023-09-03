
$('#ddlMainProduct').change(function () {
    debugger;
    /*alert("a");*/
    var MainProdId = $("#ddlMainProduct option:selected").val();
    if (MainProdId.length == 0) {
    } else {
        var s = '<option value="">- Select Product -</option>'
        $('#ddlProduct').html(s);
        $.ajax({
            url: "/Product/GetProduct",
            type: "Get",
            /* url: '@Url.Action("GetCity", "City")',*/
            dataType: "json",
            data: {
                MainProductId: MainProdId
            },
            success: function (result) {
                debugger
                var data = JSON.parse(result);
                for (var i = 0; i < data.length; i++) {
                    var opt = new Option(data[i].ProductName, data[i].ProdId);
                    $('#ddlProduct').append(opt);

                }
            }

        });
    }
});

