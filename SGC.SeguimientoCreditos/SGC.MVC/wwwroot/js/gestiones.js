function abrirModalCrear() {
    $("#modalCreate").modal("show");
}

$("#formCreate").submit(function (e) {
    e.preventDefault();

    $.ajax({
        url: "/Usuarios/Create",
        type: "POST",
        data: $(this).serialize(),
        success: function (resp) {
            if (resp.ok) {
                Swal.fire("Éxito", resp.mensaje, "success")
                    .then(() => location.reload());
            } else {
                Swal.fire("Error", resp.mensaje, "error");
            }
        }
    });
});


function editar(id) {
    $.get("/Usuarios/Get/" + id, function (data) {
        if (data == null) return;

        $("#formEdit [name='Id']").val(data.id);
        $("#formEdit [name='Identificacion']").val(data.identificacion);
        $("#formEdit [name='Nombre']").val(data.nombre);
        $("#formEdit [name='Email']").val(data.email);
        $("#formEdit [name='Password']").val(data.password);
        $("#formEdit [name='Rol']").val(data.rol);
        $("#formEdit [name='Activo']").val(data.activo.toString());

        $("#modalEdit").modal("show");
    });
}

$("#formEdit").submit(function (e) {
    e.preventDefault();

    $.ajax({
        url: "/Usuarios/Edit",
        type: "POST",
        data: $(this).serialize(),
        success: function (resp) {
            if (resp.ok) {
                Swal.fire("Éxito", resp.mensaje, "success")
                    .then(() => location.reload());
            } else {
                Swal.fire("Error", resp.mensaje, "error");
            }
        }
    });
});


function eliminar(id) {
    Swal.fire({
        title: "¿Eliminar usuario?",
        text: "Esta acción es irreversible",
        icon: "warning",
        showCancelButton: true,
        confirmButtonText: "Sí, eliminar"
    }).then(res => {
        if (res.isConfirmed) {
            $.post("/Usuarios/Delete", { id }, function (resp) {
                if (resp.ok) {
                    Swal.fire("Eliminado", resp.mensaje, "success")
                        .then(() => location.reload());
                } else {
                    Swal.fire("Error", resp.mensaje, "error");
                }
            });
        }
    });
}
