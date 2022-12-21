
// Write your JavaScript code.

function login() {
    debugger;
    var data = {};
    data.Email = $("#email").val();
    data.Password = $("#password").val();
    if (data.Email != "" && data.Password != "") {
        var details = JSON.stringify(data);
        $.ajax({
            type: "POST",
            dataType: "Json",
            url: "/Account/Login",
            data: { detail: details },
            success: function (result) {
                if (!isError) {

                }
                else {

                }
            },
            error: function () {

            }
        });
    }
}




