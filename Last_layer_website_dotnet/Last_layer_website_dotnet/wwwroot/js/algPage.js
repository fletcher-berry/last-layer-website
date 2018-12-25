$(document).ready(function (e) {
    showSources = false;
    var setSourceButtonText = function () {
        if (showSources) {
            $("#show_sources_button").text("Hide Sources");
            $(".Sources").show();
        }
        else {
            $("#show_sources_button").text("Show Sources");
            $(".Sources").hide();
        }
    };

    if (showSources) {
        $(".Sources").show();
    }


    console.log("working");
    $("#show_sources_button").click(() => {
        console.log("clicked");
        showSources = !showSources;
        setSourceButtonText();
    });

    
})