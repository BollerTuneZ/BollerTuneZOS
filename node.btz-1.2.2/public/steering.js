$(document).ready(function(){
	
	// WebSocket
	var socket = io.connect();
	// neue Nachricht
		
	socket.on('SteeringMotorConfig', function (data) {
		$('#LenkmotorSpeedMax').val(data.SteeringSpeedMax);
		$('#LenkmotorSpeedMin').val(data.SteeringSpeedMin);
		$('#LenkmotorSpeedMaxLabel').text(data.SteeringSpeedMax);
		$('#LenkmotorSpeedMinLabel').text(data.SteeringSpeedMin);
	});
/*
	window.ondevicemotion = function(event) {  
    var accelerationX = event.accelerationIncludingGravity.x;  
    var accelerationY = event.accelerationIncludingGravity.y;  
    var accelerationZ = event.accelerationIncludingGravity.z;  
    //console.log(accelerationX);
    //console.log(accelerationY);
    //console.log(accelerationZ);
    socket.emit('Lenkung', { LenkMax: accelerationX, LenkMin: accelerationY, LenkCenter:accelerationZ });
	} 
*/
			
	
	//Lenkung senden
	function SteeringConfig(){
		// range auslesen 
	
		var SteeringRangeMax = $('#LenkungMax').val();
		var SteeringRangeMin = $('#LenkungMin').val();
		var SteeringCenter = $('#LenkungCenter').val();
		var SteeringToleranz = $('#toleranz').val();
		// Socket senden 
		socket.emit('SteeringConfig', { SteeringRangeMax: SteeringRangeMax, SteeringRangeMin: SteeringRangeMin, SteeringCenter:SteeringCenter, SteeringToleranz: SteeringToleranz  });
			console.log("Lenkung");
	}
	
	//Lenkung Speed Werte 
	
	function SteeringMotorConfig(){
		var SteeringSpeedMax = $('#LenkmotorSpeedMax').val();
		var SteeringSpeedMin = $('#LenkmotorSpeedMin').val();
		socket.emit('SteeringMotorConfig', { SteeringSpeedMax: SteeringSpeedMax, SteeringSpeedMin: SteeringSpeedMin });
	}
	
	function EngineConfig(){
		// range auslesen
		var EngineSpeedMax = $('#EngineSpeedMax').val();
		var EngineSpeedStartMin = $('#EngineSpeedStartMin').val();
		var EngineRampTime = $('#EngineRampTime').val();
		
		// Socket senden 
		socket.emit('SteeringConfig', { EngineSpeedStartMin: EngineSpeedStartMin, EngineSpeedMax: EngineSpeedMax, EngineRampTime:EngineRampTime  });
	}
	
	
	
	
	
	
	
	});
