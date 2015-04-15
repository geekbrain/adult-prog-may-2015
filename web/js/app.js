var React = require('react');
var Router = require('react-router');
var flux = require('flux');

var routes = require('./routes.js');

var DataStore = require('./stores/DataStore');

Router.run(routes, Router.HistoryLocation, function(Handler) {
    React.render(<Handler />, document.body);
});
