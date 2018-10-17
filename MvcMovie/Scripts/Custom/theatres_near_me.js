var map;
var infoWindow;
var cityCircle;
var defaultLatitude = 44.9778;
var defaultLongitude = -93.2650;
var defaultZoom = 10;
var defaultRadMeters = 10000;
const MILES_IN_METER = 0.000621371;
const METERS_IN_MILE = 1609.34;
var radiusTbxValue;
var currentLocDisplayEl = document.getElementById("demo"); 
const GEOCODING_BASE_URL = "https://maps.googleapis.com/maps/api/geocode/json";
//var backendApiEndpoint = "http://localhost:51676/Theater/Find?lat=44.984957&lon=-93.255552&radius=50";
//var backendApiEndpoint = "http://localhost:51676/Theater/Find";
var backendApiEndpoint = "/Theater/Find";
const LAT_LNG_TO_ZIP_ADDRESS_PARAM = "latlng";
const ADDRESS_PARAM = "address";

$(document).ready(function() {			//Initialize the map only when the doc is loaded
	initMap();
	
	$("#radiusBtn").click(function(){
		radiusTbxValue = $("#radiusTbx").val();
		console.log(radiusTbxValue);
		setCircleRadiusFromTbx(parseFloat(radiusTbxValue) * METERS_IN_MILE);
		console.log("new radius (mi): " + radiusTbxValue * METERS_IN_MILE);
	});
	
	$("#zipBtn").click(function(){
		setCirleByZipAddressAjax("#zipTbx");
	});
	
	$("#addressBtn").click(function(){
		setCirleByZipAddressAjax("#addressTbx");
	});
	
	$("#getTheatersBtn").click(function(){
		getTheaterCoordsAjax(cityCircle.getCenter().lat(), cityCircle.getCenter().lng(), $("#radiusTbx").val());
	});
});

function setCirleByZipAddressAjax(tbxId){							//Sets values of zip and address textboxes to circle's center's current position
	var query = $(tbxId).val();
	var endpoint = GEOCODING_BASE_URL + "?" + ADDRESS_PARAM + "="+ encodeURIComponent(query) +"&key=AIzaSyCxwC-hLhTBNUKoeQeHDuiFSvqIueBshAs";		//REMOVE THE API KEY! MAKE SERVER REQUEST GMAPS
	$.ajax({url: endpoint, success: function(result){			//Grab the AJAX response
		if(result.status == "OK"){
			console.log(result.results[0].geometry.location.lat);
			var newLat = result.results[0].geometry.location.lat;
			var newLng = result.results[0].geometry.location.lng;
			cityCircle.setCenter(new google.maps.LatLng(newLat, newLng));	//Move the circle's center to new position
			map.panTo(new google.maps.LatLng(newLat, newLng));
			
			
	
		}else{
			console.log("error!");					//If response status is not "OK", give an error
			alterTbxVal("zipTbx", "Error!");
			alterTbxVal("addressTbx", "Error!");
		}
	}});
}

function drawCircle(strokeColor, fillColor, defaultRadMeters, defaultLat, defaultLng){	//Draws a circle
	cityCircle = new google.maps.Circle({
		strokeColor: strokeColor,
		strokeOpacity: 0.5,
		strokeWeight: 2,
		fillColor: fillColor,
		fillOpacity: 0.4,
		map: map,
		center: {lat: defaultLat, lng: defaultLng},
		radius: defaultRadMeters,
		clickable: false,
		editable: true,
	});
}



function alterTbxVal(tbxId, newVal){							//Pass the textbox's id and new val to change the current val
	var el = document.getElementById(tbxId);
	el.value = newVal;
/* 	if(tbxId == "radiusTbx"){
		el.value = newVal.toFixed(0) + " (meters)";
	} */
}

function showPosition(position) {								//Is executed if geolocation is available
	defaultLatitude = position.coords.latitude;					//Set default lat & lng to current values
	defaultLongitude = position.coords.longitude;	
    /* currentLocDisplayEl.innerHTML = "Latitude: " + defaultLatitude + "<br>Longitude: " + defaultLongitude; */
	
	map.panTo({lat: defaultLatitude, lng: defaultLongitude});	//Move the map's center to the new position
	cityCircle.setCenter(new google.maps.LatLng(position.coords.latitude, position.coords.longitude));	//Move the circle's center to new position
	console.log(position.coords.latitude, position.coords.longitude);
}
  
function initMap() {															//Main logic of drawing the map
	if (navigator.geolocation) {												//Check if geolocation is enabled
		navigator.geolocation.getCurrentPosition(showPosition, showError);		//If yes, pass callbacks
	} else {
		currentLocDisplayEl.innerHTML = "Geolocation is not supported by this browser.";
	}
		
	map = new google.maps.Map(document.getElementById('map'), {					//Draw the map
		center: {lat: defaultLatitude, lng: defaultLongitude},
		zoom: defaultZoom
	});
	drawCircle('#414343', '#414343', defaultRadMeters, defaultLatitude, defaultLongitude);
	alterTbxVal("radiusTbx", metersToMiles(cityCircle.getRadius()));							//Put the initial radius value to the tbx; radius_changed event not fired when the circle is just created
	setZipAddrTbxAjax();														//Put the initial value in hte tbx; center_changed not fired when the circle is just created
	
	
	google.maps.event.addListener(cityCircle, 'radius_changed', function() {	//radius_changed is fired when user edits the circle manually
		console.log(metersToMiles(cityCircle.getRadius()));
		alterTbxVal("radiusTbx", metersToMiles(cityCircle.getRadius()));
	});
		
	google.maps.event.addListener(cityCircle, 'center_changed', function() {	//center_changed is fired when user drags the circle
		console.log("Center: " + cityCircle.getCenter());
		var timer;						//Make sure the API isn't called too often
		clearTimeout(timer);
		var ms = 400; 					//Wait 400 ms before each request
		var val = this.value;
		timer = setTimeout(function() {
			setZipAddrTbxAjax();
		}, ms);
		
		
	});
	
	getTheaterCoordsAjax(defaultLatitude, defaultLongitude, defaultRadMeters * MILES_IN_METER);
}    

function showError(error) {								//If geolocation not available, display the error message. The map is still usable
    switch(error.code) {
        case error.PERMISSION_DENIED:
            currentLocDisplayEl.innerHTML = "User denied the request for Geolocation."
            break;
        case error.POSITION_UNAVAILABLE:
            currentLocDisplayEl.innerHTML = "Location information is unavailable."
            break;
        case error.TIMEOUT:
            currentLocDisplayEl.innerHTML = "The request to get user location timed out."
            break;
        case error.UNKNOWN_ERROR:
            currentLocDisplayEl.innerHTML = "An unknown error occurred."
            break;
    }
}

function setZipAddrTbxAjax(){							//Sets values of zip and address textboxes to circle's center's current position. Makes request to Google Maps API and sets the ZIP and address textbox to new vals
	var lat = cityCircle.getCenter().lat();
	var lng = cityCircle.getCenter().lng();
	var endpoint = GEOCODING_BASE_URL + "?" + LAT_LNG_TO_ZIP_ADDRESS_PARAM + "=" + lat + "," + lng + "&key=AIzaSyCxwC-hLhTBNUKoeQeHDuiFSvqIueBshAs";		//REMOVE THE API KEY! MAKE SERVER REQUEST GMAPS
	$.ajax({url: endpoint, success: function(result){			//Grab the AJAX response
		if(result.status == "OK"){
			console.log(result.results[0].formatted_address);
			
			var found = false;									//Sentinel value
			for (var i = 0; i < result.results[0].address_components.length; i++) {				//For all address_component[] elements of results[0]. We use results[0] because we're interested in the first search result only
					for(var j = 0; j < result.results[0].address_components[i].types.length; j++){	//For all types[] elements inside each address_component[]
						if(result.results[0].address_components[i].types[j] == "postal_code"){		//Find the postal code value
							alterTbxVal("zipTbx", result.results[0].address_components[i].short_name);	//Set the zip textbox to value from the AJAX response
							found = true;		//To make sure that the next iteration of outer loop won't happen
							break;
						}
					}
					if(found){
						break;
					}
			}
			alterTbxVal("addressTbx", result.results[0].formatted_address);		//Set address textbox's val to address from the AJAX response
		}else{
			console.log("error!");					//If response status is not "OK", give an error
			alterTbxVal("zipTbx", "Error!");
			alterTbxVal("addressTbx", "Error!");
		}
	}});
}

function setCircleRadiusFromTbx(radiusMeters){
	cityCircle.setRadius(radiusMeters);
}
//The following code deals with markers


	
function getTheaterCoordsAjax(lat, lon, radius){
	//backendApiEndpoint = backendApiEndpoint + "?" + "lat=" + lat + "&lon=" + lon + "&radius=" + radius;
	var endpoint = backendApiEndpoint + "?" + "lat=" + lat + "&lon=" + lon + "&radius=" + radius;
	
	$.ajax({url: endpoint, success: function(result){			//Grab the AJAX response
		console.log(result);
		result = JSON.parse(result);
		for (i = 0; i < result.length; i++) {  
			console.log(result[i])
		}
		
		
		var infowindow = new google.maps.InfoWindow();

		var marker, i;

		for (i = 0; i < result.length; i++) {  
		marker = new google.maps.Marker({
			position: new google.maps.LatLng(result[i].Latitude, result[i].Longitude),
			map: map
		});

		google.maps.event.addListener(marker, 'click', (function(marker, i) {
			return function() {
			infowindow.setContent(result[i].name);
			infowindow.open(map, marker);
			}
		})(marker, i));
		}
		
		
		
	}});
}

function metersToMiles(meters){
	return meters * MILES_IN_METER;
}

function milesToMeters(miles){
	return miles * METERS_IN_MILE;
}