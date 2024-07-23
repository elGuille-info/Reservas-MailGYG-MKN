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
using System.ComponentModel;

namespace ReservasGYG;

public partial class Form1 : Form
{

    // La descripción para las copias de seguridad. (21/Oct/21)
    // Se usará como descripción lo que esté entre comillas dobles (incluidos los espacios).
    // Ya que se comprueba que empiece con: [COPIAR]AppDescripcionCopia = y unas comillas dobles.

    // Intentar no pasar de estas marcas: 60 caracteres. 2         3         4         5         6
    //                                ---------|---------|---------|---------|---------|---------|
    //[COPIAR]AppDescripcionCopia = " no duplicar email al enviar fotos"
    // BuscarClientes mostrar reservas en la pagina

    /// <summary>
    /// La versión de la aplicación.
    /// </summary>
    public static string AppVersion { get; } = "1.0.128";

    /// <summary>
    /// La versión del fichero (la revisión)
    /// </summary>
    public static string AppFileVersion { get; } = "1.0.128.0";

    /// <summary>
    /// La fecha de última actualización
    /// </summary>
    public static string AppFechaVersion { get; } = "23-jul-2024";


    public static Form1 Current { get; set; }

    // Para el texto de aviso original y el último usado.   (23/sep/23 10.48)
    private string TextoAvisoUltimo { get; set; }
    private string TextoAvisoOriginal { get; set; }

    //
    // Métodos estáticos/compartidos
    //

    public static void ActualizarColorEnabled(CheckBox ChkIncluirTextoAviso, TextBox TxtAvisoExtra)
    {
        TxtAvisoExtra.Enabled = ChkIncluirTextoAviso.Checked;
        if (TxtAvisoExtra.Enabled)
        {
            TxtAvisoExtra.BackColor = Color.White;
        }
        else
        {
            TxtAvisoExtra.BackColor = Color.MintCream;
        }
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

        //   0          1         2           3          4      5            6           7           8          9         10       11
        //{ "Booking", "Nombre", "Teléfono", "Reserva", "PAX", "Cancelada", "H.Salida", "H.Vuelta", "Control", "Vuelta", "Email", "Notas" };
        LvwSinEmail.Columns[0].Width = 160; // booking
        LvwSinEmail.Columns[1].Width = 400; // Nombre
        LvwSinEmail.Columns[2].Width = 160; // 150 Teléfono
        LvwSinEmail.Columns[3].Width = 260; // Reserva
        LvwSinEmail.Columns[4].Width = 160; // pax
        LvwSinEmail.Columns[10].Width = 300; // Email
        LvwSinEmail.Columns[11].Width = 700; // 500 Notas
        LvwSinEmail.Columns[5].Width = 96; // Cancelada
        LvwSinEmail.Columns[6].Width = 80; // H.Salida
        LvwSinEmail.Columns[7].Width = 80; // H.Vuelta
        LvwSinEmail.Columns[8].Width = 76; // Control
        LvwSinEmail.Columns[9].Width = 64; // Vuelta
    }

    /// <summary>
    /// Asignar los datos de las reservas sin email al listView indicado.
    /// </summary>
    /// <param name="col"></param>
    /// <param name="LvwSinEmail"></param>
    public static void AsignarListView(List<ApiReservasMailGYG.ReservasGYG> col, ListView LvwSinEmail)
    {
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
    /// Copiar en el portapales el campo correspondiente al menú indicado.
    /// </summary>
    /// <param name="sender">El menú usado para saber qué copiar.</param>
    /// <param name="LvwSinEmail">El listview con el contenido a copiar.</param>
    public static void CopiarDeLvw(object sender, ListView LvwSinEmail)
    {
        if (LvwSinEmail.SelectedIndices.Count == 0) return;

        // Comprobar que no sea nulo.                       (16/sep/23 17.29)
        if (sender == null) return;

        if (sender is not ToolStripMenuItem mnu) return;

        // Los nombres de los menús contextuales:
        // MnuCopiarBooking, MnuCopiarNombre, MnuCopiarTelefono, MnuCopiarReserva, MnuCopiarPax, MnuCopiarEmail, MnuCopiarNotas, MnuCopiarTodo, MnuCopiarTodoConCr
        // Las columnas:
        //{ "Booking", "Nombre", "Teléfono", "Reserva", "PAX", "Cancelada", "H.Salida", "H.Vuelta", "Control", "Vuelta", "Email", "Notas" };

        int copiarTodo = 99;
        //int index = -1;
        //if (mnu.Name == "MnuCopiarBooking") index = 0;
        //if (mnu.Name == "MnuCopiarNombre") index = 1;
        //if (mnu.Name == "MnuCopiarTelefono") index = 2;
        //if (mnu.Name == "MnuCopiarReserva") index = 3;
        //if (mnu.Name == "MnuCopiarPax") index = 4;
        //if (mnu.Name == "MnuCopiarEmail") index = 5;
        //if (mnu.Name == "MnuCopiarNotas") index = 6;
        //if (mnu.Name == "MnuCopiarTodo") index = copiarTodo;
        //if (mnu.Name == "MnuCopiarTodoConCr") index = copiarTodo + 1;

        int index = ApiReservasMailGYG.ReservasGYG.IndexColumna(mnu.Name);
        if (index == -1) return;

        StringBuilder sb = new StringBuilder();
        if (index >= copiarTodo)
        {
            var it = LvwSinEmail.Items[LvwSinEmail.SelectedIndices[0]];
            for (int i = 0; i < it.SubItems.Count; i++)
            {
                sb.Append(ApiReservasMailGYG.ReservasGYG.Columnas[i]);
                sb.Append(": ");
                sb.Append(it.SubItems[i].Text);
                // Si es copiar todo con CR,                (16/sep/23 03.20)
                // añadir un cambio de línea.
                // Si no, añair el punto y coma.            (16/sep/23 03.23)
                if (index == copiarTodo + 1)
                {
                    sb.AppendLine();
                }
                else
                {
                    if (i < it.SubItems.Count - 1)
                    {
                        sb.Append("; ");
                    }
                }
            }
        }
        else
        {
            sb.Append(LvwSinEmail.Items[LvwSinEmail.SelectedIndices[0]].SubItems[index].Text);
        }

        CopiarPortapapeles(sb.ToString());
    }

    /// <summary>
    /// Comprueba las reservas de la fecha indicada sin email y las añade al listview.
    /// </summary>
    /// <param name="fecha"></param>
    /// <param name="LvwSinEmail"></param>
    /// <returns></returns>
    public static int ComprobarEmailsReservas(DateTime fecha, ListView LvwSinEmail, bool conAlquileres)
    {
        var col = MailGYG.ComprobarEmails(fecha, conAlquileres);

        AsignarListView(col, LvwSinEmail);

        return col.Count;
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

    //
    // Métodos y variables de instancia
    //

    private bool inicializando = true;

    public Form1()
    {
        InitializeComponent();
        Current = this;
    }

    private void Form1_Load(object sender, EventArgs e)
    {
        inicializando = false;

        // Mostrar la fecha y hora actual.                      (05/jul/24 12.54)
        LabelFechaHora.Text = $"{DateTime.Now:dd/MM/yyyy HH:mm:ss}";

        // Iniciar el temporizador                              (05/jul/24 12.57)
        TimerHoraStatus.Interval = 990;
        TimerHoraStatus.Enabled = true;

        // Copiar el texto de aviso original.               (23/sep/23 10.39)
        TextoAvisoOriginal = TxtAvisoExtra.Text;

        AsignarColumnasLvw(LvwSinEmail, asignarColumnas: true);
        LabelInfoListView.Text = "";

        HabilitarBotones(false);

        DateTimePickerGYG.Value = DateTime.Today;

        TimerInicioForm1.Enabled = true;
    }

    private void TimerInicioForm1_Tick(object sender, EventArgs e)
    {
        TimerInicioForm1.Enabled = false;
        ChkIncluirTextoAviso.Checked = false;
        ActualizarColorEnabled(ChkIncluirTextoAviso, TxtAvisoExtra);

        // Mostrar la versión de la app.                        (05/jul/24 12.52)
        LabelVersion.Text = $"v{Form1.AppVersion} ({Form1.AppFechaVersion})";

        Form1_Resize(null, null);
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

    private void Form1_Resize(object sender, EventArgs e)
    {
        if (inicializando) return;

        // Ajustar el ancho de las columnas del listview.   (27/ago/23 17.54)
        //var w = (GrbOpcionesFecha.ClientSize.Width - 60) / 3;
        //LvwSinEmail.Columns[0].Width = (GrbOpcionesFecha.ClientSize.Width - 60) - w;
        //LvwSinEmail.Columns[1].Width = w;
        AsignarColumnasLvw(LvwSinEmail, asignarColumnas: false);

        GrbAvisos.Width = GrbOpcionesFecha.ClientSize.Width - 12;

        // Ajustar el tamaño de MañanaEs y HoyEs.           (02/sep/23 19.20)
        int w;

        w = (GrbOpcionesFecha.ClientSize.Width - 30) / 2;
        BtnComprobarSinMail.Width = w; // BtnMañanaEs.Width;
        BtnFotos.Width = w; // BtnHoyEs.Width;
        BtnFotos.Left = BtnComprobarSinMail.Left + w + 12;

        BtnReservasSinSalida.Width = w;
        BtnMostrarReservas.Width = w;
        BtnMostrarReservas.Left = BtnFotos.Left;

        w = (GrbAvisos.ClientSize.Width - 30) / 2;
        BtnMañanaEs.Width = w;
        BtnHoyEs.Width = w;
        BtnHoyEs.Left = BtnMañanaEs.Left + w + 12;

        //w = (GrbOpcionesFecha.ClientSize.Width - 45) / 3;
        w = (GrbAvisos.ClientSize.Width - 45) / 3;
        BtnAlerta1.Width = w;
        BtnAlerta2.Width = w;
        BtnAlerta3.Width = w;
        BtnAlerta2.Left = BtnAlerta1.Left + w + 12;
        BtnAlerta3.Left = BtnAlerta2.Left + w + 12;
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
        BtnMañanaEs.Enabled = habilitar;
        BtnHoyEs.Enabled = habilitar;
        BtnAlerta1.Enabled = habilitar;
        BtnAlerta2.Enabled = habilitar;
        BtnAlerta3.Enabled = habilitar;
    }

    private void BtnMañanaEs_Click(object sender, EventArgs e)
    {
        DateTime fecha = DateTimePickerGYG.Value.Date;
        DialogResult ret;

        // Comprobar que la fecha no sea hoy.               (27/ago/23 14.11)
        if (fecha.Date == DateTime.Today)
        {
            MessageBox.Show("No se pueden mandar el mensaje MANAÑA ES EL DÍA a las reservas de HOY." + CrLf + CrLf +
                            $"Has indicado mandar el mensaje MAÑANA es el día a las reservas de hoy {fecha.Date:dddd dd/MM/yyyy}" + CrLf +
                            "Debes elegir la opción HOY en el día para mandar en el mismo día de la actividad." + CrLf +
                            "O bien cambiar la fecha seleccionada.",
                            "Mandar recordatorio de que MAÑANA es el día", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            DateTimePickerGYG.Focus();
            return;
        }

        ret = MessageBox.Show($"¿Quieres mandar el mensaje de 'Mañana es el día' a las reservas del {fecha.Date:dddd dd/MM/yyyy}?" + CrLf +
                              "Pulsa SÍ para mandar el mensaje de MAÑANA ES EL DÍA a las reservas de esa fecha." + CrLf +
                              $"Pulsa NO no mandar nada y seleccionar otra fecha.",
                              "Mandar recordatorio de que MAÑANA es el día", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        if (ret == DialogResult.No)
        {
            DateTimePickerGYG.Focus();
            return;
        }


        // Comprobar que todos tengan email.                (24/ago/23 04.41)
        //string res = ComprobarEmails(fecha);
        // Usando la nueva función.                         (27/ago/23 17.26)
        var res = ComprobarEmailsReservas(fecha, LvwSinEmail, ChkConAlquileres.Checked);

        if (res > 0)
        {
            LabelInfoListView.Text = $"Hay {res} {res.Plural("reserva")} del {fecha:dddd dd/MM/yyyy} sin emails.";
            MessageBox.Show($"Hay {res} {res.Plural("reserva")} del {fecha:dddd dd/MM/yyyy} sin emails.{CrLf}No se puede continuar hasta que lo soluciones.",
                            "Mandar recordatorio de que MAÑANA es el día", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }
        else
        {
            LabelInfoListView.Text = $"Todas las reservas del '{fecha:dddd dd/MM/yyyy}' tiene asignado el email.";
        }

        //var lisRes = Reservas.TablaCol($"SELECT * FROM Reservas Where idDistribuidor=10 and Activa=1 and CanceladaCliente=0 and Confirmada=1 and FechaActividad ='{fecha:yyyy-MM-dd}' ORDER By FechaActividad, HoraActividad");
        // Mañana es el día no se manda a las canceladas.   (10/sep/23 02.07)
        var lisRes = ApiReservasMailGYG.MailGYG.DatosReservas(fecha, new TimeSpan(0, 0, 0), ChkConAlquileres.Checked, conCanceladas: false);
        if (MessageBox.Show($"Se va a madar el mensaje a {lisRes.Count} {lisRes.Count.Plural("reserva")}",
                             "Mandar recordatorio de que MAÑANA es el día", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
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
        // No añadirlo para que no lleguen 2 copias.        (16/jul/24 20.23)
        //colPara.Add("kayak.makarena@gmail.com");

        StringBuilder sb = new StringBuilder();
        sb.Append("");

        // Si se indica enviar el texto extra.              (20/oct/23 22.17)
        if (ChkIncluirTextoAviso.Checked && string.IsNullOrWhiteSpace(TxtAvisoExtra.Text) == false)
        {
            // Copiar el texto que se va a enviar.          (23/sep/23 10.49)
            TextoAvisoUltimo = TxtAvisoExtra.Text;

            sb.Append("<b>*IMPORTANTE / IMPORTANT*</b>");
            // Añadir una línea de separación                   (28/jun/24 13.06)
            sb.Append("<br/>");
            sb.Append(TxtAvisoExtra.Text.Replace(CrLf, "<br/>"));
            sb.Append("<br/>");
            sb.AppendLine("<br/>");
        }

        sb.Append(Properties.Resources.Mañana_es_el_dia.Replace(CrLf, "<br/>"));
        sb.Append("<br/>");

        // Usar el método para la firma.                    (27/sep/23 11.49)
        MailGYG.FirmaMakarena(sb, enIngles: false);

        //sb.Append("<br/>");
        //// No tenía los cambios de línea, añado el teléfono (08/sep/23 13.55)
        //sb.Append("Kayak Makarena<br/>");
        //sb.Append("WhatsApp: +34 645 76 16 89<br/>");
        //sb.Append("https://kayakmakarena.com<br/>");

        string body = sb.ToString().Replace(CrLf, "<br/>");
        var msg = ApiReservasMailGYG.MailGYG.EnviarMensaje(colPara, "Mañana es el día / Tomorrow is the day", body, true);
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

        // Cambiar el orden de la comprobación.             (27/ago/23 13.22)
        // Doble comprobación                               (25/ago/23 14.12)
        // Si es el mismo día de la actividad... avisar que no se debe mandar salvo si es de madrugada
        if (DateTime.Today == fecha.Date && DateTime.Now.TimeOfDay.Hours > 8)
        {
            ret = MessageBox.Show($"Los mensajes de 'Hoy es el día' para las reservas del {fecha.Date:dddd dd/MM/yyyy}" + CrLf +
                                  "Solo se deben mandar en el mismo día si es antes de las 08:00." + CrLf + CrLf +
                                  $"AHORA SON LAS {DateTime.Now:HH:mm} Y NO DEBERÍAS MANDARLAS." + CrLf + CrLf +
                                  "Pulsa ACEPTAR para mandar los mensajes." + CrLf +
                                  $"Pulsa CANCELAR para no mandar nada y/o seleccionar otra fecha.",
                                  "Mandar recordatorio de que hoy es el día",
                                  MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (ret == DialogResult.Cancel)
            {
                DateTimePickerGYG.Focus();
                return;
            }
        }

        // Segundo aviso, sin más comprobaciones.
        ret = MessageBox.Show($"¿Quieres mandar el mensaje de 'Hoy es el día' a las reservas del {fecha.Date:dddd dd/MM/yyyy}?" + CrLf +
                              "Pulsa SÍ para mandar el mensaje a las reservas de esa fecha." + CrLf +
                              $"Pulsa NO no mandar nada y seleccionar otra fecha.",
                              "Mandar recordatorio de que hoy es el día", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        if (ret == DialogResult.No)
        {
            DateTimePickerGYG.Focus();
            return;
        }


        // Comprobar que todos tengan email.                (24/ago/23 04.41)
        // Usando la nueva función.                         (27/ago/23 17.26)
        var res = ComprobarEmailsReservas(fecha, LvwSinEmail, ChkConAlquileres.Checked);

        if (res > 0)
        {
            LabelInfoListView.Text = $"Hay {res} {res.Plural("reserva")} del {fecha:dddd dd/MM/yyyy} sin emails.";
            MessageBox.Show($"Hay {res} {res.Plural("reserva")} del {fecha:dddd dd/MM/yyyy} sin emails:{CrLf}No se puede continuar hasta que lo soluciones.",
                            "Mandar recordatorio de que hoy es el día", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }
        else
        {
            LabelInfoListView.Text = $"Todas las reservas del '{fecha:dddd dd/MM/yyyy}' tiene asignado el email.";
        }

        //var lisRes = Reservas.TablaCol($"SELECT * FROM Reservas Where idDistribuidor=10 and Activa=1 and CanceladaCliente=0 and Confirmada=1 and FechaActividad ='{fecha:yyyy-MM-dd}' ORDER By FechaActividad, HoraActividad");
        // En hoy es el día no se usan las canceladas.      (10/sep/23 02.04)
        var lisRes = ApiReservasMailGYG.MailGYG.DatosReservas(fecha, new TimeSpan(0, 0, 0), ChkConAlquileres.Checked, conCanceladas: false);
        if (MessageBox.Show($"Se va a mandar el mensaje a {lisRes.Count} {lisRes.Count.Plural("reserva")}",
                             "Mandar recordatorio de que hoy es el día", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
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
        // No añadirlo para que no lleguen 2 copias.        (16/jul/24 20.28)
        //colPara.Add("kayak.makarena@gmail.com");

        StringBuilder sb = new StringBuilder();
        sb.Append("");

        // Si se indica enviar el texto extra.              (20/oct/23 22.18)
        if (ChkIncluirTextoAviso.Checked && string.IsNullOrWhiteSpace(TxtAvisoExtra.Text) == false)
        {
            // Copiar el texto que se va a enviar.          (23/sep/23 10.49)
            TextoAvisoUltimo = TxtAvisoExtra.Text;

            sb.Append("<b>*IMPORTANTE / IMPORTANT*</b>");
            // Añadir una línea de separación               (05/jul/24 12.32)
            sb.Append("<br/>");
            sb.Append(TxtAvisoExtra.Text.Replace(CrLf, "<br/>"));
            sb.Append("<br/>");
            sb.AppendLine("<br/>");
        }

        sb.Append(Properties.Resources.Hoy_es_el_dia.Replace(CrLf, "<br/>"));

        // Añadir la firma de Kayak Makarena                (18/sep/23 05.33)
        MailGYG.FirmaMakarena(sb, enIngles: false);

        //sb.Append("<br/>");
        //sb.Append("<br/>");
        //// No tenía los cambios de línea, añado el teléfono (08/sep/23 13.55)
        //sb.Append("Kayak Makarena<br/>");
        //sb.Append("WhatsApp: +34 645 76 16 89<br/>");
        //sb.Append("https://kayakmakarena.com<br/>");

        string body = sb.ToString().Replace(CrLf, "<br/>");
        var msg = MailGYG.EnviarMensaje(colPara, "Hoy es el día / Today is the day", body, true);
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

        var res = ComprobarEmailsReservas(fecha, LvwSinEmail, ChkConAlquileres.Checked);
        if (res > 0)
        {
            LabelInfoListView.Text = $"Hay {res} {res.Plural("reserva")} del {fecha:dddd dd/MM/yyyy} sin emails.";
            MessageBox.Show($"Hay {res} {res.Plural("reserva")} del {fecha:dddd dd/MM/yyyy} sin emails.{CrLf}No se debe continuar hasta que lo soluciones.", "Comprobar reservas sin email", MessageBoxButtons.OK, MessageBoxIcon.Error);
            HabilitarBotones(false);
        }
        else
        {
            LabelInfoListView.Text = $"Todas las reservas del '{fecha:dddd dd/MM/yyyy}' tiene asignado el email.";
            //MessageBox.Show($"Todas las reservas del '{fecha:dddd dd/MM/yyyy}' tiene asignado el email.", "Comprobar reservas sin email", MessageBoxButtons.OK, MessageBoxIcon.Information);
            HabilitarBotones(true);
        }
    }

    private void BtnReservasSinSalida_Click(object sender, EventArgs e)
    {
        // Comprobar las reservas que no han salido         (29/ago/23 11.18)
        // de la fecha indicada 
        var fecha = DateTimePickerGYG.Value.Date;

        // Incluir o no las canceladas.                     (10/sep/23 13.36)
        var col = ApiReservasMailGYG.MailGYG.ReservasSinSalida(fecha, ChkConCanceladas.Checked);

        AsignarListView(col, LvwSinEmail);

        if (col.Count == 0)
        {
            LabelInfoListView.Text = $"Todas las reservas del {fecha:dddd dd/MM/yyyy} han salido.";
            //MessageBox.Show($"Todas las reservas del {fecha:dddd dd/MM/yyyy} han salido.", "Comprobar reservas sin salida", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        else
        {
            LabelInfoListView.Text = $"Hay {col.Count} {col.Count.Plural("reserva")} del {fecha:dddd dd/MM/yyyy} que no han salido.";
            //MessageBox.Show($"Hay {col.Count} {col.Count.Plural("reserva")} del {fecha:dddd dd/MM/yyyy} que no han salido.", "Comprobar reservas sin salida", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        //MessageBox.Show(LabelInfoListView.Text, "Comprobar reservas sin salida", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void BtnMostrarReservas_Click(object sender, EventArgs e)
    {
        // Lista de las reservas de la fecha indicada.      (29/ago/23 11.32)
        var fecha = DateTimePickerGYG.Value.Date;

        List<ApiReservasMailGYG.ReservasGYG> col;
        // Mostrar reservas usa el parámetro ConCanceladas. (10/sep/23 02.09)
        // Aquí se debe comprobar si se quieren mostrar solo las canceladas.
        if (ChkSoloCanceladas.Checked)
        {
            col = ApiReservasMailGYG.MailGYG.DatosReservasCanceladas(fecha, new TimeSpan(0, 0, 0), ChkConAlquileres.Checked);
        }
        else
        {
            col = ApiReservasMailGYG.MailGYG.DatosReservas(fecha, new TimeSpan(0, 0, 0), ChkConAlquileres.Checked, ChkConCanceladas.Checked);
        }

        AsignarListView(col, LvwSinEmail);

        if (col.Count == 0)
        {
            LabelInfoListView.Text = $"No hay reservas del {fecha:dddd dd/MM/yyyy}.";
            //MessageBox.Show($"No hay reservas del {fecha:dddd dd/MM/yyyy}.", "Listado de reservas", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        else
        {
            LabelInfoListView.Text = $"Hay {col.Count} {col.Count.Plural("reserva")} del {fecha:dddd dd/MM/yyyy}.";
            //MessageBox.Show($"Hay {col.Count} {col.Count.Plural("reserva")} del {fecha:dddd dd/MM/yyyy}.", "Listado de reservas", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }

    private void ContextMenuListView_Opening(object sender, System.ComponentModel.CancelEventArgs e)
    {
        bool hab = LvwSinEmail.SelectedIndices.Count > 0;
        foreach (var mnu in ContextMenuListView.Items)
        {
            // Por si es el separador.                      (13/sep/23 09.56)
            var mnu1 = mnu as ToolStripMenuItem;
            if (mnu1 == null) continue;
            (mnu as ToolStripMenuItem).Enabled = hab;
        }
    }

    private void MnuCopiarDeLvw_Click(object sender, EventArgs e)
    {
        CopiarDeLvw(sender, LvwSinEmail);
    }

    /// <summary>
    /// Enviar el mensaje de alerta indicado en el parámetro, valores válidos: 1, 2 o 3.
    /// </summary>
    /// <param name="alerta">Número de la alerta (1, 2 3)</param>
    private void EnviarAlerta(int alerta)
    {
        if (alerta < 1 || alerta > 3) alerta = 1;

        DateTime fecha = DateTimePickerGYG.Value.Date;
        DialogResult ret;

        ret = MessageBox.Show($"¿Quieres mandar el mensaje de 'Alerta {alerta}' a las reservas del {fecha.Date:dddd dd/MM/yyyy}?" + CrLf +
                              $"Pulsa SÍ para mandar el mensaje de ALERTA {alerta} a las reservas de esa fecha." + CrLf +
                              $"Pulsa NO no mandar nada y seleccionar otra fecha.",
                              $"Mandar aviso de Alerta {alerta}", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        if (ret == DialogResult.No)
        {
            DateTimePickerGYG.Focus();
            return;
        }


        // Comprobar que todos tengan email.                (24/ago/23 04.41)
        // Usando la nueva función.                         (27/ago/23 17.26)
        var res = ComprobarEmailsReservas(fecha, LvwSinEmail, ChkConAlquileres.Checked);
        if (res > 0)
        {
            LabelInfoListView.Text = $"Hay {res} {res.Plural("reserva")} del {fecha:dddd dd/MM/yyyy} sin emails.";
            MessageBox.Show($"Hay {res} {res.Plural("reserva")} del {fecha:dddd dd/MM/yyyy} sin emails.{CrLf}No se puede continuar hasta que lo soluciones.",
                            $"Mandar aviso de Alerta {alerta}", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        else
        {
            LabelInfoListView.Text = $"Todas las reservas del '{fecha:dddd dd/MM/yyyy}' tiene asignado el email.";
        }

        //var lisRes = Reservas.TablaCol($"SELECT * FROM Reservas Where idDistribuidor=10 and Activa=1 and CanceladaCliente=0 and Confirmada=1 and FechaActividad ='{fecha:yyyy-MM-dd}' ORDER By FechaActividad, HoraActividad");
        // No enviar las alertas a las reservas canceladas. (10/sep/23 02.06)
        var lisRes = ApiReservasMailGYG.MailGYG.DatosReservas(fecha, new TimeSpan(0, 0, 0), ChkConAlquileres.Checked, conCanceladas: false);

        if (MessageBox.Show($"Se va a mandar el mensaje a {lisRes.Count} {lisRes.Count.Plural("reserva")}",
                            $"Mandar aviso de Alerta {alerta}", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
        {
            return;
        }

        var colPara = new List<string>();
        for (int i = 0; i < lisRes.Count; i++)
        {
            var re = lisRes[i];
            if (re == null) continue;
            // Ya se ha comprobado que todos tengan email.  (24/ago/23 04.47)
            // Si no tiene @ no mandar el correo.           (24/ago/23 04.48)
            if (re.Email.Contains('@') == false) continue;
            colPara.Add(re.Email);
        }

        // Agregar el email de kayak.makarena@gmail.com     (23/ago/23 22.18)
        colPara.Add("kayak.makarena@gmail.com");

        StringBuilder sb = new StringBuilder();
        sb.Append("");

        // Si se indica enviar el texto extra.              (17/sep/23 20.51)
        if (ChkIncluirTextoAviso.Checked && string.IsNullOrWhiteSpace(TxtAvisoExtra.Text) == false)
        {
            // Copiar el texto que se va a enviar.              (23/sep/23 10.49)
            TextoAvisoUltimo = TxtAvisoExtra.Text;

            sb.Append("<b>*IMPORTANTE / IMPORTANT*</b>");
            // Añadir línea de separación                       (05/jul/24 12.30)
            sb.Append("<br/>");
            sb.Append(TxtAvisoExtra.Text.Replace(CrLf, "<br/>"));
            sb.Append("<br/>");
            sb.AppendLine("<br/>");
        }

        if (alerta == 1)
            sb.Append(Properties.Resources.Alerta1_es_en.Replace(CrLf, "<br/>"));
        else if (alerta == 2)
            sb.Append(Properties.Resources.Alerta2_es_en.Replace(CrLf, "<br/>"));
        else if (alerta == 3)
            sb.Append(Properties.Resources.Alerta3_es_en.Replace(CrLf, "<br/>"));

        sb.Append("<br/>");

        // Usar el método para la firma.                    (27/sep/23 11.54)
        MailGYG.FirmaMakarena(sb, enIngles: false);

        // La fecha en español e inglés.                    (02/sep/23 21.05)
        var fechaES = $"{fecha.Date.ToString("dddd dd/MM/yyyy", System.Globalization.CultureInfo.CreateSpecificCulture("es-ES"))}";
        var fechaEN = $"{fecha.Date.ToString("dddd dd/MM/yyyy", System.Globalization.CultureInfo.CreateSpecificCulture("en-US"))}";

        string body = sb.ToString().Replace(CrLf, "<br/>");
        var msg = ApiReservasMailGYG.MailGYG.EnviarMensaje(colPara, $"Alerta {alerta} para {fechaES} / Alert {alerta} for {fechaEN}", body, true);
        if (msg.StartsWith("ERROR"))
        {
            MessageBox.Show($"ERROR al enviar el email:{CrLf}{msg}.", "Error al enviar el email de la reserva", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        else
        {
            MessageBox.Show($"{msg}", "Enviar email de la reserva", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }

    private void BtnAlerta1_Click(object sender, EventArgs e)
    {
        EnviarAlerta(1);
    }

    private void BtnAlerta2_Click(object sender, EventArgs e)
    {
        EnviarAlerta(2);
    }

    private void BtnAlerta3_Click(object sender, EventArgs e)
    {
        EnviarAlerta(3);
    }

    private void ChkIncluirTextoAviso_CheckedChanged(object sender, EventArgs e)
    {
        ActualizarColorEnabled(ChkIncluirTextoAviso, TxtAvisoExtra);
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

    private void BtnAnalizar_Click(object sender, EventArgs e)
    {
        //if (Form1.Current == null || Form1.Current.IsDisposed)
        //{
        //    Form1.Current = new Form1();
        //}
        //Form1.Current.BringToFront();
        //Form1.Current.Show();
        //Form1.Current.Focus();

        FormAnalizaEmail frm1 = FormAnalizaEmail.Current;

        if (frm1 == null || frm1.IsDisposed)
        {
            frm1 = new FormAnalizaEmail();
        }

        frm1.BringToFront();
        frm1.Show();
        frm1.Focus();
    }

    private void TimerHoraStatus_Tick(object sender, EventArgs e)
    {
        LabelFechaHora.Text = $"{DateTime.Now:dd/MM/yyyy HH:mm:ss}";
    }
}