var MemberGrid = {
    Init: function () {
        //var members = [];
        var gridId = $("#hiddenGridId").val();

        var randomizeNumbers = function (totalNumbers) {
            $.get("/Grid/RandomizeNumbers/" + totalNumbers + "/" + gridId,
                function (result) {
                    $("#numbersTextArea").val(result.numbers);
                }).fail(function(result) {
                    bootbox.alert(result.message);
                });
        };
        
        var addMemberToTable = function (member) {
            member.GridId = gridId;
            $.post("/Grid/AddMember",
                { "member": member },
                function(result) {
                    $("#addMemberModal").modal("hide");
                    bootbox.alert(result.message,
                        function() {
                            $("#addMemberModal").modal("show");
                            var tr = "<tr><td>" + member.MemberName + "</td><td> " + member.NumberCsv + "</td><td></td></tr>";
                            if ($("#memberTable > tbody > tr").length === 0) {
                                $("#memberTable > tbody").append(tr);
                            } else {
                                $("#memberTable > tbody > tr").after(tr);
                            }
                            $("#addMemberToTableForm")[0].reset();
                        });

                }).fail(function(result) {
                $("#addMemberModal").modal("hide");
                bootbox.alert(result.message,
                    function() {
                        $("#addMemberModal").modal("show");
                    });
                });
            //we will get fancy later with jq data tables.
            
        };

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

