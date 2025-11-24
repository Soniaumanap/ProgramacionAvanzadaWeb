function buscarTracking() {
    let id = $("#gestionId").val();

    if (!id) {
        Swal.fire("Error", "Debe ingresar un número de gestión.", "error");
        return;
    }

    $.get("/Reporte/Buscar", { gestionId: id }, function (lista) {

        if (!lista || lista.length === 0) {
            Swal.fire("Sin datos", "No hay movimientos para esta gestión.", "info");
            return;
        }

        let html = `
            <table class="table table-bordered">
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
            const fecha = new Date(t.fecha).toLocaleString();

            html += `
                <tr>
                    <td>${fecha}</td>
                    <td>${t.accion}</td>
                    <td>${t.comentario ?? ""}</td>
                    <td>${t.usuarioNombre}</td>
                </tr>`;
        });

        html += `</tbody></table>`;

        Swal.fire({
            title: "Tracking de la Gestión " + id,
            html: html,
            width: 800
        });
    });
}
