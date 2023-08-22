﻿using System;
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
            label15 = new Label();
            BtnCrearConEmail = new Button();
            TxtPais = new TextBox();
            label14 = new Label();
            TxtID = new TextBox();
            label13 = new Label();
            TxtGYG = new TextBox();
            ChkEnviarConfirm = new CheckBox();
            ChkCrearConEmail = new CheckBox();
            BtnEnviarConfirm = new Button();
            TxtPrice = new TextBox();
            label12 = new Label();
            BtnCrearReserva = new Button();
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
            LabelFechaHora = new ToolStripStatusLabel();
            timer1 = new Timer(components);
            timerCrearReserva = new Timer(components);
            GrbEmail.SuspendLayout();
            GrbReserva.SuspendLayout();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // GrbEmail
            // 
            GrbEmail.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            GrbEmail.Controls.Add(BtnLimpiarTexto);
            GrbEmail.Controls.Add(BtnAnalizarEmail);
            GrbEmail.Controls.Add(BtnPegarEmail);
            GrbEmail.Controls.Add(RtfEmail);
            GrbEmail.Location = new Point(12, 12);
            GrbEmail.Name = "GrbEmail";
            GrbEmail.Size = new Size(1346, 510);
            GrbEmail.TabIndex = 0;
            GrbEmail.TabStop = false;
            GrbEmail.Text = "Correo a procesar";
            // 
            // BtnLimpiarTexto
            // 
            BtnLimpiarTexto.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            BtnLimpiarTexto.BackColor = Color.LightGoldenrodYellow;
            BtnLimpiarTexto.Location = new Point(1135, 182);
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
            BtnAnalizarEmail.Location = new Point(1135, 434);
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
            BtnPegarEmail.Location = new Point(1135, 30);
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
            RtfEmail.Size = new Size(1099, 474);
            RtfEmail.TabIndex = 0;
            RtfEmail.Text = resources.GetString("RtfEmail.Text");
            RtfEmail.WordWrap = false;
            RtfEmail.TextChanged += RtfEmail_TextChanged;
            // 
            // GrbReserva
            // 
            GrbReserva.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            GrbReserva.Controls.Add(label15);
            GrbReserva.Controls.Add(BtnCrearConEmail);
            GrbReserva.Controls.Add(TxtPais);
            GrbReserva.Controls.Add(label14);
            GrbReserva.Controls.Add(TxtID);
            GrbReserva.Controls.Add(label13);
            GrbReserva.Controls.Add(TxtGYG);
            GrbReserva.Controls.Add(ChkEnviarConfirm);
            GrbReserva.Controls.Add(ChkCrearConEmail);
            GrbReserva.Controls.Add(BtnEnviarConfirm);
            GrbReserva.Controls.Add(TxtPrice);
            GrbReserva.Controls.Add(label12);
            GrbReserva.Controls.Add(BtnCrearReserva);
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
            GrbReserva.Location = new Point(12, 528);
            GrbReserva.Name = "GrbReserva";
            GrbReserva.Size = new Size(1346, 519);
            GrbReserva.TabIndex = 1;
            GrbReserva.TabStop = false;
            GrbReserva.Text = "Datos de la reserva";
            // 
            // label15
            // 
            label15.Location = new Point(21, 417);
            label15.Margin = new Padding(3);
            label15.Name = "label15";
            label15.Size = new Size(180, 31);
            label15.TabIndex = 35;
            label15.Text = "Otra info:";
            // 
            // BtnCrearConEmail
            // 
            BtnCrearConEmail.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            BtnCrearConEmail.BackColor = Color.MistyRose;
            BtnCrearConEmail.Location = new Point(1135, 65);
            BtnCrearConEmail.Margin = new Padding(12, 3, 3, 12);
            BtnCrearConEmail.Name = "BtnCrearConEmail";
            BtnCrearConEmail.Size = new Size(205, 70);
            BtnCrearConEmail.TabIndex = 30;
            BtnCrearConEmail.Text = "Crear reserva y enviar email de confirmación";
            BtnCrearConEmail.UseVisualStyleBackColor = false;
            BtnCrearConEmail.Click += BtnCrearConEmail_Click;
            // 
            // TxtPais
            // 
            TxtPais.Location = new Point(743, 284);
            TxtPais.Name = "TxtPais";
            TxtPais.Size = new Size(335, 31);
            TxtPais.TabIndex = 19;
            // 
            // label14
            // 
            label14.Location = new Point(557, 284);
            label14.Margin = new Padding(3);
            label14.Name = "label14";
            label14.Size = new Size(180, 31);
            label14.TabIndex = 18;
            label14.Text = "País:";
            // 
            // TxtID
            // 
            TxtID.Location = new Point(743, 358);
            TxtID.Name = "TxtID";
            TxtID.Size = new Size(100, 31);
            TxtID.TabIndex = 28;
            TxtID.Text = "0";
            // 
            // label13
            // 
            label13.Location = new Point(557, 361);
            label13.Margin = new Padding(3);
            label13.Name = "label13";
            label13.Size = new Size(180, 31);
            label13.TabIndex = 27;
            label13.Text = "ID:";
            // 
            // TxtGYG
            // 
            TxtGYG.Location = new Point(207, 414);
            TxtGYG.Multiline = true;
            TxtGYG.Name = "TxtGYG";
            TxtGYG.Size = new Size(530, 90);
            TxtGYG.TabIndex = 26;
            // 
            // ChkEnviarConfirm
            // 
            ChkEnviarConfirm.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            ChkEnviarConfirm.AutoSize = true;
            ChkEnviarConfirm.Location = new Point(1135, 178);
            ChkEnviarConfirm.Name = "ChkEnviarConfirm";
            ChkEnviarConfirm.Size = new Size(171, 29);
            ChkEnviarConfirm.TabIndex = 31;
            ChkEnviarConfirm.Text = "Habilitar 2 pasos";
            ChkEnviarConfirm.UseVisualStyleBackColor = true;
            ChkEnviarConfirm.CheckedChanged += ChkEnviarConfirm_CheckedChanged;
            // 
            // ChkCrearConEmail
            // 
            ChkCrearConEmail.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            ChkCrearConEmail.AutoSize = true;
            ChkCrearConEmail.Location = new Point(1137, 30);
            ChkCrearConEmail.Name = "ChkCrearConEmail";
            ChkCrearConEmail.Size = new Size(201, 29);
            ChkCrearConEmail.TabIndex = 29;
            ChkCrearConEmail.Text = "Habilitar crear+email";
            ChkCrearConEmail.UseVisualStyleBackColor = true;
            ChkCrearConEmail.CheckedChanged += ChkCrearReserva_CheckedChanged;
            // 
            // BtnEnviarConfirm
            // 
            BtnEnviarConfirm.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            BtnEnviarConfirm.BackColor = Color.LightBlue;
            BtnEnviarConfirm.Location = new Point(1133, 292);
            BtnEnviarConfirm.Margin = new Padding(12, 3, 3, 12);
            BtnEnviarConfirm.Name = "BtnEnviarConfirm";
            BtnEnviarConfirm.Size = new Size(205, 89);
            BtnEnviarConfirm.TabIndex = 33;
            BtnEnviarConfirm.Text = "Enviar email confirmación";
            BtnEnviarConfirm.UseVisualStyleBackColor = false;
            BtnEnviarConfirm.Click += BtnEnviarConfirm_Click;
            // 
            // TxtPrice
            // 
            TxtPrice.Location = new Point(743, 321);
            TxtPrice.Name = "TxtPrice";
            TxtPrice.Size = new Size(200, 31);
            TxtPrice.TabIndex = 17;
            // 
            // label12
            // 
            label12.Location = new Point(557, 321);
            label12.Margin = new Padding(3);
            label12.Name = "label12";
            label12.Size = new Size(180, 31);
            label12.TabIndex = 16;
            label12.Text = "Price:";
            // 
            // BtnCrearReserva
            // 
            BtnCrearReserva.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            BtnCrearReserva.BackColor = Color.Honeydew;
            BtnCrearReserva.Location = new Point(1133, 213);
            BtnCrearReserva.Margin = new Padding(12, 3, 3, 6);
            BtnCrearReserva.Name = "BtnCrearReserva";
            BtnCrearReserva.Size = new Size(205, 70);
            BtnCrearReserva.TabIndex = 32;
            BtnCrearReserva.Text = "Crear reserva";
            BtnCrearReserva.UseVisualStyleBackColor = false;
            BtnCrearReserva.Click += BtnCrearReserva_Click;
            // 
            // TxtMenoresG
            // 
            TxtMenoresG.Location = new Point(207, 358);
            TxtMenoresG.Name = "TxtMenoresG";
            TxtMenoresG.Size = new Size(100, 31);
            TxtMenoresG.TabIndex = 25;
            // 
            // label11
            // 
            label11.Location = new Point(21, 361);
            label11.Margin = new Padding(3);
            label11.Name = "label11";
            label11.Size = new Size(180, 31);
            label11.TabIndex = 24;
            label11.Text = "Menores (4 a 6):";
            // 
            // TxtMenores
            // 
            TxtMenores.Location = new Point(207, 321);
            TxtMenores.Name = "TxtMenores";
            TxtMenores.Size = new Size(100, 31);
            TxtMenores.TabIndex = 23;
            // 
            // label10
            // 
            label10.Location = new Point(21, 324);
            label10.Margin = new Padding(3);
            label10.Name = "label10";
            label10.Size = new Size(180, 31);
            label10.TabIndex = 22;
            label10.Text = "Menores (7 a 15):";
            // 
            // TxtAdultos
            // 
            TxtAdultos.Location = new Point(207, 284);
            TxtAdultos.Name = "TxtAdultos";
            TxtAdultos.Size = new Size(100, 31);
            TxtAdultos.TabIndex = 21;
            TxtAdultos.Text = "99";
            // 
            // label9
            // 
            label9.Location = new Point(21, 287);
            label9.Margin = new Padding(3);
            label9.Name = "label9";
            label9.Size = new Size(180, 31);
            label9.TabIndex = 20;
            label9.Text = "Adultos (16+):";
            // 
            // TxtLanguage
            // 
            TxtLanguage.Location = new Point(743, 41);
            TxtLanguage.Name = "TxtLanguage";
            TxtLanguage.Size = new Size(335, 31);
            TxtLanguage.TabIndex = 3;
            // 
            // label8
            // 
            label8.Location = new Point(557, 44);
            label8.Margin = new Padding(6, 3, 3, 3);
            label8.Name = "label8";
            label8.Size = new Size(180, 31);
            label8.TabIndex = 2;
            label8.Text = "Tour language:";
            // 
            // TxtNotas
            // 
            TxtNotas.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point);
            TxtNotas.Location = new Point(207, 189);
            TxtNotas.Multiline = true;
            TxtNotas.Name = "TxtNotas";
            TxtNotas.Size = new Size(871, 89);
            TxtNotas.TabIndex = 15;
            TxtNotas.Text = "Las notas del wasap y edades";
            // 
            // label7
            // 
            label7.Location = new Point(21, 192);
            label7.Margin = new Padding(3);
            label7.Name = "label7";
            label7.Size = new Size(180, 86);
            label7.TabIndex = 14;
            label7.Text = "Notas:\r\n(Wasap y edades)";
            // 
            // TxtFechaHora
            // 
            TxtFechaHora.Location = new Point(743, 78);
            TxtFechaHora.Name = "TxtFechaHora";
            TxtFechaHora.Size = new Size(335, 31);
            TxtFechaHora.TabIndex = 7;
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
            // 
            // label4
            // 
            label4.Location = new Point(21, 155);
            label4.Margin = new Padding(3);
            label4.Name = "label4";
            label4.Size = new Size(180, 31);
            label4.TabIndex = 12;
            label4.Text = "Email:";
            // 
            // TxtTelefono
            // 
            TxtTelefono.Location = new Point(743, 115);
            TxtTelefono.Name = "TxtTelefono";
            TxtTelefono.Size = new Size(335, 31);
            TxtTelefono.TabIndex = 11;
            // 
            // label3
            // 
            label3.Location = new Point(557, 118);
            label3.Margin = new Padding(3);
            label3.Name = "label3";
            label3.Size = new Size(180, 31);
            label3.TabIndex = 10;
            label3.Text = "Teléfono:";
            // 
            // TxtNombre
            // 
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
            BtnLimpiarReserva.Location = new Point(1135, 434);
            BtnLimpiarReserva.Margin = new Padding(12, 3, 3, 3);
            BtnLimpiarReserva.Name = "BtnLimpiarReserva";
            BtnLimpiarReserva.Size = new Size(205, 70);
            BtnLimpiarReserva.TabIndex = 34;
            BtnLimpiarReserva.Text = "Limpiar campos";
            BtnLimpiarReserva.UseVisualStyleBackColor = false;
            BtnLimpiarReserva.Click += BtnLimpiarReserva_Click;
            // 
            // BtnOpciones
            // 
            BtnOpciones.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            BtnOpciones.Location = new Point(1058, 1053);
            BtnOpciones.Name = "BtnOpciones";
            BtnOpciones.Size = new Size(300, 40);
            BtnOpciones.TabIndex = 2;
            BtnOpciones.Text = "Mostrar ventana de otras opciones";
            BtnOpciones.UseVisualStyleBackColor = true;
            BtnOpciones.Click += BtnOpciones_Click;
            // 
            // statusStrip1
            // 
            statusStrip1.ImageScalingSize = new Size(24, 24);
            statusStrip1.Items.AddRange(new ToolStripItem[] { LabelStatus, LabelFechaHora });
            statusStrip1.Location = new Point(0, 1115);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(1370, 32);
            statusStrip1.TabIndex = 3;
            statusStrip1.Text = "statusStrip1";
            // 
            // LabelStatus
            // 
            LabelStatus.Name = "LabelStatus";
            LabelStatus.Size = new Size(1155, 25);
            LabelStatus.Spring = true;
            LabelStatus.Text = "Crear reservas de GetYourGuide en la app de MKN Reservas y enviar email de confirmación";
            LabelStatus.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // LabelFechaHora
            // 
            LabelFechaHora.AutoSize = false;
            LabelFechaHora.Name = "LabelFechaHora";
            LabelFechaHora.Size = new Size(200, 25);
            LabelFechaHora.Text = "dd/MM/yyyy HH:mm:ss";
            // 
            // timer1
            // 
            timer1.Interval = 900;
            timer1.Tick += timer1_Tick;
            // 
            // timerCrearReserva
            // 
            timerCrearReserva.Tick += timerCrearReserva_Tick;
            // 
            // FormAnalizaEmailGYG
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1370, 1147);
            Controls.Add(statusStrip1);
            Controls.Add(BtnOpciones);
            Controls.Add(GrbReserva);
            Controls.Add(GrbEmail);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "FormAnalizaEmailGYG";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FormAnalizaEmailGYG";
            Load += FormAnalizaEmailGYG_Load;
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
        private Button BtnCrearReserva;
        private TextBox TxtMenoresG;
        private Label label11;
        private TextBox TxtMenores;
        private Label label10;
        private TextBox TxtPrice;
        private Label label12;
        private Button BtnEnviarConfirm;
        private CheckBox ChkCrearConEmail;
        private CheckBox ChkEnviarConfirm;
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
        private Timer timer1;
        private Timer timerCrearReserva;
    }
}