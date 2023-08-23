using System.Drawing;
using System.Windows.Forms;

namespace ReservasGYG
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            toolTip1 = new ToolTip(components);
            GrbOpciones = new GroupBox();
            BtnComprobarSinMail = new Button();
            BtnFotos = new Button();
            BtnHoyEs = new Button();
            BtnMostrarReservas = new Button();
            BtnAnalizarEmail = new Button();
            DateTimePickerGYG = new DateTimePicker();
            label8 = new Label();
            GrbOpciones.SuspendLayout();
            SuspendLayout();
            // 
            // GrbOpciones
            // 
            GrbOpciones.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            GrbOpciones.Controls.Add(DateTimePickerGYG);
            GrbOpciones.Controls.Add(label8);
            GrbOpciones.Controls.Add(BtnComprobarSinMail);
            GrbOpciones.Controls.Add(BtnFotos);
            GrbOpciones.Controls.Add(BtnHoyEs);
            GrbOpciones.Controls.Add(BtnMostrarReservas);
            GrbOpciones.Controls.Add(BtnAnalizarEmail);
            GrbOpciones.Location = new Point(12, 12);
            GrbOpciones.Name = "GrbOpciones";
            GrbOpciones.Size = new Size(776, 436);
            GrbOpciones.TabIndex = 0;
            GrbOpciones.TabStop = false;
            GrbOpciones.Text = "Opciones";
            // 
            // BtnComprobarSinMail
            // 
            BtnComprobarSinMail.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            BtnComprobarSinMail.BackColor = Color.FloralWhite;
            BtnComprobarSinMail.Location = new Point(6, 340);
            BtnComprobarSinMail.Name = "BtnComprobarSinMail";
            BtnComprobarSinMail.Size = new Size(764, 40);
            BtnComprobarSinMail.TabIndex = 4;
            BtnComprobarSinMail.Text = "Comprobar si hay clientes sin mail";
            BtnComprobarSinMail.UseVisualStyleBackColor = false;
            BtnComprobarSinMail.Click += BtnComprobarSinMail_Click;
            // 
            // BtnFotos
            // 
            BtnFotos.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            BtnFotos.BackColor = Color.LightCoral;
            BtnFotos.Location = new Point(6, 150);
            BtnFotos.Name = "BtnFotos";
            BtnFotos.Size = new Size(764, 40);
            BtnFotos.TabIndex = 3;
            BtnFotos.Text = "Enviar fotos";
            BtnFotos.UseVisualStyleBackColor = false;
            BtnFotos.Click += BtnFotos_Click;
            // 
            // BtnHoyEs
            // 
            BtnHoyEs.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            BtnHoyEs.BackColor = Color.Gold;
            BtnHoyEs.Location = new Point(6, 280);
            BtnHoyEs.Name = "BtnHoyEs";
            BtnHoyEs.Size = new Size(758, 40);
            BtnHoyEs.TabIndex = 2;
            BtnHoyEs.Text = "Enviar hoy es el día";
            BtnHoyEs.UseVisualStyleBackColor = false;
            BtnHoyEs.Click += BtnHoyEs_Click;
            // 
            // BtnMostrarReservas
            // 
            BtnMostrarReservas.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            BtnMostrarReservas.BackColor = Color.LightSkyBlue;
            BtnMostrarReservas.Location = new Point(6, 90);
            BtnMostrarReservas.Name = "BtnMostrarReservas";
            BtnMostrarReservas.Size = new Size(764, 40);
            BtnMostrarReservas.TabIndex = 1;
            BtnMostrarReservas.Text = "Mostrar Reservas";
            BtnMostrarReservas.UseVisualStyleBackColor = false;
            BtnMostrarReservas.Click += BtnMostrarReservas_Click;
            // 
            // BtnAnalizarEmail
            // 
            BtnAnalizarEmail.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            BtnAnalizarEmail.BackColor = Color.Honeydew;
            BtnAnalizarEmail.Location = new Point(6, 30);
            BtnAnalizarEmail.Name = "BtnAnalizarEmail";
            BtnAnalizarEmail.Size = new Size(764, 40);
            BtnAnalizarEmail.TabIndex = 0;
            BtnAnalizarEmail.Text = "Analizar email y crear reserva";
            BtnAnalizarEmail.UseVisualStyleBackColor = false;
            BtnAnalizarEmail.Click += BtnAnalizarEmail_Click;
            // 
            // DateTimePickerGYG
            // 
            DateTimePickerGYG.CustomFormat = "dd/MM/yyyy";
            DateTimePickerGYG.Format = DateTimePickerFormat.Custom;
            DateTimePickerGYG.Location = new Point(198, 219);
            DateTimePickerGYG.Name = "DateTimePickerGYG";
            DateTimePickerGYG.Size = new Size(171, 31);
            DateTimePickerGYG.TabIndex = 6;
            // 
            // label8
            // 
            label8.Location = new Point(12, 224);
            label8.Margin = new Padding(6, 3, 3, 3);
            label8.Name = "label8";
            label8.Size = new Size(180, 31);
            label8.TabIndex = 5;
            label8.Text = "Selecciona la fecha:";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 520);
            Controls.Add(GrbOpciones);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form1";
            Load += Form1_Load;
            GrbOpciones.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private ToolTip toolTip1;
        private GroupBox GrbOpciones;
        private Button BtnAnalizarEmail;
        private Button BtnFotos;
        private Button BtnHoyEs;
        private Button BtnMostrarReservas;
        private Button BtnComprobarSinMail;
        private DateTimePicker DateTimePickerGYG;
        private Label label8;
    }
}