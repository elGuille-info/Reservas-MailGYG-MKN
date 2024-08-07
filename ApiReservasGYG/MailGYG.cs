﻿//
// Analizar los correos de GetYourGuide con nuevas reservas (12/ago/23 17.55)
//
// Lo cambio para usar .Net Standard 2.0                    (29/ago/23 16.17)
//

using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

using System.Globalization;
//using System.IO;

using KNDatos;
using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;
//using MimeKit.Text;

namespace ApiReservasMailGYG
{
    // Delegado para usar con los recursos.                 (18/sep/23 09.11)
    
    /// <summary>
    /// Delegado para acceder a los recursos.
    /// </summary>
    /// <returns>Una cadena con el contenido del recurso.</returns>
    public delegate Task<string> AccederRecursoDelegate(string recurso);

    public class MailGYG
    {
        // Esto no va...                                    (18/sep/23 18.17)
        // lo mismo hay que probar de otra forma...   

        /// <summary>
        /// Función para usar al enviar los mensajes al crear, modificar o cancelar una reserva.
        /// </summary>
        /// <param name="re">Referencia a la reserva nueva, modificada o cancelada.</param>
        /// <param name="delegado">Función a la que se llamará para obtener el texto del recurso que se deba usar en el mensaje.</param>
        /// <returns>Una tupla con el texto de la confirmación y el asunto a enviar por email.</returns>
        public static (string Body, string Asunto) TextoMensajeConfirmacion(Reservas re, AccederRecursoDelegate delegado)
        {
            var LaReserva = re;
            StringBuilder sb = new StringBuilder();

            var DatosVPWiz = new MKNUtilidades.VentasPlayaWiz(re);
            MKNUtilidades.VentasPlayaWiz.IncluirReportajeConfirmarReserva = false;
            MKNUtilidades.VentasPlayaWiz.IncluirTextosConfirmarReserva = false;

            bool enIngles = false; // = re.GYGLanguage.Contains("English");
            if (string.IsNullOrEmpty(re.GYGLanguage) == false)
            {
                enIngles = re.GYGLanguage.Contains("English");
            }

            string asunto;
            // Mandar el mensaje según sea modificar, cancelar o nueva. (09/sep/23 01.55)
            if (LaReserva.GYGTipo == Reservas.GYGTipos.Cancelada)
            {
                if (enIngles)
                {
                    asunto = $"Booking cancelled - {re.GYGReference}";
                }
                else
                {
                    asunto = $"Reserva cancelada - {re.GYGReference}";
                }

                sb.AppendLine("Reserva cancelada // Booking cancelled.");
                sb.AppendLine();
                sb.AppendLine($"GetYourGuide Booking # {re.GYGReference}");
                sb.AppendLine($"{re.ActividadMostrar} {re.FechaActividad:dd/MM/yyyy} {re.HoraActividad:hh\\:mm}");
                sb.AppendLine();
                sb.AppendLine($"Número de reserva MKN: {re.ID:#,###}");
                //sb.AppendLine();
                sb.AppendLine();
            }
            else
            {
                if (LaReserva.GYGTipo == Reservas.GYGTipos.Modificada)
                {
                    if (enIngles)
                    {
                        asunto = $"Booking changed - {re.GYGReference}";
                    }
                    else
                    {
                        asunto = $"Modificación reserva - {re.GYGReference}";
                    }
                }
                else
                {
                    if (enIngles)
                    {
                        asunto = $"Booking confirmation - {re.GYGReference}";
                    }
                    else
                    {
                        asunto = $"Confirmación reserva - {re.GYGReference}";
                    }
                }

                sb.Append(DatosVPWiz.ResumenReserva(esWeb: false, enIngles: enIngles));
                sb.AppendLine();
                sb.AppendLine();

                // Para alquiler, mandar otro texto diferente.  (09/sep/23 22.08)
                if (KNDatos.BaseKayak.ActividadesAlquiler().Contains(re.Actividad))
                {
                    //sb.Append(Properties.Resources.IMPORTANTE_ALQUILER.Replace(CrLf, "<br/>"));
                    sb.Append(delegado("IMPORTANTE_ALQUILER").Result.Replace(CrLf, "<br/>"));
                }
                else
                {
                    // Mandar el texto según el idioma.         (22/ago/23 10.58)
                    if (re.HoraActividad.Hours == 9)
                    {
                        if (enIngles)
                        {
                            //sb.Append(Properties.Resources.IMPORTANTE_EN_09_30.Replace(CrLf, "<br/>"));
                            sb.Append(delegado("IMPORTANTE_EN_09_30").Result.Replace(CrLf, "<br/>"));
                        }
                        else
                        {
                            //sb.Append(Properties.Resources.IMPORTANTE_ES_09_30.Replace(CrLf, "<br/>"));
                            sb.Append(delegado("IMPORTANTE_ES_09_30").Result.Replace(CrLf, "<br/>"));
                        }
                    }
                    else if (re.HoraActividad.Hours == 10 || re.HoraActividad == new TimeSpan(11, 0, 0))
                    {
                        if (enIngles)
                        {
                            //sb.Append(Properties.Resources.IMPORTANTE_EN_10_30_11_00.Replace(CrLf, "<br/>"));
                            sb.Append(delegado("IMPORTANTE_EN_10_30_11_00").Result.Replace(CrLf, "<br/>"));
                        }
                        else
                        {
                            //sb.Append(Properties.Resources.IMPORTANTE_ES_10_30_11_00.Replace(CrLf, "<br/>"));
                            sb.Append(delegado("IMPORTANTE_ES_10_30_11_00").Result.Replace(CrLf, "<br/>"));
                        }
                    }
                    else
                    {
                        if (enIngles)
                        {
                            //sb.Append(Properties.Resources.IMPORTANTE_EN.Replace(CrLf, "<br/>"));
                            sb.Append(delegado("IMPORTANTE_EN").Result.Replace(CrLf, "<br/>"));
                        }
                        else
                        {
                            //sb.Append(Properties.Resources.IMPORTANTE_ES.Replace(CrLf, "<br/>"));
                            sb.Append(delegado("IMPORTANTE_ES").Result.Replace(CrLf, "<br/>"));
                        }
                    }
                }

                // En temporada alta, hasta mediados septiembre (18/sep/23 05.18)
                if (DateTime.Today <= new DateTime(2023, 9, 15))
                {
                    sb.Append(delegado("IMPORTANTE_Lee_esto_Maro").Result.Replace(CrLf, "<br/>"));
                }

                // Indicar siempre que hagan la reseña.         (11/sep/23 10.25)
                //// Si es para el mismo día de la actividad.     (24/ago/23 06.24)
                //if (DateTime.Today == re.FechaActividad)
                sb.AppendLine("<br/>");
                sb.Append("<br/>");
                if (enIngles)
                {
                    sb.Append("We would love to receive a review on the GetYourGuide website with your opinion on this activity, taking into account that <b>Kayak Makarena</b> is responsible for managing the reservations and <b>Maro - Kayak Nerja</b> carries out the routes.<br/>");
                }
                else
                {
                    sb.Append("Nos encantaría recibir una reseña en el sitio de GetYourGuide con tu opinión sobre esta actividad, teniendo en cuenta que <b>Kayak Makarena</b> es la encargada de gestionar las reservas y <b>Maro - Kayak Nerja</b> realiza las rutas.<br/>");
                }
            }

            // Añadir la firma de Kayak Makarena                (18/sep/23 05.33)
            MailGYG.FirmaMakarena(sb, enIngles);

            return (sb.ToString(), asunto);
        }

        /// <summary>
        /// El cambio de línea en las cajas de texto.
        /// </summary>
        /// <remarks>En aplicaciones de Windows Forms es \n en WinUI es \r en Android e iOS es \n</remarks>
        public static string CambioLinea { get; set; } = "\n";

        /// <summary>
        /// El retorno de carro.
        /// </summary>
        public static string CrLf => "\r\n";

        //public static List<string> CorreosRecibidos(DateTime fecha1, DateTime fecha2, bool soloNuevos)
        //{
        //    List<string> col = new List<string>();

        //    return col;
        //}

        // Extraer el contenido entre las partes indicadas en campo1 y campo2 que contenga buscar

        /// <summary>
        /// Extraer el contenido entre las partes indicadas en campo1 y campo2 que contenga buscar.
        /// </summary>
        /// <param name="texto">El texto a evaluar.</param>
        /// <param name="campo1">La primera cadena a comprobar.</param>
        /// <param name="campo2">La segunda cadena a comprobar.</param>
        /// <param name="buscar">El texto que debe estar entre las 2 cadenas.</param>
        /// <returns>Una cadena con el contenido buscado o una cadena vacía.</returns>
        private static string ExtraerEntre(string texto, string campo1, string campo2, string buscar)
        {
            int posIni = 0;
            int i;
            string ret = "";

            do
            {
                i = texto.IndexOf(campo1, posIni, StringComparison.OrdinalIgnoreCase);
                if (i > -1)
                {
                    int j = texto.IndexOf(campo2, i + campo1.Length, StringComparison.OrdinalIgnoreCase);
                    if (j > -1)
                    {
                        var res = texto.Substring(i + campo1.Length, j - i - campo1.Length);
                        // Comprobar si tiene la cadena a buscar.
                        if (res.IndexOf(buscar, StringComparison.OrdinalIgnoreCase) > -1)
                        {
                            ret = res;
                            break;
                        }
                    }
                    posIni += campo1.Length;
                }
            } while (i > -1);

            return ret;
        }

        // Extraer el texto entre lo indicado en campo1 y hasta campo2, devolverlo como array

        /// <summary>
        /// Extrae una cadena entre dos textos.
        /// </summary>
        /// <param name="texto">El texto a evaluar.</param>
        /// <param name="campo1">La primera cadena a comprobar.</param>
        /// <param name="campo2">La segunda cadena a comprobar.</param>
        /// <returns>El contenido de lo que haya entre campo1 y campo2.</returns>
        private static List<string> ExtraerEntre(string texto, string campo1, string campo2)
        {
            List<string> filas = new List<string>();

            int i = texto.IndexOf(campo1, StringComparison.OrdinalIgnoreCase);
            if (i > -1)
            {
                int j = texto.IndexOf(campo2, i + campo1.Length, StringComparison.OrdinalIgnoreCase);
                if (j > -1)
                {
                    string res = texto.Substring(i + campo1.Length, j - i - campo1.Length);
                    var lineas = res.Split(CambioLinea.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    filas.AddRange(lineas.ToList());
                }
            }
            return filas;
        }

        /// <summary>
        /// Extraer del texto indicado lo que haya entre los dos campos.
        /// </summary>
        /// <param name="texto">El texto a evaluar.</param>
        /// <param name="campo1">La cadena a buscar.</param>
        /// <param name="campo2">La segunda cadana a buscar.</param>
        /// <returns>Lo que haya en el texto entre los dos campos indicados.</returns>
        private static string Extraer(string texto, string campo1, string campo2)
        {
            string res = "";

            int i = texto.IndexOf(campo1, StringComparison.OrdinalIgnoreCase);
            if (i > -1)
            {
                int j = texto.IndexOf(campo2, i + campo1.Length, StringComparison.OrdinalIgnoreCase);
                if (j == -1)
                {
                    j = texto.Length + 2;
                }
                //res = texto.Substring(i + campo1.Length, j - i - campo1.Length).Replace("\n", "\r\n");
                res = texto.Substring(i + campo1.Length, j - i - campo1.Length); //.Replace("\v", "\n");
                                                                                 // Esto es por si se añaden líneas manualmente. (25/ago/23 23.48)
                int k = res.IndexOf("\v");
                if (k > -1)
                {
                    res = texto.Substring(i + campo1.Length, j - i - campo1.Length).Replace("\v", "\r\n");
                }
            }
            return res.Trim();
        }

        /// <summary>
        /// Extraer del texto indicado lo que haya en la línea index posterior al campo indicado.
        /// </summary>
        /// <param name="texto">El texto a evaluar.</param>
        /// <param name="campo">La cadena a buscar.</param>
        /// <param name="index">El número de línea posterior al campo a buscar.</param>
        /// <returns>El contenido de la línea indicada después de campo.</returns>
        private static string ExtraerDespues(string texto, string campo, int index)
        {
            string res = "";

            int i = texto.IndexOf(campo, StringComparison.OrdinalIgnoreCase);
            if (i > -1)
            {
                var elTexto = texto.Substring(i);
                var lineas = elTexto.Split(CambioLinea.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (lineas.Length <= index) { return ""; }
                res = lineas[index];
            }
            return res.Trim();
        }

        /// <summary>
        /// Extraer del texto indicado lo que haya a continuación del campo indicado hasta final de la línea.
        /// </summary>
        /// <param name="texto">El texto a evaluar.</param>
        /// <param name="campo">La cadena a buscar.</param>
        /// <returns>Lo que haya en la línea donde está campo.</returns>
        private static string Extraer(string texto, string campo)
        {
            string res = "";

            int i = texto.IndexOf(campo, StringComparison.OrdinalIgnoreCase);
            if (i > -1)
            {
                int j = texto.IndexOf(CambioLinea, i + campo.Length, StringComparison.OrdinalIgnoreCase);
                if (j == -1) { j = texto.Length + 2; }
                res = texto.Substring(i + campo.Length, j - i - campo.Length);
            }
            return res.Trim();
        }

        /// <summary>
        /// Extrae lo que haya en la línea desde el principio hasta el campo indicado.
        /// </summary>
        /// <param name="texto">El texto a evaluar.</param>
        /// <param name="campo">La cadena a buscar.</param>
        /// <returns>Lo que haya antes del texto indicado en el campo</returns>
        private static string ExtraerAntes(string texto, string campo)
        {
            string res = "";

            int i = texto.IndexOf(campo, StringComparison.OrdinalIgnoreCase);
            if (i > -1)
            {
                var elTexto = texto.Substring(0, i).Trim();
                res = elTexto;

                //// Buscar el espacio anterior
                //int j = elTexto.LastIndexOf(" ");
                //if (j == -1)
                //{
                //    j = 0;
                //}
                //res = elTexto.Substring(j);
            }
            return res.Trim();
        }

        /// <summary>
        /// Comprobar si todos los campos usados están en la cadena a evaluar.
        /// </summary>
        /// <param name="texto">El texto a evaluar.</param>
        /// <returns>True si algunas de los campos buscados no está en el texto.</returns>
        /// <remarks>Esto es para las reservas, no las modificaciones ni cancelaciones.</remarks>
        private static bool ComprobarCamposEmailGYG(string texto)
        {
            int i;

            // Cuando es alquiler, algunos campos no se indican. (05/sep/23 11.30)
            i = texto.IndexOf("Option:");
            if (i == -1) { return true; }
            i = texto.IndexOf("Date:");
            if (i == -1) { return true; }
            i = texto.IndexOf("Price:");
            if (i == -1) { return true; }
            i = texto.IndexOf("Reference number:");
            if (i == -1) { return true; }
            i = texto.IndexOf("Phone:");
            if (i == -1) { return true; }
            i = texto.IndexOf("Main customer:");
            if (i == -1) { return true; }
            // Esto no está en los alquileres               (05/sep/23 11.32)
            //i = texto.IndexOf("children in your group.:");
            //if (i == -1) { return true; }
            // Esto no está en los alquileres               (05/sep/23 11.33)
            //i = texto.IndexOf("Tour language:");
            //if (i == -1) { return true; }
            i = texto.IndexOf("Number of participants:");
            if (i == -1) { return true; }

            return false;
        }

        /*
        We would like to inform you that the following booking has changed:
        GYGX7QWFLGXX

        Name: Mathilde Sabouni

        Tour: Nerja: Cliffs of Maro-Cerro Gordo Guided Kayak Tour

        Date: September 9, 2023 , 10:30 AM

        Number of participants: 2

        Language: English

        Comment: -

        Answer to your question: No children    
        */

        /// <summary>
        /// Comprueba si es una modificación de reserva.
        /// </summary>
        /// <param name="email"></param>
        /// <returns>Nulo si no es modificación o los datos de la reserva a cambiar.</returns>
        private static Reservas EsModificar(string email)
        {
            if (email.Contains("following booking has changed:") == false)
            {
                return null;
            }

            var refGyG = ExtraerDespues(email, "following booking has changed:", 1);
            var nombre = Extraer(email, "Name:");
            var actividad = Extraer(email, "Tour:");
            var notas = Extraer(email, "Comment:");
            if (notas.StartsWith(" -")) { notas = ""; }
            var fecGYG = Extraer(email, "Date:");
            // Date: September 9, 2023 , 10:30 AM

            //var fec = DateTime.ParseExact(fecGYG, "MMMM dd, yyyy , h:mm", System.Globalization.CultureInfo.InvariantCulture);
            //var fec = DateTime.ParseExact(fecGYG, "MMMM d, yyyy , h:mm", System.Globalization.CultureInfo.GetCultureInfo("en-US"));
            //var fec = DateTime.ParseExact(fecGYG, "MMMM d, yyyy h:mm", System.Globalization.CultureInfo.GetCultureInfo("en-US"));
            string fecD ="01/01/23", fecH="00:00";
            int i = fecGYG.LastIndexOf(",");
            if (i > -1)
            {
                fecD = fecGYG.Substring(0, i).Trim();
                int j = fecGYG.IndexOf(" ", i + 1);
                int k = fecGYG.IndexOf(" ", j + 1);
                fecH = fecGYG.Substring(i+1, k-j).Trim();
            }
            // Comprobar que la hora no sea menor           (06/oct/23 04.14)
            // de la hora de inicio de la actividad.
            var laHora = fecH.AsTimeSpan();
            var laFecha = DateTime.Parse(fecD);
            // Cargar el Config del año de la actividad.    (09/oct/23 04.16)
            // Asegurse que KNDatos.Config.Current está inicializada.
            if (KNDatos.Config.Current == null)
            {
                KNDatos.Config.LeerDatos(laFecha.Year, "MailGYG");
            }

            if (laHora.Hours < KNDatos.Config.Current.HoraActividadIni.Hours)
            {
                laHora = new TimeSpan(laHora.Hours + 12, laHora.Minutes, 0);
            }
            var pax = Extraer(email, "Number of participants:");

            Reservas re = new Reservas
            {
                GYGTipo = Reservas.GYGTipos.Modificada,
                GYGFechaHora = fecGYG,
                GYGOption = actividad,
                GYGReference = refGyG, // El número de booking
                Nombre = nombre,
                FechaActividad = laFecha, // DateTime.Parse(fecD), // fec.Date,
                HoraActividad = laHora, // fecH.AsTimeSpan(), //.TimeOfDay,
                Adultos = pax.AsInteger(),
                GYGNotas = notas,
            };

            return re;
        }

        /*
        We’re writing to let you know that the following booking has been canceled.

        Reference Number:	GYG998GKNNZN 
        Name:	Guillermo Som Cerezo
        Date:	September 25, 2023 4:00 PM
        Activity:	Nerja: Cliffs of Maro-Cerro Gordo Kayak Rental for 2 pax
        Please remove this customer from your list.


        We’re writing to let you know that the following booking has been canceled.

        Reference Number:	GYGN6GHXM5R3 
        Name:	Raquel Sánchez Rubio
        Date:	September 10, 2023 12:00 PM
        Activity:	Nerja: Cliffs of Maro-Cerro Gordo Guided Kayak Tour
        Please remove this customer from your list.
        */

        /// <summary>
        /// Comprueba si es una cancelación.
        /// </summary>
        /// <param name="email"></param>
        /// <returns>Nulo si no es cancelación o los datos de la reserva a cancelar.</returns>
        private static Reservas EsCancelacion(string email)
        {
            // Las cancelaciones por mal tiempo no indican  (22/mar/24 16.14)
            // following booking has been canceled si no: Cancellation date
            // pero no se indica el resto de textos... 

            // En el mensaje incluye "was cancelled" delante está el número de reserva.
            // El nombre del cliente está en:
            //      Customer: Raphael rubiano vasco
            // La actividad:
            //      Tour: Nerja: Cliffs of Maro-Cerro Gordo Guided Kayak Tour
            // La fecha de la actividad:
            //      Date: 2024-03-23 11:00:00
            // Si es por mal tiempo:
            //      Cancellation reason: Bad weather conditions
            /*
            GYGZG27V3455 was cancelled
            ...
            Customer: Raphael rubiano vasco
            Tour: Nerja: Cliffs of Maro-Cerro Gordo Guided Kayak Tour
            Date: 2024-03-23 11:00:00
            Price paid: 100.02 EUR
            Cancellation date: 2024-03-22 16:04:01
            Cancellation reason: Bad weather conditions
            */

            // Si se cancela porque nosotros lo solicitamos (31/may/24 15.05)
            // tendrá: Cancellation date

            //if (email.Contains("following booking has been canceled") == false)
            //{
            //    // No sirve salvo que se tenga acceso al asunto en el que se indica el número de reserva
            //    //if (email.Contains("Cancellation date") == false)
            //    //{
            //    //    return null;
            //    //}
            //    return null;
            //}

            string refGyG; // = Extraer(email, "Reference Number:");
            string nombre; // = Extraer(email, "Name:");
            string fecGYG; // = Extraer(email, "Date:");
            string actividad; // = Extraer(email, "Activity:");
            string notas;

            if (email.Contains("following booking has been canceled"))
            {
                refGyG = Extraer(email, "Reference Number:");
                nombre = Extraer(email, "Name:");
                fecGYG = Extraer(email, "Date:");
                actividad = Extraer(email, "Activity:");
                notas = "Cancelado por el cliente";
            }
            else if (email.Contains("Cancellation date"))
            {
                // GYG7VL448XK7 was cancelled
                var lista = ExtraerEntre(email, "GYG", "was cancelled");
                if (lista.Count > 0)
                {
                    refGyG = $"GYG{lista[0]}";
                }
                else
                {
                    refGyG = ExtraerAntes(email, "was cancelled");
                }
                nombre = Extraer(email, "Customer:");
                fecGYG = Extraer(email, "Date:");
                actividad = Extraer(email, "Tour:");
                notas = Extraer(email, "Cancellation reason:");
            }
            else
            {
                return null;
            }

            Reservas re = new Reservas
            {
                GYGTipo = Reservas.GYGTipos.Cancelada, 
                GYGFechaHora = fecGYG,
                GYGOption = actividad,
                GYGReference = refGyG, // El número de booking
                Nombre = nombre,
                GYGNotas = notas,
                //FechaActividad = fec.Date,
                //HoraActividad = fec.TimeOfDay
            };

            return re;
        }

        /*
        El contenido del email/mensaje será como este:

        Hi supply partner,

        Great news! The following offer has been booked:

        Nerja: Cliffs of Maro-Cerro Gordo Guided Kayak Tour (RutasKayak)
        Option: 2-Hour Tour (Ruta Corta)

        View booking
        Most important data for this booking:

        Date: 08 September 2023, 16:15 (04:15pm)

        Price: € 97.70

        Number of participants:
        1 x Child (Age 4 - 6 ) (€ 0.00)
        1 x Youth (Age 7 - 15 ) (€ 23.90)
        2 x Adults (Age 16 - 99 ) (€ 36.90)
        Reference number: GYG2RA2KZ692

        Main customer:
        Pedro Pacheco dominguez


        Spain
        customer-ewtbjjgqeoquqzyr@reply.getyourguide.com
        Phone: +34 607 68 95 93
        Please provide a phone number connected to Whatsapp. Please also provide the ages of all children in your group.:
        Una niña de 12 años, y otra de 5 años número de teléfono 607689593

        Tour language: Spanish (Live tour guide)

        Best regards,
        The GetYourGuide Team
        */

        /// <summary>
        /// Crea una reserva a partir del texto del email de GYG
        /// </summary>
        /// <param name="email">El texto del email a evaluar.</param>
        /// <returns>Un objeto Reservas si todo es correcto o nulo si no se pudo evaluar.</returns>
        public static Reservas AnalizarEmail(string email)
        {
            // Comprobar si es un cambio de reserva o       (06/sep/23 21.09)
            // si es una cancelación.
            Reservas re;

            // Si se ha copiado el texto de la página de bookings
            if (email.ToLower().Contains("booked on:"))
            {
                return AnalizarBooking(email);
            }

            re = EsCancelacion(email);
            if (re != null)
            {
                // Procesar la cancelación
                return re;
            }

            re = EsModificar(email);
            if (re != null)
            {
                // Procesar la modificación
                return re;
            }

            // Si llega aquí es que es nueva reserva.

            if (ComprobarCamposEmailGYG(email))
            {
                return null;
            }

            // Comprobar que estén los campos que deben estar.
            //if (Comprobar)
            re = new Reservas
            {
                idDistribuidor = 10,
                Agente = "MAKARENA",
                Usuario = "Makarena",
                Comision = 20,
                FechaApunte = DateTime.Today,
                BusHora = DateTime.Now.TimeOfDay,
                MedioContacto = "GetYourGuide", //KNDatos.Reservas.MedioContactoAPIWeb,
                Confirmada = true,
                //
                GYGOption = Extraer(email, "Option:"),
                GYGFechaHora = Extraer(email, "Date:"),
                GYGPrice = Extraer(email, "Price:"),
                GYGReference = Extraer(email, "Reference number:"),
                Telefono = Extraer(email, "Phone:").ValidarTextoTelefono(añadirPais: true),
                Nombre = ExtraerDespues(email, "Main customer:", 1).ToTitle(),
                GYGPais = ExtraerDespues(email, "Main customer:", 2),
                Email = ExtraerDespues(email, "Main customer:", 3),
                // Por si las notas están en varias líneas.     (25/ago/23 23.36)
                //GYGNotas = Extraer(email, "children in your group.:", "Tour language:"),
                GYGLanguage = Extraer(email, "Tour language:"),
                GYGTipo = Reservas.GYGTipos.Email,
            };

            // Las notas de alquiler estarán entre este texto:
            //please indicate their ages:
            // y Best regards,
            // Por si las notas están en varias líneas.     (25/ago/23 23.36)
            var notas = Extraer(email, "children in your group.:", "Tour language:");
            if (string.IsNullOrEmpty(notas))
            {
                notas = Extraer(email, "please indicate their ages:", "Best regards,");
            }
            re.GYGNotas = notas;

            var pax = ExtraerEntre(email, "Number of participants:", "Reference number:");
            for (int i = 0; i < pax.Count; i++)
            {
                if (string.IsNullOrEmpty(pax[i])) continue;
                int j = pax[i].IndexOf('x');
                if (j > -1)
                {
                    var s = pax[i].Substring(0, j).Trim();
                    int.TryParse(s, out int n);
                    if (pax[i].Contains("Adult"))
                    {
                        re.Adultos = n;
                    }
                    else if (pax[i].Contains("Youth"))
                    {
                        re.Niños = n;
                    }
                    else if (pax[i].Contains("Child"))
                    {
                        re.Niños2 = n;
                    }
                }
            }

            var fecGYG = re.GYGFechaHora;
            int k = re.GYGFechaHora.IndexOf("(");
            if (k > -1)
            {
                fecGYG = re.GYGFechaHora.Substring(0, k).Trim();
            }

            // Si la fecha no tiene contenido válido, devolver nulo. (22/ago/23 20.22)
            if (string.IsNullOrWhiteSpace(fecGYG))
            {
                return null;
            }

            // Date: 08 September 2023, 16:15 (04:15pm)

            var fec = DateTime.ParseExact(fecGYG, "dd MMMM yyyy, HH:mm", System.Globalization.CultureInfo.InvariantCulture);
            //var fec = DateTime.ParseExact(fecGYG, "dd MMMM yyyy, HH:mm", System.Globalization.CultureInfo.GetCultureInfo("en-US"));
            re.FechaActividad = fec.Date;
            re.HoraActividad = fec.TimeOfDay;

            // Ahora puede haber alquileres.                (05/sep/23 08.46)
            //Option: Nerja: Cliffs of Maro-Cerro Gordo Kayak Rental (AlquilerKayak 1h)
            //Option: 2-Hour Tour (Ruta Corta)
            //Option: 2-Hour Tour Low Season (RutaCortaTBaja)
            //Option: 2.5-Hour Tour (Ruta Larga)
            //Option: 2.5-Hour Tour Low Season (RutaLargaTBaja)
            //Option: Disfruta de los acantilados de Nerja-Maro-Cerro Gordo (RutaTabla)

            if (re.GYGOption.Contains("Corta"))
            {
                re.Actividad = "RUTA";
            }
            else if (re.GYGOption.Contains("Larga"))
            {
                re.Actividad = "RUTA VIP";
            }
            else if (re.GYGOption.Contains("AlquilerKayak"))
            {
                re.Actividad = "KAYAK";
            }
            else if (re.GYGOption.Contains("RutaTabla"))
            {
                re.Actividad = "RUTA TABLAS";
            }
            else
            {
                re.Actividad = "RUTA";
            }

            // Si GYGLanguage está en blanco,               (06/sep/23 21.13)
            // asignar el idioma según el código internacional o el país (GYGPais).
            // Mostrar la actividad                         (07/sep/23 08.29)
            // en vez de XXX (Live tour guide)
            if (string.IsNullOrWhiteSpace(re.GYGLanguage))
            {
                // Añadir el país según prefijo telefónico  (18/sep/23 06.46)
                var pais = KNDatos.Extensiones.PaisTelefono(re.Telefono);
                // Añadir el idioma según el país.          (18/sep/23 06.52)
                var idioma = $"{KNDatos.Extensiones.IdiomaPais(pais)} ";
                re.GYGLanguage = $"{idioma}({pais} - {re.ActividadMostrar.ToTitle()})";
            }

            return re;
        }

        /*
        Formato rutas si se coge el texto desde la página Bookings

    RutasKayak | Nerja: Cliffs of Maro-Cerro Gordo Guided Kayak Tour 
    Option: Ruta Corta | 2-Hour Tour High Season
    Sep 8, 2023 - 4:15pm
    GYG2RA2KZ692
    Pedro Pacheco dominguez
    2x Adult 1x Youth · Spanish · €97.70
    Active

    Hide details
    Request cancellation
    Contact Lead Traveler:
    Pedro Pacheco dominguez (Spain)
    +34 607 68 95 93
    customer-ewtbjjgqeoquqzyr@reply.getyourguide.com
    Number of Travelers:
    1x 
    Child (Age 4 - 6) ( €0.00 )
    1x 
    Youth (Age 7 - 15) ( €23.90 )
    2x 
    Adults (Age 16 - 99) ( €73.80 )
    Total: 4 Persons
    Language(s):
    Spanish (LANGUAGE_LIVE)
    Please provide a phone number connected to Whatsapp. Please also provide the ages of all children in your group.:
    Una niña de 12 años, y otra de 5 años número de teléfono 607689593
    Tickets & supply partner reference numbers:
    2x Adult

    DSXS48REY0YDMAZU3T0PMQKNHW6JK7KR-XCER0
    DSXS48REY0YDMAZU3T0PMQKNHW6JK7KR-CFZG0
    1x Youth

    DSXS48REY0YDMAZU3T0PMQKNHW6JK7KR-WH7BG
    Edit
    Booked on:
    Aug 20, 2023
        */

        /*
Formato Alquiler desde la página de Bookings
        
AlquilerKayak 2pax | Nerja: Cliffs of Maro-Cerro Gordo Kayak Rental for 2 pax 
Option: AlquilerKayak 1h | Nerja: Cliffs of Maro-Cerro Gordo Kayak Rental for 2 persons
Sep 25, 2023 - 4:00pm
GYG998GKNNZN
Guillermo Som Cerezo
2x Adult · €30.00
Active

Hide details
Request cancellation
Contact Lead Traveler:
Guillermo Som Cerezo (Spain)
+34651940855
customer-rq44z7oygij7mri5@reply.getyourguide.com
Number of Travelers:
2x 
Adults (Age 7 - 99) ( €30.00 )
1x 
Child (Age 4 - 6) ( €0.00 )
Total: 3 Persons
Tickets & supply partner reference numbers:
2x Adult

UQA3A80GTRIQ0MDR6UBY8RK6NYW1CV5P-A7D4A
UQA3A80GTRIQ0MDR6UBY8RK6NYW1CV5P-QSOA6
Edit
Booked on:
Sep 5, 2023
        */

        /// <summary>
        /// Analizar la reserva si se coge el texto desde la página Bookings.
        /// </summary>
        /// <param name="email">El texto a analizar</param>
        /// <returns>Un objeto Reservas si todo es correcto o nulo si no se pudo evaluar.</returns>
        /// <remarks>Este texto contiene Booked on:</remarks>
        public static Reservas AnalizarBooking(string email)
        {
            // Si no contiene Booked on: no es válido.
            if (email.ToLower().Contains("booked on:") == false)
            {
                return null;
            }
            // Option: AlquilerKayak 1h | Nerja: Cliffs of Maro-Cerro Gordo Kayak Rental for 2 persons
            // Sep 25, 2023 - 4:00pm
            // GYG998GKNNZN
            // Guillermo Som Cerezo
            // 2x Adult · €30.00
            // 2x Adult 1x Youth · Spanish · €97.70
            string opGYG = Extraer(email, "Option:");
            string fechaHoraGYG = ExtraerDespues(email, "Option:", 1);
            string refGYG = ExtraerDespues(email, "Option:", 2);
            string paxGYG = ExtraerDespues(email, "Option:", 4);
            string priceGYG = "€15.00";
            int i, j, k, n;
            n = paxGYG.IndexOf("·");
            if (n > -1)
            {
                priceGYG = paxGYG.Substring(n + 1).Trim();
                paxGYG = paxGYG.Substring(0, n).Trim();
            }
            //...
            // Contact Lead Traveler:
            // Guillermo Som Cerezo (Spain)
            // Pedro Pacheco dominguez (Spain)
            // +34651940855
            // customer-rq44z7oygij7mri5@reply.getyourguide.com
            string nombreGYG = ExtraerDespues(email, "Contact Lead Traveler:", 1);
            string telefonoGYG = ExtraerDespues(email, "Contact Lead Traveler:", 2);
            string emailGYG = ExtraerDespues(email, "Contact Lead Traveler:", 3);
            string paisGYG = "";
            // El nombre siempre lleva después del país entre paréntesis.
            n = nombreGYG.IndexOf("(");
            if (n > -1)
            {
                paisGYG = nombreGYG.Substring(n + 1).Trim();
                nombreGYG = nombreGYG.Substring(0, n).Trim();
            }
            string languageGYG, notasGYG;
            // Si es alquiler no se indica el idioma ni las notas,
            // si es ruta sí.
            if (opGYG.Contains("AlquilerKayak"))
            {
                notasGYG = "";
                languageGYG = paisGYG;
            }
            else
            {
                //Language(s):
                //Spanish (LANGUAGE_LIVE)
                languageGYG = ExtraerDespues(email, "Language(s):", 1);
                //Please provide a phone number connected to Whatsapp. Please also provide the ages of all children in your group.:
                //Una niña de 12 años, y otra de 5 años número de teléfono 607689593
                notasGYG = ExtraerDespues(email, "children in your group.:", 1);
            }

            // Number of Travelers:
            // 2x 
            // Adults (Age 7 - 99) ( €30.00 )
            // 1x 
            // Child (Age 4 - 6) ( €0.00 )
            // Total: 3 Persons

            Reservas re = new Reservas
            {
                idDistribuidor = 10,
                Agente = "MAKARENA",
                Usuario = "Makarena",
                Comision = 20,
                FechaApunte = DateTime.Today,
                BusHora = DateTime.Now.TimeOfDay,
                MedioContacto = "GetYourGuide", //KNDatos.Reservas.MedioContactoAPIWeb,
                Confirmada = true,
                //
                GYGOption = opGYG,
                GYGFechaHora = fechaHoraGYG,
                GYGPrice = priceGYG,
                GYGReference = refGYG,
                Telefono = telefonoGYG.ValidarTextoTelefono(añadirPais: true),
                Nombre = nombreGYG.ToTitle(),
                GYGPais = ExtraerDespues(email, "Main customer:", 2),
                Email = emailGYG,
                GYGNotas = notasGYG,
                GYGLanguage = languageGYG,
                GYGTipo = Reservas.GYGTipos.Booking,
            };

            // Usar lo que haya en paxGYG                   (05/sep/23 10.18)
            // 2x Adult · €30.00
            // 2x Adult 1x Youth · Spanish · €97.70
            i = paxGYG.IndexOf("x Adult");
            n = 0;
            if (i > -1)
            {
                j = i - 2;
                if (j < 0) j = 0;
                n = paxGYG.Substring(j, i - j).AsInteger();
            }
            re.Adultos = n;
            i = paxGYG.IndexOf("x Youth");
            n = 0;
            if (i > -1)
            {
                j = i - 2;
                if (j < 0) j = 0;
                n = paxGYG.Substring(j, i - j).AsInteger();
            }
            re.Niños = n;

            // Comprobar si hay menores de 7
            // Number of Travelers:
            // 2x 
            // Adults (Age 7 - 99) ( €30.00 )
            // 1x 
            // Child (Age 4 - 6) ( €0.00 )
            i = email.IndexOf("Child");
            n = 0;
            if (i > -1)
            {
                j = i - 6;
                if (j < 0) j = 0;
                k = email.IndexOf("x", j);
                if (k > -1)
                {
                    n = paxGYG.Substring(j, k - j).AsInteger();
                }
            }
            re.Niños2 = n;

            // El formato de fechaHora es: MMM d, yyyy - H:mm
            // Sep 25, 2023 - 4:00pm
            // Sep 8, 2023 - 4:15pm
            var fecGYG = re.GYGFechaHora;

            string fecD = "01/01/23", fecH = "00:00";
            i = fecGYG.LastIndexOf("-");
            if (i > -1)
            {
                fecD = fecGYG.Substring(0, i).Trim();
                fecH = fecGYG.Substring(i + 1).Trim();
            }
            re.FechaActividad = DateTime.Parse(fecD);
            re.HoraActividad = DateTime.Parse(fecH).TimeOfDay;

            // Ahora puede haber alquileres.                (05/sep/23 08.36)
            //Option: Nerja: Cliffs of Maro-Cerro Gordo Kayak Rental (AlquilerKayak 1h)
            //Option: 2-Hour Tour Low Season (RutaCortaTBaja)
            //Option: 2.5-Hour Tour Low Season (RutaLargaTBaja)
            //Option: Disfruta de los acantilados de Nerja-Maro-Cerro Gordo (RutaTabla)

            if (re.GYGOption.Contains("Corta"))
            {
                re.Actividad = "RUTA";
            }
            else if (re.GYGOption.Contains("Larga"))
            {
                re.Actividad = "RUTA VIP";
            }
            else if (re.GYGOption.Contains("AlquilerKayak"))
            {
                re.Actividad = "KAYAK";
            }
            else if (re.GYGOption.Contains("RutaTabla"))
            {
                re.Actividad = "RUTA TABLAS";
            }
            else
            {
                re.Actividad = "RUTA";
            }

            return re;
        }

        /// <summary>
        /// Enviar un mensaje al cliente con los datos de la reserva.
        /// </summary>
        /// <param name="para">El destinatario del correo.</param>
        /// <param name="asunto">El asunto del mensaje.</param>
        /// <param name="bodyMensaje">El cuerpo del mensaje.</param>
        /// <param name="esHtml">True si es en formato HTML.</param>
        /// <returns>Un mensaje si se envió bien o empezando por ERROR si se produjo un error al enviarlo.</returns>
        public static string EnviarMensaje(string para, string asunto, string bodyMensaje, bool esHtml)
        {
            var user = "reservas@kayakmakarena.com";

            // Enviar un mensaje con las clases de .NET Framework 2.0
            using (var unMail = new System.Net.Mail.MailMessage(user, para))
            {
                unMail.IsBodyHtml = esHtml;
                unMail.Priority = System.Net.Mail.MailPriority.Normal;
                unMail.BodyEncoding = System.Text.Encoding.Default;
                unMail.Body = bodyMensaje;
                unMail.Subject = asunto;

                // Mandar copia oculta a gmail.             (30/abr/23 07.10)
                unMail.Bcc.Add("kayak.makarena@gmail.com");

                /*
                            <smtp deliveryMethod="Network" from="reservas@kayakmakarena.com">
                                <network host="smtp.servidor-correo.net"
                                         port="587"
                                         defaultCredentials="false"
                                         enableSsl="true"
                                         password="Net13@Riti"
                                         userName="reservas@kayakmakarena.com" />
                            </smtp>
                */

                var smtp = new System.Net.Mail.SmtpClient("smtp.servidor-correo.net", 587);
                smtp.EnableSsl = true;
                // Nuevo password email.                    (23/mar/24 15.58)
                //NetworkCredential netCre = new NetworkCredential("reservas@kayakmakarena.com", "Net13@Riti");
                NetworkCredential netCre = new NetworkCredential("reservas@kayakmakarena.com", "Net13@rIti");
                smtp.Credentials = netCre;

                try
                {
                    smtp.Send(unMail);
                }
                catch (Exception ex)
                {
                    return "ERROR: " + ex.Message;
                }
            }
            return "Mensaje enviado correctamente a '" + para + "'";
        }

        /// <summary>
        /// Enviar un mensaje a todos los correos indicados en para.
        /// </summary>
        /// <param name="para">Una colección con todos los correos a enviar en el CCO.</param>
        /// <param name="asunto">El asunto del mensaje.</param>
        /// <param name="bodyMensaje">El cuerpo del mensaje.</param>
        /// <param name="esHtml">True si es en formato HTML.</param>
        /// <returns>Un mensaje si se envió bien o empezando por ERROR si se produjo un error al enviarlo.</returns>
        public static string EnviarMensaje(List<string> para, string asunto, string bodyMensaje, bool esHtml)
        {
            var user = "reservas@kayakmakarena.com";

            // Enviar un mensaje con las clases de .NET Framework 2.0
            using (var unMail = new System.Net.Mail.MailMessage(user, user))
            {
                unMail.IsBodyHtml = esHtml;
                unMail.Priority = System.Net.Mail.MailPriority.Normal;
                unMail.BodyEncoding = System.Text.Encoding.Default;
                unMail.Body = bodyMensaje;
                unMail.Subject = asunto;

                // Mandar los mensajes con copia oculta.
                foreach (var re in para)
                {
                    unMail.Bcc.Add(re);
                }

                var smtp = new System.Net.Mail.SmtpClient("smtp.servidor-correo.net", 587);
                smtp.EnableSsl = true;
                // Nuevo password                           (23/mar/24 15.57)
                //NetworkCredential netCre = new NetworkCredential("reservas@kayakmakarena.com", "Net13@Riti");
                NetworkCredential netCre = new NetworkCredential("reservas@kayakmakarena.com", "Net13@rIti");
                smtp.Credentials = netCre;

                try
                {
                    smtp.Send(unMail);
                }
                catch (Exception ex)
                {
                    return "ERROR: " + ex.Message;
                }
            }
            // En realidad se debe contar uno menos,        (31/ago/23 01.43)
            // ya que se añade el correo de kayak.makarena@gmail.com
            // Ahora no se añade el de gmail.                   (16/jul/24 20.38)
            int enBcc = para.Count; // - 1;
            //return $"Mensajes enviados correctamente a {para.Count} {para.Count.Plural("reserva")}.";
            return $"Mensajes enviados correctamente a {enBcc} {enBcc.Plural("reserva")}.";
        }


        // Extraer las horas del texto en TxtFotosDia.      (23/ago/23 11.57)

        // Faltaba la ruta de las 15:00                     (15/sep/23 23.10)
        // Añado algunas horas más                          (26/sep/23 18.09)
        // Que haya en todas las .00 y .30                  (19/abr/24 14.04)

        /// <summary>
        /// Las Horas en que hay rutas, usando el separador punto.
        /// </summary>
        public static List<string> LasHoras { get; set; } = new List<string>()
                { "09.00", "09.30", "10.00", "10.30", "11.00", "11.05", "11.30", "11.45", "12.00", "12.30", "13.00", "13.15", "13.30",
                  "14.00", "14.30", "15.00", "15.30", "16.00", "16.15", "16.30", "17.00", "17.30", "17.45", "18.00", "18.30" };

        //{ "09.30", "10.30", "11.00", "11.05", "11.45", "13.15", "13.30", "14.00", "15.30", "16.15", "16.30", "17.45", "18.30" };


        /// <summary>
        /// Extrae los textos de las fotos de las horas.
        /// </summary>
        /// <param name="texto">El texto con todas las fotos del día.</param>
        /// <returns>Una lista con los textos a enviar en cada hora.</returns>
        public static List<string> AnalizarTextoFotos(string texto)
        {
            List<string> col = new List<string>();

            // Extraer HH.mm desde ¡Hola! hasta ¡Muchas gracias!
            for (int i = 0; i < LasHoras.Count; i++)
            {
                var res = ExtraerEntre(texto, "Te paso el enlace a las fotos de la ruta:", "¡Muchas gracias!", LasHoras[i]);

                if (string.IsNullOrEmpty(res) == false)
                {
                    col.Add($"¡Hola!{CrLf}Te paso el enlace a las fotos de la ruta:{CrLf}{res}{CrLf}¡Muchas gracias!{CrLf}");
                }
                else
                {
                    // Si no está esa hora, añadir una cadena vacía. (23/ago/23 22.26)
                    col.Add("");
                }
            }

            return col;
        }

        /// <summary>
        /// Extraer la fecha para las fotos. El formato será 'Fotos rutas dd-mm-yyyy'
        /// </summary>
        /// <param name="texto">El texto a analizar.</param>
        /// <returns>La cadena con la fecha después de 'Fotos rutas '</returns>
        public static string FechaFotos(string texto)
        {
            // Buscar el texto "Fotos rutas" y lo que sigue es la fecha

            return Extraer(texto, "Fotos rutas");
        }

        /*
    Fotos rutas 18-08-2023


    ¡Hola!
    Te paso el enlace a las fotos de la ruta:

    Ruta Viernes 18.08.23 09.30h - Juanda
    https://photos.app.goo.gl/vsZqGK5VTVRKMgrP9

    Ruta Viernes 18.08.23 09.30h - Timmy
    https://photos.app.goo.gl/1hqzqiSTt8QtFLXf8

    ¡Muchas gracias!


    ¡Hola!
    Te paso el enlace a las fotos de la ruta:

    Ruta Viernes 18.08.23 10.30h - Mery
    https://photos.app.goo.gl/Hb6bUbyM3LV8s9s88

    ¡Muchas gracias!


    ¡Hola!
    Te paso el enlace a las fotos de la ruta:

    Ruta Viernes 18.08.23 11.00h - Perico
    https://photos.app.goo.gl/vxBeKQv5sesnCFv59

    ¡Muchas gracias!


    ¡Hola!
    Te paso el enlace a las fotos de la ruta:

    Ruta Viernes 18.08.23 11.45h - Joselu y Timmy
    https://photos.app.goo.gl/CbqH9BRxYmfU1UBk6

    ¡Muchas gracias!


    ¡Hola!
    Te paso el enlace a las fotos de la ruta:

    Ruta Viernes 18.08.23 13.15h - Juanda
    https://photos.app.goo.gl/Kj4eRWt4P9af1cJH8

    Ruta Viernes 18.08.23 13.15h - Paco
    https://photos.app.goo.gl/Lq5C6QU4v5ByD1FR6

    ¡Muchas gracias!


    ¡Hola!
    Te paso el enlace a las fotos de la ruta:

    Ruta Viernes 18.08.23 15.30h - Mery
    https://photos.app.goo.gl/cEnH2VYshf2GG8Hk8

    Ruta Viernes 18.08.23 15.30h - Paco
    https://photos.app.goo.gl/SZKCyG2hkqEgyPvN7

    ¡Muchas gracias!


    ¡Hola!
    Te paso el enlace a las fotos de la ruta:

    Ruta Viernes 18.08.23 16.15h - Joselu
    https://photos.app.goo.gl/uXkPzWrb5VTEd9Ua6

    Ruta Viernes 18.08.23 16.15h - Timmy
    https://photos.app.goo.gl/F5NFbFoyEbzYWMX17

    ¡Muchas gracias!


    ¡Hola!
    Te paso el enlace a las fotos de la ruta:

    Ruta Viernes 18.08.23 16.30h - Juanda
    https://photos.app.goo.gl/YXusGShNEu2KfmAe8

    ¡Muchas gracias!


    ¡Hola!
    Te paso el enlace a las fotos de la ruta:

    Ruta Viernes 18.08.23 17.45h - ⁬Michelle
    https://photos.app.goo.gl/31deS4WW8g1b6k5Q8

    Ruta Viernes 18.08.23 17.45h - Perico
    https://photos.app.goo.gl/qqxWBkVthdBGMjFEA

    ¡Muchas gracias!
        */

        ///// <summary>
        ///// Comprueba si las reservas de la fecha indicada tienen todas el email.
        ///// </summary>
        ///// <param name="fecha">Fecha con las reservas a comprobar.</param>
        ///// <param name="conCabecera">True para añadir la cabecera con las reservas sin email.</param>
        ///// <returns>Una cadena vacía si todo está bien, si no, con los datos de las reservas que no tienen email.</returns>
        //[Obsolete("No usar este método, usar el que devuelve la colección")]
        //public static string ComprobarEmails(DateTime fecha, bool conAlquileres, bool conCabecera)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    sb.Append("Select * from Reservas ");
        //    sb.Append("where Activa = 1 and CanceladaCliente = 0 and idDistribuidor = 10 ");
        //    sb.Append("and Email = '' and Nombre != 'Makarena (GYG)' ");
        //    // Solo las rutas                               (02/sep/23 13.52)
        //    //sb.Append($"and Actividad like 'ruta%' ");
        //    // también los alquileres...                    (05/sep/23 23.47)
        //    // Ahora como parámetro.                        (05/sep/23 23.58)
        //    if (conAlquileres == false)
        //    {
        //        sb.Append($"and Actividad like 'ruta%' ");
        //    }

        //    sb.Append($"and FechaActividad = '{fecha:yyyy-MM-dd}' ");
        //    sb.Append("order by FechaActividad, HoraActividad, ID");

        //    var colRes = Reservas.TablaCol(sb.ToString());

        //    sb.Clear();

        //    if (colRes.Count > 0)
        //    {
        //        if (conCabecera)
        //        {
        //            sb.AppendLine($"Hay {colRes.Count} {colRes.Count.Plural("reserva")} del {fecha:dddd dd/MM/yyyy} sin email.");
        //        }
        //        for (int i = 0; i < colRes.Count; i++)
        //        {
        //            string nota = "";
        //            // Poner solo los xx primeros caracteres de las notas. (25/ago/23 14.18)
        //            //GetYourGuide - GYGLMWA85MXQ - (Spain) - Spanish (Live tour guide) -
        //            //sb.AppendLine($"{colRes[i].Nombre}, {colRes[i].Notas}");
        //            if (colRes[i].Notas.StartsWith("GetYourGuide"))
        //            {
        //                int j = colRes[i].Notas.IndexOf("- GYG");
        //                if (j > -1)
        //                {
        //                    nota = colRes[i].Notas.Substring(j + 2, 12);
        //                }
        //            }
        //            else
        //            {
        //                nota = colRes[i].Notas;
        //            }
        //            if (string.IsNullOrEmpty(nota))
        //            {
        //                if (string.IsNullOrEmpty(colRes[i].Notas) == false)
        //                {
        //                    nota = colRes[i].Notas.Substring(0, 28);
        //                }
        //            }
        //            sb.AppendLine($"{colRes[i].Nombre}, {nota}");
        //        }
        //    }
        //    return sb.ToString();
        //}

        /// <summary>
        /// Comprueba las reservas sin email de la fecha indicada.
        /// </summary>
        /// <param name="fecha">Fecha con las reservas a comprobar.</param>
        /// <returns>Una colección con las reservas que no tienen email o una colección vacía si todas tienen email.</returns>
        public static List<ReservasGYG> ComprobarEmails(DateTime fecha, bool conAlquileres)
        {
            return ComprobarEmails(fecha, new TimeSpan(0, 0, 0), conAlquileres);
        }

        /// <summary>
        /// Comprueba si hay reservas de rutas sin email en la fecha y hora indicada.
        /// </summary>
        /// <param name="fecha">La fecha de las reservas a comprobar.</param>
        /// <param name="hora">La hora de las reservas a comprobar, si la hora es cero, no se comprueba la hora.</param>
        /// <returns>Una colección con los datos de las reservas sin email.</returns>
        public static List<ReservasGYG> ComprobarEmails(DateTime fecha, TimeSpan hora, bool conAlquileres)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Select * from Reservas ");
            sb.Append("where Activa = 1 and CanceladaCliente = 0 and idDistribuidor = 10 ");
            sb.Append("and Email = '' and Nombre != 'Makarena (GYG)' ");
            // Solo las rutas                               (02/sep/23 13.54)
            //sb.Append($"and Actividad like 'ruta%' ");
            // también los alquileres...                    (05/sep/23 23.47)
            // Ahora como parámetro.                        (05/sep/23 23.58)
            if (conAlquileres == false)
            {
                sb.Append($"and Actividad like 'ruta%' ");
            }

            sb.Append($"and FechaActividad = '{fecha:yyyy-MM-dd}' ");
            if (hora.Hours > 0)
            {
                sb.Append($"and HoraActividad = '{hora:hh\\:mm}' ");
            }
            sb.Append("order by FechaActividad, HoraActividad, ID");

            var colRes = Reservas.TablaCol(sb.ToString());

            List<ReservasGYG> col = new();

            if (colRes.Count > 0)
            {
                for (int i = 0; i < colRes.Count; i++)
                {
                    col.Add(new ReservasGYG(colRes[i]));
                }
            }
            return col;
        }

        /// <summary>
        /// Devuelve una lista de las reservas de la fecha y hora indicadas tengan o no email.
        /// </summary>
        /// <param name="fecha">La fecha de las reservas a comprobar.</param>
        /// <param name="hora">La hora de las reservas a comprobar, si la hora es cero, no se comprueba la hora.</param>
        /// <param name="conAlquileres">Incluir los alquileres al comprobar las reservas.</param>
        /// <param name="conCanceladas">Incluir las reservas canceladas.</param>
        /// <returns>Una colección con las reservas de la fecha y hora indicada. Si no se indica la hora, todas las de la fecha.</returns>
        public static List<ReservasGYG> DatosReservas(DateTime fecha, TimeSpan hora, bool conAlquileres, bool conCanceladas)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Select * from Reservas ");
            sb.Append("where Activa = 1 and idDistribuidor = 10 ");
            // Que estén confirmadas                        (02/sep/23 20.55)
            sb.Append("and Confirmada = 1 ");
            // Incluir también las canceladas               (10/sep/23 02.01)
            if (conCanceladas == false)
            {
                sb.Append("and CanceladaCliente = 0 ");
            }
            sb.Append("and Nombre != 'Makarena (GYG)' ");
            // también los alquileres...                    (05/sep/23 23.47)
            // Ahora como parámetro.                        (05/sep/23 23.58)
            if (conAlquileres == false)
            {
                sb.Append($"and Actividad like 'ruta%' ");
            }

            sb.Append($"and FechaActividad = '{fecha:yyyy-MM-dd}' ");
            if (hora.Hours > 0)
            {
                sb.Append($"and HoraActividad = '{hora:hh\\:mm}' ");
            }
            sb.Append("order by FechaActividad, HoraActividad, ID");

            var colRes = Reservas.TablaCol(sb.ToString());

            List<ReservasGYG> col = new();

            if (colRes.Count > 0)
            {
                for (int i = 0; i < colRes.Count; i++)
                {
                    col.Add(new ReservasGYG(colRes[i]));
                }
            }
            return col;
        }

        /// <summary>
        /// Buscar solo las reservas canceladas.
        /// </summary>
        /// <param name="fecha"></param>
        /// <param name="hora"></param>
        /// <returns></returns>
        public static List<ReservasGYG> DatosReservasCanceladas(DateTime fecha, TimeSpan hora, bool conAlquileres)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Select * from Reservas ");
            sb.Append("where Activa = 1 and idDistribuidor = 10 ");
            // Que estén confirmadas                        (02/sep/23 20.55)
            sb.Append("and Confirmada = 1 ");
            // Y que estén canceladas.                      (10/sep/23 02.02)
            sb.Append("and CanceladaCliente = 1 ");
            sb.Append("and Nombre != 'Makarena (GYG)' ");
            // también los alquileres...                    (05/sep/23 23.47)
            // Ahora como parámetro.                        (05/sep/23 23.58)
            if (conAlquileres == false)
            {
                sb.Append($"and Actividad like 'ruta%' ");
            }

            sb.Append($"and FechaActividad = '{fecha:yyyy-MM-dd}' ");
            if (hora.Hours > 0)
            {
                sb.Append($"and HoraActividad = '{hora:hh\\:mm}' ");
            }
            sb.Append("order by FechaActividad, HoraActividad, ID");

            var colRes = Reservas.TablaCol(sb.ToString());

            List<ReservasGYG> col = new();

            if (colRes.Count > 0)
            {
                for (int i = 0; i < colRes.Count; i++)
                {
                    col.Add(new ReservasGYG(colRes[i]));
                }
            }
            return col;
        }

        /// <summary>
        /// Reservas sin salida en la fecha indicada.
        /// </summary>
        /// <param name="fecha">La fecha de las reservas a comprobar.</param>
        /// <param name="conCanceladas">Si se tienen en cuenta las canceladas.</param>
        /// <param name="conAlquileres">Si se deben incluir los alquileres o no.</param>
        /// <returns>Una colección con las reservas sin salida de la fecha indicada.</returns>
        /// <remarks>Aquí no se mira si es con o sin alquileres.</remarks>
        public static List<ReservasGYG> ReservasSinSalida(DateTime fecha, bool conCanceladas, bool conAlquileres)
        {
            StringBuilder sb = new StringBuilder();
            //
            sb.Append("Select * from Reservas ");
            sb.Append("where Activa = 1 and idDistribuidor = 10 ");
            // Que estén confirmadas                        (02/sep/23 20.55)
            sb.Append("and Confirmada = 1 ");
            // Incluir también las canceladas               (10/sep/23 02.01)
            if (conCanceladas == false)
            {
                sb.Append("and CanceladaCliente = 0 ");
            }

            // Si se deben incluir los alquileres               (30/jul/24 14.16)
            if (conAlquileres == false)
            {
                sb.Append($"and Actividad like 'ruta%' ");
            }

            //
            //sb.Append("Select * from Reservas ");
            //sb.Append("where Activa = 1 and CanceladaCliente = 0 and idDistribuidor = 10 ");
            sb.Append("and Nombre != 'Makarena (GYG)' ");
            sb.Append("and Control = 0 ");
            sb.Append($"and FechaActividad = '{fecha:yyyy-MM-dd}' ");
            sb.Append("order by FechaActividad, HoraActividad, CanceladaCliente, ID");

            var colRes = Reservas.TablaCol(sb.ToString());

            List<ApiReservasMailGYG.ReservasGYG> col = new();

            if (colRes.Count > 0)
            {
                for (int i = 0; i < colRes.Count; i++)
                {
                    col.Add(new ApiReservasMailGYG.ReservasGYG(colRes[i]));
                }
            }
            return col;
        }

        // Se usa el de Extensiones en KNDatos.

        //// El nombre del país según el código internacional del teléfono. (06/sep/23 23.35)

        ///// <summary>
        ///// Averiguar el país según el código internacional del teléfono.
        ///// </summary>
        ///// <param name="telefono">El número de teléfono con el código internacional.</param>
        ///// <returns>El nombre del país correspondiente al código internacional.</returns>
        //public static string PaisTelefono(string telefono)
        //{
        //    if (string.IsNullOrWhiteSpace(telefono)) { return ""; }

        //    string elPais;

        //    if (telefono.StartsWith("+1"))
        //        elPais = "United States/Canada";
        //    else if (telefono.StartsWith("+7"))
        //        elPais = "Russia";
        //    else if (telefono.StartsWith("+30"))
        //        elPais = "Greece";
        //    else if (telefono.StartsWith("+31"))
        //        elPais = "Netherlands";
        //    else if (telefono.StartsWith("+32"))
        //        elPais = "Belgium";
        //    else if (telefono.StartsWith("+33"))
        //        elPais = "France";
        //    else if (telefono.StartsWith("+34"))
        //        elPais = "Spain";
        //    else if (telefono.StartsWith("+36"))
        //        elPais = "Hungary";
        //    else if (telefono.StartsWith("+39"))
        //        elPais = "Italy";
        //    else if (telefono.StartsWith("+40"))
        //        elPais = "Romania";
        //    else if (telefono.StartsWith("+41"))
        //        elPais = "Switzerland";
        //    else if (telefono.StartsWith("+43"))
        //        elPais = "Austria";
        //    else if (telefono.StartsWith("+44"))
        //        elPais = "United Kingdom";
        //    else if (telefono.StartsWith("+45"))
        //        elPais = "Denmark";
        //    else if (telefono.StartsWith("+46"))
        //        elPais = "Sweden";
        //    else if (telefono.StartsWith("+47"))
        //        elPais = "Norway";
        //    else if (telefono.StartsWith("+48"))
        //        elPais = "Poland";
        //    else if (telefono.StartsWith("+49"))
        //        elPais = "Germany";
        //    else if (telefono.StartsWith("+52"))
        //        elPais = "Mexico";
        //    else if (telefono.StartsWith("+54"))
        //        elPais = "Argentina";
        //    else if (telefono.StartsWith("+61"))
        //        elPais = "Australia";
        //    else if (telefono.StartsWith("+81"))
        //        elPais = "Japan";
        //    else if (telefono.StartsWith("+212"))
        //        elPais = "Morocco";
        //    else if (telefono.StartsWith("+350"))
        //        elPais = "Gibraltar";
        //    else if (telefono.StartsWith("+351"))
        //        elPais = "Portugal";
        //    else if (telefono.StartsWith("+352"))
        //        elPais = "Luxembourg";
        //    else if (telefono.StartsWith("+353"))
        //        elPais = "Ireland";
        //    else if (telefono.StartsWith("+354"))
        //        elPais = "Iceland";
        //    else if (telefono.StartsWith("+355"))
        //        elPais = "Albania";
        //    else if (telefono.StartsWith("+356"))
        //        elPais = "Malta";
        //    else if (telefono.StartsWith("+357"))
        //        elPais = "Cyprus";
        //    else if (telefono.StartsWith("+358"))
        //        elPais = "Findland";
        //    else if (telefono.StartsWith("+359"))
        //        elPais = "Bulgaria";
        //    else if (telefono.StartsWith("+370"))
        //        elPais = "Lithuania";
        //    else if (telefono.StartsWith("+371"))
        //        elPais = "Latvia";
        //    else if (telefono.StartsWith("+372"))
        //        elPais = "Estonia";
        //    else if (telefono.StartsWith("+373"))
        //        elPais = "Moldova";
        //    else if (telefono.StartsWith("+374"))
        //        elPais = "Armenia";
        //    else if (telefono.StartsWith("+375"))
        //        elPais = "Belarus";
        //    else if (telefono.StartsWith("+376"))
        //        elPais = "Andorra";
        //    else if (telefono.StartsWith("+377"))
        //        elPais = "Monaco";
        //    else if (telefono.StartsWith("+378"))
        //        elPais = "San Marino";
        //    else if (telefono.StartsWith("+379"))
        //        elPais = "Vatican City";
        //    else if (telefono.StartsWith("+380"))
        //        elPais = "Ukraine";
        //    else if (telefono.StartsWith("+381"))
        //        elPais = "Serbia";
        //    else if (telefono.StartsWith("+382"))
        //        elPais = "Montenegro";
        //    else if (telefono.StartsWith("+383"))
        //        elPais = "Kosovo";
        //    else if (telefono.StartsWith("+385"))
        //        elPais = "Croatia";
        //    else if (telefono.StartsWith("+386"))
        //        elPais = "Slovenia";
        //    else if (telefono.StartsWith("+387"))
        //        elPais = "Bosnia and Herzegovina";
        //    else if (telefono.StartsWith("+389"))
        //        elPais = "North Macedonia";
        //    else if (telefono.StartsWith("+420"))
        //        elPais = "Czechia";
        //    else if (telefono.StartsWith("+421"))
        //        elPais = "Slovakia";
        //    else if (telefono.StartsWith("+423"))
        //        elPais = "Liechtenstein";
        //    else if (telefono.StartsWith("+82"))
        //        elPais = "South Korea";
        //    else if (telefono.StartsWith("+852"))
        //        elPais = "Hong Kong";
        //    else if (telefono.StartsWith("+965"))
        //        elPais = "Kuwait";
        //    else if (telefono.StartsWith("+966"))
        //        elPais = "Saudi Arabia";
        //    else if (telefono.StartsWith("+972"))
        //        elPais = "Israel";
        //    else if (telefono.StartsWith("+974"))
        //        elPais = "Qatar";
        //    else
        //        elPais = "";

        //    return elPais;
        //}

        /// <summary>
        /// Añadir al StringBuilder indicado la firma de Kayak Makarena.
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="enIngles"></param>
        public static void FirmaMakarena(StringBuilder sb, bool enIngles)
        {
            sb.AppendLine("<br/>");
            sb.AppendLine("<br/>");
            // No tenía los cambios de línea, añado el teléfono (08/sep/23 13.55)
            sb.AppendLine("Kayak Makarena<br/>");
            // Quito lo de iMessage                             (06/ago/24 19.39)
            sb.Append("WhatsApp: +34 645 76 16 89 ");
            //sb.Append("iMessage / WhatsApp: +34 645 76 16 89 ");
            // Quito lo de las llamadas por wasap.              (05/jul/24 10.42)
            if (enIngles)
            {
                //sb.Append("<small>(Please, only WhatsApp messages or calls as I usually don't have phone coverage)</small>");
                sb.Append("<small>(Please, only WhatsApp messages as I usually don't have phone coverage)</small>");
            }
            else
            {
                //sb.Append("<small>(Por favor, solo mensajes o llamadas por wasap ya que no suelo tener cobertura)</small>");
                sb.Append("<small>(Por favor, solo mensajes por wasap ya que no suelo tener cobertura)</small>");
            }
            sb.AppendLine("<br/>");
            sb.AppendLine("https://kayakmakarena.com<br/>");
        }
    }
}