function Export() {

        $.ajax({
            url: "/City/ExportCity",
            type: "POST",
            contentType: false,
            processData: false,
            
            success: function (result) {
                debugger
            }
        })
    }



