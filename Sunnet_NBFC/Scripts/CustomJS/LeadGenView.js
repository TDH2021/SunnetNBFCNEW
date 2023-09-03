function ShowData() {

    var ReqType = "Insert";
    var MainProductId = $("#MainProductId :selected").val();
    var ProductId = $("#ProductId :selected").val();
    var LeadNo = $("#txtLeadNo").val();
    var CustomerName = $("#txtCustName").val();
    var MobileNo1 = $("#txtMobileNo").val();
    var PanNo = $("#txtPanNo").val();
    var AadharNo = $("#txtAadharNo").val();
    var Status = $("#ddlStatus :selected").val();
    
    var filedata = new FormData();
    debugger;



    var AllDataArray = {
        "MainProductId": MainProductId,
        "ProductId": ProductId,
        "LeadNo": LeadNo,
        "CustomerName": CustomerName,
        "MobileNo1": MobileNo1,
        "PanNo": PanNo,
        "AadharNo": AadharNo,
        "Status": Status
    }

    filedata.append('AllDataArray', JSON.stringify(AllDataArray));

    $.ajax({
        url: "/Lead/LeadView",
        type: "POST",
        contentType: false,
        processData: false,
        data: filedata,

        success: function (result) {
            debugger
            /*window.location.reload();*/
            
        }



    })



}


