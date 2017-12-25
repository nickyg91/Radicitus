// override jquery validate plugin defaults
$.validator.setDefaults({
    highlight: function (element) {
        $(element).closest(".form-group").addClass("is-invalid");
        $(element).closest(".form-control").addClass("is-invalid");
    },
    unhighlight: function (element) {
        $(element).closest(".form-group").removeClass("is-invalid").addClass("is-valid");
        $(element).closest(".form-control").removeClass("is-invalid").addClass("is-valid");
    }
});