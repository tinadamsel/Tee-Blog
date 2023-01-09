
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
                debugger;
                if (!result.isError) {
                    //location.href = result.url;
                    newSuccessAlert(result.message, result.url);
                }
                else {

                }
            },
            error: function () {

            }
        });
    }
}

function register() {

    debugger;
    var person = {};
    person.FirstName = $("#firstName").val();
    person.LastName = $("#lastName").val();
    person.Email = $("#email").val();
    person.Password = $("#password").val();
    person.ConfirmPassword = $("#confirmPassword").val();
    if (person.FirstName != "" && person.LastName != "" && person.Email != "" && person.Password != "" && person.ConfirmPassword != "") {
        var allDetails = JSON.stringify(person);
        $.ajax({
            type: "POST",
            dataType: "Json",
            url: "/Account/Register",
            data: { allDetail: allDetails },
            success: function (result) {
                debugger;
                if (!result.isError) {
                    message = "Registration successful. Login to your email to verify account."
                    url = "/Account/login"
                    newSuccessAlert(result.message, result.url);
                }
                else {
                    message = " Check your details before clicking register",
                        infoAlert(message)
                }
            },
            error: function () {
                message = "Please fill the form correctly",
                    errorAlert(message);
            }
        })
    }
}




