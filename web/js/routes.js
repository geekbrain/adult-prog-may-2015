var React = require('react');
var Router = require('react-router');

var DefaultRoute = Router.DefaultRoute;
var Route = Router.Route;

var App = require('./components/Main');
var Generator = require('./components/Generator.js');
var Stats = require('./components/Stats.js');
var Dashboard = require('./components/Dashboard.js');

var routes = (
    <Route name="app" path="/" handler={App}>
        <Route name="dailystat" handler={Generator}/>
        <Route name="statbyname" handler={Stats}/>
        <DefaultRoute handler={Dashboard}/>
    </Route>
);

module.exports = routes;
