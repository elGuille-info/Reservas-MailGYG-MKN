// Para mandar las fotos a los del día y hora indicada.     (22/ago/23 16.50)
//
// Hay que mirar las reservas que hay y ponerlas por horas. (01/sep/23 19.44)
// Comprobar las fotos que hay (horas) y avisar si hay reservas y no hay fotos,
// al revés no es necesario.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using ApiReservasMailGYG;

using KNDatos;

using static ApiReservasMailGYG.MailGYG;

namespace ReservasGYG;

public partial class FormEnviarFotos : Form
{
    private bool inicializando = true;

    public static FormEnviarFotos Current { get; set; }
    public FormEnviarFotos()
    {
        InitializeComponent();
        Current = this;
    }

    public DateTime FechaFotos { get => DateTimePickerGYG.Value; set => DateTimePickerGYG.Value = value; }

    private void FormEnviarFotos_Load(object sender, EventArgs e)
    {
        TxtFotosDia.Text = "";

        BtnPegar.Image = Properties.Resources.Paste;
        BtnLimpiar.Image = Properties.Resources.CleanData;

        // Llenar el combo con las horas.
        CboHoras.Items.Clear();
        CboHoras.Items.AddRange(MailGYG.LasHoras.ToArray());

        LabelInfoListView.Text = "";

        inicializando = false;
    }

    private void CboHoras_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (inicializando) return;

        int index = CboHoras.SelectedIndex;
        if (index < 0 || string.IsNullOrWhiteSpace(CboHoras.Text))
        {
            BtnEnviarFotos.Enabled = false;
            TxtFotosSeleccionada.Text = "";
            return;
        }

        if (CboHoras.Tag == null)
        {
            // No hay datos asignados.
            MessageBox.Show("No hay datos con las fotos a enviar.", "Seleccionar hora con las fotos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        List<string> listaHoras = (List<string>)CboHoras.Tag;
        if (listaHoras == null || listaHoras.Count == 0)
        {
            MessageBox.Show("No hay datos con las fotos a enviar.", "Seleccionar hora con las fotos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        TxtFotosSeleccionada.Text = listaHoras[CboHoras.SelectedIndex];

        BtnEnviarFotos.Text = $"Enviar las fotos de {CboHoras.Text} {DateTimePickerGYG.Value:dd/MM/yyyy}";
        BtnEnviarFotosDia.Text = $"Enviar todas las fotos del día {DateTimePickerGYG.Value:dd/MM/yyyy}";
    }

    private void DateTimePickerFotos_ValueChanged(object sender, EventArgs e)
    {
        if (inicializando) return;

        // No aceptar la fecha de hoy o posterior.
        if (DateTimePickerGYG.Value.Date >= DateTime.Today)
        {
            DateTimePickerGYG.Show();
            return;
        }

        int index = CboHoras.SelectedIndex;
        if (index < 0 || string.IsNullOrWhiteSpace(CboHoras.Text))
        {
            BtnEnviarFotos.Enabled = false;
            return;
        }

        BtnEnviarFotos.Text = $"Enviar las fotos de {CboHoras.Text} {DateTimePickerGYG.Value:dd/MM/yyyy}";
        BtnEnviarFotosDia.Text = $"Enviar todas las fotos del día {DateTimePickerGYG.Value:dd/MM/yyyy}";
    }

    private void BtnEnviarFotos_Click(object sender, EventArgs e)
    {
        //TODO: Solo comprobar las reservas de la hora           (02/sep/23 13.50
        // que se va a mandar.

        if (ComprobarReservasSinEmail()) return;

        // Mandar los mensaje con las fotos.
        // Mandar el de la hora indicada en el combo.

        if (string.IsNullOrWhiteSpace(TxtFotosSeleccionada.Text))
        {
            MessageBox.Show($"No hay texto con las fotos a enviar.", "Enviar fotos de una hora concreta", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        var fecha = DateTimePickerGYG.Value;
        var hora = CboHoras.Text.AsTimeSpan();
        if (EnviarFotos(fecha, hora, TxtFotosSeleccionada.Text, conAlertas: true))
        {
            //MessageBox.Show($"Error al enviar las fotos.", "Enviar fotos de una hora concreta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            return;
        }

        MessageBox.Show($"Fotos enviadas de {fecha:dddd dd/MM/yyyy} {hora:hh\\:mm}", "Enviar fotos de una hora concreta", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void BtnEnviarFotosDia_Click(object sender, EventArgs e)
    {
        if (ComprobarReservasSinEmail()) return;

        var fecha = DateTimePickerGYG.Value;

        // Comprobar si están todas las horas.              (02/sep/23 13.44)
        if (ComprobarHorasFotosReservas(fecha, conAlertas: false))
        {
            var ret = MessageBox.Show("Hay horas de fotos y de reservas que no coinciden." + CrLf +
                                      "¿Quieres mandarlas de todas formas?",
                                      "Hay reservas sin fotos",
                                      MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (ret == DialogResult.No)
            {
                return;
            }
        }

        int totalFotos = 0;


        // Mandar todos los mensajes con todas las fotos del día.
        for (int i = 0; i < CboHoras.Items.Count; i++)
        {
            CboHoras.SelectedIndex = i;
            Application.DoEvents();
            if (string.IsNullOrWhiteSpace(TxtFotosSeleccionada.Text))
            {
                continue;
            }
            var hora = CboHoras.Items[i].ToString().AsTimeSpan();
            if (EnviarFotos(fecha, hora, TxtFotosSeleccionada.Text, conAlertas: false))
            {
                Debug.WriteLine("{0} {1}", fecha, hora);
            }
            totalFotos += 1;
        }

        MessageBox.Show($"Se han enviado {totalFotos} {totalFotos.Plural("enlace")} con fotos de la fecha {fecha:dddd dd/MM/yyyy}.", "Enviar todas las fotos del día", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    /// <summary>
    /// Enviar las fotos del día y hora indicados.
    /// </summary>
    /// <param name="fecha">La fecha de la reserva.</param>
    /// <param name="hora">La hora de la reserva.</param>
    /// <param name="enlacesFotos">El texto a enviar.</param>
    /// <param name="conAlertas">Si se muestra el MessageBox en caso de error.</param>
    /// <returns>True si hubo algún error, false si fue todo bien.</returns>
    private static bool EnviarFotos(DateTime fecha, TimeSpan hora, string enlacesFotos, bool conAlertas)
    {
        if (string.IsNullOrWhiteSpace(enlacesFotos))
        {
            if (conAlertas)
                MessageBox.Show($"El texto con los enlaces a las fotos de la fecha '{fecha:dddd dd/MM/yyyy}' y hora {hora:hh\\:mm} está vacío.", "Enviar las fotos por email", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            return true;
        }

        StringBuilder sb = new StringBuilder();
        sb.Append("Select * from Reservas ");
        sb.Append("where Activa = 1 and CanceladaCliente = 0 and idDistribuidor = 10 ");
        //sb.Append("and Nombre != 'Makarena (GYG)' ");
        // Solo las reservas confirmadas, por si las moscas (01/sep/23 19.49)
        sb.Append($"and Confirmada = 1 ");
        // Solo las rutas                                   (01/sep/23 21.15)
        sb.Append($"and Actividad like 'ruta%' ");
        sb.Append($"and FechaActividad = '{fecha:yyyy-MM-dd}' and HoraActividad = '{hora:hh\\:mm}' ");
        sb.Append("order by FechaActividad, HoraActividad, ID");

        var colRes = Reservas.TablaCol(sb.ToString());

        if (colRes.Count == 0)
        {
            if (conAlertas)
                MessageBox.Show($"No hay reservas de la fecha '{fecha:dddd dd/MM/yyyy}' y hora {hora:hh\\:mm}.", "Enviar las fotos por email", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            return true;
        }

        // Colección con los emails a mandar.
        List<string> list = new List<string>();

        // Añadir los emails
        for (int i = 0; i < colRes.Count; i++)
        {
            // Comprobar si tiene @ añadirlo.               (24/ago/23 03.16)
            if (colRes[i].Email.Contains('@'))
            {
                list.Add(colRes[i].Email);
            }
        }

        if (list.Count == 0)
        {
            if (conAlertas)
                MessageBox.Show($"No hay reservas con emails en la fecha '{fecha:dddd dd/MM/yyyy}' y hora {hora:hh\\:mm}.", "Enviar las fotos por email", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            return true;
        }

        // Agregar el email de kayak.makarena@gmail.com     (23/ago/23 22.16)
        list.Add("kayak.makarena@gmail.com");

        sb.Clear();
        sb.Append("");
        sb.AppendLine(enlacesFotos);
        sb.Append("<br/>");
        sb.Append(Properties.Resources.Fotos_ruta.Replace(CrLf, "<br/>"));
        sb.Append("<br/>");
        sb.Append("<br/>");
        // No tenía los cambios de línea, añado el teléfono (08/sep/23 13.55)
        sb.Append("Kayak Makarena<br/>");
        sb.Append("WhatsApp: +34 645 76 16 89<br/>");
        sb.Append("https://kayakmakarena.com<br/>");

        string body = sb.ToString().Replace(CrLf, "<br/>");

        MailGYG.EnviarMensaje(list, $"Fotos Ruta {fecha:dd/MM/yyyy} {hora:hh\\:mm}", body, true);

        return false;
    }

    private bool ComprobarReservasSinEmail()
    {
        // Comprobar si hay clientes sin email en la fecha indicada. (24/ago/23 04.31)
        var fecha = DateTimePickerGYG.Value.Date;

        // No se mandan fotos a los alquileres.             (06/sep/23 00.04)
        var res = Form1.ComprobarEmailsReservas(fecha, LvwSinEmail, conAlquileres: false);
        if (res > 0)
        {
            LabelInfoListView.Text = $"Hay {res} {res.Plural("reserva")} (de rutas) del {fecha:dddd dd/MM/yyyy} sin emails.";
            MessageBox.Show($"Hay {res} {res.Plural("reserva")} (de rutas) del {fecha:dddd dd/MM/yyyy} sin emails.{CrLf}No se debe continuar hasta que lo soluciones.", "Comprobar reservas sin email", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return true;
        }
        else
        {
            LabelInfoListView.Text = $"Todas las reservas del '{fecha:dddd dd/MM/yyyy}' tiene asignado el email.";
        }

        return false;
    }

    private void BtnComprobarEmails_Click(object sender, EventArgs e)
    {
        BtnEnviarFotos.Enabled = false;
        BtnEnviarFotosDia.Enabled = false;

        // Comprobar si hay clientes sin email en la fecha indicada. (24/ago/23 04.31)
        var fecha = DateTimePickerGYG.Value.Date;

        // Para las fotos son sin alquiler.                 (06/sep/23 00.03)
        var res = Form1.ComprobarEmailsReservas(fecha, LvwSinEmail, conAlquileres: false);
        if (res > 0)
        {
            LabelInfoListView.Text = $"Hay {res} {res.Plural("reserva")} (de rutas) del {fecha:dddd dd/MM/yyyy} sin emails.;
            MessageBox.Show($"Hay {res} {res.Plural("reserva")} (de rutas) del {fecha:dddd dd/MM/yyyy} sin emails.{CrLf}No se debe continuar hasta que lo soluciones.", "Comprobar reservas sin email", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        else
        {
            LabelInfoListView.Text = $"Todas las reservas del '{fecha:dddd dd/MM/yyyy}' tiene asignado el email.";
            //MessageBox.Show($"Todas las reservas de rutas del '{fecha:dddd dd/MM/yyyy}' tiene asignado el email.", "Comprobar reservas sin email", MessageBoxButtons.OK, MessageBoxIcon.Information);
            BtnEnviarFotos.Enabled = true;
            BtnEnviarFotosDia.Enabled = true;
        }
    }

    private void BtnExtraerHoras_Click(object sender, EventArgs e)
    {
        //LimpiarTextosFotos();

        CboHoras.Tag = null;

        // Extraer las horas del texto en TxtFotosDia.      (23/ago/23 11.57)
        var fotosHoras = MailGYG.AnalizarTextoFotos(TxtFotosDia.Text);

        if (fotosHoras.Count == 0)
        {
            MessageBox.Show("Parece que el texto de las fotos no tiene datos válidos.", "Extraer horas de las fotos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            return;
        }

        // Asignar el array al tag del combo.               (27/ago/23 20.33)
        CboHoras.Tag = fotosHoras;

        var laFechaFotos = MailGYG.FechaFotos(TxtFotosDia.Text);
        DateTime fechaFotos = DateTime.Today;
        var culture = System.Globalization.CultureInfo.CurrentCulture;
        string[] formatos = {
                "dd-MM-yyyy", "dd.MM.yyyy", "dd.MM.yy", "dd/MM/yyyy", "dd-MM-yy", "dd/MM/yy",
                "d/M/yyyy", "d-M-yyyy","d/M/yy", "d-M-yy",
                "dd/M/yyyy", "dd-M-yyyy","dd/M/yy", "dd-M-yy",
                "d/MM/yyyy", "d-MM-yyyy","d/MM/yy", "d-MM-yy",
                "yyyy-MM-dd", "yyyy/MM/dd", "yy-MM-dd", "yy/MM/dd" };

        if (string.IsNullOrEmpty(laFechaFotos) == false)
        {
            if (DateTime.TryParseExact(laFechaFotos, formatos, culture, System.Globalization.DateTimeStyles.None, out fechaFotos) == false)
            {
                // La fecha no es correcta.
                MessageBox.Show($"La fecha '{laFechaFotos}' no es correcta.", "Extraer horas de las fotos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                CboHoras.Tag = null;
                return;
            }
        }
        DateTimePickerGYG.Value = fechaFotos;

        // Seleccionar la primera hora
        CboHoras.SelectedIndex = 0;

        /*
Fotos rutas 18-08-2023


¡Hola!
Te paso el enlace a las fotos de la ruta:

Ruta Viernes 18.08.23 09.30h - Juanda
https://photos.app.goo.gl/vsZqGK5VTVRKMgrP9

Ruta Viernes 18.08.23 09.30h - Timmy
https://photos.app.goo.gl/1hqzqiSTt8QtFLXf8

¡Muchas gracias!


¡Hola!
Te paso el enlace a las fotos de la ruta:

Ruta Viernes 18.08.23 10.30h - Mery
https://photos.app.goo.gl/Hb6bUbyM3LV8s9s88

¡Muchas gracias!


¡Hola!
Te paso el enlace a las fotos de la ruta:

Ruta Viernes 18.08.23 11.00h - Perico
https://photos.app.goo.gl/vxBeKQv5sesnCFv59

¡Muchas gracias!


¡Hola!
Te paso el enlace a las fotos de la ruta:

Ruta Viernes 18.08.23 11.45h - Joselu y Timmy
https://photos.app.goo.gl/CbqH9BRxYmfU1UBk6

¡Muchas gracias!


¡Hola!
Te paso el enlace a las fotos de la ruta:

Ruta Viernes 18.08.23 13.15h - Juanda
https://photos.app.goo.gl/Kj4eRWt4P9af1cJH8

Ruta Viernes 18.08.23 13.15h - Paco
https://photos.app.goo.gl/Lq5C6QU4v5ByD1FR6

¡Muchas gracias!


¡Hola!
Te paso el enlace a las fotos de la ruta:

Ruta Viernes 18.08.23 15.30h - Mery
https://photos.app.goo.gl/cEnH2VYshf2GG8Hk8

Ruta Viernes 18.08.23 15.30h - Paco
https://photos.app.goo.gl/SZKCyG2hkqEgyPvN7

¡Muchas gracias!


¡Hola!
Te paso el enlace a las fotos de la ruta:

Ruta Viernes 18.08.23 16.15h - Joselu
https://photos.app.goo.gl/uXkPzWrb5VTEd9Ua6

Ruta Viernes 18.08.23 16.15h - Timmy
https://photos.app.goo.gl/F5NFbFoyEbzYWMX17

¡Muchas gracias!


¡Hola!
Te paso el enlace a las fotos de la ruta:

Ruta Viernes 18.08.23 16.30h - Juanda
https://photos.app.goo.gl/YXusGShNEu2KfmAe8

¡Muchas gracias!


¡Hola!
Te paso el enlace a las fotos de la ruta:

Ruta Viernes 18.08.23 17.45h - ⁬Michelle
https://photos.app.goo.gl/31deS4WW8g1b6k5Q8

Ruta Viernes 18.08.23 17.45h - Perico
https://photos.app.goo.gl/qqxWBkVthdBGMjFEA

¡Muchas gracias!
        */

    }

    private void BtnLimpiar_Click(object sender, EventArgs e)
    {
        //LimpiarTextosFotos();

        TxtFotosDia.Text = "";
    }

    private void BtnPegar_Click(object sender, EventArgs e)
    {
        //LimpiarTextosFotos();

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

        TxtFotosDia.Text = s;
    }

    private void TxtFotosDia_TextChanged(object sender, EventArgs e)
    {
        TxtFotosSeleccionada.Text = TxtFotosDia.Text;
    }

    /// <summary>
    /// Comprobar si las horas de las reservas y la de las fotos coinciden.
    /// </summary>
    /// <param name="fecha"></param>
    /// <param name="conAlertas"></param>
    private bool ComprobarHorasFotosReservas(DateTime fecha, bool conAlertas)
    {
        //StringBuilder sb = new StringBuilder();
        //sb.Append("Select * from Reservas ");
        //sb.Append("where Activa = 1 and CanceladaCliente = 0 and idDistribuidor = 10 ");
        //// Solo las reservas confirmadas, por si las moscas (01/sep/23 19.49)
        //sb.Append($"and Confirmada = 1 ");
        //// Solo las rutas                                   (01/sep/23 21.15)
        //sb.Append($"and Actividad like 'ruta%' ");
        //sb.Append($"and FechaActividad = '{fecha:yyyy-MM-dd}' ");
        //sb.Append("order by FechaActividad, HoraActividad, ID");

        //var colRes = Reservas.TablaCol(sb.ToString());

        //// Crear una lista de reservas para el listview
        //List<ApiReservasMailGYG.ReservasGYG> col = new();

        //if (colRes.Count > 0)
        //{
        //    for (int i = 0; i < colRes.Count; i++)
        //    {
        //        col.Add(new ApiReservasMailGYG.ReservasGYG(colRes[i]));
        //    }
        //}
        //Form1.AsignarListView(col, LvwSinEmail);

        // No se mandan las fotos a las canceladas.         (10/sep/23 02.06)
        var col = ApiReservasMailGYG.MailGYG.DatosReservas(fecha, new TimeSpan(0, 0, 0), conAlquileres: false, conCanceladas: false);
        Form1.AsignarListView(col, LvwSinEmail);

        // Extraer las reservas de cada hora
        var hora = new TimeSpan(0, 0, 0);
        List<string> listHoras = new List<string>();
        //for (int i = 0; i < colRes.Count; i++)
        //{
        //    if (colRes[i].HoraActividad != hora)
        //    {
        //        hora = colRes[i].HoraActividad;
        //        listHoras.Add($"{colRes[i].HoraActividad:hh\\:mm}");
        //    }
        //}
        for (int i = 0; i < col.Count; i++)
        {
            if (col[i].Hora != hora)
            {
                hora = col[i].Hora;
                listHoras.Add($"{col[i].Hora:hh\\:mm}");
            }
        }

        // Las horas en las que hay reservas

        var fotosHoras = MailGYG.AnalizarTextoFotos(TxtFotosDia.Text);

        // contar las horas con fotos.                      (01/sep/23 21.02)
        int horasFotos = 0;
        foreach (var s in fotosHoras)
        {
            if (string.IsNullOrWhiteSpace(s) == false)
            {
                horasFotos++;
            }
        }
        if (horasFotos != listHoras.Count)
        {
            if (conAlertas)
                MessageBox.Show("La cantidad de horas de las reservas y el de las fotos no coincide." + CrLf +
                                $"Horas diferentes en las reserva: {listHoras.Count}." + CrLf +
                                $"Horas diferentes en las fotos: {horasFotos}.",
                                "Comprobar horas de reservas y fotos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            return true;
        }
        else
        {
            if (conAlertas)
                MessageBox.Show("La cantidad de horas de las reservas y el de las fotos son iguales:" + CrLf +
                               $"Horas diferentes en las reserva: {listHoras.Count}." + CrLf +
                               $"Horas diferentes en las fotos: {horasFotos}.",
                               "Comprobar horas de reservas y fotos", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        return false;
    }

    private void BtnComprobarReservasHoras_Click(object sender, EventArgs e)
    {
        var fecha = DateTimePickerGYG.Value.Date;

        ComprobarHorasFotosReservas(fecha, conAlertas: true);

        // Mostrar las reservas que hay por si alguna de las fotos no hay reservas en esa hora.

    }

    private void ContextMenuListView_Opening(object sender, System.ComponentModel.CancelEventArgs e)
    {
        bool hab = LvwSinEmail.SelectedIndices.Count > 0;
        foreach (var mnu in ContextMenuListView.Items)
        {
            (mnu as ToolStripMenuItem).Enabled = hab;
        }
    }

    //public static string[] Columnas { get; } = { "Booking", "Nombre", "Teléfono", "Reserva", "PAX", "Email", "Notas" };
    private void MnuCopiarDeLvw_Click(object sender, EventArgs e)
    {
        Form1.CopiarDeLvw(sender, LvwSinEmail);
    }
}
