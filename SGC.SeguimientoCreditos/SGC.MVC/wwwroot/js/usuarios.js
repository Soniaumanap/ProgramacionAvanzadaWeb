let esEdicion = false;

function abrirModalCrear() {
    esEdicion = false;
    $("#tituloModalUsuario").text("Nuevo usuario");
    $("#UsuarioId").val("");
    $("#Identificacion").val("");
    $("#Nombre").val("");
    $("#Email").val("");
    $("#Rol").val("");
    $("#Activo").val("true");
    $("#Password").val("");
    $("#grupoPassword").show();

    const modal = new bootstrap.Modal(document.getElementById("modalUsuario"));
    modal.show();
}

function abrirModalEditar(id) {
    esEdicion = true;
    $("#tituloModalUsuario").text("Editar usuario");
    $("#Password").val("");
    $("#grupoPassword").hide(); // no cambiamos password aquí

    $.get("/Usuarios/Obtener", { id: id }, function (u) {
        // OJO: nombres en camelCase porque System.Text.Json suele serializar así
        $("#UsuarioId").val(u.id);
        $("#Identificacion").val(u.identificacion);
        $("#Nombre").val(u.nombre);
        $("#Email").val(u.email);
        $("#Rol").val(u.rol);
        $("#Activo").val(u.activo ? "true" : "false");

        const modal = new bootstrap.Modal(document.getElementById("modalUsuario"));
        modal.show();
    }).fail(function () {
        Swal.fire("Error", "No se pudo cargar la información del usuario.", "error");
    });
}

$("#formUsuario").submit(function (e) {
    e.preventDefault();

    const id = $("#UsuarioId").val();
    const identificacion = $("#Identificacion").val().trim();
    const nombre = $("#Nombre").val().trim();
    const email = $("#Email").val().trim();
    const rol = $("#Rol").val();
    const activo = $("#Activo").val() === "true";
    const password = $("#Password").val();

    if (!identificacion || !nombre || !email || !rol || (!esEdicion && !password)) {
        Swal.fire("Validación", "Todos los campos son obligatorios. En edición la contraseña puede quedar vacía.", "warning");
        return;
    }

    if (!esEdicion) {
        // CREAR
        $.post("/Usuarios/Crear",
            {
                identificacion: identificacion,
                nombre: nombre,
                email: email,
                password: password,
                rol: rol,
                activo: activo
            },
            function (resp) {
                if (resp.ok) {
                    Swal.fire("Éxito", resp.mensaje, "success")
                        .then(() => location.reload());
                } else {
                    Swal.fire("Error", resp.mensaje, "error");
                }
            })
            .fail(function () {
                Swal.fire("Error", "Ocurrió un error al crear el usuario.", "error");
            });

    } else {
        // EDITAR
        $.post("/Usuarios/Editar",
            {
                id: id,
                identificacion: identificacion,
                nombre: nombre,
                email: email,
                rol: rol,
                activo: activo
            },
            function (resp) {
                if (resp.ok) {
                    Swal.fire("Éxito", resp.mensaje, "success")
                        .then(() => location.reload());
                } else {
                    Swal.fire("Error", resp.mensaje, "error");
                }
            })
            .fail(function () {
                Swal.fire("Error", "Ocurrió un error al actualizar el usuario.", "error");
            });
    }
});

function eliminarUsuario(id) {
    Swal.fire({
        title: "Eliminar usuario",
        text: "¿Está seguro que desea eliminar este usuario?",
        icon: "warning",
        showCancelButton: true,
        confirmButtonText: "Sí, eliminar",
        cancelButtonText: "Cancelar"
    }).then(res => {
        if (res.isConfirmed) {
            $.post("/Usuarios/Eliminar", { id: id }, function (resp) {
                if (resp.ok) {
                    Swal.fire("Eliminado", resp.mensaje, "success")
                        .then(() => location.reload());
                } else {
                    Swal.fire("Error", resp.mensaje, "error");
                }
            }).fail(function () {
                Swal.fire("Error", "Ocurrió un error al eliminar el usuario.", "error");
            });
        }
    });
}
