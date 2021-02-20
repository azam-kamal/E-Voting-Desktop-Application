const http = require('http');
const port = process.env.port || 3000;
//const port = 1433;
const app = require('./app');

const server = http.createServer(app);

server.listen(port);