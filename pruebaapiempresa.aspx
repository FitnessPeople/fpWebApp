<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="pruebaapiempresa.aspx.cs" Inherits="fpWebApp.pruebaapiempresa" %>



<!DOCTYPE html>
<html lang="es">
<head>
  <meta charset="UTF-8">
  <title>Consulta empresa por NIT</title>
</head>
<body>
<input type="text" id="txbDocumento" placeholder="Escribe NIT" />
<button id="btnConsultar">Consultar empresa</button>
<pre id="salida"></pre>

<script>
    document.getElementById("btnConsultar").addEventListener("click", async () => {
        const nit = document.getElementById("txbDocumento").value.trim();
        if (!nit) { alert("Ingresa un NIT"); return; }

        const recurso = "f9nk-qw9u";
        const url = `https://www.datos.gov.co/resource/${recurso}.json?$where=identificacion='${encodeURIComponent(nit)}'`;
        console.log("Consultando:", url);

        try {
            const response = await fetch(url);
            if (!response.ok) throw new Error("HTTP error: " + response.status);
            const datos = await response.json();
            console.log("Datos obtenidos:", datos);

            if (datos.length === 0) {
                document.getElementById("salida").textContent = "No se encontró información para ese NIT.";
            } else {
                document.getElementById("salida").textContent = JSON.stringify(datos, null, 2);
            }
        } catch (err) {
            console.error("Error al consultar:", err);
            document.getElementById("salida").textContent = "Error: " + err;
        }
    });
</script>
</body>
</html>
