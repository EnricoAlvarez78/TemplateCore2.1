"use strict";
function dataTableInit(path, params) {
	var obj = {
		processing: true,
		serverSide: true,
		filter: true,
		orderMulti: true,
		pagingType: "full_numbers",
		language: languagePtBr(),
		ajax: {
			url: path,
			type: "POST",
			datatype: "json",
			data: {
				model: params
			},
			dataSrc: function (response) {
				//console.log(response.data);
				return response.data;
			},
			error: function (xhr, error, thrown) {
				errorAjaxHandler(xhr, status, error);
			}
		}
	};

    return obj;
}