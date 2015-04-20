var React = require('react');
var Glyphicon = require('react-bootstrap').Glyphicon;

var Header = React.createClass({
    render: function() {
        return (
            <header className="main">
                <h1><Glyphicon glyph='star'/> Рейтинг популярности <Glyphicon glyph='star'/></h1>
                <small>made by Geekbrains' students</small>
            </header>
        );
    }
});

module.exports = Header;
