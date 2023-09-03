$('#State').change(function () {
    debugger;

    var stateid = $("#State option:selected").val();
    if (stateid.length == 0) {
    } else {
        var s = '<option value="">- Select City -</option>'
        $('#City').html(s);
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
                    $('#City').append(opt);

                }
            }


        });
    }
});

function Validation() {
    var regex = /^[a-zA-Z]*$/;
    var ReqType = "Insert";
    var Name = $("#Name").val();
    var Address = $("#Address").val();
    var City = $("#City").val();
    var State = $("#State").val();
    var Country = $("#Country").val();
    var PinCode = $("#Pincode").val();
    var PANNo = $("#PANNo").val();
    var GSTNo = $("#GSTNo").val();
    var CompanyType = $("#CompanyType").val();
    var CompanyDesc = $("#CompanyDesc").val();
    var CompanyOthDesc = $("#CompanyOthDesc").val();
    var CINNo = $("#CINNo").val();
    var RBIRegd = $("#RBIRegd").val();
    var EmailId = $("#EmailId").val();
    var Website = $("#Website").val();
    var MobileNo = $("#MobileNo").val();
    var DateofIncorporation = $("#DateofIncorporation").val();
    var PANregex = /[A-Z]{5}[0-9]{4}[A-Z]{1}$/;
    var GSTreggst = new RegExp('^[0-9]{2}[A-Z]{5}[0-9]{4}[A-Z]1}[1-9A-Z]{1}Z[0-9A-Z]{1}$');
    var CompanyId = 1;
    var name = "";

    var fileUpload = $("#fileuploadapprovalmail").get(0);
    var files = fileUpload.files;
    for (var i = 0; i < files.length; i++) {

        var name = files[i].name;

    }
    if (Name.length == 0) {
        swal("TDH","Please enter company name","error");

    } else if (CompanyType.length == 0) {
        swal("TDH","Please enter company type","error");

    } else if (Address.length == 0) {
        swal("TDH","Please enter address","error");

    }
    else if (GSTNo.length == 0) {
        swal("TDH", "Please enter gstno", "error");
    } else if (!GSTreggst.test(GSTNo) && GSTNo.length!=15) {
        swal("TDH", "GST Identification Number is not valid. It should be in this 11AAAAA1111Z1A1", "error");
    }
    else if (PANNo.length == 0) {
        swal("TDH","Please enter PAN No.");

    } else if (!PANregex.test(PANNo)) {
        swal("TDH","Invalid PAN No.","errror");

    } else if (Country.length == 0) {
        swal("TDH","Please enter country","error");

    } else if (State.length == 0) {
        swal("TDH","Please enter state","error");

    } else if (City.length == 0) {
        swal("TDH","Please enter city","error");

    } else if (Pincode.length == 0) {
        alert("TDH","Please enter pincode","error");

    } else if (CompanyDesc.length == 0) {
        swal("TDH","Please enter CompanyDesc","error");

    } else if (CompanyOthDesc.length == 0) {
        swal("TDH","Please enter CompanyOthDesc","eror");

    } else {

        var filedata = new FormData();
        debugger;
        if (files.length <= 0) {
            filedata.append('LOGO', "");
        }
        for (var i = 0; i < files.length; i++) {

            filedata.append('LOGO', files[i]);
        }
       


        var AllDataArray = {
            "ReqType": ReqType,
            "CompanyName": Name,
            "Address": Address,
            "City": City,
            "State": State,
            "Country": Country,
            "PinCode": PinCode,
            "PANNo": PANNo,
            "GSTNo": GSTNo,
            "CompanyType": CompanyType,
            "CompanyDesc": CompanyDesc,
            "CompanyOthDesc": CompanyOthDesc,
            "CompanyId": CompanyId,
            "RBIRegd": RBIRegd,
            "EmailId": EmailId,
            "Website": Website,
            "MobileNo": MobileNo,
            "DateofIncorporation": DateofIncorporation,
            "CINNo": CINNo,
            "LOGO": name
        }

        filedata.append('AllDataArray', JSON.stringify(AllDataArray));

        $.ajax({
            url: "/Company/AddRequestCompany",
            type: "POST",
            contentType: false,
            processData: false,
            data: filedata,

            success: function (result) {
                debugger

                var message = JSON.parse(result)[0].ReturnMessage;
                swal("TDH",message,"Success");
                if (message == "Record saved succussfully") {

                    window.location.href = '@Url.Action("CompanyView", "Company")'

                } else {


                }

            }



        })



    }

}
