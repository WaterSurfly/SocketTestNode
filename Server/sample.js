const wsServer = require("ws").Server;
const url = require('url');

const wss = new wsServer({ port: 8888 }); 
console.log("Server opened on port 8888.");

wss.on("connection", function connection(ws, req) {

    const clientId = req.headers["sec-websocket-key"];
    const parsedUrl = url.parse(req.url, true).query;
    const roomNumber = Number(parsedUrl.room);
    ws.roomNumber = roomNumber;
    
    const ip = req.socket.remoteAddress;
    console.log("Client connected : " + ip + ", id: " + clientId + ", room: " + roomNumber);

    ws.on("message", function message(data, isBinary) {
        wss.clients.forEach(function each(client) {
            if (
                client.readyState === ws.OPEN && 
                client.roomNumber === ws.roomNumber
            ) {
                console.log("broadcast: %s", data);
                client.send(data, { binary: isBinary });
            }
        });
    });

    ws.send("welcome to connected! room: " + roomNumber);

});