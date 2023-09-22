using System;
using System.Drawing;
using System.Windows.Forms;

namespace ReservasGYG
{
    partial class FormAnalizaEmail
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAnalizaEmail));
            GrbEmail = new GroupBox();
            BtnLimpiarTexto = new Button();
            BtnAnalizarEmail = new Button();
            BtnPegarEmail = new Button();
            RtfEmail = new RichTextBox();
            GrbReserva = new GroupBox();
            LabelAvisoCambiarFecha = new Label();
            TxtTipo = new TextBox();
            label16 = new Label();
            label15 = new Label();
            BtnCrearConEmail = new Button();
            TxtPais = new TextBox();
            label14 = new Label();
            TxtID = new TextBox();
            label13 = new Label();
            TxtGYG = new TextBox();
            ChkCrearConEmail = new CheckBox();
            TxtPrice = new TextBox();
            label12 = new Label();
            TxtMenoresG = new TextBox();
            label11 = new Label();
            TxtMenores = new TextBox();
            label10 = new Label();
            TxtAdultos = new TextBox();
            label9 = new Label();
            TxtLanguage = new TextBox();
            label8 = new Label();
            TxtNotas = new TextBox();
            label7 = new Label();
            TxtFechaHora = new TextBox();
            label6 = new Label();
            TxtActividad = new TextBox();
            label5 = new Label();
            TxtEmail = new TextBox();
            label4 = new Label();
            TxtTelefono = new TextBox();
            label3 = new Label();
            TxtNombre = new TextBox();
            label2 = new Label();
            TxtReference = new TextBox();
            label1 = new Label();
            BtnLimpiarReserva = new Button();
            BtnOpciones = new Button();
            statusStrip1 = new StatusStrip();
            LabelStatus = new ToolStripStatusLabel();
            LabelVersion = new ToolStripStatusLabel();
            LabelFechaHora = new ToolStripStatusLabel();
            TimerHoraStatus = new Timer(components);
            ToolTip1 = new ToolTip(components);
            ChkIncluirTextoAviso = new CheckBox();
            TxtAvisoExtra = new TextBox();
            TimerInicio = new Timer(components);
            GrbEmail.SuspendLayout();
            GrbReserva.SuspendLayout();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // GrbEmail
            // 
            GrbEmail.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            GrbEmail.Controls.Add(BtnLimpiarTexto);
            GrbEmail.Controls.Add(BtnAnalizarEmail);
            GrbEmail.Controls.Add(BtnPegarEmail);
            GrbEmail.Controls.Add(RtfEmail);
            GrbEmail.Location = new Point(12, 12);
            GrbEmail.Name = "GrbEmail";
            GrbEmail.Size = new Size(1462, 384);
            GrbEmail.TabIndex = 0;
            GrbEmail.TabStop = false;
            GrbEmail.Text = "Texto del correo a analizar";
            // 
            // BtnLimpiarTexto
            // 
            BtnLimpiarTexto.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            BtnLimpiarTexto.BackColor = Color.LightGoldenrodYellow;
            BtnLimpiarTexto.ImageAlign = ContentAlignment.TopLeft;
            BtnLimpiarTexto.Location = new Point(1251, 120);
            BtnLimpiarTexto.Name = "BtnLimpiarTexto";
            BtnLimpiarTexto.Size = new Size(205, 70);
            BtnLimpiarTexto.TabIndex = 2;
            BtnLimpiarTexto.Text = "Limpiar texto";
            BtnLimpiarTexto.UseVisualStyleBackColor = false;
            BtnLimpiarTexto.Click += BtnLimpiarTexto_Click;
            // 
            // BtnAnalizarEmail
            // 
            BtnAnalizarEmail.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            BtnAnalizarEmail.BackColor = Color.Azure;
            BtnAnalizarEmail.Location = new Point(1251, 308);
            BtnAnalizarEmail.Name = "BtnAnalizarEmail";
            BtnAnalizarEmail.Size = new Size(205, 70);
            BtnAnalizarEmail.TabIndex = 3;
            BtnAnalizarEmail.Text = "Analizar email";
            BtnAnalizarEmail.UseVisualStyleBackColor = false;
            BtnAnalizarEmail.Click += BtnAnalizarEmail_Click;
            // 
            // BtnPegarEmail
            // 
            BtnPegarEmail.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            BtnPegarEmail.BackColor = Color.Gold;
            BtnPegarEmail.ImageAlign = ContentAlignment.TopLeft;
            BtnPegarEmail.Location = new Point(1251, 30);
            BtnPegarEmail.Name = "BtnPegarEmail";
            BtnPegarEmail.Size = new Size(205, 70);
            BtnPegarEmail.TabIndex = 1;
            BtnPegarEmail.Text = "Pegar texto";
            BtnPegarEmail.UseVisualStyleBackColor = false;
            BtnPegarEmail.Click += BtnPegarEmail_Click;
            // 
            // RtfEmail
            // 
            RtfEmail.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            RtfEmail.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point);
            RtfEmail.Location = new Point(6, 30);
            RtfEmail.Name = "RtfEmail";
            RtfEmail.Size = new Size(1215, 348);
            RtfEmail.TabIndex = 0;
            RtfEmail.Text = resources.GetString("RtfEmail.Text");
            RtfEmail.WordWrap = false;
            RtfEmail.TextChanged += RtfEmail_TextChanged;
            // 
            // GrbReserva
            // 
            GrbReserva.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            GrbReserva.Controls.Add(LabelAvisoCambiarFecha);
            GrbReserva.Controls.Add(TxtTipo);
            GrbReserva.Controls.Add(label16);
            GrbReserva.Controls.Add(label15);
            GrbReserva.Controls.Add(BtnCrearConEmail);
            GrbReserva.Controls.Add(TxtPais);
            GrbReserva.Controls.Add(label14);
            GrbReserva.Controls.Add(TxtID);
            GrbReserva.Controls.Add(label13);
            GrbReserva.Controls.Add(TxtGYG);
            GrbReserva.Controls.Add(ChkCrearConEmail);
            GrbReserva.Controls.Add(TxtPrice);
            GrbReserva.Controls.Add(label12);
            GrbReserva.Controls.Add(TxtMenoresG);
            GrbReserva.Controls.Add(label11);
            GrbReserva.Controls.Add(TxtMenores);
            GrbReserva.Controls.Add(label10);
            GrbReserva.Controls.Add(TxtAdultos);
            GrbReserva.Controls.Add(label9);
            GrbReserva.Controls.Add(TxtLanguage);
            GrbReserva.Controls.Add(label8);
            GrbReserva.Controls.Add(TxtNotas);
            GrbReserva.Controls.Add(label7);
            GrbReserva.Controls.Add(TxtFechaHora);
            GrbReserva.Controls.Add(label6);
            GrbReserva.Controls.Add(TxtActividad);
            GrbReserva.Controls.Add(label5);
            GrbReserva.Controls.Add(TxtEmail);
            GrbReserva.Controls.Add(label4);
            GrbReserva.Controls.Add(TxtTelefono);
            GrbReserva.Controls.Add(label3);
            GrbReserva.Controls.Add(TxtNombre);
            GrbReserva.Controls.Add(label2);
            GrbReserva.Controls.Add(TxtReference);
            GrbReserva.Controls.Add(label1);
            GrbReserva.Controls.Add(BtnLimpiarReserva);
            GrbReserva.Location = new Point(12, 402);
            GrbReserva.Name = "GrbReserva";
            GrbReserva.Size = new Size(1462, 483);
            GrbReserva.TabIndex = 1;
            GrbReserva.TabStop = false;
            GrbReserva.Text = "Datos de la reserva";
            // 
            // LabelAvisoCambiarFecha
            // 
            LabelAvisoCambiarFecha.BackColor = Color.Firebrick;
            LabelAvisoCambiarFecha.ForeColor = Color.Yellow;
            LabelAvisoCambiarFecha.Location = new Point(1137, 150);
            LabelAvisoCambiarFecha.Margin = new Padding(6, 3, 3, 3);
            LabelAvisoCambiarFecha.Name = "LabelAvisoCambiarFecha";
            LabelAvisoCambiarFecha.Size = new Size(201, 214);
            LabelAvisoCambiarFecha.TabIndex = 34;
            LabelAvisoCambiarFecha.Text = "Has cambiado la fecha, comprueba que es correcta:";
            LabelAvisoCambiarFecha.Visible = false;
            // 
            // TxtTipo
            // 
            TxtTipo.BackColor = Color.MintCream;
            TxtTipo.Location = new Point(502, 333);
            TxtTipo.Name = "TxtTipo";
            TxtTipo.Size = new Size(200, 31);
            TxtTipo.TabIndex = 27;
            // 
            // label16
            // 
            label16.Location = new Point(316, 333);
            label16.Margin = new Padding(6, 3, 3, 3);
            label16.Name = "label16";
            label16.Size = new Size(180, 31);
            label16.TabIndex = 26;
            label16.Text = "Tipo reserva:";
            // 
            // label15
            // 
            label15.Location = new Point(21, 376);
            label15.Margin = new Padding(3);
            label15.Name = "label15";
            label15.Size = new Size(180, 31);
            label15.TabIndex = 28;
            label15.Text = "Otra info:";
            // 
            // BtnCrearConEmail
            // 
            BtnCrearConEmail.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            BtnCrearConEmail.BackColor = Color.MistyRose;
            BtnCrearConEmail.Location = new Point(1251, 65);
            BtnCrearConEmail.Margin = new Padding(12, 3, 3, 12);
            BtnCrearConEmail.Name = "BtnCrearConEmail";
            BtnCrearConEmail.Size = new Size(205, 70);
            BtnCrearConEmail.TabIndex = 33;
            BtnCrearConEmail.Text = "Crear reserva y enviar email de confirmación";
            BtnCrearConEmail.UseVisualStyleBackColor = false;
            BtnCrearConEmail.Click += BtnCrearConEmail_Click;
            // 
            // TxtPais
            // 
            TxtPais.Location = new Point(502, 259);
            TxtPais.Name = "TxtPais";
            TxtPais.Size = new Size(335, 31);
            TxtPais.TabIndex = 23;
            TxtPais.TextChanged += TxtPais_TextChanged;
            // 
            // label14
            // 
            label14.Location = new Point(316, 259);
            label14.Margin = new Padding(6, 3, 3, 3);
            label14.Name = "label14";
            label14.Size = new Size(180, 31);
            label14.TabIndex = 22;
            label14.Text = "País:";
            ToolTip1.SetToolTip(label14, "Puedes modificar el país");
            // 
            // TxtID
            // 
            TxtID.BackColor = Color.MintCream;
            TxtID.Location = new Point(978, 373);
            TxtID.Name = "TxtID";
            TxtID.Size = new Size(100, 31);
            TxtID.TabIndex = 31;
            TxtID.Text = "0";
            // 
            // label13
            // 
            label13.Location = new Point(792, 376);
            label13.Margin = new Padding(6, 3, 3, 3);
            label13.Name = "label13";
            label13.Size = new Size(180, 31);
            label13.TabIndex = 30;
            label13.Text = "ID:";
            // 
            // TxtGYG
            // 
            TxtGYG.BackColor = Color.MintCream;
            TxtGYG.Location = new Point(207, 373);
            TxtGYG.Multiline = true;
            TxtGYG.Name = "TxtGYG";
            TxtGYG.ScrollBars = ScrollBars.Both;
            TxtGYG.Size = new Size(530, 90);
            TxtGYG.TabIndex = 29;
            TxtGYG.Text = "Línea 1\r\nLínea 2\r\nLínea 3";
            // 
            // ChkCrearConEmail
            // 
            ChkCrearConEmail.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            ChkCrearConEmail.AutoSize = true;
            ChkCrearConEmail.Location = new Point(1253, 30);
            ChkCrearConEmail.Name = "ChkCrearConEmail";
            ChkCrearConEmail.Size = new Size(201, 29);
            ChkCrearConEmail.TabIndex = 32;
            ChkCrearConEmail.Text = "Habilitar crear+email";
            ChkCrearConEmail.UseVisualStyleBackColor = true;
            ChkCrearConEmail.CheckedChanged += ChkCrearReserva_CheckedChanged;
            // 
            // TxtPrice
            // 
            TxtPrice.BackColor = Color.MintCream;
            TxtPrice.Location = new Point(502, 296);
            TxtPrice.Name = "TxtPrice";
            TxtPrice.Size = new Size(200, 31);
            TxtPrice.TabIndex = 25;
            // 
            // label12
            // 
            label12.Location = new Point(316, 296);
            label12.Margin = new Padding(6, 3, 3, 3);
            label12.Name = "label12";
            label12.Size = new Size(180, 31);
            label12.TabIndex = 24;
            label12.Text = "Price:";
            // 
            // TxtMenoresG
            // 
            TxtMenoresG.BackColor = Color.MintCream;
            TxtMenoresG.Location = new Point(207, 333);
            TxtMenoresG.Name = "TxtMenoresG";
            TxtMenoresG.Size = new Size(100, 31);
            TxtMenoresG.TabIndex = 21;
            // 
            // label11
            // 
            label11.Location = new Point(21, 336);
            label11.Margin = new Padding(3);
            label11.Name = "label11";
            label11.Size = new Size(180, 31);
            label11.TabIndex = 20;
            label11.Text = "Menores (4 a 6):";
            // 
            // TxtMenores
            // 
            TxtMenores.BackColor = Color.MintCream;
            TxtMenores.Location = new Point(207, 296);
            TxtMenores.Name = "TxtMenores";
            TxtMenores.Size = new Size(100, 31);
            TxtMenores.TabIndex = 19;
            // 
            // label10
            // 
            label10.Location = new Point(21, 299);
            label10.Margin = new Padding(3);
            label10.Name = "label10";
            label10.Size = new Size(180, 31);
            label10.TabIndex = 18;
            label10.Text = "Menores (7 a 15):";
            // 
            // TxtAdultos
            // 
            TxtAdultos.BackColor = Color.MintCream;
            TxtAdultos.Location = new Point(207, 259);
            TxtAdultos.Name = "TxtAdultos";
            TxtAdultos.Size = new Size(100, 31);
            TxtAdultos.TabIndex = 17;
            TxtAdultos.Text = "99";
            // 
            // label9
            // 
            label9.Location = new Point(21, 262);
            label9.Margin = new Padding(3);
            label9.Name = "label9";
            label9.Size = new Size(180, 31);
            label9.TabIndex = 16;
            label9.Text = "Adultos (16+):";
            // 
            // TxtLanguage
            // 
            TxtLanguage.Location = new Point(743, 41);
            TxtLanguage.Name = "TxtLanguage";
            TxtLanguage.Size = new Size(335, 31);
            TxtLanguage.TabIndex = 3;
            TxtLanguage.TextChanged += TxtLanguage_TextChanged;
            // 
            // label8
            // 
            label8.Location = new Point(557, 44);
            label8.Margin = new Padding(6, 3, 3, 3);
            label8.Name = "label8";
            label8.Size = new Size(180, 31);
            label8.TabIndex = 2;
            label8.Text = "Tour language:";
            ToolTip1.SetToolTip(label8, "Indica English para mandar el mensaje en inglés");
            // 
            // TxtNotas
            // 
            TxtNotas.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point);
            TxtNotas.Location = new Point(207, 189);
            TxtNotas.Multiline = true;
            TxtNotas.Name = "TxtNotas";
            TxtNotas.ScrollBars = ScrollBars.Both;
            TxtNotas.Size = new Size(871, 64);
            TxtNotas.TabIndex = 15;
            TxtNotas.Text = "Las notas del wasap y edades\r\nSegunda línea";
            TxtNotas.TextChanged += TxtNotas_TextChanged;
            // 
            // label7
            // 
            label7.Location = new Point(21, 192);
            label7.Margin = new Padding(3);
            label7.Name = "label7";
            label7.Size = new Size(180, 61);
            label7.TabIndex = 14;
            label7.Text = "Notas:\r\n(Wasap y edades)";
            ToolTip1.SetToolTip(label7, "Puedes modificar las notas");
            // 
            // TxtFechaHora
            // 
            TxtFechaHora.Location = new Point(743, 78);
            TxtFechaHora.Name = "TxtFechaHora";
            TxtFechaHora.Size = new Size(335, 31);
            TxtFechaHora.TabIndex = 7;
            TxtFechaHora.TextChanged += TxtFechaHora_TextChanged;
            TxtFechaHora.KeyUp += TxtFechaHora_KeyUp;
            TxtFechaHora.Leave += TxtFechaHora_Leave;
            // 
            // label6
            // 
            label6.Location = new Point(557, 81);
            label6.Margin = new Padding(12, 3, 3, 3);
            label6.Name = "label6";
            label6.Size = new Size(180, 31);
            label6.TabIndex = 6;
            label6.Text = "Fecha/hora:";
            // 
            // TxtActividad
            // 
            TxtActividad.BackColor = Color.MintCream;
            TxtActividad.Location = new Point(207, 78);
            TxtActividad.Name = "TxtActividad";
            TxtActividad.Size = new Size(335, 31);
            TxtActividad.TabIndex = 5;
            // 
            // label5
            // 
            label5.Location = new Point(21, 81);
            label5.Margin = new Padding(3);
            label5.Name = "label5";
            label5.Size = new Size(180, 31);
            label5.TabIndex = 4;
            label5.Text = "Actividad:";
            // 
            // TxtEmail
            // 
            TxtEmail.Location = new Point(207, 152);
            TxtEmail.Name = "TxtEmail";
            TxtEmail.Size = new Size(871, 31);
            TxtEmail.TabIndex = 13;
            TxtEmail.TextChanged += TxtEmail_TextChanged;
            // 
            // label4
            // 
            label4.Location = new Point(21, 155);
            label4.Margin = new Padding(3);
            label4.Name = "label4";
            label4.Size = new Size(180, 31);
            label4.TabIndex = 12;
            label4.Text = "Email:";
            ToolTip1.SetToolTip(label4, "Puedes modificar el email");
            // 
            // TxtTelefono
            // 
            TxtTelefono.Location = new Point(743, 115);
            TxtTelefono.Name = "TxtTelefono";
            TxtTelefono.Size = new Size(335, 31);
            TxtTelefono.TabIndex = 11;
            TxtTelefono.TextChanged += TxtTelefono_TextChanged;
            // 
            // label3
            // 
            label3.Location = new Point(557, 118);
            label3.Margin = new Padding(3);
            label3.Name = "label3";
            label3.Size = new Size(180, 31);
            label3.TabIndex = 10;
            label3.Text = "Teléfono:";
            ToolTip1.SetToolTip(label3, "Puedes modificar el teléfono");
            // 
            // TxtNombre
            // 
            TxtNombre.BackColor = Color.MintCream;
            TxtNombre.Location = new Point(207, 115);
            TxtNombre.Name = "TxtNombre";
            TxtNombre.Size = new Size(335, 31);
            TxtNombre.TabIndex = 9;
            // 
            // label2
            // 
            label2.Location = new Point(21, 118);
            label2.Margin = new Padding(3);
            label2.Name = "label2";
            label2.Size = new Size(180, 31);
            label2.TabIndex = 8;
            label2.Text = "Nombre:";
            // 
            // TxtReference
            // 
            TxtReference.BackColor = Color.MintCream;
            TxtReference.Location = new Point(207, 41);
            TxtReference.Name = "TxtReference";
            TxtReference.Size = new Size(335, 31);
            TxtReference.TabIndex = 1;
            // 
            // label1
            // 
            label1.Location = new Point(21, 44);
            label1.Margin = new Padding(3);
            label1.Name = "label1";
            label1.Size = new Size(180, 31);
            label1.TabIndex = 0;
            label1.Text = "Reference number:";
            // 
            // BtnLimpiarReserva
            // 
            BtnLimpiarReserva.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            BtnLimpiarReserva.BackColor = Color.LightYellow;
            BtnLimpiarReserva.Location = new Point(1251, 398);
            BtnLimpiarReserva.Margin = new Padding(12, 3, 3, 3);
            BtnLimpiarReserva.Name = "BtnLimpiarReserva";
            BtnLimpiarReserva.Size = new Size(205, 70);
            BtnLimpiarReserva.TabIndex = 35;
            BtnLimpiarReserva.Text = "Limpiar campos";
            BtnLimpiarReserva.UseVisualStyleBackColor = false;
            BtnLimpiarReserva.Click += BtnLimpiarReserva_Click;
            // 
            // BtnOpciones
            // 
            BtnOpciones.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            BtnOpciones.BackColor = SystemColors.Highlight;
            BtnOpciones.ForeColor = SystemColors.Window;
            BtnOpciones.Location = new Point(12, 1105);
            BtnOpciones.Margin = new Padding(3, 3, 3, 12);
            BtnOpciones.Name = "BtnOpciones";
            BtnOpciones.Size = new Size(361, 54);
            BtnOpciones.TabIndex = 4;
            BtnOpciones.Text = "Mostrar ventana de otras opciones";
            BtnOpciones.UseVisualStyleBackColor = false;
            BtnOpciones.Click += BtnOpciones_Click;
            // 
            // statusStrip1
            // 
            statusStrip1.ImageScalingSize = new Size(24, 24);
            statusStrip1.Items.AddRange(new ToolStripItem[] { LabelStatus, LabelVersion, LabelFechaHora });
            statusStrip1.Location = new Point(0, 1167);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.ShowItemToolTips = true;
            statusStrip1.Size = new Size(1486, 36);
            statusStrip1.TabIndex = 5;
            statusStrip1.Text = "statusStrip1";
            // 
            // LabelStatus
            // 
            LabelStatus.BorderSides = ToolStripStatusLabelBorderSides.Right;
            LabelStatus.Name = "LabelStatus";
            LabelStatus.Size = new Size(1025, 29);
            LabelStatus.Spring = true;
            LabelStatus.Text = "Crear reservas de GetYourGuide en la app de MKN Reservas y enviar email de confirmación";
            LabelStatus.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // LabelVersion
            // 
            LabelVersion.AutoSize = false;
            LabelVersion.BorderSides = ToolStripStatusLabelBorderSides.Right;
            LabelVersion.Name = "LabelVersion";
            LabelVersion.Size = new Size(200, 29);
            LabelVersion.Text = "v1.0.18.1 (11-sep-2023)";
            // 
            // LabelFechaHora
            // 
            LabelFechaHora.AutoSize = false;
            LabelFechaHora.Name = "LabelFechaHora";
            LabelFechaHora.Size = new Size(200, 29);
            LabelFechaHora.Text = "dd/MM/yyyy HH:mm:ss";
            // 
            // TimerHoraStatus
            // 
            TimerHoraStatus.Interval = 900;
            TimerHoraStatus.Tick += TimerHoraStatus_Tick;
            // 
            // ChkIncluirTextoAviso
            // 
            ChkIncluirTextoAviso.Location = new Point(12, 891);
            ChkIncluirTextoAviso.Name = "ChkIncluirTextoAviso";
            ChkIncluirTextoAviso.Size = new Size(1346, 29);
            ChkIncluirTextoAviso.TabIndex = 2;
            ChkIncluirTextoAviso.Text = "Incluir este texto en enviar el mensaje de confirmación";
            ChkIncluirTextoAviso.UseVisualStyleBackColor = true;
            // 
            // TxtAvisoExtra
            // 
            TxtAvisoExtra.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            TxtAvisoExtra.Location = new Point(12, 926);
            TxtAvisoExtra.Multiline = true;
            TxtAvisoExtra.Name = "TxtAvisoExtra";
            TxtAvisoExtra.ScrollBars = ScrollBars.Both;
            TxtAvisoExtra.Size = new Size(1462, 154);
            TxtAvisoExtra.TabIndex = 3;
            TxtAvisoExtra.Text = resources.GetString("TxtAvisoExtra.Text");
            // 
            // TimerInicio
            // 
            TimerInicio.Interval = 300;
            TimerInicio.Tick += TimerInicio_Tick;
            // 
            // FormAnalizaEmail
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1486, 1203);
            Controls.Add(ChkIncluirTextoAviso);
            Controls.Add(TxtAvisoExtra);
            Controls.Add(statusStrip1);
            Controls.Add(BtnOpciones);
            Controls.Add(GrbReserva);
            Controls.Add(GrbEmail);
            Icon = (Icon)resources.GetObject("$this.Icon");
            KeyPreview = true;
            Name = "FormAnalizaEmail";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Analizar Email de GYG y crear reserva con confirmación";
            Load += FormAnalizaEmailGYG_Load;
            Resize += FormAnalizaEmail_Resize;
            GrbEmail.ResumeLayout(false);
            GrbReserva.ResumeLayout(false);
            GrbReserva.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private GroupBox GrbEmail;
        private RichTextBox RtfEmail;
        private Button BtnPegarEmail;
        private GroupBox GrbReserva;
        private Button BtnAnalizarEmail;
        private Button BtnLimpiarReserva;
        private TextBox TxtEmail;
        private Label label4;
        private TextBox TxtTelefono;
        private Label label3;
        private TextBox TxtNombre;
        private Label label2;
        private TextBox TxtReference;
        private Label label1;
        private TextBox TxtFechaHora;
        private Label label6;
        private TextBox TxtActividad;
        private Label label5;
        private TextBox TxtNotas;
        private Label label7;
        private TextBox TxtLanguage;
        private Label label8;
        private TextBox TxtAdultos;
        private Label label9;
        private TextBox TxtMenoresG;
        private Label label11;
        private TextBox TxtMenores;
        private Label label10;
        private TextBox TxtPrice;
        private Label label12;
        private CheckBox ChkCrearConEmail;
        private TextBox TxtGYG;
        private TextBox TxtID;
        private Label label13;
        private Button BtnLimpiarTexto;
        private TextBox TxtPais;
        private Label label14;
        private Button BtnCrearConEmail;
        private Button BtnOpciones;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel LabelStatus;
        private ToolStripStatusLabel LabelFechaHora;
        private Label label15;
        private Timer TimerHoraStatus;
        private TextBox TxtTipo;
        private Label label16;
        private ToolStripStatusLabel LabelVersion;
        private ToolTip ToolTip1;
        private Label LabelAvisoCambiarFecha;
        private CheckBox ChkIncluirTextoAviso;
        private TextBox TxtAvisoExtra;
        private Timer TimerInicio;
    }
}