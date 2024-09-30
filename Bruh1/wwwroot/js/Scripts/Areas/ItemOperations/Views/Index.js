let Index = {

    HandleKeyEvent: function (event, func, id=-1)
    {
        if (event.key === 'Enter')
        {
            if (id !== -1) {
                var button = $('#row-' + id + ' #save-btn')[0];
                Index[func](button);
            }
            else Index[func](id);
        }

    },

    Edit: function (button)
    {
        var id = button.dataset.id;

        //Reset Form Validation Message if it exists
        $('#row-' + id + ' input').removeClass('input-validation-error');
        $('#row-' + id + ' .field-validation-error').removeClass('field-validation-error').addClass('field-validation-valid').empty();


        //Hide Other Opened Rows.
        $('tr[data-type="edit"]').not('#row-' + id).hide();

        //Show The previously hidden Records
        $('tr[data-type="display"]').not('#row-' + id).show();

        //Hide Create Inputs
        $('#create').hide();

        //Hide This Record
        $('#row-' + id + '[data-type="display"]').hide();
       
        //Show Edit Options
        $('#row-' + id + '[data-type="edit"]').show();
        
        $('#create-btn').show();
    },

    Create: function (button) 
    {
        var id = button.dataset.id;

        // Reset validation only when switching rows
        $('#create input').removeClass('input-validation-error'); // Remove error classes
        $('#create .field-validation-error').removeClass('field-validation-error').addClass('field-validation-valid').empty(); // Clear error messages

        //Hide Other Edit  Options
        $('tr[data-type="edit"]').hide();

        //Show Hidden Records
        $('tr[data-type="display"]').show();
        
        //Show Create Options
        $('tr#create').show();
        $('#create-btn').hide();
    },

    Cancel: function (button) 
    {
        var id = button.dataset.id;

        // Reset validation only when switching rows
        $('#row-' + id + ' input').removeClass('input-validation-error'); // Remove error classes
        $('#row-' + id + ' .field-validation-error').removeClass('field-validation-error').addClass('field-validation-valid').empty();

        var name = $('#row-' + id + ' #display-name').text();
        var price = $('#row-' + id + ' #display-price').text();

        //Hide Edit Options and reset values
        $('#row-' + id + '[data-type="edit"]').hide();
        $('#row-' + id + ' #edit-name').val(name);
        $('#row-' + id + ' #edit-price').val(price);


        //Show Record
        $('#row-' + id + '[data-type="display"]').show();

        //Hide Create Options if Shown
        $('#create-btn').show();
        $('tr#create').hide();

        $('#confirmDelete-' + id).modal('hide');
    },

    EditItem: function (button) 
    {
        var token = $('input[name="__RequestVerificationToken"]').val();
        if (!$('#submitForm').valid())
        {
            return;
        }
        var id = button.dataset.id;
        //Get Data And Convert It To A Form
        var formData = {
            ID: id,
            Name: $('#row-' + id + ' #edit-name').val(),
            Price: $('#row-' + id + ' #edit-price').val()
        }

        var headers = {'RequestVerificationToken' : token}

        //Ajax Success Callback
        var success = function (response) {

            //Update And Show Record
            $('#row-' + id + ' #display-name').text(formData.Name);
            $('#row-' + id + ' #display-price').text(parseFloat(formData.Price).toFixed(2));
            $('#row-' + id + '[data-type="display"]').show();

            //Hide Edit Options
            $('#row-' + id + '[data-type="edit"]').hide();
        }

        //Ajax Error Callback
        var error = function (response) {
            alert('Ajax Failure.');
            console.log('oops ' + response)
        }

        AjaxHelpers.Post('Submission', formData, success, error, headers);
    },

    CreateItem: function (button) 
    {
        var token = $('input[name="__RequestVerificationToken"]').val();

        var id = $('#create-id').text().trim();
        var name = $('#create-name input').val();
        var price = $('#create-price input').val();

        if (!$('#submitForm').valid()) {
            return;
        }

        var formData = {
            ID: id,
            Name: name,
            Price: price
        }

        var settings = {
            url: 'Submission',
            methodType: 'POST',
            data: formData,
            token: token,
            target: '#create-btn',
            appendType: 'before',
            view: 'ItemSlice',
            viewType: 'POST',
            PartialViewMapping: formData
        }

        AjaxHelpers.LoadPartial(settings);
        id++;
        $('#create-id').text(id);
        $('#create-btn').show();
        $('#create').hide();
        
    },

    Delete: function (button)
    {
        var id = button.dataset.id;

        var formData = { ID: id }
        $.ajax({
            url:"ConfirmDelete",
            type: "POST",
            data: formData,
            success: (response) => { $('#row-' + id).before(response); $('#confirmDelete-' + id).modal(); $('#confirmDelete-' + id).modal('show'); },
            error: (response) => console.log(response)
        })
        
    },

    DeleteItem: function (button) 
    {
        var id = button.dataset.id;
        var token = $('input[name="__RequestVerificationToken"]').val();

        //Create Model Form
        var formData = {
            id:id,
        }

        var headers = {'RequestVerificationToken': token}

        var success = function (response) {

            $('#row-' + id).hide();
            $('#confirmDelete-' + id).modal('hide');
        }

        var error = function (response) {
            alert('Ajax Failure.');
        }

        AjaxHelpers.Post('Delete', formData, success, error, headers);
    }
}