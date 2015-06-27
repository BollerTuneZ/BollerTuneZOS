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
		
		socket.on('RestoreSettings', function (data) {
			console.log(data.SteeringRangeMax );
			
			document.getElementById("LenkungMax").max = data.SteeringRangeMax;
			document.getElementById("LenkungMax").max = data.SteeringRangeMax;
			document.getElementById("LenkungMax").max = data.SteeringRangeMax;
			document.getElementById("LenkungMax").max = data.SteeringRangeMax;
			document.getElementById("LenkungMax").max = data.SteeringRangeMax;
			document.getElementById("LenkungMax").max = data.SteeringRangeMax;
			
		});
		
		
		function StartService(){
			socket.emit('StartService');
		}
		
		function StopService(){
			socket.emit('StopService');
		}
		
		function SaveSettings(){
			socket.emit('SaveSettings');
		}
		
		function RestoreSettings(){
		
			
			var a = 321;
			var b = 324;
			var c = 121;
			var d = 213;
			var e = 722;
			var f = 543;
			var g = 1212;
			var i = 232;
			var h = 121;
	/*
	console.log(a);
			console.log(b);
*/
			
			socket.emit('RestoreSettings', { SteeringRangeMax: a , SteeringRangeMin: b , SteeringCenter: c , SteeringToleranz: d , SteeringSpeedMin: e , SteeringSpeedMax: f , EngineSpeedStartMin: g , EngineSpeedMax : h , EngineRampTime: i });
			
			
		}
		
		
		// bei einem Klick
		
		$('#StartService').click(StartService);
		$('#StopService').click(StopService);
		$('#SaveSettings').click(SaveSettings);
		$('#RestoreSettings').click(RestoreSettings);

	});
