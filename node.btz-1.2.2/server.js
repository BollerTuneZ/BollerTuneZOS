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

		io.sockets.emit('Lenkung', { LenkMax: x });				
		// der Client ist verbunden
		socket.on('Lenkung', function (data) {
			// so wird dieser Text an alle anderen Benutzer gesendet
			x = data.LenkMax;
			
			io.sockets.emit('Lenkung', { LenkMax: x, LenkMin: data.LenkMin, LenkCenter: data.LenkCenter });
			
			console.log("Lenkung");
			console.log("Lenkung Max : " + data.LenkMax);
			console.log("Lenkung Min : " + data.LenkMin);
			console.log("Lenkung Center : " + data.LenkCenter);
			console.log("");
			
			
			
			
			
		});
		
		
		socket.on('LenkungSpeed', function (data) {
			// so wird dieser Text an alle anderen Benutzer gesendet
			io.sockets.emit('LenkungSpeed', { LenkSpeedMax: data.LenkSpeedMax, LenkSpeedMin: data.LenkSpeedMin });
			
			console.log("Lenkung Speed");
			console.log("Lenkmotor Speed Max : " + data.LenkSpeedMax);
			console.log("Lenkmotor Speed Min : " + data.LenkSpeedMin);
			console.log("");

		});
		
		
		socket.on('tol', function (data) {
			// so wird dieser Text an alle anderen Benutzer gesendet
			io.sockets.emit('tol', { tole: data.tole });
			console.log("Winkelgeber");
			console.log("Lenkung Toleranz : " + data.tole);
			console.log("");
			

		});
		
		socket.on('refresh', function (data) {
			
			io.sockets.emit('refresh', { ref: data.ref });
			console.log(data.ref);
			console.log("");
			
		});
	
	
});




// Portnummer in die Konsole schreiben
console.log('Der Server läuft nun unter http://127.0.0.1:' + conf.port + '/');
