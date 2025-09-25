<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="pruebaapiempresa.aspx.cs" Inherits="fpWebApp.pruebaapiempresa" %>


<!DOCTYPE html>
<html lang="es">
<head>
  <meta charset="utf-8" />
  <title>Debug búsqueda Bucaramanga (datos.gov.co)</title>
  <style>
    body { font-family: Arial, sans-serif; padding: 12px; }
    #resultado div { margin-bottom: 10px; padding: 8px; border: 1px solid #ddd; border-radius: 6px; }
    pre { background:#f6f6f6; padding:8px; overflow:auto; max-height:240px; }
    .small { font-size:0.9em; color:#555; }
  </style>
</head>
<body>
  <h3>Buscar empresa (Bucaramanga - wf53-j577)</h3>
  <input id="inputText" placeholder="Ingresa NIT o texto (ej: 8000192059)" style="width:300px" />
  <button id="btnWhere">Buscar con $where (contains)</button>
  <button id="btnQ">Buscar con $q (búsqueda general)</button>

  <h4>Resultados</h4>
  <div id="resultado"></div>

  <h4>Debug / Network info</h4>
  <pre id="debug" class="small"></pre>

  <script>
      const base = "https://www.datos.gov.co/resource/wf53-j577.json";
      const out = document.getElementById("resultado");
      const dbg = document.getElementById("debug");

      function limpiar() {
          out.innerHTML = "";
          dbg.textContent = "";
      }

      function mostrarDebug(...args) {
          dbg.textContent += args.join(" ") + "\n";
          console.log(...args);
      }

      function mostrarDatos(data) {
          out.innerHTML = "";
          if (!Array.isArray(data) || data.length === 0) {
              out.innerHTML = "<div>No se encontró información.</div>";
              return;
          }
          data.forEach(item => {
              const html = `
          <div>
            <div><b>Razón social:</b> ${item.razon_social || "—"}</div>
            <div><b>NIT:</b> ${item.identificacion || "—"}</div>
            <div><b>Dirección:</b> ${item.dir_comercial || "—"}</div>
            <div><b>Municipio:</b> ${item.mun_comercial || "—"}</div>
            <div><b>Representante:</b> ${item.nom_rep_legal || "—"}</div>
          </div>
        `;
              out.innerHTML += html;
          });
      }

      async function fetchAndShow(url) {
          limpiar();
          mostrarDebug("URL ->", url);
          try {
              const res = await fetch(url);
              mostrarDebug("HTTP status:", res.status, res.statusText);
              const text = await res.text();
              mostrarDebug("Respuesta cruda (primeros 1000 chars):\n", text.slice(0, 1000));
              try {
                  const json = JSON.parse(text);
                  mostrarDebug("JSON parse OK. Registros:", Array.isArray(json) ? json.length : "no-array");
                  mostrarDatos(json);
              } catch (errJson) {
                  mostrarDebug("Error parseando JSON:", errJson);
                  out.innerHTML = `<div>Error al parsear JSON. Revisa debug (response puede no ser JSON válido).</div>`;
              }
          } catch (err) {
              // Este catch suele cubrir errores de red o CORS
              mostrarDebug("Fetch error:", err);
              out.innerHTML = `<div>Error de red o CORS. Mira la consola y el panel Network. Mensaje: ${err}</div>`;
          }
      }

      // Build URL usando $where (encodeamos todo)
      function urlWhere(term) {
          const where = `contains(identificacion,'${term}') OR contains(razon_social,'${term}') OR contains(mun_comercial,'${term}')`;
          return `${base}?$where=${encodeURIComponent(where)}&$limit=50`;
      }

      // Build URL usando $q (búsqueda full-text más simple)
      function urlQ(term) {
          return `${base}?$q=${encodeURIComponent(term)}&$limit=50`;
      }

      document.getElementById("btnWhere").addEventListener("click", () => {
          const t = document.getElementById("inputText").value.trim();
          if (!t) { alert("Ingresa un NIT o texto"); return; }
          // prueba $where primero
          const url = urlWhere(t);
          fetchAndShow(url);
      });

      document.getElementById("btnQ").addEventListener("click", () => {
          const t = document.getElementById("inputText").value.trim();
          if (!t) { alert("Ingresa un NIT o texto"); return; }
          const url = urlQ(t);
          fetchAndShow(url);
      });

      // SUGERENCIA: prueba con este valor
      // document.getElementById("inputText").value = "8000192059";
  </script>
</body>
</html>
