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

using ApiReservasMailGYG;
using static ApiReservasMailGYG.MailGYG;


namespace ReservasGYG;

public partial class FormAnalizaEmail : Form
{
    private bool inicializando = true;
    private string StatusAnt;
    //private object QueBoton;

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

        BtnPegarEmail.Image = Properties.Resources.Paste; // Bitmap.FromFile("..\\Resources\\Paste.png");
        BtnLimpiarTexto.Image = Properties.Resources.CleanData; //Bitmap.FromFile("..\\Resources\\CleanData.png");

        timer1.Interval = 990;
        timer1.Enabled = true;

        RtfEmail.Text = "";

        LimpiarControlesReserva();
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
        ChkCrearConEmail.Enabled = false;
        ChkCrearConEmail.Checked = false;

        if (string.IsNullOrEmpty(RtfEmail.Text)) return;

        Reservas re;
        DialogResult ret = DialogResult.No;

        // Comprobar si se usa desde la página de Bookings  (05/sep/23 09.02)
        // o desde el email.
        bool desdeBooking = RtfEmail.Text.Contains("Booked on", StringComparison.OrdinalIgnoreCase);

        if (desdeBooking)
        {
            ret = MessageBox.Show("El texto de la reserva se ha tomado desde la página Bookings." + CrLf +
                                  "Pulsa SÍ para analizarla con el formato de Bookings." + CrLf +
                                  "Pulsa NO para analizarla con el formato de email (no recomendable)." + CrLf +
                                  "Pulsa CANCELAR para no analizar nada.",
                                  "Analizar email de GYG", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (ret == DialogResult.Cancel)
            {
                return;
            }
        }

        if (ret == DialogResult.No)
        {
            re = MailGYG.AnalizarEmail(RtfEmail.Text);
        }
        else
        {
            re = MailGYG.AnalizarBooking(RtfEmail.Text);
        }

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
        ChkCrearConEmail.Checked = false;

        // Si es desde booking a visar que revise las cosas antes de guardar.
        if (desdeBooking)
        {
            MessageBox.Show("El texto de la reserva se ha tomado desde la página Bookings." + CrLf +
                            "Comprueba que los datos son correctos antes de guardar y enviar el mensaje.",
                            "Analizar email de GYG", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
        else
        {
            ChkCrearConEmail.Checked = true;
        }

        BtnCrearConEmail.Enabled = ChkCrearConEmail.Checked;
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

        ChkCrearConEmail.Checked = false;
        BtnCrearConEmail.Enabled = ChkCrearConEmail.Checked;
    }

    private void BtnCrearConEmail_Click(object sender, EventArgs e)
    {
        // Las dos cosas seguidas.                      (22/ago/23 10.28)
        // Si se hacen desde el temporizador no va bien.

        // Comprobar si es alquiler y hay como mínimo 2 adultos. (05/sep/23 10.53)
        if (KNDatos.BaseKayak.ActividadesAlquiler().Contains(LaReserva.Actividad))
        {
            DialogResult ret;
            if (LaReserva.Adultos < 2)
            {
                ret = MessageBox.Show("Es un alquiler para menos de 2 pax (adultos)." + CrLf +
                                      "NO se debe aceptar esta reserva." + CrLf + CrLf +
                                      "Hay que contactar con el cliente por wasap y email y avisarle que el mínimo es 2 personas de 7 años o más." + CrLf + CrLf +
                                      "Esta reserva hay que cancelarla." + CrLf + CrLf +
                                      "¿Quieres continuar creando la reserva?",
                                      "Nueva reserva de alquiler",
                                      MessageBoxButtons.YesNo, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2);
                if (ret != DialogResult.Yes)
                    return;
            }
            if (MessageBox.Show("Es un alquiler antes de continuar comprueba que esté correcta la reserva.",
                                "Nueva reserva de alquiler", 
                                MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.Cancel) 
                return;
        }

        // Deshabilitar el botón hasta que finalice.        (27/ago/23 09.58)
        ChkCrearConEmail.Checked = false;

        //QueBoton = sender;

        StatusAnt = LabelStatus.Text;
        LabelStatus.Text = "Creando la reserva...";
        Application.DoEvents();

        // Si devuelve true, no continuar con el envío del email.   (26/ago/23 00.06)
        bool res = CrearReserva();

        LabelStatus.Text = StatusAnt;
        Application.DoEvents();

        if (res)
        {
            ChkCrearConEmail.Checked = false;
            ChkCrearConEmail.Enabled = false;
            BtnCrearConEmail.Enabled = ChkCrearConEmail.Checked;

            MessageBox.Show(InfoCrearConEmail.ToString(), "Crear reserva y enviar email", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        //QueBoton = sender;

        StatusAnt = LabelStatus.Text;
        LabelStatus.Text = "Enviando el email de confirmación...";
        Application.DoEvents();

        EnviarMensajeConfirmacion();

        Application.DoEvents();

        ChkCrearConEmail.Checked = false;
        BtnCrearConEmail.Enabled = ChkCrearConEmail.Checked;

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
        // Actualizar el producto.                          (23/ago/23 20.06)
        pr.TotalPax += re.TotalPax();
        pr.Actualizar2();

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
        if (re2 != null)
        {
            // Avisar, pero permitir continuar              (24/ago/23 06.22)
            //MessageBox.Show($"Ya hay una reserva para ese cliente y actividad.{CrLf}Se continúa pero habrá que contactar con el cliente y comprobar porqué hay más de una reserva.",
            //                "Ya existe esa reserva", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            // Avisar y dar la opción a crearla o no.
            var ret = MessageBox.Show($"Ya hay una reserva para ese cliente y actividad.{CrLf}" +
                                      "Pulsa ACEPTAR para continuar creándola por si es que el cliente se ha equivocado, " +
                                      $"pero habrá que contactar con el cliente y comprobar porqué hay más de una reserva.{CrLf}{CrLf}" +
                                      "Pulsa CANCELAR para no crear la reserva ni mandarle el mensaje de confirmación.",
                                      "Ya existe esa reserva",
                                      MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            if (ret == DialogResult.Cancel)
            {
                InfoCrearConEmail.Clear();
                InfoCrearConEmail.AppendLine($"Ya existe una reserva con estos datos:{CrLf}Cliente: {re2.Nombre} ({re2.Telefono}){CrLf}Actividad: {re2.ActividadMostrar}, {re2.FechaActividad:dd/MM/yyyy}, {re2.HoraActividad:hh\\:mm}{CrLf}PAX: {re2.PaxsLargo}");
                InfoCrearConEmail.AppendLine("Por favor comprueba que esa reserva ya existe.");
                InfoCrearConEmail.AppendLine();

                LabelStatus.Text = StatusAnt;

                return true;
            }
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

        // Asignar los kayaks.                              (24/ago/23 06.23)
        CalcularPiraguas(re);

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

        return false;
    }

    /// <summary>
    /// Calcular las piraguas según los pax.
    /// </summary>
    private void CalcularPiraguas(Reservas re)
    {
        var tPax = re.TotalPax();
        int dobles = (int)Math.Ceiling(tPax / 2.0);
        int individuales = 0;
        int tanques = 0;

        if (dobles * 2 < tPax)
        {
            individuales = 1;
        }


        //// Si hay 2 adultos y 1 niño, poner 1 tanque
        //// sean que pagan o no
        //if ((re.Adultos == 2 || re.Adultos2 == 2)
        //    && (re.Niños == 1 || re.Niños2 == 1))
        //{
        //    tanques = 1;
        //    tPax -= 3;
        //}

        //if (!tPax.EsPar())
        //{
        //    tPax--;
        //    individuales = 1;
        //}
        ////int dobles = (int)Math.Ceiling(tPax / 2.0);

        //// poner tanque si:
        ////   Niños2 es > 1
        ////   Usar 2 adultos por cada 2 niños2 y se pone un tanque
        //if (re.Niños2 > 1)
        //{
        //    var n2 = re.Niños2;
        //    if (!n2.EsPar())
        //        n2 -= 1;
        //    while (re.Adultos + re.Adultos2 < n2)
        //        n2 -= 2;
        //    if (re.Adultos + re.Adultos2 >= 2)
        //    {
        //        tanques = (n2 / 2);
        //        //if (tanques > KNDatos.Config.Current.MaxTanques)
        //        //    tanques = KNDatos.Config.Current.MaxTanques;
        //    }
        //}
        //if (dobles > 0)
        //    dobles -= tanques;

        re.Dobles = dobles;
        re.Individuales = individuales;
        re.Tanque = tanques;
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

        // Si es para el mismo día de la actividad.         (24/ago/23 06.24)
        if (DateTime.Today == re.FechaActividad)
        {
            sb.Append("<br/>");
            if (enIngles)
            {
                sb.Append("We would love to receive a review on the GetYourGuide website with your opinion on this activity, taking into account that <b>Kayak Makarena</b> is responsible for managing the reservations and <b>Maro - Kayak Nerja</b> carries out the routes.");
            }
            else
            {
                sb.Append("Nos encantaría recibir una reseña en el sitio de GetYourGuide con tu opinión sobre esta actividad, teniendo en cuenta que <b>Kayak Makarena</b> es la encargada de gestionar las reservas y <b>Maro - Kayak Nerja</b> realiza las rutas.");
            }
        }

        sb.Append("<br/>");
        sb.Append("<br/>");
        sb.Append("Kayak Makarena");
        sb.Append("https://kayakmakarena.com");

        var asunto = $"Booking - S271506 - {re.GYGReference}";
        var para = re.Email;
        string body = sb.ToString().Replace(CrLf, "<br/>");
        var msg = ApiReservasMailGYG.MailGYG.EnviarMensaje(para, asunto, body, true);

        InfoCrearConEmail.AppendLine(msg);
        InfoCrearConEmail.AppendLine();

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

        BtnCrearConEmail.Enabled = ChkCrearConEmail.Checked;
    }

    //private void HabilitarBotonesReservas()
    //{
    //    BtnCrearConEmail.Enabled = ChkCrearConEmail.Checked;

    //    //// No habilitar los otros                       (22/ago/23 20.18)
    //    //// si el de hacer las dos cosas está habilitado
    //    ////ChkEnviarConfirm.Enabled = !ChkCrearConEmail.Checked;
    //    //if (ChkCrearConEmail.Checked == false)
    //    //{
    //    //    ChkEnviarConfirm.Enabled = false;
    //    //}
    //    //if (ChkEnviarConfirm.Enabled == false)
    //    //{
    //    //    ChkEnviarConfirm.Enabled = false;
    //    //    ChkEnviarConfirm.Checked = false;
    //    //}

    //    //BtnCrearReserva.Enabled = ChkEnviarConfirm.Checked;
    //    //BtnEnviarConfirm.Enabled = ChkEnviarConfirm.Checked;
    //}

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
}
