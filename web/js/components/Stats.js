var React = require('react'),
    Griddle = require('griddle-react');

module.exports = React.createClass({
    render: function() {
        var fakeData = [{
            "Путин": Math.floor(Math.random() * 100 + 1),
            "Медведев": Math.floor(Math.random() * 100 + 1),
            "Навальный": Math.floor(Math.random() * 100 + 1)
        }];
        /*for (var i = 0; i < 1000; i++) {
            fakeData.push({

            });
        }*/
        return (
            <div>
                <h1>Статистика</h1>
                <Griddle results={fakeData} tableClassName="table" showFilter={true}
                    showSettings={true} columns={["Путин", "Медведев", "Навальный"]}/>
            </div>
        );
    }
});
