var MemberGrid = {
    Init: function () {
        var members = [];

        var randomizeNumbers = function (totalNumbers, gridId) {
            $.get("/Grid/RandomizeNumbers/" + totalNumbers + "/" + gridId,
                function (result) {
                    $("#numbersTextArea").val(result.numbers);
                });
        };
        
        var addMemberToTable = function(member) {
            //we will get fancy later with jq data tables.
            var tr = "<tr><td>" + member.MemberName + "</td><td> " + member.NumberCsv + "</td><td></td></tr>";
            if ($("#memberTable > tbody > tr").length === 0) {
                $("#memberTable > tbody").append(tr);
            } else {
                $("#memberTable > tbody > tr").after(tr);
            }
            
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
                randomizeNumbers($("#totalNumbersToRandomize").val(), $("#hiddenGridId").val());
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

