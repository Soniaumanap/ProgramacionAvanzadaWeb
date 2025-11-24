function abrirModalSolicitud() {
    $("#formSolicitud")[0].reset();
    $("#modalSolicitud").modal("show");
}

$("#formSolicitud").submit(function (e) {
    e.preventDefault();

    $.ajax({
        url: "/Solicitudes/Crear",
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
