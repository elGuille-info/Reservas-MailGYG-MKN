﻿// Analizar el email de GYG para crear una reserva.         (21/ago/23 06.48)
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
    // Para el texto de aviso original y el último usado.   (23/sep/23 10.48)
    private string TextoAvisoUltimo { get; set; }
    private string TextoAvisoOriginal { get; set; }

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
        // Copiar el texto de aviso original.               (23/sep/23 10.39)
        TextoAvisoOriginal = TxtAvisoExtra.Text;

        LabelFechaHora.Text = $"{DateTime.Now:dd/MM/yyyy HH:mm:ss}";

        inicializando = false;

        BtnPegarEmail.Image = Properties.Resources.Paste; // Bitmap.FromFile("..\\Resources\\Paste.png");
        BtnLimpiarTexto.Image = Properties.Resources.CleanData; //Bitmap.FromFile("..\\Resources\\CleanData.png");

        TimerHoraStatus.Interval = 990;
        TimerHoraStatus.Enabled = true;

        TimerInicio.Enabled = true;

        RtfEmail.Text = "";

        LimpiarControlesReserva();
        MostrarTamaño();
    }

    private void TimerInicio_Tick(object sender, EventArgs e)
    {
        TimerInicio.Enabled = false;


        if (Height < 900)
        {
            statusStrip1.Dock = DockStyle.None;
            //this.StartPosition = FormStartPosition.Manual;

            Height = 1000;
            Application.DoEvents();

            //this.StartPosition = FormStartPosition.CenterScreen;
            Left = (Screen.PrimaryScreen.Bounds.Width - Width) / 2;
            Top = (Screen.PrimaryScreen.Bounds.Height - Height) / 2;

            Application.DoEvents();
            statusStrip1.Dock = DockStyle.Bottom;
            statusStrip1.Refresh();
            Application.DoEvents();
        }

        ChkIncluirTextoAviso.Checked = false;
        Form1.ActualizarColorEnabled(ChkIncluirTextoAviso, TxtAvisoExtra);

        MostrarTamaño();
    }

    private void FormAnalizaEmail_Resize(object sender, EventArgs e)
    {
        if (inicializando) return;
        MostrarTamaño();
    }

    private void MostrarTamaño()
    {
        LabelStatus.ToolTipText = $"W,H: {Width}, {Height} - Cliente W,H: {ClientSize.Width}, {ClientSize.Height}";
        ToolTip1.SetToolTip(GrbEmail, LabelStatus.ToolTipText);
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
        // Opción para no enviar el email.                  (19/oct/23 15.18)
        ChkNOEnviarEmail.Enabled = false;
        ChkNOEnviarEmail.Checked = false;

        LabelAvisoCambiarFecha.Visible = false;

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

        // Comprobar si es reserva para cambiar de fecha    (20/sep/23 19.28)
        // Comprobar también bad weather                    (23/sep/23 09.50)
        if (re.GYGOption.Contains("to change", StringComparison.OrdinalIgnoreCase) ||
            re.GYGOption.Contains("bad weather", StringComparison.OrdinalIgnoreCase))
        {
            // No comprobar fechas,                         (21/sep/23 09.41)
            // la reserva se dejará para el día que eligió.

            // Cambiar el mensaje por malas previsiones     (25/sep/23 15.24)
            StringBuilder mensajeChange = new StringBuilder();
            if (re.GYGLanguage.Contains("English"))
            {
                //mensajeChange.Append(" *The option you booked is for dates we expect BAD WEATHER conditions in the sea.*");
                mensajeChange.Append(" *The option you have booked is for dates with unfavorable forecasts at sea.*");

                //mensajeChange.Append(" *The option you booked is for 'change to another date' that we expect better weather conditions in the sea.*");
                //mensajeChange.Append(" *Please tell me by WhatsApp message which day is good for you.*");
            }
            else
            {
                mensajeChange.Append(" *La opción que has reservado es para fechas con previsiones desfavorables en el mar.*");

                //mensajeChange.Append(" *La opción que has reservado es para 'cambiar a otra fecha' que las previsiones del mar estén mejor.*");
                //mensajeChange.Append(" *Por favor dime por mensaje de WhatsApp qué día te viene bien.*");
            }
            re.GYGNotas += mensajeChange.ToString();

            LabelAvisoCambiarFecha.Text = $"Es una reserva para cambiar de fecha de {re.GYGFechaHora} a una con mejor tiempo.";
            LabelAvisoCambiarFecha.Visible = true;
        }

        TxtNombre.Text = re.Nombre;
        TxtTelefono.Text = re.Telefono;
        TxtNotas.Text = re.GYGNotas;
        TxtActividad.Text = re.ActividadMostrar; // re.GYGOption; // re.Actividad;
        TxtEmail.Text = re.Email;
        TxtAdultos.Text = re.Adultos.ToString();
        TxtMenores.Text = re.Niños.ToString();
        TxtMenoresG.Text = re.Niños2.ToString();
        inicializando = true;
        TxtFechaHora.Text = $"{re.FechaActividad:dd/MM/yyyy} {re.HoraActividad:hh\\:mm}"; //re.GYGFechaHora; // $"{re.FechaActividad:dd/MM/yyyy} {re.HoraActividad:hh\\mm}";
        inicializando = false;
        TxtPrice.Text = re.GYGPrice;
        TxtLanguage.Text = re.GYGLanguage;
        TxtReference.Text = re.GYGReference;
        TxtPais.Text = re.GYGPais;

        TxtTipo.Text = re.GYGTipo.ToString();

        TxtGYG.Text = $"Option: {re.GYGOption}\r\nDate: {re.GYGFechaHora}\r\nPrice:{re.GYGPrice}";
        TxtID.Text = re.ID.ToString();

        // Comprobar el tipo de reserva y asignar el texto  (09/sep/23 00.04)
        // del botón crear
        //if (re.GYGTipo == Reservas.GYGTipos.Cancelada)
        //{
        //    BtnCrearConEmail.Text = "Cancelar reserva y enviar email";
        //}
        //else if (re.GYGTipo == Reservas.GYGTipos.Modificada)
        //{
        //    BtnCrearConEmail.Text = "Modificar reserva y enviar email";
        //}
        //else
        //{
        //    BtnCrearConEmail.Text = "Crear reserva y enviar email de confirmación";
        //}

        // Aquí no hace falta comprobar el texto.           (19/oct/23 15.47)
        ComprobarConEmail();

        // Comprobar el alquiler con menos de 2 adultos.    (08/sep/23 23.51)
        if (KNDatos.BaseKayak.ActividadesAlquiler().Contains(LaReserva.Actividad))
        {
            DialogResult ret;
            // Los menores de 7+ pagan igual que adultos    (22/mar/24 15.35)
            //if (LaReserva.Adultos < 2)
            if ((LaReserva.Adultos + LaReserva.Niños) < 2)
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
            if (MessageBox.Show("Es un alquiler, antes de continuar comprueba que esté correcta la reserva.",
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
        ChkNOEnviarEmail.Enabled = true;
        ChkNOEnviarEmail.Checked = false;

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
        ChkNOEnviarEmail.Enabled = false;
        ChkNOEnviarEmail.Checked = false;

        BtnCrearConEmail.Enabled = ChkCrearConEmail.Checked;

        // Ocultar el aviso al limpiar campos.              (21/sep/23 09.51)
        LabelAvisoCambiarFecha.Visible = false;

        ChkIncluirTextoAviso.Checked = false;

        LabelVersion.Text = $"v{Form1.AppVersion} ({Form1.AppFechaVersion})";
    }

    private void BtnCrearConEmail_Click(object sender, EventArgs e)
    {
        // Las dos cosas seguidas.                          (22/ago/23 10.28)

        // Comprobar si hay aviso extra a añadir.           (22/sep/23 16.52)
        if (ChkIncluirTextoAviso.Checked)
        {
            if (MessageBox.Show($"Has indicado enviar el texto extra.{CrLf}¿Seguro que está bien ese texto?{CrLf}Pulsa SÍ para enviar, pulsa NO para cambiarlo o quitar la marca.",
                "Crear y confirmar con aviso extra", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                TxtAvisoExtra.Focus();
                return;
            }
        }

        // Copiar el texto que se va a enviar.              (23/sep/23 10.49)
        TextoAvisoUltimo = TxtAvisoExtra.Text;

        // Deshabilitar el botón hasta que finalice.        (27/ago/23 09.58)
        ChkCrearConEmail.Checked = false;
        ChkCrearConEmail.Enabled = false;
        // Aquí no quitar la marca.                         (19/oct/23 15.32)
        //ChkNOEnviarEmail.Checked = false;
        ChkNOEnviarEmail.Enabled = false;
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
            ChkNOEnviarEmail.Checked = false;
            ChkNOEnviarEmail.Enabled = false;

            BtnCrearConEmail.Enabled = ChkCrearConEmail.Checked;

            MessageBox.Show(InfoCrearConEmail.ToString() + CrLf + "Se ha producido un error. No se manda el email.", "Crear reserva y enviar email", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        //QueBoton = sender;

        StatusAnt = LabelStatus.Text;
        LabelStatus.Text = "Enviando el email de confirmación...";
        Application.DoEvents();

        // si se indica no mandar email.                    (19/oct/23 15.23)
        if (ChkNOEnviarEmail.Checked == false)
        {
            EnviarMensajeConfirmacion();
        }

        Application.DoEvents();

        ChkCrearConEmail.Checked = false;
        ChkNOEnviarEmail.Checked = false;
        //ChkNOEnviarEmail.Enabled = false;
        BtnCrearConEmail.Enabled = ChkCrearConEmail.Checked;

        // Si InfoCrearConEmail no tiene nada, mostrar un mensaje normal. (27/may/04 11.07)
        if(InfoCrearConEmail.Length == 0)
        {
            InfoCrearConEmail.AppendLine("Reserva procesada correctamente.");
        }

        MessageBox.Show(InfoCrearConEmail.ToString(), "Crear reserva y enviar email", MessageBoxButtons.OK, MessageBoxIcon.Information);

        // Limpiar donde se pone el texto a analizar.       (09/sep/23 22.15)
        inicializando = true;
        RtfEmail.Text = "";
        inicializando = false;
    }

    private bool ModificarReserva()
    {
        InfoCrearConEmail.Clear();

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
        if (string.IsNullOrWhiteSpace(LaReserva.GYGNotas) == false)
        {
            re.Notas = string.Concat(re.Notas, CrLf, LaReserva.GYGNotas);
        }
        re.HoraActividad = pr.Hora;
        re.FechaActividad = pr.Fecha;
        re.Notas2 = string.Concat("Modificada //", re.Notas2);
        re.Actualizar2();

        InfoCrearConEmail.AppendLine("Se ha modificado la :");
        InfoCrearConEmail.AppendLine($"Booking: '{LaReserva.GYGReference}'");

        // Asignar los valores no en base datos.            (11/sep/23 12.17)
        re.GYGReference = LaReserva.GYGReference;
        re.GYGTipo = LaReserva.GYGTipo;
        re.GYGFechaHora = LaReserva.GYGFechaHora;
        re.GYGOption = LaReserva.GYGOption;
        re.GYGLanguage = LaReserva.GYGLanguage;
        LaReserva = re;

        return false;
    }

    private bool CancelarReserva()
    {
        // Para que no esté vacio si no se manda email.     (27/may/24 11.10)
        InfoCrearConEmail.Clear();

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

        InfoCrearConEmail.AppendLine("Se ha cancelado la reserva:");
        InfoCrearConEmail.AppendLine($"Booking: '{LaReserva.GYGReference}'");

        // Asignar los valores no en base datos.            (11/sep/23 12.15)
        re.GYGReference = LaReserva.GYGReference;
        re.GYGTipo = LaReserva.GYGTipo;
        re.GYGFechaHora = LaReserva.GYGFechaHora;
        re.GYGOption = LaReserva.GYGOption;
        re.GYGLanguage = LaReserva.GYGLanguage;
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

        // Cargar el Config del año de la actividad.        (05/oct/23 14.24)
        // Asegurse que KNDatos.Config.Current está inicializada.
        if (KNDatos.Config.Current == null)
        {
            KNDatos.Config.LeerDatos(re.FechaActividad.Year, "ReservasGYG");
        }

        Producto pr;
        TimeSpan hora = re.HoraActividad;

        // Si es alquiler no habrá producto a las :05,      (07/sep/23 08.10)
        // buscar a las 00
        if (re.Actividad == "KAYAK")
        {
            hora = new TimeSpan(re.HoraActividad.Hours, 0, 0);
            // Si es alquiler, buscar con 1h de duración.   (03/jun/24 14.31)
            pr = Producto.Buscar(re.FechaActividad, hora, re.Actividad, 1);
        }
        else // Para las rutas o tablas
        {
            pr = Producto.Buscar(re.FechaActividad, hora, re.Actividad);
        }
        // Buscar el producto.
        //pr = Producto.Buscar(re.FechaActividad, hora, re.Actividad);
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
            // Avisar que es un producto por libre.         (05/oct/23 14.11)
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Actividad: {pr.Actividad}");
            sb.AppendLine($"Fecha: {pr.Fecha:dddd dd/MM/yyyy}");
            sb.AppendLine($"Hora: {pr.Hora:hh\\:mm}");
            sb.AppendLine($"Pax: {re.TotalPax()}");
            MessageBox.Show($"El producto no existe, se usará un producto 'por libre':{CrLf}{sb}",
                            "Se ha creado una reserva por libre", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
                sb.Append(Properties.Resources.IMPORTANTE_ALQUILER.Replace(CrLf, "<br/>"));
            }
            else
            {
                // Solo mandar el IMPORTANTE                (26/sep/23 21.15)
                if (enIngles)
                {
                    sb.Append(Properties.Resources.IMPORTANTE_EN.Replace(CrLf, "<br/>"));
                }
                else
                {
                    sb.Append(Properties.Resources.IMPORTANTE_ES.Replace(CrLf, "<br/>"));
                }


                //
                // TODO: Después habrá que hacer varios textos para temporada alta y baja
                // y para cuando quiten el bus.
                //

                // Mandar el texto según el idioma y hora   (22/ago/23 10.58)
                //if (re.HoraActividad.Hours == 9)
                //{
                //    if (enIngles)
                //    {
                //        sb.Append(Properties.Resources.IMPORTANTE_EN_09_30.Replace(CrLf, "<br/>"));
                //    }
                //    else
                //    {
                //        sb.Append(Properties.Resources.IMPORTANTE_ES_09_30.Replace(CrLf, "<br/>"));
                //    }
                //}
                //else if (re.HoraActividad.Hours == 10 || re.HoraActividad == new TimeSpan(11, 0, 0))
                //{
                //    if (enIngles)
                //    {
                //        sb.Append(Properties.Resources.IMPORTANTE_EN_10_30_11_00.Replace(CrLf, "<br/>"));
                //    }
                //    else
                //    {
                //        sb.Append(Properties.Resources.IMPORTANTE_ES_10_30_11_00.Replace(CrLf, "<br/>"));
                //    }
                //}
                //else
                //{
                //    if (enIngles)
                //    {
                //        sb.Append(Properties.Resources.IMPORTANTE_EN.Replace(CrLf, "<br/>"));
                //    }
                //    else
                //    {
                //        sb.Append(Properties.Resources.IMPORTANTE_ES.Replace(CrLf, "<br/>"));
                //    }
                //}
            }

            // En temporada alta, hasta mediados septiembre (18/sep/23 05.18)
            if (DateTime.Today <= new DateTime(2023, 9, 15))
            {
                sb.Append(Properties.Resources.IMPORTANTE_Lee_esto_Maro.Replace(CrLf, "<br/>"));
            }

            // Indicar siempre que hagan la reseña.         (11/sep/23 10.25)
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

            // Si se ha indicado enviar el mensaje extra.   (22/sep/23 17.04)
            if (ChkIncluirTextoAviso.Checked)
            {
                sb.Append("<br/>");
                sb.Append("<br/>");
                if (enIngles)
                {
                    sb.Append("<b>Please read this:</b><br/>");
                }
                else
                {
                    sb.Append("<b>Por favor lee esto:</b><br/>");
                }
                sb.Append(TxtAvisoExtra.Text.Replace(CrLf, "<br/>"));
            }
        }

        // Añadir la firma de Kayak Makarena                (18/sep/23 05.33)
        MailGYG.FirmaMakarena(sb, enIngles);

        //TODO: Usar MailGYG.TextoMensajeConfirmacion       (18/sep/23 10.31)

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

        ChkNOEnviarEmail.Enabled = ChkCrearConEmail.Checked;
        BtnCrearConEmail.Enabled = ChkCrearConEmail.Checked;
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

    private void TxtLanguage_TextChanged(object sender, EventArgs e)
    {
        if (inicializando) return;
        if (LaReserva == null) return;

        LaReserva.GYGLanguage = TxtLanguage.Text;
    }

    private void TxtNotas_TextChanged(object sender, EventArgs e)
    {
        // Si se modifican las notas.                       (11/sep/23 23.42)
        if (inicializando) return;
        if (LaReserva == null) return;

        LaReserva.GYGNotas = TxtNotas.Text;
    }

    private void TxtTelefono_TextChanged(object sender, EventArgs e)
    {
        // Si se modifica el teléfono.                      (11/sep/23 23.43)
        if (inicializando) return;
        if (LaReserva == null) return;

        LaReserva.Telefono = TxtTelefono.Text;
    }

    private void TxtPais_TextChanged(object sender, EventArgs e)
    {
        // Si se modifica el país.                          (11/sep/23 23.45)
        if (inicializando) return;
        if (LaReserva == null) return;

        LaReserva.GYGPais = TxtPais.Text;
    }

    private void TxtEmail_TextChanged(object sender, EventArgs e)
    {
        // Si se modifica el email.                         (11/sep/23 23.49)
        if (inicializando) return;
        if (LaReserva == null) return;

        LaReserva.Email = TxtEmail.Text;
    }

    private void TxtFechaHora_TextChanged(object sender, EventArgs e)
    {
        //LabelAvisoCambiarFecha.Visible = false;
        if (inicializando) return;
        if (LaReserva == null) return;

        if (LaReserva.GYGFechaHora == TxtFechaHora.Text) return;

        if (string.IsNullOrEmpty(TxtFechaHora.Text)) return;

        LabelAvisoCambiarFecha.Text = $"Has cambiado la fecha, si la estás escribiendo pulsa ENTER para confirmarla y comprobar que es correcta:{CrLf}{TxtFechaHora.Text}{CrLf}Fecha Reserva: {LaReserva.FechaActividad:dddd dd/MM/yyyy}{CrLf}Hora: {LaReserva.HoraActividad:hh\\:mm}";
        LabelAvisoCambiarFecha.Visible = true;
    }

    private void TxtFechaHora_KeyUp(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            CambiarFechaGYG();
        }
    }

    private void TxtFechaHora_Leave(object sender, EventArgs e)
    {
        CambiarFechaGYG();
    }

    private void CambiarFechaGYG()
    {
        LabelAvisoCambiarFecha.Visible = false;

        if (inicializando) return;
        if (LaReserva == null) return;

        if (LaReserva.GYGFechaHora == TxtFechaHora.Text) return;

        LaReserva.GYGFechaHora = TxtFechaHora.Text;
        var re = LaReserva;
        var fecGYG = re.GYGFechaHora;
        int k = re.GYGFechaHora.IndexOf("(");
        if (k > -1)
        {
            fecGYG = re.GYGFechaHora.Substring(0, k).Trim();
        }

        // Si la fecha no tiene contenido válido, devolver nulo. (22/ago/23 20.22)
        if (string.IsNullOrWhiteSpace(fecGYG))
        {
            return;
        }

        // Date: 08 September 2023, 16:15 (04:15pm)

        //var fec = DateTime.ParseExact(fecGYG, "dd MMMM yyyy, HH:mm", System.Globalization.CultureInfo.InvariantCulture);
        DateTime fec;

        try
        {
            fec = DateTime.ParseExact(fecGYG, "dd/MM/yyyy HH:mm", System.Globalization.CultureInfo.InvariantCulture);
            re.FechaActividad = fec.Date;
            re.HoraActividad = fec.TimeOfDay;
        }
        catch (Exception ex)
        {
            LabelAvisoCambiarFecha.Text = $"Error en la fecha indicada:{CrLf}{TxtFechaHora.Text}{CrLf}{ex.Message}.";
            LabelAvisoCambiarFecha.Visible = true;
        }
    }

    // Las opciones del menú contextual de TxtAvisoExtra    (23/sep/23 10.50)

    private void MenuPegarTextoOriginal_Click(object sender, EventArgs e)
    {
        TxtAvisoExtra.Text = TextoAvisoOriginal;
    }

    private void MnuPegarÚltimoTextoEnviado_Click(object sender, EventArgs e)
    {
        // Comprobar que tenga contenido asignado.
        if (string.IsNullOrEmpty(TextoAvisoUltimo) == false)
        {
            TxtAvisoExtra.Text = TextoAvisoUltimo;
        }
    }

    private void ContextMenuTextoAviso_Opening(object sender, CancelEventArgs e)
    {
        MnuPegarÚltimoTextoEnviado.Enabled = !string.IsNullOrEmpty(TextoAvisoUltimo);
    }

    // Ajustar el color del textBox si está o no activado   (23/sep/23 10.57)

    private void ChkIncluirTextoAviso_CheckedChanged(object sender, EventArgs e)
    {
        Form1.ActualizarColorEnabled(ChkIncluirTextoAviso, TxtAvisoExtra);
    }

    private void ChkNOEnviarEmail_CheckedChanged(object sender, EventArgs e)
    {
        ComprobarConEmail();
    }

    private void ComprobarConEmail()
    {
        string noEnviar;
        noEnviar = ChkNOEnviarEmail.Checked ? "NO " : "";

        if (ChkNOEnviarEmail.Checked )
        {
            ChkCrearConEmail.Text = "Habilitar crear-email ";
        }
        else
        {
            ChkCrearConEmail.Text = "Habilitar crear+email";
        }
        
        if (LaReserva.GYGTipo == Reservas.GYGTipos.Cancelada)
        {
            BtnCrearConEmail.Text = $"Cancelar reserva y {noEnviar}enviar email";
        }
        else if (LaReserva.GYGTipo == Reservas.GYGTipos.Modificada)
        {
            BtnCrearConEmail.Text = $"Modificar reserva y {noEnviar}enviar email";
        }
        else
        {
            BtnCrearConEmail.Text = $"Crear reserva y {noEnviar}enviar email de confirmación";
        }

        //if (ChkNOEnviarEmail.Checked)
        //{
        //    if (BtnCrearConEmail.Text.Contains(" y enviar email"))
        //    {
        //        int j = BtnCrearConEmail.Text.IndexOf(" y enviar email");
        //        if (j > -1)
        //        {
        //            BtnCrearConEmail.Text = BtnCrearConEmail.Text.Substring(0, j);
        //        }
        //    }
        //    else
        //    {
        //        BtnCrearConEmail.Text += " y enviar email";
        //    }
        //}
        //else
        //{
        //    if (BtnCrearConEmail.Text.Contains(" y enviar email") == false)
        //    {
        //        BtnCrearConEmail.Text += " y enviar email";
        //    }
        //}
    }
}
