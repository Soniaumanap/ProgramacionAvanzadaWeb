function enviarAprobacion(id) {
    $.post("/Gestiones/EnviarAprobacion", { id }, function (resp) {
        if (resp.ok) {
            Swal.fire("Éxito", resp.mensaje, "success").then(() => location.reload());
        } else {
            Swal.fire("Error", resp.mensaje, "error");
        }
    });
}

function aprobar(id) {
    $.post("/Gestiones/Aprobar", { id }, function (resp) {
        if (resp.ok) {
            Swal.fire("Éxito", resp.mensaje, "success").then(() => location.reload());
        } else {
            Swal.fire("Error", resp.mensaje, "error");
        }
    });
}

function devolver(id) {
    Swal.fire({
        title: "Comentario de devolución",
        input: "textarea",
        showCancelButton: true,
        confirmButtonText: "Devolver",
        preConfirm: v => {
            if (!v) Swal.showValidationMessage("Debe ingresar un comentario");
            return v;
        }
    }).then(res => {
        if (res.isConfirmed) {
            $.post("/Gestiones/Devolver", { id, comentario: res.value }, function (resp) {
                if (resp.ok) {
                    Swal.fire("Éxito", resp.mensaje, "success").then(() => location.reload());
                } else {
                    Swal.fire("Error", resp.mensaje, "error");
                }
            });
        }
    });
}

function eliminarGestion(id) {
    Swal.fire({
        title: "Eliminar gestión",
        text: "¿Está seguro que desea eliminar esta gestión?",
        icon: "warning",
        showCancelButton: true,
        confirmButtonText: "Eliminar"
    }).then(res => {
        if (res.isConfirmed) {
            $.post("/Gestiones/Eliminar", { id }, function (resp) {
                if (resp.ok) {
                    Swal.fire("Eliminado", resp.mensaje, "success").then(() => location.reload());
                } else {
                    Swal.fire("Error", resp.mensaje, "error");
                }
            });
        }
    });
}
