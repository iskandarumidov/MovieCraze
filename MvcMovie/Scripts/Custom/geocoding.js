function geocodeAddress(geocoder, resultsMap, address) {
        //var address = "Emagine Willow Creek";
        geocoder.geocode({'address': address}, function(results, status) {
          if (status === 'OK') {
//            resultsMap.setCenter(results[0].geometry.location);
			console.log("address: " + address);
			console.log("lat: " + results[0].geometry.location.lat());
			console.log("long: " + results[0].geometry.location.lng());
            var marker = new google.maps.Marker({
              map: resultsMap,
              position: results[0].geometry.location
            });
          } else {
            alert('Geocode was not successful for the following reason: ' + status);
          }
        });
}