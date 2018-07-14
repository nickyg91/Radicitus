$(function () {
    Admin.Init();
});

var Admin = new function () {
    this.Init = function () {
        $("#Content").on("keyup",
            function () {
                var text = $(this).val();
                if (text.indexOf("<script>") > 0 || text.indexOf("</script>")) {
                    text = text.replace("<script>", "");
                    text = text.replace("</script>", "");
                }
                $(".preview").html(text);
            });
        $("#eventDate").datetimepicker();

        $(".edit-event").on("click",
            function () {
                var currentButton = $(this);
                var eventId = currentButton.attr("id");
                $.get("/Admin/EditEvent?eventId=" + eventId,
                    function (response) {
                        $("#createEvent").empty().append(response);
                    });
            });

        $(".delete-event").on("click",
            function () {
                var currentButton = $(this);
                bootbox.confirm("Are you sure you want to delete this?",
                    function (response) {
                        if (response) {
                           
                            var eventId = currentButton.attr("id");
                            $.post("/Admin/DeleteEvent", { eventId: eventId },
                                function (response) {
                                    if (response) {
                                        location.reload();
                                    } else {
                                        bootbox.alert("Your item was not deleted.");
                                    }
                                });
                        } else {
                            return;
                        }
                    });
            });

    };
    this.NewsFeedAdded = function (response) {
        bootbox.alert(response.message);
    };
    this.NewsFeedError = function () {
        bootbox.alert("An error occurred while adding your news feed item!");
    };
    this.CreateEventSuccess = function () {
        //lol too lazy
        location.reload();
    };
    this.CreateEventFailure = function () {
        bootbox.alert("An error occurred while creating this event.");
    };
    this.UpdateEventSuccess = function () {
        location.reload();
    };
};