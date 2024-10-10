let Index = {

    HandleKeyEvent: function (event) {

        if (event.key === 'Enter') {

            event.preventDefault();
            var id = event.currentTarget.dataset.id

            if (event.currentTarget.closest('#row-create') !== null) {

                document.querySelector('#row-create #save-btn').click();
            } else 
            document.querySelector(`#row-${id}[data-type="edit"] #save-btn`).click();
            
        }
    },

    Edit: function (button) {

        //Set up Variables
        var id = button.dataset.id;
        var otherDisplayRows = Array.from(document.querySelectorAll(`tr[data-type="display"]`)).filter((row) => row.dataset.id !== id);
        var otherEditRows = Array.from(document.querySelectorAll(`tr[data-type="edit"]`)).filter((row) => row.dataset.id !== id);

        //Reset Validation Error If Exists
        document.querySelector(`#row-${id}[data-type="edit"] input`).classList.remove('input-validation-error');

        if (document.querySelector(`#row-${id}[data-type="edit"] .field-validation-error`)) { 
            document.querySelector(`#row-${id}[data-type="edit"] .field-validation-error`).classList.remove('input-validation-error');
        }

        //Reset Other Rows
        otherDisplayRows.forEach((row) => row.style.display ='table-row')
        otherEditRows.forEach((row) => row.style.display ='none')

        //Reset Create Row
        document.getElementById('row-create').style.display = "none";
        document.getElementById('row-create-btn').style.display = "table-row";

        //Display Edit, Hide Display
        document.querySelector(`#row-${id}[data-type="display"]`).style.display= 'none';
        document.querySelector(`#row-${id}[data-type="edit"]`).style.display = 'table-row';
    },

    Create: function (button) {

        //Reset Other Rows
        document.querySelectorAll('tr[data-type="edit"]').forEach(row => row.style.display = "none")
        document.querySelectorAll('tr[data-type="display"]').forEach(row => row.style.display = "table-row")

        //Show Create Options
        document.querySelector('#row-create').style.display = "table-row";
        document.querySelector('#row-create-btn').style.display = "none";
    },

    Delete: function (button) {

        var id = button.dataset.id

        //Call Confirmation Message View
        var formData = { ID: id }
        
        var settings = {
            url: '/Home/ConfirmDelete',
            methodType: 'POST',
            data: formData,
            target: `#row-create`,
            modalID: `confirmDelete-${id}`
        }

        AjaxHelpers.LoadModal(settings)
    },

    Cancel: function (button) {

        var id = button.dataset.id;

        //If Create Row Is Cancelled, Reset it
        if ((button.parentElement).parentElement.id === 'row-create') {

            document.querySelector(`#row-create`).style.display = "none";
            document.querySelector(`#row-create-btn`).style.display = "table-row";
        }
        else {
            //Otherwise Reset Edited Row
            document.querySelector(`#row-${id}[data-type="edit"]`).style.display = "none";
            document.querySelector(`#row-${id}[data-type="display"]`).style.display = "table-row";
        }
        $(`#confirmDelete-${id}`).modal('hide')
    },

    EditClient: function (button) {

        var id = button.dataset.id
        var token = document.querySelector('input[name="__RequestVerificationToken"]').value

        //Check Form Validation
        if (!$('#submit-form').valid()) {
            return;
        }
        var formData = {
            ID: id,
            Name: document.querySelector(`#row-${id}[data-type="edit"] input`).value,
            Type: document.querySelector(`#row-${id}[data-type="edit"] select`).value
        }

        $.ajax({
            url:'Submission',
            type:'POST',
            data: formData,
            headers: { 'RequestVerificationToken': token },
            success: (response) => {

                //Change Row Values and Hide Edit Options
                document.querySelector(`#row-${id}[data-type="display"] #display-id`).innerHTML = formData.ID;
                document.querySelector(`#row-${id}[data-type="display"] #display-name`).innerHTML = formData.Name;
                document.querySelector(`#row-${id}[data-type="display"] #display-type`).innerHTML = formData.Type;

                document.querySelector(`#row-${id}[data-type="display"]`).style.display= "table-row";
                document.querySelector(`#row-${id}[data-type="edit"]`).style.display = "none";
            },
            error: (response) => alert(response.message)
        })
    },

    CreateClient: function (button) {

        var id = document.getElementById('create-id').innerHTML

        //Check Form Validation
        if (!$('#submit-form').valid()) {
            return;
        }

        var token = document.querySelector('input[name="__RequestVerificationToken"]').value

        var formData = {
            ID: 0,
            Name: document.getElementById(`create-name`).value,
            Type: document.getElementById(`create-type`).value
        }

        var settings = {
            url: 'Submission',
            methodType: 'POST',
            data: formData,
            token: token,
            target: "#row-create",
            appendType: 'before',
            viewResponse: "ClientSlice",
            PartialViewMapping: {
                ID: id,
                Name: document.getElementById(`create-name`).value,
                Type: document.getElementById(`create-type`).value
            }
        }

        //Insert New Slice
        AjaxHelpers.LoadPartial(settings)
        id++;
        document.getElementById('create-id').innerHTML = id;
        document.getElementById('row-create').style.display = 'none';
        document.getElementById('row-create-btn').style.display = 'table-row';

    },

    ConfirmDelete: function (button) {

        var id = button.dataset.id

        //Check Form Validation
        if (!$('#submit-form').valid()) {
            return;
        }

        var token = document.querySelector('input[name="__RequestVerificationToken"]').value

        var formData = { ID: id }

        $.ajax({
            url: 'Delete',
            type: 'POST',
            data: formData,
            headers: { 'RequestVerificationToken': token },
            success: (response) => {
                //Hide Row
                document.querySelector(`#row-${id}[data-type="display"]`).style.display = "none";
                document.querySelector(`#row-${id}[data-type="edit"]`).style.display = "none";
                $(`#confirmDelete-${id}`).modal('hide')
            },
            error: (response) => alert(response.message)
        })
    },

    Initialize: function () {

        document.querySelectorAll('#edit-btn').forEach((button) => button.addEventListener('click', () => Index.Edit(button)))

        document.querySelectorAll('#create-btn').forEach((button) => button.addEventListener('click', () => Index.Create(button)))

        document.querySelectorAll('#delete-btn').forEach((button) => { if (button.closest('#confirmDelete') === null) button.addEventListener('click', () => Index.Delete(button))})

        document.querySelectorAll('#cancel-btn').forEach((button) => button.addEventListener('click', () => Index.Cancel(button)))

        document.querySelectorAll('#save-btn').forEach((button) => {if(button.closest('#row-create') === null) button.addEventListener('click', () => Index.EditClient(button))})

        document.querySelectorAll('#row-create #save-btn').forEach((button) => button.addEventListener('click', () => Index.CreateClient(button)))

        document.querySelectorAll('#confirmDelete #delete-btn').forEach((button) => button.addEventListener('click', () => Index.DeleteClient(button)))

        document.querySelectorAll('input').forEach((input) => input.addEventListener('keydown', Index.HandleKeyEvent))
    }
}

document.addEventListener('DOMContentLoaded', Index.Initialize)