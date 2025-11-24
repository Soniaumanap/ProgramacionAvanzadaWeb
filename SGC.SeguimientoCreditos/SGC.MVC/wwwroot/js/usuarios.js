function enviarAprobacion(id) {
    Swal.fire({
        title: "¿Enviar a aprobación?",
        text: "La gestión se enviará a aprobación.",
        icon: "question",
        showCancelButton: true,
        confirmButtonText: "Sí, enviar"
    }).then(res => {
        if (res.isConfirmed) {
            $.post("/Gestiones/EnviarAprobacion", { id: id }, function (resp) {
                if (resp.ok) {
                    Swal.fire("Éxito", resp.mensaje, "success")
                        .then(() => location.reload());
                } else {
                    Swal.fire("Error", resp.mensaje, "error");
                }
            });
        }
    });
}

function aprobar(id) {
    Swal.fire({
        title: "¿Aprobar gestión?",
        text: "La gestión quedará aprobada.",
        icon: "question",
        showCancelButton: true,
        confirmButtonText: "Sí, aprobar"
    }).then(res => {
        if (res.isConfirmed) {
            $.post("/Gestiones/Aprobar", { id: id }, function (resp) {
                if (resp.ok) {
                    Swal.fire("Éxito", resp.mensaje, "success")
                        .then(() => location.reload());
                } else {
                    Swal.fire("Error", resp.mensaje, "error");
                }
            });
        }
    });
}

function devolver(id) {
    Swal.fire({
        title: "Devolver gestión",
        input: "textarea",
        inputLabel: "Comentario de devolución",
        inputPlaceholder: "Indique el motivo de la devolución...",
        inputAttributes: {
            "aria-label": "Comentario de devolución"
        },
        showCancelButton: true,
        confirmButtonText: "Devolver",
        preConfirm: (value) => {
            if (!value) {
                Swal.showValidationMessage("Debe ingresar un comentario");
            }
            return value;
        }
    }).then(res => {
        if (res.isConfirmed) {
            $.post("/Gestiones/Devolver", { id: id, comentario: res.value }, function (resp) {
                if (resp.ok) {
                    Swal.fire("Éxito", resp.mensaje, "success")
                        .then(() => location.reload());
                } else {
                    Swal.fire("Error", resp.mensaje, "error");
                }
            });
        }
    });
}

function verTracking(id) {
    $.get("/Gestiones/Tracking", { id: id }, function (lista) {
        if (!lista || lista.length === 0) {
            Swal.fire("Tracking", "No hay movimientos registrados para esta gestión.", "info");
            return;
        }

        let html = `
            <table class="table table-sm table-bordered">
                <thead>
                    <tr>
                        <th>Fecha</th>
                        <th>Acción</th>
                        <th>Comentario</th>
                        <th>Usuario</th>
                    </tr>
                </thead>
                <tbody>
        `;

        lista.forEach(t => {
            const fecha = new Date(t.fecha);
            const fechaStr = fecha.toLocaleString();
            html += `
                <tr>
                    <td>${fechaStr}</td>
                    <td>${t.accion}</td>
                    <td>${t.comentario ?? ""}</td>
                    <td>${t.usuarioNombre}</td>
                </tr>
            `;
        });

        html += `</tbody></table>`;

        Swal.fire({
            title: "Tracking gestión " + id,
            html: html,
            width: 800,
            confirmButtonText: "Cerrar"
        });
    });
}
