(function () {

});

var MemberGrid = {
    Init: function () {
        var usedNumbers = {};
        var members = {};
        $("#addMember").on("click",
            function () {
                var member = {
                    memberName: $("#memberName").val(),
                    numbers: $("#chosenNumbers").split(","),
                    owes: numbers.length * 500,
                    paid: false
                };

                if (members[member.memberName] !== null) {
                    
                    for (var number in member.numbers) {
                        if (usedNumbers[number] === true) {

                        } else {
                            
                        }
                    }
                }

                
            });
    }
};