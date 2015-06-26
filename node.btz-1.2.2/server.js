var express = require('express')
,   app = express()
,   server = require('http').createServer(app)
,   io = require('socket.io').listen(server)
,   conf = require('./config.json')
, 	nodeCouchDB = require("node-couchdb");

// Datenbank 

var couch = new nodeCouchDB("localhost", 5984);


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

var x = 0;

// Websocket
io.sockets.on('connection', function (socket) {

		io.sockets.emit('RestoreSettings', { LenkMax: x });	
		
		
					
		// der Client ist verbunden
		socket.on('SteeringConfig', function (data) {
			// so wird dieser Text an alle anderen Benutzer gesendet
			x = data.LenkMax;
			
			io.sockets.emit('SteeringConfig', { SteeringRangeMax: data.SteeringRangeMax , SteeringRangeMin: data.SteeringRangeMin, SteeringCenter: data.SteeringCenter, SteeringToleranz: data.SteeringToleranz });
			
			console.log("Lenkung");
			console.log("Lenkung Max : " + data.SteeringRangeMax);
			console.log("Lenkung Min : " + data.SteeringRangeMin);
			console.log("Lenkung Center : " + data.SteeringCenter);
			console.log("");
		});
		
		
		socket.on('SteeringMotorConfig', function (data) {
			// so wird dieser Text an alle anderen Benutzer gesendet
			io.sockets.emit('SteeringMotorConfig', { SteeringSpeedMax: data.SteeringSpeedMax, SteeringSpeedMin: data.SteeringSpeedMin });
			console.log("Lenkung Speed");
			console.log("Lenkmotor Speed Max : " + data.SteeringSpeedMax);
			console.log("Lenkmotor Speed Min : " + data.SteeringSpeedMin);
			console.log("");

		});
		
		socket.on('EngineConfig', function (data) {
			// so wird dieser Text an alle anderen Benutzer gesendet
			io.sockets.emit('EngineConfig', { EngineSpeedStartMin: data.EngineSpeedStartMin, EngineSpeedMax: data.EngineSpeedMax, EngineRampTime: data.EngineRampTime });
			console.log("Motor Speed");
			console.log("Motor Start Speed Max : " + data.EngineSpeedStartMin);
			console.log("Motor Speed Max : " + data.EngineSpeedMax);
			console.log("Motor Start Rampe : " + data.EngineRampTime);
			console.log("");

		});
		

		socket.on('StartService', function() {
			
			io.sockets.emit('StartService');
			console.log("StartService");
			
		});
		
		socket.on('StopService', function() {
		
			io.sockets.emit('StopService');
			console.log("StopService");
			
		});
		
		socket.on('SaveSettings', function() {
			
			io.sockets.emit('SaveSettings');
			console.log("SaveSettings");
			
		});
		
		socket.on('RestoreSettings', function() {
			
			io.sockets.emit('RestoreSettings');
			console.log("RestoreSettings");
			
		});
	
	
});




// Portnummer in die Konsole schreiben
console.log('Der Server läuft nun unter http://127.0.0.1:' + conf.port + '/');
