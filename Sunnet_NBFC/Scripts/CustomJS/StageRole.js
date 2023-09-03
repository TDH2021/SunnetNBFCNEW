
$('#EmpID').change(function () {
    //debugger;
    /*alert("a");*/
    var empid = $("#EmpID option:selected").val();
    if (empid.length == 0) {
    } else {
        //var s = '<option value="">- Select City -</option>'
        //$('#CityID').html(s);
        $.ajax({
            url: "/Employee/GetEmpdtl",
            type: "Get",
            /* url: '@Url.Action("GetCity", "City")',*/
            dataType: "json",
            data: {
                EmpId: empid
            },
            success: function (result) {
                //debugger
                var data = JSON.parse(result);
                //for (var i = 0; i < data.length; i++) {
                //    var opt = new Option(data[i].CityName, data[i].Cityid);
                //    $('#CityID').append(opt);
                //}
                $('#EmpCode').val(data[0].EmpCode);
                $('#EmpName').val(data[0].EmpName);
            }
            //var data = JSON.parse(result.Data);
            // var s = '<option value="">- Select City -</option>';
            // for (var i = 0; i < data.length; i++) {
            //     s += '<option value="' + data[i].Cityid + '">' + data[i].CityName + '</option>';
            // }
            // $("#DSACity").html(s);

        });
    }
});

function callfillemp() {
    var empid = $("#EmpID option:selected").val();
    if (empid.length == 0) {
    } else {
        //var s = '<option value="">- Select City -</option>'
        //$('#CityID').html(s);
        $.ajax({
            url: "/Employee/GetEmpdtl",
            type: "Get",
            /* url: '@Url.Action("GetCity", "City")',*/
            dataType: "json",
            data: {
                EmpId: empid
            },
            success: function (result) {
                //debugger
                var data = JSON.parse(result);
                $('#EmpCode').val(data[0].EmpCode);
                $('#EmpName').val(data[0].EmpName);
            }
        });
    }
}

