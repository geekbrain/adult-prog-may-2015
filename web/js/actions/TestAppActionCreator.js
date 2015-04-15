var TestAppDispatcher = require('../dispatcher/TestAppDispatcher');
var TestAppConstants = require('../constants/TestAppConstants');

var ActionTypes = TestAppConstants.ActionTypes;

module.exports = {
    createClick: function(date, clicks, shows) {
        console.log(date, clicks, shows);
        TestAppDispatcher.dispatch({
            type: ActionTypes.RECEIVE_RAW_DATA,
            date: date,
            clicks: clicks,
            shows: shows
        });
    },
    startInterval: function() {
        TestAppDispatcher.dispatch({
            type: ActionTypes.RUN_INTERVAL
        });
    },
    stopInterval: function() {
        TestAppDispatcher.dispatch({
            type: ActionTypes.STOP_INTERVAL
        });
    }
};
