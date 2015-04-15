var React = require('react');
var DataStore = require('../stores/DataStore');

function getStateFromStores() {
    return {
        lastDate: DataStore.getLastUpdated()
    };
};

module.exports = React.createClass({
    getInitialState: function() {
        return getStateFromStores();
    },

    componentDidMount: function() {
        DataStore.addChangeListener(this._onChange);
    },

    componentWillUnmount: function() {
        DataStore.removeChangeListener(this._onChange);
    },

    render: function() {
        return (
            <span><nobr>Последнее обновление: {this.state.lastDate}</nobr></span>
        );
    },

    _onChange: function() {
        this.setState(getStateFromStores());
    }
});
