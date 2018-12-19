"use strict";
$(document).ready(function () {
	//dataTable();

	$('#tableUser').DataTable({
		processing: true,
		serverSide: true,
		filter: true,
		orderMulti: true,
		pagingType: "full_numbers",
		language: languagePtBr(),
		ajax: {
			url: "User/Grid",
			type: "POST",
			datatype: "json",
			//data: {
			//	model: params
			//},
			dataSrc: function (response) {
				//console.log(response.data);
				return response.data;
			},
			error: function (xhr, error, thrown) {
				errorAjaxHandler(xhr, status, error);
			}
		},
		columns: [
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
		]
	});
});

function dataTable() {
	try {

		var dataTableObj = dataTableInit(pathRoot.concat('User/Grid'));

		dataTableObj.columns = mountColumns();

		$('#tableUser').DataTable().destroy();

		return $("#tableUser").DataTable(dataTableObj);

	} catch (e) {
		exceptionHandler(e);
	}
}

function createFilter() {
	//var empresaId = $("#buscaempresa option:selected").val();
	//var sessionEmpresa = sessionStorage.getItem("empresa");
	//var eventoId = $("#filtro_eventos option:selected").val();
	//var tipoRegistro = $("#tipo_registro option:selected").val();
	//var dataGeracaoDe = $("#geracao_datade").val();
	//var dataGeracaoAte = $("#geracao_dataate").val();
	//var dataTransmissaoDe = $("#transmissao_datade").val();
	//var dataTransmissaoAte = $("#transmissao_dataate").val();

	//if (empresaId === undefined || empresaId === '' || parseInt(empresaId, 10) < 0) {
	//	if (sessionEmpresa === undefined || sessionEmpresa === '' || parseInt(sessionEmpresa, 10) < 0) {
	//		empresaId = 0;
	//	}
	//	else {
	//		empresaId = sessionEmpresa;
	//	}
	//}

	//eventoId = eventoId === undefined || eventoId === null ? 0 : parseInt(eventoId, 10);
	//tipoRegistro = tipoRegistro === undefined || tipoRegistro === null || tipoRegistro === '' ? 0 : parseInt(tipoRegistro, 10);

	//return {
	//	empresaId: empresaId,
	//	eventoId: eventoId,
	//	tipoRegistro: tipoRegistro,
	//	dataGeracaoDe: dataGeracaoDe,
	//	dataGeracaoAte: dataGeracaoAte,
	//	dataTransmissaoDe: dataTransmissaoDe,
	//	dataTransmissaoAte: dataTransmissaoAte
	//};

	return null;
}

function mountColumns() {
	return [
		{ data: "name", name: "name", orderable: false },
		{ data: "email", name: "email", orderable: false },
		{
			data: null, orderable: false,
			render: function (data, type, row, meta) {
				return '<button class="btn btn-success btn-sm"><i class="fa fa-download"></i>AA</button>';
			}
		}
	];
}