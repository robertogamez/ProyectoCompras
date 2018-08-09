/// <reference path="../../jquery-2.2.4.js" />
/// <reference path="../../bootstrap.js" />
/// <reference path="productos-validacion.js" />

var ShoppingValidations = window.Shopping.Validations.Shopping;

$(function () {

    $('#frmGuardarCambios').validate(ShoppingValidations);

    var tabla = $('#tblProductos').DataTable({
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
            "url": "/api/products",
            "type": "get"
        },
        "aoColumnDefs": [{ "bVisible": false, "aTargets": [0] }],
        "columns": [
                { "data": "Id", searchable: true, },
                { "data": "Name" },
                { "data": "PrecioCompra", searchable: true },
                { "data": "PrecioAlPorMenor", searchable: false },
                {
                    'render': function (el, type, data) {

                        console.log(data);

                        var $botonEditar = $('<button />')
                            .text('Editar')
                            .addClass('btn btn-default btnEditar');

                        return $botonEditar.get(0).outerHTML;
                    }
                },
                {
                    'render': function (el, type, data) {

                        console.log(data);

                        var $botonEliminar = $('<button />')
                            .text('Eliminar')
                            .addClass('btn btn-danger btnEliminar');

                        return $botonEliminar.get(0).outerHTML;
                    }
                }
        ],
        "order": [[0, "asc"]]
    });

    /*
     * Mostrar el formulario modal para crear o editar un producto
     */
    $('#btnCrearProducto').on('click', function (event) {

        var $alertaFormulario = $('#alertFormulario');
        $alertaFormulario.addClass('hide');

        $('#btnGuardar').data('operacion', 1);
        $('#lblGuardarCambios').text('Crear producto');
        $('#mdlModalProducto').modal('show');

    });

    /*
     * Guardar la información de un nuevo producto
     */
    $('#btnGuardar').on('click', function (event) {

        event.preventDefault();

        if ($('#frmGuardarCambios').valid()) {
            var $btnGuardar = $(this),
            $lblGuardarCambios = $('#lblGuardarCambios'),
            $lblProcesando = $('#lblProcesando'),
            $lblMensaje = $('#lblMensaje'),
            $alerta = $('#alertCorrecto');

            var producto = ObtenerValoresFormulario();
            var operacion = $btnGuardar.attr('operacion');

            var url = '/api/products/';
            var verbo = 'post';
            if (operacion == 2) {
                url += producto.Id;
                verbo = 'put';
            }

            // Validar que operación se debe realizar
            $btnGuardar.prop('disabled', true);
            $lblGuardarCambios.addClass('hide');
            $lblProcesando.removeClass('hide');

            $.ajax(url, {
                'type': verbo,
                'contentType': 'application/json',
                'dataType': 'json',
                'data': JSON.stringify(producto)
            }).then(function (respuesta) {

                $lblMensaje.text('Producto guardado correctamente');
                $alerta.removeClass('alert-danger').removeClass('hide').addClass('alert-success');

                // Recargar la información de la tabla
                tabla.ajax.reload(null, false);

            }, function (error) {

                var mensaje = 'Ocurrio un error. Contacte al administrador';

                if (error.status === 500) {
                    mensaje = 'No se pudo crear el producto';
                }

                $lblMensaje.text(mensaje);
                $alerta.removeClass('alert-success').removeClass('hide').addClass('alert-danger');

                $('#mdlModalProducto').modal('hide');

            }).then(function () {

                $btnGuardar.prop('disabled', false);
                $lblGuardarCambios.removeClass('hide');
                $lblProcesando.addClass('hide');

                $('#mdlModalProducto').modal('hide');

            });
        }

    });

    /*
     * Opciones de editar
     */
    $('#tblProductos tbody').on('click', '.btnEditar', function () {
        var $dataRow = $(this).closest('tr');

        var pruductoEditar = tabla.row($dataRow).data();

        $('#hdId').val(pruductoEditar.Id);

        $('#btnGuardar').attr('operacion', 2);

        $('#lblGuardarCambios').text('Editar producto');

        $('#mdlModalProducto').modal('show');
    });

    // Cargar los valores del formulario cuando este se complete
    $('#mdlModalProducto').on('shown.bs.modal', function (event) {

        var productoId = $('#hdId').val();
        var $alertaFormulario = $('#alertFormulario');

        if (productoId > 0) {
            $alertaFormulario.removeClass('hide').removeClass('alert-danger').addClass('alert-info');
            $alertaFormulario.text('Cargando información...');

            $.ajax('/api/products/' + productoId, {
                'type': 'get',
                'contentType': 'application/json',
                'dataType': 'json'
            }).then(function (respuesta) {

                CargarValoresFormulario(respuesta);
                $alertaFormulario.addClass('hide');

            }, function (error) {

                var mensaje = 'Ocurrio un error. Contacte al administrador';

                if (error.status === 500) {
                    mensaje = 'No se pudo crear el producto';
                }

                $lblMensaje.text(mensaje);
                $alerta.removeClass('alert-success').removeClass('hide').addClass('alert-danger').show();
                $('#mdlModalProducto').modal('hide');
            });
        }

    });

    $('#mdlModalProducto').on('hidden.bs.modal', function (event) {
        LimpiarFormulario();
    });

    function ObtenerValoresFormulario() {
        return {
            Name: $('#txtNombreProducto').val(),
            PrecioCompra: $('#txtPrecioCompra').val(),
            PrecioAlPorMenor: $('#txtPrecioAlPorMenor').val(),
            Id: $('#hdId').val()
        };
    }

    function CargarValoresFormulario(producto) {
        $('#txtNombreProducto').val(producto.Name);
        $('#txtPrecioCompra').val(producto.PrecioCompra);
        $('#txtPrecioAlPorMenor').val(producto.PrecioAlPorMenor);
        $('#hdId').val(producto.Id);

    }

    function LimpiarFormulario() {
        $('#txtNombreProducto').val('');
        $('#txtPrecioCompra').val('');
        $('#txtPrecioAlPorMenor').val('');
        $('#hdId').val('0');
    }

    $('#btnCancel').on('click', function (event) {
        event.preventDefault();

        $('#mdlModalProducto').modal('hide');
    });

    $('#tblProductos tbody').on('click', '.btnEliminar', function () {
        var $dataRow = $(this).closest('tr');

        var pruductoEliminar = tabla.row($dataRow).data();

        // Mensaje de confirmación
        swal({
            title: "¿Desea eliminar el producto?",
            text: "",
            type: 'warning',
            showCancelButton: true,
            showConfirmButton: true,
            cancelButtonText: 'Cancelar',
            confirmButtonText: 'Aceptar'
        }, function (respuesta) {
            if (respuesta) {
                $.ajax('/api/products/' + pruductoEliminar.Id, {
                    'type': 'delete',
                    'contentType': 'application/json',
                    'dataType': 'json'
                }).then(function (respuesta) {

                    swal({
                        title: "Producto eliminado correctamente",
                        type: 'success'
                    });

                    // Recargar la información de la tabla
                    tabla.ajax.reload(null, false);

                }, function (error) {

                    swal({
                        title: "No se pudo eliminar el producto",
                        type: 'danger'
                    });

                });

            }
        });

    });
});