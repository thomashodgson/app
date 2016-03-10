require('signalr');
var _ = require('underscore');
var Auth = require('../auth/Auth.js');
var $ = require('jquery');


var onAuthFailure = function (replaceState, error) {
    if (error.status === 403) {
        replaceState({
            User: null,
            UserAuthenticationFinished: true
        });
    };
}

module.exports = function(setState, replaceState) {

    var connection = $.connection.appHub;

    connection.client.updateExistingViewModel = function(viewModel) {
        setState(JSON.parse(viewModel));
    };

    connection.client.createNewViewModel = function(viewModel) {
        replaceState(JSON.parse(viewModel));
    };

    Auth.logIn().then($.noop, _.partial(onAuthFailure, replaceState));
}