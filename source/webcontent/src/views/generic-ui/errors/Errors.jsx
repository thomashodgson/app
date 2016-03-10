var React = require('react');
var moment = require('moment');
var _ = require('underscore');

module.exports = React.createClass({

    getInitialState: function(){
        return {errors: []};
    },

    componentWillReceiveProps: function(nextProps){
        if (_.isEqual(nextProps.newErrorMessage, this.lastProcessedError)) {
            return;
        }

        this.lastProcessedError = nextProps.newErrorMessage;
        var currentErrors = this.state.errors;
        currentErrors.push(nextProps.newErrorMessage);
        this.setState({errors: currentErrors});
    },

    dismissError: function(errorToDismiss) {
        this.setState({ errors:  _.difference(this.state.errors, [errorToDismiss])})
    },

    createError: function(error, index){
        return error ? <li className="errorFeed-row" key={"Error-" + index}>
                            <div className="errorFeed-message">
                                <div className="errorFeed-time">{moment(error.Time).calendar()}</div>
                                <div>{error.ErrorMessage}</div>
                            </div>
                            <div className="errorFeed-dismiss" onClick={_.bind(this.dismissError, this, error)}>x</div>
        </li> : null;
    },

    render: function () {
        return (
            <div className="errorFeed">
                <ul>
                    {_.map(this.state.errors.reverse(), this.createError)}
                </ul>
            </div>
        );
    }
})