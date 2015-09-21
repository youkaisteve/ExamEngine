var gulp = require("gulp");
var less = require("gulp-less");
var livereload = require("gulp-livereload");
var autoprefixer = require("gulp-autoprefixer");
var minifyCss = require('gulp-minify-css');
var uglify = require('gulp-uglify');
var minifyHTML = require('gulp-htmlmin');
var rimraf = require('gulp-rimraf');
var imagemin = require('gulp-imagemin');
var rename = require("gulp-rename");
var zip = require('gulp-zip');
var merge = require('merge-stream');
var dateFmt = require("dateformat");
var replace = require('gulp-replace');
var md5 = require('md5');

gulp.task("less", function () {
    return gulp.src("less/**/*.less")
        .pipe(less())
        .pipe(autoprefixer())
        .pipe(gulp.dest("css"))
        .pipe(livereload());
});

gulp.task("watcher", function () {
    livereload.listen();

    //less watcher
    gulp.watch("less/**/*.less", ["less"]);

    //js watcher
    gulp.watch([
        "js/controllers/**/*.js"
        , "js/*.js"
    ], function (event) {
        gulp.src(event.path).pipe(livereload());
    });

    //html watcher
    gulp.watch([
        "views/**/*.html"
        , "partials/**/*.html"
        , "forms/**/*.html"
    ], function (event) {
        gulp.src(event.path).pipe(livereload());
    });

});

gulp.task("clean-dist", function () {
    return gulp.src("dist").pipe(rimraf());
});

gulp.task("release", ["clean-dist"], function () {

    var htmlOps = {
        collapseWhitespace: true
    };

    return merge(
        //compress css & dest
        gulp.src("less/**/*.less").pipe(less()).pipe(autoprefixer()).pipe(minifyCss()).pipe(gulp.dest("dist/css"))
        //compress & dest html
        , gulp.src("js/directive/**/*.html").pipe(minifyHTML(htmlOps)).pipe(gulp.dest("dist/js/directive"))
        , gulp.src("views/**/*.html").pipe(minifyHTML(htmlOps)).pipe(gulp.dest("dist/views"))
        , gulp.src("partials/**/*.html").pipe(minifyHTML(htmlOps)).pipe(gulp.dest("dist/partials"))
        , gulp.src("forms/**/*.html").pipe(gulp.dest("dist/forms"))
        , gulp.src("index.html").pipe(minifyHTML(htmlOps)).pipe(gulp.dest("dist"))
        //compress & dest image
        , gulp.src("img/**/*").pipe(imagemin({progressive: true})).pipe(gulp.dest("dist/img"))
        //compress & dest js
        , gulp.src("js/controllers/*.js").pipe(uglify()).pipe(gulp.dest("dist/js/controllers"))
        , gulp.src(["js/*.js", "!js/main.js", "!js/main.release.js", "!js/app.config.js", "!js/app.config.release.js"]).pipe(uglify()).pipe(gulp.dest("dist/js"))
        , gulp.src(["js/app.config.release.js"]).pipe(rename("app.config.js")).pipe(uglify()).pipe(gulp.dest("dist/js"))
        , gulp.src(["js/main.release.js"]).pipe(rename("main.js")).pipe(replace("#VERSION#",md5(Date.now()))).pipe(uglify()).pipe(gulp.dest("dist/js"))
        , gulp.src("js/directive/**/*.js").pipe(uglify()).pipe(gulp.dest("dist/js/directive"))
        , gulp.src("js/service/**/*.js").pipe(uglify()).pipe(gulp.dest("dist/js/service"))
        //dest lib
        , gulp.src(["js/lib/**/*"]).pipe(gulp.dest("dist/js/lib"))
        //other
        , gulp.src("favicon.ico").pipe(gulp.dest("dist"))
        //, gulp.src("dist/css/*").pipe(zip("app.zip")).pipe(gulp.dest("dist"))
    ).on("end", function () {
            var suffix = "-" + dateFmt(new Date(), "yyyymmddHMs");
            gulp.src("dist/**/*", {base: process.cwd()}).pipe(zip("app.zip")).pipe(rename({
                suffix: suffix
            })).pipe(gulp.dest("dist"));
        });

});

gulp.task("default", ["less", "watcher"]);