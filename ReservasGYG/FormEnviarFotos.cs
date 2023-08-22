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

namespace ReservasGYG
{
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
            foreach(var c in FlowLayoutPanelFotos.Controls) 
            {
                var txt = c as TextBox;
                if (txt == null) continue;

                txt.Text = "";
            }
            TxtFotosSeleccionada.Text = "";
            
            DateTimePickerFotos.Value = DateTime.Now;

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
            BtnEnviarFotos.Text = $"Enviar las fotos de {CboHoras.Text} {DateTimePickerFotos.Value:dd/MM/yyyy}";

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
            if (DateTimePickerFotos.Value.Date >= DateTime.Today)
            {
                DateTimePickerFotos.Show();
                return;
            }

            int index = CboHoras.SelectedIndex;
            if (index < 0 || string.IsNullOrWhiteSpace(CboHoras.Text))
            {
                BtnEnviarFotos.Enabled = false;
                return;
            }

            BtnEnviarFotos.Text = $"Enviar las fotos de {CboHoras.Text} {DateTimePickerFotos.Value:dd/MM/yyyy}";
        }

        private void TxtFotosHoras_Enter(object sender, EventArgs e)
        {
            if (inicializando) return;

            // Seleccionar todo el texto,                   (22/ago/23 17.45)
            // copiarlo en el portapapeles
            // y si no es TxtFotosSeleccionada pegarlo en ese control
            var txt = sender as TextBox;
            if (txt == null) return;
            if (string.IsNullOrWhiteSpace (txt.Text)) return;

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

            BtnEnviarFotos.Enabled = true;
        }
    }
}
