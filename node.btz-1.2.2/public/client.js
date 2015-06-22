$(document).ready(function(){
	// WebSocket
	var socket = io.connect();
	// neue Nachricht
	socket.on('Lenkung', function (data) {
		$('#LenkungMax').val(data.LenkMax);
		$('#LenkungMin').val(data.LenkMin);
		$('#LenkungCenter').val(data.LenkCenter);
		$('#LenkungMaxLabel').text(data.LenkMax);
		$('#LenkungMinLabel').text(data.LenkMin);
		$('#LenkungCenterLabel').text(data.LenkCenter);
		
	});
			
			
	socket.on('LenkungSpeed', function (data) {
		$('#LenkmotorSpeedMax').val(data.LenkSpeedMax);
		$('#LenkmotorSpeedMin').val(data.LenkSpeedMin);
		$('#LenkmotorSpeedMaxLabel').text(data.LenkSpeedMax);
		$('#LenkmotorSpeedMinLabel').text(data.LenkSpeedMin);
	});
			
	
	socket.on('tol', function (data) {
		$('#toleranz').val(data.tole);
		$('#toleranzLabel').text(data.tole);
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
		var LenkMax = $('#LenkungMax').val();
		var LenkMin = $('#LenkungMin').val();
		var LenkCenter = $('#LenkungCenter').val();
		// Socket senden 
		socket.emit('Lenkung', { LenkMax: LenkMax, LenkMin: LenkMin, LenkCenter:LenkCenter });
				
		
	}
	
	//Lenkung Speed Werte 
	
	function LenkungSpeedSenden(){
		var LenkSpeedMax = $('#LenkmotorSpeedMax').val();
		var LenkSpeedMin = $('#LenkmotorSpeedMin').val();
		socket.emit('LenkungSpeed', { LenkSpeedMax: LenkSpeedMax, LenkSpeedMin: LenkSpeedMin });
	}
	
	function tol(){
		var tole = $('#toleranz').val();
		socket.emit('tol', { tole: tole });
	}
	
	
	function refresh(){
		var ref = 1;
		socket.emit('refresh', { ref: ref })
	}
	

	// bei einem Klick
	$('#refresh').click(refresh)
	
		//Maus -- LenkungMax -----------------------------------------------------------------------------------------
	$("#LenkungMax").mousedown(function(event){
	    	interval = setInterval(function(){
	        	$("#LenkungMaxLabel").html($("#LenkungMax").val());
				var range_in = $("#LenkungMax").val();
				LenkungSenden()
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
				LenkungSenden()
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
				LenkungSenden()
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
				LenkungSenden()
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
				LenkungSenden()
				RangeControll()
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
				LenkungSenden()
				RangeControll()
		},11);
	};
	document.getElementById('LenkungCenter').ontouchend = function (eve) {
	clearInterval(interval);
	};//touch -- LenkungCenter ende ----------------------------------------------------------------------------------------

	//Maus -- LenkmotorSpeedMax -----------------------------------------------------------------------------------------
	$("#LenkmotorSpeedMax").mousedown(function(event){
	    	interval = setInterval(function(){
	        	$("#LenkmotorSpeedMaxLabel").html($("#LenkmotorSpeedMax").val());
				LenkungSpeedSenden()
	    	},11);
		});
	$("#LenkmotorSpeedMax").mouseup(function(event){
	clearInterval(interval);
	});//Maus -- LenkmotorSpeedMax ende -----------------------------------------------------------------------------------------
	
	//touch -- LenkmotorSpeedMax -----------------------------------------------------------------------------------------
	document.getElementById('LenkmotorSpeedMax').ontouchstart = function (eve) {
			interval = setInterval(function(){
				$("#LenkmotorSpeedMaxLabel").html($("#LenkmotorSpeedMax").val());
				LenkungSpeedSenden()
		},11);
	};
	document.getElementById('LenkmotorSpeedMax').ontouchend = function (eve) {
	clearInterval(interval);
	};//touch -- LenkmotorSpeedMax ende ----------------------------------------------------------------------------------------
	
	//Maus -- LenkmotorSpeedMin -----------------------------------------------------------------------------------------
	$("#LenkmotorSpeedMin").mousedown(function(event){
	    	interval = setInterval(function(){
	        	$("#LenkmotorSpeedMinLabel").html($("#LenkmotorSpeedMin").val());
				LenkungSpeedSenden()
	    	},11);
		});
	$("#LenkmotorSpeedMin").mouseup(function(event){
	clearInterval(interval);
	});//Maus -- LenkmotorSpeedMin ende -----------------------------------------------------------------------------------------
	
	//touch -- LenkmotorSpeedMin -----------------------------------------------------------------------------------------
	document.getElementById('LenkmotorSpeedMin').ontouchstart = function (eve) {
			interval = setInterval(function(){
				$("#LenkmotorSpeedMinLabel").html($("#LenkmotorSpeedMin").val());
				LenkungSpeedSenden()
		},11);
	};
	document.getElementById('LenkmotorSpeedMin').ontouchend = function (eve) {
	clearInterval(interval);
	};//touch -- LenkmotorSpeedMin ende ----------------------------------------------------------------------------------------
	
	//Maus -- toleranz -----------------------------------------------------------------------------------------
	$("#toleranz").mousedown(function(event){
	    	interval = setInterval(function(){
	        	$("#toleranzLabel").html($("#toleranz").val());
				tol()
	    	},11);
		});
	$("#toleranz").mouseup(function(event){
	clearInterval(interval);
	});//Maus -- toleranz ende -----------------------------------------------------------------------------------------
	
	//touch -- toleranz -----------------------------------------------------------------------------------------
	document.getElementById('toleranz').ontouchstart = function (eve) {
			interval = setInterval(function(){
				$("#toleranzLabel").html($("#toleranz").val());
				tol()
		},11);
	};
	document.getElementById('toleranz').ontouchend = function (eve) {
	clearInterval(interval);
	};//touch -- toleranz ende ----------------------------------------------------------------------------------------

	
    
   


	
	
	
	
	});
