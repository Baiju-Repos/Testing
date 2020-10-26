function displayAlerts(message, type) {
    var className = type == null ? "alert-warning" : type ? "alert-success" : "alert-danger";
    $("#closeAlert").attr("data-class", className);
    $("#alertMessage").empty().html(message);
    $("#alert_message").css({ "height": 70, "padding": 30, "z-index": 9999 }).addClass(className).addClass("show").show();
    $("#closeAlert").show();
    setTimeout(function () {
        $("#alert_message").css({ "height": 0, "padding": 0, "z-Index": 0 }).hide().removeClass(className).removeClass("show");
        $("#closeAlert").hide();
        $("#alertMessage").empty();
    }, 7000);
}
//To close the alert displayed at the top of the page
function closeAlert() {
    $("#alert_message").css({ "height": 0, "padding": 0, "z-Index": 0 }).hide().removeClass($("#closeAlert").attr("data-class")).removeClass("show");
    $("#closeAlert").hide();
    $("#alertMessage").empty();
}