

$(document).ready(function () {
    //$('.datepicker').datepicker();

    $("#chkCIBILVerification").click(
        function () {
            if ($(this).is(":checked")) {
                $("#clsLeadCredit_CibilRemarks").removeAttr('disabled')
                $("#CibilDocPostedFile").removeAttr('disabled')
            } else {
                $("#clsLeadCredit_CibilRemarks").attr('disabled', 'disabled')
                $("#CibilDocPostedFile").attr('disabled', 'disabled')
            }
        });

    $("#chkCAMVerification").click(
        function () {
            if ($(this).is(":checked")) {
                $("#clsLeadCredit_CAMRemarks").removeAttr('disabled')
                $("#CAMDocPostedFile").removeAttr('disabled')
            } else {
                $("#clsLeadCredit_CAMRemarks").attr('disabled', 'disabled')
                $("#CAMDocPostedFile").attr('disabled', 'disabled')
            }
        });

    $("#chkFIVerification").click(
        function () {
            if ($(this).is(":checked")) {
                $("#clsLeadCredit_FIRemarks").removeAttr('disabled')
                $("#FIDocPostedFile").removeAttr('disabled')
            } else {
                $("#clsLeadCredit_FIRemarks").attr('disabled', 'disabled')
                $("#FIDocPostedFile").attr('disabled', 'disabled')
            }
        });

    $("#chkTVRVerification").click(
        function () {
            if ($(this).is(":checked")) {
                $("#clsLeadCredit_TVRRemarks").removeAttr('disabled')
                $("#TVRDocPostedFile").removeAttr('disabled')
            } else {
                $("#clsLeadCredit_TVRRemarks").attr('disabled', 'disabled')
                $("#TVRDocPostedFile").attr('disabled', 'disabled')
            }
        });


    $("#chkDependentFamilyAssessmentVerification").click(
        function () {
            if ($(this).is(":checked")) {
                $("#clsLeadCredit_DependentFamilyAssessmentRemarks").removeAttr('disabled')
                $("#DependentFamilyAssessmentDocPostedFile").removeAttr('disabled')
            } else {
                $("#clsLeadCredit_DependentFamilyAssessmentRemarks").attr('disabled', 'disabled')
                $("#DependentFamilyAssessmentDocPostedFile").attr('disabled', 'disabled')
            }
        });


    $("#chkCashFlowVerification").click(
        function () {
            if ($(this).is(":checked")) {
                $("#clsLeadCredit_CashFlowRemarks").removeAttr('disabled')
                $("#CashFlowDocPostedFile").removeAttr('disabled')
            } else {
                $("#clsLeadCredit_CashFlowRemarks").attr('disabled', 'disabled')
                $("#CashFlowDocPostedFile").attr('disabled', 'disabled')
            }
        });


    $("#chkViechleValVerfication").click(
        function () {
            if ($(this).is(":checked")) {
                $("#clsLeadCredit_ViechleRemarks").removeAttr('disabled')
                $("#ViechleDocPostedFile").removeAttr('disabled')
            } else {
                $("#clsLeadCredit_ViechleRemarks").attr('disabled', 'disabled')
                $("#ViechleDocPostedFile").attr('disabled', 'disabled')
            }
        });


    $("#chkBankStmtVerification").click(
        function () {
            if ($(this).is(":checked")) {
                $("#clsLeadCredit_BankStmtRemarks").removeAttr('disabled')
                $("#BankStmtDocPostedFile").removeAttr('disabled')
            } else {
                $("#clsLeadCredit_BankStmtRemarks").attr('disabled', 'disabled')
                $("#BankStmtDocPostedFile").attr('disabled', 'disabled')
            }
        });

    $("#chkIncomeStmtVerification").click(
        function () {
            if ($(this).is(":checked")) {
                $("#clsLeadCredit_IncomeStmtRemarks").removeAttr('disabled')
                $("#IncomeStmtDocPostedFile").removeAttr('disabled')
            } else {
                $("#clsLeadCredit_IncomeStmtRemarks").attr('disabled', 'disabled')
                $("#IncomeStmtDocPostedFile").attr('disabled', 'disabled')
            }
        });


    $("#chkPersonalDiscussVerification").click(
        function () {
            if ($(this).is(":checked")) {
                $("#clsLeadCredit_PersonalDiscusssRemarks").removeAttr('disabled')
                $("#PersonalDiscussDocPostedFile").removeAttr('disabled')
            } else {
                $("#clsLeadCredit_PersonalDiscusssRemarks").attr('disabled', 'disabled')
                $("#PersonalDiscussDocPostedFile").attr('disabled', 'disabled')
            }
        });

    $("#chkEligiblity").click(
        function () {
            if ($(this).is(":checked")) {
                $("#clsLeadCredit_EligiblityRemarks").removeAttr('disabled')
                $("#EligiblityDocPostedFile").removeAttr('disabled')
            } else {
                $("#clsLeadCredit_EligiblityRemarks").attr('disabled', 'disabled')
                $("#EligiblityDocPostedFile").attr('disabled', 'disabled')
            }
        });

    $("#chkPropertyDocVerification").click(
        function () {
            if ($(this).is(":checked")) {
                $("#clsLeadCredit_PropertyDocRemarks").removeAttr('disabled')
                $("#PropertyDocPostedFile").removeAttr('disabled')
            } else {
                $("#clsLeadCredit_PropertyDocRemarks").attr('disabled', 'disabled')
                $("#PropertyDocPostedFile").attr('disabled', 'disabled')
            }
        });



});

function Validation() {


    if($("#ddlStatus").val() == "") {
        swal("TDH", "Please select status", "error");
        return false;
    }

    if ($('#chkCAMVerification').is(":checked") != true) {
        swal("TDH", "Please CAM verification", "error");
        return false;
    }


    if ($('#chkCIBILVerification').is(":checked") == true) {
        var filedata = new FormData();
        var fileUpload = $("#CibilDocPostedFile").get(0);
        var files = fileUpload.files;
        debugger;
        if (files.length <= 0 && $("#clsLeadCredit_CibilDoc").val()=="") {
            swal("TDH", "Please Upload CIBIL File.", "error");
            return false;
        }
        else if ($("#clsLeadCredit_CibilRemarks").val().trim() == "") {
            swal("TDH", "Please Input CIBIL Remarks.", "error");
            return false;
        }
    }
    if ($('#chkCAMVerification').is(":checked") == true) {
        var filedata = new FormData();
        var fileUpload = $("#CAMDocPostedFile").get(0);
        var files = fileUpload.files;
        debugger;
        if (files.length <= 0 && $("#clsLeadCredit_CAMDoc").val() == "") {
            swal("TDH", "Please Upload CAM File.", "error");
            return false;
        }
        else if ($("#clsLeadCredit_CAMRemarks").val().trim() == "") {
            swal("TDH", "Please Input CAM Remarks", "error");
            return false;
        }
    }

    if ($('#chkFIVerification').is(":checked") == true) {
        var filedata = new FormData();
        var fileUpload = $("#FIDocPostedFile").get(0);
        var files = fileUpload.files;
        debugger;
        if (files.length <= 0 && $("#clsLeadCredit_FIDoc").val() == "") {
            swal("TDH", "Please Upload FI Verification File.", "error");
            return false;
        }
        else if ($("#clsLeadCredit_FIRemarks").val().trim() == "") {
            swal("TDH", "Please Input FI Remarks", "error");
            return false;
        }
    }

    if ($('#chkTVRVerification').is(":checked") == true) {
        var filedata = new FormData();
        var fileUpload = $("#TVRDocPostedFile").get(0);
        var files = fileUpload.files;
        debugger;
        if (files.length <= 0 && $("#clsLeadCredit_TVRDoc").val() == "") {
            swal("TDH", "Please Upload TVR File.", "error");
            return false;
        }
        else if ($("#clsLeadCredit_TVRRemarks").val().trim() == "") {
            swal("TDH", "Please Input TVR Remarks.", "error");
            return false;
        }
    }

    if ($('#chkDependentFamilyAssessmentVerification').is(":checked") == true) {
        var filedata = new FormData();
        var fileUpload = $("#DependentFamilyAssessmentDocPostedFile").get(0);
        var files = fileUpload.files;
        debugger;
        if (files.length <= 0 && $("#clsLeadCredit_DependentFamilyAssessmentDoc").val() == "") {
            swal("TDH", "Please Upload Depended Assesment File.", "error");
            return false;
        }
        else if ($("#clsLeadCredit_DependentFamilyAssessmentRemarks").val().trim() == "") {
            swal("TDH", "Please Input Depedent Remarks.", "error");
            return false;
        }
    }

    if ($('#chkCashFlowVerification').is(":checked") == true) {
        var filedata = new FormData();
        var fileUpload = $("#CashFlowDocPostedFile").get(0);
        var files = fileUpload.files;
        debugger;
        if (files.length <= 0 && $("#clsLeadCredit_CashFlowDoc").val() == "") {
            swal("TDH", "Please Upload Cash Flow File.", "error");
            return false;
        }
        else if ($("#clsLeadCredit_CashFlowRemarks").val().trim() == "") {
            swal("TDH", "Please Input Cash Flow Remarks.", "error");
            return false;
        }
    }
    if ($('#chkViechleValVerfication').is(":checked") == true) {
        var filedata = new FormData();
        var fileUpload = $("#ViechleDocPostedFile").get(0);
        var files = fileUpload.files;
        debugger;
        if (files.length <= 0 && $("#clsLeadCredit_ViechleDoc").val() == "") {
            swal("TDH", "Please Upload Viechle File.", "error");
            return false;
        }
        else if ($("#clsLeadCredit_ViechleRemarks").val().trim() == "") {
            swal("TDH", "Please Input Viechle Remarks.", "error");
            return false;
        }
    }

    if ($('#chkBankStmtVerification').is(":checked") == true) {
        var filedata = new FormData();
        var fileUpload = $("#BankStmtDocPostedFile").get(0);
        var files = fileUpload.files;
        debugger;
        if (files.length <= 0 && $("#clsLeadCredit_BankStmtDoc").val() == "") {
            swal("TDH", "Please Upload Bank Statement File.", "error");
            return false;
        }
        else if ($("#clsLeadCredit_BankStmtRemarks").val().trim() == "") {
            swal("TDH", "Please Input Bank Remarks.", "error");
            return false;
        }
    }

    if ($('#chkIncomeStmtVerification').is(":checked") == true) {
        var filedata = new FormData();
        var fileUpload = $("#IncomeStmtDocPostedFile").get(0);
        var files = fileUpload.files;
        debugger;
        if (files.length <= 0 && $("#clsLeadCredit_IncomeStmtDoc").val() == "") {
            swal("TDH", "Please Upload Income Statement File.", "error");
            return false;
        }
        else if ($("#clsLeadCredit_IncomeStmtRemarks").val().trim() == "") {
            swal("TDH", "Please Input Income Statement Remarks.", "error");
            return false;
        }
    }

    if ($('#chkPersonalDiscussVerification').is(":checked") == true) {
        var filedata = new FormData();
        var fileUpload = $("#PersonalDiscussDocPostedFile").get(0);
        var files = fileUpload.files;
        debugger;
        if (files.length <= 0 && $("#clsLeadCredit_PersonalDiscussDoc").val() == "") {
            swal("TDH", "Please Upload Personal Discussion File.", "error");
            return false;
        }
        else if ($("#clsLeadCredit_PersonalDiscusssRemarks").val().trim() == "") {
            swal("TDH", "Please Input Personal Discuss Remarks.", "error");
            return false;
        }
    }

    if ($('#chkEligiblity').is(":checked") == true) {
        var filedata = new FormData();
        var fileUpload = $("#EligiblityDocPostedFile").get(0);
        var files = fileUpload.files;
        debugger;
        if (files.length <= 0 && $("#clsLeadCredit_EligiblityDoc").val() == "") {
            swal("TDH", "Please Upload Elegiblity File.", "error");
            return false;
        }
        else if ($("#clsLeadCredit_EligiblityRemarks").val().trim() == "") {
            swal("TDH", "Please Input Elgiblity Remarks.", "error");
            return false;
        }
    }

    if ($('#chkPropertyDocVerification').is(":checked") == true) {
        var filedata = new FormData();
        var fileUpload = $("#PropertyDocPostedFile").get(0);
        var files = fileUpload.files;
        debugger;
        if (files.length <= 0 && $("#clsLeadCredit_PropertyDoc").val() == "") {
            swal("TDH", "Please Upload Property File.", "error");
            return false;
        }
        else if ($("#clsLeadCredit_PropertyDocRemarks").val().trim() == "") {
            swal("TDH", "Please Input Property Remarks.", "error");
            return false;
        }
    }


    if ($("#hfCrId").val() == "0") {
        if ($('#chkCIBILVerification').is(":checked") == false) {
            swal("TDH", "Please Select CIBIL Verification.", "error");
            return false;

        }
        else if ($('#chkCAMVerification').is(":checked") == false) {
            swal("TDH", "Please Select CAM Verification.", "error");
            return false;
        }
    }


    if ($("#ddlStatus :selected").text() == "Select") {
        swal("TDH", "Please Select Status.", "error");
    }
    else {
        if ($("#txtRemarks").val().trim() == "") {
            swal("TDH", "Please Input Remarks Before Submit.", "error");
            return false;
        }
    }
}