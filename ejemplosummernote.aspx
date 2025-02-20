<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ejemplosummernote.aspx.cs" Inherits="fpWebApp.ejemplosummernote" %>

<html lang="es">
<head runat="server">
    <meta charset="utf-8">
    <title>FooTable con Summernote</title>

    <!-- jQuery -->
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>

    <!-- FooTable -->
    <link href="https://cdn.jsdelivr.net/npm/footable/css/footable.bootstrap.min.css" rel="stylesheet">
    <script src="https://cdn.jsdelivr.net/npm/footable/js/footable.min.js"></script>

    <!-- Summernote -->
    <link href="https://cdnjs.cloudflare.com/ajax/libs/summernote/0.8.18/summernote-lite.min.css" rel="stylesheet">
    <script src="https://cdnjs.cloudflare.com/ajax/libs/summernote/0.8.18/summernote-lite.min.js"></script>

    <script>
        $(document).ready(function () {
            // Inicializa FooTable
            $('.foo-table').footable();

            // Inicializa Summernote en cada campo de texto dentro de la tabla
            $('.summernote').summernote({
                height: 150,
                placeholder: 'Escribe aquí...',
                toolbar: [
                    ['style', ['bold', 'italic', 'underline']],
                    ['para', ['ul', 'ol', 'paragraph']],
                    ['view', ['fullscreen', 'codeview']]
                ]
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container mt-4">
            <h2>FooTable con Summernote</h2>

            <!-- Tabla FooTable -->
            <table class="foo-table table table-bordered" data-toggle="footable">
                <thead>
                    <tr>
                        <th>ID</th>
                        <th>Nombre</th>
                        <th>Descripción</th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td>1</td>
                        <td>Artículo 1</td>
                        <td>
                            <textarea class="summernote" name="descripcion1"></textarea></td>
                    </tr>
                    <tr>
                        <td>2</td>
                        <td>Artículo 2</td>
                        <td>
                            <textarea id="basic-example">
  <p><img style="display: block; margin-left: auto; margin-right: auto;" title="Tiny Logo" src="https://www.tiny.cloud/docs/images/logos/android-chrome-256x256.png" alt="TinyMCE Logo" width="128" height="128"></p>
  <h2 style="text-align: center;">Welcome to the TinyMCE editor demo!</h2>

  <h2>Got questions or need help?</h2>

  <ul>
    <li>Our <a href="https://www.tiny.cloud/docs/tinymce/6/">documentation</a> is a great resource for learning how to configure TinyMCE.</li>
    <li>Have a specific question? Try the <a href="https://stackoverflow.com/questions/tagged/tinymce" target="_blank" rel="noopener"><code>tinymce</code> tag at Stack Overflow</a>.</li>
    <li>We also offer enterprise grade support as part of <a href="https://www.tiny.cloud/pricing">TinyMCE premium plans</a>.</li>
  </ul>

  <h2>A simple table to play with</h2>

  <table style="border-collapse: collapse; width: 100%;" border="1">
    <thead>
      <tr>
        <th>Product</th>
        <th>Cost</th>
        <th>Really?</th>
      </tr>
    </thead>
    <tbody>
      <tr>
        <td>TinyMCE</td>
        <td>Free</td>
        <td>YES!</td>
      </tr>
      <tr>
        <td>Plupload</td>
        <td>Free</td>
        <td>YES!</td>
      </tr>
    </tbody>
  </table>

  <h2>Found a bug?</h2>

  <p>
    If you think you have found a bug please create an issue on the <a href="https://github.com/tinymce/tinymce/issues">GitHub repo</a> to report it to the developers.
  </p>

  <h2>Finally ...</h2>

  <p>
    Don't forget to check out our other product <a href="http://www.plupload.com" target="_blank">Plupload</a>, your ultimate upload solution featuring HTML5 upload support.
  </p>
  <p>
    Thanks for supporting TinyMCE! We hope it helps you and your users create great content.<br>All the best from the TinyMCE team.
  </p>
</textarea></td>
                    </tr>
                </tbody>
            </table>

            <br>
            <!-- Botón para guardar contenido -->
            <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-primary" OnClick="btnGuardar_Click" />

            <!-- Muestra el contenido guardado -->
            <div class="mt-4">
                <h4>Contenido Guardado:</h4>
                <asp:Literal ID="litContenido" runat="server"></asp:Literal>
            </div>
        </div>
    </form>
</body>
</html>
