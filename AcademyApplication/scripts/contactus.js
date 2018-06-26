function myMap() {
    var mapOptions = {
        center: new google.maps.LatLng(12.8347513, 79.7041187),
        zoom: 10,
        mapTypeId: google.maps.MapTypeId.HYBRID
    }
    var map = new google.maps.Map(document.getElementById("map"), mapOptions);
}

$("#submitValues").click(function()
{
    if (validate()) {
        var data = {
            Name: $("#Name").val(),
            Email: $("#EmailAddress").val(),
            Phone: $("#Phone").val(),
            Info: $("#Info").val()
        }
        $.ajax({
            type: "POST",
            url: "/contactformsubmission",
            data: data,
            success: function (result) {
                if (result != null && result=="saved") {
                    alert("Thanks for contacting us. We will contact you soon.")
                    window.location = "/contact";
                }
                else {
                    alert("!Error occured while saving details to database")
                }
            },

        });

    }
    else {
        alert("Please fill all the values");
    }
})
function validate()
{
    if($("#Name").val() != "" && $("#EmailAddress").val() != "" && $("#Phone").val() != "")
    {
        return true;
    }

    return false;
}