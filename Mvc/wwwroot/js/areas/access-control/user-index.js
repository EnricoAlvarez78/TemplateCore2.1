"use strict";
$(document).ready(function () {
	dataTable('tableUser', 'User/Grid', createFilter(), mountColumns());
});

function createFilter() {
	return {};
}

function mountColumns() {
	return [
		{ data: "name", name: "name", orderable: false },
		{ data: "email", name: "email", orderable: false },
		{
			data: "status", name: "status", orderable: false, className: "text-center",
			"render": function (data, type, row, meta) {
				return row.status.toString() === "true" ? '<input type="checkbox" disabled checked>' : '<input type="checkbox" disabled>';
			}
		},
		{ data: "creationDate", name: "creationDate", orderable: false },
		{
			searchable: false, orderable: false, width: "10%", className: "text-center",
			render: function (data, type, row, meta) {
				return '<a class="btn btn-warning"><span class="fa fa-edit"></span></a>&nbsp;' +
					'<a class="btn btn-danger"><span class="fa fa-trash"></span></a>';
			}
		}
	];
}