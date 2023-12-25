
//$('#EmpID').change(function () {
//    //debugger;
//    /*alert("a");*/
//    var empid = $("#EmpID option:selected").val();
//    if (empid.length == 0) {
//    } else {
//        //var s = '<option value="">- Select City -</option>'
//        //$('#CityID').html(s);
//        $.ajax({
//            url: "/Employee/GetEmpdtl",
//            type: "Get",
//            /* url: '@Url.Action("GetCity", "City")',*/
//            dataType: "json",
//            data: {
//                EmpId: empid
//            },
//            success: function (result) {
//                //debugger
//                var data = JSON.parse(result);
//                $('#EmpCode').val(data[0].EmpCode);
//                $('#EmpName').val(data[0].EmpName);

//            }
            
//        });
//    }
//});



$(function () {
    $('#selectall').click(function () {
        if (this.checked) {
            $(".checkbox").prop("checked", true);
        } else {
            $(".checkbox").prop("checked", false);
        }
    });
});
function Valid() {
    debugger
    var RoleId = $("#ddlRoleId option:selected").val();
    var RoleName = $("#ddlRoleId option:selected").text();
    var EmpID = "0";
    var EmpCode = "";
    var EmpName = "";
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
        "EmpCode": "",
        "EmpName": "",

    }
    debugger
    if (RoleId.length == 0 && EmpID.length == 0) {
        swal("TDH", "Please Select Role or Employee.", "error");
    } else if (customers.length == 0) {
    } else {
        filedata.append('AllDataArray', JSON.stringify(AllDataArray));
        $.ajax({
            url: "/UserRole/Role",
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


function Valid_Employee() {
    debugger
    var RoleId = "0";
    var RoleName = "";
    var EmpID = $("#EmpID option:selected").val(); ;
    var EmpCode = "";
    var EmpName = "";
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
        "EmpCode": "",
        "EmpName": "",

    }
    debugger
    if (EmpID.length == 0) {
        swal("TDH", "Please Select Employee.", "error");
    } else if (customers.length == 0) {
    } else {
        filedata.append('AllDataArray', JSON.stringify(AllDataArray));
        $.ajax({
            url: "/UserRole/Role",
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
                            window.location.pathname = 'UserRole/RoleMaster_EmpWise';
                        }
                    });
            }

        })
    }
}
