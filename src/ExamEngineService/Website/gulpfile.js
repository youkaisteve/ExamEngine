var gulp = require("gulp");
var less = require("gulp-less");
var livereload = require("gulp-livereload");
var autoprefixer = require("gulp-autoprefixer");


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


gulp.task("default", ["less", "watcher"]);