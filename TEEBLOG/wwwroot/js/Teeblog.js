
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

function addblog() {
    debugger;
    var post = {};
    post.Name = $("#name").val();
    post.CategoryId = $("#categories").val();
    post.ShortDescription = $("#summernote2").val();
    post.Description = $("#summernote").val();
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
                    url: "/Blog/addblog",
                    data: { allPosts: allPost },
                    success: function (result) {
                        debugger
                        if (!result.isError) {
                            message = "Post added successfully."
                            url = "/Blog/AdminBlogPage"
                            newSuccessAlert(result.message, result.url);
                        }
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
                    message = "Your Message was successfully sent. Thank you for Reaching Out."
                    url = "/Contact/AdminContactPage"
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


$(document).ready(function () {
    $('#summernote').summernote({
        height: 300
    });
});

$(document).ready(function () {
    $('#summernote2').summernote({
        height: 100
    });
});

$(document).ready(function () {
    $('#table-show').DataTable();
});



