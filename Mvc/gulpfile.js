'use strict';

var gulp = require('gulp');
var rimraf = require('rimraf');
var merge = require('merge-stream');

// Dependency Dirs
var deps = {
	"jquery": {
		"dist/jquery.min.js": ""
	},
	"bootstrap": {
		"dist/css/bootstrap.min.css": "css/",
		"dist/css/bootstrap.min.css.map": "css/",
		"dist/js/bootstrap.min.js": "js/"
	},
	"font-awesome": {
		"fonts/*": "fonts/",
		"css/*": "css/"
	},
	"admin-lte": {
		"dist/css/AdminLTE.min.css": "css/",
		"dist/css/skins/skin-blue.min.css": "css/skins/",
		"dist/js/adminlte.min.js": "js/",
		"dist/img/**/*": "img/"
	},
	"ionicons": {
		"dist/css/ionicons.min.css": "css/"
	},
	"datatables.net": {
		"js/jquery.dataTables.min.js": "js/"
	},
	"datatables.net-bs": {
		"js/dataTables.bootstrap.min.js": "js/",
		"css/dataTables.bootstrap.min.css": "css/"		
	}
};

gulp.task("clean", function (cb) {
	return rimraf("wwwroot/lib/", cb);
});

gulp.task("copy", function () {

	var streams = [];

	for (var prop in deps) {
		console.log("Prepping Scripts for: " + prop);
		for (var itemProp in deps[prop]) {
			streams.push(gulp.src("node_modules/" + prop + "/" + itemProp)
				.pipe(gulp.dest("wwwroot/lib/" + prop + "/" + deps[prop][itemProp])));
		}
	}

	return merge(streams);

});

gulp.task("default", gulp.series('clean', 'copy'));
