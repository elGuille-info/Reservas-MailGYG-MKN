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
            BtnAnalizarEmail = new Button();
            BtnFotos = new Button();
            GrbOpcionesFecha = new GroupBox();
            BtnReservasSinSalida = new Button();
            LvwSinEmail = new ListView();
            columnHeader1 = new ColumnHeader();
            columnHeader2 = new ColumnHeader();
            ContextMenuListView = new ContextMenuStrip(components);
            MnuCopiarNombre = new ToolStripMenuItem();
            MnuCopiarNotas = new ToolStripMenuItem();
            BtnMañanaEs = new Button();
            BtnMostrarReservas = new Button();
            DateTimePickerGYG = new DateTimePicker();
            label8 = new Label();
            BtnComprobarSinMail = new Button();
            BtnHoyEs = new Button();
            TimerCargarAnalizarEmail = new Timer(components);
            GrbOpciones.SuspendLayout();
            GrbOpcionesFecha.SuspendLayout();
            ContextMenuListView.SuspendLayout();
            SuspendLayout();
            // 
            // GrbOpciones
            // 
            GrbOpciones.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            GrbOpciones.Controls.Add(BtnAnalizarEmail);
            GrbOpciones.Location = new Point(12, 12);
            GrbOpciones.Name = "GrbOpciones";
            GrbOpciones.Size = new Size(1121, 104);
            GrbOpciones.TabIndex = 0;
            GrbOpciones.TabStop = false;
            GrbOpciones.Text = "Opciones crear reservas";
            // 
            // BtnAnalizarEmail
            // 
            BtnAnalizarEmail.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            BtnAnalizarEmail.BackColor = Color.Honeydew;
            BtnAnalizarEmail.Location = new Point(6, 30);
            BtnAnalizarEmail.Name = "BtnAnalizarEmail";
            BtnAnalizarEmail.Size = new Size(1109, 40);
            BtnAnalizarEmail.TabIndex = 0;
            BtnAnalizarEmail.Text = "Analizar email y crear reserva";
            BtnAnalizarEmail.UseVisualStyleBackColor = false;
            BtnAnalizarEmail.Click += BtnAnalizarEmail_Click;
            // 
            // BtnFotos
            // 
            BtnFotos.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            BtnFotos.BackColor = Color.MistyRose;
            BtnFotos.Location = new Point(6, 448);
            BtnFotos.Margin = new Padding(3, 12, 3, 3);
            BtnFotos.Name = "BtnFotos";
            BtnFotos.Size = new Size(1103, 40);
            BtnFotos.TabIndex = 6;
            BtnFotos.Text = "Enviar fotos";
            BtnFotos.UseVisualStyleBackColor = false;
            BtnFotos.Click += BtnFotos_Click;
            // 
            // GrbOpcionesFecha
            // 
            GrbOpcionesFecha.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            GrbOpcionesFecha.Controls.Add(BtnReservasSinSalida);
            GrbOpcionesFecha.Controls.Add(LvwSinEmail);
            GrbOpcionesFecha.Controls.Add(BtnFotos);
            GrbOpcionesFecha.Controls.Add(BtnMañanaEs);
            GrbOpcionesFecha.Controls.Add(BtnMostrarReservas);
            GrbOpcionesFecha.Controls.Add(DateTimePickerGYG);
            GrbOpcionesFecha.Controls.Add(label8);
            GrbOpcionesFecha.Controls.Add(BtnComprobarSinMail);
            GrbOpcionesFecha.Controls.Add(BtnHoyEs);
            GrbOpcionesFecha.Location = new Point(12, 122);
            GrbOpcionesFecha.Name = "GrbOpcionesFecha";
            GrbOpcionesFecha.Size = new Size(1115, 598);
            GrbOpcionesFecha.TabIndex = 1;
            GrbOpcionesFecha.TabStop = false;
            GrbOpcionesFecha.Text = "Opciones con fecha";
            // 
            // BtnReservasSinSalida
            // 
            BtnReservasSinSalida.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            BtnReservasSinSalida.BackColor = Color.PeachPuff;
            BtnReservasSinSalida.Location = new Point(6, 135);
            BtnReservasSinSalida.Margin = new Padding(3, 12, 3, 3);
            BtnReservasSinSalida.Name = "BtnReservasSinSalida";
            BtnReservasSinSalida.Size = new Size(1103, 40);
            BtnReservasSinSalida.TabIndex = 3;
            BtnReservasSinSalida.Text = "Comprobar reservas sin salida (not show)";
            BtnReservasSinSalida.UseVisualStyleBackColor = false;
            BtnReservasSinSalida.Click += BtnReservasSinSalida_Click;
            // 
            // LvwSinEmail
            // 
            LvwSinEmail.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            LvwSinEmail.Columns.AddRange(new ColumnHeader[] { columnHeader1, columnHeader2 });
            LvwSinEmail.ContextMenuStrip = ContextMenuListView;
            LvwSinEmail.FullRowSelect = true;
            LvwSinEmail.GridLines = true;
            LvwSinEmail.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            LvwSinEmail.Location = new Point(9, 239);
            LvwSinEmail.Margin = new Padding(3, 6, 3, 12);
            LvwSinEmail.MultiSelect = false;
            LvwSinEmail.Name = "LvwSinEmail";
            LvwSinEmail.Size = new Size(1100, 185);
            LvwSinEmail.TabIndex = 5;
            LvwSinEmail.UseCompatibleStateImageBehavior = false;
            LvwSinEmail.View = View.Details;
            // 
            // columnHeader1
            // 
            columnHeader1.Text = "Booking";
            columnHeader1.Width = 200;
            // 
            // columnHeader2
            // 
            columnHeader2.Text = "Nombre";
            columnHeader2.Width = 400;
            // 
            // ContextMenuListView
            // 
            ContextMenuListView.ImageScalingSize = new Size(24, 24);
            ContextMenuListView.Items.AddRange(new ToolStripItem[] { MnuCopiarNombre, MnuCopiarNotas });
            ContextMenuListView.Name = "ContextMenuListView";
            ContextMenuListView.Size = new Size(208, 68);
            // 
            // MnuCopiarNombre
            // 
            MnuCopiarNombre.Name = "MnuCopiarNombre";
            MnuCopiarNombre.Size = new Size(207, 32);
            MnuCopiarNombre.Text = "Copiar Nombre";
            MnuCopiarNombre.Click += MnuCopiarNombre_Click;
            // 
            // MnuCopiarNotas
            // 
            MnuCopiarNotas.Name = "MnuCopiarNotas";
            MnuCopiarNotas.Size = new Size(207, 32);
            MnuCopiarNotas.Text = "Copiar Notas";
            MnuCopiarNotas.Click += MnuCopiarNotas_Click;
            // 
            // BtnMañanaEs
            // 
            BtnMañanaEs.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            BtnMañanaEs.BackColor = Color.LightYellow;
            BtnMañanaEs.Location = new Point(6, 503);
            BtnMañanaEs.Margin = new Padding(3, 12, 3, 3);
            BtnMañanaEs.Name = "BtnMañanaEs";
            BtnMañanaEs.Size = new Size(1103, 40);
            BtnMañanaEs.TabIndex = 7;
            BtnMañanaEs.Text = "Enviar MAÑANA es el día";
            BtnMañanaEs.UseVisualStyleBackColor = false;
            BtnMañanaEs.Click += BtnMañanaEs_Click;
            // 
            // BtnMostrarReservas
            // 
            BtnMostrarReservas.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            BtnMostrarReservas.BackColor = Color.LightSkyBlue;
            BtnMostrarReservas.Location = new Point(6, 190);
            BtnMostrarReservas.Margin = new Padding(3, 12, 3, 3);
            BtnMostrarReservas.Name = "BtnMostrarReservas";
            BtnMostrarReservas.Size = new Size(1103, 40);
            BtnMostrarReservas.TabIndex = 4;
            BtnMostrarReservas.Text = "Mostrar Reservas";
            BtnMostrarReservas.UseVisualStyleBackColor = false;
            BtnMostrarReservas.Click += BtnMostrarReservas_Click;
            // 
            // DateTimePickerGYG
            // 
            DateTimePickerGYG.CustomFormat = "dd/MM/yyyy";
            DateTimePickerGYG.Format = DateTimePickerFormat.Custom;
            DateTimePickerGYG.Location = new Point(195, 30);
            DateTimePickerGYG.Name = "DateTimePickerGYG";
            DateTimePickerGYG.Size = new Size(171, 31);
            DateTimePickerGYG.TabIndex = 1;
            DateTimePickerGYG.ValueChanged += DateTimePickerGYG_ValueChanged;
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
            BtnComprobarSinMail.Location = new Point(6, 80);
            BtnComprobarSinMail.Name = "BtnComprobarSinMail";
            BtnComprobarSinMail.Size = new Size(1103, 40);
            BtnComprobarSinMail.TabIndex = 2;
            BtnComprobarSinMail.Text = "Comprobar reservas sin email en la fecha";
            BtnComprobarSinMail.UseVisualStyleBackColor = false;
            BtnComprobarSinMail.Click += BtnComprobarSinMail_Click;
            // 
            // BtnHoyEs
            // 
            BtnHoyEs.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            BtnHoyEs.BackColor = Color.Gold;
            BtnHoyEs.Location = new Point(6, 552);
            BtnHoyEs.Margin = new Padding(3, 6, 3, 3);
            BtnHoyEs.Name = "BtnHoyEs";
            BtnHoyEs.Size = new Size(1103, 40);
            BtnHoyEs.TabIndex = 8;
            BtnHoyEs.Text = "Enviar HOY es el día";
            BtnHoyEs.UseVisualStyleBackColor = false;
            BtnHoyEs.Click += BtnHoyEs_Click;
            // 
            // TimerCargarAnalizarEmail
            // 
            TimerCargarAnalizarEmail.Interval = 300;
            TimerCargarAnalizarEmail.Tick += TimerCargarAnalizarEmail_Tick;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1145, 732);
            Controls.Add(GrbOpcionesFecha);
            Controls.Add(GrbOpciones);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Opciones Reservas GetYourGuide";
            FormClosing += Form1_FormClosing;
            Load += Form1_Load;
            Resize += Form1_Resize;
            GrbOpciones.ResumeLayout(false);
            GrbOpcionesFecha.ResumeLayout(false);
            ContextMenuListView.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private ToolTip toolTip1;
        private GroupBox GrbOpciones;
        private Button BtnAnalizarEmail;
        private Button BtnFotos;
        private GroupBox GrbOpcionesFecha;
        private DateTimePicker DateTimePickerGYG;
        private Label label8;
        private Button BtnComprobarSinMail;
        private Button BtnHoyEs;
        private Button BtnMostrarReservas;
        private Timer TimerCargarAnalizarEmail;
        private Button BtnMañanaEs;
        private ListView LvwSinEmail;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private ContextMenuStrip ContextMenuListView;
        private ToolStripMenuItem MnuCopiarNombre;
        private ToolStripMenuItem MnuCopiarNotas;
        private Button BtnReservasSinSalida;
    }
}