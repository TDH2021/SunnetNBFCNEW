
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
function SubmitData() {
    debugger;
    var filedata = new FormData();
    if ($("#RoleId option:selected").val() == "" && $("#EmpID option:selected").val() == "") {
        swal("TDH", "Please Select Role or Employee for Provide Role");
        return false;
    }
    if ($("#RoleId option:selected").val() != "" && $("#EmpID option:selected").val() != "") {
        swal("TDH", "Please Select Either Role or Employee for Provide Role");
        return false;
    }
    else {
        var selectRole = false;
        //Loop through the Table rows and build a JSON array.
        var table = document.getElementById("example1");
        var rows = table.getElementsByTagName("INPUT");
        var rowCount = $("#example1 tr").length;
        var Role = new Array();
        for (var i = 1; i < rowCount; i++) {
            debugger
            var currentRow = rowCount[i];

            var IsSelected = table.rows[i].cells[3].getElementsByTagName('input')[0].checked;

            console.log("Selected" + IsSelected);


            if (IsSelected) {
                selectRole = true;
                var TmpRole = {};
                var MenuId = table.rows[i].cells[1].getElementsByTagName('input')[0].value;
                var SubMenuId = table.rows[i].cells[2].getElementsByTagName('input')[0].value;
                TmpRole.MenuId = MenuId;
                TmpRole.SubMenuId = SubMenuId;
                TmpRole.IsSelected = IsSelected;
                if ($("#EmpID option:selected").val() != "") {
                    TmpRole.EmpId = $("#EmpID option:selected").val();
                    TmpRole.EmpCode = $('#EmpCode').val();
                }
                else {
                    TmpRole.EmpId = "0";
                    TmpRole.EmpCode = "";
                }
               
                if ($("#RoleId option:selected").val() != "") {
                    TmpRole.RoleId = $("#RoleId option:selected").val();
                    TmpRole.RoleName = $("#RoleId option:selected").text();
                }
                else {
                    TmpRole.RoleId = "0";
                    TmpRole.RoleName = "";
                }
                
                TmpRole.CompanyId = $('#hfCompanyId').val();
                Role.push(TmpRole);
            }
        }
        filedata.append('Role', JSON.stringify(Role));
        console.log(Role)
        console.log(filedata)
        
        if (selectRole == true) {
            //Send the JSON array to Controller using AJAX.
            $.ajax({
                type: "POST",
                url: "/UserRole/Role",
                contentType: false,
                processData: false,
                data: filedata,//JSON.stringify({ UserRole: Role }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    debugger;
                    var message = result ;
                }
            });
        }
        else{
            swal("TDH", "Please Select any one Options", "errror");
            return; 
        }
    }
      
   
}
