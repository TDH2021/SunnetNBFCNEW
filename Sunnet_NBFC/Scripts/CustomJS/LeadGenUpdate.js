var CompanyID = $("#hdnCompanyId").val();

$(function () {
    //var PlBranchId = $('#hdnPLBranchId').val();
    //$("[id*=ddlBranch] option").each(function () {
    //    if ($(this).val() == PlBranchId) {
    //        $(this).attr('selected', 'selected');
    //    }
    //});
    //$('#ddlCenter').empty();
    //if (PlBranchId.length == 0) {
    //    var s = '<option value="">- Select Center -</option>'
    //    $('#ddlCenter').html(s);
    //} else {
    //    var s = '<option value="0">- Select Center-</option>'
    //    $('#ddlCenter').html(s);
    //    $.ajax({
    //        url: "/Center/GetCenter",
    //        type: "Get",
    //        dataType: "json",
    //        data: {
    //            BranchId: PlBranchId
    //        },
    //        success: function (result) {

    //            var data = JSON.parse(result);

    //            for (var i = 0; i < data.length; i++) {
    //                var opt = new Option(data[i].CenterName, data[i].CenterId);
    //                $('#ddlCenter').append(opt);
    //            }
    //        }
    //    });
    //}
    //var CenterId = $('#hdnCenterId').val();
    //$("[id*=ddlCenter] option").each(function () {
    //    if ($(this).val() == CenterId) {
    //        $(this).attr('selected', 'selected');
    //    }
    //});


    var CustPrefix = $('#hdnPrefix').val();
    $("[id*=Prefix] option").each(function () {
        if ($(this).val() == CustPrefix) {
            $(this).attr('selected', 'selected');
        }
    });

    var CoPrefix = $('#hdnCOPrefix').val();
    $("[id*=CoPrefix] option").each(function () {
        if ($(this).val() == CoPrefix) {
            $(this).attr('selected', 'selected');
        }
    });

    var Relation1 = $('#hfReleation1').val();
    $("[id*=RefenceRelation] option").each(function () {
        if ($(this).val() == Relation1) {
            $(this).attr('selected', 'selected');
        }
    });

    var Relation2 = $('#hfReleation2').val();
    $("[id*=RefenceRelation1] option").each(function () {
        if ($(this).val() == Relation2) {
            $(this).attr('selected', 'selected');
        }
    });

    var Repaymenttype = $('#hfRepayment').val();
    $("[id*=ddlRepaymentType] option").each(function () {
        if ($(this).text() == Repaymenttype) {
            $(this).attr('selected', 'selected');
        }
    });
    var FualType = $('#hfFualType').val();
    $("[id*=ddlFualType] option").each(function () {
        if ($(this).text() == FualType) {
            $(this).attr('selected', 'selected');
        }
    });
    var InsuranceStatus = $('#hfInsuranceStatus').val();
    $("[id*=ddlInsuranceStatus] option").each(function () {
        if ($(this).text() == InsuranceStatus) {
            $(this).attr('selected', 'selected');
        }
    });
    var InsuranceType = $('#hfInsuranceType').val();
    $("[id*=ddlInsuranceType] option").each(function () {
        if ($(this).text() == InsuranceType) {
            $(this).attr('selected', 'selected');
        }
    });

    var DSAId = $('#hfDealer').val();
    $("[id*=ddlDealer] option").each(function () {
        if ($(this).val() == DSAId) {
            $(this).attr('selected', 'selected');
        }
    });
    var Collertal = $('#hfCollertal').val();
    $("[id*=BusinessColletralSecurityType] option").each(function () {
        if ($(this).val() == Collertal) {
            $(this).attr('selected', 'selected');
        }
    });

    var PropetyArea = $('#hfPropertyArea').val();
    $("[id*=ddlPropertyArea] option").each(function () {
        if ($(this).text() == PropetyArea) {
            $(this).attr('selected', 'selected');
        }
    });

    var PropertyType = $('#hfPropertyType').val();
    $("[id*=ddlPropertyType] option").each(function () {
        if ($(this).text() == PropertyType) {
            $(this).attr('selected', 'selected');
        }
    });
    var CustOwnerShip = $('#hdnOwneShip').val();
    $("[id*=ddlCustOwnerShip] option").each(function () {
        if ($(this).text() == CustOwnerShip) {
            $(this).attr('selected', 'selected');
        }
    });
    var CoCustOwnerShip = $('#hdnCoOwneShip').val();
    $("[id*=ddlCoOwnerShip] option").each(function () {
        if ($(this).text() == CoCustOwnerShip) {
            $(this).attr('selected', 'selected');
        }
    });
    if ($('#txtMainProdName').val() == "Individual Loan") {
        $('#business_div').hide();
        $('#vechical_div').hide();
        $('#personal_div').show();
        $('#divPersonal').show();
        $('#divRef1').hide();
        $('#divRef2').hide();
    }
    else if ($('#txtMainProdName').val().toUpperCase() == "VEHICLE LOAN" || $('#txtMainProdName').val().toUpperCase() == "TWO WHEELER LOAN" || $('#txtMainProdName').val().toUpperCase() == "COMMERCIAL VEHICLE") {
        $('#business_div').hide();
        $('#vechical_div').show();
        $('#personal_div').show();
        $('#divPersonal').hide();
        $('#divRef1').show();
        $('#divRef2').show();
    } else if ($('#txtMainProdName').val().toUpperCase() == "BUSSINESS LOAN") {
        $('#business_div').show();
        $('#personal_div').show();
        $('#vechical_div').hide();
        $('#divPersonal').hide();
        $('#divRef1').show();
        $('#divRef2').show();
    }

    if ($('#hdn_type').val() == "c") {
        $("#co_applicant_div").show();
        $("#co_guranter_div").hide();

    } else if ($('#hdn_type').val() == "g") {
        $("#co_applicant_div").hide();
        $("#co_guranter_div").show();
        //    document.getElementById("ProductId").value = data;
    } else if ($('#hdn_type').val() == "b") {
        $("#co_applicant_div").show();
        $("#co_guranter_div").show();
        //    document.getElementById("ProductId").value = data;
    }
    if (document.getElementById("chkSameCurper").checked = -true) {

        mytest();
    }
    if (document.getElementById("co_chlpercorr").checked = -true) {

        Co_Applicantpermascorr();
    }
    
});

function underAgeValidate(birthday) {

    /*   debugger;*/
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
    var yes = document.getElementById("chkSameCurper").checked;
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
        var PresentPincode = $("#txtGPresentPincode").val();
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

function UpdateDetails() {
    debugger
    var regex = /^[a-zA-Z]*$/;
    var PANregex = /[A-Z]{5}[0-9]{4}[A-Z]{1}$/;
    var ReqType = "Update";
    var LeadId = $("#hdnLeadId").val();
    var LeadNo = $("#txtLeadNo").val();
    var MainProductId = $("#hfMainProdId").val();
    var MainProductText = $("#txtMainProdName").val();
    var ProductId = $("#hfProdId").val();
    var ProductName = $("#txtProdName").val();
    //var CenterID = $("#ddlCenter option:selected").val();
    //var PLBranchID = $("#ddlBranch option:selected").val();
    //if (PLBranchID.length == 0) {
    //    PLBranchID = 0;
    //}
    //if (CenterID.length == 0) {
    //    CenterID = 0;
    //}
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
        FORecomedAmt = $("#txtFORecomedAmtcomedAmt").val();
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
    var DSAId = $("#ddlDealer :selected").val();
    var RepaymentType = $("#ddlRepaymentType :selected").val();

    var InsEndValidityDate = $("#txtEndValidityDate").val();
    if (MainProductText != "Individual Loan") {
        RepaymentType = "";
    }
    if (DSAId.length == 0) {
        DSAId = "0";
    }
    debugger
    var filedata = new FormData();

    debugger;

    if (MainProductId.length == 0) {
        swal("TDH", "Please Select Main Product", "error");
    }
    else if (ProductId.length == 0) {
        swal("TDH", "Please Select Product", "error");
    }
    if (ReuestedLoanAmount.length == 0) {
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
        swal("TDH", "Please enter refrence 2 person name.", "error");
    }
    else if (MainProductText != "Individual Loan" && RefenceRelation1.length == 0) {
        swal("TDH", "Please enter refrence 2 with relationship.", "error");
    }
    else if (MainProductText != "Individual Loan" && FORecomedAmt == 0) {
        swal("TDH", "Please enter FO Recomended Amount/Loan Recomended by FO.", "error");
    }
    else {

        AllDataArray = {
            "ReqType": ReqType,
            "MainProductId": MainProductId,
            "ProductId": ProductId,
            "LeadId": LeadId,
            "LeadNo":LeadNo,
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
            "InsEndValidityDate": InsEndValidityDate
        }
        filedata.append('AllDataArray', JSON.stringify(AllDataArray));

        $.ajax({
            url: "/LeadGenUpdate/AddRequestLeadGeneration",
            type: "POST",
            contentType: false,
            processData: false,
            data: filedata,

            success: function (result) {
                debugger

                var message = JSON.parse(result)[0].ReturnMessage;

                if (message == "Record updated succussfully") {
                    swal("TDH", message, "success");
                    window.location.reload();
                } else {
                    swal("TDH", "Not Save", "error");

                }
            }
        })
    }

}



function UpdateGurnter() {
    debugger
    var G_AadharVerify, G_PanVerify;
    var regex = /^[a-zA-Z]*$/;
    var PANregex = /[A-Z]{5}[0-9]{4}[A-Z]{1}$/;
    var ReqType = "Update";
    var G_LeadCustomerId = $("#hfLeadCustomerID").val();
    var G_Prefix = $("#GPrefix :selected").text();
    var G_FirstName = $("#G_FName").val();
    var G_MiddleName = $("#G_MName").val();
    var G_LastName = $("#G_LName").val();
    var G_FatherName = $("#G_FatherName").val();
    var G_SpouseName = $("#G_SpouseName").val();
    var G_Dob = $("#G_Dob").val();
    var G_Gender = $("#G_Gender option:selected").val();

    var G_MartialStatus = $("#G_MartialStatus option:selected").val();
    var G_PresentAddress = $("#G_PresentAddress").val();
    var G_PresentPincode = $("#txtGPresentPincode").val();
    var G_PresentStateId = $("#G_C_State").val();
    var G_PresentCityId = $("#G_C_City").val();
    var G_PresentVillage = $("#G_PresentVillage").val();
    var G_PresentDistrict = $("#G_PresentDistrict").val();
    var G_IsSameCurrentperadd = $('#G_chlpercorr').prop('checked');
    if (G_IsSameCurrentperadd == false) {
        G_IsSameCurrentperadd = 0;
    }
    else {
        G_IsSameCurrentperadd = 1;
    }

    var G_PermanentAddress = $("#G_PermanentAddress").val();
    var G_PermanentPincode = $("#G_PermanentPincode").val();
    var G_P_State = $("#G_P_State").val();
    var G_P_City = $("#G_P_City").val();
    var G_PermanentVillage = $("#G_PermanentVillage").val();
    var G_PermanentDistrict = $("#G_PermanentDistrict").val();
    var CibilScore = $("#CibilScore").val();
    var G_Mobile_No = $("#G_Mobile_No").val();
    /*var MobileNumber2 = $("#MobileNumber2").val();*/

    var G_EmailId = $("#G_EmailId").val();
    var G_CIF = $("#txtGCIF").val();
    var G_AadharNo = $("#G_AadharNo").val();
    var G_PanNo = $("#G_PanNo").val();
    var OwnerShip = $("#ddlGOwnerShip option:selected").val();
    G_PanVerify = 1; G_AadharVerify = 1
    var G_LandMark = $("#txtLandMark").val();

    debugger;

    if (G_LeadCustomerId.length == 0) {
        swal("TDH", "Please Select Guarantor .", "error");
    }
    else if (G_Prefix.length == 0) {
        swal("TDH", "Please Select Prefix.", "error");
    }
    else if (G_FName.length == 0) {
        swal("TDH", "Please enter Guarantor first name", "error");
    }

    else if (G_MartialStatus == "Married" && SpouseName.length == 0) {
        swal("TDH", "Please enter Guarantor spouse name.", "error");
    }
    else if (G_Gender.length == 0) {
        swal("TDH", "Please enter Guarantor gender.", "error");
    }
    else if (G_Dob.length == 0) {
        swal("TDH", "Please enter Guarantor dob.", "error");
    }
    else if (underAgeValidate(G_Dob) < 18 || underAgeValidate(G_Dob) > 60) {
        swal("TDH", "Guarantor age must be between 18 & 60 year.", "error");
    }
    else if (G_MartialStatus.length == 0) {
        swal("TDH", "Please enter Guarantor material status.", "error");
    }
    else if (G_AadharNo.length == 0) {
        swal("TDH", "Please enter Guarantor aadhar no.", "error");
    }
    else if (G_AadharNo.length != 12) {
        swal("TDH", "Guarantor invalid aadhar no.", "error");
    }
    else if (G_AadharNo.charAt(0) == "0" || G_AadharNo.charAt(0) == "1") {
        swal("TDH", "Guarantor Adhaar No would not start with 0 & 1.", "error");
    }
    else if (G_PanNo.length == 0) {
        swal("TDH", "Please enter customer pan no.", "error");
    }
    else if (G_PanNo.length != 10) {
        swal("TDH", "Guarantor invalid pan no.", "error");
    }
    else if (G_PresentAddress.length == 0) {
        swal("TDH", "Please enter Guarantor present address.", "error");
    }
    else if (G_PresentPincode.length == 0) {
        swal("TDH", "Please enter Guarantor present pincode.", "error");
    }
    else if (G_PresentPincode.length != 6) {
        swal("TDH", "Guarantor invalid present pincode.", "error");
    }
    else if (G_PermanentAddress.length == 0) {
        swal("TDH", "Please enter Guarantor permanent address.", "error");
    }
    else if (G_PermanentPincode.length == 0) {
        swal("TDH", "Please enter Guarantor permanent pincode.", "error");
    }
    else if (G_PermanentPincode.length != 6) {
        swal("TDH", "Guarantor invalid permanent pincode.", "error");
    }
    else if (G_Mobile_No.length == 0) {
        swal("TDH", "Please enter Guarantor mobile number.", "error");
    }
    else {
        var filedata = new FormData();
        AllDataArray = {
            "ReqType": ReqType,
            "G_LeadCustomerId": G_LeadCustomerId,
            "G_CIF": G_CIF,
            "G_PrefixName": G_Prefix,
            "G_FirstName": G_FirstName,
            "G_MiddleName": G_MiddleName,
            "G_LastName": G_LastName,
            "G_FatherName": G_FatherName,
            "G_SpouseName": G_SpouseName,
            "G_Gender": G_Gender,
            "G_DOB": G_Dob,
            "G_Marital_Status": G_MartialStatus,
            "G_PresentAddress": G_PresentAddress,
            "G_PresentPinCode": G_PresentPincode,
            "G_PresentStateId": G_PresentStateId,
            "G_PresentCityId": G_PresentCityId,
            "G_PresentVillage": G_PresentVillage,
            "G_PresentDistrict": G_PresentDistrict,
            "G_PermanentAddress": G_PermanentAddress,
            "G_PermanentPincode": G_PermanentPincode,
            "G_P_State": G_P_State,
            "G_P_City": G_P_City,

            "G_Mobile_No": G_Mobile_No,
            "G_EmailId": G_EmailId,
            "G_CibilScore": CibilScore,
            "G_AadharNo": G_AadharNo,
            "G_AadharVerify": G_AadharVerify,
            "G_PanNo": G_PanNo,
            "G_PanVerify": G_PanVerify,
            "G_OwnerShip": OwnerShip,
            "G_LandMark": G_LandMark,
            "G_IsSameCurrentperadd": G_IsSameCurrentperadd,
            "CompanyId": CompanyID

        }

        filedata.append('AllDataArray', JSON.stringify(AllDataArray));

        $.ajax({
            url: "/LeadGenUpdate/UpdateGurrenter",
            type: "POST",
            contentType: false,
            processData: false,
            data: filedata,

            success: function (result) {
                debugger

                var message = JSON.parse(result)[0].ReturnMessage;

                if (message == "Record updated successfully") {
                    window.location.reload();
                } else {
                    swal("TDH", "Not Save", "error");

                }
            }



        })

    }


}

function UpdateCustomer() {
    debugger
    var filedata = new FormData();
    var hdn_customer_aadhar_verify, hdn_customer_PanVerify;
    var regex = /^[a-zA-Z]*$/;
    var PANregex = /[A-Z]{5}[0-9]{4}[A-Z]{1}$/;
    var ReqType = "Update";
    var LeadNo = $("#txtLeadNo").val();
    var LeadCustomerId = $("#hfCustLeadCustomerID").val();
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
    var PanNo = $("#PanNo").val();
    var OwnerShip = $("#ddlCustOwnerShip option:selected").val();
    var Cust_LandMark = $("#txtCustLandMark").val();
    var G_IsSameCurrentperadd = $('#chkSameCurper').prop('checked');
    if (G_IsSameCurrentperadd == false) {
        G_IsSameCurrentperadd = 0;
    }
    else {
        G_IsSameCurrentperadd = 1;
    }
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

    hdn_customer_aadhar_verify = 1;
    hdn_customer_PanVerify = 1;
    debugger;

    if (LeadCustomerId.length == 0) {
        swal("TDH", "Please Select Customer .", "error");
    }
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

    else {

        AllDataArray = {
            "ReqType": ReqType,
            "LeadCustomerId": LeadCustomerId,
            "PrefixName": Prefix,
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
            "FatherMobileNo": FatherMobileNumber,
            "MotherMobileNo": MotherMobileNumber,
            "SpouseMobileNo": SpouseMobileNumber,
            "AadharNo": AadharNo,
            "PanNo": PanNo,
            "CompanyId": CompanyID,
            "EmailId": EmailId,
            "CIFID": CIFID,
            "OwnerShip": OwnerShip,
            "AAdharverfiy": hdn_customer_aadhar_verify,
            "Cust_IsSameCurrentperadd": G_IsSameCurrentperadd,
            "CustLandMark": Cust_LandMark,
            "PanVerify": hdn_customer_PanVerify,
            "LeadNo": LeadNo
        }

        filedata.append('AllDataArray', JSON.stringify(AllDataArray));

        $.ajax({
            url: "/LeadGenUpdate/UpdateCustomer",
            type: "POST",
            contentType: false,
            processData: false,
            data: filedata,

            success: function (result) {
                debugger

                var message = JSON.parse(result)[0].ReturnMessage;

                if (message == "Record updated successfully") {
                    swal("TDH", message, "success");
                } else {
                    swal("TDH", "Not Save", "error");

                }
            }
        })
    }

}
function UpdateCoBorrower() {
    debugger;
    var filedata = new FormData();
    var CO_AAdharverfiy, CO_Panverfiy;
    CO_AAdharverfiy = 1;
    CO_Panverfiy = 1;
    var regex = /^[a-zA-Z]*$/;
    var PANregex = /[A-Z]{5}[0-9]{4}[A-Z]{1}$/;
    var ReqType = "Update";
    var G_LeadCustomerId = $("#hfCOLeadCustomerID").val();
    var LeadNo = $("#txtLeadNo").val();
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

    var G_IsSameCurrentperadd = $('#co_chlpercorr').prop('checked');
    if (G_IsSameCurrentperadd == false) {
        G_IsSameCurrentperadd = 0;
    }
    else {
        G_IsSameCurrentperadd = 1;
    }
    var Co_LandMark = $("#txtCoLandMark").val();
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
        extension1 = fileName1.substring(fileName1.lastIndexOf('.') + 1);
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

    if (G_LeadCustomerId.length == 0) {
        swal("TDH", "Please Select Guarantor .", "error");
    }

    else if (CO_Prefix.length == 0) {
        swal("TDH", "Please select co applicant prefix.", "error");
    }
    else if (CO_FName.length == 0) {
        swal("TDH", "Please enter co applicant first name.", "error");
    }
    //else if (CO_FatherName.length == 0) {
    //    swal("TDH", "Please enter co applicant father name.", "error");
    //}
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
    else if (Co_LandMark.length == 0) {
        swal("TDH", "Please enter co applicant Land Mark.", "error");
    }

    else if (CO_PanNo.length != 10) {
        swal("TDH", "Please enter co applicant pan no.", "error");
    }
    else if (CO_AadharNo.length != 12) {
        swal("TDH", "Please enter co applicant Aadhar No.", "error");
    }
    else if (CO_AadharNo.charAt(0) == "0" || CO_AadharNo.charAt(0) == "1") {
        swal("TDH", "Co Applicant Adhaar No would not start with 0 & 1.", "error");
    } else if (Co_LandMark.length == 0) {
        debugger
        swal("TDH", "Please Enter co applicant Land Mark.", "error");
    }

    else {
        
        AllDataArray = {
            "ReqType": ReqType,
            "CO_LeadCustomerId": G_LeadCustomerId,
            "Co_PrefixName": CO_Prefix,
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
            "CO_FatherName": CO_FatherName,
            "CO_MotherName": CO_MotherName,
            "Co_LandMark": Co_LandMark,
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
            "CO_IsSameCurrentperadd": G_IsSameCurrentperadd,
            "CompanyId": CompanyID,
            "CO_Panverfiy": CO_Panverfiy,
            "CO_AAdharverfiy": CO_AAdharverfiy,
            "LeadNo": LeadNo
        }

        filedata.append('AllDataArray', JSON.stringify(AllDataArray));

        $.ajax({
            url: "/LeadGenUpdate/UpdateCoBorrower",
            type: "POST",
            contentType: false,
            processData: false,
            data: filedata,

            success: function (result) {
                debugger

                var message = JSON.parse(result)[0].ReturnMessage;

                if (message == "Record updated successfully") {
                    swal("TDH", message, "success");
                } else {
                    swal("TDH", "Not Save", "error");

                }
            }



        })

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


//function GetStateCity_PIN(PinCode, txtState, txtCity, txtPincode) {
//    debugger;
//    $.ajax({
//        url: $("#hdn_postalAPI").val() + PinCode,
//        type: "Get",
//        dataType: "json",
//        cache: false,
//        processData: false,
//        data: {
//            MainProductId: MainProductId
//        },
//        success: function (result) {
//            debugger
//            if (result[0]["Message"].toUpperCase() == "NO RECORDS FOUND") {
//                swal("TDH", result[0]["Message"], "error");
//                $('#txtPincode').val("");
//                return;
//            }
//            else {
//                var citydata = result[0].PostOffice[0].Division;
//                const myArray = citydata.split(" ");
//                let CityName = myArray[0];
//                txtCity.value = CityName;

//                var statedata = result[0].PostOffice[0].State;
//                txtState.value = statedata;

//                /*txtState = statedata;*/
//            }

//        }

//    });

//}

function GetStateCity_PIN(PinCode, txtState, txtCity, txtPincode) {
    debugger;
    $.ajax({
        url: "/Common/GetPinCode",
        type: "Get",
        dataType: "json",
        data: {
            PinCode: PinCode
        },
        success: function (result) {
            debugger;

            if (JSON.parse(result).length <= 1) {
                swal("TDH", result[0]["Message"], "error");
                $('#txtPincode').val("");
                return;
            }
            else {
                txtCity.value = JSON.parse(result)[0].district;
                txtState.value = JSON.parse(result)[0].circle;;

                /*txtState = statedata;*/
            }
        }


    });

}

function FindEndInsdate() {
    var inputDateValue = $("#txtValidityDate").val();

    // Check if a date is entered
    if (inputDateValue) {
        // Convert the input value to a JavaScript Date object using moment.js
        var inputDate = moment(inputDateValue, "DD/MM/YYYY", true);

        // Check if the date is valid
        if (inputDate.isValid()) {
            // Add 365 days to the input date
            inputDate.add(365, 'days');

            // Format the new date as a string in dd/mm/yyyy format
            var newDate = inputDate.format("DD/MM/YYYY");

            // Update the input field with the new date
            $("#txtEndValidityDate").val(newDate);
        } else {
            alert("Please enter a valid date in dd/mm/yyyy format.");
        }
    } else {
        alert("Please enter a date first.");
    }
    // Check if a date is selected

}
