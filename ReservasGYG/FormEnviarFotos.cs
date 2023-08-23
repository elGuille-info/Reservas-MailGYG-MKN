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

using KNDatos;

using static ApiReservasMailGYG.MailGYG;

namespace ReservasGYG;

public partial class FormEnviarFotos : Form
{
    private bool inicializando = true;
    public static FormEnviarFotos Current;
    public FormEnviarFotos()
    {
        InitializeComponent();
        Current = this;
    }

    private bool FaltanEmails { get; set; } = true;
    private void FormEnviarFotos_Load(object sender, EventArgs e)
    {
        // Limpiar el contenido de los textBox
        foreach (var c in FlowLayoutPanelFotos.Controls)
        {
            var txt = c as TextBox;
            if (txt == null) continue;

            txt.Text = "";
        }
        TxtFotosSeleccionada.Text = "";

        DateTimePickerGYG.Value = DateTime.Now;

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
            return;
        }
        //BtnEnviarFotos.Enabled = true;
        BtnEnviarFotos.Text = $"Enviar las fotos de {CboHoras.Text} {DateTimePickerGYG.Value:dd/MM/yyyy}";

        // Seleccionar el textBox de la hora seleccionada.
        TextBox txt = null;
        switch (index)
        {
            case 0: txt = TxtFotos0930; break;
            case 1: txt = TxtFotos1030; break;
            case 2: txt = txtFotos1100; break;
            case 3: txt = txtFotos1145; break;
            case 4: txt = TxtFotos1315; break;
            case 5: txt = TxtFotos1330; break;
            case 6: txt = TxtFotos1400; break;
            case 7: txt = TxtFotos1530; break;
            case 8: txt = TxtFotos1615; break;
            case 9: txt = TxtFotos1630; break;
            case 10: txt = TxtFotos1745; break;
            case 11: txt = TxtFotos1800; break;
            default: break;
        }
        if (txt == null) return;

        if (string.IsNullOrEmpty(txt.Text)) return;
        txt.Focus();
        TxtFotosHoras_Enter(txt, null);
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
        string msg = "";

        if (FaltanEmails)
        {
            msg = "No se ha comprobado si faltan emails o faltan emails por meter.";
        }
        // Enviar el texto indicado en TxtFotosSeleccionada a las reservas de la fecha y hora seleccionada
        if (string.IsNullOrWhiteSpace(TxtFotosSeleccionada.Text))
        {
            msg = "No hay texto de las fotos a enviar.";
        }
        if (string.IsNullOrEmpty(msg) == false)
        {
            MessageBox.Show(msg, "No se pueden mandar las fotos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            return;
        }
        // Mandar los mensaje con las fotos.
    }

    private void BtnComprobarEmails_Click(object sender, EventArgs e)
    {
        BtnEnviarFotos.Enabled = false;

        // Habilitar enviar fotos solo si todos tienen email y hay seleccionado algo

        var fecha = DateTimePickerGYG.Value.Date;

        /*
Select ID, FechaActividad, HoraActividad, Nombre, Telefono, Email, Notas from reservas 
where Activa = 1 and CanceladaCliente = 0 and idDistribuidor = 10
and Email = '' and Nombre != 'Makarena (GYG)'
and FechaActividad = '2023-08-23'
order by FechaActividad, HoraActividad, ID

        */
        StringBuilder sb = new StringBuilder();
        sb.Append("Select ID, FechaActividad, HoraActividad, Nombre, Telefono, Email, Notas from reservas ");
        sb.Append("where Activa = 1 and CanceladaCliente = 0 and idDistribuidor = 10 ");
        sb.Append("and Email = '' and Nombre != 'Makarena (GYG)' ");
        sb.Append($"and FechaActividad = '{fecha:yyyy-MM-dd}' ");
        sb.Append("order by FechaActividad, HoraActividad, ID");

        var colRes = Reservas.TablaCol(sb.ToString());

        if (colRes.Count > 0)
        {
            sb.Clear();
            for (int i = 0; i < colRes.Count; i++)
            {
                sb.AppendLine($"{colRes[i].Nombre}, {colRes[i].Notas}");
            }
            MessageBox.Show($"Hay {colRes.Count} {colRes.Count.Plural("reserva")} del {fecha:dddd dd/MM/yyyy} sin email.\r\nSi envías los mensajes a esos clientes no les llegará.\r\n" + sb.ToString(), "Comprobar reservas sin email", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //return;
        }
        else
        {
            MessageBox.Show("Todas las reservas de la fecha tiene asignado el email.", "Comprobar reservas sin email", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        BtnEnviarFotos.Enabled = true;
    }

    private void TimerColorearHoras_Tick(object sender, EventArgs e)
    {
        TimerColorearHoras.Enabled = false;

        // Ruta corta de color verdoso Honeydew
        TxtFotos0930.BackColor = Color.Honeydew;
        txtFotos1100.BackColor = Color.Honeydew;
        txtFotos1145.BackColor = Color.Honeydew;
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
        TxtFotos1800.BackColor = Color.MistyRose;

        
    }
}
