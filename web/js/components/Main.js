var React         = require('react'),
    Router        = require('react-router'),
    Col           = require('react-bootstrap').Col,
    Grid          = require('react-bootstrap').Grid,
    Row           = require('react-bootstrap').Row,
    Nav           = require('react-bootstrap').Nav,
    NavItem       = require('react-bootstrap').NavItem,
    Header        = require('./header/Header'),
    Menu          = require('./menu/Menu'),
    ListGroup     = require('react-bootstrap').ListGroup,
    ListGroupItem = require('react-bootstrap').ListGroupItem,
    Glyphicon     = require('react-bootstrap').Glyphicon,

    RouteHandler  = Router.RouteHandler,
    Link          = Router.Link;

module.exports = React.createClass({
    render: function() {
        return (
            <Grid fluid={true}>
                <Row className='show-grid'>
                    <Col xsOffset={3} xs={6}>
                        <Header />
                    </Col>
                </Row>
                <Row className='show-grid'>
                    <Col className="section main-menu" sm={3} md={2}>
                        <Menu />
                    </Col>
                    <Col className="section" sm={3} md={2}>

                    </Col>
                    <Col className="section main-content" sm={6} md={8}>
                        <RouteHandler />
                    </Col>
                </Row>
            </Grid>
        );
    }
});
