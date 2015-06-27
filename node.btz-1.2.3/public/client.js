$(document).ready(function(){
	
	var socket = io.connect();
	
	socket.on('StartService', function() {
	/* 	console.log("StartService"); */
	});
	socket.on('StopService', function() {
		/* console.log("StopService"); */
	});
	socket.on('SaveSettings', function() {
		/* console.log("SaveSettings"); */
	});
	socket.on('RestoreSettings', function() {
		/* console.log("RestoreSettings"); */
	});

	
	function StartService(){
		var hallo;
		socket.emit('StartService');
		
	}
	
	function StopService(){
		socket.emit('StopService');
		
	}
	
	function SaveSettings(){
		socket.emit('SaveSettings');
		
	}
	
	function RestoreSettings(){
		socket.emit('RestoreSettings');
		
	}
	
	// bei einem Klick
	
	$('#StartService').click(StartService);
	$('#StopService').click(StopService);
	$('#SaveSettings').click(SaveSettings);
	$('#RestoreSettings').click(RestoreSettings);
	
	
	
	
	});
