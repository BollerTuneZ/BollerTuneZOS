
var socket = io.connect();
$(document).ready(function(){

 	/*
	
	RestoreSettingsDOM(); 
*/
	
	
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
		
/*
// nur zum testen ! 
		function RestoreSettingsDOM(){
		
			
			socket.emit('EngineConfigDOM', { EngineSpeedMax_MaxDOM: 132 , EngineSpeedMax_MinDOM: 12 , EngineRampTime_MaxDOM: 231 , EngineRampTime_MinDOM: 54 , EngineSpeedStartMin_MaxDOM: 1000 , EngineSpeedStartMin_MinDOM: 22 });

			socket.emit('SteeringConfigDOM', { SteeringRangeMax_MaxDOM: 434 , SteeringRangeMax_MinDOM: 32 , SteeringRangeMin_MaxDOM: 231 , SteeringRangeMin_MinDOM: 21 , SteeringCenter_MaxDOM: 322 , SteeringCenter_MinDOM: 6 , SteeringToleranz_MaxDOM: 1211 , SteeringToleranz_MinDOM: 454 });
			console.log("RestoreSettings");
			
			socket.emit('SteeringMotorConfigDOM', { SteeringSpeedMax_MaxDOM: 342 , SteeringSpeedMax_MinDOM: 42 , SteeringSpeedMin_MaxDOM: 422 , SteeringSpeedMin_MinDOM: 22 });
			console.log("RestoreSettings");

			
		}
*/

		
		// bei einem Klick
		
		$('#StartService').click(StartService);
		$('#StopService').click(StopService);
		$('#SaveSettings').click(SaveSettings);
		$('#RestoreSettings').click(RestoreSettings;

	});
