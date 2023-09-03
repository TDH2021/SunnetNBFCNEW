
function Validation() {
    var ReqType = "Check";
    var Uname = $("#txtUserName").val();
    var Pass = $("#txtPassword").val();
    //$("#txtUserName").val("");
    //$("#txtPassword").val("");
    if (Uname.length == 0) {
        swal("TDH", "Please Enter User Name", "error");
        return false;
    } else if (Pass.length == 0) {
        swal("TDH", "Please Enter Password", "error");
        return false;
    }
    //else {
    //    var filedata = new FormData();

    //    var UserName = Uname;
    //    var UserPassword = Pass;

    //    var AllDataArray = {
    //        "ReqType": ReqType,
    //        "UserName": UserName,
    //        "UserPassword": UserPassword
    //    }

    //    filedata.append('AllDataArray', JSON.stringify(AllDataArray));

    //    $.ajax({
    //        url: "/Login/ChecKLogin",
    //        type: "POST",
    //        contentType: false,
    //        processData: false,
    //        data: filedata,

    //        success: function (result) {
    //            if (result.length > 2) {
    //                debugger;
    //                if (JSON.parse(result)[0].ChangePasswordYN == "0") {
    //                    $("#HfUserid").val(JSON.parse(result)[0].UserID);
    //                    window.location.pathname = 'Login/ChangePassword';
    //                }
    //                else {

    //                    ReqType = "Update";
    //                    var UserID = JSON.parse(result)[0].UserID;
    //                    var Type = JSON.parse(result)[0].Type;
    //                    var RefID = JSON.parse(result)[0].RefID;

    //                    if (UserID != "") {
    //                        var LoginDetails = {
    //                            "ReqType": ReqType,
    //                            "Type": Type,
    //                            "UserID": UserID,
    //                            "RefID": RefID
    //                        }
    //                        filedata.append('LoginDetails', JSON.stringify(LoginDetails));
    //                        $.ajax({
    //                            url: "/Login/UpdateLogin",
    //                            type: "POST",
    //                            contentType: false,
    //                            processData: false,
    //                            data: filedata,
    //                            success: function (result) {
    //                                debugger
    //                                var msgdata = JSON.parse(result.Data).Msg;
    //                                if (msgdata == "Success") {
    //                                    window.location.pathname = 'Home/Index';
    //                                    //swal({
    //                                    //    title: "Success",
    //                                    //    text: "Login Successfully",
    //                                    //    icon: "success",
    //                                    //    button: true,
    //                                    //})
    //                                    //    .then((willConfirm) => {
    //                                    //        if (willConfirm) {
    //                                    //            window.location.pathname = 'Home/Index';
    //                                    //        }
    //                                    //    });
    //                                } else {
    //                                    swal({
    //                                        title: "Error",
    //                                        text: "Login Error",
    //                                        icon: "error",
    //                                        button: true,

    //                                    })
    //                                        .then((willConfirm) => {
    //                                            if (willConfirm) {

    //                                            }
    //                                        });
    //                                }
    //                            }
    //                        })
    //                    }
    //                }
    //            }
    //            else {
    //                swal("TDH", "Please Enter Coorect UserId and Password", "error");
    //            }

    //        }



    //    })



    //}

}

function ChangePswdValid() {
    var ReqType = "ChgPass";
    var Password = $("#txtPassword").val();
    var ConfirmPass = $("#txtConfPassword").val();
    var UserId = $("#HfUserid").val();
    $("#txtConfPassword").val("");
    $("#txtPassword").val("");
    if (Password.length == 0) {
        swal("TDH", "Please Enter Password", "error");
    } else if (ConfirmPass.length == 0) {
        swal("TDH", "Please Enter Confrim Password", "error");
    }
    else if (Password!= ConfirmPass) {
        swal("TDH", "Both Password Cannot Match", "error");
    }

    else {
        var filedata = new FormData();

        var UserId = UserId;
        var UserPassword = Password;

        var AllDataArray = {
            "ReqType": ReqType,
            "UserID": UserId,
            "UserPassword": Password
        }

        filedata.append('AllDataArray', JSON.stringify(AllDataArray));

        $.ajax({
            url: "/Login/UpdatePassword",
            type: "POST",
            contentType: false,
            processData: false,
            data: filedata,

            success: function (result) {
                debugger;
                if (result.length > 2) {
                    debugger;
                    window.location.pathname = 'Login/Index';
                }
                else {
                    swal("TDH", "Please Enter Coorect Password", "error");
                }

            }



        })



    }
}

function CheckUserName() {
    var ReqType = "ChgPass";
    var Password = $("#txtPassword").val();
    var ConfirmPass = $("#txtConfPassword").val();
    var UserId = $("#HfUserid").val();
    $("#txtConfPassword").val("");
    $("#txtPassword").val("");
    if (Password.length == 0) {
        swal("TDH", "Please Enter Password", "error");
    } else if (ConfirmPass.length == 0) {
        swal("TDH", "Please Enter Confrim Password", "error");
    }
    else if (Password != ConfirmPass) {
        swal("TDH", "Both Password Cannot Match", "error");
    }

    else {
        var filedata = new FormData();

        var UserId = UserId;
        var UserPassword = Password;

        var AllDataArray = {
            "ReqType": ReqType,
            "UserID": UserId,
            "UserPassword": Password
        }

        filedata.append('AllDataArray', JSON.stringify(AllDataArray));

        $.ajax({
            url: "/Login/UpdatePassword",
            type: "POST",
            contentType: false,
            processData: false,
            data: filedata,

            success: function (result) {
                debugger;
                if (result.length > 2) {
                    debugger;
                    window.location.pathname = 'Login/Index';
                }
                else {
                    swal("TDH", "Please Enter Coorect Password", "error");
                }

            }



        })



    }
}

function ForgetPassword() {
    var ReqType = "ForgetPss";
    var Password = $("#txtPassword").val();
    var ConfirmPass = $("#txtConfPassword").val();
    var UserName = $("#txtUserName").val();
    $("#txtConfPassword").val("");
    $("#txtPassword").val("");
    $("#txtUserName").val("");
    if (UserName.length == 0) {
        swal("TDH", "Please Enter UserName", "error");
    }
    else if (Password.length == 0) {
        swal("TDH", "Please Enter Password", "error");
    } else if (ConfirmPass.length == 0) {
        swal("TDH", "Please Enter Confrim Password", "error");
    }
    else if (Password != ConfirmPass) {
        swal("TDH", "Both Password Cannot Match", "error");
    }

    else {
        var filedata = new FormData();

        var UserName = UserName;
        var UserPassword = Password;

        var AllDataArray = {
            "ReqType": ReqType,
            "UserName": UserName,
            "UserPassword": Password
        }

        filedata.append('AllDataArray', JSON.stringify(AllDataArray));

        $.ajax({
            url: "/Login/UpdatePassword",
            type: "POST",
            contentType: false,
            processData: false,
            data: filedata,

            success: function (result) {
                debugger;
                if (result.length > 2) {
                    debugger;
                    window.location.pathname = 'Login/Index';
                }
                else {
                    swal("TDH", "Please Enter Coorect Id or Password", "error");
                }

            }



        })



    }
}