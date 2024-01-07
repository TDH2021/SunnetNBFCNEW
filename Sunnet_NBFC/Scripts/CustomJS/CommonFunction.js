$('#MainProductId').change(function () {
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


$('#ddlBranch').change(function () {
    var BranchId = $("#ddlBranch option:selected").val();
    if (BranchId.length == 0) {
        var s = '<option value="">- Select Center -</option>'
        $('#ddlCenter').html(s);
    } else {

        var s = '<option value="">- Select Center-</option>'
        $('#ddlCenter').html(s);
        $.ajax({
            url: "/Center/GetCenter",
            type: "Get",
            dataType: "json",
            data: {
                BranchId: BranchId
            },
            success: function (result) {

                var data = JSON.parse(result);
                for (var i = 0; i < data.length; i++) {
                    var opt = new Option(data[i].CenterName, data[i].CenterId);
                    $('#ddlCenter').append(opt);
                }
            }
        });
    }
});

function isNumber(evt) {
    var charCode = (window.event.which) ? window.event.which : window.event.keyCode
    if (charCode > 31 && charCode != 46 && (charCode < 48 || charCode > 57)) {
        return false;
    }
    else {
        return true;
    }

}

function ValidatePAN(strval, id) {
    var txtPANCard = strval;
    /*alert(strval);*/
    var regex = /([A-Z]){5}([0-9]){4}([A-Z]){1}$/;
    if (regex.test(txtPANCard.toUpperCase())) {
        return true;
    } else {
        alert("Invalid PAN No.")
        document.getElementById(id).value = "";
        return false;
    }
}
