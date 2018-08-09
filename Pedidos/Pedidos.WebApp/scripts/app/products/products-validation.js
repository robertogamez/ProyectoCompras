/// <reference path="../../jquery-2.2.4.js" />
/// <reference path="../../jquery.validate.js" />


window.Shopping = window.Shopping || {};
var ShoppingApp = window.Shopping;

ShoppingApp.Validations = ShoppingApp.Validations || {};

ShoppingApp.Validations.Shopping = (function ($) {

    /*
     * Rules 
     */
    var _shopping = {
        rules: {
            // Personal Info
            txtNombreProducto: {
                required: true,
                maxlength: 200,
            },
            txtPrecioCompra: {
                required: true,
                regx: /^\d{1,15}(\.\d{1,2})?$/
            },
            txtPrecioAlPorMenor: {
                required: true,
                regx: /^\d{1,15}(\.\d{1,2})?$/
            }
        },
        messages: {
            // Personal Info
            txtNombreProducto: {
                required: 'Ingrese un nombre de producto'
            },
            txtPrecioCompra: {
                required: 'Ingrese un monto de compra',
                regx: 'El formato del monto de compra no es correcto'
            },
            txtPrecioAlPorMenor: {
                required: 'Ingrese un monto al por menor',
                regx: 'El formato del monto al por menor no es correcto'
            }
        }
    };


    $.validator.addMethod("regx", function (value, element, param) {

        return this.optional(element) || param.test(value);

    }, 'El formato del RFC es incorrecto');

    return _shopping;

})(jQuery);