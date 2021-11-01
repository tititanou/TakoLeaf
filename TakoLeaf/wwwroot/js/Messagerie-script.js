$("#all").change(function () {
    if ($("#all").is(':checked')) {
        $(".messCB").prop("checked", true);
    }
    else {
        $(".messCB").prop("checked", false);
    }
});

$("#rep").on("click", function () {
    $("#reponse").css({ display: "block" });
});