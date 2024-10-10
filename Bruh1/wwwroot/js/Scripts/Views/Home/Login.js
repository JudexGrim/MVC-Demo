
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
                headers: { 'RequestVerificationToken': token },
                success: (response) => { console.log(response) },
                error: (response) => { console.log(response) }
            })
        }
    },

    LoginFailed: function () {

        var settings = {
            url: '/Home/LoginFailed',
            methodType: 'GET',
            target: `btn-login`,
            modalID: `incorrectLogin`
        }

        alert('bad login');

    //    AjaxHelpers.LoadModal(settings)
    }
}