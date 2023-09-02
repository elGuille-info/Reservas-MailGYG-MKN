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
using ApiReservasMailGYG;

namespace ReservasGYG;

public partial class Form1 : Form
{
    private bool inicializando = true;
    public static Form1 Current { get; set; }

    public Form1()
    {
        InitializeComponent();
        Current = this;
    }

    private void Form1_Load(object sender, EventArgs e)
    {
        inicializando = false;

        //LvwSinEmail.Items.Clear();
        //LvwSinEmail.Columns.Clear();
        //for (int j = 0; j < ApiReservasMailGYG.ReservasGYG.Columnas.Length; j++)
        //{
        //    LvwSinEmail.Columns.Add(ApiReservasMailGYG.ReservasGYG.Columnas[j]);
        //}
        AsignarColumnasLvw(LvwSinEmail, asignarColumnas: true);

        // No cargarlo.                                     (25/ago/23 14.11)

        //// Cargar el programa de analizar emails.           (24/ago/23 15.38)
        ////CargarAnalizarEmail();
        //// Hacerlo con el timer.                            (24/ago/23 15.40)
        //TimerCargarAnalizarEmail.Interval = 300;
        //TimerCargarAnalizarEmail.Enabled = true;

        HabilitarBotones(false);

        DateTimePickerGYG.Value = DateTime.Today;

        Form1_Resize(null, null);
    }

    private void Form1_FormClosing(object sender, FormClosingEventArgs e)
    {
        // Si la cierra el usuario
        //if (e.CloseReason == CloseReason.UserClosing) 
        //{
        //    if (FormAnalizaEmail.Current != null && FormAnalizaEmail.Current.IsDisposed == false)
        //    {
        //        var ret = MessageBox.Show($"La ventana de analizar mails est� abierta.{CrLf}�Quieres cerrar todas las ventanas abiertas?", 
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

    private void Form1_Resize(object sender, EventArgs e)
    {
        if (inicializando) return;

        // Ajustar el ancho de las columnas del listview.   (27/ago/23 17.54)
        //var w = (GrbOpcionesFecha.ClientSize.Width - 60) / 3;
        //LvwSinEmail.Columns[0].Width = (GrbOpcionesFecha.ClientSize.Width - 60) - w;
        //LvwSinEmail.Columns[1].Width = w;
        AsignarColumnasLvw(LvwSinEmail, asignarColumnas: false);

        // Ajustar el tama�o de Ma�anaEs y HoyEs.           (02/sep/23 19.20)
        int w = (GrbOpcionesFecha.ClientSize.Width - 30) / 2;
        BtnMa�anaEs.Width = w;
        BtnHoyEs.Width = w;
        BtnHoyEs.Left = BtnMa�anaEs.Left + w + 12;

        BtnReservasSinSalida.Width = w;
        BtnMostrarReservas.Width = w;
        BtnMostrarReservas.Left = BtnReservasSinSalida.Left + w + 12;

        w = (GrbOpcionesFecha.ClientSize.Width - 45) / 3;
        BtnAlerta1.Width = w;
        BtnAlerta2.Width = w;
        BtnAlerta3.Width = w;
        BtnAlerta2.Left = BtnAlerta1.Left + w + 12;
        BtnAlerta3.Left = BtnAlerta2.Left + w + 12;
    }

    /// <summary>
    /// Asignar las columnas y el ancho al listview indicado.
    /// </summary>
    /// <param name="LvwSinEmail"></param>
    /// <param name="asignarColumnas">True para asignar columnas y limpiar contenido, false para cambiar el ancho.</param>
    public static void AsignarColumnasLvw(ListView LvwSinEmail, bool asignarColumnas)
    {
        if (asignarColumnas)
        {
            LvwSinEmail.Items.Clear();
            LvwSinEmail.Columns.Clear();

            for (int j = 0; j < ApiReservasMailGYG.ReservasGYG.Columnas.Length; j++)
            {
                LvwSinEmail.Columns.Add(ApiReservasMailGYG.ReservasGYG.Columnas[j]);
            }
        }

        LvwSinEmail.Columns[0].Width = 160; // booking
        LvwSinEmail.Columns[1].Width = 400; // Nombre
        LvwSinEmail.Columns[2].Width = 150; // Tel�fono
        LvwSinEmail.Columns[3].Width = 260; // Reserva
        LvwSinEmail.Columns[4].Width = 160; // pax
        LvwSinEmail.Columns[5].Width = 300; // Email
        LvwSinEmail.Columns[6].Width = 300; // Notas
    }

    private void DateTimePickerGYG_ValueChanged(object sender, EventArgs e)
    {
        if (inicializando) return;

        //Comprobar clientes sin email en la fecha
        BtnComprobarSinMail.Text = $"Comprobar reservas sin email del {DateTimePickerGYG.Value:dddd dd/MM/yyyy}";
        BtnMostrarReservas.Text = $"Mostrar reservas del {DateTimePickerGYG.Value:dddd dd/MM/yyyy}";
        BtnFotos.Text = $"Enviar las fotos de las reservas del {DateTimePickerGYG.Value:dddd dd/MM/yyyy}";

        BtnReservasSinSalida.Text = $"Mostrar las reservas sin salida del {DateTimePickerGYG.Value:dddd dd/MM/yyyy}";

        BtnMostrarReservas.Text = $"Mostrar Reservas del {DateTimePickerGYG.Value:dddd dd/MM/yyyy}";

        // Al cambiar de fecha, deshabilitar los botones, salvo el de comprobar. (27/ago/23 18.21)
        HabilitarBotones(false);
    }

    private void HabilitarBotones(bool habilitar)
    {
        BtnMa�anaEs.Enabled = habilitar;
        BtnHoyEs.Enabled = habilitar;
        BtnAlerta1.Enabled = habilitar;
        BtnAlerta2.Enabled = habilitar;
        BtnAlerta3.Enabled = habilitar;
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

    private void BtnMa�anaEs_Click(object sender, EventArgs e)
    {
        DateTime fecha = DateTimePickerGYG.Value.Date;
        DialogResult ret;

        // Comprobar que la fecha no sea hoy.               (27/ago/23 14.11)
        if (fecha.Date == DateTime.Today)
        {
            MessageBox.Show("No se pueden mandar el mensaje MANA�A ES EL D�A a las reservas de HOY." + CrLf + CrLf +
                            $"Has indicado mandar el mensaje MA�ANA es el d�a a las reservas de hoy {fecha.Date:dddd dd/MM/yyyy}" + CrLf +
                            "Debes elegir la opci�n HOY en el d�a para mandar en el mismo d�a de la actividad." + CrLf +
                            "O bien cambiar la fecha seleccionada.",
                            "Mandar recordatorio de que MA�ANA es el d�a.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            DateTimePickerGYG.Focus();
            return;
        }

        ret = MessageBox.Show($"�Quieres mandar el mensaje de 'Ma�ana es el d�a' a las reservas del {fecha.Date:dddd dd/MM/yyyy}?" + CrLf +
                              "Pulsa S� para mandar el mensaje de MA�ANA ES EL D�A a las reservas de esa fecha." + CrLf +
                              $"Pulsa NO no mandar nada y seleccionar otra fecha.",
                              "Mandar recordatorio de que MA�ANA es el d�a.", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        if (ret == DialogResult.No)
        {
            DateTimePickerGYG.Focus();
            return;
        }


        // Comprobar que todos tengan email.                (24/ago/23 04.41)
        //string res = ComprobarEmails(fecha);
        // Usando la nueva funci�n.                         (27/ago/23 17.26)
        var res = ComprobarEmailsReservas(fecha, LvwSinEmail);

        if (res > 0)
        {
            MessageBox.Show($"Hay {res} {res.Plural("reserva")} del {fecha:dddd dd/MM/yyyy} sin emails.{CrLf}No se puede continuar hasta que lo soluciones.",
                            "Mandar recordatorio de que MA�ANA es el d�a.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        var lisRes = Reservas.TablaCol($"SELECT * FROM Reservas Where idDistribuidor=10 and Activa=1 and CanceladaCliente=0 and Confirmada=1 and FechaActividad ='{fecha:yyyy-MM-dd}' ORDER By FechaActividad, HoraActividad");
        if (MessageBox.Show($"Se va a madar el mensaje a {lisRes.Count} {lisRes.Count.Plural("reserva")}",
                             "Enviar MA�ANA es el d�a", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
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
        sb.Append(Properties.Resources.Ma�ana_es_el_dia.Replace(CrLf, "<br/>"));
        sb.Append("<br/>");
        sb.Append("<br/>");
        sb.Append("Kayak Makarena");

        string body = sb.ToString().Replace(CrLf, "<br/>");
        var msg = ApiReservasMailGYG.MailGYG.EnviarMensaje(colPara, "Ma�ana es el d�a / Tomorrow is the day", body, true);
        if (msg.StartsWith("ERROR"))
        {
            MessageBox.Show($"ERROR al enviar el email:{CrLf}{msg}.", "Error al enviar el email de la reserva", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        else
        {
            MessageBox.Show($"{msg}", "Enviar email de la reserva", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

    }

    private void BtnHoyEs_Click(object sender, EventArgs e)
    {
        DateTime fecha = DateTimePickerGYG.Value.Date;
        DialogResult ret;

        // Cambiar el orden de la comprobaci�n.             (27/ago/23 13.22)
        // Doble comprobaci�n                               (25/ago/23 14.12)
        // Si es el mismo d�a de la actividad... avisar que no se debe mandar salvo si es de madrugada
        if (DateTime.Today == fecha.Date && DateTime.Now.TimeOfDay.Hours > 8)
        {
            ret = MessageBox.Show($"Los mensajes de 'Hoy es el d�a' para las reservas del {fecha.Date:dddd dd/MM/yyyy}" + CrLf +
                                  "Solo se deben mandar en el mismo d�a si es antes de las 08:00." + CrLf + CrLf +
                                  $"AHORA SON LAS {DateTime.Now:HH:mm} Y NO DEBER�AS MANDARLAS." + CrLf + CrLf +
                                  "Pulsa ACEPTAR para mandar los mensajes." + CrLf +
                                  $"Pulsa CANCELAR para no mandar nada y/o seleccionar otra fecha.",
                                  "Mandar recordatorio de que hoy es el d�a.",
                                  MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (ret == DialogResult.Cancel)
            {
                DateTimePickerGYG.Focus();
                return;
            }
        }

        // Segundo aviso, sin m�s comprobaciones.
        ret = MessageBox.Show($"�Quieres mandar el mensaje de 'Hoy es el d�a' a las reservas del {fecha.Date:dddd dd/MM/yyyy}?" + CrLf +
                              "Pulsa S� para mandar el mensaje a las reservas de esa fecha." + CrLf +
                              $"Pulsa NO no mandar nada y seleccionar otra fecha.",
                              "Mandar recordatorio de que hoy es el d�a.", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        if (ret == DialogResult.No)
        {
            DateTimePickerGYG.Focus();
            return;
        }


        // Comprobar que todos tengan email.                (24/ago/23 04.41)
        // Usando la nueva funci�n.                         (27/ago/23 17.26)
        var res = ComprobarEmailsReservas(fecha, LvwSinEmail);

        if (res > 0)
        {
            MessageBox.Show($"Hay {res} {res.Plural("reserva")} del {fecha:dddd dd/MM/yyyy} sin emails:{CrLf}No se puede continuar hasta que lo soluciones.",
                            "Mandar recordatorio de que hoy es el d�a.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        var lisRes = Reservas.TablaCol($"SELECT * FROM Reservas Where idDistribuidor=10 and Activa=1 and CanceladaCliente=0 and Confirmada=1 and FechaActividad ='{fecha:yyyy-MM-dd}' ORDER By FechaActividad, HoraActividad");
        if (MessageBox.Show($"Se va a mandar el mensaje a {lisRes.Count} {lisRes.Count.Plural("reserva")}",
                             "Enviar Hoy es el d�a", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
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
        var msg = MailGYG.EnviarMensaje(colPara, "Hoy es el d�a / Today is the day", body, true);
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
        FormEnviarFotos.Current.FechaFotos = DateTimePickerGYG.Value;
        FormEnviarFotos.Current.BringToFront();
        FormEnviarFotos.Current.Show();
        FormEnviarFotos.Current.Focus();
    }

    private void BtnComprobarSinMail_Click(object sender, EventArgs e)
    {
        // Comprobar si hay clientes sin email en la fecha indicada. (24/ago/23 04.31)
        var fecha = DateTimePickerGYG.Value.Date;

        var res = ComprobarEmailsReservas(fecha, LvwSinEmail);
        if (res > 0)
        {
            MessageBox.Show($"Hay {res} {res.Plural("reserva")} del {fecha:dddd dd/MM/yyyy} sin emails.{CrLf}No se debe continuar hasta que lo soluciones.", "Comprobar reservas sin email", MessageBoxButtons.OK, MessageBoxIcon.Error);
            HabilitarBotones(false);
        }
        else
        {
            MessageBox.Show($"Todas las reservas del '{fecha:dddd dd/MM/yyyy}' tiene asignado el email.", "Comprobar reservas sin email", MessageBoxButtons.OK, MessageBoxIcon.Information);
            HabilitarBotones(true);
        }
    }

    private void BtnReservasSinSalida_Click(object sender, EventArgs e)
    {
        // Comprobar las reservas que no han salido         (29/ago/23 11.18)
        // de la fecha indicada 
        var fecha = DateTimePickerGYG.Value.Date;

        //StringBuilder sb = new StringBuilder();
        //sb.Append("Select * from Reservas ");
        //sb.Append("where Activa = 1 and CanceladaCliente = 0 and idDistribuidor = 10 ");
        //sb.Append("and Nombre != 'Makarena (GYG)' ");
        //sb.Append("and Control = 0 ");
        //sb.Append($"and FechaActividad = '{fecha:yyyy-MM-dd}' ");
        //sb.Append("order by FechaActividad, HoraActividad, ID");

        //var colRes = Reservas.TablaCol(sb.ToString());

        //List<ApiReservasMailGYG.ReservasGYG> col = new();

        //if (colRes.Count > 0)
        //{
        //    for (int i = 0; i < colRes.Count; i++)
        //    {
        //        col.Add(new ApiReservasMailGYG.ReservasGYG(colRes[i]));
        //    }
        //}

        var col = ApiReservasMailGYG.MailGYG.ReservasSinSalida(fecha);

        AsignarListView(col, LvwSinEmail);

        if (col.Count == 0)
        {
            MessageBox.Show($"Todas las reservas del {fecha:dddd dd/MM/yyyy} han salido.", "Comprobar reservas sin salida", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        else
        {
            MessageBox.Show($"Hay {col.Count} {col.Count.Plural("reserva")} del {fecha:dddd dd/MM/yyyy} que no han salido.", "Comprobar reservas sin salida", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }

    private void BtnMostrarReservas_Click(object sender, EventArgs e)
    {
        // Lista de las reservas de la fecha indicada.      (29/ago/23 11.32)
        var fecha = DateTimePickerGYG.Value.Date;

        var col = ApiReservasMailGYG.MailGYG.DatosReservas(fecha, new TimeSpan(0, 0, 0));
        AsignarListView(col, LvwSinEmail);

        //StringBuilder sb = new StringBuilder();
        //sb.Append("Select * from Reservas ");
        //sb.Append("where Activa = 1 and CanceladaCliente = 0 and idDistribuidor = 10 ");
        //sb.Append("and Nombre != 'Makarena (GYG)' ");
        //// Solo las rutas                               (02/sep/23 18.20)
        //sb.Append($"and Actividad like 'ruta%' ");
        //sb.Append($"and FechaActividad = '{fecha:yyyy-MM-dd}' ");
        //sb.Append("order by FechaActividad, HoraActividad, ID");

        //var colRes = Reservas.TablaCol(sb.ToString());

        //List<ApiReservasMailGYG.ReservasGYG> col = new();

        //if (colRes.Count > 0)
        //{
        //    for (int i = 0; i < colRes.Count; i++)
        //    {
        //        col.Add(new ApiReservasMailGYG.ReservasGYG(colRes[i]));
        //    }
        //}
        //AsignarListView(col, LvwSinEmail);

        if (col.Count == 0)
        {
            MessageBox.Show($"No hay reservas del {fecha:dddd dd/MM/yyyy}.", "Listado de reservas", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        else
        {
            MessageBox.Show($"Hay {col.Count} {col.Count.Plural("reserva")} del {fecha:dddd dd/MM/yyyy}.", "Listado de reservas", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }


    /// <summary>
    /// Asignar los datos de las reservas sin email al listView indicado.
    /// </summary>
    /// <param name="col"></param>
    /// <param name="LvwSinEmail"></param>
    public static void AsignarListView(List<ApiReservasMailGYG.ReservasGYG> col, ListView LvwSinEmail)
    {
        //LvwSinEmail.Items.Clear();
        //LvwSinEmail.Columns.Clear();

        //for (int j = 0; j < ApiReservasMailGYG.ReservasGYG.Columnas.Length; j++)
        //{
        //    LvwSinEmail.Columns.Add(ApiReservasMailGYG.ReservasGYG.Columnas[j]);
        //}
        AsignarColumnasLvw(LvwSinEmail, asignarColumnas: true);

        for (int i = 0; i < col.Count; i++)
        {
            var item = LvwSinEmail.Items.Add(col[i].ValorColumna("booking"));
            for (int j = 1; j < ApiReservasMailGYG.ReservasGYG.Columnas.Length; j++)
            {
                item.SubItems.Add(col[i].ValorColumna(ApiReservasMailGYG.ReservasGYG.Columnas[j]));
            }
        }
    }

    /// <summary>
    /// Comprueba las reservas de la fecha indicada sin email y las a�ade al listview.
    /// </summary>
    /// <param name="fecha"></param>
    /// <param name="LvwSinEmail"></param>
    /// <returns></returns>
    public static int ComprobarEmailsReservas(DateTime fecha, ListView LvwSinEmail)
    {
        var col = MailGYG.ComprobarEmails(fecha);

        AsignarListView(col, LvwSinEmail);

        return col.Count;
    }

    private void ContextMenuListView_Opening(object sender, System.ComponentModel.CancelEventArgs e)
    {
        bool hab = LvwSinEmail.SelectedIndices.Count > 0;
        foreach (var mnu in ContextMenuListView.Items)
        {
            (mnu as ToolStripMenuItem).Enabled = hab;
        }
    }

    private void MnuCopiarDeLvw_Click(object sender, EventArgs e)
    {
        if (LvwSinEmail.SelectedIndices.Count == 0) return;
        //var mnu = sender as ToolStripMenuItem;
        //if (mnu == null) return;
        int index = -1;
        if (sender == MnuCopiarBooking) index = 0;
        if (sender == MnuCopiarNombre) index = 1;
        if (sender == MnuCopiarTelefono) index = 2;
        if (sender == MnuCopiarNotas) index = 6;
        if (index == -1) return;

        string texto = LvwSinEmail.Items[LvwSinEmail.SelectedIndices[0]].SubItems[index].Text;
        CopiarPortapapeles(texto);
    }

    /// <summary>
    /// Copiar el texto indicado en el portapapeles.
    /// </summary>
    /// <param name="texto">el texto a copiar en el portapapeles.</param>
    public static void CopiarPortapapeles(string texto)
    {
        try
        {
            Clipboard.SetText(texto);
        }
        catch { }
    }

    private void BtnAlerta1_Click(object sender, EventArgs e)
    {

    }

    private void BtnAlerta2_Click(object sender, EventArgs e)
    {

    }

    private void BtnAlerta3_Click(object sender, EventArgs e)
    {

    }
}