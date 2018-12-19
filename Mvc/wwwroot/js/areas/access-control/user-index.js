"use strict";
$(document).ready(function () {
	dataTable();
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