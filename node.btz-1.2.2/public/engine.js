$(document).ready(function(){

	// WebSocket
	var socket = io.connect();
	// neue Nachricht
	socket.on('EngineConfig', function (data) {
		$('#EngineSpeedMax').val(data.EngineSpeedMax);
		$('#EngineSpeedMaxLabel').text(data.EngineSpeedMax);
		
		$('#EngineRampTime').val(data.EngineRampTime);
		$('#EngineRampTimeLabel').text(data.EngineRampTime);
		
		$('#EngineSpeedStartMin').val(data.EngineSpeedStartMin);
		$('#EngineSpeedStartMinLabel').text(data.EngineSpeedStartMin);
	
	});
	
	socket.on('EngineConfigDOM', function (data) {

		document.getElementById("EngineSpeedMax").max = data.EngineSpeedMax_MaxDOM;
		document.getElementById("EngineSpeedMax").min = data.EngineSpeedMax_MinDOM;
		console.log(data.EngineSpeedMax_MinDOM);
		document.getElementById("EngineRampTime").max = data.EngineRampTime_MaxDOM;
		document.getElementById("EngineRampTime").min = data.EngineRampTime_MinDOM;
		
		document.getElementById("EngineSpeedStartMin").max = data.EngineSpeedStartMin_MaxDOM;
		document.getElementById("EngineSpeedStartMin").min = data.EngineSpeedStartMin_MinDOM;
	
	});

	// senden
	function EngineConfig(){
		// range auslesen 
	
		var EngineSpeedMax = $('#EngineSpeedMax').val();
		var EngineRampTime = $('#EngineRampTime').val();
		var EngineSpeedStartMin = $('#EngineSpeedStartMin').val();
	
		// Socket senden 
		socket.emit('EngineConfig', { EngineSpeedStartMin: EngineSpeedStartMin, EngineSpeedMax: EngineSpeedMax, EngineRampTime: EngineRampTime });
			console.log("EngineConfig");
	}



//Maus -- EngineSpeedMax -----------------------------------------------------------------------------------------
	$("#EngineSpeedMax").mousedown(function(event){
	    	interval = setInterval(function(){
	        	$("#EngineSpeedMaxLabel").html($("#EngineSpeedMax").val());
				EngineConfig()
	    	},11);
		});
	$("#EngineSpeedMax").mouseup(function(event){
	clearInterval(interval);
	});//Maus -- LenkmotorSpeedMax ende -----------------------------------------------------------------------------------------
	
	//touch -- EngineSpeedMax -----------------------------------------------------------------------------------------
	document.getElementById('EngineSpeedMax').ontouchstart = function (eve) {
			interval = setInterval(function(){
				$("#EngineSpeedMaxLabel").html($("#EngineSpeedMax").val());
				EngineConfig()
		},11);
	};
	document.getElementById('EngineSpeedMax').ontouchend = function (eve) {
	clearInterval(interval);
	};//touch -- EngineSpeedMax ende ----------------------------------------------------------------------------------------
	
	//Maus -- EngineRampTime -----------------------------------------------------------------------------------------
	$("#EngineRampTime").mousedown(function(event){
	    	interval = setInterval(function(){
	        	$("#EngineRampTimeLabel").html($("#EngineRampTime").val());
				EngineConfig()
	    	},11);
		});
	$("#EngineRampTime").mouseup(function(event){
	clearInterval(interval);
	});//Maus -- EngineRampTime ende -----------------------------------------------------------------------------------------
	
	//touch -- EngineRampTime -----------------------------------------------------------------------------------------
	document.getElementById('EngineRampTime').ontouchstart = function (eve) {
			interval = setInterval(function(){
				$("#EngineRampTimeLabel").html($("#EngineRampTime").val());
				EngineConfig()
		},11);
	};
	document.getElementById('EngineRampTime').ontouchend = function (eve) {
	clearInterval(interval);
	};//touch -- EngineRampTime ende ----------------------------------------------------------------------------------------
	
	
	//Maus -- EngineSpeedStartMin -----------------------------------------------------------------------------------------
	$("#EngineSpeedStartMin").mousedown(function(event){
	    	interval = setInterval(function(){
	        	$("#EngineSpeedStartMinLabel").html($("#EngineSpeedStartMin").val());
				EngineConfig()
	    	},11);
		});
	$("#EngineSpeedStartMin").mouseup(function(event){
	clearInterval(interval);
	});//Maus -- EngineSpeedStartMin ende -----------------------------------------------------------------------------------------
	
	//touch -- EngineSpeedStartMin -----------------------------------------------------------------------------------------
	document.getElementById('EngineSpeedStartMin').ontouchstart = function (eve) {
			interval = setInterval(function(){
				$("#EngineSpeedStartMinLabel").html($("#EngineSpeedStartMin").val());
				EngineConfig()
		},11);
	};
	document.getElementById('EngineSpeedStartMin').ontouchend = function (eve) {
	clearInterval(interval);
	};//touch -- toleranz ende ----------------------------------------------------------------------------------------
	
	});