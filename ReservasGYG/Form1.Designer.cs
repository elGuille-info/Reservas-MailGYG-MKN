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
            BtnFotos = new Button();
            BtnAnalizarEmail = new Button();
            groupBox1 = new GroupBox();
            DateTimePickerGYG = new DateTimePicker();
            label8 = new Label();
            BtnComprobarSinMail = new Button();
            BtnHoyEs = new Button();
            BtnMostrarReservas = new Button();
            GrbOpciones.SuspendLayout();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // GrbOpciones
            // 
            GrbOpciones.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            GrbOpciones.Controls.Add(BtnFotos);
            GrbOpciones.Controls.Add(BtnAnalizarEmail);
            GrbOpciones.Location = new Point(12, 12);
            GrbOpciones.Name = "GrbOpciones";
            GrbOpciones.Size = new Size(776, 496);
            GrbOpciones.TabIndex = 0;
            GrbOpciones.TabStop = false;
            GrbOpciones.Text = "Opciones";
            // 
            // BtnFotos
            // 
            BtnFotos.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            BtnFotos.BackColor = Color.LightCoral;
            BtnFotos.Location = new Point(6, 90);
            BtnFotos.Name = "BtnFotos";
            BtnFotos.Size = new Size(764, 40);
            BtnFotos.TabIndex = 1;
            BtnFotos.Text = "Enviar fotos";
            BtnFotos.UseVisualStyleBackColor = false;
            BtnFotos.Click += BtnFotos_Click;
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
            // groupBox1
            // 
            groupBox1.Controls.Add(BtnMostrarReservas);
            groupBox1.Controls.Add(DateTimePickerGYG);
            groupBox1.Controls.Add(label8);
            groupBox1.Controls.Add(BtnComprobarSinMail);
            groupBox1.Controls.Add(BtnHoyEs);
            groupBox1.Location = new Point(12, 180);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(770, 328);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "Acciones con fecha";
            // 
            // DateTimePickerGYG
            // 
            DateTimePickerGYG.CustomFormat = "dd/MM/yyyy";
            DateTimePickerGYG.Format = DateTimePickerFormat.Custom;
            DateTimePickerGYG.Location = new Point(195, 30);
            DateTimePickerGYG.Name = "DateTimePickerGYG";
            DateTimePickerGYG.Size = new Size(171, 31);
            DateTimePickerGYG.TabIndex = 1;
            // 
            // label8
            // 
            label8.Location = new Point(9, 32);
            label8.Margin = new Padding(6, 3, 3, 3);
            label8.Name = "label8";
            label8.Size = new Size(180, 31);
            label8.TabIndex = 0;
            label8.Text = "Selecciona la fecha:";
            label8.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // BtnComprobarSinMail
            // 
            BtnComprobarSinMail.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            BtnComprobarSinMail.BackColor = Color.FloralWhite;
            BtnComprobarSinMail.Location = new Point(9, 140);
            BtnComprobarSinMail.Name = "BtnComprobarSinMail";
            BtnComprobarSinMail.Size = new Size(755, 40);
            BtnComprobarSinMail.TabIndex = 3;
            BtnComprobarSinMail.Text = "Comprobar si hay clientes sin email";
            BtnComprobarSinMail.UseVisualStyleBackColor = false;
            BtnComprobarSinMail.Click += BtnComprobarSinMail_Click;
            // 
            // BtnHoyEs
            // 
            BtnHoyEs.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            BtnHoyEs.BackColor = Color.Gold;
            BtnHoyEs.Location = new Point(6, 80);
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
            BtnMostrarReservas.Enabled = false;
            BtnMostrarReservas.Location = new Point(9, 200);
            BtnMostrarReservas.Name = "BtnMostrarReservas";
            BtnMostrarReservas.Size = new Size(755, 40);
            BtnMostrarReservas.TabIndex = 4;
            BtnMostrarReservas.Text = "Mostrar Reservas";
            BtnMostrarReservas.UseVisualStyleBackColor = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 520);
            Controls.Add(groupBox1);
            Controls.Add(GrbOpciones);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Opciones Reservas GetYourGuide";
            Load += Form1_Load;
            GrbOpciones.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private ToolTip toolTip1;
        private GroupBox GrbOpciones;
        private Button BtnAnalizarEmail;
        private Button BtnFotos;
        private GroupBox groupBox1;
        private DateTimePicker DateTimePickerGYG;
        private Label label8;
        private Button BtnComprobarSinMail;
        private Button BtnHoyEs;
        private Button BtnMostrarReservas;
    }
}