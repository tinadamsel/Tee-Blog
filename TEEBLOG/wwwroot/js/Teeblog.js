
// Write your JavaScript code.

function login() {
    debugger;
    var data = {};
    data.Email = $("#email").val();
    data.Password = $("#password").val();
    if (data.Email != "" && data.Password != "") {
        let details = JSON.stringify(data);
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
                    message = "Email or password is incorrect",
                        infoAlert(message);
                }
            },
            error: function () {
                message = "Please proceed to your email to verify your account.",
                    errorAlert(message);
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

    if (person.FirstName == "" || person.FirstName == undefined && person.LastName == "" || person.LastName == undefined) {
        validateForm("firstName", "Lirst name cannnot be empty");
        validateForm("lastName", "Last name cannnot be empty");
    }
    //if (person.LastName == "" || person.LastName == undefined) {
    //    validateForm("lastName", "Last name cannnot be empty");
    //}
    if (person.ConfirmPassword !== person.Password) {
        message = "Password do not match",
            infoAlert(message);
    }
    if (person.FirstName != "" && person.LastName != "" && person.Email != "" && person.Password != "" && person.ConfirmPassword != "")
    {
          var allDetails = JSON.stringify(person);
          $.ajax({
             type: "POST",
              dataType: "Json",
              url: "/Account/Register",
              data: { allDetail: allDetails },
              success: function (result) {
                    debugger;
                    if (!result.isError) {
                        message = "Registration successful. Login to your email to verify account.",
                            url = "/Account/login",
                            newSuccessAlert(result.message, result.url);
                    }
                    else {
                        message = "Check your details before clicking register",
                            infoAlert(message);
                    }
                },
              error: function () {
                 message = "Please fill the form correctly", 
                    errorAlert(message);
              }
          })
    }
    //else if (person.ConfirmPassword !== person.Password) {
    //        message = "Password do not match",
    //            infoAlert(message);
    //}

}
function validateForm(id, message)
{
    $("#" + id + "Error").text(message).css({color:"red"});
    $("#" + id).css({ border:"2px solid red" });
}

function addblog() {
    debugger;
    var post = {};
    post.Name = $("#name").val();
    post.CategoryId = $("#categories").val();
    post.ShortDescription = $("#shortDes").val();
    post.Description = $("#blogContent").val();
    var blogPix = document.getElementById("picture").files; 
    debugger
    if (post.Name != "" && post.Shortdescription != "" && post.Description != "" && blogPix[0] != null && post.Category != "") {
        if(blogPix[0] != null) {
            const reader = new FileReader();
            reader.readAsDataURL(blogPix[0]);
            reader.onload = function () 
            {
                post.Picture = reader.result;
                let allPost = JSON.stringify(post);
                $.ajax({
                    type: "POST",
                    dataType: "Json",
                    url: "/Blog/Addblog",
                    data: { allPosts: allPost },
                    success: function (result) {
                        debugger
                        if (!result.isError) {
                            message = "Post added successfully."
                            url = result.url;
                            newSuccessAlert(result.message, url);
                        }
                        else {
                            message = "Your details are not Complete",
                                infoAlert(message);
                        }
                    },
                    error: function() {
                        message = "Incomplete Details",
                            errorAlert(message);
                    }

                })
            }
            
        }

    }
}

function AddSupport() {

    debugger;
    var support = {}; 
    support.Name = $("#name").val();
    support.Email = $("#email").val();
    support.Subject = $("#subject").val();
    support.Message = $("#message").val();
    if (support.Name != "" && support.Email != "" && support.Subject != "" && support.Message != "") {
        var supports = JSON.stringify(support);
        $.ajax({
            type: "POST",
            dataType: "Json",
            url: "/Contact/AddSupport",
            data: { allSupport: supports },
            success: function (result) {
                debugger;
                if (!result.isError) {
                    newSuccessAlert(result.message, result.url);
                }
                else {
                    message = " Sorry! Message not sent",
                        infoAlert(message)
                }
            },
            error: function () {
                message = "Your message could not be sent",
                    errorAlert(message);
            }
        })
    }
}


function AddReview(id) {
    debugger;
    var review = {};
    review.Name = $('#name').val();
    review.Email = $('#email').val();
    review.Message = $('#message').val();
    review.BlogId = id;
    debugger;
    if (review.Name != "" && review.Email != "" && review.Message != "") {
        let myreview = JSON.stringify(review);
        $.ajax({
            type: 'POST',
            dataType: 'Json',
            url: '/Reviews/AddReview',
            data: { allReview: myreview },
            success: function (result) {
                debugger;
                if (!result.isError) {
                    message = "Review Successfully Submitted. Thank you!"
                    newSuccessAlert(result.message, result.url);
                }
                else {
                    message = " Sorry! Review not sent",
                        errorAlert(message)
                }
            },
            error: function () {
                message = "Your review could not be sent",
                    errorAlert(message);
            }
        });
    }
}


//function EditpayForm(id) {
//    debugger;
//    $.ajax({
//        type: 'GET',
//        url: '/Admin/GetEditPayment', // we are calling json method
//        dataType: 'json',
//        data:
//        {
//            id: id
//        },
//        success: function (result) {
//            debugger
//            if (!result.isError) {
//                debugger
//                $("#deleteId").val(result.result.id);
//                $("#editId").val(result.result.id);
//                $("#editpatname").val(result.result.PatientName);
//                $("#editmodofpay").val(result.result.ModeOfPay);
//                $("#editproof").val(result.result.Proof);
//                $("#editpayday").val(result.result.PaymentDate);
//                $("#editpaidamount").val(result.result.PaidAmount);
//            }
//        },
//        error: function (ex) {
//            "please fill the form correctly" + errorAlert(ex);
//        }
//    });
//};

function Description(id) {
    debugger;
    $.ajax({
        type: "GET",
        url: '/Blog/Description/' + id,
        dataType: "jason",
        data: { id: id },
        success: function (data) {
            $('.modal-body').html(data);
            $("#myDescription").modal("show");

        }

    })

}

function Description(id) {
    debugger;
    $.ajax({
        type: "GET",
        url: '/Blog/GetDescription', // we are calling json method
        dataType: 'json',
        data:
        {
            id: id
        },
        success: function (result) {
            debugger
            if (!result.isError) {
                $("#descid").val(result.data.id);
                $('#desc').summernote('code', result.data.shortDescription);
                $('.modal-body').text(data);
                
            }
        },
        error: function (ex) {
            "No Description Found" + errorAlert(ex);
        }
    });
};

function Message(id) {
    debugger;
    $.ajax({
        type: "GET",
        url: '/Contact/GetMessage', // we are calling json method
        dataType: 'json',
        data:
        {
            id: id
        },
        success: function (result) {
            debugger
            if (!result.isError) {
                $("#msgid").val(result.data.id);
                $("#msg").val(result.data.message);
                $('.modal-body').text(data);

            }
        },
        error: function (ex) {
            "No Message Found" + errorAlert(ex);
        }
    });
};

function ConvertPictureToBase64(picture) {
    var pix = picture;
    var result = "";
    if (pix[0] != null) {
        const reader = new FileReader();
        reader.readAsDataURL(pix[0]);
        reader.onload = function () {
            result = reader.result;
        }
    }
    return result;
}


//$(document).ready(function () {
//  $('#tableShow').DataTable();
//});
    




