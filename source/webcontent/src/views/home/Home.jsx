var React = require('react');
var ServerRequests = require('../../js/view-model-updates/ServerRequests.js');
var $ = require('jquery');
var Loader = require('../generic-ui/loader/Loader.jsx');

var HelloMessage = React.createClass({
    render: function() {
        return this.props.IsLoading ? <Loader text="Saying hello to server..."></Loader> : <div>{this.props.Message}</div>;
    }
});

module.exports = React.createClass({
    sayHello: function() {
        ServerRequests.sayHello($(this.refs.input).val());
    },

    sayBye: function () {
        ServerRequests.sayBye($(this.refs.input).val());
    },

    render: function () {
        return (<div>
                    Name: <input ref="input"/><button onClick={this.sayHello}>hello</button><button onClick={this.sayBye}>bye</button>
                    {this.props.HelloMessageViewModel ? <HelloMessage Message={this.props.HelloMessageViewModel.Message} IsLoading={this.props.HelloMessageViewModel.IsLoading}></HelloMessage> : null}
                </div>);
    }
})