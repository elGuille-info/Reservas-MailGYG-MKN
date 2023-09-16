// Clase para las reservas que no tienen email.             (27/ago/23 17.48)

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using KNDatos;

namespace ApiReservasMailGYG
{
    public class ReservasGYG
    {
        //
        // Las propiedades
        //

        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }

        public string Notas { get; set; }
        public string BookingGYG { get; set; }

        public string Actividad { get; set; }
        public DateTime Fecha { get; set; }

        public TimeSpan Hora { get; set; }

        /// <summary>
        /// Los datos de la reserva usada en esta instancia.
        /// </summary>
        public KNDatos.Reservas LaReserva { get; set; }

        /// <summary>
        /// Devuelve los datos de la actividad
        /// </summary>
        public string Reserva { get => $"{Fecha:dd/MM/yy} {Hora:hh\\:mm} {Actividad}"; }

        public string Pax { get => LaReserva.Paxs; }

        public bool Cancelada { get; set; }

        // Añado datos para saber si ha salido, etc.        (15/sep/23 17.00)

        /// <summary>
        /// El número de control.
        /// </summary>
        public int Control { get; set; }

        /// <summary>
        /// Para saber si ha vuelto.
        /// </summary>
        /// <remarks>Será 0 si no ha salido, 1 si es vuelta parcial, 2 si ya ha vuelto.</remarks>
        public int Vuelta { get; set; }

        /// <summary>
        /// La hora de salida.
        /// </summary>
        public TimeSpan HoraSalida { get; set; }
        /// <summary>
        /// La hora de vuelta.
        /// </summary>
        public TimeSpan HoraVuelta { get; set; }

        public ReservasGYG(KNDatos.Reservas re)
        {
            LaReserva = re.Clone();
            // Poner el nombre con título.                  (16/sep/23 03.25)
            Nombre = re.Nombre.ToTitle();
            Telefono = re.Telefono;
            Email = re.Email;
            Notas = re.Notas;
            Actividad = re.ActividadMostrar;
            Fecha = re.FechaActividad;
            Hora = re.HoraActividad;
            BookingGYG = AsignarBookingGYG(Notas);
            Cancelada = re.CanceladaCliente;
            HoraSalida = re.HoraSalida;
            HoraVuelta = re.HoraVuelta;
            Control = re.Control;
            Vuelta = re.Vuelta;
        }

        // Las columnas a usar en esta clase.                   (29/ago/23 12.09)
        public static string[] Columnas { get; } =
            { "Booking", "Nombre", "Teléfono", "Reserva", "PAX", "Cancelada", "H.Salida", "H.Vuelta", "Control", "Vuelta", "Email", "Notas" };
        //{ "Booking", "Nombre", "Teléfono", "Reserva", "PAX", "Email", "Notas", "Cancelada", "Control", "Vuelta", "H.Salida", "H.Vuelta" };

        /// <summary>
        /// Devuelve el contenido de la columna indicada.
        /// </summary>
        /// <param name="columna"></param>
        public string ValorColumna(string columna)
        {
            columna = columna.ToLower();
            if (columna == "nombre") return Nombre;
            if (columna == "teléfono") return Telefono;
            if (columna == "reserva") return Reserva;
            if (columna == "booking") return BookingGYG;
            if (columna == "pax") return Pax;
            if (columna == "email") return Email;
            if (columna == "notas") return Notas;
            if (columna == "cancelada") return Cancelada.ToString();
            if (columna == "control") return Control.ToString();
            if (columna == "vuelta") return Vuelta.ToString();
            if (columna == "h.salida") return HoraSalida.ToString("hh\\:mm");
            if (columna == "h.vuelta") return HoraVuelta.ToString("hh\\:mm");
            return "";
        }

        /// <summary>
        /// El índice de la columna (por el nombre).
        /// </summary>
        /// <param name="sender"></param>
        /// <returns>El índice de la columna o -1 si no existe.</returns>
        public static int IndexColumna(string mnu)
        {
            // Se supone que no será nulo, pero...          (16/sep/23 17.30)
            //if (sender == null) return -1;

            //var mnu = sender.GetType().Name;
            int copiarTodo = 99;
            int index = -1;
            //   0          1         2           3          4      5            6           7           8          9         10       11
            //{ "Booking", "Nombre", "Teléfono", "Reserva", "PAX", "Cancelada", "H.Salida", "H.Vuelta", "Control", "Vuelta", "Email", "Notas" };
            if (mnu == "MnuCopiarBooking") index = 0;
            else if (mnu == "MnuCopiarNombre") index = 1;
            else if (mnu == "MnuCopiarTelefono") index = 2;
            else if (mnu == "MnuCopiarReserva") index = 3;
            else if (mnu == "MnuCopiarPax") index = 4;
            else if (mnu == "MnuCopiarEmail") index = 10;
            else if (mnu == "MnuCopiarNotas") index = 11;
            else if (mnu == "MnuCopiarTodo") index = copiarTodo;
            else if (mnu == "MnuCopiarTodoConCr") index = copiarTodo + 1;
            //if (index == -1) return;
            return index;
        }

        /// <summary>
        /// Asignar el Booking de GYG según el contenido de las notas de la reserva.
        /// </summary>
        /// <param name="notas">Las notas de la reserva</param>
        /// <returns>Una cadena con el código de booking o un trozo de las notas.</returns>
        public static string AsignarBookingGYG(string notas)
        {
            string nota = "";
            // Poner solo los xx primeros caracteres de las notas. (25/ago/23 14.18)
            //GetYourGuide - GYGLMWA85MXQ - (Spain) - Spanish (Live tour guide) -
            //if (notas.StartsWith("GetYourGuide"))
            // Por si se añade texto al principio.          (15/sep/23 17.05)
            if (notas.Contains("GetYourGuide"))
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
    }
}