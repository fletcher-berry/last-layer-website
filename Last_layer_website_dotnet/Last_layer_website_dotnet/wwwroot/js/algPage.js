$(document).ready(function (e) {
    var showSources = false;
    console.log("working");
    $("#show_sources_button").click(() => {
        console.log("clicked");
        showSources = !showSources;
        if (showSources) {
            $("#show_sources_button").text("Hide Sources");
        }
        else {
            $("#show_sources_button").text("Show Sources");
        }
    });
})