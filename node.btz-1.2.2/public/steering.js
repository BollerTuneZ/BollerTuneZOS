$(document).ready(function(){
	
	// WebSocket
	var socket = io.connect();
	// neue Nachricht
	socket.on('SteeringConfig', function (data) {
		$('#LenkungMax').val(data.SteeringRangeMax);
		$('#LenkungMaxLabel').text(data.SteeringRangeMax);
		
		$('#LenkungMin').val(data.SteeringRangeMin);
		$('#LenkungMinLabel').text(data.SteeringRangeMin);
		
		$('#LenkungCenter').val(data.SteeringCenter);
		$('#LenkungCenterLabel').text(data.SteeringCenter);
		
		$('#toleranz').val(data.SteeringToleranz);
		$('#toleranzLabel').text(data.SteeringToleranz);
		
	});
	
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
	
		//Maus -- LenkungMax -----------------------------------------------------------------------------------------
	$("#LenkungMax").mousedown(function(event){
	    	interval = setInterval(function(){
	        	$("#LenkungMaxLabel").html($("#LenkungMax").val());
				var range_in = $("#LenkungMax").val();
				SteeringConfig()
	    	},11);
		});
	$("#LenkungMax").mouseup(function(event){
	clearInterval(interval);
	});//Maus -- LenkungMax ende -----------------------------------------------------------------------------------------
	
	//touch -- LenkungMax -----------------------------------------------------------------------------------------
	document.getElementById('LenkungMax').ontouchstart = function (eve) {
			interval = setInterval(function(){
				$("#LenkungMaxLabel").html($("#LenkungMax").val());
				var range_in = $("#LenkungMax").val();
				SteeringConfig()
		},11);
	};
	document.getElementById('LenkungMax').ontouchend = function (eve) {
	clearInterval(interval);
	};//touch -- LenkungMax ende ----------------------------------------------------------------------------------------
	
	

	//Maus -- LenkungMin -----------------------------------------------------------------------------------------
	$("#LenkungMin").mousedown(function(event){
	    	interval = setInterval(function(){
	        	$("#LenkungMinLabel").html($("#LenkungMin").val());
				var range_in = $("#LenkungMin").val();
				SteeringConfig()
	    	},11);
		});
	$("#LenkungMin").mouseup(function(event){
	clearInterval(interval);
	});//Maus -- LenkungMin ende -----------------------------------------------------------------------------------------
	
	//touch -- LenkungMin -----------------------------------------------------------------------------------------
	document.getElementById('LenkungMin').ontouchstart = function (eve) {
			interval = setInterval(function(){
				$("#LenkungMinLabel").html($("#LenkungMin").val());
				var range_in = $("#LenkungMin").val();
				SteeringConfig()
		},11);
	};
	document.getElementById('LenkungMin').ontouchend = function (eve) {
	clearInterval(interval);
	};//touch -- LenkungMin ende ----------------------------------------------------------------------------------------

	//Maus -- LenkungCenter -----------------------------------------------------------------------------------------
	$("#LenkungCenter").mousedown(function(event){
	    	interval = setInterval(function(){
	        	$("#LenkungCenterLabel").html($("#LenkungCenter").val());
				var range_in = $("#LenkungCenter").val();
				SteeringConfig()
				 
	    	},11);
		});
	$("#LenkungCenter").mouseup(function(event){
	clearInterval(interval);
	});//Maus -- LenkungCenter ende -----------------------------------------------------------------------------------------
	
	//touch -- LenkungCenter -----------------------------------------------------------------------------------------
	document.getElementById('LenkungCenter').ontouchstart = function (eve) {
			interval = setInterval(function(){
				$("#LenkungCenterLabel").html($("#LenkungCenter").val());
				var range_in = $("#LenkungCenter").val();
				SteeringConfig()
			
		},11);
	};
	document.getElementById('LenkungCenter').ontouchend = function (eve) {
	clearInterval(interval);
	};//touch -- LenkungCenter ende ----------------------------------------------------------------------------------------

	//Maus -- LenkmotorSpeedMax -----------------------------------------------------------------------------------------
	$("#LenkmotorSpeedMax").mousedown(function(event){
	    	interval = setInterval(function(){
	        	$("#LenkmotorSpeedMaxLabel").html($("#LenkmotorSpeedMax").val());
				SteeringMotorConfig()
	    	},11);
		});
	$("#LenkmotorSpeedMax").mouseup(function(event){
	clearInterval(interval);
	});//Maus -- LenkmotorSpeedMax ende -----------------------------------------------------------------------------------------
	
	//touch -- LenkmotorSpeedMax -----------------------------------------------------------------------------------------
	document.getElementById('LenkmotorSpeedMax').ontouchstart = function (eve) {
			interval = setInterval(function(){
				$("#LenkmotorSpeedMaxLabel").html($("#LenkmotorSpeedMax").val());
				SteeringMotorConfig()
		},11);
	};
	document.getElementById('LenkmotorSpeedMax').ontouchend = function (eve) {
	clearInterval(interval);
	};//touch -- LenkmotorSpeedMax ende ----------------------------------------------------------------------------------------
	
	//Maus -- LenkmotorSpeedMin -----------------------------------------------------------------------------------------
	$("#LenkmotorSpeedMin").mousedown(function(event){
	    	interval = setInterval(function(){
	        	$("#LenkmotorSpeedMinLabel").html($("#LenkmotorSpeedMin").val());
				SteeringMotorConfig()
	    	},11);
		});
	$("#LenkmotorSpeedMin").mouseup(function(event){
	clearInterval(interval);
	});//Maus -- LenkmotorSpeedMin ende -----------------------------------------------------------------------------------------
	
	//touch -- LenkmotorSpeedMin -----------------------------------------------------------------------------------------
	document.getElementById('LenkmotorSpeedMin').ontouchstart = function (eve) {
			interval = setInterval(function(){
				$("#LenkmotorSpeedMinLabel").html($("#LenkmotorSpeedMin").val());
				SteeringMotorConfig()
		},11);
	};
	document.getElementById('LenkmotorSpeedMin').ontouchend = function (eve) {
	clearInterval(interval);
	};//touch -- LenkmotorSpeedMin ende ----------------------------------------------------------------------------------------
	
	//Maus -- toleranz -----------------------------------------------------------------------------------------
	$("#toleranz").mousedown(function(event){
	    	interval = setInterval(function(){
	        	$("#toleranzLabel").html($("#toleranz").val());
				SteeringConfig()
	    	},11);
		});
	$("#toleranz").mouseup(function(event){
	clearInterval(interval);
	});//Maus -- toleranz ende -----------------------------------------------------------------------------------------
	
	//touch -- toleranz -----------------------------------------------------------------------------------------
	document.getElementById('toleranz').ontouchstart = function (eve) {
			interval = setInterval(function(){
				$("#toleranzLabel").html($("#toleranz").val());
				SteeringConfig()
		},11);
	};
	document.getElementById('toleranz').ontouchend = function (eve) {
	clearInterval(interval);
	};//touch -- toleranz ende ----------------------------------------------------------------------------------------


	
	
	
	
	
	});
