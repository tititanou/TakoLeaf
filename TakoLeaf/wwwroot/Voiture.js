////$("#ddlmarques").change(function () {
////    var valueSelected = $("#ddlmarques").val();
////    if (valueSelected == "Audi") {
////        var myOptions = {
////            val1: 'A2',
////            val2: 'A4'
////        };
////        var mySelect = $('#ddlmodeles');
////        mySelect.find('option').remove().end();
////        $.each(myOptions, function (val, text) {
////            mySelect.append(
////                $('<option></option>').val(val).html(text)
////            );
////        });


////    }   

////});

$("#ddlmarques").change(function () {
    var valueSelected = $("#ddlmarques").val();
    $.ajax({
        type: "GET",
        dataType: "json",
        contentType: "application/json",
        url: '/Login/GetModeles',
        data: { marque: valueSelected },
        success: function (data) {
            //remove disabled from province and change the options
            var mySelect = $('#ddlmodeles');
            mySelect.find('option').remove().end();
            $.each(data, function (index) {
                mySelect.append(
                    $('<option></option>').val(data[index]['nom']).html(data[index]['nom'])
                );
            });
        }
    });

});