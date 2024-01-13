var CompanyID = $("#hdnCompanyId").val();
$('#MainProductId').change(function () {


    var MainProductId = $("#MainProductId option:selected").val();
    var MainProducttext = $("#MainProductId option:selected").text();
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
                if (MainProducttext == "Individual Loan") {
                    $('#business_div').hide();
                    $('#vechical_div').hide();
                    $('#personal_div').show();
                    $('#divPersonal').show();
                    $('#divRef1').hide();
                    $('#divRef2').hide();
                } else if (MainProducttext.toUpperCase() == "VEHICLE LOAN" || MainProducttext.toUpperCase() == "TWO WHEELER LOAN" || MainProducttext.toUpperCase() == "COMMERCIAL VEHICLE") {
                    $('#business_div').hide();
                    $('#vechical_div').show();
                    $('#personal_div').show();
                    $('#divPersonal').hide();
                    $('#divRef1').show();
                    $('#divRef2').show();
                } else if (MainProducttext == "Bussiness Loan") {
                    $('#business_div').show();
                    $('#personal_div').show();
                    $('#vechical_div').hide();
                    $('#divPersonal').hide();
                    $('#divRef1').show();
                    $('#divRef2').show();
                }
                var data = JSON.parse(result);
                for (var i = 0; i < data.length; i++) {
                    var opt = new Option(data[i].ProductName, data[i].ProdId);
                    $('#ProductId').append(opt);

                }
            }

        });
    }
});
$('#ProductId').change(function () {
    debugger;

    var MainProductId = $("#MainProductId option:selected").val();
    var ProductId = $("#ProductId option:selected").val();


    $.ajax({
        url: "/Product/GetSubProduct",
        type: "Get",

        dataType: "json",
        data: {
            MainProductId: MainProductId,
            ProductId: ProductId
        },
        success: function (result) {
            debugger
            var data = JSON.parse(result)[0].CustTypeRequried;
            document.getElementById("hdn_type").value = data;
            if (data == "c") {
                $("#co_applicant_div").show();
                $("#co_guranter_div").hide();

            } else if (data == "g") {
                $("#co_applicant_div").hide();
                $("#co_guranter_div").show();
                //    document.getElementById("ProductId").value = data;
            } else if (data == "b") {
                $("#co_applicant_div").show();
                $("#co_guranter_div").show();
                //    document.getElementById("ProductId").value = data;
            }
        }

    });

});

function underAgisapicheckeValidate(birthday) {


    // it will accept two types of format yyyy-mm-dd and yyyy/mm/dd
    var optimizedBirthday = birthday.replace(/-/g, "/");

    //set date based on birthday at 01:00:00 hours GMT+0100 (CET)
    var myBirthday = new Date(optimizedBirthday);

    // set current day on 01:00:00 hours GMT+0100 (CET)
    var currentDate = new Date().toJSON().slice(0, 10) + ' 01:00:00';

    // calculate age comparing current date and borthday
    var myAge = ~~((Date.now(currentDate) - myBirthday) / (31557600000));

    if (myAge < 18) {
        return myAge;
    } else {
        return myAge;
    }


}
function underAgeValidate(birthday) {

    debugger;
    // it will accept two types of format yyyy-mm-dd and yyyy/mm/dd
    var optimizedBirthday = birthday.replace(/-/g, "/");
    let [day, month, year] = birthday.split('/')
    const dateObj = new Date(+year, +month - 1, +day)
    //set date based on birthday at 01:00:00 hours GMT+0100 (CET)
    var myBirthday = new Date(optimizedBirthday);

    // set current day on 01:00:00 hours GMT+0100 (CET)
    var currentDate = new Date().toJSON().slice(0, 10) + ' 01:00:00';

    // calculate age comparing current date and borthday
    var myAge = ~~((Date.now(currentDate) - dateObj) / (31557600000));

    if (myAge < 18) {
        return myAge;
    } else {
        return myAge;
    }


}
function AAdharVerification() {
    debugger
    var AadharNo = $("#AadharNo").val();
    var emailid = "";

    if (AadharNo.length != 12) {
        swal({
            title: "Error",
            text: "Please enter correct aadhar no",
            icon: "error",
            button: true,

        })
            .then((willConfirm) => {
                if (willConfirm) {

                }
            });

    } else {
        debugger
        var urlvariable;

        urlvariable = "https://sm-kyc-sandbox.scoreme.in/kyc/external/aadhaarverificationdetails";

        var ItemJSON;

        ItemJSON = '{"aadhaarNumber": "' + AadharNo + '","email":  "' + emailid + '"}';
        debugger
        URL = urlvariable;  //Your URL

        var xmlhttp = new XMLHttpRequest();
        xmlhttp.onreadystatechange = callbackFunction(xmlhttp);
        xmlhttp.open("POST", URL, false);
        xmlhttp.setRequestHeader("Content-Type", "application/json");
        xmlhttp.setRequestHeader('ClientId', "9b7f56d730cb292199b7a49620fa80f6");
        xmlhttp.setRequestHeader('ClientSecret', "60812117e31f68b0475960d167a6fc856a09de16cc89f17f8ece51e29f216b15");
        xmlhttp.onreadystatechange = callbackFunction(xmlhttp);
        xmlhttp.send(ItemJSON);
        debugger

        var data = JSON.parse(xmlhttp.responseText);

        if (data.responseMessage == "OTP successfully sent to mobile number.") {
            document.getElementById("referenceId").value = data.data.referenceId;
            swal({
                title: "Success",
                text: data.responseMessage,
                icon: "success",
                button: true,

            })
                .then((willConfirm) => {
                    if (willConfirm) {

                        debugger

                        $.ajax({
                            url: "/LeadGeneration/SetApiResponse",
                            type: "Get",
                            /* url: '@Url.Action("GetCity", "City")',*/
                            dataType: "json",
                            data: {
                                stage: 1,
                                OTP: "",
                                referenceId: "",
                                AadharNo: AadharNo,
                                email: emailid,
                                urlvariable: urlvariable,
                                responseCode: data.responseCode,
                                responseMessage: data.responseMessage,
                                response: JSON.parse(xmlhttp.responseText)
                            },
                            success: function (result) {

                            }

                        });


                        $("#myModal").modal()
                    }
                });

        } else if (data.message == "Credits not available.") {
            swal({
                title: "Error",
                text: data.message,
                icon: "error",
                button: true,

            })
                .then((willConfirm) => {
                    if (willConfirm) {

                        $.ajax({
                            url: "/LeadGeneration/SetApiResponse",
                            type: "Get",
                            /* url: '@Url.Action("GetCity", "City")',*/
                            dataType: "json",
                            data: {
                                stage: 1,
                                OTP: "",
                                referenceId: "",
                                AadharNo: AadharNo,
                                email: emailid,
                                urlvariable: urlvariable,
                                responseCode: data.status,
                                responseMessage: data.message,
                                response: JSON.parse(xmlhttp.responseText)
                            },
                            success: function (result) {

                            }

                        });

                    }
                });

        } else {
            swal({
                title: "Error",
                text: data.responseMessage,
                icon: "error",
                button: true,

            })
                .then((willConfirm) => {
                    if (willConfirm) {

                        $.ajax({
                            url: "/LeadGeneration/SetApiResponse",
                            type: "Get",
                            /* url: '@Url.Action("GetCity", "City")',*/
                            dataType: "json",
                            data: {
                                stage: 1,
                                OTP: "",
                                referenceId: "",
                                AadharNo: AadharNo,
                                email: emailid,
                                urlvariable: urlvariable,
                                responseCode: data.responseCode,
                                responseMessage: data.responseMessage,
                                response: JSON.parse(xmlhttp.responseText)
                            },
                            success: function (result) {

                            }

                        });

                    }
                });
        }

    }


}
function callbackFunction(xmlhttp) {
    //alert(xmlhttp.responseXML);
}
function ChkAadharOTP() {
    debugger
    var OTP = $("#OTP1").val() + $("#OTP2").val() + $("#OTP3").val() + $("#OTP4").val() + $("#OTP5").val() + $("#OTP6").val();
    if (OTP.length != 6) {
        swal("TDH", "Please Enter Valid OTP", "error");

    } else {


        debugger
        var referenceId = $("#referenceId").val();
        var urlvariable;

        urlvariable = "https://sm-kyc-sandbox.scoreme.in/kyc/external/aadhaarotpverification";

        var ItemJSON;

        ItemJSON = '{"otp": "' + OTP + '","referenceId":  "' + referenceId + '"}';

        URL = urlvariable;  //Your URL

        var xmlhttp = new XMLHttpRequest();
        xmlhttp.onreadystatechange = callbackFunction(xmlhttp);
        xmlhttp.open("POST", URL, false);
        xmlhttp.setRequestHeader('ClientId', "9b7f56d730cb292199b7a49620fa80f6");
        xmlhttp.setRequestHeader('ClientSecret', "60812117e31f68b0475960d167a6fc856a09de16cc89f17f8ece51e29f216b15");
        xmlhttp.onreadystatechange = callbackFunction(xmlhttp);
        debugger

        xmlhttp.send(ItemJSON);

        var data = JSON.parse(xmlhttp.responseText);


        if (data.responseMessage == "Successfully Completed.") {

            swal({
                title: "Success",
                text: "Aadhar verify successfully",
                icon: "success",
                button: true,

            })
                .then((willConfirm) => {
                    if (willConfirm) {
                        debugger
                        document.getElementById("hdn_customer_aadhar_verify").value = 1;


                        $.ajax({
                            url: "/LeadGeneration/SetApiResponse",
                            type: "Get",
                            /* url: '@Url.Action("GetCity", "City")',*/
                            dataType: "json",
                            data: {
                                stage: 2,
                                OTP: OTP,
                                referenceId: referenceId,
                                AadharNo: "",
                                email: "",
                                urlvariable: urlvariable,
                                responseCode: data.responseCode,
                                responseMessage: data.responseMessage,
                                response: JSON.parse(xmlhttp.responseText)
                            },
                            success: function (result) {

                            }

                        });

                        $("#myModal").modal("hide");
                    }
                });


        } else if (data.message == "Credits not available.") {
            swal({
                title: "Error",
                text: data.message,
                icon: "error",
                button: true,

            })
                .then((willConfirm) => {
                    if (willConfirm) {

                        $.ajax({
                            url: "/LeadGeneration/SetApiResponse",
                            type: "Get",
                            /* url: '@Url.Action("GetCity", "City")',*/
                            dataType: "json",
                            data: {
                                stage: 1,
                                OTP: "",
                                referenceId: "",
                                AadharNo: AadharNo,
                                email: emailid,
                                urlvariable: urlvariable,
                                responseCode: data.status,
                                responseMessage: data.message,
                                response: JSON.parse(xmlhttp.responseText)
                            },
                            success: function (result) {

                            }

                        });

                    }
                });

        } else {
            swal({
                title: "Error",
                text: data.responseMessage,
                icon: "error",
                button: true,

            })
                .then((willConfirm) => {
                    if (willConfirm) {
                        $.ajax({
                            url: "/LeadGeneration/SetApiResponse",
                            type: "Get",
                            /* url: '@Url.Action("GetCity", "City")',*/
                            dataType: "json",
                            data: {
                                stage: 2,
                                OTP: OTP,
                                referenceId: referenceId,
                                AadharNo: "",
                                email: "",
                                urlvariable: urlvariable,
                                responseCode: data.responseCode,
                                responseMessage: data.responseMessage,
                                response: JSON.parse(xmlhttp.responseText)
                            },
                            success: function (result) {

                            }

                        });
                    }
                });
        }

    }

}
function PanVerification() {
    debugger

    var panNumber = $("#PanNo").val();
    var fullName = $("#FName").val() + " " + $("#MName").val() + " " + $("#LName").val();
    var dob = moment($("#Dob").val()).format('DD-MMM-YYYY');

    if (fullName.trim().length == 0) {
        swal({
            title: "Error",
            text: "Please enter customer full name",
            icon: "error",
            button: true,

        })
            .then((willConfirm) => {
                if (willConfirm) {

                }
            });

    }
    else if (panNumber.length != 10) {
        swal({
            title: "Error",
            text: "Please enter correct pan no",
            icon: "error",
            button: true,

        })
            .then((willConfirm) => {
                if (willConfirm) {

                }
            });

    }
    else if (dob.length == 0) {
        swal("TDH", "Please Enter DOB.", "error");
    } else if (dob == "Invalid date") {
        swal("TDH", "Please Enter Correct DOB", "error");
    } else {
        debugger
        var urlvariable;

        urlvariable = "https://sm-kyc-sandbox.scoreme.in/kyc/external/panverification";

        var ItemJSON;

        ItemJSON = '{"panNumber": "' + panNumber + '","fullName":  "' + fullName + '", "dateOfBirth":  "' + dob + '", "status":  "' + "individual" + '"}';

        debugger
        alert(ItemJSON)

        URL = urlvariable;  //Your URL

        var xmlhttp = new XMLHttpRequest();
        xmlhttp.onreadystatechange = callbackFunction(xmlhttp);
        xmlhttp.open("POST", URL, false);
        xmlhttp.setRequestHeader("Content-Type", "application/json");
        xmlhttp.setRequestHeader('ClientId', "9b7f56d730cb292199b7a49620fa80f6");
        xmlhttp.setRequestHeader('ClientSecret', "60812117e31f68b0475960d167a6fc856a09de16cc89f17f8ece51e29f216b15");
        xmlhttp.onreadystatechange = callbackFunction(xmlhttp);
        xmlhttp.send(ItemJSON);
        debugger

        var data = JSON.parse(xmlhttp.responseText);

        if (data.responseMessage == "Successfully Submitted.") {
            document.getElementById("referenceId").value = data.data.referenceId;
            swal({
                title: "Success",
                text: data.responseMessage,
                icon: "success",
                button: true,

            })
                .then((willConfirm) => {
                    if (willConfirm) {
                        debugger
                        data = JSON.parse(xmlhttp.responseText);
                        var rid = data.data.referenceId;
                        debugger


                        $.ajax({
                            url: "/LeadGeneration/SetPANApiResponse",
                            type: "Get",
                            /* url: '@Url.Action("GetCity", "City")',*/
                            dataType: "json",
                            data: {
                                stage: 1,
                                ApiName: "PanVerify",
                                OTP: "",
                                referenceId: "",
                                Panno: panNumber,
                                email: "",
                                urlvariable: urlvariable,
                                responseCode: data.responseCode,
                                responseMessage: data.responseMessage,
                                response: JSON.parse(xmlhttp.responseText)
                            },
                            success: function (result) {

                            }

                        });



                        $.ajax({
                            url: "https://sm-kyc-sandbox.scoreme.in/kyc/external/getkycrequestresponse?referenceId=" + rid,
                            type: 'GET',
                            dataType: 'json',
                            headers: {
                                'ClientId': "9b7f56d730cb292199b7a49620fa80f6",
                                'ClientSecret': "60812117e31f68b0475960d167a6fc856a09de16cc89f17f8ece51e29f216b15"
                            },
                            contentType: 'application/json; charset=utf-8',
                            success: function (result) {
                                // CallBack(result);
                                debugger

                                if (data.responseMessage == "Successfully Submitted.") {

                                    swal({
                                        title: "Success",
                                        text: "Pan verify successfully",
                                        icon: "success",
                                        button: true,

                                    })
                                        .then((willConfirm) => {
                                            if (willConfirm) {
                                                document.getElementById("hdn_customer_aadhar_verify").value = 1;

                                                $.ajax({
                                                    url: "/LeadGeneration/SetPANApiResponse",
                                                    type: "Get",
                                                    /* url: '@Url.Action("GetCity", "City")',*/
                                                    dataType: "json",
                                                    data: {
                                                        stage: 2,
                                                        ApiName: "PanOTPVerify",
                                                        OTP: "",
                                                        referenceId: rid,
                                                        Panno: panNumber,
                                                        email: "",
                                                        urlvariable: urlvariable,
                                                        responseCode: data.responseCode,
                                                        responseMessage: data.responseMessage,
                                                        response: JSON.parse(xmlhttp.responseText)
                                                    },
                                                    success: function (result) {

                                                    }

                                                });

                                            }
                                        });


                                } else {
                                    swal({
                                        title: "Error",
                                        text: data.responseMessage,
                                        icon: "error",
                                        button: true,

                                    })
                                        .then((willConfirm) => {
                                            if (willConfirm) {
                                                $.ajax({
                                                    url: "/LeadGeneration/SetPANApiResponse",
                                                    type: "Get",
                                                    /* url: '@Url.Action("GetCity", "City")',*/
                                                    dataType: "json",
                                                    data: {
                                                        stage: 2,
                                                        ApiName: "PanOTPVerify",
                                                        OTP: "",
                                                        referenceId: rid,
                                                        Panno: panNumber,
                                                        email: "",
                                                        urlvariable: urlvariable,
                                                        responseCode: data.responseCode,
                                                        responseMessage: data.responseMessage,
                                                        response: JSON.parse(xmlhttp.responseText)
                                                    },
                                                    success: function (result) {

                                                    }

                                                });
                                            }
                                        });
                                }
                            },
                            error: function (error) {
                                debugger
                                alert(error);
                            }
                        })

                    }
                });
        }

        else {
            swal({
                title: "Error",
                text: data.responseMessage,
                icon: "error",
                button: true,

            })
                .then((willConfirm) => {
                    if (willConfirm) {
                        $.ajax({
                            url: "/LeadGeneration/SetPANApiResponse",
                            type: "Get",
                            /* url: '@Url.Action("GetCity", "City")',*/
                            dataType: "json",
                            data: {
                                stage: 1,
                                ApiName: "PanVerify",
                                OTP: "",
                                referenceId: "",
                                Panno: panNumber,
                                email: "",
                                urlvariable: urlvariable,
                                responseCode: data.responseCode,
                                responseMessage: data.responseMessage,
                                response: JSON.parse(xmlhttp.responseText)
                            },
                            success: function (result) {

                            }

                        });

                    }
                });
        }

    }


}
$('#MartialStatus').change(function () {
    debugger;

    var MartialStatus = $("#MartialStatus option:selected").val();


    if (MartialStatus == "0") {
        document.getElementById("SpouseName").disabled = false;
        document.getElementById("SpouseMobileNumber").disabled = false;
    } else if (MartialStatus == "Single") {
        document.getElementById("SpouseName").disabled = true;
        document.getElementById("SpouseMobileNumber").disabled = true;
    } else if (MartialStatus == "Married") {
        document.getElementById("SpouseName").disabled = false;
        document.getElementById("SpouseMobileNumber").disabled = false;

    }


});
function mytest() {
    var yes = document.getElementById("vehicle1").checked;
    if (yes == true) {
        var PresentAddress = $("#PresentAddress").val();
        var PresentPincode = $("#PresentPincode").val();
        var PresentStateId = $("#customerpresentState").val();
        var PresentCityId = $("#customerpresentcity").val();
        var PresentVillage = $("#PresentVillage").val();
        var PresentDistrict = $("#PresentDistrict").val();

        document.getElementById("PermanentAddress").value = PresentAddress;
        document.getElementById("PermanentPincode").value = PresentPincode;
        document.getElementById("customerpermanentstate").value = PresentStateId;
        document.getElementById("PermanentVillage").value = PresentVillage;
        document.getElementById("PermanentDistrict").value = PresentDistrict;
        document.getElementById("customerpermanentcity").value = PresentCityId;

        document.getElementById("PermanentAddress").disabled = true;
        document.getElementById("PermanentPincode").disabled = true;
        document.getElementById("customerpermanentstate").disabled = true;
        document.getElementById("customerpermanentcity").disabled = true;
        document.getElementById("PermanentVillage").disabled = true;
        document.getElementById("PermanentDistrict").disabled = true;

    } else {
        document.getElementById("PermanentAddress").value = "";
        document.getElementById("PermanentPincode").value = "";
        document.getElementById("customerpermanentstate").value = "";
        document.getElementById("customerpermanentcity").value = "";
        document.getElementById("PermanentVillage").value = "";
        document.getElementById("PermanentDistrict").value = "";

        document.getElementById("PermanentAddress").disabled = false;
        document.getElementById("PermanentPincode").disabled = false;
        document.getElementById("customerpermanentstate").disabled = false;
        document.getElementById("customerpermanentcity").disabled = false;
        document.getElementById("PermanentVillage").disabled = false;
        document.getElementById("PermanentDistrict").disabled = false;
    }

}
function SetValues() {
    var yes = document.getElementById("vehicle1").checked;
    if (yes == true) {
        var PresentAddress = $("#PresentAddress").val();

        document.getElementById("PermanentAddress").value = PresentAddress;

        document.getElementById("PermanentAddress").disabled = true;

    } else {
        document.getElementById("PermanentAddress").value = "";
        document.getElementById("PermanentAddress").disabled = false;

    }

}
function SetValues1() {
    debugger;

    var yes = document.getElementById("vehicle1").checked;

    var PresentPin = $("#PresentPincode").val();

    $.ajax({
        url: $("#hdn_postalAPI").val() + PresentPin,
        type: "Get",
        dataType: "json",
        cache: false,
        processData: false,
        data: {
            MainProductId: MainProductId
        },
        success: function (result) {
            debugger
            if (result[0]["Message"].toUpperCase() == "NO RECORD FOUND") {
                swal("TDH", result[0]["Message"], "error");
                return;
            }
            else {
                var citydata = result[0].PostOffice[0].Division;
                const myArray = citydata.split(" ");
                let word = myArray[0];

                document.getElementById("customerpresentcity").value = word;

                var statedata = result[0].PostOffice[0].State;
                document.getElementById("customerpresentState").value = statedata;
            }

        }

    });

    if (yes == true) {
        var PresentPincode = $("#PresentPincode").val();

        document.getElementById("PermanentPincode").value = PresentPincode;

        document.getElementById("PermanentPincode").disabled = true;

    } else {
        document.getElementById("PermanentPincode").value = "";
        document.getElementById("PermanentPincode").disabled = false;

    }



}
function SetValues2() {
    var yes = document.getElementById("vehicle1").checked;
    if (yes == true) {
        var PresentStateId = $("#customerpresentState").val();
        document.getElementById("customerpermanentstate").value = PresentStateId;
        document.getElementById("customerpermanentstate").disabled = true;

    } else {
        document.getElementById("customerpermanentstate").value = "";
        document.getElementById("customerpermanentstate").disabled = false;

    }

}
function SetValues3() {
    debugger
    var yes = document.getElementById("vehicle1").checked;
    if (yes == true) {
        var PresentStateId = $("#customerpresentState").val();
        var PresentCityId = $("#customerpresentcity").val();

        document.getElementById("customerpermanentcity").value = PresentCityId;
        document.getElementById("customerpermanentcity").disabled = true;

    } else {
        document.getElementById("customerpermanentcity").value = "";
        document.getElementById("customerpermanentcity").disabled = false;

    }

}
function SetValues4() {
    var yes = document.getElementById("vehicle1").checked;
    if (yes == true) {
        var PresentVillage = $("#PresentVillage").val();

        document.getElementById("PermanentVillage").value = PresentVillage;

        document.getElementById("PermanentVillage").disabled = true;

    } else {
        document.getElementById("PermanentVillage").value = "";
        document.getElementById("PermanentVillage").disabled = false;

    }

}
function SetValues5() {
    var yes = document.getElementById("vehicle1").checked;
    if (yes == true) {
        var PresentDistrict = $("#PresentDistrict").val();

        document.getElementById("PermanentDistrict").value = PresentDistrict;

        document.getElementById("PermanentDistrict").disabled = true;

    } else {
        document.getElementById("PermanentDistrict").value = "";
        document.getElementById("PermanentDistrict").disabled = false;

    }

}
function SetValues6() {

    var PermanentPin = $("#PermanentPincode").val();

    $.ajax({
        url: $("#hdn_postalAPI").val() + PermanentPin,
        type: "Get",
        dataType: "json",
        cache: false,
        processData: false,
        data: {
            MainProductId: MainProductId
        },
        success: function (result) {
            debugger
            var citydata = result[0].PostOffice[0].Division;
            const myArray = citydata.split(" ");
            let word = myArray[0];

            document.getElementById("customerpermanentcity").value = word;


            var statedata = result[0].PostOffice[0].State;
            document.getElementById("customerpermanentstate").value = statedata;


        }

    });



}
function Co_Applicantpermascorr() {
    var yes = document.getElementById("co_chlpercorr").checked;
    if (yes == true) {
        var PresentAddress = $("#CO_PresentAddress").val();
        var PresentPincode = $("#CO_PresentPincode").val();
        var PresentStateId = $("#Co_State").val();
        var PresentCityId = $("#CO_City").val();
        var PresentVillage = $("#CO_PresentVillage").val();
        var PresentDistrict = $("#CO_PresentDistrict").val();

        document.getElementById("CO_PermanentAddress").value = PresentAddress;
        document.getElementById("CO_PermanentPincode").value = PresentPincode;
        document.getElementById("CO_permanentstate").value = PresentStateId;
        document.getElementById("CO_PermanentVillage").value = PresentVillage;
        document.getElementById("CO_PermanentDistrict").value = PresentDistrict;
        document.getElementById("CO_permanentcity").value = PresentCityId;


        document.getElementById("CO_PermanentAddress").disabled = true;
        document.getElementById("CO_PermanentPincode").disabled = true;
        document.getElementById("CO_permanentstate").disabled = true;
        document.getElementById("CO_permanentcity").disabled = true;
        document.getElementById("CO_PermanentVillage").disabled = true;
        document.getElementById("CO_PermanentDistrict").disabled = true;

    } else {
        document.getElementById("CO_PermanentAddress").value = "";
        document.getElementById("CO_PermanentPincode").value = "";
        document.getElementById("CO_permanentstate").value = "";
        document.getElementById("CO_permanentcity").value = "";
        document.getElementById("CO_PermanentVillage").value = "";
        document.getElementById("CO_PermanentDistrict").value = "";
        document.getElementById("CO_PermanentAddress").disabled = false;
        document.getElementById("CO_PermanentPincode").disabled = false;
        document.getElementById("CO_permanentstate").disabled = false;
        document.getElementById("CO_permanentcity").disabled = false;
        document.getElementById("CO_PermanentVillage").disabled = false;
        document.getElementById("CO_PermanentDistrict").disabled = false;
    }
}
function G_Applicantpermascorr() {
    var yes = document.getElementById("G_chlpercorr").checked;
    if (yes == true) {
        debugger
        var PresentAddress = $("#G_PresentAddress").val();
        var PresentPincode = $("#G_PresentPincode").val();
        var PresentStateId = $("#G_C_State").val();
        var PresentCityId = $("#G_C_City").val();
        var PresentVillage = $("#G_PresentVillage").val();
        var PresentDistrict = $("#G_PresentDistrict").val();



        document.getElementById("G_PermanentAddress").value = PresentAddress;
        document.getElementById("G_PermanentPincode").value = PresentPincode;
        document.getElementById("G_P_State").value = PresentStateId;

        document.getElementById("G_PermanentVillage").value = PresentVillage;
        document.getElementById("G_PermanentDistrict").value = PresentDistrict;
        document.getElementById("G_P_City").value = PresentCityId;

        var stateid = PresentStateId;
        if (stateid.length == 0) {
            var s = '<option value="">- Select City -</option>'
            $('#G_P_City').html(s);
        } else {

        }
        document.getElementById("G_PermanentAddress").disabled = true;
        document.getElementById("G_PermanentPincode").disabled = true;
        document.getElementById("G_P_State").disabled = true;
        document.getElementById("G_P_City").disabled = true;
        document.getElementById("G_PermanentVillage").disabled = true;
        document.getElementById("G_PermanentDistrict").disabled = true;

    } else {
        document.getElementById("G_PermanentAddress").value = "";
        document.getElementById("G_PermanentPincode").value = "";
        document.getElementById("G_P_State").value = "";
        document.getElementById("G_P_City").value = "";
        document.getElementById("G_PermanentVillage").value = "";
        document.getElementById("G_PermanentDistrict").value = "";

        document.getElementById("G_PermanentAddress").disabled = false;
        document.getElementById("G_PermanentPincode").disabled = false;
        document.getElementById("G_P_State").disabled = false;
        document.getElementById("G_P_City").disabled = false;
        document.getElementById("G_PermanentVillage").disabled = false;
        document.getElementById("G_PermanentDistrict").disabled = false;
    }

}
function SetValues_G() {
    var yes = document.getElementById("G_chlpercorr").checked;
    if (yes == true) {
        var PresentAddress = $("#G_PresentAddress").val();

        document.getElementById("G_PermanentAddress").value = PresentAddress;

        document.getElementById("G_PermanentAddress").disabled = true;

    } else {
        document.getElementById("G_PermanentAddress").value = "";
        document.getElementById("G_PermanentAddress").disabled = false;

    }

}
function SetValues1_G() {
    var yes = document.getElementById("G_chlpercorr").checked;


    var PresentPin = $("#G_PresentPincode").val();

    $.ajax({
        url: $("#hdn_postalAPI").val() + PresentPin,
        type: "Get",
        dataType: "json",
        cache: false,
        processData: false,

        success: function (result) {
            debugger
            var citydata = result[0].PostOffice[0].Division;
            const myArray = citydata.split(" ");
            let word = myArray[0];

            document.getElementById("G_C_City").value = word;


            var statedata = result[0].PostOffice[0].State;
            document.getElementById("G_C_State").value = statedata;


        }

    });




    if (yes == true) {
        var PresentPincode = $("#G_PresentPincode").val();

        document.getElementById("G_PermanentPincode").value = PresentPincode;

        document.getElementById("G_PermanentPincode").disabled = true;

    } else {
        document.getElementById("G_PermanentPincode").value = "";
        document.getElementById("G_PermanentPincode").disabled = false;

    }

}
function SetValues2_G() {
    var yes = document.getElementById("G_chlpercorr").checked;
    if (yes == true) {
        var PresentStateId = $("#G_C_State").val();
        document.getElementById("G_P_State").value = PresentStateId;
        document.getElementById("G_P_State").disabled = true;

    } else {
        document.getElementById("G_P_State").value = "";
        document.getElementById("G_P_State").disabled = false;

    }

}
function SetValues3_G() {
    debugger
    var yes = document.getElementById("G_chlpercorr").checked;
    if (yes == true) {
        var PresentStateId = $("#G_C_State").val();
        var PresentCityId = $("#G_C_City").val();
        var stateid = PresentStateId;

        document.getElementById("G_P_City").value = PresentCityId;
        document.getElementById("G_P_City").disabled = true;

    } else {
        document.getElementById("G_P_City").value = "";
        document.getElementById("G_P_City").disabled = false;

    }

}
function SetValues4_G() {
    var yes = document.getElementById("G_chlpercorr").checked;
    if (yes == true) {
        var PresentVillage = $("#G_PresentVillage").val();

        document.getElementById("G_PermanentVillage").value = PresentVillage;

        document.getElementById("G_PermanentVillage").disabled = true;

    } else {
        document.getElementById("G_PermanentVillage").value = "";
        document.getElementById("G_PermanentVillage").disabled = false;

    }

}
function SetValues5_G() {
    var yes = document.getElementById("G_chlpercorr").checked;
    if (yes == true) {
        var PresentDistrict = $("#G_PresentDistrict").val();

        document.getElementById("G_PermanentDistrict").value = PresentDistrict;

        document.getElementById("G_PermanentDistrict").disabled = true;

    } else {
        document.getElementById("G_PermanentDistrict").value = "";
        document.getElementById("G_PermanentDistrict").disabled = false;

    }

}

function SetValues6_G() {
    debugger;
    var PermanentPin = $("#G_PermanentPincode").val();

    $.ajax({
        url: $("#hdn_postalAPI").val() + PermanentPin,
        type: "Get",
        dataType: "json",
        cache: false,
        processData: false,
        success: function (result) {
            debugger
            var citydata = result[0].PostOffice[0].Division;
            const myArray = citydata.split(" ");
            let word = myArray[0];
            document.getElementById("G_P_City").value = word;
            var statedata = result[0].PostOffice[0].State;
            document.getElementById("G_P_State").value = statedata;


        }

    });



}


//function underAgeValidate(birthday) {


//    // it will accept two types of format yyyy-mm-dd and yyyy/mm/dd
//    var optimizedBirthday = birthday.replace(/-/g, "/");

//    //set date based on birthday at 01:00:00 hours GMT+0100 (CET)
//    var myBirthday = new Date(optimizedBirthday);

//    // set current day on 01:00:00 hours GMT+0100 (CET)
//    var currentDate = new Date().toJSON().slice(0, 10) + ' 01:00:00';

//    // calculate age comparing current date and borthday
//    var myAge = ~~((Date.now(currentDate) - myBirthday) / (31557600000));

//    if (myAge < 18) {
//        return myAge;
//    } else {
//        return myAge;
//    }


//}
function GetCustomerData() {
    debugger
    var cfid = $('#CIFID').val();
    var AadharNo = $('#AadharNo').val();
    var PanNo = $('#PanNo').val();
    $.ajax({
        url: "/LeadGeneration/GetCustomerData",
        type: "Get",
        /* url: '@Url.Action("GetCity", "City")',*/
        dataType: "json",
        data: {
            cfid: cfid,
            Aadharno: AadharNo,
            PanNo: PanNo
        },
        success: function (result) {
            debugger
            var data = JSON.parse(result);
            if (data.length > 0) {
                document.getElementById("FName").value = data[0].FirstName;
                document.getElementById("LName").value = data[0].LastName;
                document.getElementById("MName").value = data[0].MiddleName;
                document.getElementById("FatherName").value = data[0].FatherName
                document.getElementById("MotherName").value = data[0].MotherName
                document.getElementById("SpouseName").value = data[0].SpouseName
                document.getElementById("Gender").value = data[0].Gender;
                document.getElementById("Dob").value = data[0].DateofBirth;
                document.getElementById("MartialStatus").value = data[0].MartialStatus;
                document.getElementById("PresentAddress").value = data[0].PresentAddress;
                document.getElementById("PresentPincode").value = data[0].PresentPincode;
                document.getElementById("customerpresentState").value = data[0].PresentStateId;
                debugger
                var stateid = data[0].PresentStateId;
                if (stateid.length == 0) {
                    var s = '<option value="">- Select City -</option>'
                    $('#customerpresentcity').html(s);
                } else {
                    var s = '<option value="">- Select City -</option>'
                    $('#customerpresentcity').html(s);
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
                            var data1 = JSON.parse(result);
                            for (var i = 0; i < data1.length; i++) {
                                var opt = new Option(data1[i].CityName, data1[i].Cityid);
                                $('#customerpresentcity').append(opt);

                            }
                            debugger
                            var ss = data[0].PresentCityId;
                            document.getElementById("customerpresentcity").value = data[0].PresentCityId;
                        }

                    });
                }
                document.getElementById("PermanentAddress").value = data[0].PermanentAddress;
                document.getElementById("PermanentPincode").value = data[0].PermanentPincode;
                document.getElementById("customerpermanentstate").value = data[0].PermanentStateId;
                document.getElementById("CibilScore").value = data[0].CibilScore;
                document.getElementById("MobileNumber1").value = data[0].MobileNo1;
                document.getElementById("FatherMobileNumber").value = data[0].FatherMobileNo;
                document.getElementById("MotherMobileNumber").value = data[0].MotherMobileNo;
                document.getElementById("SpouseMobileNumber").value = data[0].SpouseMobileNo;
                document.getElementById("EmailId").value = data[0].EmailId;
                document.getElementById("PanNo").value = data[0].PanNo;
                document.getElementById("AadharNo").value = data[0].AadharNo
                document.getElementById("hdn_customer_PanVerify").value = data[0].PanVerification
                document.getElementById("hdn_customer_aadhar_verify").value = data[0].AAdharVerification
                stateid = data[0].PermanentStateId;
                if (stateid.length == 0) {
                    var s = '<option value="">- Select City -</option>'
                    $('#customerpermanentcity').html(s);
                } else {
                    var s = '<option value="">- Select City -</option>'
                    $('#customerpermanentcity').html(s);
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
                            var data1 = JSON.parse(result);
                            for (var i = 0; i < data1.length; i++) {
                                var opt = new Option(data1[i].CityName, data1[i].Cityid);
                                $('#customerpermanentcity').append(opt);

                            }
                            debugger

                            document.getElementById("customerpermanentcity").value = data[0].PermanentCityId;
                        }

                    });
                }
            }
            else {
                swal("TDH", "No Data Found", "success");
            }


        }

    });

}
function ValidationChk() {
    debugger
    var regex = /^[a-zA-Z]*$/;
    var PANregex = /[A-Z]{5}[0-9]{4}[A-Z]{1}$/;
    var ReqType = "Insert";
    var MainProductId = $("#MainProductId option:selected").val();
    var MainProductText = $("#MainProductId option:selected").text();
    var ProductId = $("#ProductId option:selected").val();

    var Prefix = $("#Prefix :selected").text();
    var FName = $("#FName").val();
    var MName = $("#MName").val();
    var LName = $("#LName").val();
    var FatherName = $("#FatherName").val();
    var MotherName = $("#MotherName").val();
    var SpouseName = $("#SpouseName").val();
    var Dob = $("#Dob").val();
    var Gender = $("#Gender option:selected").val();

    var MartialStatus = $("#MartialStatus option:selected").val();
    var PresentAddress = $("#PresentAddress").val();
    var PresentPincode = $("#PresentPincode").val();
    var PresentStateId = $("#customerpresentState").val();
    var PresentCityId = $("#customerpresentcity").val();
    var PresentVillage = $("#PresentVillage").val();
    var PresentDistrict = $("#PresentDistrict").val();
    var PermanentAddress = $("#PermanentAddress").val();
    var PermanentPincode = $("#PermanentPincode").val();
    var PermanentStateId = $("#customerpermanentstate").val();
    var PermanentCityId = $("#customerpermanentcity").val();
    var PermanentVillage = $("#PermanentVillage").val();
    var PermanentDistrict = $("#PermanentDistrict").val();
    var CibilScore = $("#CibilScore").val();
    var MobileNumber1 = $("#MobileNumber1").val();
    /*var MobileNumber2 = $("#MobileNumber2").val();*/
    var FatherMobileNumber = $("#FatherMobileNumber").val();
    var MotherMobileNumber = $("#MotherMobileNumber").val();
    var SpouseMobileNumber = $("#SpouseMobileNumber").val();
    var SpouseMobileNumber = $("#SpouseMobileNumber").val();
    var EmailId = $("#EmailId").val();
    var CIFID = $("#CIFID").val();
    var AadharNo = $("#AadharNo").val();
    var OwnerShip = $("#ddlCustOwnerShip option:selected").val();
    var hdn_customer_aadhar_verify, hdn_customer_PanVerify;
    if ($(".btnAadharVerify").is(":disabled") == true) {
        hdn_customer_aadhar_verify = 1;
    }
    else {
        hdn_customer_aadhar_verify = $("#hdn_customer_aadhar_verify").val();
    }
    if ($(".btnPanVerify").is(":disabled") == true) {
        hdn_customer_PanVerify = 1;
    }
    else {
        hdn_customer_PanVerify = $("#hdn_customer_PanVerify").val();
    }
    var hdn_co_aadhar_verify, hdn_co_aadhar_verify;
    if ($(".btnCoAadharVerify").is(":disabled") == true) {
        hdn_co_aadhar_verify = 1;
    }
    else {
        hdn_co_aadhar_verify = $("#hdn_co_aadhar_verify").val();
    }


    if ($(".btnCoPanVerify").is(":disabled") == true) {
        hdn_co_PanVerify = 1;
    }
    else {
        hdn_co_PanVerify = $("#hdn_co_aadhar_verify").val();
    }
    var PanNo = $("#PanNo").val();
    var hdn_type = $("#hdn_type").val();


    var CenterID = $("#ddlCenter option:selected").val();
    var PLBranchID = $("#ddlBranch option:selected").val();
    if (PLBranchID.length == 0) {
        PLBranchID = 0;
    }
    if (CenterID.length == 0) {
        CenterID = 0;
    }
    //CO_Applicant Info
    var CO_Prefix = $("#CoPrefix :selected").text();
    var CO_FName = $("#CO_FName").val();
    var CO_MName = $("#CO_MName").val();
    var CO_LName = $("#CO_LName").val();
    var CO_FatherName = $("#txtCoFathName").val();
    var CO_MotherName = $("#txtCoMothName").val();
    var CO_Gender = $("#CO_Gender option:selected").val();
    var CO_Dob = $("#CO_Dob").val();
    var CO_MartialStatus = $("#CO_MartialStatus option:selected").val();
    var CO_PresentAddress = $("#CO_PresentAddress").val();
    var CO_PresentPincode = $("#CO_PresentPincode").val();
    var CO_PresentStateId = $("#Co_State").val();
    var CO_City = $("#CO_City").val();
    var CO_PresentVillage = $("#CO_PresentVillage").val();
    var CO_PresentDistrict = $("#CO_PresentDistrict").val();

    var CO_PermanentAddress = $("#CO_PermanentAddress").val();
    var CO_PermanentPincode = $("#CO_PermanentPincode").val();
    var CO_permanentstate = $("#CO_permanentstate").val();
    var CO_permanentcity = $("#CO_permanentcity").val();
    var CO_PermanentVillage = $("#CO_PermanentVillage").val();
    var CO_PermanentDistrict = $("#CO_PermanentDistrict").val();

    var CO_MObileNO = $("#CO_MObileNO").val();
    var CO_EmailId = $("#CO_EmailId").val();
    var CO_PanNo = $("#CO_PanNo").val();
    var CO_AadharNo = $("#CO_AadharNo").val();
    var CO_CibilScore = $("#CO_CibilScore").val();
    var Co_CIF = $("#txtCoCIF").val();
    var Co_OwnerShip = "";
    if (CO_FName.trim() != "") {
        Co_OwnerShip = $("#ddlCoOwnerShip option:selected").val();
    }

    //Additional Info
    var ReuestedLoanAmount = $("#ReuestedLoanAmount").val();
    var ReuestedLoanTenure = $("#ReuestedLoanTenure").val();
    var EstValueViechle = $("#EstValueViechle").val();
    var EstMonthIncome = $("#EstMonthIncome").val();
    var EstFamilyIncome = $("#txtEstFamilyIncome").val();
    var EstMonthExpense = $("#EstMonthExpense").val();
    var FORecomedAmt = 0;
    var NoofDependent = 0;
    var LoanPurpose = $("#PersonalLoanPurpose").val();
    var CurMonthObligation = $("#CurMonthObligation").val();
    var EstValueofscurity = $("#EstValueofscurity").val();
    var Propertyarea = $("#ddlPropertyArea option:selected").val();
    var PropertyType = $("#ddlPropertyType option:selected").val();
    var PropertyAddress = $("#txtPropertyAddress").val();
    var ColletralSecurityType = "";

    if (MainProductText == "Vehicle Loan" || MainProductText == "Commercial Vehicle" || MainProductText == "Two Wheeler Loan") {
        NoofDependent = $("#txtVchNoofDependent").val();
        FORecomedAmt = $("#txtFORecomedAmt").val();
    }
    else {
        NoofDependent = $("#txtNoofDependent").val();
        FORecomedAmt = $("#BusinessFORecomedAmt").val();
    }
    if (MainProductText == "Bussiness Loan") {
        ColletralSecurityType = $("#BusinessColletralSecurityType option:selected").val();
    }
    if (NoofDependent == "") {
        NoofDependent = 0;
    }
    if (FORecomedAmt == "") {
        FORecomedAmt = 0;
    }
    var ViechleNo = $("#ViechleNo").val();
    var ViechleRegYear = $("#ViechleRegYear").val();
    var MFGYear = $("#MFGYear").val();
    var ViechleModel = $("#ViechleModel").val();
    var ViechleColor = $("#ViechleColor").val();
    var ViechleCompany = $("#ViechleCompany").val();
    var ViechleOwner = $("#ViechleOwner").val();
    var RefernceName = $("#RefernceName").val();
    var RefenceMobileNo = $("#RefenceMobileNo").val();
    var RefenceRelation = $("#RefenceRelation :selected").text();

    var RefernceName1 = $("#RefernceName1").val();
    var RefenceMobileNo1 = $("#RefenceMobileNo1").val();
    var RefenceRelation1 = $("#RefenceRelation1 :selected").text();
    var UserRemarks = $("#txtRemarks").val();
    var FuelType = $("#ddlFualType :selected").val();
    var Owner = $("#ddlOwner :selected").val();
    var InsuranceStatus = $("#ddlInsuranceStatus :selected").val();
    var InsuranceType = $("#ddlInsuranceType :selected").val();
    var RegistrationDate = $("#txtRegDate").val();
    var ERikshawMaker = $("#txtERikshawMaker").val();
    var PerformaInvoice = $("#txtPerformaInvoice").val();
    var ValidityDate = $("#txtValidityDate").val();
    var ExShowRoomPrice = $("#txtExShowRoomPrice").val();
    var OnRoadPrice = $("#txtOnRoadPrice").val();
    var Insurer = $("#txtInsuar").val();
    var PolicyNo = $("#txtPolicyNo").val();
    var customers = new Array();
    var DSAId = $("#ddlDealer :selected").val();
    var RepaymentType = $("#ddlRepaymentType :selected").val();
    if (MainProductText != "Individual Loan") {
        RepaymentType = "";
    }
    if (DSAId.length == 0) {
        DSAId = "0";
    }
    debugger
    var filedata = new FormData();


    var fileUpload = $("#fileuploadapplicantimg").get(0);
    var files = fileUpload.files;
    debugger;
    if (files.length <= 0) {
        filedata.append('ApplicantImg', "");
    }
    var fileName = "";
    var extension = "";
    for (var i = 0; i < files.length; i++) {
        debugger
        fileName = document.querySelector('#fileuploadapplicantimg').value;
        extension = fileName.substring(fileName.lastIndexOf('.') + 1);
        filedata.append('ApplicantImg', files[i]);
    }

    var fileUpload1 = $("#fileuploadElectricBill").get(0);
    var files1 = fileUpload1.files;
    debugger;
    if (files1.length <= 0) {
        filedata.append('AppElectricBill', "");
    }
    var fileName1 = "";
    var extension1 = "";
    for (var i = 0; i < files1.length; i++) {
        debugger
        fileName1 = document.querySelector('#fileuploadElectricBill').value;
        extension1 = fileName1.substring(fileName.lastIndexOf('.') + 1);
        filedata.append('AppElectricBill', files1[i]);
    }

    var cofileUpload = $("#cofileuploadapplicantimg").get(0);
    var cofiles = cofileUpload.files;
    var fileName1 = "";
    var extension1 = "";
    debugger;
    if (cofiles.length <= 0) {
        filedata.append('COApplicantImg', "");
    }
    for (var i = 0; i < cofiles.length; i++) {
        fileName1 = document.querySelector('#cofileuploadapplicantimg').value;
        extension1 = fileName.substring(fileName.lastIndexOf('.') + 1);
        filedata.append('COApplicantImg', cofiles[i]);
    }

    var cofileUpload1 = $("#cofileuploadElectricBill").get(0);
    var cofiles_1 = cofileUpload1.files;
    var fileName_c = "";
    var extension1_c = "";
    debugger;
    if (cofiles_1.length <= 0) {
        filedata.append('COElectricBill', "");
    }
    for (var i = 0; i < cofiles_1.length; i++) {
        fileName_c = document.querySelector('#cofileuploadElectricBill').value;
        extension1_c = fileName_c.substring(fileName_c.lastIndexOf('.') + 1);
        filedata.append('COElectricBill', cofiles_1[i]);
    }


    debugger;

    if (MainProductId.length == 0) {
        swal("TDH", "Please Select Main Product", "error");
    }
    else if (ProductId.length == 0) {
        swal("TDH", "Please Select Product", "error");
    } else {
        if (hdn_type == "b") {
            $("#example10 TBODY TR").each(function () {
                var row = $(this);
                var customer = {};
                customer.G_Prefix = row.find("TD").eq(0).html();
                customer.G_FirstName = row.find("TD").eq(1).html();
                customer.G_MiddleName = row.find("TD").eq(2).html();
                customer.G_LastName = row.find("TD").eq(3).html();
                customer.G_Gender = row.find("TD").eq(4).html();
                customer.G_DOB = row.find("TD").eq(5).html();
                customer.G_Marital_Status = row.find("TD").eq(6).html();
                customer.G_PresentAddress = row.find("TD").eq(7).html();
                customer.G_PresentPinCode = row.find("TD").eq(8).html();
                customer.G_PresentStateId = row.find("TD").eq(9).html();
                customer.G_PresentCityId = row.find("TD").eq(10).html();
                customer.G_PresentVillage = row.find("TD").eq(11).html();
                customer.G_PresentDistrict = row.find("TD").eq(12).html();
                customer.G_PermanentAddress = row.find("TD").eq(13).html();
                customer.G_PermanentPincode = row.find("TD").eq(14).html();
                customer.G_P_State = row.find("TD").eq(15).html();
                customer.G_P_City = row.find("TD").eq(16).html();
                customer.G_PermanentVillage = row.find("TD").eq(17).html();
                customer.G_PermanentDistrict = row.find("TD").eq(18).html();
                customer.G_Mobile_No = row.find("TD").eq(19).html();
                customer.G_EmailId = row.find("TD").eq(20).html();
                customer.G_PanNo = row.find("TD").eq(21).html();
                customer.G_AadharNo = row.find("TD").eq(22).html();
                customer.G_CibilScore = row.find("TD").eq(23).html();
                /*customer.G_FilePath = row.find("TD").eq(19).html();*/
                customer.G_AadharVerify = row.find("TD").eq(24).html();
                customer.G_PanVerify = row.find("TD").eq(25).html();
                customer.G_CIF = row.find("TD").eq(26).html();
                customer.G_OwnerShip = row.find("TD").eq(27).html();
                customers.push(customer);

            });

            if (Prefix.length == 0) {
                swal("TDH", "Please Select Prefix.", "error");
            }
            else if (FName.length == 0) {
                swal("TDH", "Please enter customer first name", "error");
            }
            else if (FatherName.length == 0) {
                swal("TDH", "Please enter customer father name.", "error");
            }
            else if (MartialStatus == "Married" && SpouseName.length == 0) {
                swal("TDH", "Please enter customer spouse name.", "error");
            }
            else if (Gender.length == 0) {
                swal("TDH", "Please enter customer gender.", "error");
            }
            else if (Dob.length == 0) {
                swal("TDH", "Please enter customer dob.", "error");
            }
            else if (underAgeValidate(Dob) < 18 || underAgeValidate(Dob) > 60) {
                swal("TDH", "Customer age must be between 18 & 60 year.", "error");
            }
            else if (MartialStatus.length == 0) {
                swal("TDH", "Please enter customer material status.", "error");
            }
            else if (AadharNo.length == 0) {
                swal("TDH", "Please enter customer aadhar no.", "error");
            }
            else if (AadharNo.length != 12) {
                swal("TDH", "Customer invalid aadhar no.", "error");
            }
            else if (AadharNo.charAt(0) == "0" || AadharNo.charAt(0) == "1") {
                swal("TDH", "Customer Adhaar No would not start with 0 & 1.", "error");
            }
            else if (PanNo.length == 0) {
                swal("TDH", "Please enter customer pan no.", "error");
            }
            else if (PanNo.length != 10) {
                swal("TDH", "Customer invalid pan no.", "error");
            }
            else if (PresentAddress.length == 0) {
                swal("TDH", "Please enter customer present address.", "error");
            }
            else if (PresentPincode.length == 0) {
                swal("TDH", "Please enter customer present pincode.", "error");
            }
            else if (PresentPincode.length != 6) {
                swal("TDH", "Customer invalid present pincode.", "error");
            }
            else if (PermanentAddress.length == 0) {
                swal("TDH", "Please enter customer permanent address.", "error");
            }
            else if (PermanentPincode.length == 0) {
                swal("TDH", "Please enter customer permanent pincode.", "error");
            }
            else if (PermanentPincode.length != 6) {
                swal("TDH", "Customer invalid permanent pincode.", "error");
            }
            //else if (CibilScore.length == 0) {
            //    swal("TDH", "Please enter customer cibil score.", "error");
            //}
            //else if (CibilScore > 900 && CibilScore < 200) {
            //    swal("TDH", "Cibil score must be betweeen 200 to 900.", "error");
            //}
            else if (MobileNumber1.length == 0) {
                swal("TDH", "Please enter customer mobile number.", "error");
            }
            //else if (FatherMobileNumber.length == 0) {
            //    swal("TDH", "Please enter father mobile number.", "error");
            //}

            else if (MartialStatus == "Married" && SpouseMobileNumber.length == 0) {
                swal("TDH", "Please enter spouse mobile number.", "error");
            }

            else if (extension.length == 0) {
                debugger;
                swal("TDH", "Please select correct file customer.", "error");
            }
            else if (hdn_customer_aadhar_verify != "Y" && $(".btnAadharVerify").is(":disabled") == false) {
                debugger;
                swal("TDH", "Aadhar not verify of Customer.", "error");
            }
            else if (hdn_customer_PanVerify != "Y" && $(".btnPanVerify").is(":disabled") == false) {
                debugger
                swal("TDH", "PAN not verify of Customer.", "error");
            }
            else if (CO_Prefix.length == 0) {
                swal("TDH", "Please select co applicant prefix.", "error");
            }
            else if (CO_FName.length == 0) {
                swal("TDH", "Please enter co applicant first name.", "error");
            }
            else if (CO_FatherName.length == 0) {
                swal("TDH", "Please enter co applicant father name.", "error");
            }
            //else if (CO_MotherName.length == 0) {
            //    swal("TDH", "Please enter co applicant Mother name.", "error");
            //}

            else if (CO_Gender.length == 0) {
                swal("TDH", "Please enter co applicant gender.", "error");
            }
            else if (CO_Dob.length == 0) {
                swal("TDH", "Please enter co applicant DOB.", "error");
            }
            else if (underAgeValidate(CO_Dob) < 18 || underAgeValidate(CO_Dob) > 60) {
                swal("TDH", "Co applicant Age between 18 and 60 years.", "error");
            }
            else if (CO_MartialStatus.length == 0) {
                swal("TDH", "Please enter Co applicant matrial status.", "error");
            }
            else if (CO_PresentAddress.length == 0) {
                swal("TDH", "Please enter co applicant PresentAddress.", "error");
            }
            else if (CO_PresentPincode.length == 0) {
                swal("TDH", "Please enter co applicant PresentPincode.", "error");
            }
            else if (CO_PresentPincode.length != 6) {
                swal("TDH", "Invalid co applicant Present Pincode.", "error");
            }
            else if (CO_PresentStateId.length == 0) {
                swal("TDH", "Please enter co applicant state.", "error");
            }
            else if (CO_City.length == 0) {
                swal("TDH", "Please enter co applicant city.", "error");
            }
            else if (CO_PermanentAddress.length == 0) {
                swal("TDH", "Please enter co applicant PermanentAddress.", "error");
            }
            else if (CO_PermanentPincode.length == 0) {
                swal("TDH", "Please enter co applicant PermanentPincode.", "error");
            }
            else if (CO_PermanentPincode.length != 6) {
                swal("TDH", "Co applicant Permanent Pincode Not Valid.", "error");
            }
            else if (CO_permanentstate.length == 0) {
                swal("TDH", "Please enter co applicant state.", "error");
            }
            else if (CO_permanentcity.length == 0) {
                swal("TDH", "Please enter co applicant city.", "error");
            }
            else if (CO_MObileNO.length == 0) {
                swal("TDH", "Please enter co applicant mobile no.", "error");
            }

            else if (CO_PanNo.length != 10) {
                swal("TDH", "Please enter co applicant pan no.", "error");
            }
            else if (CO_AadharNo.length != 12) {
                swal("TDH", "Please enter co applicant Aadhar No.", "error");
            }
            else if (CO_AadharNo.charAt(0) == "0" || CO_AadharNo.charAt(0) == "1") {
                swal("TDH", "Co Applicant Adhaar No would not start with 0 & 1.", "error");
            }
            else if (hdn_co_aadhar_verify != "Y" && $(".btnCoAadharVerify").is(":disabled") == false) {
                debugger;
                swal("TDH", "Co Applicant Aadhar not verify.", "error");
            }
            else if (hdn_co_PanVerify != "Y" && $(".btnCoPanVerify").is(":disabled") == false) {
                debugger
                swal("TDH", "Co Applicant PAN not verify.", "error");
            }
            //else if (CO_CibilScore.length == 0) {
            //    swal("TDH", "Please enter co aplicant CibilScore.", "error");
            //}
            //else if (CO_CibilScore > 900 && CO_CibilScore < 200) {
            //    swal("TDH", "Co aplicant cibil score must be between 200 and 900.", "error");
            //}
            else if (extension1.length == 0) {
                swal("TDH", "Please select correct file Co Applicant.", "error");
            }

            else if (customers.length == 0) {
                swal("TDH", "Please enter gurantor details.", "error");
            }
            else if (ReuestedLoanAmount.length == 0) {
                swal("TDH", "Please enter Requested Loan Amount.", "error");
            }
            else if (ReuestedLoanTenure.length == 0) {
                swal("TDH", "Please enter Requested Loan Tenure.", "error");
            }
            else if (ReuestedLoanTenure.length > 100) {
                swal("TDH", "Invalid Requested Loan Tenure.", "error");
            }

            else if (EstMonthIncome.length == 0) {
                swal("TDH", "Please enter est monthly self income.", "error");
            }
            else if (EstFamilyIncome.length == 0) {
                swal("TDH", "Please enter est monthly family income.", "error");
            }

            else if (NoofDependent.length == 0 && (MainProductText == "Vehicle Loan" || MainProductText == "Bussiness Loan" || MainProductText == "Commercial Vehicle" || MainProductText == "Two Wheeler Loan")) {
                swal("TDH", "Please enter no of dependent person.", "error");
            }
            else if (NoofDependent.length > 10 && (MainProductText == "Vehicle Loan" || MainProductText == "Bussiness Loan" || MainProductText == "Commercial Vehicle" || MainProductText == "Two Wheeler Loan")) {
                swal("TDH", "Invalid no of dependent person.", "error");
            }
            else if (LoanPurpose.length == 0) {
                swal("TDH", "Please enter purpose of loan.", "error");
            }
            else if (MainProductText != "Individual Loan" && RefernceName.length == 0) {
                swal("TDH", "Please enter refrence person Person Name.", "error");
            }
            else if (MainProductText != "Individual Loan" && RefenceMobileNo.length != 10) {
                swal("TDH", "Invalid refrence person mobile No.", "error");
            }
            else if (MainProductText != "Individual Loan" && RefenceRelation.length == 0) {
                swal("TDH", "Please Select refrence person Relation.", "error");
            }
            else if (MainProductText != "Individual Loan" && RefernceName1.length == 0) {
                swal("TDH", "Please enter refrence 1 person name.", "error");
            }
            else if (MainProductText != "Individual Loan" && RefenceRelation1.length == 0) {
                swal("TDH", "Please enter refrence 1 with relationship.", "error");
            }
            else if (MainProductText != "Individual Loan" && RefenceMobileNo1.length == 0) {
                swal("TDH", "Please enter refrence 1 person mobile no.", "error");
            }

            else {

                AllDataArray = {
                    "ReqType": ReqType,
                    "MainProductId": MainProductId,
                    "ProductId": ProductId,
                    "Prefix": Prefix,
                    "FirstName": FName,
                    "MiddleName": MName,
                    "LastName": LName,
                    "FatherName": FatherName,
                    "MotherName": MotherName,
                    "SpouseName": SpouseName,
                    "Gender": Gender,
                    "DateofBirth": Dob,
                    "MartialStatus": MartialStatus,
                    "PresentAddress": PresentAddress,
                    "PresentPincode": PresentPincode,
                    "PresentStateId": PresentStateId,
                    "PresentCityId": PresentCityId,
                    "PresentVillage": PresentVillage,
                    "PresentDistrict": PresentDistrict,

                    "PermanentAddress": PermanentAddress,
                    "PermanentPincode": PermanentPincode,
                    "PermanentStateId": PermanentStateId,
                    "PermanentCityId": PermanentCityId,
                    "PermanentVillage": PermanentVillage,
                    "PermanentDistrict": PermanentDistrict,
                    "CibilScore": CibilScore,
                    "MobileNo1": MobileNumber1,

                    /*"MobileNo2": MobileNumber2,*/
                    "FatherMobileNo": FatherMobileNumber,
                    "MotherMobileNo": MotherMobileNumber,
                    "SpouseMobileNo": SpouseMobileNumber,
                    "AadharNo": AadharNo,
                    "PanNo": PanNo,
                    "CompanyId": CompanyID,
                    "Hdn_type": hdn_type,
                    "EmailId": EmailId,
                    "CIFID": CIFID,
                    "AAdharverfiy": hdn_customer_aadhar_verify,
                    "OwnerShip": OwnerShip,
                    "CO_Prefix": CO_Prefix,
                    "CO_FirstName": CO_FName,
                    "CO_MiddleName": CO_MName,
                    "CO_LastName": CO_LName,
                    "CO_DOB": CO_Dob,
                    "CO_Marital_Status": CO_MartialStatus,
                    "CO_Gender": CO_Gender,
                    "CO_PresentAddress": CO_PresentAddress,
                    "CO_PresentPinCode": CO_PresentPincode,
                    "CO_PresentStateId": CO_PresentStateId,
                    "CO_PresentCityId": CO_City,
                    "CO_PresentVillage": CO_PresentVillage,
                    "CO_PresentDistrict": CO_PresentDistrict,

                    "CO_PermanentAddress": CO_PermanentAddress,
                    "CO_PermanentPincode": CO_PermanentPincode,
                    "CO_PermanentStateId": CO_permanentstate,
                    "CO_PermanentCityId": CO_permanentcity,
                    "CO_PermanentVillage": CO_PermanentVillage,
                    "CO_PermanentDistrict": CO_PermanentDistrict,
                    "CO_Mobile_No": CO_MObileNO,
                    "CO_Email_Id": CO_EmailId,
                    "CO_PAN": CO_PanNo,
                    "CO_Adhaar": CO_AadharNo,
                    "CO_CIBIL": CO_CibilScore,
                    "Co_CIF": Co_CIF,
                    "CO_AAdharverfiy": hdn_co_aadhar_verify,
                    "CO_Panverfiy": hdn_co_PanVerify,
                    "Co_CIF": Co_CIF,
                    "Co_OwnerShip": Co_OwnerShip,
                    "ReuestedLoanAmount": ReuestedLoanAmount,
                    "ReuestedLoanTenure": ReuestedLoanTenure,
                    "EstValueViechle": EstValueViechle,
                    "EstMonthIncome": EstMonthIncome,
                    "EstFamilyIncome": EstFamilyIncome,
                    "EstMonthExpense": EstMonthExpense,
                    "CurMonthObligation": CurMonthObligation,
                    "FORecomedAmt": FORecomedAmt,
                    "NoofDependent": NoofDependent,
                    "ViechleNo": ViechleNo,
                    "ViechleRegYear": ViechleRegYear,
                    "MFGYear": MFGYear,
                    "ViechleModel": ViechleModel,
                    "ViechleColor": ViechleColor,
                    "ViechleCompany": ViechleCompany,
                    "ViechleOwner": ViechleOwner,
                    "RefernceName": RefernceName,
                    "RefenceMobileNo": RefenceMobileNo,
                    "RefenceRelation": RefenceRelation,
                    "RefernceName1": RefernceName1,
                    "RefenceMobileNo1": RefenceMobileNo1,
                    "RefenceRelation1": RefenceRelation1,
                    "LoanPurpose": LoanPurpose,
                    "ColletralSecurityType": ColletralSecurityType,
                    "EstValueofscurity": EstValueofscurity,
                    "Propertyarea": Propertyarea,
                    "PropertyType": PropertyType,
                    "PropertyAddress": PropertyAddress,
                    "CenterId": CenterID,
                    "PLLoanBranch": PLBranchID,
                    "UserRemarks": UserRemarks,
                    "FuelType": FuelType,
                    "Owner": Owner,
                    "InsuranceStatus": InsuranceStatus,
                    "InsuranceType": InsuranceType,
                    "RegistrationDate": RegistrationDate,
                    "ValidityDate": ValidityDate,
                    "ExShowRoomPrice": ExShowRoomPrice,
                    "OnRoadPrice": OnRoadPrice,
                    "Insurer": Insurer,
                    "PolicyNo": PolicyNo,
                    "PerformaInvoice": PerformaInvoice,
                    "ERikshawMaker": ERikshawMaker,
                    "DSAId": DSAId,
                    "RepaymentType": RepaymentType,
                    "CO_FatherName": CO_FatherName,
                    "CO_MotherName": CO_MotherName
                }

                filedata.append('AllDataArray', JSON.stringify(AllDataArray));
                filedata.append('Gurantor_Details', JSON.stringify(customers));
                $.ajax({
                    url: "/LeadGeneration/AddRequestLeadGeneration",
                    type: "POST",
                    contentType: false,
                    processData: false,
                    data: filedata,

                    success: function (result) {
                        debugger

                        var message = JSON.parse(result)[0].ReturnMessage;

                        if (message == "Lead saved succussfully") {
                            swal("TDH", message + " Your Lead No: " + JSON.parse(result)[0].LeadNo, "success");
                            window.location.reload();
                        } else {
                            swal("TDH", "Not Save", "error");

                        }
                    }
                })
            }

        }
        else if (hdn_type == "c") {

            if (Prefix.length == 0) {
                swal("TDH", "Please Select Prefix.", "error");
            }
            else if (FName.length == 0) {
                swal("TDH", "Please enter customer first name", "error");
            }
            else if (FatherName.length == 0) {
                swal("TDH", "Please enter customer father name.", "error");
            }
            else if (MartialStatus == "Married" && SpouseName.length == 0) {
                swal("TDH", "Please enter customer spouse name.", "error");
            }
            else if (Gender.length == 0) {
                swal("TDH", "Please enter customer gender.", "error");
            }
            else if (Dob.length == 0) {
                swal("TDH", "Please enter customer dob.", "error");
            }
            else if (underAgeValidate(Dob) < 18 || underAgeValidate(Dob) > 60) {
                swal("TDH", "Customer age must be between Age 18 and 60.", "error");
            }
            else if (MartialStatus.length == 0) {
                swal("TDH", "Please enter customer material status.", "error");
            }
            else if (AadharNo.length == 0) {
                swal("TDH", "Please enter customer aadhar no.", "error");
            }
            else if (AadharNo.length != 12) {
                swal("TDH", "Customer invalid aadhar no.", "error");
            }
            else if (AadharNo.charAt(0) == "0" || AadharNo.charAt(0) == "1") {
                swal("TDH", "Customer Adhaar No would not start with 0 & 1.", "error");
            }
            else if (PanNo.length == 0) {
                swal("TDH", "Please enter customer pan no.", "error");
            }
            else if (PanNo.length != 10) {
                swal("TDH", "Customer invalid pan no.", "error");
            }
            else if (PresentAddress.length == 0) {
                swal("TDH", "Please enter customer present address.", "error");
            }
            else if (PresentPincode.length == 0) {
                swal("TDH", "Please enter customer present pincode.", "error");
            }
            else if (PresentPincode.length != 6) {
                swal("TDH", "Customer invalid present pincode.", "error");
            }
            else if (PermanentAddress.length == 0) {
                swal("TDH", "Please enter customer permanent address.", "error");
            }
            else if (PermanentPincode.length == 0) {
                swal("TDH", "Please enter customer permanent pincode.", "error");
            }
            else if (PermanentPincode.length != 6) {
                swal("TDH", "Customer invalid permanent pincode.", "error");
            }
            //else if (CibilScore.length == 0) {
            //    swal("TDH", "Please enter customer cibil score.", "error");
            //}
            //else if (CibilScore > 900 && CibilScore < 200) {
            //    swal("TDH", "Cibil score must be betweeen 200 to 900.", "error");
            //}
            else if (MobileNumber1.length == 0) {
                swal("TDH", "Please enter customer mobile number.", "error");
            }
            //else if (FatherMobileNumber.length == 0) {
            //    swal("TDH", "Please enter father mobile number.", "error");
            //}
            else if (MartialStatus == "Married" && SpouseMobileNumber.length == 0) {
                swal("TDH", "Please enter spouse mobile number.", "error");
            }

            else if (extension.length == 0) {
                debugger;
                swal("TDH", "Please select customer Images.", "error");
            }
            else if (hdn_customer_aadhar_verify != "Y" && $(".btnAadharVerify").is(":disabled") == false) {
                debugger;
                swal("TDH", "Aadhar not verify of Customer.", "error");
            }
            else if (hdn_customer_PanVerify != "Y" && $(".btnPanVerify").is(":disabled") == false) {
                debugger
                swal("TDH", "PAN not verify of Customer.", "error");
            }
            else if (CO_Prefix.length == 0) {
                swal("TDH", "Please select co applicant prefix.", "error");
            }
            else if (CO_FName.length == 0) {
                swal("TDH", "Please enter co applicant first name.", "error");
            }
            else if (CO_FatherName.length == 0) {
                swal("TDH", "Please enter co applicant father's name.", "error");
            }
            //else if (CO_MotherName.length == 0) {
            //    swal("TDH", "Please enter co applicant mother's name.", "error");
            //}
            
            else if (CO_Gender.length == 0) {
                swal("TDH", "Please enter co applicant gender.", "error");
            }
            else if (CO_Dob.length == 0) {
                swal("TDH", "Please enter co applicant DOB.", "error");
            }
            else if (underAgeValidate(CO_Dob) < 18 || underAgeValidate(CO_Dob) > 60) {
                swal("TDH", "Co applicant Age between 18 && 60 years.", "error");
            }
            else if (CO_MartialStatus.length == 0) {
                swal("TDH", "Please enter Co Applicant matrial status.", "error");
            }
            else if (CO_PresentAddress.length == 0) {
                swal("TDH", "Please enter co applicant PresentAddress.", "error");
            }
            else if (CO_PresentPincode.length == 0) {
                swal("TDH", "Please enter co applicant PresentPincode.", "error");
            }
            else if (CO_PresentPincode.length != 6) {
                swal("TDH", "Invalid co applicant Present Pincode.", "error");
            }
            else if (CO_PresentStateId.length == 0) {
                swal("TDH", "Please enter co applicant state.", "error");
            }
            else if (CO_City.length == 0) {
                swal("TDH", "Please enter co applicant city.", "error");
            }
            else if (CO_PermanentAddress.length == 0) {
                swal("TDH", "Please enter co applicant PermanentAddress.", "error");
            }
            else if (CO_PermanentPincode.length == 0) {
                swal("TDH", "Please enter co applicant PermanentPincode.", "error");
            }
            else if (CO_PermanentPincode.length != 6) {
                swal("TDH", "Co applicant Permanent Pincode Not Valid.", "error");
            }
            else if (CO_permanentstate.length == 0) {
                swal("TDH", "Please enter co applicant state.", "error");
            }
            else if (CO_permanentcity.length == 0) {
                swal("TDH", "Please enter co applicant city.", "error");
            }
            else if (CO_MObileNO.length == 0) {
                swal("TDH", "Please enter co applicant mobile no.", "error");
            }

            else if (CO_PanNo.length != 10) {
                swal("TDH", "Please enter co applicant pan no.", "error");
            }
            else if (CO_AadharNo.length != 12) {
                swal("TDH", "Please enter co applicant Aadhar No.", "error");
            }
            else if (CO_AadharNo.charAt(0) == "0" || CO_AadharNo.charAt(0) == "1") {
                swal("TDH", "Co applicant Adhaar No would not start with 0 & 1.", "error");
            }
            else if (hdn_co_aadhar_verify != "Y" && $(".btnCoAadharVerify").is(":disabled") == false) {
                debugger;
                swal("TDH", "Co Applicant Aadhar not verify.", "error");
            }
            else if (hdn_co_PanVerify != "Y" && $(".btnCoPanVerify").is(":disabled") == false) {
                debugger
                swal("TDH", "Co Applicant PAN not verify.", "error");
            }
            //else if (CO_CibilScore.length == 0) {
            //    swal("TDH", "Please enter co aplicant CibilScore.", "error");
            //}
            //else if (CO_CibilScore > 900 && CO_CibilScore < 200) {
            //    swal("TDH", "Co aplicant cibil score must be between 200 and 900.", "error");
            //}
            else if (extension1.length == 0) {
                swal("TDH", "Please select Co Applicant Images.", "error");
            }
            else if (ReuestedLoanAmount.length == 0) {
                swal("TDH", "Please enter Requested Loan Amount.", "error");
            } else if (ReuestedLoanTenure.length == 0) {
                swal("TDH", "Please enter Requested Loan Tenure.", "error");
            }
            else if (ReuestedLoanTenure.length > 100) {
                swal("TDH", "Invalid Requested Loan Tenure.", "error");
            }
            else if (MainProductText != "Individual Loan" && RefernceName.length == 0) {
                swal("TDH", "Please enter refrence person Person Name.", "error");
            }
            else if (MainProductText != "Individual Loan" && RefenceMobileNo.length != 10) {
                swal("TDH", "Invalid refrence person mobile No.", "error");
            }
            else if (MainProductText != "Individual Loan" && RefenceRelation.length == 0) {
                swal("TDH", "Please Select refrence person Relation.", "error");
            }
            else if (MainProductText != "Individual Loan" && RefernceName1.length == 0) {
                swal("TDH", "Please enter refrence 1 person name.", "error");
            }
            else if (MainProductText != "Individual Loan" && RefenceRelation1.length == 0) {
                swal("TDH", "Please enter refrence 1 with relationship.", "error");
            }
            else if (MainProductText != "Individual Loan" && RefenceMobileNo1.length == 0) {
                swal("TDH", "Please enter refrence 1 person mobile no.", "error");
            }
            else if (MainProductText != "Individual Loan" && RefenceMobileNo1.length != 10) {
                swal("TDH", "Invalid refrence 1 person mobile no.", "error");
            }

            else if (EstMonthIncome.length == 0) {
                swal("TDH", "Please enter est month self income.", "error");
            }
            else if (EstFamilyIncome.length == 0) {
                swal("TDH", "Please enter est month family income.", "error");
            }

            else if (NoofDependent.length == 0 && (MainProductText == "Vehicle Loan" || MainProductText == "Bussiness Loan" || MainProductText == "Commercial Vehicle" || MainProductText == "Two Wheeler Loan")) {
                swal("TDH", "Please enter no of dependent person.", "error");

            }
            else if (NoofDependent.length > 10 && (MainProductText == "Vehicle Loan" || MainProductText == "Bussiness Loan" || MainProductText == "Commercial Vehicle" || MainProductText == "Two Wheeler Loan")) {
                swal("TDH", "Invalid no of dependent person.", "error");
            }
            else if (LoanPurpose.length == 0 && (MainProductText == "Individual Loan" || MainProductText == "Bussiness Loan" || MainProductText == "Commercial Vehicle" || MainProductText == "Two Wheeler Loan")) {
                swal("TDH", "Please enter purpose of loan.", "error");
            }
            else {

                AllDataArray = {
                    "ReqType": ReqType,
                    "MainProductId": MainProductId,
                    "ProductId": ProductId,
                    "Prefix": Prefix,
                    "FirstName": FName,
                    "MiddleName": MName,
                    "LastName": LName,
                    "FatherName": FatherName,
                    "MotherName": MotherName,
                    "SpouseName": SpouseName,
                    "Gender": Gender,
                    "DateofBirth": Dob,
                    "MartialStatus": MartialStatus,
                    "PresentAddress": PresentAddress,
                    "PresentPincode": PresentPincode,
                    "PresentStateId": PresentStateId,
                    "PresentCityId": PresentCityId,
                    "PresentVillage": PresentVillage,
                    "PresentDistrict": PresentDistrict,
                    "PermanentAddress": PermanentAddress,
                    "PermanentPincode": PermanentPincode,
                    "PermanentStateId": PermanentStateId,
                    "PermanentCityId": PermanentCityId,
                    "PermanentVillage": PermanentVillage,
                    "PermanentDistrict": PermanentDistrict,
                    "CibilScore": CibilScore,
                    "OwnerShip": OwnerShip,
                    "MobileNo1": MobileNumber1,

                    /*"MobileNo2": MobileNumber2,*/
                    "FatherMobileNo": FatherMobileNumber,
                    "MotherMobileNo": MotherMobileNumber,
                    "SpouseMobileNo": SpouseMobileNumber,
                    "AadharNo": AadharNo,
                    "PanNo": PanNo,
                    "CompanyId": CompanyID,
                    "Hdn_type": hdn_type,
                    "EmailId": EmailId,
                    "CIFID": CIFID,
                    "AAdharverfiy": hdn_customer_aadhar_verify,
                    "CO_Prefix": CO_Prefix,
                    "CO_FirstName": CO_FName,
                    "CO_MiddleName": CO_MName,
                    "CO_LastName": CO_LName,
                    "CO_DOB": CO_Dob,
                    "CO_Marital_Status": CO_MartialStatus,
                    "CO_Gender": CO_Gender,
                    "CO_PresentAddress": CO_PresentAddress,
                    "CO_PresentPinCode": CO_PresentPincode,
                    "CO_PresentStateId": CO_PresentStateId,
                    "CO_PresentCityId": CO_City,
                    "CO_PresentVillage": CO_PresentVillage,
                    "CO_PresentDistrict": CO_PresentDistrict,

                    "CO_PermanentAddress": CO_PermanentAddress,
                    "CO_PermanentPincode": CO_PermanentPincode,
                    "CO_PermanentStateId": CO_permanentstate,
                    "CO_PermanentCityId": CO_permanentcity,
                    "CO_PermanentVillage": CO_PermanentVillage,
                    "CO_PermanentDistrict": CO_PermanentDistrict,
                    "CO_Mobile_No": CO_MObileNO,
                    "CO_Email_Id": CO_EmailId,
                    "CO_PAN": CO_PanNo,
                    "CO_Adhaar": CO_AadharNo,
                    "CO_CIBIL": CO_CibilScore,
                    "Co_CIF": Co_CIF,
                    "Co_OwnerShip": Co_OwnerShip,
                    "ReuestedLoanAmount": ReuestedLoanAmount,
                    "ReuestedLoanTenure": ReuestedLoanTenure,
                    "EstValueViechle": EstValueViechle,
                    "EstMonthIncome": EstMonthIncome,
                    "EstFamilyIncome": EstFamilyIncome,
                    "EstMonthExpense": EstMonthExpense,
                    "CurMonthObligation": CurMonthObligation,
                    "FORecomedAmt": FORecomedAmt,
                    "NoofDependent": NoofDependent,
                    "ViechleNo": ViechleNo,
                    "ViechleRegYear": ViechleRegYear,
                    "MFGYear": MFGYear,
                    "ViechleModel": ViechleModel,
                    "ViechleColor": ViechleColor,
                    "ViechleCompany": ViechleCompany,
                    "ViechleOwner": ViechleOwner,
                    "RefernceName": RefernceName,
                    "RefenceMobileNo": RefenceMobileNo,
                    "RefenceRelation": RefenceRelation,
                    "RefernceName1": RefernceName1,
                    "RefenceMobileNo1": RefenceMobileNo1,
                    "RefenceRelation1": RefenceRelation1,
                    "LoanPurpose": LoanPurpose,
                    "ColletralSecurityType": ColletralSecurityType,
                    "EstValueofscurity": EstValueofscurity,
                    "Propertyarea": Propertyarea,
                    "PropertyType": PropertyType,
                    "PropertyAddress": PropertyAddress,
                    "CenterId": CenterID,
                    "PLLoanBranch": PLBranchID,
                    "UserRemarks": UserRemarks,
                    "FuelType": FuelType,
                    "Owner": Owner,
                    "InsuranceStatus": InsuranceStatus,
                    "InsuranceType": InsuranceType,
                    "RegistrationDate": RegistrationDate,
                    "ValidityDate": ValidityDate,
                    "ExShowRoomPrice": ExShowRoomPrice,
                    "OnRoadPrice": OnRoadPrice,
                    "Insurer": Insurer,
                    "PolicyNo": PolicyNo,
                    "PerformaInvoice": PerformaInvoice,
                    "ERikshawMaker": ERikshawMaker,
                    "DSAId": DSAId,
                    "RepaymentType": RepaymentType,
                    "CO_FatherName": CO_FatherName,
                    "CO_MotherName": CO_MotherName
                }

                filedata.append('AllDataArray', JSON.stringify(AllDataArray));
                filedata.append('Gurantor_Details', JSON.stringify(customers));
                $.ajax({
                    url: "/LeadGeneration/AddRequestLeadGeneration",
                    type: "POST",
                    contentType: false,
                    processData: false,
                    data: filedata,

                    success: function (result) {
                        debugger

                        var message = JSON.parse(result)[0].ReturnMessage;

                        if (message == "Lead saved succussfully") {
                            swal("TDH", message + " Your Lead No: " + JSON.parse(result)[0].LeadNo, "success");
                            window.location.reload();
                        } else {
                            swal("TDH", "Not Save", "error");

                        }



                    }



                })

            }


        }
        else if (hdn_type == "g") {
            $("#example10 TBODY TR").each(function () {
                var row = $(this);
                var customer = {};
                customer.G_Prefix = row.find("TD").eq(0).html();
                customer.G_FirstName = row.find("TD").eq(1).html();
                customer.G_MiddleName = row.find("TD").eq(2).html();
                customer.G_LastName = row.find("TD").eq(3).html();
                customer.G_Gender = row.find("TD").eq(4).html();
                customer.G_DOB = row.find("TD").eq(5).html();
                customer.G_Marital_Status = row.find("TD").eq(6).html();
                customer.G_PresentAddress = row.find("TD").eq(7).html();
                customer.G_PresentPinCode = row.find("TD").eq(8).html();
                customer.G_PresentStateId = row.find("TD").eq(9).html();
                customer.G_PresentCityId = row.find("TD").eq(10).html();
                customer.G_PresentVillage = row.find("TD").eq(11).html();
                customer.G_PresentDistrict = row.find("TD").eq(12).html();
                customer.G_PermanentAddress = row.find("TD").eq(13).html();
                customer.G_PermanentPincode = row.find("TD").eq(14).html();
                customer.G_P_State = row.find("TD").eq(15).html();
                customer.G_P_City = row.find("TD").eq(16).html();
                customer.G_PermanentVillage = row.find("TD").eq(17).html();
                customer.G_PermanentDistrict = row.find("TD").eq(18).html();
                customer.G_Mobile_No = row.find("TD").eq(19).html();
                customer.G_EmailId = row.find("TD").eq(20).html();
                customer.G_PanNo = row.find("TD").eq(21).html();
                customer.G_AadharNo = row.find("TD").eq(22).html();
                customer.G_CibilScore = row.find("TD").eq(23).html();
                /*customer.G_FilePath = row.find("TD").eq(19).html();*/
                customer.G_AadharVerify = row.find("TD").eq(24).html();
                customer.G_PanVerify = row.find("TD").eq(25).html();
                customer.G_CIF = row.find("TD").eq(26).html();
                customer.G_OwnerShip = row.find("TD").eq(27).html();
                customers.push(customer);
            });
            if (Prefix.length == 0) {
                swal("TDH", "Please select prefix", "error");

            }
            else if (FName.length == 0) {
                swal("TDH", "Please Enter Customer First Name.", "error");
            }
            else if (FatherName.length == 0) {
                swal("TDH", "Please Enter Customer Father Name.", "error");
            }
            else if (MartialStatus == "Married" && SpouseName.length == 0) {
                swal("TDH", "Please Enter Customer Spouse Name.", "error");
            }
            else if (Gender.length == 0) {
                swal("TDH", "Please Select Customer Gender.", "error");

            }
            else if (Dob.length == 0) {
                swal("TDH", "Please Select Customer Date of Birth.", "error");
            }
            else if (underAgeValidate(Dob) < 18 || underAgeValidate(Dob) > 60) {
                swal("TDH", "Customer Age must be between 18 & 60 year.", "error");
            }
            else if (MartialStatus.length == 0) {
                swal("TDH", "Please enter customer material status.", "error");
            }
            else if (AadharNo.length == 0) {
                swal("TDH", "Please enter customer Aadhar No.", "error");
            }
            else if (AadharNo.length != 12) {
                swal("TDH", "Customer invalid aadhar no.", "error");
            }
            else if (AadharNo.charAt(0) == "0" || AadharNo.charAt(0) == "1") {
                swal("TDH", "Customer Adhaar No would not start with 0 & 1.", "error");
            }
            else if (PanNo.length == 0) {
                swal("TDH", "Please enter customer Pan No.", "error");
            }
            else if (PanNo.length != 10) {
                swal("TDH", "Customer invalid pan no.", "error");
            }
            else if (PresentAddress.length == 0) {
                swal("TDH", "Please enter customer present address.", "error");
            }
            else if (PresentPincode.length == 0) {
                swal("TDH", "Please enter customer present pincode.", "error");
            }
            else if (PresentPincode.length != 6) {
                swal("TDH", "Customer invalid present pincode.", "error");
            }
            else if (PermanentAddress.length == 0) {
                swal("TDH", "Please enter customer permanent address.", "error");
            }
            else if (PermanentPincode.length == 0) {
                swal("TDH", "Please enter customer permanent pincode.", "error");
            }
            else if (PermanentPincode.length != 6) {
                swal("TDH", "Customer invalid permanent pincode.", "error");
            }
            else if (CibilScore.length == 0) {
                swal("TDH", "Please enter customer cibil score.", "error");
            }
            else if (CibilScore > 900 && CibilScore < 200) {
                swal("TDH", "Cibil score must be betweeen 200 to 900.", "error");
            }
            else if (MobileNumber1.length == 0) {
                swal("TDH", "Please enter customer mobile number.", "error");
            }
            //else if (FatherMobileNumber.length == 0) {
            //    swal("TDH", "Please enter father mobile number.", "error");
            //}

            else if (MartialStatus == "Married" && SpouseMobileNumber.length == 0) {
                swal("TDH", "Please enter spouse mobile number.", "error");
            }

            else if (extension.length == 0) {
                swal("TDH", "Please select correct file customer.", "error");
            }
            else if (hdn_customer_aadhar_verify != "Y" && $(".btnAadharVerify").is(":disabled") == false) {
                debugger
                swal("TDH", "Aadhar not verify of Customer.", "error");
            }
            else if (hdn_customer_PanVerify != "Y" && $(".btnPanVerify").is(":disabled") == false) {
                debugger
                swal("TDH", "PAN not verify of Customer.", "error");
            }
            else if (customers.length == 0) {
                swal("TDH", "Please enter gurantor details.", "error");
            }
            else if (ReuestedLoanAmount.length == 0) {
                swal("TDH", "Please enter Requested Loan Amount.", "error");
            }
            else if (ReuestedLoanTenure.length == 0) {
                swal("TDH", "Please enter Requested Loan Tenure.", "error");
            }
            else if (ReuestedLoanTenure.length > 100) {
                swal("TDH", "Invalid Requested Loan Tenure.", "error");
            }
            else if (MainProductText != "Individual Loan" && RefernceName.length == 0) {
                swal("TDH", "Please enter refrence person Person Name.", "error");
            }
            else if (MainProductText != "Individual Loan" && RefenceMobileNo.length != 10) {
                swal("TDH", "Invalid refrence person mobile No.", "error");
            }
            else if (MainProductText != "Individual Loan" && RefenceRelation.length == 0) {
                swal("TDH", "Please Select refrence person Relation.", "error");
            }
            else if (MainProductText != "Individual Loan" && RefernceName1.length == 0) {
                swal("TDH", "Please enter refrence 1 person name.", "error");
            }
            else if (MainProductText != "Individual Loan" && RefenceRelation1.length == 0) {
                swal("TDH", "Please enter refrence 1 with relationship.", "error");
            }
            else if (RefenceRelation1.length == 0) {
                swal("TDH", "Please enter refrence1 person with relationship", "error");
            }
            else if (MainProductText != "Individual Loan" && RefenceRelation1.length == 0) {
                swal("TDH", "Please enter refrence 1 with relationship.", "error");
            }
            else if (MainProductText != "Individual Loan" && RefenceMobileNo1.length == 0) {
                swal("TDH", "Please enter refrence 1 person mobile no.", "error");
            }
            else if (EstMonthIncome.length == 0) {
                swal("TDH", "Please enter est month self income", "error");
            }
            else if (EstFamilyIncome.length == 0) {
                swal("TDH", "Please enter est month family income", "error");
            }


            else if (EstMonthExpense.length == 0) {
                swal("TDH", "Please enter est. month expense", "error");
            }

            else if (NoofDependent.length == 0 && (MainProductText == "Vehicle Loan" || MainProductText == "Bussiness Loan" || MainProductText == "Commercial Vehicle" || MainProductText == "Two Wheeler Loan")) {
                swal("TDH", "Please enter no of dependent person", "error");
            }
            else if (NoofDependent.length > 10 && (MainProductText == "Vehicle Loan" || MainProductText == "Bussiness Loan" || MainProductText == "Commercial Vehicle" || MainProductText == "Two Wheeler Loan")) {
                swal("TDH", "Invalid no of dependent person", "error");
            }
            else if (LoanPurpose.length == 0 && (MainProductText == "Individual Loan" || MainProductText == "Bussiness Loan" || MainProductText == "Commercial Vehicle" || MainProductText == "Two Wheeler Loan")) {
                swal("TDH", "Please enter purpose of loan", "error");
            }
            else if (MainProductText.toUpperCase() == "INDIVIDUAL LOAN" && (CenterID.length == 0 || PLBranchID.length == 0)) {

                swal("TDH", "Please Select Branch and Center in case of Individual Loan.", "error");
            }
            else {


                AllDataArray = {
                    "ReqType": ReqType,
                    "MainProductId": MainProductId,
                    "ProductId": ProductId,
                    "Prefix": Prefix,
                    "FirstName": FName,
                    "MiddleName": MName,
                    "LastName": LName,
                    "FatherName": FatherName,
                    "MotherName": MotherName,
                    "SpouseName": SpouseName,
                    "Gender": Gender,
                    "DateofBirth": Dob,
                    "MartialStatus": MartialStatus,
                    "PresentAddress": PresentAddress,
                    "PresentPincode": PresentPincode,
                    "PresentStateId": PresentStateId,
                    "PresentCityId": PresentCityId,
                    "PresentVillage": PresentVillage,
                    "PresentDistrict": PresentDistrict,
                    "PermanentAddress": PermanentAddress,
                    "PermanentPincode": PermanentPincode,
                    "PermanentStateId": PermanentStateId,
                    "PermanentCityId": PermanentCityId,
                    "PermanentVillage": PermanentVillage,
                    "PermanentDistrict": PermanentDistrict,
                    "CibilScore": CibilScore,
                    "OwnerShip": OwnerShip,
                    "MobileNo1": MobileNumber1,

                    /*"MobileNo2": MobileNumber2,*/
                    "FatherMobileNo": FatherMobileNumber,
                    "MotherMobileNo": MotherMobileNumber,
                    "SpouseMobileNo": SpouseMobileNumber,
                    "AadharNo": AadharNo,
                    "PanNo": PanNo,
                    "CompanyId": CompanyID,
                    "Hdn_type": hdn_type,
                    "EmailId": EmailId,
                    "CIFID": CIFID,
                    "CenterId": CenterID,
                    "PLLoanBranch": PLBranchID,
                    "AAdharverfiy": hdn_customer_aadhar_verify,


                    "CO_Prefix": "",
                    "CO_FirstName": "",
                    "CO_MiddleName": "",
                    "CO_LastName": "",
                    "CO_DOB": "",
                    "CO_Marital_Status": "",
                    "CO_Gender": "",
                    "CO_PresentAddress": "",
                    "CO_PresentPinCode": "",
                    "CO_PresentStateId": 0,
                    "CO_PresentCityId": 0,
                    "CO_PresentVillage": "",
                    "CO_PresentDistrict": "",
                    "CO_PermanentAddress": "",
                    "CO_PermanentPincode": "",
                    "CO_PermanentStateId": 0,
                    "CO_PermanentCityId": 0,
                    "CO_PermanentVillage": "",
                    "CO_PermanentDistrict": "",
                    "CO_Mobile_No": "",
                    "CO_Email_Id": "",
                    "CO_PAN": "",
                    "CO_Adhaar": "",
                    "CO_CIBIL": "",
                    "Co_OwnerShip": "",

                    "ReuestedLoanAmount": ReuestedLoanAmount,
                    "ReuestedLoanTenure": ReuestedLoanTenure,
                    "EstValueViechle": EstValueViechle,
                    "EstMonthIncome": EstMonthIncome,
                    "EstFamilyIncome": EstFamilyIncome,
                    "EstMonthExpense": EstMonthExpense,
                    "CurMonthObligation": CurMonthObligation,
                    "FORecomedAmt": FORecomedAmt,
                    "NoofDependent": NoofDependent,
                    "ViechleNo": ViechleNo,
                    "ViechleRegYear": ViechleRegYear,
                    "MFGYear": MFGYear,
                    "ViechleModel": ViechleModel,
                    "ViechleColor": ViechleColor,
                    "ViechleCompany": ViechleCompany,
                    "ViechleOwner": ViechleOwner,
                    "RefernceName": RefernceName,
                    "RefenceMobileNo": RefenceMobileNo,
                    "RefenceRelation": RefenceRelation,
                    "RefernceName1": RefernceName1,
                    "RefenceMobileNo1": RefenceMobileNo1,
                    "RefenceRelation1": RefenceRelation1,
                    "LoanPurpose": LoanPurpose,
                    "ColletralSecurityType": ColletralSecurityType,
                    "EstValueofscurity": EstValueofscurity,
                    "Propertyarea": Propertyarea,
                    "PropertyType": PropertyType,
                    "PropertyAddress": PropertyAddress,
                    "CenterId": CenterID,
                    "PLLoanBranch": PLBranchID,
                    "UserRemarks": UserRemarks,
                    "FuelType": FuelType,
                    "Owner": Owner,
                    "InsuranceStatus": InsuranceStatus,
                    "InsuranceType": InsuranceType,
                    "RegistrationDate": RegistrationDate,
                    "ValidityDate": ValidityDate,
                    "ExShowRoomPrice": ExShowRoomPrice,
                    "OnRoadPrice": OnRoadPrice,
                    "Insurer": Insurer,
                    "PolicyNo": PolicyNo,
                    "PerformaInvoice": PerformaInvoice,
                    "ERikshawMaker": ERikshawMaker,
                    "DSAId": DSAId,
                    "RepaymentType":RepaymentType
                }
                debugger;
                filedata.append('AllDataArray', JSON.stringify(AllDataArray));
                filedata.append('Gurantor_Details', JSON.stringify(customers));
                $.ajax({
                    url: "/LeadGeneration/AddRequestLeadGeneration",
                    type: "POST",
                    contentType: false,
                    processData: false,
                    data: filedata,

                    success: function (result) {
                        debugger

                        var message = JSON.parse(result)[0].ReturnMessage;
                        if (message == "Lead saved succussfully") {
                            swal("TDH", message + " Your Lead No: " + JSON.parse(result)[0].LeadNo, "success");
                            window.location.reload();
                        } else {
                            swal("TDH", "Not Save", "error");

                        }

                    }
                })



            }


        }
        else if (hdn_type == "n") {
            debugger;
            if (Prefix.length == 0) {
                swal("TDH", "Please Select Prefix.", "error");
            }
            else if (FName.length == 0) {
                swal("TDH", "Please enter customer first name", "error");
            }
            else if (FatherName.length == 0) {
                swal("TDH", "Please enter customer father name.", "error");
            }
            else if (MartialStatus == "Married" && SpouseName.length == 0) {
                swal("TDH", "Please enter customer spouse name.", "error");
            }
            else if (Gender.length == 0) {
                swal("TDH", "Please enter customer gender.", "error");
            }
            else if (Dob.length == 0) {
                swal("TDH", "Please enter customer dob.", "error");
            }
            else if (underAgeValidate(Dob) < 18 || underAgeValidate(Dob) > 60) {
                swal("TDH", "customer age must be between 18 and 60 year.", "error");
            }
            else if (MartialStatus.length == 0) {
                swal("TDH", "Please enter customer material status.", "error");
            }
            else if (AadharNo.length == 0) {
                swal("TDH", "Please enter customer aadhar no.", "error");
            }
            else if (AadharNo.length != 12) {
                swal("TDH", "Customer invalid aadhar no.", "error");
            }
            else if (AadharNo.charAt(0) == "0" || AadharNo.charAt(0) == "1") {
                swal("TDH", "Customer Adhaar No would not start with 0 & 1.", "error");
            }
            else if (PanNo.length == 0) {
                swal("TDH", "Please enter customer pan no.", "error");
            }
            else if (PanNo.length != 10) {
                swal("TDH", "Customer invalid pan no.", "error");
            }
            else if (PresentAddress.length == 0) {
                swal("TDH", "Please enter customer present address.", "error");
            }
            else if (PresentPincode.length == 0) {
                swal("TDH", "Please enter customer present pincode.", "error");
            }
            else if (PresentPincode.length != 6) {
                swal("TDH", "Customer invalid present pincode.", "error");
            }
            else if (PermanentAddress.length == 0) {
                swal("TDH", "Please enter customer permanent address.", "error");
            }
            else if (PermanentPincode.length == 0) {
                swal("TDH", "Please enter customer permanent pincode.", "error");
            }
            else if (PermanentPincode.length != 6) {
                swal("TDH", "Customer invalid permanent pincode.", "error");
            }
            else if (CibilScore.length == 0) {
                swal("TDH", "Please enter customer cibil score.", "error");
            }
            else if (CibilScore > 900 && CibilScore < 200) {
                swal("TDH", "Cibil score must be betweeen 200 to 900.", "error");
            }
            else if (MobileNumber1.length == 0) {
                swal("TDH", "Please enter customer mobile number.", "error");
            }
            //else if (FatherMobileNumber.length == 0) {
            //    swal("TDH", "Please enter father mobile number.", "error");
            //}

            else if (MartialStatus == "Married" && SpouseMobileNumber.length == 0) {
                swal("TDH", "Please enter spouse mobile number.", "error");
            }

            else if (extension.length == 0) {
                debugger;
                swal("TDH", "Please select correct file customer.", "error");
            }
            else if (hdn_customer_aadhar_verify != "Y" && $(".btnAadharVerify").is(":disabled") == false) {
                debugger;
                swal("TDH", "Aadhar not verify of Customer.", "error");
            }
            else if (hdn_customer_PanVerify != "Y" && $(".btnPanVerify").is(":disabled") == false) {
                debugger
                swal("TDH", "PAN not verify of Customer.", "error");
            }

            else if (ReuestedLoanAmount.length == 0) {
                swal("TDH", "Please enter Requested Loan Amount.", "error");
            }
            else if (ReuestedLoanTenure.length == 0) {
                swal("TDH", "Please enter Requested Loan Tenure.", "error");
            }
            else if (ReuestedLoanTenure.length > 100) {
                swal("TDH", "Invalid Requested Loan Tenure.", "error");
            }
            else if (MainProductText != "Individual Loan" && RefenceRelation1.length == 0) {
                swal("TDH", "Please enter refrence 1 with relationship.", "error");
            }
            else if (MainProductText != "Individual Loan" && RefenceMobileNo1.length == 0) {
                swal("TDH", "Please enter refrence 1 person mobile no.", "error");
            }
            else if (EstMonthIncome.length == 0) {
                swal("TDH", "Please enter est month income.", "error");
            }
            else if (EstFamilyIncome.length == 0) {
                swal("TDH", "Please enter est family income.", "error");
            }
            else if (LoanPurpose.length == 0) {
                swal("TDH", "Please enter purpose of loan.", "error");
            }
            else if (MainProductText != "Individual Loan" && RefernceName.length == 0) {
                swal("TDH", "Please enter refrence person Person Name.", "error");
            }
            else if (MainProductText != "Individual Loan" && RefenceMobileNo.length != 10) {
                swal("TDH", "Invalid refrence person mobile No.", "error");
            }
            else if (MainProductText != "Individual Loan" && RefenceRelation.length == 0) {
                swal("TDH", "Please Select refrence person Relation.", "error");
            }
            else if (MainProductText != "Individual Loan" && RefernceName1.length == 0) {
                swal("TDH", "Please enter refrence 1 person name.", "error");
            }
            else if (MainProductText != "Individual Loan" && RefenceRelation1.length == 0) {
                swal("TDH", "Please enter refrence 1 with relationship.", "error");
            }
            else if (MainProductText != "Individual Loan" && RefenceMobileNo1.length != 10) {
                swal("TDH", "Please enter refrence 1 person mobile no.", "error");
            }

            else {

                AllDataArray = {
                    "ReqType": ReqType,
                    "MainProductId": MainProductId,
                    "ProductId": ProductId,
                    "Prefix": Prefix,
                    "FirstName": FName,
                    "MiddleName": MName,
                    "LastName": LName,
                    "FatherName": FatherName,
                    "MotherName": MotherName,
                    "SpouseName": SpouseName,
                    "Gender": Gender,
                    "DateofBirth": Dob,
                    "MartialStatus": MartialStatus,
                    "PresentAddress": PresentAddress,
                    "PresentPincode": PresentPincode,
                    "PresentStateId": PresentStateId,
                    "PresentCityId": PresentCityId,
                    "PresentVillage": PresentVillage,
                    "PresentDistrict": PresentDistrict,

                    "PermanentAddress": PermanentAddress,
                    "PermanentPincode": PermanentPincode,
                    "PermanentStateId": PermanentStateId,
                    "PermanentCityId": PermanentCityId,
                    "PermanentVillage": PermanentVillage,
                    "PermanentDistrict": PermanentDistrict,
                    "CibilScore": CibilScore,
                    "MobileNo1": MobileNumber1,

                    /*"MobileNo2": MobileNumber2,*/
                    "FatherMobileNo": FatherMobileNumber,
                    "MotherMobileNo": MotherMobileNumber,
                    "SpouseMobileNo": SpouseMobileNumber,
                    "AadharNo": AadharNo,
                    "PanNo": PanNo,
                    "CompanyId": CompanyID,
                    "Hdn_type": hdn_type,
                    "EmailId": EmailId,
                    "CIFID": CIFID,
                    "OwnerShip": OwnerShip,
                    "AAdharverfiy": hdn_customer_aadhar_verify,

                    "CO_Prefix": CO_Prefix,
                    "CO_FirstName": CO_FName,
                    "CO_MiddleName": CO_MName,
                    "CO_LastName": CO_LName,
                    "CO_DOB": CO_Dob,
                    "CO_Marital_Status": CO_MartialStatus,
                    "CO_Gender": CO_Gender,
                    "CO_PresentAddress": CO_PresentAddress,
                    "CO_PresentPinCode": CO_PresentPincode,
                    "CO_PresentStateId": CO_PresentStateId,
                    "CO_PresentCityId": CO_City,
                    "CO_PresentVillage": CO_PresentVillage,
                    "CO_PresentDistrict": CO_PresentDistrict,

                    "CO_PermanentAddress": CO_PermanentAddress,
                    "CO_PermanentPincode": CO_PermanentPincode,
                    "CO_PermanentStateId": CO_permanentstate,
                    "CO_PermanentCityId": CO_permanentcity,
                    "CO_PermanentVillage": CO_PermanentVillage,
                    "CO_PermanentDistrict": CO_PermanentDistrict,
                    "CO_Mobile_No": CO_MObileNO,
                    "CO_Email_Id": CO_EmailId,
                    "CO_PAN": CO_PanNo,
                    "CO_Adhaar": CO_AadharNo,
                    "CO_CIBIL": CO_CibilScore,
                    "Co_CIF": Co_CIF,
                    "Co_OwnerShip": Co_OwnerShip,

                    "ReuestedLoanAmount": ReuestedLoanAmount,
                    "ReuestedLoanTenure": ReuestedLoanTenure,
                    "EstValueViechle": EstValueViechle,
                    "EstMonthIncome": EstMonthIncome,
                    "EstFamilyIncome": EstFamilyIncome,
                    "EstMonthExpense": EstMonthExpense,
                    "CurMonthObligation": CurMonthObligation,
                    "FORecomedAmt": FORecomedAmt,
                    "NoofDependent": NoofDependent,
                    "ViechleNo": ViechleNo,
                    "ViechleRegYear": ViechleRegYear,
                    "MFGYear": MFGYear,
                    "ViechleModel": ViechleModel,
                    "ViechleColor": ViechleColor,
                    "ViechleCompany": ViechleCompany,
                    "ViechleOwner": ViechleOwner,
                    "RefernceName": RefernceName,
                    "RefenceMobileNo": RefenceMobileNo,
                    "RefenceRelation": RefenceRelation,
                    "RefernceName1": RefernceName1,
                    "RefenceMobileNo1": RefenceMobileNo1,
                    "RefenceRelation1": RefenceRelation1,
                    "LoanPurpose": LoanPurpose,
                    "ColletralSecurityType": ColletralSecurityType,
                    "EstValueofscurity": EstValueofscurity,
                    "Propertyarea": Propertyarea,
                    "PropertyType": PropertyType,
                    "PropertyAddress": PropertyAddress,
                    "CenterId": CenterID,
                    "PLLoanBranch": PLBranchID,
                    "UserRemarks": UserRemarks,
                    "FuelType": FuelType,
                    "Owner": Owner,
                    "InsuranceStatus": InsuranceStatus,
                    "InsuranceType": InsuranceType,
                    "RegistrationDate": RegistrationDate,
                    "ValidityDate": ValidityDate,
                    "ExShowRoomPrice": ExShowRoomPrice,
                    "OnRoadPrice": OnRoadPrice,
                    "Insurer": Insurer,
                    "PolicyNo": PolicyNo,
                    "PerformaInvoice": PerformaInvoice,
                    "ERikshawMaker": ERikshawMaker,
                    "DSAId": DSAId,
                    "RepaymentType": RepaymentType,
                    "CO_FatherName": CO_FatherName,
                    "CO_MotherName": CO_MotherName
                }

                filedata.append('AllDataArray', JSON.stringify(AllDataArray));
                filedata.append('Gurantor_Details', JSON.stringify(customers));
                $.ajax({
                    url: "/LeadGeneration/AddRequestLeadGeneration",
                    type: "POST",
                    contentType: false,
                    processData: false,
                    data: filedata,

                    success: function (result) {
                        debugger

                        var message = JSON.parse(result)[0].ReturnMessage;

                        if (message == "Lead saved succussfully") {
                            swal("TDH", message + " Your Lead No: " + JSON.parse(result)[0].LeadNo, "success");
                            window.location.reload();

                        } else {
                            swal("TDH", "Not Save", "error");

                        }
                    }



                })

            }


        }
        debugger



    }

}

function numberValidation(str) {

    if (isNaN(str)) {
        swal("TDH", "Please Input Numeric Value.", "error");
        str = "";
        return false;
    } else {
        return true;
    }
}


function GetStateCity_PIN(PinCode, txtState, txtCity, txtPincode) {
    debugger;
    $.ajax({
        url: $("#hdn_postalAPI").val() + PinCode,
        type: "Get",
        dataType: "json",
        cache: false,
        processData: false,
        data: {
            MainProductId: MainProductId
        },
        success: function (result) {
            debugger
            if (result[0]["Message"].toUpperCase() == "NO RECORDS FOUND") {
                swal("TDH", result[0]["Message"], "error");
                $('#txtPincode').val("");
                return;
            }
            else {
                var citydata = result[0].PostOffice[0].Division;
                const myArray = citydata.split(" ");
                let CityName = myArray[0];
                txtCity.value = CityName;

                var statedata = result[0].PostOffice[0].State;
                txtState.value = statedata;

                /*txtState = statedata;*/
            }

        }

    });

}