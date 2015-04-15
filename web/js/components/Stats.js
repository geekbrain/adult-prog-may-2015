var React = require('react');
var DataStore = require('../stores/DataStore');
var Clicker = require('./Clicker');
var Stopper = require('./Stopper');
var Panel = require('react-bootstrap').Panel;

function getStateFromStores() {
    return {
        data: DataStore.getAll(),
        currentID: DataStore.getCurrentID()
    };
}

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
        var dataList = this.state.data.map(function(row) {
            return (
                <Panel header={row.date}>
                Shows: {row.shows} Clicks: {row.clicks}
                </Panel>
            )
        }, this);

        return (
            <div>
            <h1>Статистика</h1>
            <Clicker />
            <Stopper />
            {dataList}
            </div>
        );
    },

    _onChange: function() {
        this.setState(getStateFromStores());
    }
});
