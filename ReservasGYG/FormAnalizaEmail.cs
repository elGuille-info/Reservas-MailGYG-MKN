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

//using ApiRes

namespace ReservasGYG
{
    public partial class FormAnalizaEmail : Form
    {
        private bool inicializando = true;

        public static FormAnalizaEmail Current;
        public FormAnalizaEmail()
        {
            InitializeComponent();
            Current = this;
        }

        /// <summary>
        /// El retorno de carro según sea UWP/Windows u otro sistema.
        /// </summary>
        public static string CrLf => "\r\n";

        private Reservas LaReserva { get; set; }

        private StringBuilder InfoCrearConEmail { get; set; } = new StringBuilder();

        private void FormAnalizaEmailGYG_Load(object sender, EventArgs e)
        {
            inicializando = false;

            timer1.Interval = 990;
            timer1.Enabled = true;

            //RtfEmail.Text = "";
            //RtfEmail.Rtf = "";
            RtfEmail.Text = "";

            ChkCrearConEmail.Checked = false;
            ChkEnviarConfirm.Checked = false;
            HabilitarBotonesReservas();

            LimpiarControlesReserva();
        }

        private void BtnPegarEmail_Click(object sender, EventArgs e)
        {
            // Pegar el texto del portapapeles en el RtfEmail
            var dClip = Clipboard.GetDataObject();
            if (dClip == null) return;
            //Console.WriteLine("Tipo: {0}",dClip.ToString());

            string s = "";

            // Están los formatos Text, Html, OEMFormat
            //if (dClip.GetDataPresent(DataFormats.Html))
            //{
            //    var obj = dClip.GetData(DataFormats.Html);
            //    if (obj != null)
            //    {
            //        s = obj.ToString();
            //    }
            //}
            if (dClip.GetDataPresent(DataFormats.Text))
            {
                var obj = dClip.GetData(DataFormats.Text);
                if (obj != null)
                {
                    s = obj.ToString();
                }
            }
            //if (dClip.GetDataPresent(DataFormats.OemText))
            //{
            //    var obj = dClip.GetData(DataFormats.OemText);
            //    if (obj != null)
            //    {
            //        s = obj.ToString();
            //    }
            //}
            //if (dClip.GetDataPresent(DataFormats.Rtf)) 
            //{
            //    var obj = dClip.GetData(DataFormats.Rtf);
            //    if (obj != null)
            //    {
            //        s = obj.ToString();
            //    }
            //}
            //if (dClip.GetDataPresent(DataFormats.UnicodeText))
            //{
            //    var obj = dClip.GetData(DataFormats.UnicodeText);
            //    if (obj != null)
            //    {
            //        s = obj.ToString();
            //    }
            //}

            RtfEmail.Text = s;
        }

        private void BtnAnalizarEmail_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(RtfEmail.Text)) return;

            Reservas re = ApiReservasMailGYG.MailGYG.AnalizarEmail(RtfEmail.Text);
            if (re == null) { return; }

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
        }

        private void BtnCrearReserva_Click(object sender, EventArgs e)
        {
            //ChkEnviarConfirm.Checked = false;
            //BtnEnviarConfirm.Enabled = ChkEnviarConfirm.Checked;

            string statusAnt = LabelStatus.Text;
            LabelStatus.Text = "Creando la reserva...";
            
            timerCrearReserva.Interval = 200;
            timerCrearReserva.Enabled = true;

            if (LaReserva == null)
            {
                MessageBox.Show("La reserva no está asignada.", "No hay reserva", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                LabelStatus.Text = statusAnt;
                return;
            }
            //https://kayakmaro.es/MKNReservasAPI?ta=3&id=10&user=makarena&bdp=0
            //&fecha=16/09/2023&hora=15:30&actividad=RUTA CORTA&ad=6&cli=MAKARENA+(GYG)&tel=+34611825646
            //&notas=pre-reservar+plazas&pago=1&modop=otro

            var re = LaReserva;
            if (re.ID > 0)
            {
                MessageBox.Show($"Ya hay una reserva con ese ID: {re.ID}.", "Ya existe esa reserva", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                LabelStatus.Text = statusAnt;
                return;
            }

            // Buscar el producto.
            var pr = Producto.Buscar(re.FechaActividad, re.HoraActividad, re.Actividad);
            if (pr == null)
            {
                MessageBox.Show("No existe un producto para la actividad indicada." + CrLf +
                                $"{re.ActividadMostrar}, {re.FechaActividad:dd/MM/yyyy}, {re.HoraActividad:hh\\:mm}",
                                "No existe el producto",
                                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                LabelStatus.Text = statusAnt;
                return;
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
                LabelStatus.Text = statusAnt;
                return;
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
                    //await DisplayAlert("Error al crear el cliente",
                    //                   $"No se ha podido crear el nuevo cliente:{CrLf}{resCli.msg}",
                    //                   "Volver");
                    LabelStatus.Text = statusAnt;
                    return;
                }
                //cli = resCli.cli;
                //Incidencias.GuardarIncidencia("Clientes-Reservas", ElUsuario, "No existía el cliente.", cli.ToStringIncidencias());
            }
            //Incidencias.GuardarIncidencia("Reservas-Detalle", ElUsuario, LaReservaCopia.ToStringIncidencias(), $"Cambios realizados: {queHaCambiado}{CrLf}Nombre: {txtResNombre.Text}{(string.IsNullOrWhiteSpace(txtResTelefono.Text) ? $"{CrLf}Teléfono: {txtResTelefono.Text}" : "")}{(string.IsNullOrWhiteSpace(txtResEmail.Text) ? $"{CrLf}Email: {txtResEmail.Text}" : "")}.");

            // Crear la reserva
            string msg = re.Crear2();
            TxtID.Text = re.ID.ToString();
            if (msg.StartsWith("ERROR"))
            {
                MessageBox.Show($"ERROR al crear la reserva:{CrLf}{msg}.", "Error al crear la reserva", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LabelStatus.Text = statusAnt;
                return;
            }
            //ChkEnviarConfirm.Checked = true;
            //BtnEnviarConfirm.Enabled = ChkEnviarConfirm.Checked;

            InfoCrearConEmail.Clear();
            InfoCrearConEmail.AppendLine($"La reserva se ha creado:{CrLf}Cliente: {re.Nombre} ({re.Telefono}){CrLf}Actividad: {re.ActividadMostrar}, {re.FechaActividad:dd/MM/yyyy}, {re.HoraActividad:hh\\:mm}{CrLf}PAX: {re.PaxsLargo}");
            InfoCrearConEmail.AppendLine();

            LabelStatus.Text = statusAnt;

            if (sender == BtnCrearReserva)
            {
                MessageBox.Show(InfoCrearConEmail.ToString(), "Reserva creada", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BtnEnviarConfirm_Click(object sender, EventArgs e)
        {
            string statusAnt = LabelStatus.Text;
            LabelStatus.Text = "Enviando el email de confirmación...";

            timerCrearReserva.Interval = 200;
            timerCrearReserva.Enabled = true;

            StringBuilder sb = new StringBuilder();

            var re = LaReserva;
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

            if (sender == BtnEnviarConfirm)
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

            LabelStatus.Text = statusAnt;
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

            BtnCrearReserva.Enabled = ChkEnviarConfirm.Checked;
            BtnEnviarConfirm.Enabled = ChkEnviarConfirm.Checked;
        }

        private void BtnLimpiarTexto_Click(object sender, EventArgs e)
        {
            RtfEmail.Text = "";
        }

        private void BtnCrearConEmail_Click(object sender, EventArgs e)
        {
            // Las dos cosas seguidas.                      (22/ago/23 10.28)
            BtnCrearReserva_Click(sender, e);
            BtnEnviarConfirm_Click(sender, e);

            ChkCrearConEmail.Checked = false;
            ChkEnviarConfirm.Checked = false;
            HabilitarBotonesReservas();

            MessageBox.Show(InfoCrearConEmail.ToString(), "Crear reserva y enviar email", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void timer1_Tick(object sender, EventArgs e)
        {
            LabelFechaHora.Text = $"{DateTime.Now:dd/MM/yyyy HH:mm:ss}";
        }

        private void timerCrearReserva_Tick(object sender, EventArgs e)
        {
            timerCrearReserva.Stop();
            timerCrearReserva.Enabled = false;
        }
    }
}
