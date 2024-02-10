
function SaveValidation() {
    // debugger;
    var isreq = 0;
    var fileds = "";
    var lt = $("#hdnMainProdId").val();
    if (lt == 4) {

        document.getElementById("ddlDocType").style.borderColor = "#ced4da";
        document.getElementById("txtDocDate").style.borderColor = "#ced4da";
        document.getElementById("txtPagesFrom").style.borderColor = "#ced4da";
        document.getElementById("txtPagesTo").style.borderColor = "#ced4da";
        document.getElementById("txtAnyOther").style.borderColor = "#ced4da";

        if ($("#ddlDocType option:selected").text() == "Select") {
            isreq = 1;
            fileds = fileds.length > 0 ? fileds + ", Document Type" : "Document Type";
            document.getElementById("ddlDocType").style.borderColor = "#FF0000";

        }

        if ($("#txtDocDate").val() == "") {
            isreq = 1;
            fileds = fileds.length > 0 ? fileds + ", Document Date" : "Document Date";
            document.getElementById("txtDocDate").style.borderColor = "#FF0000";
        }

        if ($("#txtPagesFrom").val() == "" || $("#txtPagesFrom").val() == "0") {
            isreq = 1;
            fileds = fileds.length > 0 ? fileds + ", Pages From" : "Pages From";
            document.getElementById("txtPagesFrom").style.borderColor = "#FF0000";
        }

        if ($("#txtPagesTo").val() == "" || $("#txtPagesTo").val() == "0") {
            isreq = 1;
            fileds = fileds.length > 0 ? fileds + ", Pages To" : "Pages To";
            document.getElementById("txtPagesTo").style.borderColor = "#FF0000";
        }

        if ($("#txtAnyOther").val() == "") {
            isreq = 1;
            fileds = fileds.length > 0 ? fileds + ", Any Other" : "Any Other";
            document.getElementById("txtAnyOther").style.borderColor = "#FF0000";
        }
    }
    else if (lt == 2 || lt == 3 || lt == 5) {

        document.getElementById("txtRegistrationCertificate").style.borderColor = "#ced4da";
        document.getElementById("txtInsuredHPEndorse").style.borderColor = "#ced4da";
        document.getElementById("txtInvoiceHPEndorse").style.borderColor = "#ced4da";
        document.getElementById("txtMarginMoneyEndorse").style.borderColor = "#ced4da";
        document.getElementById("txtNOC").style.borderColor = "#ced4da";
        document.getElementById("txtRTOSlip").style.borderColor = "#ced4da";
        document.getElementById("txtEndorsedRC").style.borderColor = "#ced4da";

        if ($("#txtRegistrationCertificate").val() == "") {
            isreq = 1;
            fileds = fileds.length > 0 ? fileds + ", Registration Certificate" : "Registration Certificate";
            document.getElementById("txtRegistrationCertificate").style.borderColor = "#FF0000";
        }

        if ($("#txtInsuredHPEndorse").val() == "") {
            isreq = 1;
            fileds = fileds.length > 0 ? fileds + ", Insured HPEndorse" : "Insured HPEndorse";
            document.getElementById("txtInsuredHPEndorse").style.borderColor = "#FF0000";
        }

        if ($("#txtInvoiceHPEndorse").val() == "") {
            isreq = 1;
            fileds = fileds.length > 0 ? fileds + ", Invoice HPEndorse" : "Invoice HPEndorse";
            document.getElementById("txtInvoiceHPEndorse").style.borderColor = "#FF0000";
        }

        if ($("#txtMarginMoneyEndorse").val() == "") {
            isreq = 1;
            fileds = fileds.length > 0 ? fileds + ", Margin Money Endorse" : "Margin Money Endorse";
            document.getElementById("txtMarginMoneyEndorse").style.borderColor = "#FF0000";
        }

        if ($("#txtNOC").val() == "") {
            isreq = 1;
            fileds = fileds.length > 0 ? fileds + ", NOC" : "NOC";
            document.getElementById("txtNOC").style.borderColor = "#FF0000";
        }

        if ($("#txtRTOSlip").val() == "") {
            isreq = 1;
            fileds = fileds.length > 0 ? fileds + ", RTOSlip" : "RTOSlip";
            document.getElementById("txtRTOSlip").style.borderColor = "#FF0000";
        }

        if ($("#txtEndorsedRC").val() == "") {
            isreq = 1;
            fileds = fileds.length > 0 ? fileds + ", EndorsedRC" : "EndorsedRC";
            document.getElementById("txtEndorsedRC").style.borderColor = "#FF0000";
        }
    }

    if (isreq == 1) {
        swal("TDH", fileds + " are Required.", "error");
        return false;
    }
}
