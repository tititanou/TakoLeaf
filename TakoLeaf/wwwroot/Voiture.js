$("#ddlmarques").change(function () {
    var valueSelected = $("#ddlmarques").val();
    if (valueSelected == "Audi") {
        var myOptions = {
            val1: 'A2',
            val2: 'A4'
        };
        var mySelect = $('#ddlmodeles');
        mySelect.find('option').remove().end();
        $.each(myOptions, function (val, text) {
            mySelect.append(
                $('<option></option>').val(val).html(text)
            );
        });


    }
    //if (valueSelected == "Autres") {
    //    $("#marque".show);
    //}

    //else {
    //    $("#marque".hide);
    //}


    //function GetAllCards() {
    //    $.ajax({
    //        url: 'http://{Server Address}/{WebAppName}/api/Cards',
    //        type: 'GET',
    //        dataType: 'json',
    //        success: function (data) {
    //            //do something with data
    //        },
    //        error: function (error) {
    //            //log or alert the error
    //        }
    //    });
    //}

});