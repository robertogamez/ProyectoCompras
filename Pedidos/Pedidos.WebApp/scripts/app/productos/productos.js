/// <reference path="../../jquery-2.2.4.js" />
/// <reference path="../../bootstrap.js" />

$(function () {

    var procesando = false;

    var table = $('#tblProductos').DataTable({
        language: {
            "decimal": "",
            "emptyTable": "No hay información",
            "info": "Mostrando _START_ a _END_ de _TOTAL_ Entradas",
            "infoEmpty": "Mostrando 0 to 0 of 0 Entradas",
            "infoFiltered": "(Filtrado de _MAX_ total entradas)",
            "infoPostFix": "",
            "thousands": ",",
            "lengthMenu": "Mostrar _MENU_ Entradas",
            "loadingRecords": "Cargando...",
            "processing": "Procesando...",
            "search": "Buscar:",
            "zeroRecords": "Sin resultados encontrados",
            "paginate": {
                "first": "Primero",
                "last": "Ultimo",
                "next": "Siguiente",
                "previous": "Anterior"
            }
        },
        "processing": true,
        "serverSide": true,
        "info": true,
        "stateSave": true,
        "searching": false,
        "lengthMenu": [[10, 20, 50, -1], [10, 20, 50, "All"]],
        "ajax": {
            "url": "/api/productos",
            "type": "get"
        },
        "columns": [
                { "data": "Id", searchable: true },
                { "data": "Name" },
                { "data": "PrecioCompra", searchable: true },
                {
                    'render': function (el, type, data) {

                        console.log(data);

                        var $check = $('<input />')
                            .attr('type', 'checkbox')
                            .attr('name', 'id[]')
                            .attr('checked', data.Login);

                        return $check.get(0).outerHTML;
                    }
                },
        ],
        "order": [[0, "asc"]]
    });

    /*
     * Mostrar el formulario modal para crear o editar un producto
     */
    $('#btnCrearProducto').on('click', function (event) {

        $('#lblGuardarCambios').text('Crear producto');

        $('#mdlModalProducto').modal('show');

    });

    /*
     * Guardar la información de un nuevo producto
     */
    $('#btnGuardar').on('click', function (event) {

        var $btnGuardar = $(event.target),
            $lblProcesando = $('#lblProcesando'),
            $lblMensaje = $('#lblMensaje'),
            $alerta = $('#alertCorrecto');

        var nuevoProducto = ObtenerValoresFormulario();

        // Validar que operación se debe realizar

        $btnGuardar.addClass('hide');
        $lblProcesando.removeClass('hide');

        $.ajax('/api/productos/', {
            'type': 'post',
            'contentType': 'application/json',
            'dataType': 'json',
            'data': JSON.stringify(nuevoProducto)
        }).then(function (respuesta) {

            $lblMensaje.text('Producto creado correctamente');
            $alerta.removeClass('alert-danger').removeClass('hide').addClass('alert-success');

        }, function (error) {

            var mensaje = 'Ocurrio un error. Contacte al administrador';

            if (error.status === 500) {
                mensaje = 'No se pudo crear el producto';
            }

            $lblMensaje.text(mensaje);
            $alerta.removeClass('alert-success').removeClass('hide').addClass('alert-danger').show();

        }).then(function () {
            $btnGuardar.removeClass('hide');
            $lblProcesando.addClass('hide');

            LimpiarFormulario();

            $('#mdlModalProducto').modal('hide');
        });

    });

    function ObtenerValoresFormulario() {
        return {
            Name: $('#txtNombreProducto').val(),
            PrecioCompra: $('#txtPrecioCompra').val(),
            PrecioAlPorMenor: $('#txtPrecioAlPorMenor').val()
        };
    }

    function LimpiarFormulario() {
        $('#txtNombreProducto').val('');
        $('#txtPrecioCompra').val('');
        $('#txtPrecioAlPorMenor').val('');
    }

});