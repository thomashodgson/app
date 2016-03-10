require('signalr');

var getConnection = function() {
    return $.connection.appHub;
}
module.exports = {
    stop: function() {
        getConnection().connection.stop();
    },
    start: function() {
        getConnection().connection.start();
    }
}