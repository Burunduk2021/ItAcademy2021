/// <binding BeforeBuild='default' />
"use strict";

const { watch, series } = require('gulp');
const gulp = require('gulp');
const newer = require('gulp-newer');
const rename = require("gulp-rename");
const cleanCSS = require('gulp-clean-css');
const uglify = require('gulp-uglify');
const autoprefixer = require('gulp-autoprefixer');
const sass = require('gulp-sass');
sass.compiler = require('node-sass');

function compileSCSS(cb) {
    gulp
        .src('wwwroot/scss/*.scss')
        .pipe(newer('wwwroot/scss/*.scss'))
        .pipe(sass().on('error', sass.logError))
        .pipe(rename(function (path) {
            path.dirname += "/css/";
            path.basename += "";
            path.extname = ".css";
        }))
        .pipe(gulp.dest('./wwwroot/', { overwrite: true }));
    cb();
}

function postCSS(cb) {
    gulp
        .src('wwwroot/css/*.css')
        .pipe(newer('wwwroot/css/*.css'))
        .pipe(autoprefixer({
            cascade: false
        }))
        .pipe(rename(function (path) {
            path.dirname += "/css/";
            path.basename += "";
            path.extname = ".css";
        }))
        .pipe(gulp.dest('./wwwroot/', { overwrite: true }));
    cb();
}

function minifyCSS(cb) {
    gulp
        .src('wwwroot/css/*.css')
        .pipe(newer('wwwroot/css/*.css'))
        .pipe(cleanCSS())
        .pipe(rename(function (path) {
            path.dirname += "/css/css-min/";
            path.basename += ".min";
            path.extname = ".css";
        }))
        .pipe(gulp.dest('./wwwroot/', { overwrite: true }));
    cb();
}

function minifyJS(cb) {
    gulp
        .src('wwwroot/js/*.js')
        .pipe(newer('wwwroot/js/*.js'))
        .pipe(uglify())
        .pipe(rename(function (path) {
            path.dirname += "/js/js-min/";
            path.basename += ".min";
            path.extname = ".js";
        }))
        .pipe(gulp.dest('./wwwroot/'));
    cb();
}

exports.default = function () {
    watch('wwwroot/scss/*.scss', series(compileSCSS, postCSS, minifyCSS));
    watch('wwwroot/js/*.js', minifyJS);
};
