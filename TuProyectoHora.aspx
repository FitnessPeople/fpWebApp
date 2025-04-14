<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TuProyectoHora.aspx.cs" Inherits="fpWebApp.TuProyectoHora" %>

<!DOCTYPE html>
<html lang="es">
<head runat="server">
    <meta charset="utf-8">
    <title>Ejemplo Hora con AJAX</title>
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            // Al hacer clic en el botón, se hace la llamada AJAX
            $("#btnObtenerHora").click(function () {
                // Llamada AJAX al método en el CodeBehind
                $.ajax({
                    type: "POST",
                    url: "TuProyectoHora.aspx/GetCurrentTime",  // Llamamos al método del code-behind
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        // Mostrar la hora retornada en el div
                        $("#lblHora").text("Hora actual: " + response.d);
                    },
                    failure: function (response) {
                        alert("Error al obtener la hora.");
                    }
                });
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Obtener la hora actual desde el servidor</h2>
            <button type="button" id="btnObtenerHora" class="btn btn-primary">Obtener Hora</button>
            <p id="lblHora" style="margin-top: 10px; font-size: 18px;"></p>
        </div>
    </form>
</body>
</html>
