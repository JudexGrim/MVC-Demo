window.AjaxHelpers = {
    Get: function (url, successCallback, errorCallback)
    {
        $.ajax({
            url: url,
            type: "GET",
            success: successCallback,
            error: errorCallback
        })
    },
    Post: function (url, data, successCallback, errorCallback)
    {
        $.ajax({
            url: url,
            type: "POST",
            data: data,
            success: successCallback,
            error: errorCallback
        })
    },


    LoadPartial: function (data)
    {
        var settings = {
            url: '',
            methodType : 'GET',
            data : null,
            appendtype : 'after'
        }

        try
        {
           //Check That an action url Exists
            if (data.url && data.target && data.appendType) {

                //Append View Function
                var AppendView = function (appendType, view, target) {
                    switch (appendType.toLowerCase()) {

                        case 'before': $(target).before(view);
                            break;

                        case 'after': $(target).after(view);
                            break;

                        case 'insert': $(target).append(view);
                            break;
                        default: $(target).html(view);
                            break;
                    }
                }

                var ajaxParams = { ...settings, ...data }

                    //Make Sure Method Type is Valid
                if (['GET', 'POST', 'PUT', 'DELETE'].includes(ajaxParams.methodType.toUpperCase())) {

                    //Check if Request Wants to Submit data
                    if (ajaxParams.data && ajaxParams.view) {

                        //Submit Ajax Call
                        $.ajax({
                            url: ajaxParams.url,
                            type: ajaxParams.methodType,
                            success: function (response) {

                                //After Submission, Load Partial View
                                $.ajax({
                                    url: ajaxParams.view,
                                    type: 'GET',
                                    success: function (response) {
                                        AppendView(ajaxParams.appendType, response, ajaxParams.target)
                                    },
                                    error: (response) => { alert("Error Loading Partial View: " + response.message)}
                                })
                            },
                            error: (response) => { alert("Eror Submitting Data: " + response.message) }
                        })
                    }
                    else {
                        

                        //Load Partial View
                        $.ajax({
                            url: ajaxParams.view,
                            type: 'GET',
                            success: function (response) {
                                AppendView(ajaxParams.appendType, response, ajaxParams.target)
                            },
                            error: (response) => { alert("Error Loading Partial View: " + response.message) }
                        })
                    }

                } else throw Error("AJAX Method Type Invalid.")
                
                
            }
            
            else throw Error('Provided Settings Invalid. Please Provide an Action URL.')
        
        
        } catch (error) {
            console.error(error.message)
        }
       
    }
    
}