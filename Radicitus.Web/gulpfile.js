/// <binding BeforeBuild='css:copy, scripts:copy' AfterBuild='css:copy, scripts:copy' />
"use strict";

var gulp   = require("gulp"),
    rimraf = require("rimraf"),
    concat = require("gulp-concat"),
    cssmin = require("gulp-cssmin"),
    uglify = require("gulp-uglify"),
    copy   = require("gulp-copy");
    

var paths = {
    webroot: "./wwwroot/",
    lib: "./wwwroot/lib/",
    nodeRoot: "./node_modules/",
    fontRoot: "./wwwroot/fonts/"
};

paths.bootstrap = paths.nodeRoot + "bootstrap/dist/js/bootstrap.js";
paths.jquery = paths.nodeRoot + "jquery/dist/jquery.js";
paths.bootbox = paths.nodeRoot + "bootbox/bootbox.js";
paths.jqvalidation = paths.nodeRoot + "jquery-validation/dist/jquery.validate.js";
paths.jqvalidationunob = paths.nodeRoot + "jquery-validation-unobtrusive/jquery.validate.unobtrusive.js";
paths.unobtrusiveAjax = paths.nodeRoot + "jquery-ajax-unobtrusive/jquery.unobtrusive-ajax.js";
paths.bootstrapCssPath = paths.nodeRoot + "bootstrap/dist/css/bootstrap.css";
paths.popper = paths.nodeRoot + "popper.js/dist/umd/popper.js";
paths.fontawesome = paths.nodeRoot + "font-awesome/css/font-awesome.css";
gulp.task("libs:clean",
    function(cb) {
        rimraf(paths.lib, cb);
});


gulp.task("scripts:copy",
    function() {
        gulp.src([paths.bootstrap,
            paths.bootbox,
            paths.jquery,
            paths.jqvalidation,
            paths.jqvalidationunob,
            paths.unobtrusiveAjax,
            paths.popper])
            .pipe(gulp.dest(paths.lib));
    });

gulp.task("css:copy",
    function() {
        gulp.src([paths.bootstrapCssPath, paths.fontawesome])
            .pipe(gulp.dest(paths.lib));
    });