var React = require('react');
var TestAppActionCreator = require('../actions/TestAppActionCreator');
var Button = require('react-bootstrap').Button;
var Glyphicon = require('react-bootstrap').Glyphicon;

var Clicker = React.createClass({
    render: function() {
        return (
            <Button bsStyle='primary' onClick={this._onClick}><Glyphicon glyph='star'> Clicker</Glyphicon></Button>
        );
    },
    _onClick: function(event) {
        //TestAppActionCreator.createClick("2015-02-04", 10, 1000);
        TestAppActionCreator.startInterval();
    }
});

module.exports = Clicker;
