// Analizar el email de GYG para crear una reserva.         (21/ago/23 06.48)
//


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using KNDatos;

using static ApiReservasMailGYG.MailGYG;


namespace ReservasGYG;

public partial class FormAnalizaEmail : Form
{
    private bool inicializando = true;
    private string StatusAnt;
    private object QueBoton;

    public static FormAnalizaEmail Current { get; set; }
    public FormAnalizaEmail()
    {
        InitializeComponent();
        Current = this;
    }

    private Reservas LaReserva { get; set; }

    private StringBuilder InfoCrearConEmail { get; set; } = new StringBuilder();

    private void FormAnalizaEmailGYG_Load(object sender, EventArgs e)
    {
        inicializando = false;

        timer1.Interval = 990;
        timer1.Enabled = true;

        RtfEmail.Text = "";

        LimpiarControlesReserva();
    }

    private void FormAnalizaEmail_KeyUp(object sender, KeyEventArgs e)
    {
        // Si se pulsa Ctrl+C pegar el texto en RtfEmail.   (23/ago/23 11.51)
        if (e.Control && e.KeyCode == Keys.C)
        {
            BtnPegarEmail_Click(null, null);
        }
    }

    private void BtnPegarEmail_Click(object sender, EventArgs e)
    {
        LimpiarControlesReserva();

        // Pegar el texto del portapapeles en el RtfEmail
        var dClip = Clipboard.GetDataObject();
        if (dClip == null) return;

        string s = "";

        if (dClip.GetDataPresent(DataFormats.Text))
        {
            var obj = dClip.GetData(DataFormats.Text);
            if (obj != null)
            {
                s = obj.ToString();
            }
        }

        RtfEmail.Text = s;
    }

    private void BtnAnalizarEmail_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(RtfEmail.Text)) return;

        Reservas re = ApiReservasMailGYG.MailGYG.AnalizarEmail(RtfEmail.Text);
        if (re == null)
        {
            // Mostrar aviso de que algo no ha ido bien. (22/ago/23 20.24)
            MessageBox.Show("Parece que los datos analizados no son correctos.", "Analizar email de GYG", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        LaReserva = re;
        re.Notas2 = $"Price: {re.GYGPrice}";

        LimpiarControlesReserva();

        TxtNombre.Text = re.Nombre;
        TxtTelefono.Text = re.Telefono;
        TxtNotas.Text = re.GYGNotas;
        TxtActividad.Text = re.ActividadMostrar; // re.GYGOption; // re.Actividad;
        TxtEmail.Text = re.Email;
        TxtAdultos.Text = re.Adultos.ToString();
        TxtMenores.Text = re.Niños.ToString();
        TxtMenoresG.Text = re.Niños2.ToString();
        TxtFechaHora.Text = $"{re.FechaActividad:dd/MM/yyyy} {re.HoraActividad:hh\\:mm}"; //re.GYGFechaHora; // $"{re.FechaActividad:dd/MM/yyyy} {re.HoraActividad:hh\\mm}";
        TxtPrice.Text = re.GYGPrice;
        TxtLanguage.Text = re.GYGLanguage;
        TxtReference.Text = re.GYGReference;
        TxtPais.Text = re.GYGPais;

        TxtGYG.Text = $"Option: {re.GYGOption}\r\nDate: {re.GYGFechaHora}\r\nPrice:{re.GYGPrice}";
        TxtID.Text = re.ID.ToString();

        ChkCrearConEmail.Enabled = true;
        HabilitarBotonesReservas();
    }

    private void BtnLimpiarReserva_Click(object sender, EventArgs e)
    {
        LimpiarControlesReserva();
    }

    private void LimpiarControlesReserva()
    {
        if (inicializando) return;

        // Limpiar los controles del grupo              (21/ago/23 08.30)
        foreach (Control ctl in GrbReserva.Controls)
        {
            var txt = ctl as TextBox;
            if (txt != null)
            {
                ctl.Text = "";
            }
        }

        ChkCrearConEmail.Enabled = false;
        ChkEnviarConfirm.Enabled = false;

        ChkCrearConEmail.Checked = false;
        ChkEnviarConfirm.Checked = false;
        HabilitarBotonesReservas();
    }

    private void BtnCrearReserva_Click(object sender, EventArgs e)
    {
        QueBoton = sender;

        StatusAnt = LabelStatus.Text;
        LabelStatus.Text = "Creando la reserva...";
        Application.DoEvents();

        TimerCrearReserva.Interval = 200;
        TimerCrearReserva.Enabled = true;
    }

    private void BtnEnviarConfirm_Click(object sender, EventArgs e)
    {
        QueBoton = sender;

        StatusAnt = LabelStatus.Text;
        LabelStatus.Text = "Enviando el email de confirmación...";
        Application.DoEvents();

        TimerEnviarEmail.Interval = 200;
        TimerEnviarEmail.Enabled = true;
    }

    private void BtnCrearConEmail_Click(object sender, EventArgs e)
    {
        // Las dos cosas seguidas.                      (22/ago/23 10.28)
        // Si se hacen desde el temporizador no va bien.
        //BtnCrearReserva_Click(sender, e);
        //BtnEnviarConfirm_Click(sender, e);

        QueBoton = sender;

        StatusAnt = LabelStatus.Text;
        LabelStatus.Text = "Creando la reserva...";
        Application.DoEvents();

        CrearReserva();

        LabelStatus.Text = StatusAnt;
        Application.DoEvents();

        QueBoton = sender;

        StatusAnt = LabelStatus.Text;
        LabelStatus.Text = "Enviando el email de confirmación...";
        Application.DoEvents();

        EnviarMensajeConfirmacion();

        Application.DoEvents();

        ChkCrearConEmail.Checked = false;
        ChkEnviarConfirm.Checked = false;
        HabilitarBotonesReservas();

        MessageBox.Show(InfoCrearConEmail.ToString(), "Crear reserva y enviar email", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private bool CrearReserva()
    {
        if (LaReserva == null)
        {
            MessageBox.Show("La reserva no está asignada.", "No hay reserva", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            return true;
        }
        //https://kayakmaro.es/MKNReservasAPI?ta=3&id=10&user=makarena&bdp=0
        //&fecha=16/09/2023&hora=15:30&actividad=RUTA CORTA&ad=6&cli=MAKARENA+(GYG)&tel=+34611825646
        //&notas=pre-reservar+plazas&pago=1&modop=otro

        var re = LaReserva;
        if (re.ID > 0)
        {
            MessageBox.Show($"Ya hay una reserva con ese ID: {re.ID}.", "Ya existe esa reserva", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            return true;
        }

        // Buscar el producto.
        var pr = Producto.Buscar(re.FechaActividad, re.HoraActividad, re.Actividad);
        if (pr == null)
        {
            MessageBox.Show("No existe un producto para la actividad indicada." + CrLf +
                            $"{re.ActividadMostrar}, {re.FechaActividad:dd/MM/yyyy}, {re.HoraActividad:hh\\:mm}",
                            "No existe el producto",
                            MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            return true;
        }
        re.idProducto = pr.ID;
        re.Duracion = pr.Duracion;
        re.PrecioAdulto = pr.PrecioAdulto;
        re.PrecioNiño = pr.PrecioNiño;
        re.Total = re.Adultos * pr.PrecioAdulto + re.Niños * pr.PrecioNiño;
        re.PagoACta = re.Total;
        re.ModoPagoACta = "GetYourGuide";
        re.Documento = $"Cobro total por GetYourGuide";
        re.Notas = $"GetYourGuide - {re.GYGReference} - ({re.GYGPais}) - {re.GYGLanguage} - {CrLf}{re.GYGNotas}";

        // Buscar una reserva con este nombre, teléfono y actividad.
        var re2 = Reservas.ComprobarReservaMismaActividad(re.Nombre, re.Telefono, re.Actividad, re.FechaActividad, re.HoraActividad, 0);
        // Esta reserva segurqamente tendrá el ID 0
        //var reID = Reservas.Buscar(re.ID);
        if (re2 != null)
        {
            MessageBox.Show("Ya hay una reserva para ese cliente y actividad.", "Ya existe esa reserva", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            return true;
        }

        // Crer el cliente si no existe.                (21/ago/23 22.51)
        var cli = Clientes.BuscarCliente(re.Nombre, re.Telefono, re.Email);
        if (cli == null)
        {
            // Crear un nuevo cliente.
            var resCli = KNDatos.Clientes.CrearCliente(re.Nombre, re.Telefono, re.Email, 10, "");
            if (resCli.cli == null)
            {
                resCli.msg = "ERROR: El cliente creado es nulo." + CrLf + resCli.msg;
            }
            if (resCli.msg.StartsWith("ERROR"))
            {
                MessageBox.Show($"No se ha podido crear el nuevo cliente:{CrLf}{resCli.msg}", "Error al crear el cliente", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return true;
            }
        }

        // Crear la reserva
        string msg = re.Crear2();
        TxtID.Text = re.ID.ToString();
        if (msg.StartsWith("ERROR"))
        {
            MessageBox.Show($"ERROR al crear la reserva:{CrLf}{msg}.", "Error al crear la reserva", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return true;
        }

        InfoCrearConEmail.Clear();
        InfoCrearConEmail.AppendLine($"La reserva se ha creado:{CrLf}Cliente: {re.Nombre} ({re.Telefono}){CrLf}Actividad: {re.ActividadMostrar}, {re.FechaActividad:dd/MM/yyyy}, {re.HoraActividad:hh\\:mm}{CrLf}PAX: {re.PaxsLargo}");
        InfoCrearConEmail.AppendLine();

        LabelStatus.Text = StatusAnt;

        if (QueBoton == BtnCrearReserva)
        {
            MessageBox.Show(InfoCrearConEmail.ToString(), "Reserva creada", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        return false;
    }

    private bool EnviarMensajeConfirmacion()
    {
        StringBuilder sb = new StringBuilder();

        var re = LaReserva;
        if (re == null)
        {
            MessageBox.Show($"ERROR la reserva es nula. Debes generarla primero con 'Crear reserva'.",
                            "Error al enviar el email de la reserva",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
            return true;
        }

        var DatosVPWiz = new MKNUtilidades.VentasPlayaWiz(re);
        MKNUtilidades.VentasPlayaWiz.IncluirReportajeConfirmarReserva = false;
        MKNUtilidades.VentasPlayaWiz.IncluirTextosConfirmarReserva = false;
        bool enIngles = re.GYGLanguage.Contains("English");

        sb.Append(DatosVPWiz.ResumenReserva(esWeb: false, enIngles: enIngles));
        sb.AppendLine();
        sb.AppendLine();

        // Mandar el texto según el idioma.             (22/ago/23 10.58)
        if (re.HoraActividad.Hours == 9)
        {
            if (enIngles)
            {
                sb.Append(Properties.Resources.IMPORTANTE_EN_09_30_txt.Replace(CrLf, "<br/>"));
            }
            else
            {
                sb.Append(Properties.Resources.IMPORTANTE_ES_09_30.Replace(CrLf, "<br/>"));
            }
        }
        else if (re.HoraActividad.Hours == 10 || re.HoraActividad == new TimeSpan(11, 0, 0))
        {
            if (enIngles)
            {
                sb.Append(Properties.Resources.IMPORTANTE_EN_10_30_11_00_txt.Replace(CrLf, "<br/>"));
            }
            else
            {
                sb.Append(Properties.Resources.IMPORTANTE_ES_10_30_11_00_txt.Replace(CrLf, "<br/>"));
            }
        }
        else
        {
            if (enIngles)
            {
                sb.Append(Properties.Resources.IMPORTANTE_EN_txt.Replace(CrLf, "<br/>"));
            }
            else
            {
                sb.Append(Properties.Resources.IMPORTANTE_ES_txt.Replace(CrLf, "<br/>"));
            }
        }

        sb.Append("<br/>");
        sb.Append("<br/>");
        sb.Append("Kayak Makarena");

        var asunto = $"Booking - S271506 - {re.GYGReference}";
        var para = re.Email;
        string body = sb.ToString().Replace(CrLf, "<br/>");
        var msg = ApiReservasMailGYG.MailGYG.EnviarMensaje(para, asunto, body, true);

        InfoCrearConEmail.AppendLine(msg);
        InfoCrearConEmail.AppendLine();

        if (QueBoton == BtnEnviarConfirm)
        {
            if (msg.StartsWith("ERROR"))
            {
                MessageBox.Show($"ERROR al enviar el email:{CrLf}{msg}.", "Error al enviar el email de la reserva", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                MessageBox.Show($"{msg}", "Enviar email de la reserva", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        LabelStatus.Text = StatusAnt;
        Application.DoEvents();

        return false;
    }

    private void RtfEmail_TextChanged(object sender, EventArgs e)
    {
        if (inicializando) return;

        bool hayTexto = !string.IsNullOrWhiteSpace(RtfEmail.Text);
        BtnAnalizarEmail.Enabled = hayTexto;
    }

    private void ChkCrearReserva_CheckedChanged(object sender, EventArgs e)
    {
        if (inicializando) return;

        HabilitarBotonesReservas();
    }

    private void ChkEnviarConfirm_CheckedChanged(object sender, EventArgs e)
    {
        if (inicializando) return;

        HabilitarBotonesReservas();
    }

    private void HabilitarBotonesReservas()
    {
        BtnCrearConEmail.Enabled = ChkCrearConEmail.Checked;

        // No habilitar los otros                       (22/ago/23 20.18)
        // si el de hacer las dos cosas está habilitado
        //ChkEnviarConfirm.Enabled = !ChkCrearConEmail.Checked;
        if (ChkCrearConEmail.Checked == false)
        {
            ChkEnviarConfirm.Enabled = false;
        }
        if (ChkEnviarConfirm.Enabled == false)
        {
            ChkEnviarConfirm.Enabled = false;
            ChkEnviarConfirm.Checked = false;
        }

        BtnCrearReserva.Enabled = ChkEnviarConfirm.Checked;
        BtnEnviarConfirm.Enabled = ChkEnviarConfirm.Checked;
    }

    private void BtnLimpiarTexto_Click(object sender, EventArgs e)
    {
        RtfEmail.Text = "";
    }

    private void BtnOpciones_Click(object sender, EventArgs e)
    {
        //var frm = new Form1();
        //frm.Show();
        if (Form1.Current == null || Form1.Current.IsDisposed)
        {
            Form1.Current = new Form1();
        }
        Form1.Current.BringToFront();
        Form1.Current.Show();
        Form1.Current.Focus();
    }

    private void TimerHoraStatus_Tick(object sender, EventArgs e)
    {
        LabelFechaHora.Text = $"{DateTime.Now:dd/MM/yyyy HH:mm:ss}";
    }

    private void TimerCrearReserva_Tick(object sender, EventArgs e)
    {
        TimerCrearReserva.Stop();
        TimerCrearReserva.Enabled = false;

        CrearReserva();

        LabelStatus.Text = StatusAnt;
        Application.DoEvents();
    }

    private void TimerEnviarEmail_Tick(object sender, EventArgs e)
    {
        TimerEnviarEmail.Stop();
        TimerEnviarEmail.Enabled = false;

        EnviarMensajeConfirmacion();
    }
}
