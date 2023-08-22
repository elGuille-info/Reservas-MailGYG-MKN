//
// Analizar los correos de GetYourGuide con nuevas reservas (12/ago/23 17.55)
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
//using MimeKit.Text;

namespace ApiReservasMailGYG;

public class MailGYG
{

    public static List<string> CorreosRecibidos(DateTime fecha1, DateTime fecha2, bool soloNuevos)
    {
        List<string> col = new List<string>();

        return col;
    }

    // Extraer el texto entre lo indicado en campo1 y hasta campo2, devolverlo como array
    private static List<string> ExtraerEntre(string texto, string campo1, string campo2)
    {
        List<string> filas = new List<string>();

        int i = texto.IndexOf(campo1, StringComparison.OrdinalIgnoreCase);
        if (i > -1)
        {
            int j = texto.IndexOf(campo2, i + campo1.Length);
            if (j > -1)
            {
                string res = texto.Substring(i + campo1.Length, j - i - campo1.Length);
                var lineas = res.Split("\n", StringSplitOptions.RemoveEmptyEntries);
                filas.AddRange(lineas.ToList());
            }
        }
        return filas;
    }

    /// <summary>
    /// Extraer del texto indicado lo que haya en la lineas línea posterior al campo indicado.
    /// </summary>
    /// <param name="texto"></param>
    /// <param name="campo"></param>
    /// <param name="lineas"></param>
    /// <returns></returns>
    private static string ExtraerDespues(string texto, string campo, int index)
    {
        string res = "";

        int i = texto.IndexOf(campo, StringComparison.OrdinalIgnoreCase);
        if (i > -1)
        {
            var elTexto = texto.Substring(i);
            var lineas = elTexto.Split("\n", StringSplitOptions.RemoveEmptyEntries);
            if (lineas.Length <= index) { return ""; }
            res = lineas[index];
        }
        return res.Trim();
    }

    /// <summary>
    /// Extraer del texto indicado lo que haya a continuación del campo indicado hasta final de la línea.
    /// </summary>
    /// <param name="texto"></param>
    /// <param name="campo"></param>
    /// <returns></returns>
    private static string Extraer(string texto, string campo)
    {
        string res = "";

        int i = texto.IndexOf(campo, StringComparison.OrdinalIgnoreCase);
        if (i > -1)
        {
            int j = texto.IndexOf("\n", i + campo.Length);
            if (j == -1) { j = texto.Length + 2; }
            res = texto.Substring(i + campo.Length, j - i-campo.Length);
        }
        return res.Trim();
    }

    /// <summary>
    /// Crea una reserva a partir del texto del email de GYG
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    public static Reservas AnalizarEmail(string  email) 
    {
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
            GYGOption = Extraer(email, "Option:"),
            GYGFechaHora = Extraer(email, "Date:"),
            GYGPrice = Extraer(email, "Price:"),
            GYGReference = Extraer(email, "Reference number:"),
            Telefono = Extraer(email, "Phone:").ValidarTextoTelefono(añadirPais:true),
            Nombre = ExtraerDespues(email, "Main customer:", 1),
            GYGPais = ExtraerDespues(email, "Main customer:", 2),
            Email = ExtraerDespues(email, "Main customer:", 3),
            GYGNotas = ExtraerDespues(email, "children in your group.:", 1),
            GYGLanguage = Extraer(email, "Tour language:"),
        };

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
        var fec = DateTime.ParseExact(fecGYG, "dd MMMM yyyy, HH:mm", System.Globalization.CultureInfo.InvariantCulture);
        re.FechaActividad = fec.Date;
        re.HoraActividad = fec.TimeOfDay;

        if (re.GYGOption.Contains("Corta"))
        {
            re.Actividad = "RUTA";
        }
        else if (re.GYGOption.Contains("Larga")) //Ruta 
        {
            re.Actividad = "RUTA VIP";
        }
        else
        {
            re.Actividad = "RUTA TABLAS";
        }

        /*
        El contenido del mensaje será como este:

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

        return re;
    }

    /// <summary>
    /// Enviar un mensaje al cliente con los datos de la reserva.
    /// </summary>
    /// <param name="para"></param>
    /// <param name="asunto"></param>
    /// <param name="bodyMensaje"></param>
    /// <param name="esHtml"></param>
    /// <returns></returns>
    public static string EnviarMensaje(string para, string asunto, string bodyMensaje, bool esHtml)
    {
        //SmtpSection smtp1 = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
        //var user = smtp1.From;

        //var user = new MailAddress("reservas@kayakmakarena.com");
        var user = "reservas@kayakmakarena.com";

        //if (string.IsNullOrEmpty(para))
        //    para = user;

        // Enviar un mensaje con las clases de .NET Framework 2.0
        using (var unMail = new System.Net.Mail.MailMessage(user, para))
        {
            unMail.IsBodyHtml = esHtml;
            unMail.Priority = System.Net.Mail.MailPriority.Normal;
            unMail.BodyEncoding = System.Text.Encoding.Default;
            unMail.Body = bodyMensaje;
            unMail.Subject = asunto;

            //unMail.CC.Add(user);
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
            //smtp.Credentials = CredentialCache.DefaultNetworkCredentials;
            NetworkCredential netCre = new NetworkCredential("reservas@kayakmakarena.com", "Net13@Riti");
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

    public static string EnviarMensaje(List<string> para, string asunto, string bodyMensaje, bool esHtml)
    {
        //var user = new MailAddress("reservas@kayakmakarena.com");
        var user = "reservas@kayakmakarena.com";

        // Enviar un mensaje con las clases de .NET Framework 2.0
        using (var unMail = new System.Net.Mail.MailMessage(user, user))
        {
            //unMail.From = new MailAddress(user);
            unMail.IsBodyHtml = esHtml;
            unMail.Priority = System.Net.Mail.MailPriority.Normal;
            unMail.BodyEncoding = System.Text.Encoding.Default;
            unMail.Body = bodyMensaje;
            unMail.Subject = asunto;

            // Mandar los mensajes con copia oculta.
            foreach(var re in para) 
            {
                unMail.Bcc.Add(re);
            }

            var smtp = new System.Net.Mail.SmtpClient("smtp.servidor-correo.net", 587);
            smtp.EnableSsl = true;
            //smtp.Credentials = CredentialCache.DefaultNetworkCredentials;
            NetworkCredential netCre = new NetworkCredential("reservas@kayakmakarena.com", "Net13@Riti");
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
        return $"Mensajes enviados correctamente a {para.Count} {para.Count.Plural("reserva")}.";
    }


    //public static void EnviarMensaje()
    //{
    //    MailMessage mail = new MailMessage();
    //    mail.To.Add("me@mycompany.com");
    //    mail.From = new MailAddress("you@yourcompany.com");
    //    mail.Subject = "this is a test email.";
    //    //mail.BodyFormat = MailFormat.Html;
    //    mail.IsBodyHtml = true;
    //    mail.Body = "this is my test email body.<br><b>this part is in bold</b>";
    //    //SmtpMail.SmtpServer = "localhost";  //your real server goes here
    //    //SmtpMail.Send(mail);
    //}
}