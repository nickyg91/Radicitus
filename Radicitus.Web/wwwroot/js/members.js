var MemberGrid = {
    Init: function () {
        var members = [];
        var gridId = $("#hiddenGridId").val();

        var addAlert = function(message, type) {
            var error = "<p>" + message + "</p>";
            $("#alertArea").removeAttr("class")
                .addClass("alert alert-" + type)
                .empty()
                .append(error)
                .fadeIn(1000).fadeOut(5000);
        }

        var clearSession = function () {
            $.get("/Grid/ClearGridState/" + gridId);
            members = [];
            $("#memberTable > tbody").empty();
            $("#memberNameTextBox").val("");
            $("#totalNumbersToRandomize").val("");
            $("#numbersTextArea").val("");
        }

        $("#drawWinnerButton").on("click",
            function() {
                $.get("/Grid/DrawWinner/" + gridId,
                    function(result) {
                        $("#winnerArea").text(result.message);
                    });
            });

        $("#closeAddMembersForm").on("click", function() {
            $("#addMemberModal").modal("hide");
            bootbox.confirm("Are you sure you want to close this? Any unsaved changes will be lost.",
                function(result) {
                    if (result) {
                        clearSession();
                        $("#addMemberModal").modal("hide");
                    } else {
                        $("#addMemberModal").modal("show");
                    }
                });
        });
        
        var randomizeNumbers = function (totalNumbers) {
            $.get("/Grid/RandomizeNumbers/" + totalNumbers + "/" + gridId,
                function (result) {
                    $("#numbersTextArea").val(result.numbers);
                }).fail(function(result) {
                    bootbox.alert(result.responseJSON.message);
                });
        };

        var refreshGrid = function(addedMembers) {
            $.each(addedMembers,
                function(index) {
                    var number = addedMembers[index].gridNumber;
                    var name = addedMembers[index].radMemberName;
                    $("#name_" + number).empty().append("<strong>" + name + "</strong>");
                    $("#itemContainer_" + number).addClass("text-white ");
                    $("#itemContainer_" + number).parent().addClass("bg-primary");
                });
        };

        var addMembersToGrid = function() {
            $.post("/Grid/AddMembers",
                { "members": members },
                function (result) {
                    $("#addMemberModal").modal("hide");
                    $("#memberTable > tbody").empty();
                    members = [];
                    bootbox.alert(result.message,
                        function() {
                            refreshGrid(result.members);
                        });
                }).fail(function(result) {
                addAlert(result.responseJSON.message, "danger");
            });
        };

        var addMemberToTable = function (member) {
            member.GridId = gridId;
            $.post("/Grid/AddMember",
                { "member": member },
                function (result) {
                    if (result.status === 302) {
                        addAlert(result.message, "warning");
                    } else {
                        addAlert(result.message, "success");
                        //we will get fancy later with jq data tables.
                        var button =
                            "<button class='member-button btn btn-danger'><i class='fa fa-trash'></i> Remove</button>";
                        var tr = "<tr><td>" + member.MemberName + "</td><td> " + member.NumberCsv + "</td><td>" + button + "</td></tr>";
                        $("#memberTable > tbody:last-child").append(tr);
                        $("#memberNameTextBox").val("");
                        $("#totalNumbersToRandomize").val();
                        $("#numbersTextArea").val();
                        if ($("#submitMembersButton").is(":disabled")) {
                            $("#submitMembersButton").prop("disabled", false);
                        }
                        $(".member-button", "#memberTable > tbody:last-child").on("click",
                            function () {
                                $.post("/Grid/RemoveMember", { "member": member }, function () {
                                    $("#memberTable > tbody > tr:last").remove();
                                }).fail(function () {

                                });
                            });
                        members.push(member);
                    }
                }).fail(function (result) {
                    addAlert(result.responseJSON.message, "danger");
                });
        };

        $("#submitMembersButton").on("click",
            function() {
                addMembersToGrid();
            });

        $("#addMemberToTable").on("click",
            function() {
                if ($("#addMemberToTableForm").valid()) {
                    var member = {
                        "MemberName": $("#memberNameTextBox").val(),
                        "NumberCsv": $("#numbersTextArea").val()
                    };
                    addMemberToTable(member);
                }
            });

        $("#randomizeNumbers").on("click",
            function () {
                var totalNumbers = $("#totalNumbersToRandomize").val();
                randomizeNumbers(totalNumbers);
            });

        $("#totalNumbersToRandomize").on("change",
            function () {
                var val = $(this).val();
                if (val !== "" && parseInt(val) && $("#totalNumbersToRandomize").valid()) {
                    $("#randomizeNumbers").prop("disabled", false);
                } else {
                    $("#randomizeNumbers").prop("disabled", true);
                }
            });
    }
};

(function () {
    MemberGrid.Init();
})();

