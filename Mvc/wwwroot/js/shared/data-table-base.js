"use strict";
function dataTable(tableId, serverPath, params, columns) {
	removeSharp(tableId);	
	$('#' + tableId).DataTable().destroy();
	$('#' + tableId).DataTable({
		processing: true,
		serverSide: true,
		filter: true,
		orderMulti: true,
		pagingType: "full_numbers",
		language: languagePtBr(),
		ajax: {
			url: serverPath,
			type: "POST",
			datatype: "json",
			data: { 
				model: params
			},
			dataSrc: function (response) {
				return response.data;
			},
			error: function (xhr, error, thrown) {
				//errorAjaxHandler(xhr, status, error);
			}
		},
		columns: columns
	});
}

function removeSharp(tableId) {
	if (tableId.search("#") > -1) {
		tableId = tableId.replace(/#/g, '');
	}
	return tableId;
}