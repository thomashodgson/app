/// <binding ProjectOpened='watch' />
var gulp = require('gulp');
var source = require('vinyl-source-stream');
var browserify = require('browserify');
var watchify = require('watchify');
var reactify = require('reactify');
var gutil = require('gulp-util');
var notify = require('gulp-notify');
var rename = require("gulp-rename");
var sass = require('gulp-sass');
var autoprefixer = require('gulp-autoprefixer');
var concatCss = require('gulp-concat-css');
var bower = require('gulp-bower');
var cssglobbing = require('gulp-css-globbing');
var debug = require('gulp-debug');

// http://stackoverflow.com/questions/32490328/gulp-autoprefixer-throwing-referenceerror-promise-is-not-defined
require('es6-promise').polyfill();

var buildDestination = "../webserver/bin/" + (process.env.Configuration || "Debug") + "/webbuild";

function handleErrors() {
    var args = Array.prototype.slice.call(arguments);
    notify.onError({
        title: 'Compile Error',
        message: '<%= error.message %>'
    }).apply(this, args);
    this.emit('end'); // Keep gulp from hanging on this task
}

function buildJs(shouldWatch) {

    var props = {
        entries: ['src/main.jsx'],
        debug: true,
        transform: [reactify],
        cache: {},
        packageCache: {},
        fullPaths: true
    };

    var bundler = shouldWatch ? watchify(browserify(props)) : browserify(props);

    function rebundle() {
        var stream = bundler.bundle();
        return stream
          .on('error', handleErrors)
          .pipe(source('src/main.jsx'))
          .pipe(rename("main.js"))
          .pipe(gulp.dest(buildDestination));
    }

    bundler.on('update', function () {
        rebundle();
        gutil.log('Rebundle...');
    });

    return rebundle();
}

gulp.task('default', ['build']);

gulp.task('build', ['build:js', 'build:html','build:resources', 'build:sass']);

gulp.task('build:honeycomb-css', ['build:bower'], function () {
    return gulp.src(['bower_components/honeycomb/src/*/css/*/_*.scss', 'bower_components/honeycomb/src/*/vendor/**/*.scss'])
        .pipe(gulp.dest('bower_components/honeycomb/dist'));
});

gulp.task('build:honeycomb-glob', ['build:bower', 'build:honeycomb-css', 'build:honeycomb-fonts'], function () {
    return gulp.src('bower_components/honeycomb/src/*/css/_main.scss')
        .pipe(cssglobbing({
            extensions: ['.scss'],
            scssImportPath: {
                leading_underscore: false,
                filename_extension: false
            }
        }))
        .pipe(debug({ title: 'honeycomb-glob:' })) // HACK: this line is needed otherwise not all of the files get copied :@
        .pipe(gulp.dest('bower_components/honeycomb/dist'));
});

gulp.task('build:honeycomb-fonts', function () {
    return gulp.src('bower_components/honeycomb/src/type/vendor/**')
        .pipe(gulp.dest(buildDestination + '/fonts')); //Adjust build folder to taste
});

gulp.task('build:bower', function() {
    return bower({ cmd: 'update' });
});

gulp.task('build:js', function () {
    return buildJs(false);
});

gulp.task('build:html', function () {
    gulp.src('index.html')
        .pipe(gulp.dest(buildDestination));
});

gulp.task('build:sass', ['build:bower', 'build:honeycomb-css', 'build:honeycomb-glob', 'build:honeycomb-fonts'], function () {

    return gulp.src('src/style.scss')
        .pipe(sass().on('error', sass.logError))
        .pipe(autoprefixer({
            browsers: ['last 2 versions'],
            cascade: false
        }))
        .pipe(concatCss('style.css'))
        .pipe(gulp.dest(buildDestination));
});

gulp.task('build:resources', function() {
    gulp.src(['resources/**/*'])
        .pipe(gulp.dest(buildDestination + '/resources'));
});

gulp.task('watch', function () {
    gulp.watch('src/**/*.scss', ['build:sass']);
    gulp.watch('index.html', ['build:html']);
    gulp.watch('resources/**/*', ['build:resources']);
    return buildJs(true);
});
