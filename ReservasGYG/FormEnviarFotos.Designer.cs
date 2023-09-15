using System.Drawing;
using System.Windows.Forms;

namespace ReservasGYG
{
    partial class FormEnviarFotos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormEnviarFotos));
            TxtFotosSeleccionada = new TextBox();
            label8 = new Label();
            CboHoras = new ComboBox();
            BtnEnviarFotos = new Button();
            DateTimePickerGYG = new DateTimePicker();
            BtnComprobarEmails = new Button();
            LabelInfo = new Label();
            TxtFotosDia = new TextBox();
            BtnExtraerHoras = new Button();
            BtnLimpiar = new Button();
            BtnPegar = new Button();
            BtnEnviarFotosDia = new Button();
            BtnComprobarReservasHoras = new Button();
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
            LabelInfoListView = new Label();
            ContextMenuListView.SuspendLayout();
            SuspendLayout();
            // 
            // TxtFotosSeleccionada
            // 
            TxtFotosSeleccionada.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            TxtFotosSeleccionada.BackColor = Color.LightCyan;
            TxtFotosSeleccionada.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point);
            TxtFotosSeleccionada.Location = new Point(633, 55);
            TxtFotosSeleccionada.Margin = new Padding(12, 6, 12, 12);
            TxtFotosSeleccionada.Multiline = true;
            TxtFotosSeleccionada.Name = "TxtFotosSeleccionada";
            TxtFotosSeleccionada.ReadOnly = true;
            TxtFotosSeleccionada.ScrollBars = ScrollBars.Both;
            TxtFotosSeleccionada.Size = new Size(654, 281);
            TxtFotosSeleccionada.TabIndex = 7;
            TxtFotosSeleccionada.Text = resources.GetString("TxtFotosSeleccionada.Text");
            // 
            // label8
            // 
            label8.Location = new Point(631, 15);
            label8.Margin = new Padding(6, 3, 3, 3);
            label8.Name = "label8";
            label8.Size = new Size(180, 31);
            label8.TabIndex = 4;
            label8.Text = "Selecciona la hora:";
            // 
            // CboHoras
            // 
            CboHoras.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            CboHoras.DropDownStyle = ComboBoxStyle.DropDownList;
            CboHoras.FormattingEnabled = true;
            CboHoras.Items.AddRange(new object[] { "09:30 (Ruta Corta)", "10:30 (Ruta Larga)", "11:00 (Ruta Corta)", "11:05 (Ruta Tablas)", "11:45 (Ruta Corta)", "13:15 (Ruta Corta)", "13:30 (Ruta Larga)", "14:00 (Ruta Corta)", "15:30 (Ruta Corta)", "16:15 (Ruta Corta)", "16:30 (Ruta Larga)", "17:45 (Ruta Corta)", "18:00 (Ruta Corta)" });
            CboHoras.Location = new Point(817, 12);
            CboHoras.Name = "CboHoras";
            CboHoras.Size = new Size(251, 33);
            CboHoras.TabIndex = 5;
            CboHoras.SelectedIndexChanged += CboHoras_SelectedIndexChanged;
            // 
            // BtnEnviarFotos
            // 
            BtnEnviarFotos.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            BtnEnviarFotos.BackColor = Color.Honeydew;
            BtnEnviarFotos.Location = new Point(633, 524);
            BtnEnviarFotos.Margin = new Padding(3, 12, 3, 3);
            BtnEnviarFotos.Name = "BtnEnviarFotos";
            BtnEnviarFotos.Size = new Size(654, 60);
            BtnEnviarFotos.TabIndex = 10;
            BtnEnviarFotos.Text = "Enviar las fotos de";
            BtnEnviarFotos.UseVisualStyleBackColor = false;
            BtnEnviarFotos.Click += BtnEnviarFotos_Click;
            // 
            // DateTimePickerGYG
            // 
            DateTimePickerGYG.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            DateTimePickerGYG.CustomFormat = "dd/MM/yyyy";
            DateTimePickerGYG.Format = DateTimePickerFormat.Custom;
            DateTimePickerGYG.Location = new Point(1074, 10);
            DateTimePickerGYG.Name = "DateTimePickerGYG";
            DateTimePickerGYG.Size = new Size(213, 31);
            DateTimePickerGYG.TabIndex = 6;
            DateTimePickerGYG.ValueChanged += DateTimePickerFotos_ValueChanged;
            // 
            // BtnComprobarEmails
            // 
            BtnComprobarEmails.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            BtnComprobarEmails.BackColor = Color.MistyRose;
            BtnComprobarEmails.Location = new Point(631, 360);
            BtnComprobarEmails.Margin = new Padding(3, 12, 3, 3);
            BtnComprobarEmails.Name = "BtnComprobarEmails";
            BtnComprobarEmails.Size = new Size(656, 60);
            BtnComprobarEmails.TabIndex = 8;
            BtnComprobarEmails.Text = "Comprobar si faltan emails";
            BtnComprobarEmails.UseVisualStyleBackColor = false;
            BtnComprobarEmails.Click += BtnComprobarEmails_Click;
            // 
            // LabelInfo
            // 
            LabelInfo.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            LabelInfo.BackColor = SystemColors.Info;
            LabelInfo.Location = new Point(12, 938);
            LabelInfo.Margin = new Padding(3);
            LabelInfo.Name = "LabelInfo";
            LabelInfo.Size = new Size(1284, 32);
            LabelInfo.TabIndex = 13;
            LabelInfo.Text = "Si 'Enviar las fotos...' no está seleccionado, pulsa en 'Comprobar si faltan emails' y asegúrate de elegir una hora con contenido y una fecha anterior a hoy.";
            // 
            // TxtFotosDia
            // 
            TxtFotosDia.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point);
            TxtFotosDia.Location = new Point(21, 15);
            TxtFotosDia.Margin = new Padding(12, 6, 12, 12);
            TxtFotosDia.Multiline = true;
            TxtFotosDia.Name = "TxtFotosDia";
            TxtFotosDia.ScrollBars = ScrollBars.Both;
            TxtFotosDia.Size = new Size(597, 569);
            TxtFotosDia.TabIndex = 0;
            TxtFotosDia.Text = resources.GetString("TxtFotosDia.Text");
            TxtFotosDia.TextChanged += TxtFotosDia_TextChanged;
            // 
            // BtnExtraerHoras
            // 
            BtnExtraerHoras.BackColor = Color.AliceBlue;
            BtnExtraerHoras.Location = new Point(129, 599);
            BtnExtraerHoras.Name = "BtnExtraerHoras";
            BtnExtraerHoras.Size = new Size(489, 60);
            BtnExtraerHoras.TabIndex = 3;
            BtnExtraerHoras.Text = "Extraer las horas de las fotos";
            BtnExtraerHoras.UseVisualStyleBackColor = false;
            BtnExtraerHoras.Click += BtnExtraerHoras_Click;
            // 
            // BtnLimpiar
            // 
            BtnLimpiar.Location = new Point(21, 599);
            BtnLimpiar.Name = "BtnLimpiar";
            BtnLimpiar.Size = new Size(48, 48);
            BtnLimpiar.TabIndex = 1;
            BtnLimpiar.TextImageRelation = TextImageRelation.ImageAboveText;
            BtnLimpiar.UseVisualStyleBackColor = true;
            BtnLimpiar.Click += BtnLimpiar_Click;
            // 
            // BtnPegar
            // 
            BtnPegar.Location = new Point(75, 599);
            BtnPegar.Name = "BtnPegar";
            BtnPegar.Size = new Size(48, 48);
            BtnPegar.TabIndex = 2;
            BtnPegar.TextImageRelation = TextImageRelation.ImageAboveText;
            BtnPegar.UseVisualStyleBackColor = true;
            BtnPegar.Click += BtnPegar_Click;
            // 
            // BtnEnviarFotosDia
            // 
            BtnEnviarFotosDia.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            BtnEnviarFotosDia.BackColor = Color.Honeydew;
            BtnEnviarFotosDia.Location = new Point(633, 599);
            BtnEnviarFotosDia.Margin = new Padding(3, 12, 3, 3);
            BtnEnviarFotosDia.Name = "BtnEnviarFotosDia";
            BtnEnviarFotosDia.Size = new Size(654, 60);
            BtnEnviarFotosDia.TabIndex = 11;
            BtnEnviarFotosDia.Text = "Enviar todas las fotos del día";
            BtnEnviarFotosDia.UseVisualStyleBackColor = false;
            BtnEnviarFotosDia.Click += BtnEnviarFotosDia_Click;
            // 
            // BtnComprobarReservasHoras
            // 
            BtnComprobarReservasHoras.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            BtnComprobarReservasHoras.BackColor = Color.Moccasin;
            BtnComprobarReservasHoras.Location = new Point(631, 435);
            BtnComprobarReservasHoras.Margin = new Padding(3, 12, 3, 3);
            BtnComprobarReservasHoras.Name = "BtnComprobarReservasHoras";
            BtnComprobarReservasHoras.Size = new Size(656, 60);
            BtnComprobarReservasHoras.TabIndex = 9;
            BtnComprobarReservasHoras.Text = "Comprobar las horas de reservas y fotos";
            BtnComprobarReservasHoras.UseVisualStyleBackColor = false;
            BtnComprobarReservasHoras.Click += BtnComprobarReservasHoras_Click;
            // 
            // LvwSinEmail
            // 
            LvwSinEmail.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            LvwSinEmail.Columns.AddRange(new ColumnHeader[] { columnHeader1, columnHeader2 });
            LvwSinEmail.ContextMenuStrip = ContextMenuListView;
            LvwSinEmail.FullRowSelect = true;
            LvwSinEmail.GridLines = true;
            LvwSinEmail.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            LvwSinEmail.Location = new Point(21, 674);
            LvwSinEmail.Margin = new Padding(3, 12, 3, 3);
            LvwSinEmail.Name = "LvwSinEmail";
            LvwSinEmail.Size = new Size(1266, 211);
            LvwSinEmail.TabIndex = 12;
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
            ContextMenuListView.Items.AddRange(new ToolStripItem[] { MnuCopiarBooking, MnuCopiarNombre, MnuCopiarTelefono, MnuCopiarReserva, MnuCopiarPax, MnuCopiarEmail, MnuCopiarNotas, MnuSep1, MnuCopiarTodo });
            ContextMenuListView.Name = "ContextMenuListView";
            ContextMenuListView.Size = new Size(209, 266);
            ContextMenuListView.Opening += ContextMenuListView_Opening;
            // 
            // MnuCopiarBooking
            // 
            MnuCopiarBooking.Name = "MnuCopiarBooking";
            MnuCopiarBooking.Size = new Size(208, 32);
            MnuCopiarBooking.Text = "Copiar Booking";
            MnuCopiarBooking.Click += MnuCopiarDeLvw_Click;
            // 
            // MnuCopiarNombre
            // 
            MnuCopiarNombre.Name = "MnuCopiarNombre";
            MnuCopiarNombre.Size = new Size(208, 32);
            MnuCopiarNombre.Text = "Copiar Nombre";
            MnuCopiarNombre.Click += MnuCopiarDeLvw_Click;
            // 
            // MnuCopiarTelefono
            // 
            MnuCopiarTelefono.Name = "MnuCopiarTelefono";
            MnuCopiarTelefono.Size = new Size(208, 32);
            MnuCopiarTelefono.Text = "Copiar Teléfono";
            MnuCopiarTelefono.Click += MnuCopiarDeLvw_Click;
            // 
            // MnuCopiarReserva
            // 
            MnuCopiarReserva.Name = "MnuCopiarReserva";
            MnuCopiarReserva.Size = new Size(208, 32);
            MnuCopiarReserva.Text = "Copiar Reserva";
            MnuCopiarReserva.Click += MnuCopiarDeLvw_Click;
            // 
            // MnuCopiarPax
            // 
            MnuCopiarPax.Name = "MnuCopiarPax";
            MnuCopiarPax.Size = new Size(208, 32);
            MnuCopiarPax.Text = "Copiar Pax";
            MnuCopiarPax.Click += MnuCopiarDeLvw_Click;
            // 
            // MnuCopiarEmail
            // 
            MnuCopiarEmail.Name = "MnuCopiarEmail";
            MnuCopiarEmail.Size = new Size(208, 32);
            MnuCopiarEmail.Text = "Copiar Email";
            MnuCopiarEmail.Click += MnuCopiarDeLvw_Click;
            // 
            // MnuCopiarNotas
            // 
            MnuCopiarNotas.Name = "MnuCopiarNotas";
            MnuCopiarNotas.Size = new Size(208, 32);
            MnuCopiarNotas.Text = "Copiar Notas";
            MnuCopiarNotas.Click += MnuCopiarDeLvw_Click;
            // 
            // MnuSep1
            // 
            MnuSep1.Name = "MnuSep1";
            MnuSep1.Size = new Size(205, 6);
            // 
            // MnuCopiarTodo
            // 
            MnuCopiarTodo.Name = "MnuCopiarTodo";
            MnuCopiarTodo.Size = new Size(208, 32);
            MnuCopiarTodo.Text = "Copiar Todo";
            MnuCopiarTodo.Click += MnuCopiarDeLvw_Click;
            // 
            // LabelInfoListView
            // 
            LabelInfoListView.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            LabelInfoListView.Location = new Point(21, 891);
            LabelInfoListView.Margin = new Padding(3);
            LabelInfoListView.Name = "LabelInfoListView";
            LabelInfoListView.Size = new Size(1266, 31);
            LabelInfoListView.TabIndex = 16;
            LabelInfoListView.Text = "Hay n elementos";
            LabelInfoListView.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // FormEnviarFotos
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1308, 982);
            Controls.Add(LabelInfoListView);
            Controls.Add(LvwSinEmail);
            Controls.Add(BtnComprobarReservasHoras);
            Controls.Add(BtnEnviarFotosDia);
            Controls.Add(BtnPegar);
            Controls.Add(BtnLimpiar);
            Controls.Add(BtnExtraerHoras);
            Controls.Add(TxtFotosDia);
            Controls.Add(LabelInfo);
            Controls.Add(BtnComprobarEmails);
            Controls.Add(DateTimePickerGYG);
            Controls.Add(BtnEnviarFotos);
            Controls.Add(CboHoras);
            Controls.Add(label8);
            Controls.Add(TxtFotosSeleccionada);
            Name = "FormEnviarFotos";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Enviar Fotos de las Rutas";
            Load += FormEnviarFotos_Load;
            ContextMenuListView.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private TextBox TxtFotosSeleccionada;
        private Label label8;
        private ComboBox CboHoras;
        private Button BtnEnviarFotos;
        private DateTimePicker DateTimePickerGYG;
        private Button BtnComprobarEmails;
        private Label LabelInfo;
        private TextBox TxtFotosDia;
        private Button BtnExtraerHoras;
        private Button BtnLimpiar;
        private Button BtnPegar;
        private Button BtnEnviarFotosDia;
        private Button BtnComprobarReservasHoras;
        private ListView LvwSinEmail;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private ContextMenuStrip ContextMenuListView;
        private ToolStripMenuItem MnuCopiarBooking;
        private ToolStripMenuItem MnuCopiarNombre;
        private ToolStripMenuItem MnuCopiarTelefono;
        private ToolStripMenuItem MnuCopiarEmail;
        private ToolStripMenuItem MnuCopiarNotas;
        private ToolStripMenuItem MnuCopiarReserva;
        private ToolStripMenuItem MnuCopiarPax;
        private ToolStripSeparator MnuSep1;
        private ToolStripMenuItem MnuCopiarTodo;

        private Label LabelInfoListView;
    }
}