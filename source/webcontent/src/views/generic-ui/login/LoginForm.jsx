var React = require('react');

module.exports = React.createClass({

    onKeyPress: function (e) {
        if (e.nativeEvent.keyCode === 13) { //enter
            this.onSelect();
            e.preventDefault();
        }
    },

    onLogin: function () {
        this.props.onLogin({
            Username: $(this.refs.username).val(),
            Password: $(this.refs.password).val()
        });
    },

    render: function () {
        return (<form onKeyPress={this.onKeyPress }>
                    <input ref="username" placeholder="user name" />
                    <input ref="password" type="password" placeholder="password" />
                    <button className="button button--primary" type="button" onClick={this.onLogin}>{this.props.buttonText}</button>
                </form>);
}
})