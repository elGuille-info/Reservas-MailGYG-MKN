// Aplicación para móvil para los emails de GetYourGuide.   (26/ago/23 02.02)


using System.Runtime.CompilerServices;
using System.Text;

using ApiReservasMailGYG;

using KNDatos;

using static ApiReservasMailGYG.MailGYG;


namespace ReservasGYG_MAUI;

public partial class MainPage : ContentPage
{

    private Reservas LaReserva { get; set; }
    private StringBuilder InfoCrearConEmail { get; set; } = new StringBuilder();

    private bool inicializando = true;
    private string StatusAnt;
    private object QueBoton;

    public static MainPage Current { get; set; }

    public MainPage()
    {
        InitializeComponent();
        Current = this;
    }

    private void ContentPage_Appearing(object sender, EventArgs e)
    {
        BtnCrearConEmail.IsEnabled = false;
        BtnAnalizarEmail.IsEnabled = false;

        if (DeviceInfo.Platform == DevicePlatform.WinUI) 
        {
            MailGYG.CambioLinea = "\r";
        }
        else 
        {
            MailGYG.CambioLinea = "\n";
        }
        inicializando = false;
    }

    private void LimpiarControlesReserva()
    {
        if (inicializando) return;

        // Limpiar los controles del grupo              (21/ago/23 08.30)
        foreach (View ctl in GrbReserva.Children)
        {
            
            var txt = ctl as InputView;
            if (txt != null)
            {
                txt.Text = "";
            }
        }
    }

    private async void BtnPegarMail_Clicked(object sender, EventArgs e)
    {
        if (MainThread.IsMainThread)
        {
            try
            {
                // Code to run if this is the main thread
                string sClip = await Clipboard.GetTextAsync();
                RtfEmail.Text = sClip;
            }
            catch { }
            if(string.IsNullOrWhiteSpace(RtfEmail.Text) == false)
            {
                BtnAnalizarEmail.IsEnabled = true;
            }
            else
            {
                BtnAnalizarEmail.IsEnabled = false;
            }
        }
    }

    private async void BtnAnalizarEmail_Clicked(object sender, EventArgs e)
    {
        BtnCrearConEmail.IsEnabled = false;

        if (string.IsNullOrEmpty(RtfEmail.Text)) return;

        Reservas re = ApiReservasMailGYG.MailGYG.AnalizarEmail(RtfEmail.Text);
        if (re == null)
        {
            // Mostrar aviso de que algo no ha ido bien. (22/ago/23 20.24)
            //MessageBox.Show("Parece que los datos analizados no son correctos.", "Analizar email de GYG", MessageBoxButtons.OK, MessageBoxIcon.Error);
            await DisplayAlert("Analizar email de GYG", "Parece que los datos analizados no son correctos.", "Aceptar");
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

        BtnCrearConEmail.IsEnabled = true;
    }

    private void BtnCrearConEmail_Clicked(object sender, EventArgs e)
    {

    }

    private void BtnCrearConEmail_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == "IsEnabled")
        {
            var btn = (Button)sender;
            if (btn.IsEnabled) 
            {
                btn.FontAttributes = FontAttributes.None;
            }
            else
            {
                btn.FontAttributes = FontAttributes.Italic;
            }
        }
    }

    private async Task<bool> CrearReserva()
    {
        if (LaReserva == null)
        {
            //MessageBox.Show("La reserva no está asignada.", "No hay reserva", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            await DisplayAlert("No hay reserva", "La reserva no está asignada.", "Aceptar");
            return true;
        }
        //https://kayakmaro.es/MKNReservasAPI?ta=3&id=10&user=makarena&bdp=0
        //&fecha=16/09/2023&hora=15:30&actividad=RUTA CORTA&ad=6&cli=MAKARENA+(GYG)&tel=+34611825646
        //&notas=pre-reservar+plazas&pago=1&modop=otro

        var re = LaReserva;
        if (re.ID > 0)
        {
            await DisplayAlert("Ya existe esa reserva", $"Ya hay una reserva con ese ID: {re.ID}.", "Aceptar");
            //MessageBox.Show($"Ya hay una reserva con ese ID: {re.ID}.", "Ya existe esa reserva", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            return true;
        }

        // Buscar el producto.
        var pr = Producto.Buscar(re.FechaActividad, re.HoraActividad, re.Actividad);
        if (pr == null)
        {
            await DisplayAlert("No existe el producto",
                               $"No existe un producto para la actividad indicada.{CrLf}" + 
                               $"{re.ActividadMostrar}, {re.FechaActividad:dd/MM/yyyy}, {re.HoraActividad:hh\\:mm}", 
                               "Aceptar");
            //MessageBox.Show("No existe un producto para la actividad indicada." + CrLf +
            //                $"{re.ActividadMostrar}, {re.FechaActividad:dd/MM/yyyy}, {re.HoraActividad:hh\\:mm}",
            //                "No existe el producto",
            //                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
            var ret = await DisplayAlert("Ya existe esa reserva",
                                         $"Ya hay una reserva para ese cliente y actividad.{CrLf}" +
                                         "Pulsa ACEPTAR para continuar creándola por si es que el cliente se ha equivocado, " +
                                         $"pero habrá que contactar con el cliente y comprobar porqué hay más de una reserva.{CrLf}{CrLf}" +
                                         "Pulsa CANCELAR para no crear la reserva ni mandarle el mensaje de confirmación.", 
                                         "Aceptar", "Cancelar");
            if (ret == false)
            {
                InfoCrearConEmail.Clear();
                InfoCrearConEmail.AppendLine($"Ya existe una reserva con estos datos:{CrLf}Cliente: {re2.Nombre} ({re2.Telefono}){CrLf}Actividad: {re2.ActividadMostrar}, {re2.FechaActividad:dd/MM/yyyy}, {re2.HoraActividad:hh\\:mm}{CrLf}PAX: {re2.PaxsLargo}");
                InfoCrearConEmail.AppendLine("Por favor comprueba que esa reserva ya existe.");
                InfoCrearConEmail.AppendLine();

                LabelStatus.Text = StatusAnt;

                return true;
            }
            //// Avisar y dar la opción a crearla o no.
            //var ret = MessageBox.Show($"Ya hay una reserva para ese cliente y actividad.{CrLf}" +
            //                          "Pulsa ACEPTAR para continuar creándola por si es que el cliente se ha equivocado, " +
            //                          $"pero habrá que contactar con el cliente y comprobar porqué hay más de una reserva.{CrLf}{CrLf}" +
            //                          "Pulsa CANCELAR para no crear la reserva ni mandarle el mensaje de confirmación.",
            //                          "Ya existe esa reserva",
            //                          MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            //if (ret == DialogResult.Cancel)
            //{
            //    InfoCrearConEmail.Clear();
            //    InfoCrearConEmail.AppendLine($"Ya existe una reserva con estos datos:{CrLf}Cliente: {re2.Nombre} ({re2.Telefono}){CrLf}Actividad: {re2.ActividadMostrar}, {re2.FechaActividad:dd/MM/yyyy}, {re2.HoraActividad:hh\\:mm}{CrLf}PAX: {re2.PaxsLargo}");
            //    InfoCrearConEmail.AppendLine("Por favor comprueba que esa reserva ya existe.");
            //    InfoCrearConEmail.AppendLine();

            //    LabelStatus.Text = StatusAnt;

            //    return true;
            //}
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
                await DisplayAlert("", "", "Aceptar");
                //MessageBox.Show($"No se ha podido crear el nuevo cliente:{CrLf}{resCli.msg}", "Error al crear el cliente", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            await DisplayAlert("Error al crear la reserva", $"ERROR al crear la reserva:{CrLf}{msg}.", "Aceptar");
            //MessageBox.Show($"ERROR al crear la reserva:{CrLf}{msg}.", "Error al crear la reserva", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return true;
        }

        InfoCrearConEmail.Clear();
        InfoCrearConEmail.AppendLine($"La reserva se ha creado:{CrLf}Cliente: {re.Nombre} ({re.Telefono}){CrLf}Actividad: {re.ActividadMostrar}, {re.FechaActividad:dd/MM/yyyy}, {re.HoraActividad:hh\\:mm}{CrLf}PAX: {re.PaxsLargo}");
        InfoCrearConEmail.AppendLine();

        LabelStatus.Text = StatusAnt;

        //if (QueBoton == BtnCrearReserva)
        //{
        //    MessageBox.Show(InfoCrearConEmail.ToString(), "Reserva creada", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //}

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


    private async Task<string> LeerAsset(string asset)
    {
        using var stream = await FileSystem.OpenAppPackageFileAsync(asset);
        using var reader = new StreamReader(stream);

        return reader.ReadToEnd();
    }

    private bool EnviarMensajeConfirmacion()
    {
        StringBuilder sb = new StringBuilder();

        var re = LaReserva;
        if (re == null)
        {
            //MessageBox.Show($"ERROR la reserva es nula. Debes generarla primero con 'Crear reserva'.",
            //                "Error al enviar el email de la reserva",
            //                MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                //sb.Append(Properties.Resources.IMPORTANTE_EN_09_30_txt.Replace(CrLf, "<br/>"));
            }
            else
            {
                //sb.Append(Properties.Resources.IMPORTANTE_ES_09_30.Replace(CrLf, "<br/>"));
            }
        }
        else if (re.HoraActividad.Hours == 10 || re.HoraActividad == new TimeSpan(11, 0, 0))
        {
            if (enIngles)
            {
                //sb.Append(Properties.Resources.IMPORTANTE_EN_10_30_11_00_txt.Replace(CrLf, "<br/>"));
            }
            else
            {
                //sb.Append(Properties.Resources.IMPORTANTE_ES_10_30_11_00_txt.Replace(CrLf, "<br/>"));
            }
        }
        else
        {
            if (enIngles)
            {
                //sb.Append(Properties.Resources.IMPORTANTE_EN_txt.Replace(CrLf, "<br/>"));
            }
            else
            {
                //sb.Append(Properties.Resources.IMPORTANTE_ES_txt.Replace(CrLf, "<br/>"));
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

        var asunto = $"Booking - S271506 - {re.GYGReference}";
        var para = re.Email;
        string body = sb.ToString().Replace(CrLf, "<br/>");
        //var msg = ApiReservasMailGYG.MailGYG.EnviarMensaje(para, asunto, body, true);
        var msg = MainPage.EnviarMensaje(para, asunto, body, true).Result;

        InfoCrearConEmail.AppendLine(msg);
        InfoCrearConEmail.AppendLine();

        //if (QueBoton == BtnEnviarConfirm)
        //{
        //    if (msg.StartsWith("ERROR"))
        //    {
        //        MessageBox.Show($"ERROR al enviar el email:{CrLf}{msg}.", "Error al enviar el email de la reserva", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //    }
        //    else
        //    {
        //        MessageBox.Show($"{msg}", "Enviar email de la reserva", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }
        //}
        LabelStatus.Text = StatusAnt;
        //Application.DoEvents();

        return false;
    }

    private static async Task<string> EnviarMensaje(string para, string asunto, string bodyMensaje, bool esHtml)
    {
        var message = new EmailMessage
        {
            Subject = asunto,
            Body = bodyMensaje,
        };
        message.To.Add(para);
        message.To.Add("reservas@kayakmakarena.com");
        message.Bcc.Add("kayak.makarena@gmail.com");

        try
        {
            await Email.ComposeAsync(message);
        }
        catch (Exception ex)
        {
            return $"ERROR: {ex.Message}";
            //await Browser.OpenAsync("mailto:reservas@kayakmakarena.com" +
            //    $"?Subject={asunto.Replace(" ", "%20").Replace(".", "%2e").Replace("@", "%40")}");
        }
        return "Mensaje enviado correctamente a '" + para + "'";
    }
}