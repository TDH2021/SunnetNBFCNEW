
$('#DSAState').change(function () {
    debugger;
    /*alert("a");*/
    var stateid = $("#DSAState option:selected").val();
    if (stateid.length == 0) {
    } else {
        var s = '<option value="">- Select City -</option>'
        $('#DSACity').html(s);
        $.ajax({
            url: "/City/GetCity",
            type: "Get",
            /* url: '@Url.Action("GetCity", "City")',*/
            dataType: "json",
            data: {
                StateId: stateid
            },
            success: function (result) {
                debugger
                var data = JSON.parse(result);
                for (var i = 0; i < data.length; i++) {
                    var opt = new Option(data[i].CityName, data[i].Cityid);
                    $('#DSACity').append(opt);

                }
            }


        });
    }
});
$("#txtDsaIFSCCode").change(function () {
    var inputvalues = $(this).val();
    var reg = /[A-Z|a-z]{4}[0][a-zA-Z0-9]{6}$/;
    if (inputvalues.match(reg)) {
        return true;
    }
    else {
        $("#txtDsaIFSCCode").val("");
        swal("TDH","You entered invalid IFSC code","error");
        document.getElementById("txtDsaIFSCCode").focus();
        return false;
    }
});

