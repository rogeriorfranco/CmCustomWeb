﻿$(document).ready(function () {
    $('#txtDtValidadeReserva').inputmask('99/99/9999');
});

$(document).ready(function () {
    $("#txtOnt").on('keyup keydown blur', function (event) {
        $(this).val($(this).val().replace(/[^0-9]/g, ''));
    });
});


$(document).ready(function () {
    $("#txtBandaMB").on('keyup keydown blur', function (event) {
        $(this).val($(this).val().replace(/[^0-9]/g, ''));
    });
});

$(document).ready(function () {
    $("#txtBandaUpLink").on('keyup keydown blur', function (event) {
        $(this).val($(this).val().replace(/[^0-9]/g, ''));
    });
});

$(document).ready(function () {
    $("#txtVlanInner").on('keyup keydown blur', function (event) {
        $(this).val($(this).val().replace(/[^0-9]/g, ''));
    });
});

$(document).ready(function () {
    $("#txtVlan").on('keyup keydown blur', function (event) {
        $(this).val($(this).val().replace(/[^0-9]/g, ''));
    });
});

$(document).ready(function () {
    $("#txtSerial").on('keyup keydown blur', function (event) {
        $(this).val($(this).val().replace(/[^a-zA-Z0-9 ]/g, ''));
    });
});

$(document).ready(function () {
    $("#txtCircuito").on('keypress blur', function (event) {
        $(this).val($(this).val().replace(/[^0-9]/g, ''));
    });
});

$(document).ready(function () {
    $('#ddlStatus').change(function () {
        if (this.value == "RESERVADO") {            
            $("#txtDtValidadeReserva").prop('disabled', false);
        } else {
            $("#txtDtValidadeReserva").prop('disabled', true);
            $('#txtDtValidadeReserva').val('');
        }
    });
});