var React = require('react');
var startSingalR = require('../js/signalR/StartSignalR.js');
var ClientSideViewModelUpdates = require('../js/view-model-updates/ClientSide.js');
var _ = require('underscore');
var Router = require('../js/router/Router.js');

var Home = require('./home/Home.jsx');

var ErrorFeed = require('./generic-ui/errors/Errors.jsx');
var LogIn = require("./generic-ui/login/LogIn.jsx");
var Four0Four = require('./generic-ui/four0four/Four0Four.jsx');
var Loader = require('./generic-ui/loader/Loader.jsx');

Router.initialize([
   {
        pattern: "Home",
        page: "Home",
        getView: function(state) {
            return <Home HelloMessageViewModel={state.HelloMessageViewModel}></Home>;
        }
    },{
        pattern: ".*",
        page: "404",
        disableUrlUpdates: true,
        getView: function() {
            return <Four0Four></Four0Four>;
        }
    }
]);

module.exports = React.createClass({
    getInitialState: function () {
        return Router.getViewModelFromUrl();
    },

    componentDidMount: function () {
        ClientSideViewModelUpdates.initialize(_.bind(this.setState, this), _.bind(this.replaceState, this));
        startSingalR(_.bind(this.setState, this), _.bind(this.replaceState, this));
    },
    
    componentDidUpdate: function() {
        Router.updateUrlHash(this.state);
    },

    render: function () {
        var view;
        
        if (this.state.UserAuthenticationFinished) {
            view = this.state.User ? Router.getView(this.state) : <LogIn />;
        } else {
            view = <Loader text="Authenticating..."></Loader>;
        }
        
        return (
            <div>
                <div>
                    {view}
                </div>
                <ErrorFeed newErrorMessage={this.state.Error}></ErrorFeed>
            </div>
        );
    }
})