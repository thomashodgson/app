global.$ = require('jquery');
// the 'jQuery' definition is needed for the generated signalR files
global.jQuery = global.$;

var React = require('react');
var ReactDOM = require('react-dom');
var Page = require('./views/Page.jsx');



$(function () {
        ReactDOM.render(
            <Page></Page>,
            document.getElementById("react-element")
        );
});