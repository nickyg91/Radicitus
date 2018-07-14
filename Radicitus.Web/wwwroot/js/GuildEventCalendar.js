$(function () {
    GuildEventCalendar.Init();
});

var GuildEventCalendar = new function() {
    this.Init = function () {
        var calEvents = events.map(function(item) {
            return {
                title: item.Title,
                start: item.EventDate
            }
        });
        $("#calendar").fullCalendar({
            defaultView: 'month',
            events: calEvents,
            header: {
                right: ""
            }
        });
    }
}