/*function myMap() {                                                      //Creates the map
    var myCenter = new google.maps.LatLng(45.5532695, -94.1529164);     //Var to hold the center of the map
    var mapCanvas = document.getElementById("map");                     //Var for the canvas
    var mapOptions = { center: myCenter, zoom: 12 };                    //Object for the center and zoom level
    var map = new google.maps.Map(mapCanvas, mapOptions);               //Actually creates the map with specified canvas and options
    var marker = new google.maps.Marker({ position: myCenter });        //Create the marker
    marker.setMap(map);                                                 //Sets the marker at the specified position
}*/

/*function onSubmitForm() {                                               //Fn is called when the user tries to submit the form
    if (validatePasswdConditions()) {                                   //Check if password met required conditions
        if ($("#YesRbtn")[0].checked) {                                 //Check if the user agreed to terms 
            if ($("input:disabled").length) { //If there is a disabled element (namely, the age field), then use ContactBase which does not have the corresp field
                document.contactForm.action = "ContactBase";
            } else { //Else, use Contact, which has an Age field and validation will take place
                document.contactForm.action = "Contact";
            }
            return true;                //Must return true regardless, true allows form submission to appropriate action
        } else {
            $("#NoRbtn").tooltip('toggle'); //Since the ifs are nested, we don't get to this condition, we exit the fn before, if passwd conditions arent met. Thus we get to this only after passwd is met
            return false;
        }
    } else {
        $("#Password").tooltip('toggle');           //We first validate the password. If validation failed, immediately display tooltip and exit function. When the user corrected the password, the next validation is of terms and cond. checkbox
        return false;
    }
    
}*/



function addBox() {                                                     //When the user checks the 18+ checkbox, this is called
    if (!document.getElementById('Check18').checked) {                  //Check if checkbx is checked, if not - then set label and the Age field to visible and the unobtrusive val message (if exists) to visible
        document.getElementById('Age').style.display = 'block';
        document.getElementById('AgeLabel').style.display = 'block';
        $("span[data-valmsg-for=Age]").css("display", "block");
        $("#Age").prop("disabled", false);                              //Also, the Age textbx must be enabled if user is under 18. I disable it otherwise so that it is not submitted and a different controller action is called (along with a different data model)
    } else {                                                            //If the 18+ check box is checked, then we don't need user's age. Therefore hide the tbx and the label and the validation message if exists
        document.getElementById('Age').style.display = 'none';
        document.getElementById('AgeLabel').style.display = 'none';
        $("span[data-valmsg-for=Age]").css("display", "none");
        $("#Age").prop("disabled", true);                               //Set the disabled prop to true so that this field does not get submitted
    }
}
$(document).ready(function () {
    addBox();
    $("#Check18").change(function () {
        //if ($('#Check18').is(':checked')) {
        addBox();
        //}
    });

    $("#passVisibilityBtn").click(function () {                         //Executes every time the show/hide password button is clicked
        var passVisibilityBtn = $("#passVisibilityBtn");
        var passField = $("#Password");                                 //Get the password field
        var passField2 = $("#Password2");                               //Get the field where the user retypes the passwd
        if (passField.attr("type") == "password" && passField2.attr("type") == "password") {    //If both fields are of type "password", then the user can't see the text, so we should reveal it
            passField.attr("type", "text");                             //Set the type of both fields to "text", so that the text becomes visible
            passField2.attr("type", "text");
            passVisibilityBtn.html("hide");                             //Set the text of the button to "hide", i.e. when the user clicks it next time, it will hide the passwd again
            passVisibilityBtn.css("background-color", "#595959");       //Change the color of the btn so that it's more visible on the background
        } else {                                                    //If fields are not of type "password", i.e. they are of type "text", then we must hide the password
            passField.attr("type", "password");                         //Set the type to "password"
            passField2.attr("type", "password");
            passVisibilityBtn.html("show");                             //Set the btn text to "show", so that the next time the user clicks it, it will reveal the pass, but now it will hide it
            passVisibilityBtn.css("background-color", "#AA3939");
        }
        //return false;                                                   //Because I used the button tag inside of the form, it would submit the form if I don't return false. This function is called every time the button is pressed and false is returned, so the form never gets submitted through this button
    });
});


/*(function () {

    //setup an object full of arrays
    //alternativly it could be something like
    //{"yes":[{value:sweet, text:Sweet}.....]}
    //so you could set the label of the option tag something different than the name
    var bOptions = {
        "---": ["---"],
        "PS4": ["Gods of War", "Crash Bandicoot"],
        "XBox One": ["Halo", "Gears of War"]
    };

    var ConsoleDbx = document.getElementById('ConsoleDbx');
    var GamesDbx = document.getElementById('GamesDbx');

    //on change is a good event for this because you are guarenteed the value is different
    ConsoleDbx.onchange = function () {
        //clear out B
        GamesDbx.length = 0;
        //get the selected value from A
        var _val = this.options[this.selectedIndex].value;
        //loop through bOption at the selected value
        for (var i in bOptions[_val]) {
            //create option tag
            var op = document.createElement('option');
            //set its value
            op.value = bOptions[_val][i];
            //set the display label
            op.text = bOptions[_val][i];
            //append it to B
            GamesDbx.appendChild(op);
        }
    };
    //fire this to update B on load
    ConsoleDbx.onchange();

})();*/

/*
var tmp = $.fn.tooltip.Constructor.prototype.show;
$.fn.tooltip.Constructor.prototype.show = function () {
    tmp.call(this);
    if (this.options.callback) {
        this.options.callback();
    }
}

$('#Password').attr("title", "<ul id='passConditions' name='passConditions'><li id='eight' name='eight'>Eight chars</li><li id='lower' name='lower'>1 Lowercase</li><li id='upper' name='upper'>1 Uppercase</li><li id='numeric' name='numeric'>1 Numeric</li><li id='special' name='special'>1 Special</li></ul>");
this.$('#Password').tooltip({
    placement: 'right',
    callback: function () { //Insert here code to be executed every time tooltip is created
        validatePasswdConditions();
    }
});

var counter = 0;                                                    //Used to count the num of met conditions (only 3 of 4 are required)

function validatePasswdConditions() {                               //Checks if all passwd reqs have been met
    var enoughLength = false;                                       //The length of 8 chars is a requirement (aside from 3 of 4 conditions, so don't use a counter for it, instead keep it as a separate var)
    counter = 0;                                                    //When the function begins, reset the counter
    var eightChars = new RegExp("^(.{8,})");                        //Are there 8 chars?
    var oneLower = new RegExp("[a-z]{1,}");                         //Is there at least 1 lowercase letter?
    var oneUpper = new RegExp("[A-Z]{1,}");                         //Is there at least 1 uppercase letter?
    var oneDigit = new RegExp("[0-9]{1,}");                         //Is there at least 1 digit?
    var oneSpecial = new RegExp("[!@#\$%\^\&*\)\(+=._-]{1,}");      //Is there at least 1 symbol out of listed in square braces?

    var passwdField = $("#Password").val();                         //For efficiency purposes, get current value of passwd to a var

    var ENOUGH_CHECKS = 3;                                          //Only 3 out of 4 conditions have to be met

    if (eightChars.test(passwdField)) {                             //If regex matches the string, the passwd length requirement is met
        $("#eight").removeClass("badPassCondition");                //First make sure that the bad class is removed
        $("#eight").addClass("goodPassCondition");                  //Then set the good class
        enoughLength = true;                                        //Since passwd length is mandatory in any case, set its bool to true to indicate that it's been satisfied
    } else {                                                        //If regex doesnt match, ...
        $("#eight").removeClass("goodPassCondition");               //Make sure good class is removed
        $("#eight").addClass("badPassCondition");                   //Set the bad class
        enoughLength = false;                                       //Indicate that mandatory length condition was not met
    }

    if (oneLower.test(passwdField)) {                               //Check if there's a lowercase char
        $("#lower").removeClass("badPassCondition");
        $("#lower").addClass("goodPassCondition");
        counter++;                                                  //Since we need only 3 of 4 conditions, increment the counter
    } else {
        $("#lower").removeClass("goodPassCondition");
        $("#lower").addClass("badPassCondition");
    }

    if (oneUpper.test(passwdField)) {                               //Check if there's an uppercase char
        $("#upper").removeClass("badPassCondition");
        $("#upper").addClass("goodPassCondition");
        counter++;                                                  //Increment counter
    } else {
        $("#upper").removeClass("goodPassCondition");
        $("#upper").addClass("badPassCondition");
    }

    if (oneDigit.test(passwdField)) {                               //Check if there's a digit
        $("#numeric").removeClass("badPassCondition");
        $("#numeric").addClass("goodPassCondition");
        counter++;                                                  //Increment counter
    } else {
        $("#numeric").removeClass("goodPassCondition");
        $("#numeric").addClass("badPassCondition");
    }

    if (oneSpecial.test(passwdField)) {                             //Check if there's a special char
        $("#special").removeClass("badPassCondition");
        $("#special").addClass("goodPassCondition");
        counter++;                                                  //Increment counter
    } else {
        $("#special").removeClass("goodPassCondition");
        $("#special").addClass("badPassCondition");
    }
    return (counter >= ENOUGH_CHECKS && enoughLength);              //If there are at least 3 met conditions AND the mandatory length condition, return true
}	



*/


