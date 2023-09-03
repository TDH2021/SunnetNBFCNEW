

$('#permanentstate').change(function () {
    debugger;
   
    var stateid = $("#permanentstate option:selected").val();
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
                    $('#permanentcity').append(opt);

                }
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


$('#CorState').change(function () {
    debugger;
    /*alert("a");*/
    var stateid = $("#CorState option:selected").val();
    if (stateid.length == 0) {
    } else {
        var s = '<option value="">- Select City -</option>'
        $('#Corcity').html(s);
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
                    $('#Corcity').append(opt);

                }
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


function Validation() {
    var regex = /^[a-zA-Z]*$/;
    var ReqType = "Insert";
    var FName = $("#FName").val();
    var MName = $("#MName").val();
    var LName = $("#LName").val();
    var FatherName = $("#FatherName").val();
    var MotherName = $("#MotherName").val();
    var SpouseName = $("#SpouseName").val();
    var Dob = $("#Dob").val();
    var Gender = $("#Gender option:selected").val();
    var MartialStatus = $("#MartialStatus").val();
    var PresentAddress = $("#PresentAddress").val();
    var PresentPincode = $("#PresentPincode").val();
    var PresentStateId = $("#presentstate option:selected").val();
    var PresentCityId = $("#presentcity option:selected").val();
    var PermanentAddress = $("#PermanentAddress").val();
    var PermanentPincode = $("#PermanentPincode").val();
    var PermanentStateId = $("#permanentstate option:selected").val();
    var PermanentCityId = $("#permanentcity option:selected").val();
    var CibilScore = $("#CibilScore").val();
    var MobileNumber1 = $("#MobileNumber1").val();
    /*var MobileNumber2 = $("#MobileNumber2").val();*/
    var FatherMobileNumber = $("#FatherMobileNumber").val();
    var MotherMobileNumber = $("#MotherMobileNumber").val();
    var SpouseMobileNumber = $("#SpouseMobileNumber").val();
    var AadharNo = $("#AadharNo").val();
    var PanNo = $("#PanNo").val();
    var CompanyID = 1;
    if (FName.length == 0) {
        alert("Please enter first name");
    } else if (MName.length == 0) {
        alert("Please enter middle name");
    } else if (LName.length == 0) {
        alert("Please enter last name");
    } else if (FatherName.length == 0) {
        alert("Please enter father name");
    } else if (MotherName.length == 0) {
        alert("Please enter mother name");
    } else if (SpouseName.length == 0) {
        alert("Please enter spouse name");
    } else if (Dob.length == 0) {
        alert("Please enter Dob");
    } else if (MartialStatus.length == 0) {
        alert("Please enter material status");
    } else if (PresentAddress.length == 0) {
        alert("Please enter present address");
    } else if (PresentPincode.length == 0) {
        alert("Please enter present pincode");
    } else if (PermanentAddress.length == 0) {
        alert("Please enter permanent address");
    } else if (PermanentPincode.length == 0) {
        alert("Please enter permanent pincode");
    } else if (CibilScore.length == 0) {
        alert("Please enter cibil score");
    } else if (MobileNumber1.length == 0) {
        alert("Please enter mobile number1");

    } 
    //    else if (MobileNumber2.length == 0) {
    ////    alert("Please enter mobile number2");
    //}
    else if (FatherMobileNumber.length == 0) {
        alert("Please enter father mobile number");
    } else if (MotherMobileNumber.length == 0) {
        alert("Please enter mother mobile number");
    } else if (SpouseMobileNumber.length == 0) {
        alert("Please enter spouse mobile number");
    } else if (AadharNo.length == 0) {
        alert("Please enter Aadhar no");
    } else if (PanNo.length == 0) {
        alert("Please enter Pan no");
    } else {
        var filedata = new FormData();

        var FName = $("#FName").val();
        var MName = $("#MName").val();
        var LName = $("#LName").val();
        var FatherName = $("#FatherName").val();
        var MotherName = $("#MotherName").val();
        var SpouseName = $("#SpouseName").val();
        var Gender = $("#Gender").val();
        var Dob = $("#Dob").val();
        var MartialStatus = $("#MartialStatus").val();
        var PresentAddress = $("#PresentAddress").val();
        var PresentStateId = $("#CorState").val();
        var PresentCityId = $("#Corcity").val();
        var PresentPincode = $("#PresentPincode").val();
        var PermanentAddress = $("#PermanentAddress").val();
        var PermanentPincode = $("#PermanentPincode").val();
        var PermanentStateId = $("#permanentstate").val();
        var PermanentCityId = $("#permanentcity").val();
        var CibilScore = $("#CibilScore").val();
        var MobileNumber1 = $("#MobileNumber1").val();
 /*       var MobileNumber2 = $("#MobileNumber2").val();*/
        var FatherMobileNumber = $("#FatherMobileNumber").val();
        var MotherMobileNumber = $("#MotherMobileNumber").val();
        var SpouseMobileNumber = $("#SpouseMobileNumber").val();
        var AadharNo = $("#AadharNo").val();
        var PanNo = $("#PanNo").val();


        var AllDataArray = {
            "ReqType": ReqType,
            "FirstName": FName,
            "MiddleName": MName,
            "LastName": LName,
            "FastherName": FatherName,
            "MotherName": MotherName,
            "SpouseName": SpouseName,
            "Gender": Gender,
            "Dob": Dob,
            "MaterialStatus": MartialStatus,
            "PresentAddress": PresentAddress,
            "PresentPincode": PresentPincode,
            "PresentStateId": PresentStateId,
            "PresentCityId": PresentCityId,
            "PermanentAddress": PermanentAddress,
            "PermanentPincode": PermanentPincode,
            "PermanentStateId": PermanentStateId,
            "PermanentCityId": PermanentCityId,
            "CibilScore": CibilScore,
            "MobileNo1": MobileNumber1,
            /*"MobileNo2": MobileNumber2,*/
            "FatherMobileNo": FatherMobileNumber,
            "MotherMobileNo": MotherMobileNumber,
            "SpouseMobileNo": SpouseMobileNumber,
            "AadharNo": AadharNo,
            "PanNo": PanNo,
            "CompanyId": CompanyID
        }

        filedata.append('AllDataArray', JSON.stringify(AllDataArray));

        $.ajax({
            url: "/Customer/AddRequestCustomer",
            type: "POST",
            contentType: false,
            processData: false,
            data: filedata,

            success: function (result) {
                debugger

                var message = JSON.parse(result)[0].ReturnMessage;
                alert(message);
                if (message == "Customer saved succussfully") {

                    window.location.href = "/Customer/CustomerView";

                } else {


                }




            }



        })



    }

}
