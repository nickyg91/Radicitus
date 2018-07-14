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
            function() {
                var currentButton = $(this);
                var eventId = currentButton.attr("id");
                $.get("/Admin/EditEvent?eventId=" + eventId,
                    function(response) {
                        $("#createEvent").empty().append(response);
                    });
            });

    };
    this.NewsFeedAdded = function(response) {
        bootbox.alert(response.message);
        //lol too lazy
        window.reload();
    }
    this.NewsFeedError = function() {
        bootbox.alert("An error occurred while adding your news feed item!");
    };
};