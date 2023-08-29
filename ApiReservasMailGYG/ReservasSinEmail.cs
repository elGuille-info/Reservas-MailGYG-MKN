// Clase para las reservas que no tienen email.             (27/ago/23 17.48)

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiReservasMailGYG
{
    public class ReservasSinEmail
    {
        //public ReservasSinEmail()
        //{
        //}

        public ReservasSinEmail(KNDatos.Reservas re)
        {
            LaReserva = re.Clone();
            Nombre = re.Nombre;
            Telefono = re.Telefono;
            Email = re.Email;
            Notas = re.Notas;
            Actividad = re.ActividadMostrar;
            Fecha = re.FechaActividad;
            Hora = re.HoraActividad;
            ReservaGYG = AsignarReservaGYG(Notas);
        }

        public KNDatos.Reservas LaReserva { get; set; }

        /// <summary>
        /// Devuelve los datos de la actividad
        /// </summary>
        public string Reserva { get => $"{Fecha:dd/MM/yy} {Hora:hh\\:mm} {Actividad}"; }

        public string Pax { get => LaReserva.Paxs; }

        // Las columnas a usar en esta clase.                   (29/ago/23 12.09)
        public static string[] Columnas { get; } = { "Booking", "Nombre", "Teléfono", "Reserva", "PAX", "Email", "Notas" };

        public string ValorColumna(string columna)
        {
            columna = columna.ToLower();
            if (columna == "nombre") return Nombre;
            if (columna == "teléfono") return Telefono;
            if (columna == "reserva") return Reserva;
            if (columna == "booking") return ReservaGYG;
            if (columna == "pax") return Pax;
            if (columna == "email") return Email;
            if (columna == "notas") return Notas;
            return "";
        }

        /// <summary>
        /// Asignar el Booking de GYG según el contenido de las notas de la reserva.
        /// </summary>
        /// <param name="notas">Las notas de la reserva</param>
        /// <returns>Una cadena con el código de booking o un trozo de las notas.</returns>
        public static string AsignarReservaGYG(string notas)
        {
            string nota = "";
            // Poner solo los xx primeros caracteres de las notas. (25/ago/23 14.18)
            //GetYourGuide - GYGLMWA85MXQ - (Spain) - Spanish (Live tour guide) -
            if (notas.StartsWith("GetYourGuide"))
            {
                int j = notas.IndexOf("- GYG");
                if (j > -1)
                {
                    nota = notas.Substring(j + 2, 12);
                }
            }
            else
            {
                nota = notas;
            }
            if (string.IsNullOrEmpty(nota))
            {
                if (string.IsNullOrEmpty(notas) == false)
                {
                    nota = notas.Substring(0, 28);
                }
            }
            return nota;
        }

        //public ReservasSinEmail(string nombre, string notas)
        //{
        //    Nombre = nombre;
        //    Notas = notas;
        //}

        //public ReservasSinEmail(string nombre, string telefono, string email, string notas, string actividad, DateTime fecha, TimeSpan hora)
        //{

        //}

        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }

        public string Notas { get; set; }
        public string ReservaGYG { get; set; }

        public string Actividad { get; set; }
        public DateTime Fecha { get; set; }

        public TimeSpan Hora { get; set; }

    }
}