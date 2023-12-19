
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
function Valid() {
    debugger
    var RoleId = $("#RoleId option:selected").val();
    var RoleName = $("#RoleId option:selected").text();
    var EmpID = $("#EmpID option:selected").val();
    var EmpCode = $("#EmpCode").val();
    var EmpName = $("#EmpName").val();
    var customers = new Array();
    $("#example45 TBODY TR").each(function () {
        var row = $(this);
        var customer = {};

        var sro = row.find("TD").eq(0).html();
        customer.SNo = sro.trim();

        var muname = row.find("TD").eq(1).html();
        customer.MenuName = muname.trim();

        var sbname = row.find("TD").eq(2).html();
        customer.SubMenuName = sbname.trim();



        var mid = row.find("TD").eq(4).html();
        customer.MenuId = mid.trim();

        var sid = row.find("TD").eq(5).html();
        customer.SubMenuId = sid.trim();

        var yes = document.getElementById(customer.SNo);
        if (yes.checked == true) {
            customers.push(customer);
        } else {


        }


    });
    debugger
    var filedata = new FormData();
    filedata.append('MenuRoleS', JSON.stringify(customers));

    var AllDataArray = {
        "RoleId": RoleId,
        "RoleName": RoleName,
        "EmpID": EmpID,
        "EmpCode": EmpCode,
        "EmpName": EmpName

    }
    debugger
    if (RoleId.length == 0 && EmpID.length == 0) {
        swal("TDH", "Please Select Role or Employee.", "error");
    } else if (customers.length == 0) {
    } else {
        filedata.append('AllDataArray', JSON.stringify(AllDataArray));
        $.ajax({
            url: "/UserRole/UpdateMenu",
            type: "POST",
            contentType: false,
            processData: false,
            data: filedata,

            success: function (result) {
                debugger
                var message = JSON.parse(result)[0].ReturnMessage;

                swal({
                    title: "Success",
                    text: message,
                    icon: "success",
                    button: true,

                })
                    .then((willConfirm) => {
                        if (willConfirm) {
                            window.location.pathname = 'UserRole/RoleMaster';
                        }
                    });
            }

        })

    }


}
