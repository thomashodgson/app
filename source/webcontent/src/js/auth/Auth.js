var SignalRConnection = require('../signalr/SignalRConnection.js');
var AjaxClient = require('../api/ajaxClient.js');
var ClientSide = require('../view-model-updates/ClientSide.js');

var wrap = function(promiseF) {
    SignalRConnection.stop();
    return promiseF().done(function() {
        SignalRConnection.start();
    });
}
module.exports = {
    logIn: function(data) {
        return  wrap(() => AjaxClient.post("api/account/LogIn", data))
            .fail(function(err) {
                ClientSide.updateExistingViewModel({
                    Error: {
                        ErrorMessage: "Login failed. Please make sure you are using valid email and password"
                    }
                });
            });
    },
    logOut: function() {
        SignalRConnection.stop();
        ClientSide.createNewViewModel({ Username: undefined });
        return AjaxClient.ping("api/account/LogOut");
    }
}