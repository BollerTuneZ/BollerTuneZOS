
var socket = io.connect();
$(document).ready(function(){

var options = { enableHighAccuracy: true, timeout: 5000, maximumAge: 0 };
	var lat;
	var lng;
	var lastMarker;
	var interval;

	
	function geobegin(){
			interval = setInterval(function(){
				navigator.geolocation.getCurrentPosition(success, error, options); 
				console.log("geostart");
				
				
				
					
			},500);
		};
		function geoend() {
			clearInterval(interval);
		};
		
		function success(pos) {
			var crd = pos.coords;
			var showgeo = document.getElementById('showgeo');
			showgeo.innerHTML = 'Aktuelle Position: <br>Latituide : ' + crd.latitude + '<br>Longitude : ' + crd.longitude + '<br>Mehr oder weniger auf ' + crd.accuracy + ' Meter.';
	    
			lat = crd.latitude;
			lng = crd.longitude;
			
	//		lastMarker.setMap(null);
	//		clearMarkers();
			lastMarker = new google.maps.Marker({
				map: map,
				position: new google.maps.LatLng(lat, lng)
			});
			
			
			
			socket.emit('geodata', { latitude: lat, longitude: lng });
			console.log("Geo Daten Versendet");
			
			
			
			
		};
		
		function error(err) {
			showgeo.innerHTML = 'Fehler aufgetreten (' + 
			                    err.code + '): ' + err.message;
		};
		
		$('#geostart').click(geobegin);
		$('#geoend').click(geoend);
	
	
	socket.on('geodata', function (data) {

		lastMarker = new google.maps.Marker({
				map: map,
				position: new google.maps.LatLng(data.latitude, data.longitude)
			});

		
		
		
		
	});
	
	
	// In the following example, markers appear when the user clicks on the map.
	// The markers are stored in an array.
	// The user can then click an option to hide, show or delete the markers.
	var map;
	var markers = [];
	
	function initialize() {
	  var haightAshbury = new google.maps.LatLng(0, 0);
	  var mapOptions = {
	    zoom: 2,
	    center: haightAshbury,
	    mapTypeId: google.maps.MapTypeId.TERRAIN
	  };
	  map = new google.maps.Map(document.getElementById('map-canvas'),
	      mapOptions);
	
	  // This event listener will call addMarker() when the map is clicked.
	/*
	  google.maps.event.addListener(map, 'click', function(event) {
	    addMarker(event.latLng);
	  });
	*/
	
	  // Adds a marker at the center of the map.
	  addMarker(haightAshbury);
	}
	
	// Add a marker to the map and push to the array.
	function addMarker(location) {
	  var marker = new google.maps.Marker({
	    position: location,
	    map: map
	  });
	  markers.push(marker);
	}
	
	// Sets the map on all markers in the array.
	function setAllMap(map) {
	  for (var i = 0; i < markers.length; i++) {
	    markers[i].setMap(map);
	  }
	}
	
	// Removes the markers from the map, but keeps them in the array.
	function clearMarkers() {
	  setAllMap(null);
	}
	
	// Shows any markers currently in the array.
	function showMarkers() {
	  setAllMap(map);
	}
	
	// Deletes all markers in the array by removing references to them.
	function deleteMarkers() {
	  clearMarkers();
	  markers = [];
	}
	
	google.maps.event.addDomListener(window, 'load', initialize);
	
	



});