(function() {
    GridHome.Init();
});

var GridHome = {
    Init: function() {

    },
    AddSuccess: function(result) {
        $.get("/Grid/GetAllGridsPartial",
            function(data) {
                $("#gridsDisplay").empty().append(data);
                $("#addGridModal").modal("hide");
            });
    },
    AddError: function () {
        $("#addGridModal").modal("hide");
        bootbox.alert("An error occurred while adding the grid.", function() {
            $("#addGridModal").modal("show");
        });
    }
};