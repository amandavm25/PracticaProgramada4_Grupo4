using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using System.Security.Policy;
using System.Web.Http;
using System.Web.Mvc;

namespace PracticaProgramada4.Controllers
{
    public class ContenidoController : ApiController

    {

        private static readonly List<string> chistes = new List<string>
        {
            "¿Qué hace una abeja en el gimnasio? ¡Zum-ba!",
            "¿Por qué los pájaros no usan Facebook? Porque ya tienen Twitter.",
            "¿Cuál es el café más peligroso del mundo? El ex-preso.",
            "¿Qué hace una computadora en la playa? ¡Nada, porque no sabe nadar!",
            "¿Qué hace un pez? ¡Nada!"
        };

        // Lista de datos curiosos
        private static readonly List<string> datos = new List<string>
        {
            "La miel nunca se echa a perder.",
            "Los pulpos tienen tres corazones.",
            "Un día en Venus dura más que un año en Venus.",
            "Los koalas duermen hasta 22 horas al día.",
            "Las abejas pueden reconocer rostros humanos.",
            "En Júpiter y Saturno llueven diamantes.",
            "Los flamencos nacen grises, no rosados."
        };

        // Adivinanza y su respuesta
        private static readonly List<Adivinanza> adivinanzas = new List<Adivinanza>
        {
            new Adivinanza { Pregunta = "Blanca por dentro, verde por fuera. Si quieres que te lo diga, espera.", Respuesta = "pera" },
            new Adivinanza { Pregunta = "Vuelo de noche, duermo en el día y nunca verás plumas en ala mía.", Respuesta = "murciélago" },
            new Adivinanza { Pregunta = "Tengo agujas y no sé coser, doy las horas y no sé leer.", Respuesta = "reloj" }
        };

        private static Adivinanza ultimaAdivinanza;

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/chistes")]
        public IHttpActionResult GetChiste()
        {
            var random = new Random();
            int index = random.Next(chistes.Count);
            return Ok(new { chiste = chistes[index] });
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/datos")]
        public IHttpActionResult GetDato()
        {
            var random = new Random();
            int index = random.Next(datos.Count);
            return Ok(new { dato = datos[index] });
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/adivinanza")]
        public IHttpActionResult GetAdivinanza()
        {
            var random = new Random();
            int index = random.Next(adivinanzas.Count);
            ultimaAdivinanza = adivinanzas[index]; 
            return Ok(new { adivinanza = ultimaAdivinanza.Pregunta });
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/adivinanza")]
        public IHttpActionResult PostRespuesta([FromBody] RespuestaUsuario respuesta)
        {
            if (ultimaAdivinanza == null)
            {
                return BadRequest("Primero debes pedir una adivinanza.");
            }

            string correcta = ultimaAdivinanza.Respuesta.ToLower();
            string enviada = respuesta?.respuesta?.Trim().ToLower();

            if (enviada == correcta)
            {
                return Ok(new { resultado = "¡Correcto!" });
            }
            else
            {
                return Ok(new { resultado = "Incorrecto", respuestaCorrecta = correcta });
            }
        }

        // Clases auxiliares
        public class RespuestaUsuario
        {
            public string respuesta { get; set; }
        }

        public class Adivinanza
        {
            public string Pregunta { get; set; }
            public string Respuesta { get; set; }
        }
    }
}