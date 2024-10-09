
let Login = {

    HandleKeyEvent: function (event) {
        if (event.key === 'Enter') {
            this.AttemptLogin();
        }

    },

    AttemptLogin: function ()  {

        var token = $('input[name="__RequestVerificationToken"]').val();
        if (!$('#submitLoginForm').valid()) {

            $('#submitLoginForm').validate().resetForm();
            return;
        }


        var formData = {
            User: $('#User').val(),
            Password: $('#Password').val(),
            returnUrl: $('#ReturnUrl')
        }

        $.ajax({
            url: 'Account/AttemptLogin',
            type: 'POST',
            data: formData,
            headers: { 'RequestVerificationToken': token },
            success: console.log('login ajax good'),
            error: this.LoginFailed()
        })
    },

    LoginFailed: function () {

        var settings = {
            url: '/Home/Message',
            methodType: 'GET',
            target: `btn-login`,
            modalID: `incorrectLogin`
        }

        AjaxHelpers.LoadModal(settings)
    }
}