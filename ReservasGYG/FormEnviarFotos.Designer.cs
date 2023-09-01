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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormEnviarFotos));
            TxtFotosSeleccionada = new System.Windows.Forms.TextBox();
            label8 = new System.Windows.Forms.Label();
            CboHoras = new System.Windows.Forms.ComboBox();
            BtnEnviarFotos = new System.Windows.Forms.Button();
            DateTimePickerGYG = new System.Windows.Forms.DateTimePicker();
            BtnComprobarEmails = new System.Windows.Forms.Button();
            label1 = new System.Windows.Forms.Label();
            TxtFotosDia = new System.Windows.Forms.TextBox();
            BtnExtraerHoras = new System.Windows.Forms.Button();
            BtnLimpiar = new System.Windows.Forms.Button();
            BtnPegar = new System.Windows.Forms.Button();
            BtnEnviarFotosDia = new System.Windows.Forms.Button();
            BtnComprobarReservasHoras = new System.Windows.Forms.Button();
            SuspendLayout();
            // 
            // TxtFotosSeleccionada
            // 
            TxtFotosSeleccionada.BackColor = System.Drawing.Color.LightCyan;
            TxtFotosSeleccionada.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            TxtFotosSeleccionada.Location = new System.Drawing.Point(633, 55);
            TxtFotosSeleccionada.Margin = new System.Windows.Forms.Padding(12, 6, 12, 12);
            TxtFotosSeleccionada.Multiline = true;
            TxtFotosSeleccionada.Name = "TxtFotosSeleccionada";
            TxtFotosSeleccionada.ReadOnly = true;
            TxtFotosSeleccionada.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            TxtFotosSeleccionada.Size = new System.Drawing.Size(654, 338);
            TxtFotosSeleccionada.TabIndex = 7;
            TxtFotosSeleccionada.Text = resources.GetString("TxtFotosSeleccionada.Text");
            // 
            // label8
            // 
            label8.Location = new System.Drawing.Point(631, 15);
            label8.Margin = new System.Windows.Forms.Padding(6, 3, 3, 3);
            label8.Name = "label8";
            label8.Size = new System.Drawing.Size(180, 31);
            label8.TabIndex = 4;
            label8.Text = "Selecciona la hora:";
            // 
            // CboHoras
            // 
            CboHoras.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            CboHoras.FormattingEnabled = true;
            CboHoras.Items.AddRange(new object[] { "09:30 (Ruta Corta)", "10:30 (Ruta Larga)", "11:00 (Ruta Corta)", "11:05 (Ruta Tablas)", "11:45 (Ruta Corta)", "13:15 (Ruta Corta)", "13:30 (Ruta Larga)", "14:00 (Ruta Corta)", "15:30 (Ruta Corta)", "16:15 (Ruta Corta)", "16:30 (Ruta Larga)", "17:45 (Ruta Corta)", "18:00 (Ruta Corta)" });
            CboHoras.Location = new System.Drawing.Point(817, 12);
            CboHoras.Name = "CboHoras";
            CboHoras.Size = new System.Drawing.Size(251, 33);
            CboHoras.TabIndex = 5;
            CboHoras.SelectedIndexChanged += CboHoras_SelectedIndexChanged;
            // 
            // BtnEnviarFotos
            // 
            BtnEnviarFotos.BackColor = System.Drawing.Color.Honeydew;
            BtnEnviarFotos.Location = new System.Drawing.Point(633, 584);
            BtnEnviarFotos.Margin = new System.Windows.Forms.Padding(3, 12, 3, 3);
            BtnEnviarFotos.Name = "BtnEnviarFotos";
            BtnEnviarFotos.Size = new System.Drawing.Size(654, 60);
            BtnEnviarFotos.TabIndex = 10;
            BtnEnviarFotos.Text = "Enviar las fotos de";
            BtnEnviarFotos.UseVisualStyleBackColor = false;
            BtnEnviarFotos.Click += BtnEnviarFotos_Click;
            // 
            // DateTimePickerGYG
            // 
            DateTimePickerGYG.CustomFormat = "dd/MM/yyyy";
            DateTimePickerGYG.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            DateTimePickerGYG.Location = new System.Drawing.Point(1074, 10);
            DateTimePickerGYG.Name = "DateTimePickerGYG";
            DateTimePickerGYG.Size = new System.Drawing.Size(213, 31);
            DateTimePickerGYG.TabIndex = 6;
            DateTimePickerGYG.ValueChanged += DateTimePickerFotos_ValueChanged;
            // 
            // BtnComprobarEmails
            // 
            BtnComprobarEmails.BackColor = System.Drawing.Color.MistyRose;
            BtnComprobarEmails.Location = new System.Drawing.Point(633, 420);
            BtnComprobarEmails.Margin = new System.Windows.Forms.Padding(3, 12, 3, 3);
            BtnComprobarEmails.Name = "BtnComprobarEmails";
            BtnComprobarEmails.Size = new System.Drawing.Size(654, 60);
            BtnComprobarEmails.TabIndex = 8;
            BtnComprobarEmails.Text = "Comprobar si faltan emails";
            BtnComprobarEmails.UseVisualStyleBackColor = false;
            BtnComprobarEmails.Click += BtnComprobarEmails_Click;
            // 
            // label1
            // 
            label1.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            label1.BackColor = System.Drawing.SystemColors.Info;
            label1.Location = new System.Drawing.Point(627, 750);
            label1.Margin = new System.Windows.Forms.Padding(6, 3, 3, 3);
            label1.Name = "label1";
            label1.Padding = new System.Windows.Forms.Padding(12);
            label1.Size = new System.Drawing.Size(669, 107);
            label1.TabIndex = 12;
            label1.Text = "Si 'Enviar las fotos...' no está seleccionado, pulsa en 'Comprobar si faltan emails' y asegúrate de elegir una hora con contenido y una fecha anterior a hoy.";
            label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TxtFotosDia
            // 
            TxtFotosDia.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            TxtFotosDia.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            TxtFotosDia.Location = new System.Drawing.Point(21, 15);
            TxtFotosDia.Margin = new System.Windows.Forms.Padding(12, 6, 12, 12);
            TxtFotosDia.Multiline = true;
            TxtFotosDia.Name = "TxtFotosDia";
            TxtFotosDia.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            TxtFotosDia.Size = new System.Drawing.Size(597, 767);
            TxtFotosDia.TabIndex = 0;
            TxtFotosDia.Text = resources.GetString("TxtFotosDia.Text");
            TxtFotosDia.TextChanged += TxtFotosDia_TextChanged;
            // 
            // BtnExtraerHoras
            // 
            BtnExtraerHoras.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            BtnExtraerHoras.BackColor = System.Drawing.Color.AliceBlue;
            BtnExtraerHoras.Location = new System.Drawing.Point(129, 797);
            BtnExtraerHoras.Name = "BtnExtraerHoras";
            BtnExtraerHoras.Size = new System.Drawing.Size(489, 60);
            BtnExtraerHoras.TabIndex = 3;
            BtnExtraerHoras.Text = "Extraer las horas de las fotos";
            BtnExtraerHoras.UseVisualStyleBackColor = false;
            BtnExtraerHoras.Click += BtnExtraerHoras_Click;
            // 
            // BtnLimpiar
            // 
            BtnLimpiar.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            BtnLimpiar.Location = new System.Drawing.Point(21, 797);
            BtnLimpiar.Name = "BtnLimpiar";
            BtnLimpiar.Size = new System.Drawing.Size(48, 48);
            BtnLimpiar.TabIndex = 1;
            BtnLimpiar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            BtnLimpiar.UseVisualStyleBackColor = true;
            BtnLimpiar.Click += BtnLimpiar_Click;
            // 
            // BtnPegar
            // 
            BtnPegar.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            BtnPegar.Location = new System.Drawing.Point(75, 797);
            BtnPegar.Name = "BtnPegar";
            BtnPegar.Size = new System.Drawing.Size(48, 48);
            BtnPegar.TabIndex = 2;
            BtnPegar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            BtnPegar.UseVisualStyleBackColor = true;
            BtnPegar.Click += BtnPegar_Click;
            // 
            // BtnEnviarFotosDia
            // 
            BtnEnviarFotosDia.BackColor = System.Drawing.Color.Honeydew;
            BtnEnviarFotosDia.Location = new System.Drawing.Point(633, 659);
            BtnEnviarFotosDia.Margin = new System.Windows.Forms.Padding(3, 12, 3, 3);
            BtnEnviarFotosDia.Name = "BtnEnviarFotosDia";
            BtnEnviarFotosDia.Size = new System.Drawing.Size(654, 60);
            BtnEnviarFotosDia.TabIndex = 11;
            BtnEnviarFotosDia.Text = "Enviar todas las fotos del día";
            BtnEnviarFotosDia.UseVisualStyleBackColor = false;
            BtnEnviarFotosDia.Click += BtnEnviarFotosDia_Click;
            // 
            // BtnComprobarReservasHoras
            // 
            BtnComprobarReservasHoras.BackColor = System.Drawing.Color.Moccasin;
            BtnComprobarReservasHoras.Location = new System.Drawing.Point(633, 495);
            BtnComprobarReservasHoras.Margin = new System.Windows.Forms.Padding(3, 12, 3, 3);
            BtnComprobarReservasHoras.Name = "BtnComprobarReservasHoras";
            BtnComprobarReservasHoras.Size = new System.Drawing.Size(654, 60);
            BtnComprobarReservasHoras.TabIndex = 9;
            BtnComprobarReservasHoras.Text = "Comprobar las horas de reservas y fotos";
            BtnComprobarReservasHoras.UseVisualStyleBackColor = false;
            BtnComprobarReservasHoras.Click += BtnComprobarReservasHoras_Click;
            // 
            // FormEnviarFotos
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1308, 869);
            Controls.Add(BtnComprobarReservasHoras);
            Controls.Add(BtnEnviarFotosDia);
            Controls.Add(BtnPegar);
            Controls.Add(BtnLimpiar);
            Controls.Add(BtnExtraerHoras);
            Controls.Add(TxtFotosDia);
            Controls.Add(label1);
            Controls.Add(BtnComprobarEmails);
            Controls.Add(DateTimePickerGYG);
            Controls.Add(BtnEnviarFotos);
            Controls.Add(CboHoras);
            Controls.Add(label8);
            Controls.Add(TxtFotosSeleccionada);
            Name = "FormEnviarFotos";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Enviar Fotos de las Rutas";
            Load += FormEnviarFotos_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private System.Windows.Forms.TextBox TxtFotosSeleccionada;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox CboHoras;
        private System.Windows.Forms.Button BtnEnviarFotos;
        private System.Windows.Forms.DateTimePicker DateTimePickerGYG;
        private System.Windows.Forms.Button BtnComprobarEmails;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TxtFotosDia;
        private System.Windows.Forms.Button BtnExtraerHoras;
        private System.Windows.Forms.Button BtnLimpiar;
        private System.Windows.Forms.Button BtnPegar;
        private System.Windows.Forms.Button BtnEnviarFotosDia;
        private System.Windows.Forms.Button BtnComprobarReservasHoras;
    }
}