
let Login = {

    HandleKeyEvent: function (event) {
        if (event.key === 'Enter') {
            Login.AttemptLogin();
        }

    },

    AttemptLogin: function ()  {

        var token = $('input[name="__RequestVerificationToken"]').val();
        if (!$('#submitLoginForm').valid()) {

            $('#submitLoginForm').validate().resetForm();
            return;
        }
        else { 

            var formData = {
                User: $('#User').val(),
                Password: $('#Password').val(),
                returnUrl: $('#ReturnUrl').val()
            }

            $.ajax({
                url: '/Account/AttemptLogin',
                type: 'POST',
                data: formData,
                headers: { RequestVerificationToken: token },
                success: (response) => {

                    if (response.isSuccess) {

                        window.location.href = response.data.redirectUrl || '/'
                    } else {
                        Login.LoginFailed()
                    }
                },
                error: (response) => Login.LoginFailed()
            })
        }
    },

    LoginFailed: function () {

        var settings = {
            url: '/Home/LoginFailed',
            methodType: 'GET',
            target: `#login-btn`,
            modalID: `incorrectLogin`
        }


        AjaxHelpers.LoadModal(settings)

    },

    HideModal: function () {

        $(`#incorrectLogin`).modal('hide')
    },

    Initialize: function () {

        document.querySelectorAll('input').forEach((input) => input.addEventListener('keydown', Login.HandleKeyEvent))

        document.querySelector('#login-btn button').addEventListener('click', () => Login.AttemptLogin());
    }
}

document.addEventListener('DOMContentLoaded', Login.Initialize)