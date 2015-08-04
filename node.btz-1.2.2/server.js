var express = require('express')
,   app = express()
,   server = require('http').createServer(app)
,   io = require('socket.io').listen(server)
,   conf = require('./config.json');

// Daten


var SteeringConfig = {
    SteeringRangeMax: 0 ,
    SteeringRangeMin: 0 ,
    SteeringCenter: 0 ,
    SteeringToleranz: 0
};
 
var SteeringMotorConfig = {
	SteeringSpeedMax: 0 , 
	SteeringSpeedMin: 0
	
};

var EngineConfig = {
	EngineSpeedStartMin: 0 , 
	EngineSpeedMax: 0,
	EngineRampTime: 0
	
};



// Webserver
// auf den Port x schalten
server.listen(conf.port);

app.configure(function(){
	// statische Dateien ausliefern
	app.use(express.static(__dirname + '/public'));
});



// wenn der Pfad / aufgerufen wird
app.get('/', function (req, res) {
	// so wird die Datei index.html ausgegeben
	res.sendfile(__dirname + '/public/index.html');
});

app.get('/lenkung', function (req, res) {
	// so wird die Datei index.html ausgegeben
	res.sendfile(__dirname + '/public/lenkung.html');
});

app.get('/index', function (req, res) {
	// so wird die Datei index.html ausgegeben
	res.sendfile(__dirname + '/public/index.html');
});

app.get('/antrieb', function (req, res) {
	// so wird die Datei index.html ausgegeben
	res.sendfile(__dirname + '/public/antrieb.html');
});

app.get('/steuerung', function (req, res) {
	// so wird die Datei index.html ausgegeben
	res.sendfile(__dirname + '/public/steuerung.html');
});

app.get('/test', function (req, res) {
	// so wird die Datei index.html ausgegeben
	res.sendfile(__dirname + '/public/test.html');
});

var x = 0;

// Websocket
io.sockets.on('connection', function (socket) {

		// der Client ist verbunden
		
		
		// socket.id wird ausgelesen und in der console ausgegeben 
		var sessionid = socket.id;
		console.log(sessionid);
		
		
		// wenn ein neuer Client verbunden ist wird ihm jäglicher inhalt einmal zugesendet 
		io.sockets.emit('SteeringConfig', { SteeringRangeMax: SteeringConfig.SteeringRangeMax , SteeringRangeMin: SteeringConfig.SteeringRangeMin, SteeringCenter: SteeringConfig.SteeringCenter, SteeringToleranz: SteeringConfig.SteeringToleranz });
					
		
		io.sockets.emit('SteeringMotorConfig', { SteeringSpeedMax: SteeringMotorConfig.SteeringSpeedMax, SteeringSpeedMin: SteeringMotorConfig.SteeringSpeedMin });
		
		
		
		io.sockets.emit('EngineConfig', { EngineSpeedStartMin: EngineConfig.EngineSpeedStartMin, EngineSpeedMax: EngineConfig.EngineSpeedMax, EngineRampTime: EngineConfig.EngineRampTime });
	
		
		socket.on('SteeringConfig', function (data) {
			// so wird dieser Text an alle anderen Benutzer gesendet
			SteeringConfig.SteeringRangeMax = data.SteeringRangeMax;
			SteeringConfig.SteeringRangeMin = data.SteeringRangeMin;
			SteeringConfig.SteeringCenter = data.SteeringCenter;
			SteeringConfig.SteeringToleranz = data.SteeringToleranz;
			
			io.sockets.emit('SteeringConfig', { SteeringRangeMax: SteeringConfig.SteeringRangeMax , SteeringRangeMin: SteeringConfig.SteeringRangeMin, SteeringCenter: SteeringConfig.SteeringCenter, SteeringToleranz: SteeringConfig.SteeringToleranz });
			
		/*
	console.log("Lenkung");
			console.log("Lenkung Max : " + data.SteeringRangeMax);
			console.log("Lenkung Min : " + data.SteeringRangeMin);
			console.log("Lenkung Center : " + data.SteeringCenter);
			console.log("");
*/
		});
		
		
		socket.on('SteeringMotorConfig', function (data) {
			// so wird dieser Text an alle anderen Benutzer gesendet
			SteeringMotorConfig.SteeringSpeedMax = data.SteeringSpeedMax;
			SteeringMotorConfig.SteeringSpeedMin = data.SteeringSpeedMin;
			
			io.sockets.emit('SteeringMotorConfig', { SteeringSpeedMax: SteeringMotorConfig.SteeringSpeedMax, SteeringSpeedMin: SteeringMotorConfig.SteeringSpeedMin });
		/*
	console.log("Lenkung Speed");
			console.log("Lenkmotor Speed Max : " + data.SteeringSpeedMax);
			console.log("Lenkmotor Speed Min : " + data.SteeringSpeedMin);
			console.log("");
*/

		});
		
		socket.on('EngineConfig', function (data) {
		
			EngineConfig.EngineSpeedStartMin = data.EngineSpeedStartMin;
			EngineConfig.EngineSpeedMax = data.EngineSpeedMax;
			EngineConfig.EngineRampTime = data.EngineRampTime;
			// so wird dieser Text an alle anderen Benutzer gesendet
			io.sockets.emit('EngineConfig', { EngineSpeedStartMin: EngineConfig.EngineSpeedStartMin, EngineSpeedMax: EngineConfig.EngineSpeedMax, EngineRampTime: EngineConfig.EngineRampTime });
			/*
console.log("Motor Speed");
			console.log("Motor Start Speed Max : " + data.EngineSpeedStartMin);
			console.log("Motor Speed Max : " + data.EngineSpeedMax);
			console.log("Motor Start Rampe : " + data.EngineRampTime);
			console.log("");
*/

		});
		
		socket.on('RestoreSettings', function() {
			io.sockets.emit('RestoreSettings');			
		});
		
		socket.on('EngineConfigDOM', function (data) {
			io.sockets.emit('EngineConfigDOM', { EngineSpeedMax_MaxDOM: data.EngineSpeedMax_MaxDOM , EngineSpeedMax_MinDOM: data.EngineSpeedMax_MinDOM , EngineRampTime_MaxDOM: data.EngineRampTime_MaxDOM , EngineRampTime_MinDOM: data.EngineRampTime_MinDOM , EngineSpeedStartMin_MaxDOM: data.EngineSpeedStartMin_MaxDOM , EngineSpeedStartMin_MinDOM: data.EngineSpeedStartMin_MinDOM });
						
			console.log(data.EngineSpeedMax_MaxDOM);
			console.log(data.EngineSpeedMax_MinDOM);
	
			console.log(data.EngineRampTime_MaxDOM);
			console.log(data.EngineRampTime_MinDOM);
			
			console.log(data.EngineSpeedStartMin_MaxDOM);
			console.log(data.EngineSpeedStartMin_MinDOM);
			
			
		});
		
		socket.on('SteeringConfigDOM', function (data) {
			io.sockets.emit('SteeringConfigDOM', { SteeringRangeMax_MaxDOM: data.SteeringRangeMax_MaxDOM , SteeringRangeMax_MinDOM: data.SteeringRangeMax_MinDOM , SteeringRangeMin_MaxDOM: data.SteeringRangeMin_MaxDOM , SteeringRangeMin_MinDOM: data.SteeringRangeMin_MinDOM , SteeringCenter_MaxDOM: data.SteeringCenter_MaxDOM , SteeringCenter_MinDOM: data.SteeringCenter_MinDOM , SteeringToleranz_MaxDOM: data.SteeringToleranz_MaxDOM , SteeringToleranz_MinDOM: data.SteeringToleranz_MinDOM });
			
		});
		
		socket.on('SteeringMotorConfigDOM', function (data) {
			//io.sockets.emit('SteeringMotorConfigDOM', { SteeringSpeedMax_MaxDOM: data.SteeringSpeedMax_MaxDOM , SteeringSpeedMax_MinDOM: data.SteeringSpeedMax_MinDOM , SteeringSpeedMin_MaxDOM: data.SteeringSpeedMin_MaxDOM , SteeringSpeedMin_MinDOM: data.SteeringSpeedMin_MinDOM });
			console.log("JOJO");
			console.log(data.SteeringSpeedMax_MaxDOM);
			console.log(data.SteeringSpeedMax_MinDOM);
			console.log(data.SteeringSpeedMin_MaxDOM);
			console.log(data.SteeringSpeedMin_MinDOM);
		});
		


		socket.on('StartService', function() {
			
			io.sockets.emit('StartService');
/* 			console.log("StartService"); */
			
		});
		
		socket.on('StopService', function() {
		
			io.sockets.emit('StopService');
			/* console.log("StopService"); */
			
		});
		
		socket.on('SaveSettings', function() {
			
			io.sockets.emit('SaveSettings');
			/* console.log("SaveSettings"); */
			
		});
		
		socket.on('geodata', function(data) {
			
			io.sockets.emit('geodata', { latitude: data.latitude, longitude: data.longitude});
			console.log(data.latitude + " " + data.longitude); 
			
		});
		
		
	
	
});




// Portnummer in die Konsole schreiben
console.log('Der Server läuft nun unter http://127.0.0.1:' + conf.port + '/');
