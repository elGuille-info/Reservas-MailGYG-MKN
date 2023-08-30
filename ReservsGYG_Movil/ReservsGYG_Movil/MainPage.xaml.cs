// Aplicación para móvil para los emails de GetYourGuide.   (29/ago/23 13.47)


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.CompilerServices;

using ApiReservasMailGYG;
using KNDatos;
using static ApiReservasMailGYG.MailGYG;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

namespace ReservasGYG_Movil
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        //public MainPage()
        //{
        //    InitializeComponent();
        //}

        private Reservas LaReserva { get; set; }
        private StringBuilder InfoCrearConEmail { get; set; } = new StringBuilder();

        private bool inicializando = true;
        private string StatusAnt;
        //private object QueBoton;

        public static MainPage Current { get; set; }

        public MainPage()
        {
            InitializeComponent();
            Current = this;
        }

        private void ContentPage_Appearing(object sender, EventArgs e)
        {
            GrbTextoEmail.IsVisible = false;
            GrbDatosReserva.IsVisible = false;

            BtnCrearConEmail.IsEnabled = false;
            BtnAnalizarEmail.IsEnabled = false;

            if (DeviceInfo.Platform == DevicePlatform.UWP)
            {
                MailGYG.CambioLinea = "\r";
            }
            else
            {
                MailGYG.CambioLinea = "\n";
            }
            inicializando = false;

            ActualizarImagenExpander();
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
                if (string.IsNullOrWhiteSpace(RtfEmail.Text) == false)
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
                await DisplayAlert("Analizar email de GYG", "Parece que los datos analizados no son correctos.", "Aceptar");
                return;
            }

            GrbDatosReserva.IsVisible = true;
            ActualizarImagenExpander();

            LaReserva = re;
            re.Notas2 = $"Price: {re.GYGPrice}";

            LimpiarControlesReserva();

            TxtNombre.Text = re.Nombre;
            TxtTelefono.Text = re.Telefono;
            TxtNotas.Text = re.GYGNotas;
            TxtActividad.Text = re.ActividadMostrar;
            TxtEmail.Text = re.Email;
            TxtAdultos.Text = re.Adultos.ToString();
            TxtMenores.Text = re.Niños.ToString();
            TxtMenoresG.Text = re.Niños2.ToString();
            TxtFechaHora.Text = $"{re.FechaActividad:dd/MM/yyyy} {re.HoraActividad:hh\\:mm}";
            TxtPrice.Text = re.GYGPrice;
            TxtLanguage.Text = re.GYGLanguage;
            TxtReference.Text = re.GYGReference;
            TxtPais.Text = re.GYGPais;

            TxtGYG.Text = $"Option: {re.GYGOption}\r\nDate: {re.GYGFechaHora}\r\nPrice:{re.GYGPrice}";
            TxtID.Text = re.ID.ToString();

            LabelAsuntoEmail.Text = $"Booking - S271506 - {re.GYGReference}";
            LabelParaEmail.Text = re.Email;

            _ = await EnviarMensajeConfirmacion();
            
            GrbTextoEmail.IsVisible = true;
            ActualizarImagenExpander();

            BtnCrearConEmail.IsEnabled = true;
        }

        private async void BtnCrearConEmail_Clicked(object sender, EventArgs e)
        {
            StatusAnt = LabelStatus.Text;
            LabelStatus.Text = "Creando la reserva...";

            // Si devuelve true, no continuar con el envío del email.   (26/ago/23 00.06)
            bool res = await CrearReserva();

            LabelStatus.Text = StatusAnt;

            if (res)
            {
                await DisplayAlert("Crear reserva y enviar email", InfoCrearConEmail.ToString(), "Aceptar");
                return;
            }

            // Parece que falla al manda el email.              (26/ago/23 07.10)

            // Probar en iPhone.                                (26/ago/23 08.44)
            // También falla.

            StatusAnt = LabelStatus.Text;
            LabelStatus.Text = "Enviando el email de confirmación...";

            res = await EnviarMensajeConfirmacion();
            if (res)
            {
                await DisplayAlert("Crear reserva y enviar email", InfoCrearConEmail.ToString(), "Aceptar");
                return;
            }
            await DisplayAlert("Crear reserva y enviar email", InfoCrearConEmail.ToString(), "Aceptar");

            //await DisplayAlert("Crear reserva SIN enviar email", InfoCrearConEmail.ToString(), "Aceptar");
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
                await DisplayAlert("No hay reserva", "La reserva no está asignada.", "Aceptar");
                return true;
            }

            var re = LaReserva;
            if (re.ID > 0)
            {
                await DisplayAlert("Ya existe esa reserva", $"Ya hay una reserva con ese ID: {re.ID}.", "Aceptar");
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
                    return true;
                }
            }

            // Asignar los kayaks.                              (24/ago/23 06.23)
            MainPage.CalcularPiraguas(re);

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

            return false;
        }

        /// <summary>
        /// Calcular las piraguas según los pax.
        /// </summary>
        private static void CalcularPiraguas(Reservas re)
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

        private static async Task<string> LeerAsset(string asset)
        {
            try
            {
                //using var stream = await FileSystem.OpenAppPackageFileAsync(asset);
                //using var reader = new System.IO.StreamReader(stream);
                using (var stream = await FileSystem.OpenAppPackageFileAsync(asset))
                {
                    using (var reader = new System.IO.StreamReader(stream))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }
            catch (Exception ex)
            {
                return $"ERROR: {ex.Message}";
            }
        }

        private async Task<bool> EnviarMensajeConfirmacion()
        {
            StringBuilder sb = new StringBuilder();

            var re = LaReserva;
            if (re == null)
            {
                await DisplayAlert("Error al enviar el email de la reserva",
                                   "ERROR la reserva es nula. Debes generarla primero con 'Crear reserva'.",
                                   "Aceptar");
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
                    sb.Append(await MainPage.LeerAsset("IMPORTANTE_EN_09_30.txt"));
                }
                else
                {
                    sb.Append(await MainPage.LeerAsset("IMPORTANTE_ES_09_30.txt"));
                }
            }
            else if (re.HoraActividad.Hours == 10 || re.HoraActividad == new TimeSpan(11, 0, 0))
            {
                if (enIngles)
                {
                    sb.Append(await MainPage.LeerAsset("IMPORTANTE_EN_10_30_11_00.txt"));
                }
                else
                {
                    sb.Append(await MainPage.LeerAsset("IMPORTANTE_ES_10_30_11_00.txt"));
                }
            }
            else
            {
                if (enIngles)
                {
                    sb.Append(await MainPage.LeerAsset("IMPORTANTE_EN.txt"));
                }
                else
                {
                    sb.Append(await MainPage.LeerAsset("IMPORTANTE_ES.txt"));
                }
            }

            // Si es para el mismo día de la actividad.         (24/ago/23 06.24)
            if (DateTime.Today == re.FechaActividad)
            {
                //sb.Append("<br/>");
                sb.AppendLine();
                if (enIngles)
                {
                    sb.Append("We would love to receive a review on the GetYourGuide website with your opinion on this activity, taking into account that <b>Kayak Makarena</b> is responsible for managing the reservations and <b>Maro - Kayak Nerja</b> carries out the routes.");
                }
                else
                {
                    sb.Append("Nos encantaría recibir una reseña en el sitio de GetYourGuide con tu opinión sobre esta actividad, teniendo en cuenta que <b>Kayak Makarena</b> es la encargada de gestionar las reservas y <b>Maro - Kayak Nerja</b> realiza las rutas.");
                }
                sb.AppendLine();
            }

            sb.AppendLine();
            sb.AppendLine();
            //sb.Append("<br/>");
            //sb.Append("<br/>");
            sb.Append("Kayak Makarena");

            //var asunto = $"Booking - S271506 - {re.GYGReference}";
            //var para = re.Email;
            ////string body = sb.ToString().Replace(CrLf, "<br/>");
            ////var msg = ApiReservasMailGYG.MailGYG.EnviarMensaje(para, asunto, body, true);
            //string body = sb.ToString();

            LabelAsuntoEmail.Text = $"Booking - S271506 - {re.GYGReference}";
            LabelParaEmail.Text = re.Email;
            TxtTextoEmail.Text = sb.ToString();

            // No enviar el email,                          (30/ago/23 02.49)
            // pegar el texto en otra parte para poder copiarlo y enviarlo manualmente.

            //var msg = MainPage.EnviarMensaje(para, asunto, body).Result;

            //InfoCrearConEmail.AppendLine(msg);
            //InfoCrearConEmail.AppendLine();

            LabelStatus.Text = StatusAnt;

            return false;
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            GrbDatosReserva.IsVisible = !GrbDatosReserva.IsVisible;
            ActualizarImagenExpander();
        }

        private void TapGestureTextoEmail_Tapped(object sender, EventArgs e)
        {
            GrbTextoEmail.IsVisible = !GrbTextoEmail.IsVisible;
            ActualizarImagenExpander();
        }

        /// <summary>
        /// Muestra las imágenes que correspondan según estén visibles o no.
        /// </summary>
        private void ActualizarImagenExpander()
        {
            MostrarImagenExpander(ImgDatosReserva, GrbDatosReserva.IsVisible);
            MostrarImagenExpander(ImgTextoEmail, GrbTextoEmail.IsVisible);
            //MostrarImagenExpander(ImgComprobarCambios, grbComprobarCambiosContent.IsVisible);
            //MostrarImagenExpander(ImgMostrarEdicionReservas, grbMostrarEdicionReservasContent.IsVisible);
        }

        /// <summary>
        /// Mostrar la imagen que corresponde. (01/Nov/21 22.35)
        /// </summary>
        /// <param name="img">El control de imagen a asignar.</param>
        /// <param name="expanded">si se debe mostrar expand o collapse.</param>
        private void MostrarImagenExpander(Image img, bool expanded)
        {
            string imgSource;
            if (expanded)
            {
                imgSource = "expand_white.png";
            }
            else
            {
                imgSource = "collapse_white.png";
            }
            img.Source = FileImageSource.FromResource($"ReservasGYG_Movil.Resources.{imgSource}", typeof(MainPage).Assembly);
        }

        private void ButtonCopiaAsunto_Clicked(object sender, EventArgs e)
        {
            CopiarClipBoard(LabelAsuntoEmail.Text);
        }

        private void ButtonCopiaEmail_Clicked(object sender, EventArgs e)
        {
            CopiarClipBoard(LabelParaEmail.Text);
        }

        private void ButtonCopiaTextoEmail_Clicked(object sender, EventArgs e)
        {
            CopiarClipBoard(TxtTextoEmail.Text);
        }

        private static async void CopiarClipBoard(string texto)
        {
            if (MainThread.IsMainThread)
            {
                try
                {

                    // Code to run if this is the main thread
                    await Clipboard.SetTextAsync(texto);
                }
                catch { }
            }
        }

        ///// <summary>
        ///// Enviar un mensaje de correo.
        ///// </summary>
        ///// <param name="para"></param>
        ///// <param name="asunto"></param>
        ///// <param name="bodyMensaje"></param>
        ///// <returns></returns>
        //private static async Task<string> EnviarMensaje(string para, string asunto, string bodyMensaje)
        //{
        //    var message = new EmailMessage
        //    {
        //        Subject = asunto,
        //        Body = bodyMensaje,
        //    };
        //    message.To.Add(para);
        //    message.To.Add("reservas@kayakmakarena.com");
        //    message.Bcc.Add("kayak.makarena@gmail.com");

        //    try
        //    {
        //        await Email.ComposeAsync(message);
        //    }
        //    catch (Exception ex)
        //    {
        //        return $"ERROR: {ex.Message}";
        //        //await Browser.OpenAsync("mailto:reservas@kayakmakarena.com" +
        //        //    $"?Subject={asunto.Replace(" ", "%20").Replace(".", "%2e").Replace("@", "%40")}");
        //    }
        //    return "Mensaje enviado correctamente a '" + para + "'";
        //}
    }
}