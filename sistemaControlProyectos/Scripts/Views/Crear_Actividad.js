﻿var permisoEdi = false;
console.log($("#txtIDCargo").val());
//Obtener Permisos 
jQuery.ajax({
    url: "/Permisos/Listar" + "?IDCargo=" + $("#txtIDCargo").val(),
    type: "GET",
    dataType: "json",
    contentType: "application/json; charset=utf-8",
    success: function (data) {
        $("#cboProyecto").html("");

        if (data.data != null) {
            $.each(data.data, function (i, item) {
                console.log(item);
                permisoEdi = item;
            })
        }
    },
    error: function (error) {
        console.log(error)
    },
    beforeSend: function () {
    },
});

//Cambiar Fechas
function Cambiarfecha(fechaEntra) {
    var fechaString = fechaEntra.substr(6);
    var fechaActual = new Date(parseInt(fechaString));
    var mes = fechaActual.getMonth() + 1;
    var dia = fechaActual.getDate();
    var anio = fechaActual.getFullYear();
    if (dia < 10 && mes < 10) {
        var fecha = anio + "-0" + mes + "-0" + dia;
    } else {
        if (dia < 10)
            var fecha = anio + "-" + mes + "-0" + dia;
        else {
            if (mes < 10)
                var fecha = anio + "-0" + mes + "-" + dia;
            else
                var fecha = anio + "-" + mes + "-" + dia;
        }
    }
    return fecha;
}

function Cerrar() {
    $('#FormModal').modal('hide');
}

var tablaActividad;
$(document).ready(function () {
    
    tablaActividad = $('#tblActividad').DataTable({

        "ajax": {
            "url": "/Actividades/Listar",
            "type": "GET",
            "datatype": "json"
        },

        "columns": [
            {
                "data": "IDActividad", "render": function (data) {
                    return "<button class='btn btn-primary btn-sm' type='button' onclick='abrirModal(" + data + ")'><i class='fas fa-pencil-alt'></i></button>" +
                        "<button class='btn btn-danger btn-sm ml-2' type='button' onclick='Eliminar(" + data + ")'><i class='fa fa-trash'></i></button>"
                },
                "orderable": false,
                "searchable": false,
                "width": "150px"
            },
            { "data": "titActividad"},
            { "data": "fechaInicio", "render": function (data) {return Cambiarfecha(data);}},
            { "data": "fechaFin", "render": function (data) {return Cambiarfecha(data);}},
            { "data": "Descripcion" },
            {
                "data": "estado", "render": function (data) {
                    if (data) {
                        return '<span class="badge bg-success">Activo</span>';
                    } else {
                        return '<span class="badge bg-danger">No Activo</span>';
                    }
                }
            },
            { "data" : "proceso"},
        ],
        dom: 'Blfrtip',
        lengthMenu: [
            entries = 100, 50, 10,
        ],
        buttons: [
            {
                text: 'Agregar Nuevo',
                attr: { class: 'btn btn-success btn-sm' },
                action: function (e, dt, node, config) {
                    abrirModal(0)
                }
            }
        ],
        responsive: true
    });
})

function abrirModal($IDActividad) {
   
    $("#txtIdActividad").val($IDActividad);
    if ($IDActividad != 0) {

        jQuery.ajax({
            url: "/Actividades/Obtener" + "?IDActividad=" + $IDActividad,
            type: "GET",
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data) {

                if (data != null) {
                    $("#txtTitulo").val(data.titActividad);
                    $("#txtFechaIni").val(Cambiarfecha(data.fechaInicio));
                    $("#txtFechaFin").val(Cambiarfecha(data.fechaFin));
                    $("#txtDescripcion").val(data.Descripcion);
                    $("#cboEstado").val(data.estado == true ? 1 : 0);
                    $("#cboProceso").val(data.proceso);
                }
            }
        });
    } else {
        $("#txtTitulo").val("");
        $("#txtFechaIni").val("");
        $("#txtFechaFin").val("");
        $("#txtDescripcion").val("");
        $("#cboEstado").val(1);
        $("#cboProceso").val("Nuevo");
    }

    $('#FormModal').modal('show');
}

//Guardar Actividad
function Guardar() {
    
    var $request = {
        objeto: {
            IDActividad: parseInt($("#txtIdActividad").val()),
            titActividad: $("#txtTitulo").val(),
            fechaInicio: $("#txtFechaIni").val(),
            fechaFin: $("#txtFechaFin").val(),
            Descripcion: $("#txtDescripcion").val(),
            estado: $("#cboEstado").val() == "1" ? true : false,
            creador: $("#txtNombreUsuario").val(),
            proceso: ($("#cboProceso").val()),
        }
    }

    if ($request.objeto.titActividad != "") {
        if ($request.objeto.fechaInicio) {
            if ($request.objeto.fechaFin) {
                if ($request.objeto.fechaFin > $request.objeto.fechaInicio) {
                    jQuery.ajax({
                        url: "/Actividades/Guardar",
                        type: "POST",
                        data: JSON.stringify($request),
                        dataType: "json",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            console.log(data.resultado);
                            if (data.resultado) {
                                tablaActividad.ajax.reload();
                                $('#FormModal').modal('hide');
                            } else {
                                alert("Mensaje No se pudo guardar los cambios", "warning");
                            }
                        },
                        error: function (error) {
                            console.log(error)
                        },
                        beforeSend: function () {

                        },
                    });
                } else {
                    alert("La fecha de FIN debe ser mayor a la de INICIO", "warning");
                }
            } else {
                alert("Ingrese Fecha de Fin", "warning");
            }
        } else {
            alert("Ingrese Fecha de Inicio", "warning");
        }
    } else {
        alert("Ingrese Titulo", "warning");
    }

}

function Eliminar($IDActividad) {
    if (confirm("Estas seguro de Eliminar el Registro?")) {
        jQuery.ajax({
            url: "/Actividades/Eliminar" + "?IDActividad=" + $IDActividad,
            type: "GET",
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                if (data.resultado) {
                    tablaActividad.ajax.reload();
                }
            }
        });
    }
}
