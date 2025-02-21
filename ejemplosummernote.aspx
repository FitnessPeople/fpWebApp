<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ejemplosummernote.aspx.cs" Inherits="fpWebApp.ejemplosummernote" ValidateRequest="false" %>

<html lang="es">
<head runat="server">
    <meta charset="utf-8" />
    <title>Ejemplo Quill en ASPX</title>

    <!-- CSS de Quill -->
    <link href="https://cdn.quilljs.com/1.3.6/quill.snow.css" rel="stylesheet">
    <!-- JS de Quill -->
    <script src="https://cdn.quilljs.com/1.3.6/quill.min.js"></script>
    <script>
        document.addEventListener("DOMContentLoaded", function () {
        quill = new Quill('#editor', {
            theme: 'snow'
        });

    // Recuperar el contenido guardado en el campo oculto y cargarlo en Quill
    var hiddenContent = document.getElementById('<%= hiddenEditor.ClientID %>').value;
    if (hiddenContent) {
        quill.root.innerHTML = hiddenContent;
        }
    });

    function getContent() {
        // Guardar el contenido del editor en el campo oculto antes de enviar el formulario
        document.getElementById('<%= hiddenEditor.ClientID %>').value = quill.root.innerHTML;
    }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container mt-4">
            <h2>Ejemplo de Quill en ASPX</h2>

            <h3>Editor Quill:</h3>
            <div id="editor" style="height: 200px;"></div>

            <!-- Campo oculto para guardar el contenido -->
            <asp:HiddenField ID="hiddenEditor" runat="server" />

            <br />
           <asp:Button ID="btnMostrar" runat="server" CssClass="btn btn-primary" Text="Guardar Contenido"
                OnClick="btnMostrar_Click" OnClientClick="getContent()" />

            <h3 class="mt-4">Vista previa del contenido:</h3>
            <asp:Literal ID="litPreviewEditor" runat="server"></asp:Literal>
        </div>
    </form>
</body>
</html>
