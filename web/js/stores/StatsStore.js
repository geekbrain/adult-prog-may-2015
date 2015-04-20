var Dispatcher   = require('../dispatcher/Dispatcher'),
    Constants    = require('../constants/Constants'),
    EventEmitter = require('events').EventEmitter,
    assign       = require('object-assign'),

    ActionTypes  = Constants.ActionTypes,

    _data        = [];

var StatsStore = assign({}, EventEmitter.prototype, {
    init: function(rawData) {
       _data.push(rawData);
    },

    emitChange: function() {
        this.emit(CHANGE_EVENT);
    },

    addChangeListener: function(callback) {
        this.on(CHANGE_EVENT, callback);
    },

    removeChangeListener: function(callback) {
        this.removeListener(CHANGE_EVENT, callback);
    }
});

StatsStore.dispatchToken = Dispatcher.register(function(action) {
    switch(action.type) {
        case ActionTypes.CLICK_DATA:
            _currentID = action.dataID;
            StatsStore.emitChange();
            break;
        default:
            // do nothing
    }
});

module.exports = StatsStore;
