var React = require('react');
var LoginForm = require('./LoginForm.jsx');
var Auth = require('../../../js/auth/Auth.js');
var ClientSide = require('../../../js/view-model-updates/ClientSide.js');

var Auth = require('../../../js/auth/Auth.js');

module.exports = React.createClass({
    onLoginForm : function(data) {
        Auth.logIn(data);
    },
    render: function() {
        return (<LoginForm onLogin = {this.onLoginForm} buttonText = "Log In"></LoginForm>);
    }
});