//
// Mostrar las reservas de GetYourGuide recibidas por email (12/ago/23 18.04)
//

using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

using System.Windows.Forms;
using System.Drawing;
using KNDatos;

using static ApiReservasMailGYG.MailGYG;

namespace ReservasGYG;

public partial class Form1 : Form
{
    public static Form1 Current { get; set; }

    public Form1()
    {
        InitializeComponent();
        Current = this;
    }

    private void Form1_Load(object sender, EventArgs e)
    {
        // No cargarlo.                                     (25/ago/23 14.11)

        //// Cargar el programa de analizar emails.           (24/ago/23 15.38)
        ////CargarAnalizarEmail();
        //// Hacerlo con el timer.                            (24/ago/23 15.40)
        //TimerCargarAnalizarEmail.Interval = 300;
        //TimerCargarAnalizarEmail.Enabled = true;
    }

    private void Form1_FormClosing(object sender, FormClosingEventArgs e)
    {
        // Si la cierra el usuario
        //if (e.CloseReason == CloseReason.UserClosing) 
        //{
        //    if (FormAnalizaEmail.Current != null && FormAnalizaEmail.Current.IsDisposed == false)
        //    {
        //        var ret = MessageBox.Show($"La ventana de analizar mails está abierta.{CrLf}¿Quieres cerrar todas las ventanas abiertas?", 
        //                                  "Cerrar la ventana principal", 
        //                                  MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        //        if (ret != DialogResult.Yes)
        //        {
        //            e.Cancel = true;
        //            TimerCargarAnalizarEmail.Enabled = true;
        //            return;
        //        }
        //    }
        //}
    }


    private void TimerCargarAnalizarEmail_Tick(object sender, EventArgs e)
    {
        TimerCargarAnalizarEmail.Enabled = false;
        CargarAnalizarEmail();
    }

    private void BtnAnalizarEmail_Click(object sender, EventArgs e)
    {
        CargarAnalizarEmail();
    }

    private static void CargarAnalizarEmail()
    {
        if (FormAnalizaEmail.Current == null || FormAnalizaEmail.Current.IsDisposed)
        {
            FormAnalizaEmail.Current = new FormAnalizaEmail();
        }
        FormAnalizaEmail.Current.BringToFront();
        FormAnalizaEmail.Current.Show();
        FormAnalizaEmail.Current.Focus();
    }

    private void BtnHoyEs_Click(object sender, EventArgs e)
    {
        DateTime fecha = DateTimePickerGYG.Value.Date;

        var ret = MessageBox.Show($"¿Quieres mandar el mensaje de 'Hoy es el día' a las reservas del día {fecha:F}?" + CrLf +
                                  "Pulsa SÍ para mandar el mensaje a las de esa fecha." + CrLf +
                                  $"Pulsa NO no mandar nada y seleccionar otra fecha.",
                                  "Mandar recordatorio de que hoy es el día.", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        if (ret == DialogResult.No)
        {
            DateTimePickerGYG.Focus();
            return;
        }

        // Doble comprobación                               (25/ago/23 14.12)
        // Si es el mismo día de la actividad... avisar que no se debe mandar salvo si es de madrugada
        if (DateTime.Today == fecha)
        {
            ret = MessageBox.Show($"Los mensajes de 'Hoy es el día' para las reservas del día {fecha:F}" + CrLf +
                                  "Solo se deben mandar en el mismo día si es antes de las 08:00." + CrLf +
                                  "Pulsa ACEPTAR para mandar los mensajes." + CrLf +
                                  $"Pulsa CANCELAR para no mandar nada y/o seleccionar otra fecha.",
                                  "Mandar recordatorio de que hoy es el día.", 
                                  MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (ret == DialogResult.Cancel)
            {
                DateTimePickerGYG.Focus();
                return;
            }
        }

        // Comprobar que todos tengan email.                (24/ago/23 04.41)
        string res = ComprobarEmails(fecha);

        if (string.IsNullOrWhiteSpace(res) == false)
        {
            MessageBox.Show($"Hay reservas del {fecha:dddd dd/MM/yyyy} sin emails:{CrLf}{res}",
                            "Mandar recordatorio de que hoy es el día.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        //// Enviar el mensaje a todas las reservas de HOY.
        //var res = MessageBox.Show($"¿Quieres mandar el mensaje de 'Hoy es el día' a las reservas de MAÑANA día {DateTime.Today.AddDays(1):F}" + CrLf +
        //                          "Pulsa SÍ para mandar el mensaje a las de esa fecha." + CrLf +
        //                          $"Pulsa NO para mandar el mensaje a las reservas de HOY: {DateTime.Today:F}" + CrLf +
        //                          "Pulsa CANCELAR para no mandar nada.",
        //                          "Mandar recordatorio de que hoy es el día.", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
        //if (res == DialogResult.Cancel) return;
        //fecha = DateTime.Today.AddDays(1);
        //if (res == DialogResult.No)
        //{
        //    fecha = DateTime.Today;
        //}

        var lisRes = Reservas.TablaCol($"SELECT * FROM Reservas Where idDistribuidor=10 and Activa=1 and CanceladaCliente=0 and Confirmada=1 and FechaActividad ='{fecha:yyyy-MM-dd}' ORDER By FechaActividad, HoraActividad");
        if (MessageBox.Show($"Se va a madar el mensaje a {lisRes.Count} {lisRes.Count.Plural("reserva")}",
                             "Enviar Hoy es el día", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
        {
            return;
        }

        var colPara = new List<string>();
        for (int i = 0; i < lisRes.Count; i++)
        {
            var re = lisRes[i];
            if (re == null) continue;
            // Ya se ha comprobado que todos tengan email.  (24/ago/23 04.47)
            //if (string.IsNullOrEmpty(re.Email)) continue;
            // Si no tiene @ no mandar el correo.           (24/ago/23 04.48)
            if (re.Email.Contains('@') == false) continue;
            colPara.Add(re.Email);
        }

        // Agregar el email de kayak.makarena@gmail.com     (23/ago/23 22.18)
        colPara.Add("kayak.makarena@gmail.com");

        StringBuilder sb = new StringBuilder();
        sb.Append("");
        sb.Append(Properties.Resources.Hoy_es_el_dia_txt.Replace(CrLf, "<br/>"));
        sb.Append("<br/>");
        sb.Append("<br/>");
        sb.Append("Kayak Makarena");

        string body = sb.ToString().Replace(CrLf, "<br/>");
        var msg = ApiReservasMailGYG.MailGYG.EnviarMensaje(colPara, "Hoy es el día / Today is the day", body, true);
        if (msg.StartsWith("ERROR"))
        {
            MessageBox.Show($"ERROR al enviar el email:{CrLf}{msg}.", "Error al enviar el email de la reserva", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        else
        {
            MessageBox.Show($"{msg}", "Enviar email de la reserva", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

    }

    private void BtnFotos_Click(object sender, EventArgs e)
    {
        if (FormEnviarFotos.Current == null || FormEnviarFotos.Current.IsDisposed)
        {
            FormEnviarFotos.Current = new FormEnviarFotos();
        }
        FormEnviarFotos.Current.BringToFront();
        FormEnviarFotos.Current.Show();
        FormEnviarFotos.Current.Focus();
    }

    private void BtnComprobarSinMail_Click(object sender, EventArgs e)
    {
        // Comprobar si hay clientes sin email en la fecha indicada. (24/ago/23 04.31)
        var fecha = DateTimePickerGYG.Value.Date;
        string res = ComprobarEmails(fecha);

        if (string.IsNullOrWhiteSpace(res) == false)
        {
            MessageBox.Show($"Hay reservas del {fecha:dddd dd/MM/yyyy} sin emails.\r\nSi envías los mensajes a esos clientes no les llegará.\r\n{res}", "Comprobar reservas sin email", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        else
        {
            MessageBox.Show($"Todas las reservas de la fecha '{fecha:dddd dd/MM/yyyy}' tiene asignado el email.", "Comprobar reservas sin email", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        //        // Mostrar ventana, elegir fecha                    (23/ago/23 07.55)
        //        // y sacar una lista de los que no tienen email

        //        var fecha = DateTimePickerGYG.Value.Date;

        //        /*
        //Select ID, FechaActividad, HoraActividad, Nombre, Telefono, Email, Notas from reservas 
        //where Activa = 1 and CanceladaCliente = 0 and idDistribuidor = 10
        //and Email = '' and Nombre != 'Makarena (GYG)'
        //and FechaActividad = '2023-08-23'
        //order by FechaActividad, HoraActividad, ID

        //        */
        //        StringBuilder sb = new StringBuilder();
        //        sb.Append("Select ID, FechaActividad, HoraActividad, Nombre, Telefono, Email, Notas from reservas ");
        //        sb.Append("where Activa = 1 and CanceladaCliente = 0 and idDistribuidor = 10 ");
        //        sb.Append("and Email = '' and Nombre != 'Makarena (GYG)' ");
        //        sb.Append($"and FechaActividad = '{fecha:yyyy-MM-dd}' ");
        //        sb.Append("order by FechaActividad, HoraActividad, ID");

        //        var colRes = Reservas.TablaCol(sb.ToString());

        //        if (colRes.Count > 0)
        //        {
        //            sb.Clear();
        //            for (int i = 0; i < colRes.Count; i++)
        //            {
        //                sb.AppendLine($"{colRes[i].Nombre}, {colRes[i].Notas}");
        //            }
        //            MessageBox.Show($"Hay {colRes.Count} {colRes.Count.Plural("reserva")} del {fecha:dddd dd/MM/yyyy} sin email.\r\n" + sb.ToString(), "Comprobar reservas sin email", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        }
        //        else
        //        {
        //            MessageBox.Show("Todas las reservas de la fecha tiene asignado el email.", "Comprobar reservas sin email", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        }
    }
}