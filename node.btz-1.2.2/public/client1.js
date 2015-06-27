$(document).ready(function(){
	
	// WebSocket
	var socket = io.connect();
	// neue Nachricht
	socket.on('Lenkung', function (data) {
		$('#SteeringRangeMax').val(data.LenkMax);
		$('#SteeringRangeMin').val(data.LenkMin);
		$('#SteeringCenter').val(data.LenkCenter);
		$('#SteeringRangeMaxLabel').text(data.LenkMax);
		$('#SteeringRangeMinLabel').text(data.LenkMin);
		$('#SteeringCenterLabel').text(data.LenkCenter);
		
	});
			
			
	socket.on('LenkungSpeed', function (data) {
		$('#SteeringSpeedMax').val(data.LenkSpeedMax);
		$('#SteeringSpeedMin').val(data.LenkSpeedMin);
		$('#SteeringSpeedMaxLabel').text(data.LenkSpeedMax);
		$('#SteeringSpeedMinLabel').text(data.LenkSpeedMin);
	});
			
	
	socket.on('tol', function (data) {
		$('#SteeringToleranz').val(data.tole);
		$('#SteeringToleranzLabel').text(data.tole);
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
	function LenkungSenden(){
		// range auslesen 
		console.log("Lenkung");
		var LenkMax = $('#SteeringRangeMax').val();
		var LenkMin = $('#SteeringRangeMin').val();
		var LenkCenter = $('#SteeringCenter').val();
		// Socket senden 
		socket.emit('Lenkung', { LenkMax: LenkMax, LenkMin: LenkMin, LenkCenter:LenkCenter });
		var LenkSpeedMax = $('#SteeringSpeedMax').val();
		var LenkSpeedMin = $('#SteeringSpeedMin').val();
		socket.emit('LenkungSpeed', { LenkSpeedMax: LenkSpeedMax, LenkSpeedMin: LenkSpeedMin });
				
		
	}
	
	//Lenkung Speed Werte 
	
	function LenkungSpeedSenden(){
		
	}
	
	function tol(){
		var tole = $('#SteeringToleranz').val();
		socket.emit('tol', { tole: tole });
	}
	
	
	function refresh(){
		var ref = 1;
		socket.emit('refresh', { ref: ref })
	}
	

	// bei einem Klick
	$('#refresh').click(refresh)
	
		//Maus -- SteeringRangeMax -----------------------------------------------------------------------------------------
	$("#SteeringRangeMax").mousedown(function(event){
	    	interval = setInterval(function(){
	        	$("#SteeringRangeMaxLabel").html($("#SteeringRangeMax").val());
				var range_in = $("#SteeringRangeMax").val();
				LenkungSenden()
	    	},11);
		});
	$("#SteeringRangeMax").mouseup(function(event){
	clearInterval(interval);
	});//Maus -- SteeringRangeMax ende -----------------------------------------------------------------------------------------
	
	//touch -- SteeringRangeMax -----------------------------------------------------------------------------------------
	document.getElementById('SteeringRangeMax').ontouchstart = function (eve) {
			interval = setInterval(function(){
				$("#SteeringRangeMaxLabel").html($("#SteeringRangeMax").val());
				var range_in = $("#SteeringRangeMax").val();
				LenkungSenden()
		},11);
	};
	document.getElementById('SteeringRangeMax').ontouchend = function (eve) {
	clearInterval(interval);
	};//touch -- SteeringRangeMax ende ----------------------------------------------------------------------------------------
	
	

	//Maus -- SteeringRangeMin -----------------------------------------------------------------------------------------
	$("#SteeringRangeMin").mousedown(function(event){
	    	interval = setInterval(function(){
	        	$("#SteeringRangeMinLabel").html($("#SteeringRangeMin").val());
				var range_in = $("#SteeringRangeMin").val();
				LenkungSenden()
	    	},11);
		});
	$("#SteeringRangeMin").mouseup(function(event){
	clearInterval(interval);
	});//Maus -- SteeringRangeMin ende -----------------------------------------------------------------------------------------
	
	//touch -- SteeringRangeMin -----------------------------------------------------------------------------------------
	document.getElementById('SteeringRangeMin').ontouchstart = function (eve) {
			interval = setInterval(function(){
				$("#SteeringRangeMinLabel").html($("#SteeringRangeMin").val());
				var range_in = $("#SteeringRangeMin").val();
				LenkungSenden()
		},11);
	};
	document.getElementById('SteeringRangeMin').ontouchend = function (eve) {
	clearInterval(interval);
	};//touch -- SteeringRangeMin ende ----------------------------------------------------------------------------------------

	//Maus -- SteeringCenter -----------------------------------------------------------------------------------------
	$("#SteeringCenter").mousedown(function(event){
	    	interval = setInterval(function(){
	        	$("#SteeringCenterLabel").html($("#SteeringCenter").val());
				var range_in = $("#SteeringCenter").val();
				LenkungSenden()
				RangeControll()
	    	},11);
		});
	$("#SteeringCenter").mouseup(function(event){
	clearInterval(interval);
	});//Maus -- SteeringCenter ende -----------------------------------------------------------------------------------------
	
	//touch -- SteeringCenter -----------------------------------------------------------------------------------------
	document.getElementById('SteeringCenter').ontouchstart = function (eve) {
			interval = setInterval(function(){
				$("#SteeringCenterLabel").html($("#SteeringCenter").val());
				var range_in = $("#SteeringCenter").val();
				LenkungSenden()
				RangeControll()
		},11);
	};
	document.getElementById('SteeringCenter').ontouchend = function (eve) {
	clearInterval(interval);
	};//touch -- SteeringCenter ende ----------------------------------------------------------------------------------------

	//Maus -- SteeringSpeedMax -----------------------------------------------------------------------------------------
	$("#SteeringSpeedMax").mousedown(function(event){
	    	interval = setInterval(function(){
	        	$("#SteeringSpeedMaxLabel").html($("#SteeringSpeedMax").val());
				LenkungSpeedSenden()
	    	},11);
		});
	$("#SteeringSpeedMax").mouseup(function(event){
	clearInterval(interval);
	});//Maus -- SteeringSpeedMax ende -----------------------------------------------------------------------------------------
	
	//touch -- SteeringSpeedMax -----------------------------------------------------------------------------------------
	document.getElementById('SteeringSpeedMax').ontouchstart = function (eve) {
			interval = setInterval(function(){
				$("#SteeringSpeedMaxLabel").html($("#SteeringSpeedMax").val());
				LenkungSpeedSenden()
		},11);
	};
	document.getElementById('SteeringSpeedMax').ontouchend = function (eve) {
	clearInterval(interval);
	};//touch -- SteeringSpeedMax ende ----------------------------------------------------------------------------------------
	
	//Maus -- SteeringSpeedMin -----------------------------------------------------------------------------------------
	$("#SteeringSpeedMin").mousedown(function(event){
	    	interval = setInterval(function(){
	        	$("#SteeringSpeedMinLabel").html($("#SteeringSpeedMin").val());
				LenkungSpeedSenden()
	    	},11);
		});
	$("#SteeringSpeedMin").mouseup(function(event){
	clearInterval(interval);
	});//Maus -- SteeringSpeedMin ende -----------------------------------------------------------------------------------------
	
	//touch -- SteeringSpeedMin -----------------------------------------------------------------------------------------
	document.getElementById('SteeringSpeedMin').ontouchstart = function (eve) {
			interval = setInterval(function(){
				$("#SteeringSpeedMinLabel").html($("#SteeringSpeedMin").val());
				LenkungSpeedSenden()
		},11);
	};
	document.getElementById('SteeringSpeedMin').ontouchend = function (eve) {
	clearInterval(interval);
	};//touch -- SteeringSpeedMin ende ----------------------------------------------------------------------------------------
	
	//Maus -- SteeringToleranz -----------------------------------------------------------------------------------------
	$("#SteeringToleranz").mousedown(function(event){
	    	interval = setInterval(function(){
	        	$("#SteeringToleranzLabel").html($("#SteeringToleranz").val());
				tol()
	    	},11);
		});
	$("#SteeringToleranz").mouseup(function(event){
	clearInterval(interval);
	});//Maus -- SteeringToleranz ende -----------------------------------------------------------------------------------------
	
	//touch -- SteeringToleranz -----------------------------------------------------------------------------------------
	document.getElementById('SteeringToleranz').ontouchstart = function (eve) {
			interval = setInterval(function(){
				$("#SteeringToleranzLabel").html($("#SteeringToleranz").val());
				tol()
		},11);
	};
	document.getElementById('SteeringToleranz').ontouchend = function (eve) {
	clearInterval(interval);
	};//touch -- SteeringToleranz ende ----------------------------------------------------------------------------------------

	
    
   


	
	
	
	
	});
