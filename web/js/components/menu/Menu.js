require('./menu.scss');
var React = require('react');
var Router = require('react-router');
var ListGroup = require('react-bootstrap').ListGroup;
var ListGroupItem = require('react-bootstrap').ListGroupItem;
var Glyphicon = require('react-bootstrap').Glyphicon;

var RouteHandler = Router.RouteHandler;
var Link = Router.Link;

module.exports = React.createClass({
    render: function() {
        return (
            <ListGroup>
                <ListGroupItem className="white">
                    <Link to="/">
                        <Glyphicon glyph='hand-right' />
                        Общая статистика
                    </Link>
                </ListGroupItem>
                <ListGroupItem className="blue">
                    <Link to="dailystat">
                        <Glyphicon glyph='hand-right' />
                        Ежедневная статистика
                    </Link>
                </ListGroupItem>
                <ListGroupItem className="red">
                    <Link to="statbyname">
                        <Glyphicon glyph='hand-right' />
                        Статистика по имени
                    </Link>
                </ListGroupItem>
              </ListGroup>
        );
    }
});
