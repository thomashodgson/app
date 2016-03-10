var React = require('react');

module.exports = React.createClass({
    render: function () {
        return (<div>
                     <div className="loader"></div>
                     <div className="loader-text">{this.props.text}</div>
                </div>);
    }
})