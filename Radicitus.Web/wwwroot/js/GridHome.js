﻿var GridHome = {
    AddSuccess: function(result) {
        $.get("/Grid/GetAllGridsPartial",
            function (data) {
                bootbox.alert("The grid was added successfully!", function() {
                    $("#gridsDisplay").empty().append(data);
                    $("#addGridModal").modal("hide");
                });
            });
    },
    AddError: function () {
        $("#addGridModal").modal("hide");
        bootbox.alert("An error occurred while adding the grid.", function() {
            $("#addGridModal").modal("show");
        });
    }
};