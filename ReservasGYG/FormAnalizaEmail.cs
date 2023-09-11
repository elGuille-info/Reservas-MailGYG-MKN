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
    private string StatusAnt { get; set; }
    private Reservas LaReserva { get; set; }
    private StringBuilder InfoCrearConEmail { get; set; } = new StringBuilder();


    public static FormAnalizaEmail Current { get; set; }
    public FormAnalizaEmail()
    {
        InitializeComponent();
        Current = this;
    }
    private void FormAnalizaEmailGYG_Load(object sender, EventArgs e)
    {
        LabelFechaHora.Text = $"{DateTime.Now:dd/MM/yyyy HH:mm:ss}";

        inicializando = false;

        BtnPegarEmail.Image = Properties.Resources.Paste; // Bitmap.FromFile("..\\Resources\\Paste.png");
        BtnLimpiarTexto.Image = Properties.Resources.CleanData; //Bitmap.FromFile("..\\Resources\\CleanData.png");

        TimerHoraStatus.Interval = 990;
        TimerHoraStatus.Enabled = true;

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

        // En AnalizarEmail se comprueba si es desde        (08/sep/23 15.14)
        // la página de bookings.
        re = MailGYG.AnalizarEmail(RtfEmail.Text);

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

        TxtTipo.Text = re.GYGTipo.ToString();

        TxtGYG.Text = $"Option: {re.GYGOption}\r\nDate: {re.GYGFechaHora}\r\nPrice:{re.GYGPrice}";
        TxtID.Text = re.ID.ToString();

        // Comprobar el tipo de reserva y asignar el texto  (09/sep/23 00.04)
        // del botón crear
        if (re.GYGTipo == Reservas.GYGTipos.Cancelada)
        {
            BtnCrearConEmail.Text = "Cancelar reserva y enviar email";
        }
        else if (re.GYGTipo == Reservas.GYGTipos.Modificada)
        {
            BtnCrearConEmail.Text = "Modificar reserva y enviar email";
        }
        else
        {
            BtnCrearConEmail.Text = "Crear reserva y enviar email de confirmación";
        }

        // Comprobar el alquiler con menos de 2 adultos.    (08/sep/23 23.51)
        if (KNDatos.BaseKayak.ActividadesAlquiler().Contains(LaReserva.Actividad))
        {
            DialogResult ret;
            if (LaReserva.Adultos < 2)
            {
                ret = MessageBox.Show("Es un alquiler para menos de 2 pax (adultos o menores mayor de 6 años)." + CrLf +
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
        // Solo si no es cancelar.                          (09/sep/23 03.48)
        else if (re.GYGTipo != Reservas.GYGTipos.Cancelada)
        {
            // Comprobar si solo hay menores, y avisar.     (08/sep/23 23.31)
            if (re.Adultos < 1)
            {
                MessageBox.Show("¡ATENCIÓN! La reserva no incluye ningún adulto." + CrLf +
                                "Habría que avisar al cliente de que se debe incluir al menos un adulto.",
                                "Analizar email de GYG", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        ChkCrearConEmail.Enabled = true;
        ChkCrearConEmail.Checked = false;

        // Si es desde booking avisar que revise las cosas antes de guardar.
        //if (desdeBooking)
        if (re.GYGTipo == Reservas.GYGTipos.Booking)
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

        LabelVersion.Text = $"v{Form1.AppVersion} ({Form1.AppFechaVersion})";
    }

    private void BtnCrearConEmail_Click(object sender, EventArgs e)
    {
        // Las dos cosas seguidas.                      (22/ago/23 10.28)

        // Deshabilitar el botón hasta que finalice.        (27/ago/23 09.58)
        ChkCrearConEmail.Checked = false;
        ChkCrearConEmail.Enabled = false;
        BtnCrearConEmail.Enabled = ChkCrearConEmail.Checked;

        // Limpiar el contenido para que no se acumule.     (11/sep/23 10.57)
        InfoCrearConEmail.Clear();

        if (LaReserva == null)
        {
            MessageBox.Show("La reserva no está asignada.", "No hay reserva", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            return;
        }

        StatusAnt = LabelStatus.Text;
        LabelStatus.Text = "Creando la reserva...";
        Application.DoEvents();

        bool res;
        // Comporbar si es crear, cancelar y modificar      (09/sep/23 00.20)
        if (LaReserva.GYGTipo == Reservas.GYGTipos.Cancelada)
        {
            // Cancelar la reserva
            res = CancelarReserva();
        }
        else if (LaReserva.GYGTipo == Reservas.GYGTipos.Modificada)
        {
            // Modificar la reserva
            res = ModificarReserva();
        }
        else
        {
            // Crear la reserva
            res = CrearReserva();
        }

        LabelStatus.Text = StatusAnt;
        Application.DoEvents();

        // Si devuelve true, no continuar con el envío del email.   (26/ago/23 00.06)
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

        // Limpiar donde se pone el texto a analizar.       (09/sep/23 22.15)
        inicializando = true;
        RtfEmail.Text = "";
        inicializando = false;
    }

    private bool ModificarReserva()
    {
        // Buscar la reserva con el booking indicado.
        var re = Reservas.Buscar($"Notas like '%{LaReserva.GYGReference}%' and activa=1");
        if (re == null)
        {
            MessageBox.Show("No existe la reserva a modificar:" + CrLf +
                            $"Booking: '{LaReserva.GYGReference}'", "No hay reserva", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            return true;
        }
        // Puede cambiar la fecha y hora.
        // Puede cambiar el número de pax.
        Producto pr;

        // Actualizar el producto de la reserva original
        if (re.idProducto != -2)
        {
            pr = Producto.Buscar(re.idProducto);
            if (pr == null)
            {
                MessageBox.Show("El producto de la reserva no existe:" + CrLf +
                                $"ID producto: '{re.idProducto}'" + CrLf +
                                $"Actividad: {re.ActividadMostrar} {re.FechaActividad:dd/MM/yyyy} {re.HoraActividad:hh\\:mm}",
                                "No hay producto", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return true;
            }
            pr.TotalPax -= re.TotalPax();
            pr.Actualizar2();
        }
        // Buscar el producto para la nueva fecha y hora
        pr = Producto.Buscar(LaReserva.FechaActividad, LaReserva.HoraActividad, re.Actividad);
        if (pr == null)
        {
            // Asignarlo por libre
            pr = new Producto();
            pr.ID = -2;
            pr.Fecha = LaReserva.FechaActividad;
            pr.Hora = LaReserva.HoraActividad;
            pr.Actividad = re.Actividad;
        }
        re.idProducto = pr.ID;
        // Si ha cambiado el número de pax:
        if (LaReserva.Adultos != re.TotalPax())
        {
            var ret = MessageBox.Show("El número de participantes no coincide con el de la reserva original:" + CrLf +
                            $"Antes: {re.TotalPax()}, ahora: '{LaReserva.Adultos}'" + CrLf +
                            "Pulsa SÍ para usar los pax nuevos (se pondrán todos como adultos)." + CrLf +
                            "Pulsa NO para dejar los pax que ya había.",
                            "Modificar la reserva", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (ret == DialogResult.Yes)
            {
                re.Adultos = LaReserva.Adultos;
                re.Niños = 0;
                re.Niños2 = 0;
            }
        }
        // Actualizar el producto
        if (pr.ID > 0)
        {
            pr.TotalPax += re.TotalPax();
            pr.Actualizar2();
        }
        re.HoraActividad = pr.Hora;
        re.FechaActividad = pr.Fecha;
        re.Notas2 = string.Concat("Modificada //", re.Notas2);
        re.Actualizar2();

        // Asignar los valores no en base datos.            (11/sep/23 12.17)
        re.GYGReference = LaReserva.GYGReference;
        re.GYGTipo = LaReserva.GYGTipo;
        re.GYGFechaHora = LaReserva.GYGFechaHora;
        re.GYGOption = LaReserva.GYGOption;
        LaReserva = re;

        return false;
    }

    private bool CancelarReserva()
    {
        // Buscar la reserva con el booking indicado.
        var re = Reservas.Buscar($"Notas like '%{LaReserva.GYGReference}%' and activa=1");
        if (re == null)
        {
            MessageBox.Show("No existe la reserva a cancelar:" + CrLf +
                            $"Booking: '{LaReserva.GYGReference}'", "No hay reserva", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            return true;
        }
        // Cancelar la reserva
        re.PagoACta = 0;
        re.ModoPagoACta = "";
        re.Documento = "";
        re.Notas2 = string.Concat("Cancelada y devolución por GYG //", re.Notas2);
        re.CanceladaCliente = true;

        // Actualizar los pax.
        // Solo si el id no es -2
        if (re.idProducto > 0)
        {
            var pr = Producto.Buscar(re.idProducto);
            if (pr == null)
            {
                MessageBox.Show("El producto de la reserva no existe:" + CrLf +
                                $"ID producto: '{re.idProducto}'" + CrLf +
                                $"Actividad: {re.ActividadMostrar} {re.FechaActividad:dd/MM/yyyy} {re.HoraActividad:hh\\:mm}",
                                "No hay producto", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return true;
            }
            pr.TotalPax -= re.TotalPax();
            pr.Actualizar2();
        }
        re.Actualizar2();

        // Asignar los valores no en base datos.            (11/sep/23 12.15)
        re.GYGReference = LaReserva.GYGReference;
        re.GYGTipo = LaReserva.GYGTipo;
        re.GYGFechaHora = LaReserva.GYGFechaHora;
        re.GYGOption = LaReserva.GYGOption;
        LaReserva = re;

        return false;
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

        Producto pr;
        TimeSpan hora = re.HoraActividad;

        // Si es alquiler no habrá producto a las :05,      (07/sep/23 08.10)
        // buscar a las 00
        if (re.Actividad == "KAYAK")
        {
            hora = new TimeSpan(re.HoraActividad.Hours, 0, 0);
        }
        // Buscar el producto.
        pr = Producto.Buscar(re.FechaActividad, hora, re.Actividad);
        if (pr == null)
        {
            // Si no existe el producto, ponerlo por libre. (07/sep/23 08.13)
            pr = new Producto
            {
                ID = -2,
                Actividad = re.Actividad,
                Hora = hora, // re.HoraActividad;
                Fecha = re.FechaActividad,
                Activo = true,
                TotalPax = 0,
                MaxPax = re.TotalPax(),
                Duracion = BaseKayak.DuracionActividad(re.Actividad),
                PrecioAdulto = BaseKayak.PrecioAdultoActividad(re.Actividad),
                PrecioNiño = BaseKayak.PrecioNiñoActividad(re.Actividad)
            };
        }
        // Actualizar el producto.                          (23/ago/23 20.06)
        pr.TotalPax += re.TotalPax();
        if (pr.ID > 0)
        {
            pr.Actualizar2();
        }

        // Poner la hora por si es alquiler.                (08/sep/23 15.36)
        re.HoraActividad = pr.Hora;
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

        re.Dobles = dobles;
        re.Individuales = individuales;
        re.Tanque = tanques;
    }

    /// <summary>
    /// Enviar el email de confirmación según sea nueva, modificar o cancelar.
    /// </summary>
    /// <returns>True si no se mandó, false si todo fue bien.</returns>
    private bool EnviarMensajeConfirmacion()
    {
        StringBuilder sb = new StringBuilder();

        var re = LaReserva;
        if (re == null)
        {
            MessageBox.Show($"ERROR la reserva es nula. No se puede mandar el mensaje.",
                            "Error al enviar el email de la reserva",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
            return true;
        }

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
            asunto = $"Cancelación reserva - {re.GYGReference}";
            sb.AppendLine("Reserva cancelada // Booking cancelled.");
            sb.AppendLine();
            sb.AppendLine($"GetYourGuide Booking # {re.GYGReference}");
            sb.AppendLine($"{re.ActividadMostrar} {re.FechaActividad:dd/MM/yyyy} {re.HoraActividad:hh\\:mm}");
            sb.AppendLine();
            sb.AppendLine($"Número de reserva MKN: {re.ID:#,###}");
            sb.AppendLine();
            sb.AppendLine();
        }
        else
        {
            if (LaReserva.GYGTipo == Reservas.GYGTipos.Modificada)
            {
                asunto = $"Modificación reserva - {re.GYGReference}";
            }
            else
            {
                asunto = $"Booking - S271506 - {re.GYGReference}";
            }

            sb.Append(DatosVPWiz.ResumenReserva(esWeb: false, enIngles: enIngles));
            sb.AppendLine();
            sb.AppendLine();

            // Para alquiler, mandar otro texto diferente.  (09/sep/23 22.08)
            if (KNDatos.BaseKayak.ActividadesAlquiler().Contains(re.Actividad))
            {
                sb.Append(Properties.Resources.IMPORTANTE_ALQUILER.Replace(CrLf, "<br/>"));
            }
            else
            {
                // Mandar el texto según el idioma.         (22/ago/23 10.58)
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
            }
            // Indicar siempre que hagan la reseña.         (11/sep/23 10.25)
            //// Si es para el mismo día de la actividad.     (24/ago/23 06.24)
            //if (DateTime.Today == re.FechaActividad)
            sb.Append("<br/>");
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
        // No tenía los cambios de línea, añado el teléfono (08/sep/23 13.55)
        sb.Append("Kayak Makarena<br/>");
        sb.Append("iMessage / WhatsApp: +34 645 76 16 89<br/>");
        sb.Append("https://kayakmakarena.com<br/>");

        //var asunto = $"Booking - S271506 - {re.GYGReference}";
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
