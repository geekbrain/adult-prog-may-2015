var TestAppDispatcher = require('../dispatcher/TestAppDispatcher');
var TestAppConstants = require('../constants/TestAppConstants');
var EventEmitter = require('events').EventEmitter;
var assign = require('object-assign');

var ActionTypes = TestAppConstants.ActionTypes;

var _data = [];
var _currentID = 0;
var _run = false;
var _intervalID = null;
var _date = new Date();

var CHANGE_EVENT = 'change';

var DataStore = assign({}, EventEmitter.prototype, {
    init: function(rawData) {
       _data.push(rawData);
    },

    getLastUpdated() {
        return _date.toLocaleString();
    },

    run: function() {
        _date = new Date();
        this.emit(CHANGE_EVENT);
        console.log("RUN");
    },

    stop: function() {
        console.log("STOP");
    },

    emitChange: function() {
        this.emit(CHANGE_EVENT);
    },

    addChangeListener: function(callback) {
        this.on(CHANGE_EVENT, callback);
    },

    removeChangeListener: function(callback) {
        this.removeListener(CHANGE_EVENT, callback);
    },

    get: function(id) {
        return _data[id];
    },

    getAll: function() {
        return _data;
    },

    getCurrentID: function() {
        return _currentID;
    },

    getCurrent: function() {
        return this.get(this.getCurrentID());
    }
});

DataStore.dispatchToken = TestAppDispatcher.register(function(action) {
    switch(action.type) {
        case ActionTypes.CLICK_DATA:
            _currentID = action.dataID;
            DataStore.emitChange();
            break;
        case ActionTypes.RECEIVE_RAW_DATA:
            DataStore.init(action);
            DataStore.emitChange();
            break;
        case ActionTypes.STOP_INTERVAL:
            DataStore.stop();
            DataStore.emitChange();
            break;
        case ActionTypes.RUN_INTERVAL:
            DataStore.run();
            DataStore.emitChange();
            break;
        default:
            // do nothing
    }
});

module.exports = DataStore;
