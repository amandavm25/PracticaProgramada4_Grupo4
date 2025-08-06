const apiUrl = "https://localhost:44377/api";

function mostrarResultadoBootstrap(texto, tipo) {
    const resultadoDiv = document.getElementById("resultado");
    resultadoDiv.className = `alert ${tipo}`;
    resultadoDiv.innerText = texto;
}

function obtenerChiste() {
    fetch(`${apiUrl}/chistes`)
        .then(res => res.json())
        .then(data => {
            mostrarResultadoBootstrap("Chiste: " + data.chiste, "alert-primary");
            document.getElementById("respuestaUsuario").style.display = "none";
        });
}

function obtenerDato() {
    fetch(`${apiUrl}/datos`)
        .then(res => res.json())
        .then(data => {
            mostrarResultadoBootstrap("Dato curioso: " + data.dato, "alert-info");
            document.getElementById("respuestaUsuario").style.display = "none";
        });
}

function obtenerAdivinanza() {
    fetch(`${apiUrl}/adivinanza`)
        .then(res => res.json())
        .then(data => {
            mostrarResultadoBootstrap("Adivinanza: " + data.adivinanza, "alert-warning");
            document.getElementById("respuestaUsuario").style.display = "block";
        });
}

function enviarRespuesta() {
    const respuesta = document.getElementById("respuesta").value;

    fetch(`${apiUrl}/adivinanza`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ respuesta })
    })
        .then(res => res.json())
        .then(data => {
            if (data.resultado === "¡Correcto!") {
                mostrarResultadoBootstrap("Resultado: ¡Correcto!", "alert-success");
            } else {
                mostrarResultadoBootstrap(`Resultado: Incorrecto. La correcta era: ${data.respuestaCorrecta}`, "alert-danger");
            }

            document.getElementById("respuestaUsuario").style.display = "none";
            document.getElementById("respuesta").value = "";
        });
}