window.AjaxHelpers = {
    Get: function (url, successCallback, errorCallback)
    {
        $.ajax({
            url: url,
            type: 'GET',
            success: successCallback,
            error: errorCallback
        })
    },
    Post: function (url, data, successCallback, errorCallback, token=null)
    {
        $.ajax({
            url: url,
            type: 'POST',
            data: data,
            headers: token,
            success: successCallback,
            error: errorCallback
        })
    },
   
    LoadPartial: function (data)
    {
        var settings = {
            url: '',
            methodType : 'GET',
            data: null,
            token: null,
            target:null,
            appendType: 'after',
            viewResponse: null,
            PartialViewMapping: null
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
                    if (ajaxParams.data && ajaxParams.viewResponse) {

                        //Submission Ajax Call
                        $.ajax({
                            url: ajaxParams.url,
                            type: ajaxParams.methodType,
                            data : ajaxParams.data,
                            headers: {'RequestVerificationToken' : ajaxParams.token},
                            success: function (response) {

                                //After Submission, Load Partial View

                                //If The Partial View requirs a model, map one.
                                if (ajaxParams.PartialViewMapping) {

                                    $.ajax({
                                        url: ajaxParams.viewResponse,
                                        type: 'POST',
                                        data: ajaxParams.PartialViewMapping,
                                        success: function (response) {
                                            AppendView(ajaxParams.appendType, response, ajaxParams.target)
                                        },
                                        error: (response) => { alert("Error Loading Partial View: " + response.message) }
                                    })
                                }
                                //If No Mapping Is Needed, Just Do a normal return
                                else 
                                $.ajax({
                                    url: ajaxParams.viewResponse,
                                    type: 'GET',
                                    success: function (response) {
                                        AppendView(ajaxParams.appendType, response, ajaxParams.target)
                                    },
                                    error: (response) => { alert("Error Loading Partial View: " + response.message) }
                                })
                               
                            },
                            error: (response) => { alert("Eror Submitting Data: " + response.message) }
                        })
                    }
                    else {
                        

                        //Load Partial View
                        $.ajax({
                            url: ajaxParams.viewResponse,
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