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
    },

    EditItem: function (button) 
    {
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
        }

        AjaxHelpers.Post('Submission', formData, success, error);
    },

    CreateItem: function (button) 
    {
        if (!$('#submitForm').valid()) {
            return;
        }
        //Create Form Data
        var id = $('#create-id').text().trim();
        var name = $('#create-name input').val();
        var price = $('#create-price input').val();
        var formData = {
            ID: id,
            Name: name,
            Price: price
        }

        var success = function (response) {

            var newSlice = {
                ID: response.data.model.id,
                Name: response.data.model.name,
                Price: parseFloat(response.data.model.price).toFixed(2)
            }

            var InsertSlice = function (response) {

                $('#create-btn').before(response);
            }

            id = response.data.maxID + 1;
            
            $('#create-id').text(id);
            $('#create-btn').show();
            $('#create').hide();
           
            AjaxHelpers.Post("ItemSlice", newSlice, InsertSlice, function (response) { alert('baddd') });

        }

        var error = function (response) {
            alert('Ajax Failure.');
        }

        AjaxHelpers.Post("Submission",formData, success, error);
    },

    DeleteItem: function (button) 
    {
        var id = button.dataset.id;

        //Create Model Form
        var formData = {
            id:id,
        }

        var success = function (response) {

            $('#row-' + id).hide();
        }

        var error = function (response) {
            alert('Ajax Failure.');
        }

        AjaxHelpers.Post('Delete', formData, success, error);
    }
}