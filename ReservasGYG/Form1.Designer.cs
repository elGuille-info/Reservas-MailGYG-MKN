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
            ChkConAlquileres = new CheckBox();
            ChkConCanceladas = new CheckBox();
            ChkSoloCanceladas = new CheckBox();
            BtnFotos = new Button();
            GrbOpcionesFecha = new GroupBox();
            BtnAnalizar = new Button();
            GrbAvisos = new GroupBox();
            ChkIncluirTextoAviso = new CheckBox();
            TxtAvisoExtra = new TextBox();
            ContextMenuTextoAviso = new ContextMenuStrip(components);
            MenuPegarTextoOriginal = new ToolStripMenuItem();
            MnuPegarÚltimoTextoEnviado = new ToolStripMenuItem();
            PanelBotones = new Panel();
            BtnMañanaEs = new Button();
            BtnHoyEs = new Button();
            BtnAlerta1 = new Button();
            BtnAlerta2 = new Button();
            BtnAlerta3 = new Button();
            LabelInfoListView = new Label();
            BtnReservasSinSalida = new Button();
            LvwSinEmail = new ListView();
            columnHeader1 = new ColumnHeader();
            columnHeader2 = new ColumnHeader();
            ContextMenuListView = new ContextMenuStrip(components);
            MnuCopiarBooking = new ToolStripMenuItem();
            MnuCopiarNombre = new ToolStripMenuItem();
            MnuCopiarTelefono = new ToolStripMenuItem();
            MnuCopiarReserva = new ToolStripMenuItem();
            MnuCopiarPax = new ToolStripMenuItem();
            MnuCopiarEmail = new ToolStripMenuItem();
            MnuCopiarNotas = new ToolStripMenuItem();
            MnuSep1 = new ToolStripSeparator();
            MnuCopiarTodo = new ToolStripMenuItem();
            MnuCopiarTodoConCr = new ToolStripMenuItem();
            BtnMostrarReservas = new Button();
            DateTimePickerGYG = new DateTimePicker();
            label8 = new Label();
            BtnComprobarSinMail = new Button();
            TimerInicioForm1 = new Timer(components);
            GrbOpcionesFecha.SuspendLayout();
            GrbAvisos.SuspendLayout();
            ContextMenuTextoAviso.SuspendLayout();
            PanelBotones.SuspendLayout();
            ContextMenuListView.SuspendLayout();
            SuspendLayout();
            // 
            // ChkConAlquileres
            // 
            ChkConAlquileres.AutoSize = true;
            ChkConAlquileres.Checked = true;
            ChkConAlquileres.CheckState = CheckState.Checked;
            ChkConAlquileres.Location = new Point(451, 30);
            ChkConAlquileres.Name = "ChkConAlquileres";
            ChkConAlquileres.Size = new Size(164, 29);
            ChkConAlquileres.TabIndex = 2;
            ChkConAlquileres.Text = "Incluir alquileres";
            toolTip1.SetToolTip(ChkConAlquileres, "Incluir los alquileres al comprobar los emails y mostrar las reservas");
            ChkConAlquileres.UseVisualStyleBackColor = true;
            // 
            // ChkConCanceladas
            // 
            ChkConCanceladas.AutoSize = true;
            ChkConCanceladas.Location = new Point(621, 30);
            ChkConCanceladas.Name = "ChkConCanceladas";
            ChkConCanceladas.Size = new Size(175, 29);
            ChkConCanceladas.TabIndex = 3;
            ChkConCanceladas.Text = "Incluir canceladas";
            toolTip1.SetToolTip(ChkConCanceladas, "Incluir las reservas canceladas al mostrar las reservas");
            ChkConCanceladas.UseVisualStyleBackColor = true;
            // 
            // ChkSoloCanceladas
            // 
            ChkSoloCanceladas.AutoSize = true;
            ChkSoloCanceladas.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            ChkSoloCanceladas.ForeColor = Color.Firebrick;
            ChkSoloCanceladas.Location = new Point(802, 30);
            ChkSoloCanceladas.Name = "ChkSoloCanceladas";
            ChkSoloCanceladas.Size = new Size(201, 29);
            ChkSoloCanceladas.TabIndex = 4;
            ChkSoloCanceladas.Text = "Solo las canceladas";
            toolTip1.SetToolTip(ChkSoloCanceladas, "Mostrar solo las reservas canceladas");
            ChkSoloCanceladas.UseVisualStyleBackColor = true;
            // 
            // BtnFotos
            // 
            BtnFotos.BackColor = Color.MistyRose;
            BtnFotos.Location = new Point(662, 80);
            BtnFotos.Margin = new Padding(3, 12, 3, 3);
            BtnFotos.Name = "BtnFotos";
            BtnFotos.Size = new Size(645, 40);
            BtnFotos.TabIndex = 6;
            BtnFotos.Text = "Enviar fotos";
            BtnFotos.UseVisualStyleBackColor = false;
            BtnFotos.Click += BtnFotos_Click;
            // 
            // GrbOpcionesFecha
            // 
            GrbOpcionesFecha.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            GrbOpcionesFecha.Controls.Add(BtnAnalizar);
            GrbOpcionesFecha.Controls.Add(GrbAvisos);
            GrbOpcionesFecha.Controls.Add(LabelInfoListView);
            GrbOpcionesFecha.Controls.Add(ChkSoloCanceladas);
            GrbOpcionesFecha.Controls.Add(ChkConCanceladas);
            GrbOpcionesFecha.Controls.Add(ChkConAlquileres);
            GrbOpcionesFecha.Controls.Add(BtnReservasSinSalida);
            GrbOpcionesFecha.Controls.Add(LvwSinEmail);
            GrbOpcionesFecha.Controls.Add(BtnFotos);
            GrbOpcionesFecha.Controls.Add(BtnMostrarReservas);
            GrbOpcionesFecha.Controls.Add(DateTimePickerGYG);
            GrbOpcionesFecha.Controls.Add(label8);
            GrbOpcionesFecha.Controls.Add(BtnComprobarSinMail);
            GrbOpcionesFecha.Location = new Point(12, 12);
            GrbOpcionesFecha.Name = "GrbOpcionesFecha";
            GrbOpcionesFecha.Size = new Size(1316, 949);
            GrbOpcionesFecha.TabIndex = 0;
            GrbOpcionesFecha.TabStop = false;
            GrbOpcionesFecha.Text = "Opciones con fecha";
            // 
            // BtnAnalizar
            // 
            BtnAnalizar.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            BtnAnalizar.BackColor = Color.PaleTurquoise;
            BtnAnalizar.Location = new Point(1008, 571);
            BtnAnalizar.Margin = new Padding(3, 12, 3, 3);
            BtnAnalizar.Name = "BtnAnalizar";
            BtnAnalizar.Size = new Size(302, 40);
            BtnAnalizar.TabIndex = 12;
            BtnAnalizar.Text = "Mostrar analizar email";
            BtnAnalizar.UseVisualStyleBackColor = false;
            BtnAnalizar.Click += BtnAnalizar_Click;
            // 
            // GrbAvisos
            // 
            GrbAvisos.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            GrbAvisos.Controls.Add(ChkIncluirTextoAviso);
            GrbAvisos.Controls.Add(TxtAvisoExtra);
            GrbAvisos.Controls.Add(PanelBotones);
            GrbAvisos.Location = new Point(6, 608);
            GrbAvisos.Name = "GrbAvisos";
            GrbAvisos.Size = new Size(1310, 335);
            GrbAvisos.TabIndex = 11;
            GrbAvisos.TabStop = false;
            GrbAvisos.Text = "Enviar avisos";
            // 
            // ChkIncluirTextoAviso
            // 
            ChkIncluirTextoAviso.AutoSize = true;
            ChkIncluirTextoAviso.Location = new Point(6, 30);
            ChkIncluirTextoAviso.Name = "ChkIncluirTextoAviso";
            ChkIncluirTextoAviso.Size = new Size(455, 29);
            ChkIncluirTextoAviso.TabIndex = 0;
            ChkIncluirTextoAviso.Text = "Incluir este texto en Enviar es el día o en Enviar Alerta";
            ChkIncluirTextoAviso.UseVisualStyleBackColor = true;
            ChkIncluirTextoAviso.CheckedChanged += ChkIncluirTextoAviso_CheckedChanged;
            // 
            // TxtAvisoExtra
            // 
            TxtAvisoExtra.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            TxtAvisoExtra.ContextMenuStrip = ContextMenuTextoAviso;
            TxtAvisoExtra.Location = new Point(6, 65);
            TxtAvisoExtra.Multiline = true;
            TxtAvisoExtra.Name = "TxtAvisoExtra";
            TxtAvisoExtra.ScrollBars = ScrollBars.Both;
            TxtAvisoExtra.Size = new Size(1298, 152);
            TxtAvisoExtra.TabIndex = 1;
            TxtAvisoExtra.Text = resources.GetString("TxtAvisoExtra.Text");
            // 
            // ContextMenuTextoAviso
            // 
            ContextMenuTextoAviso.ImageScalingSize = new Size(24, 24);
            ContextMenuTextoAviso.Items.AddRange(new ToolStripItem[] { MenuPegarTextoOriginal, MnuPegarÚltimoTextoEnviado });
            ContextMenuTextoAviso.Name = "ContextMenuTextoAviso";
            ContextMenuTextoAviso.Size = new Size(316, 68);
            ContextMenuTextoAviso.Opening += ContextMenuTextoAviso_Opening;
            // 
            // MenuPegarTextoOriginal
            // 
            MenuPegarTextoOriginal.Name = "MenuPegarTextoOriginal";
            MenuPegarTextoOriginal.Size = new Size(315, 32);
            MenuPegarTextoOriginal.Text = "Pegar el texto original";
            MenuPegarTextoOriginal.Click += MenuPegarTextoOriginal_Click;
            // 
            // MnuPegarÚltimoTextoEnviado
            // 
            MnuPegarÚltimoTextoEnviado.Name = "MnuPegarÚltimoTextoEnviado";
            MnuPegarÚltimoTextoEnviado.Size = new Size(315, 32);
            MnuPegarÚltimoTextoEnviado.Text = "Pegar el último texto enviado";
            MnuPegarÚltimoTextoEnviado.Click += MnuPegarÚltimoTextoEnviado_Click;
            // 
            // PanelBotones
            // 
            PanelBotones.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            PanelBotones.Controls.Add(BtnMañanaEs);
            PanelBotones.Controls.Add(BtnHoyEs);
            PanelBotones.Controls.Add(BtnAlerta1);
            PanelBotones.Controls.Add(BtnAlerta2);
            PanelBotones.Controls.Add(BtnAlerta3);
            PanelBotones.Location = new Point(6, 223);
            PanelBotones.Name = "PanelBotones";
            PanelBotones.Padding = new Padding(3);
            PanelBotones.Size = new Size(1298, 106);
            PanelBotones.TabIndex = 2;
            // 
            // BtnMañanaEs
            // 
            BtnMañanaEs.BackColor = Color.LightYellow;
            BtnMañanaEs.Location = new Point(6, 6);
            BtnMañanaEs.Name = "BtnMañanaEs";
            BtnMañanaEs.Size = new Size(586, 40);
            BtnMañanaEs.TabIndex = 0;
            BtnMañanaEs.Text = "Enviar MAÑANA es el día";
            BtnMañanaEs.UseVisualStyleBackColor = false;
            BtnMañanaEs.Click += BtnMañanaEs_Click;
            // 
            // BtnHoyEs
            // 
            BtnHoyEs.BackColor = Color.Gold;
            BtnHoyEs.Location = new Point(598, 6);
            BtnHoyEs.Name = "BtnHoyEs";
            BtnHoyEs.Size = new Size(691, 40);
            BtnHoyEs.TabIndex = 1;
            BtnHoyEs.Text = "Enviar HOY es el día";
            BtnHoyEs.UseVisualStyleBackColor = false;
            BtnHoyEs.Click += BtnHoyEs_Click;
            // 
            // BtnAlerta1
            // 
            BtnAlerta1.BackColor = Color.PaleGreen;
            BtnAlerta1.Location = new Point(6, 55);
            BtnAlerta1.Margin = new Padding(3, 6, 3, 3);
            BtnAlerta1.Name = "BtnAlerta1";
            BtnAlerta1.Size = new Size(361, 40);
            BtnAlerta1.TabIndex = 2;
            BtnAlerta1.Text = "Enviar Alerta 1";
            BtnAlerta1.UseVisualStyleBackColor = false;
            BtnAlerta1.Click += BtnAlerta1_Click;
            // 
            // BtnAlerta2
            // 
            BtnAlerta2.BackColor = Color.Orange;
            BtnAlerta2.Location = new Point(373, 55);
            BtnAlerta2.Margin = new Padding(3, 6, 3, 3);
            BtnAlerta2.Name = "BtnAlerta2";
            BtnAlerta2.Size = new Size(434, 40);
            BtnAlerta2.TabIndex = 3;
            BtnAlerta2.Text = "Enviar Alerta 2";
            BtnAlerta2.UseVisualStyleBackColor = false;
            BtnAlerta2.Click += BtnAlerta2_Click;
            // 
            // BtnAlerta3
            // 
            BtnAlerta3.BackColor = Color.Salmon;
            BtnAlerta3.Location = new Point(813, 55);
            BtnAlerta3.Margin = new Padding(3, 6, 3, 3);
            BtnAlerta3.Name = "BtnAlerta3";
            BtnAlerta3.Size = new Size(476, 40);
            BtnAlerta3.TabIndex = 4;
            BtnAlerta3.Text = "Enviar Alerta 3";
            BtnAlerta3.UseVisualStyleBackColor = false;
            BtnAlerta3.Click += BtnAlerta3_Click;
            // 
            // LabelInfoListView
            // 
            LabelInfoListView.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            LabelInfoListView.Location = new Point(6, 571);
            LabelInfoListView.Margin = new Padding(3);
            LabelInfoListView.Name = "LabelInfoListView";
            LabelInfoListView.Size = new Size(996, 31);
            LabelInfoListView.TabIndex = 10;
            LabelInfoListView.Text = "Hay n elementos";
            LabelInfoListView.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // BtnReservasSinSalida
            // 
            BtnReservasSinSalida.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            BtnReservasSinSalida.BackColor = Color.PeachPuff;
            BtnReservasSinSalida.Location = new Point(6, 135);
            BtnReservasSinSalida.Margin = new Padding(3, 12, 3, 3);
            BtnReservasSinSalida.Name = "BtnReservasSinSalida";
            BtnReservasSinSalida.Size = new Size(650, 40);
            BtnReservasSinSalida.TabIndex = 7;
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
            LvwSinEmail.Location = new Point(9, 184);
            LvwSinEmail.Margin = new Padding(3, 6, 3, 3);
            LvwSinEmail.Name = "LvwSinEmail";
            LvwSinEmail.Size = new Size(1301, 381);
            LvwSinEmail.TabIndex = 9;
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
            ContextMenuListView.Items.AddRange(new ToolStripItem[] { MnuCopiarBooking, MnuCopiarNombre, MnuCopiarTelefono, MnuCopiarReserva, MnuCopiarPax, MnuCopiarEmail, MnuCopiarNotas, MnuSep1, MnuCopiarTodo, MnuCopiarTodoConCr });
            ContextMenuListView.Name = "ContextMenuListView";
            ContextMenuListView.Size = new Size(253, 298);
            ContextMenuListView.Opening += ContextMenuListView_Opening;
            // 
            // MnuCopiarBooking
            // 
            MnuCopiarBooking.Name = "MnuCopiarBooking";
            MnuCopiarBooking.Size = new Size(252, 32);
            MnuCopiarBooking.Text = "Copiar Booking";
            MnuCopiarBooking.Click += MnuCopiarDeLvw_Click;
            // 
            // MnuCopiarNombre
            // 
            MnuCopiarNombre.Name = "MnuCopiarNombre";
            MnuCopiarNombre.Size = new Size(252, 32);
            MnuCopiarNombre.Text = "Copiar Nombre";
            MnuCopiarNombre.Click += MnuCopiarDeLvw_Click;
            // 
            // MnuCopiarTelefono
            // 
            MnuCopiarTelefono.Name = "MnuCopiarTelefono";
            MnuCopiarTelefono.Size = new Size(252, 32);
            MnuCopiarTelefono.Text = "Copiar Teléfono";
            MnuCopiarTelefono.Click += MnuCopiarDeLvw_Click;
            // 
            // MnuCopiarReserva
            // 
            MnuCopiarReserva.Name = "MnuCopiarReserva";
            MnuCopiarReserva.Size = new Size(252, 32);
            MnuCopiarReserva.Text = "Copiar Reserva";
            MnuCopiarReserva.Click += MnuCopiarDeLvw_Click;
            // 
            // MnuCopiarPax
            // 
            MnuCopiarPax.Name = "MnuCopiarPax";
            MnuCopiarPax.Size = new Size(252, 32);
            MnuCopiarPax.Text = "Copiar Pax";
            MnuCopiarPax.Click += MnuCopiarDeLvw_Click;
            // 
            // MnuCopiarEmail
            // 
            MnuCopiarEmail.Name = "MnuCopiarEmail";
            MnuCopiarEmail.Size = new Size(252, 32);
            MnuCopiarEmail.Text = "Copiar Email";
            MnuCopiarEmail.Click += MnuCopiarDeLvw_Click;
            // 
            // MnuCopiarNotas
            // 
            MnuCopiarNotas.Name = "MnuCopiarNotas";
            MnuCopiarNotas.Size = new Size(252, 32);
            MnuCopiarNotas.Text = "Copiar Notas";
            MnuCopiarNotas.Click += MnuCopiarDeLvw_Click;
            // 
            // MnuSep1
            // 
            MnuSep1.Name = "MnuSep1";
            MnuSep1.Size = new Size(249, 6);
            // 
            // MnuCopiarTodo
            // 
            MnuCopiarTodo.Name = "MnuCopiarTodo";
            MnuCopiarTodo.Size = new Size(252, 32);
            MnuCopiarTodo.Text = "Copiar Todo";
            MnuCopiarTodo.Click += MnuCopiarDeLvw_Click;
            // 
            // MnuCopiarTodoConCr
            // 
            MnuCopiarTodoConCr.Name = "MnuCopiarTodoConCr";
            MnuCopiarTodoConCr.Size = new Size(252, 32);
            MnuCopiarTodoConCr.Text = "Copiar Todo (con CR)";
            MnuCopiarTodoConCr.Click += MnuCopiarDeLvw_Click;
            // 
            // BtnMostrarReservas
            // 
            BtnMostrarReservas.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            BtnMostrarReservas.BackColor = Color.LightSkyBlue;
            BtnMostrarReservas.Location = new Point(662, 135);
            BtnMostrarReservas.Margin = new Padding(3, 12, 3, 3);
            BtnMostrarReservas.Name = "BtnMostrarReservas";
            BtnMostrarReservas.Size = new Size(645, 40);
            BtnMostrarReservas.TabIndex = 8;
            BtnMostrarReservas.Text = "Mostrar Reservas";
            BtnMostrarReservas.UseVisualStyleBackColor = false;
            BtnMostrarReservas.Click += BtnMostrarReservas_Click;
            // 
            // DateTimePickerGYG
            // 
            DateTimePickerGYG.CustomFormat = "dddd dd/MM/yyyy";
            DateTimePickerGYG.Format = DateTimePickerFormat.Custom;
            DateTimePickerGYG.Location = new Point(195, 30);
            DateTimePickerGYG.Name = "DateTimePickerGYG";
            DateTimePickerGYG.Size = new Size(250, 31);
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
            BtnComprobarSinMail.Size = new Size(650, 40);
            BtnComprobarSinMail.TabIndex = 5;
            BtnComprobarSinMail.Text = "Comprobar reservas sin email en la fecha";
            BtnComprobarSinMail.UseVisualStyleBackColor = false;
            BtnComprobarSinMail.Click += BtnComprobarSinMail_Click;
            // 
            // TimerInicioForm1
            // 
            TimerInicioForm1.Interval = 300;
            TimerInicioForm1.Tick += TimerInicioForm1_Tick;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1346, 973);
            Controls.Add(GrbOpcionesFecha);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Opciones Reservas GetYourGuide";
            FormClosing += Form1_FormClosing;
            Load += Form1_Load;
            Resize += Form1_Resize;
            GrbOpcionesFecha.ResumeLayout(false);
            GrbOpcionesFecha.PerformLayout();
            GrbAvisos.ResumeLayout(false);
            GrbAvisos.PerformLayout();
            ContextMenuTextoAviso.ResumeLayout(false);
            PanelBotones.ResumeLayout(false);
            ContextMenuListView.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private ToolTip toolTip1;
        private Button BtnFotos;
        private GroupBox GrbOpcionesFecha;
        private DateTimePicker DateTimePickerGYG;
        private Label label8;
        private Button BtnComprobarSinMail;
        private Button BtnHoyEs;
        private Button BtnMostrarReservas;
        private Button BtnMañanaEs;
        private ListView LvwSinEmail;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private ContextMenuStrip ContextMenuListView;
        private ToolStripMenuItem MnuCopiarNombre;
        private ToolStripMenuItem MnuCopiarNotas;
        private Button BtnReservasSinSalida;
        private ToolStripMenuItem MnuCopiarBooking;
        private ToolStripMenuItem MnuCopiarTelefono;
        private Button BtnAlerta3;
        private Button BtnAlerta1;
        private Button BtnAlerta2;
        private ToolStripMenuItem MnuCopiarEmail;
        private CheckBox ChkConAlquileres;
        private CheckBox ChkConCanceladas;
        private CheckBox ChkSoloCanceladas;
        private Label LabelInfoListView;
        private ToolStripMenuItem MnuCopiarReserva;
        private ToolStripMenuItem MnuCopiarPax;
        private ToolStripSeparator MnuSep1;
        private ToolStripMenuItem MnuCopiarTodo;
        private ToolStripMenuItem MnuCopiarTodoConCr;
        private Panel PanelBotones;
        private GroupBox GrbAvisos;
        private TextBox TxtAvisoExtra;
        private CheckBox ChkIncluirTextoAviso;
        private Timer TimerInicioForm1;
        private ContextMenuStrip ContextMenuTextoAviso;
        private ToolStripMenuItem MenuPegarTextoOriginal;
        private ToolStripMenuItem MnuPegarÚltimoTextoEnviado;
        private Button BtnAnalizar;
    }
}