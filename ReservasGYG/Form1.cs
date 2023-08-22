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

namespace ReservasGYG;

public partial class Form1 : Form
{
    public static Form1 Current;

    public Form1()
    {
        InitializeComponent();
        Current = this;
    }

    private void Form1_Load(object sender, EventArgs e)
    {

    }

    private void ChkLeerNuevos_CheckedChanged(object sender, EventArgs e)
    {

    }

    private void BtnBuscar_Click(object sender, EventArgs e)
    {

    }

    private void TxtFechas_EnabledChanged(object sender, EventArgs e)
    {

    }

    private void TxtFechas_KeyPress(object sender, KeyPressEventArgs e)
    {

    }

    private void TxtFechas_KeyUp(object sender, KeyEventArgs e)
    {

    }

    private void BtnAnalizarEmail_Click(object sender, EventArgs e)
    {
        if (FormAnalizaEmail.Current == null || FormAnalizaEmail.Current.IsDisposed)
        {
            FormAnalizaEmail.Current = new FormAnalizaEmail();
        }
        FormAnalizaEmail.Current.BringToFront();
        FormAnalizaEmail.Current.Show();
        FormAnalizaEmail.Current.Focus();
    }

    private void BtnMostrarReservas_Click(object sender, EventArgs e)
    {
    }

    private void BtnHoyEs_Click(object sender, EventArgs e)
    {
        // Enviar el mensaje a todas las reservas de HOY.
        var res = MessageBox.Show($"¿Quieres mandar el mensaje de 'Hoy es el día' a las reservas de MAÑANA día {DateTime.Today.AddDays(1):F}" + FormAnalizaEmail.CrLf +
                                  "Pulsa SÍ para mandar el mensaje a las de esa fecha." + FormAnalizaEmail.CrLf +
                                  $"Pulsa NO para mandar el mensaje a las reservas de HOY: {DateTime.Today:F}" + FormAnalizaEmail.CrLf +
                                  "Pulsa CANCELAR para no mandar nada.",
                                  "Mandar recordatorio de que hoy es el día.", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
        if (res == DialogResult.Cancel) return;
        var fecha = DateTime.Today.AddDays(1);
        if (res == DialogResult.No)
        {
            fecha = DateTime.Today;
        }
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
            if (string.IsNullOrEmpty(re.Email)) continue;
            colPara.Add(re.Email);
        }
        StringBuilder sb = new StringBuilder();
        sb.Append("");
        sb.Append(Properties.Resources.Hoy_es_el_dia_txt.Replace(FormAnalizaEmail.CrLf, "<br/>"));
        sb.Append("<br/>");
        sb.Append("<br/>");
        sb.Append("Kayak Makarena");

        string body = sb.ToString().Replace(FormAnalizaEmail.CrLf, "<br/>");
        var msg = ApiReservasMailGYG.MailGYG.EnviarMensaje(colPara, "Hoy es el día / Today is the day", body, true);
        if (msg.StartsWith("ERROR"))
        {
            MessageBox.Show($"ERROR al enviar el email:{FormAnalizaEmail.CrLf}{msg}.", "Error al enviar el email de la reserva", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

    }
}