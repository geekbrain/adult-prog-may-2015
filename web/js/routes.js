var React = require('react');
var Router = require('react-router');

var DefaultRoute = Router.DefaultRoute;
var Route = Router.Route;

var App = require('./components/Main');
var DailyStats = require('./components/DailyStats.js');
var StatsByName = require('./components/StatsByName.js');
var Stats = require('./components/Stats.js');

var routes = (
    <Route name="app" path="/" handler={App}>
        <Route name="dailystat" handler={DailyStats}/>
        <Route name="statbyname" handler={StatsByName}/>
        <DefaultRoute handler={Stats}/>
    </Route>
);

module.exports = routes;
