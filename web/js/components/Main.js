require('./_styles/_config.scss');
var React = require('react');
var Router = require('react-router');
var Col = require('react-bootstrap').Col;
var Grid = require('react-bootstrap').Grid;
var Row = require('react-bootstrap').Row;
var Nav = require('react-bootstrap').Nav;
var NavItem = require('react-bootstrap').NavItem;
var StatsStatus = require('./StatsStatus');
var Header = require('./header/Header');
var Menu = require('./menu/Menu');
var ListGroup = require('react-bootstrap').ListGroup;
var ListGroupItem = require('react-bootstrap').ListGroupItem;
var Glyphicon = require('react-bootstrap').Glyphicon;

var RouteHandler = Router.RouteHandler;
var Link = Router.Link;

module.exports = React.createClass({
    render: function() {
        return (
            <Grid>
                <Row className='show-grid'>
                    <Col xsOffset={3} xs={6}>
                        <Header />
                    </Col>
                </Row>
                <Row className='show-grid'>
                    <Col className="section" sm={3}>
                        <Menu />
                    </Col>
                    <Col className="section" sm={3}>
                        <RouteHandler />
                    </Col>
                    <Col className="section" sm={6}>

                    </Col>
                </Row>
            </Grid>
        );
    }
});
