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
        private Reservas LaReserva { get; set; }
        private StringBuilder InfoCrearConEmail { get; set; } = new StringBuilder();

        private bool inicializando = true;
        private string StatusAnt;

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

        /// <summary>
        /// Limpiar los controles con los datos de la reserva.
        /// </summary>
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

            TxtTipo.Text = re.GYGTipo.ToString();

            TxtGYG.Text = $"Option: {re.GYGOption}\r\nDate: {re.GYGFechaHora}\r\nPrice:{re.GYGPrice}";
            TxtID.Text = re.ID.ToString();

            // Comprobar el tipo de reserva y asignar el texto  (16/sep/23 21.18)
            // del botón crear
            if (re.GYGTipo == Reservas.GYGTipos.Cancelada)
            {
                BtnCrearConEmail.Text = "Cancelar reserva y texto para email";
            }
            else if (re.GYGTipo == Reservas.GYGTipos.Modificada)
            {
                BtnCrearConEmail.Text = "Modificar reserva y texto para email";
            }
            else
            {
                BtnCrearConEmail.Text = "Crear reserva y texto para email";
            }

            LabelAsuntoEmail.Text = $"Booking - S271506 - {re.GYGReference}";
            LabelParaEmail.Text = re.Email;

            _ = await EnviarMensajeConfirmacion();

            bool todoOK = true;

            // Comprobar el alquiler con menos de 2 adultos.    (08/sep/23 23.51)
            if (KNDatos.BaseKayak.ActividadesAlquiler().Contains(LaReserva.Actividad))
            {
                bool ret;
                if (LaReserva.Adultos < 2)
                {
                    ret = await DisplayAlert("Nueva reserva de alquiler",
                                             "Es un alquiler para menos de 2 pax (adultos o menores mayor de 6 años)." + CrLf +
                                             "NO se debe aceptar esta reserva." + CrLf + CrLf +
                                             "Hay que contactar con el cliente por wasap y email y avisarle que el mínimo es 2 personas de 7 años o más." + CrLf + CrLf +
                                             "Esta reserva hay que cancelarla." + CrLf + CrLf +
                                             "¿Quieres continuar creando la reserva?",
                                             "Sí", "No");
                    todoOK = ret;
                }
                //else
                //{
                //    ret = await DisplayAlert("Nueva reserva de alquiler",
                //                             "Es un alquiler antes de continuar comprueba que esté correcta la reserva.",
                //                             "Aceptar", "Cancelar");
                //    todoOK = ret;
                //}
                await DisplayAlert("Nueva reserva de alquiler",
                                   "Es un alquiler, antes de continuar comprueba que esté correcta la reserva.",
                                   "Aceptar");
            }
            // Solo si no es cancelar.                          (09/sep/23 03.48)
            else if (re.GYGTipo != Reservas.GYGTipos.Cancelada)
            {
                // Comprobar si solo hay menores, y avisar.     (08/sep/23 23.31)
                if (re.Adultos < 1)
                {
                    await DisplayAlert("Analizar email de GYG",
                                       "¡ATENCIÓN! La reserva no incluye ningún adulto." + CrLf +
                                       "Habría que avisar al cliente de que se debe incluir al menos un adulto.",
                                       "Aceptar");
                }
            }

            // Si es desde booking avisar que revise las cosas antes de guardar.
            if (re.GYGTipo == Reservas.GYGTipos.Booking)
            {
                await DisplayAlert("Analizar email de GYG",
                                   "El texto de la reserva se ha tomado desde la página Bookings." + CrLf +
                                   "Comprueba que los datos son correctos antes de guardar y enviar el mensaje.",
                                   "Aceptar");
            }

            GrbTextoEmail.IsVisible = true;
            ActualizarImagenExpander();

            BtnCrearConEmail.IsEnabled = todoOK;
        }

        private async void BtnCrearConEmail_Clicked(object sender, EventArgs e)
        {
            StatusAnt = LabelStatus.Text;
            LabelStatus.Text = "Creando la reserva...";

            // Si devuelve true, no continuar con el envío del email.   (26/ago/23 00.06)
            //bool res = await CrearReserva();

            bool res;
            // Comporbar si es crear, cancelar y modificar  (16/sep/23 21.41)
            if (LaReserva.GYGTipo == Reservas.GYGTipos.Cancelada)
            {
                // Cancelar la reserva
                res = await CancelarReserva();
            }
            else if (LaReserva.GYGTipo == Reservas.GYGTipos.Modificada)
            {
                // Modificar la reserva
                res = await ModificarReserva();
            }
            else
            {
                // Crear la reserva
                res = await CrearReserva();
            }

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

        private async Task<bool> ModificarReserva()
        {
            // Buscar la reserva con el booking indicado.
            var re = Reservas.Buscar($"Notas like '%{LaReserva.GYGReference}%' and activa=1");
            if (re == null)
            {
                await DisplayAlert("No hay reserva",
                                   "No existe la reserva a modificar:" + CrLf +
                                   $"Booking: '{LaReserva.GYGReference}'",
                                   "Aceptar");
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
                    await DisplayAlert("No hay producto",
                                       "El producto de la reserva no existe:" + CrLf +
                                       $"ID producto: '{re.idProducto}'" + CrLf +
                                       $"Actividad: {re.ActividadMostrar} {re.FechaActividad:dd/MM/yyyy} {re.HoraActividad:hh\\:mm}",
                                       "Aceptar");
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
                var ret = await DisplayAlert("Modificar la reserva",
                                             "El número de participantes no coincide con el de la reserva original:" + CrLf +
                                             $"Antes: {re.TotalPax()}, ahora: '{LaReserva.Adultos}'" + CrLf +
                                             "Pulsa SÍ para usar los pax nuevos (se pondrán todos como adultos)." + CrLf +
                                             "Pulsa NO para dejar los pax que ya había.",
                                             "Sí", "No");
                if (ret)
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

            // Asignar los valores no en base datos.            (11/sep/23 12.17)
            re.GYGReference = LaReserva.GYGReference;
            re.GYGTipo = LaReserva.GYGTipo;
            re.GYGFechaHora = LaReserva.GYGFechaHora;
            re.GYGOption = LaReserva.GYGOption;
            re.GYGLanguage = LaReserva.GYGLanguage;
            LaReserva = re;

            return false;
        }

        private async Task<bool> CancelarReserva()
        {
            // Buscar la reserva con el booking indicado.
            var re = Reservas.Buscar($"Notas like '%{LaReserva.GYGReference}%' and activa=1");
            if (re == null)
            {
                await DisplayAlert("No hay reserva",
                                   "No existe la reserva a cancelar:" + CrLf +
                                   $"Booking: '{LaReserva.GYGReference}'",
                                   "Aceptar");
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
                    await DisplayAlert("No hay producto",
                                       "El producto de la reserva no existe:" + CrLf +
                                       $"ID producto: '{re.idProducto}'" + CrLf +
                                       $"Actividad: {re.ActividadMostrar} {re.FechaActividad:dd/MM/yyyy} {re.HoraActividad:hh\\:mm}",
                                       "Aceptar");
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
            re.GYGLanguage = LaReserva.GYGLanguage;
            LaReserva = re;

            return false;
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
            //img.Source = FileImageSource.FromResource($"ReservasGYG_Movil.Resources.{imgSource}", typeof(MainPage).Assembly);
            try
            {
                ////using var stream = await FileSystem.OpenAppPackageFileAsync(asset);
                ////using var reader = new System.IO.StreamReader(stream);
                //using (var stream = await FileSystem.OpenAppPackageFileAsync(asset))
                //{
                //    using (var reader = new System.IO.StreamReader(stream))
                //    {
                //        return reader.ReadToEnd();
                //    }
                //}
                await App.Refrescar(10);

                var assembly = typeof(MainPage).Assembly;
                //System.IO.Stream stream = assembly.GetManifestResourceStream($"ReservasGYG_Movil.Resources.{asset}");
                System.IO.Stream stream = assembly.GetManifestResourceStream($"ReservasGYG_Movil.Resources.{asset}");
                string text = "";
                using (var reader = new System.IO.StreamReader(stream))
                {
                    text = reader.ReadToEnd();
                }
                return text;
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
                    sb.Append(await MainPage.LeerAsset("IMPORTANTE_ALQUILER.txt"));
                }
                else
                {
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
                }
                // En temporada alta, hasta mediados septiembre (18/sep/23 05.18)
                if (DateTime.Today <= new DateTime(2023, 9, 15))
                {
                    sb.Append(await MainPage.LeerAsset("IMPORTANTE_Lee_esto_Maro.txt"));
                }

                // Indicar siempre que hagan la reseña.     (16/sep/23 22.44)
                sb.AppendLine();
                sb.AppendLine();
                if (enIngles)
                {
                    sb.AppendLine("We would love to receive a review on the GetYourGuide website with your opinion on this activity, taking into account that <b>Kayak Makarena</b> is responsible for managing the reservations and <b>Maro - Kayak Nerja</b> carries out the routes.");
                }
                else
                {
                    sb.AppendLine("Nos encantaría recibir una reseña en el sitio de GetYourGuide con tu opinión sobre esta actividad, teniendo en cuenta que <b>Kayak Makarena</b> es la encargada de gestionar las reservas y <b>Maro - Kayak Nerja</b> realiza las rutas.");
                }
            }

            // Añadir la firma de Kayak Makarena                (18/sep/23 05.37)
            MailGYG.FirmaMakarena(sb, enIngles);

            //sb.AppendLine();
            //sb.AppendLine();
            //sb.AppendLine("Kayak Makarena");
            //if (enIngles)
            //{
            //    sb.AppendLine("iMessage / WhatsApp: +34 645 76 16 89 <small>(Please, only WhatsApp messages or calls as I usually don't have coverage)</small>");
            //}
            //else
            //{
            //    sb.AppendLine("iMessage / WhatsApp: +34 645 76 16 89 <small>(Por favor, solo mensajes o llamadas por wasap ya que no suelo tener cobertura)</small>");
            //}
            //sb.AppendLine("https://kayakmakarena.com");

            LabelAsuntoEmail.Text = asunto; // $"Booking - S271506 - {re.GYGReference}";
            LabelParaEmail.Text = re.Email;
            // Cambiar los retornos de carro por <br/>      (18/sep/23 04.49)
            TxtTextoEmail.Text = sb.ToString().Replace(CrLf, "<br/>");

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

        private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            GrbEmailReserva.IsVisible = !GrbEmailReserva.IsVisible;
            ActualizarImagenExpander();
        }

        /// <summary>
        /// Muestra las imágenes que correspondan según estén visibles o no.
        /// </summary>
        private void ActualizarImagenExpander()
        {
            MostrarImagenExpander(ImgDatosReserva, GrbDatosReserva.IsVisible);
            MostrarImagenExpander(ImgTextoEmail, GrbTextoEmail.IsVisible);
            MostrarImagenExpander(ImgEmailReserva, GrbEmailReserva.IsVisible);
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
    }
}