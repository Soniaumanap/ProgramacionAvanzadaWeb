function abrirModalCrearCliente() {
    $("#formCreateCliente")[0].reset();
    $("#modalCreateCliente").modal("show");
}

$("#formCreateCliente").submit(function (e) {
    e.preventDefault();

    $.ajax({
        url: "/Clientes/Create",
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

function editarCliente(id) {
    $.get("/Clientes/Get/" + id, function (data) {
        if (!data) return;

        $("#formEditCliente [name='Id']").val(data.id);
        $("#formEditCliente [name='Identificacion']").val(data.identificacion);
        $("#formEditCliente [name='Nombre']").val(data.nombre);
        $("#formEditCliente [name='Telefono']").val(data.telefono);
        $("#formEditCliente [name='Email']").val(data.email);

        $("#modalEditCliente").modal("show");
    });
}

$("#formEditCliente").submit(function (e) {
    e.preventDefault();

    $.ajax({
        url: "/Clientes/Edit",
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

function eliminarCliente(id) {
    Swal.fire({
        title: "¿Eliminar cliente?",
        text: "Esta acción es irreversible",
        icon: "warning",
        showCancelButton: true,
        confirmButtonText: "Sí, eliminar"
    }).then(res => {
        if (res.isConfirmed) {
            $.post("/Clientes/Delete", { id: id }, function (resp) {
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
