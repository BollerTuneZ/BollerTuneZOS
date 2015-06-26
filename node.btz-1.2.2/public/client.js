$(document).ready(function(){
	
	var socket = io.connect();
	
	
	function StartService(){
		var hallo;
		socket.emit('StartService');
		console.log("StartService");
	}
	
	function StopService(){
		socket.emit('StopService');
		console.log("StopService");
	}
	
	function SaveSettings(){
		socket.emit('SaveSettings');
		console.log("SaveSettings");
	}
	
	function RestoreSettings(){
		socket.emit('RestoreSettings');
		console.log("RestoreSettings");
	}
	
	// bei einem Klick
	
	$('#StartService').click(StartService);
	$('#StopService').click(StopService);
	$('#SaveSettings').click(SaveSettings);
	$('#RestoreSettings').click(RestoreSettings);
	
	
	
	
	});
