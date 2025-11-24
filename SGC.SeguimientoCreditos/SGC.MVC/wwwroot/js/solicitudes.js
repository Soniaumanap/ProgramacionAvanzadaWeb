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
        dataType: "json", // esperamos JSON sí o sí
        success: function (resp) {
            console.log("Respuesta Crear Solicitud:", resp);

            if (resp.ok) {
                Swal.fire("Éxito", resp.mensaje, "success")
                    .then(() => {
                        $("#modalSolicitud").modal("hide");
                        $("#formSolicitud")[0].reset();
                    });
            } else {
                Swal.fire("Error", resp.mensaje, "error");
            }
        },
        error: function (xhr, status, errorThrown) {
            console.error("Error AJAX:", status, errorThrown, xhr.responseText);
            Swal.fire("Error", "Ocurrió un error al procesar la solicitud.", "error");
        }
    });
});
