var React = require('react');
var TestAppActionCreator = require('../actions/TestAppActionCreator');
var Button = require('react-bootstrap').Button;
var Glyphicon = require('react-bootstrap').Glyphicon;

var Stopper = React.createClass({
    render: function() {
        return (
            <Button bsStyle='primary' onClick={this._onClick}><Glyphicon glyph='user'> Stopper</Glyphicon></Button>
        );
    },
    _onClick: function(event) {
        TestAppActionCreator.stopInterval();
    }
});

module.exports = Stopper;
