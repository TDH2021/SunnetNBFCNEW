﻿$('#MainProductId').change(function () {
    var MainProductId = $("#MainProductId option:selected").val();
    if (MainProductId.length == 0) {
        var s = '<option value="">- Select Product -</option>'
        $('#ProductId').html(s);
    } else {

        var s = '<option value="">- Select Product -</option>'
        $('#ProductId').html(s);
        $.ajax({
            url: "/Product/GetProduct",
            type: "Get",
            dataType: "json",
            data: {
                MainProductId: MainProductId
            },
            success: function (result) {
               
                var data = JSON.parse(result);
                for (var i = 0; i < data.length; i++) {
                    var opt = new Option(data[i].ProductName, data[i].ProdId);
                    $('#ProductId').append(opt);

                }
            }

        });
    }
});


function isNumber(evt) {
    evt = (evt) ? evt : window.event;
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
        return false;
    }
    return true;
}