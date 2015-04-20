var Dispatcher = require('../dispatcher/Dispatcher');
var Constants = require('../constants/Constants');

var ActionTypes = Constants.ActionTypes;

module.exports = {
    createClick: function(date, clicks, shows) {
        Dispatcher.dispatch({
            type: ActionTypes.RECEIVE_RAW_DATA,
            date: date,
            clicks: clicks,
            shows: shows
        });
    }
};
