// Para mandar las fotos a los del día y hora indicada.     (22/ago/23 16.50)

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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

    private void LimpiarTextosFotos()
    {
        // Limpiar el contenido de los textBox
        foreach (var c in FlowLayoutPanelFotos.Controls)
        {
            var txt = c as TextBox;
            if (txt == null) continue;

            txt.Text = "";
        }
        TxtFotosSeleccionada.Text = "";
        //TxtFotosDia.Text = "";
    }

    private void FormEnviarFotos_Load(object sender, EventArgs e)
    {

        LimpiarTextosFotos();
        TxtFotosDia.Text = "";

        BtnPegar.Image = Properties.Resources.Paste;
        BtnLimpiar.Image = Properties.Resources.CleanData;

        //DateTimePickerGYG.Value = DateTime.Now;

        // Llenar el combo con las horas.
        CboHoras.Items.Clear();
        CboHoras.Items.AddRange(MailGYG.LasHoras.ToArray());

        // Colorear las cajas de textos de las horas.       (23/ago/23 11.47)
        TimerColorearHoras.Interval = 300;
        TimerColorearHoras.Enabled = true;

        inicializando = false;
    }

    private void CboHoras_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (inicializando) return;

        int index = CboHoras.SelectedIndex;
        if (index < 0 || string.IsNullOrWhiteSpace(CboHoras.Text))
        {
            BtnEnviarFotos.Enabled = false;
            //BtnEnviarFotosDia.Enabled = false;
            TxtFotosSeleccionada.Text = "";
            return;
        }

        if (CboHoras.Tag == null)
        {
            // No hay datos asignados.
            MessageBox.Show("No hay datos con las fotos a enviar.", "Seleccionar hora con las fotos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        List<string> listaHoras = new();

        listaHoras = (List<string>)CboHoras.Tag;

        TxtFotosSeleccionada.Text = listaHoras[CboHoras.SelectedIndex];

        BtnEnviarFotos.Text = $"Enviar las fotos de {CboHoras.Text} {DateTimePickerGYG.Value:dd/MM/yyyy}";
        BtnEnviarFotosDia.Text = $"Enviar todas las fotos del día {DateTimePickerGYG.Value:dd/MM/yyyy}";

        //// Seleccionar el textBox de la hora seleccionada.
        //TextBox txt = null;
        //switch (index)
        //{
        //    case 0: txt = TxtFotos0930; break;
        //    case 1: txt = TxtFotos1030; break;
        //    case 2: txt = TxtFotos1100; break;
        //    case 3: txt = TxtFotos1105; break; // Ruta tablas
        //    case 4: txt = TxtFotos1145; break;
        //    case 5: txt = TxtFotos1315; break;
        //    case 6: txt = TxtFotos1330; break;
        //    case 7: txt = TxtFotos1400; break;
        //    case 8: txt = TxtFotos1530; break;
        //    case 9: txt = TxtFotos1615; break;
        //    case 10: txt = TxtFotos1630; break;
        //    case 11: txt = TxtFotos1745; break;
        //    case 12: txt = TxtFotos1830; break;
        //    default: break;
        //}
        //if (txt == null || string.IsNullOrEmpty(txt.Text))
        //{
        //    //BtnEnviarFotos.Text = $"No hay fotos que enviar de {CboHoras.Text} {DateTimePickerGYG.Value:dd/MM/yyyy}";
        //    BtnEnviarFotos.Enabled = false;
        //    TxtFotosSeleccionada.Text = "";
        //    //return;
        //}
        //else
        //{
        //    BtnEnviarFotos.Enabled = true;
        //    txt.Focus();
        //}

        //TxtFotosHoras_Enter(txt, null);
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

    private void TxtFotosHoras_Enter(object sender, EventArgs e)
    {
        if (inicializando) return;

        // Seleccionar todo el texto,                   (22/ago/23 17.45)
        // copiarlo en el portapapeles
        // y si no es TxtFotosSeleccionada pegarlo en ese control
        var txt = sender as TextBox;
        if (txt == null) return;
        if (string.IsNullOrWhiteSpace(txt.Text)) return;

        if (sender != TxtFotosSeleccionada)
        {
            TxtFotosSeleccionada.Text = txt.Text;
        }

        txt.SelectAll();
        Clipboard.SetText(txt.Text);
    }

    private void BtnEnviarFotos_Click(object sender, EventArgs e)
    {
        if (ComprobarTextoFotos()) return;

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
        if (ComprobarTextoFotos()) return;

        int totalFotos = 0;

        var fecha = DateTimePickerGYG.Value;

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
                //return;
            }
            totalFotos++;
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
        sb.Append("Kayak Makarena");

        string body = sb.ToString().Replace(CrLf, "<br/>");

        MailGYG.EnviarMensaje(list, $"Fotos Ruta {fecha:dd/MM/yyyy} {hora:hh\\:mm}", body, true);

        return false;
    }

    private bool ComprobarTextoFotos()
    {
        var fecha = DateTimePickerGYG.Value.Date;
        string res = ComprobarEmails(fecha, conCabecera:true);

        if (string.IsNullOrWhiteSpace(res) == false)
        {
            MessageBox.Show($"Hay reservas del {fecha:dddd dd/MM/yyyy} sin emails.{CrLf}{CrLf}{res}", 
                            "Comprobar reservas sin email", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return true;
        }

        return false;
    }

    private void BtnComprobarEmails_Click(object sender, EventArgs e)
    {
        BtnEnviarFotos.Enabled = false;
        BtnEnviarFotosDia.Enabled = false;

        // Habilitar enviar fotos solo si todos tienen email y hay seleccionado algo

        var fecha = DateTimePickerGYG.Value.Date;

        string res = ComprobarEmails(fecha, conCabecera: true);

        if (string.IsNullOrWhiteSpace(res) == false)
        {
            MessageBox.Show($"Hay reservas del {fecha:dddd dd/MM/yyyy} sin emails.{CrLf}{res}", "Comprobar reservas sin email", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        else
        {
            MessageBox.Show($"Todas las reservas de la fecha '{fecha:dddd dd/MM/yyyy}' tiene asignado el email.", "Comprobar reservas sin email", MessageBoxButtons.OK, MessageBoxIcon.Information);
            BtnEnviarFotos.Enabled = true;
            BtnEnviarFotosDia.Enabled = true;
        }
    }

    private void TimerColorearHoras_Tick(object sender, EventArgs e)
    {
        TimerColorearHoras.Enabled = false;

        // Ruta corta de color verdoso Honeydew
        TxtFotos0930.BackColor = Color.Honeydew;
        TxtFotos1100.BackColor = Color.Honeydew;
        TxtFotos1145.BackColor = Color.Honeydew;
        TxtFotos1315.BackColor = Color.Honeydew;
        TxtFotos1530.BackColor = Color.Honeydew;
        TxtFotos1615.BackColor = Color.Honeydew;
        TxtFotos1745.BackColor = Color.Honeydew;

        // Ruta larga de color azulado AliceBlue
        TxtFotos1030.BackColor = Color.AliceBlue;
        TxtFotos1330.BackColor = Color.AliceBlue;
        TxtFotos1630.BackColor = Color.AliceBlue;

        // Rutas sin datos de color rojizo MistyRose
        TxtFotos1400.BackColor = Color.MistyRose;
        TxtFotos1830.BackColor = Color.MistyRose;

        TxtFotos1105.BackColor = Color.LightCoral;
    }

    private void BtnExtraerHoras_Click(object sender, EventArgs e)
    {
        LimpiarTextosFotos();

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

        //// Añadir cada hora a la caja de textos que corresponda.
        //for (int i = 0; i < fotosHoras.Count; i++)
        //{
        //    if (fotosHoras[i].Contains("09.30"))
        //    {
        //        TxtFotos0930.Text = fotosHoras[i];
        //    }
        //    else if (fotosHoras[i].Contains("10.30"))
        //    {
        //        TxtFotos1030.Text = fotosHoras[i];
        //    }
        //    else if (fotosHoras[i].Contains("11.00"))
        //    {
        //        TxtFotos1100.Text = fotosHoras[i];
        //    }
        //    else if (fotosHoras[i].Contains("11.05")) // Ruta tablas
        //    {
        //        TxtFotos1105.Text = fotosHoras[i];
        //    }
        //    else if (fotosHoras[i].Contains("11.45"))
        //    {
        //        TxtFotos1145.Text = fotosHoras[i];
        //    }
        //    else if (fotosHoras[i].Contains("13.15"))
        //    {
        //        TxtFotos1315.Text = fotosHoras[i];
        //    }
        //    else if (fotosHoras[i].Contains("13.30"))
        //    {
        //        TxtFotos1330.Text = fotosHoras[i];
        //    }
        //    else if (fotosHoras[i].Contains("14.00"))
        //    {
        //        TxtFotos1400.Text = fotosHoras[i];
        //    }
        //    else if (fotosHoras[i].Contains("15.30"))
        //    {
        //        TxtFotos1530.Text = fotosHoras[i];
        //    }
        //    else if (fotosHoras[i].Contains("16.15"))
        //    {
        //        TxtFotos1615.Text = fotosHoras[i];
        //    }
        //    else if (fotosHoras[i].Contains("16.30"))
        //    {
        //        TxtFotos1630.Text = fotosHoras[i];
        //    }
        //    else if (fotosHoras[i].Contains("17.45"))
        //    {
        //        TxtFotos1745.Text = fotosHoras[i];
        //    }
        //    else if (fotosHoras[i].Contains("18.30"))
        //    {
        //        TxtFotos1830.Text = fotosHoras[i];
        //    }
        //}

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
        LimpiarTextosFotos();

        TxtFotosDia.Text = "";
    }

    private void BtnPegar_Click(object sender, EventArgs e)
    {
        LimpiarTextosFotos();

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
}
