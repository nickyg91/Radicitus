$(function() {
    Admin.Init();
});

var Admin = new function() {
    this.Init = function() {
        $("#Content").on("keyup",
            function() {
                var text = $(this).val();
                if (text.indexOf("<script>") > 0 || text.indexOf("</script>")) {
                    text = text.replace("<script>", "");
                    text = text.replace("</script>", "");
                }
                $(".preview").html(text);
            });
    };
};