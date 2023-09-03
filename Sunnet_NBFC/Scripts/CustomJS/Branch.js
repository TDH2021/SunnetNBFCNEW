
$('#BranchState').change(function () {
    debugger;
    /*alert("a");*/
    var stateid = $("#BranchState option:selected").val();
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


function Validation() {
    var RStartDate = $("#txtRstart").val();
    var RendDate = $("#txtRenddate").val();
    var OwnerName = $("#txtOwnerName").val();
    if (RStartDate.length > 0 && RendDate.length==0) {
        swal("TDH", "Please Enter End Date.", "error");
        return false;
    }
    else if (RStartDate.length == 0 && RendDate.length > 0) {
        swal("TDH", "Please Enter Start Date.", "error");
        return false;
    }
    if (RStartDate.length > 0 && RendDate.length > 0) {
    if (Date.parse(RStartDate) > Date.parse(RendDate) || Date.parse(RendDate) == Date.parse(RStartDate)) {
            swal("TDH", "Rent Start Date can not greater then End Date.", "error");
            //alert
            return false;
        }
    }
    

}