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
            TxtFotos0930 = new System.Windows.Forms.TextBox();
            TxtFotosSeleccionada = new System.Windows.Forms.TextBox();
            label8 = new System.Windows.Forms.Label();
            FlowLayoutPanelFotos = new System.Windows.Forms.FlowLayoutPanel();
            TxtFotos1030 = new System.Windows.Forms.TextBox();
            txtFotos1100 = new System.Windows.Forms.TextBox();
            txtFotos1145 = new System.Windows.Forms.TextBox();
            TxtFotos1315 = new System.Windows.Forms.TextBox();
            TxtFotos1330 = new System.Windows.Forms.TextBox();
            TxtFotos1400 = new System.Windows.Forms.TextBox();
            TxtFotos1530 = new System.Windows.Forms.TextBox();
            TxtFotos1615 = new System.Windows.Forms.TextBox();
            TxtFotos1630 = new System.Windows.Forms.TextBox();
            TxtFotos1745 = new System.Windows.Forms.TextBox();
            TxtFotos1800 = new System.Windows.Forms.TextBox();
            CboHoras = new System.Windows.Forms.ComboBox();
            BtnEnviarFotos = new System.Windows.Forms.Button();
            DateTimePickerGYG = new System.Windows.Forms.DateTimePicker();
            BtnComprobarEmails = new System.Windows.Forms.Button();
            label1 = new System.Windows.Forms.Label();
            textBox1 = new System.Windows.Forms.TextBox();
            BtnExtraerHoras = new System.Windows.Forms.Button();
            TimerColorearHoras = new System.Windows.Forms.Timer(components);
            FlowLayoutPanelFotos.SuspendLayout();
            SuspendLayout();
            // 
            // TxtFotos0930
            // 
            TxtFotos0930.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            TxtFotos0930.Location = new System.Drawing.Point(12, 12);
            TxtFotos0930.Margin = new System.Windows.Forms.Padding(6);
            TxtFotos0930.Multiline = true;
            TxtFotos0930.Name = "TxtFotos0930";
            TxtFotos0930.Size = new System.Drawing.Size(576, 180);
            TxtFotos0930.TabIndex = 0;
            TxtFotos0930.Text = "¡Hola!\r\nTe paso el enlace a las fotos de la ruta:\r\n\r\nRuta Miércoles 16.08.23 13.30h - Juanda\r\nhttps://photos.app.goo.gl/8pt4HWFqB9NUWhyg6\r\n\r\n¡Muchas gracias!";
            TxtFotos0930.Enter += TxtFotosHoras_Enter;
            // 
            // TxtFotosSeleccionada
            // 
            TxtFotosSeleccionada.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            TxtFotosSeleccionada.Location = new System.Drawing.Point(633, 55);
            TxtFotosSeleccionada.Margin = new System.Windows.Forms.Padding(12, 6, 12, 12);
            TxtFotosSeleccionada.Multiline = true;
            TxtFotosSeleccionada.Name = "TxtFotosSeleccionada";
            TxtFotosSeleccionada.Size = new System.Drawing.Size(654, 338);
            TxtFotosSeleccionada.TabIndex = 6;
            TxtFotosSeleccionada.Text = resources.GetString("TxtFotosSeleccionada.Text");
            TxtFotosSeleccionada.Enter += TxtFotosHoras_Enter;
            // 
            // label8
            // 
            label8.Location = new System.Drawing.Point(631, 15);
            label8.Margin = new System.Windows.Forms.Padding(6, 3, 3, 3);
            label8.Name = "label8";
            label8.Size = new System.Drawing.Size(180, 31);
            label8.TabIndex = 3;
            label8.Text = "Selecciona la hora:";
            // 
            // FlowLayoutPanelFotos
            // 
            FlowLayoutPanelFotos.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            FlowLayoutPanelFotos.AutoScroll = true;
            FlowLayoutPanelFotos.Controls.Add(TxtFotos0930);
            FlowLayoutPanelFotos.Controls.Add(TxtFotos1030);
            FlowLayoutPanelFotos.Controls.Add(txtFotos1100);
            FlowLayoutPanelFotos.Controls.Add(txtFotos1145);
            FlowLayoutPanelFotos.Controls.Add(TxtFotos1315);
            FlowLayoutPanelFotos.Controls.Add(TxtFotos1330);
            FlowLayoutPanelFotos.Controls.Add(TxtFotos1400);
            FlowLayoutPanelFotos.Controls.Add(TxtFotos1530);
            FlowLayoutPanelFotos.Controls.Add(TxtFotos1615);
            FlowLayoutPanelFotos.Controls.Add(TxtFotos1630);
            FlowLayoutPanelFotos.Controls.Add(TxtFotos1745);
            FlowLayoutPanelFotos.Controls.Add(TxtFotos1800);
            FlowLayoutPanelFotos.Location = new System.Drawing.Point(12, 420);
            FlowLayoutPanelFotos.Name = "FlowLayoutPanelFotos";
            FlowLayoutPanelFotos.Padding = new System.Windows.Forms.Padding(6);
            FlowLayoutPanelFotos.Size = new System.Drawing.Size(606, 437);
            FlowLayoutPanelFotos.TabIndex = 2;
            // 
            // TxtFotos1030
            // 
            TxtFotos1030.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            TxtFotos1030.Location = new System.Drawing.Point(12, 204);
            TxtFotos1030.Margin = new System.Windows.Forms.Padding(6);
            TxtFotos1030.Multiline = true;
            TxtFotos1030.Name = "TxtFotos1030";
            TxtFotos1030.Size = new System.Drawing.Size(576, 180);
            TxtFotos1030.TabIndex = 1;
            TxtFotos1030.Text = "¡Hola!\r\nTe paso el enlace a las fotos de la ruta:\r\n\r\nRuta Miércoles 16.08.23 13.30h - Juanda\r\nhttps://photos.app.goo.gl/8pt4HWFqB9NUWhyg6\r\n\r\n¡Muchas gracias!";
            // 
            // txtFotos1100
            // 
            txtFotos1100.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            txtFotos1100.Location = new System.Drawing.Point(12, 396);
            txtFotos1100.Margin = new System.Windows.Forms.Padding(6);
            txtFotos1100.Multiline = true;
            txtFotos1100.Name = "txtFotos1100";
            txtFotos1100.Size = new System.Drawing.Size(576, 180);
            txtFotos1100.TabIndex = 2;
            txtFotos1100.Text = "¡Hola!\r\nTe paso el enlace a las fotos de la ruta:\r\n\r\nRuta Miércoles 16.08.23 13.30h - Juanda\r\nhttps://photos.app.goo.gl/8pt4HWFqB9NUWhyg6\r\n\r\n¡Muchas gracias!";
            // 
            // txtFotos1145
            // 
            txtFotos1145.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            txtFotos1145.Location = new System.Drawing.Point(12, 588);
            txtFotos1145.Margin = new System.Windows.Forms.Padding(6);
            txtFotos1145.Multiline = true;
            txtFotos1145.Name = "txtFotos1145";
            txtFotos1145.Size = new System.Drawing.Size(576, 180);
            txtFotos1145.TabIndex = 3;
            txtFotos1145.Text = "¡Hola!\r\nTe paso el enlace a las fotos de la ruta:\r\n\r\nRuta Miércoles 16.08.23 13.30h - Juanda\r\nhttps://photos.app.goo.gl/8pt4HWFqB9NUWhyg6\r\n\r\n¡Muchas gracias!";
            // 
            // TxtFotos1315
            // 
            TxtFotos1315.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            TxtFotos1315.Location = new System.Drawing.Point(12, 780);
            TxtFotos1315.Margin = new System.Windows.Forms.Padding(6);
            TxtFotos1315.Multiline = true;
            TxtFotos1315.Name = "TxtFotos1315";
            TxtFotos1315.Size = new System.Drawing.Size(576, 180);
            TxtFotos1315.TabIndex = 4;
            TxtFotos1315.Text = "¡Hola!\r\nTe paso el enlace a las fotos de la ruta:\r\n\r\nRuta Miércoles 16.08.23 13.30h - Juanda\r\nhttps://photos.app.goo.gl/8pt4HWFqB9NUWhyg6\r\n\r\n¡Muchas gracias!";
            // 
            // TxtFotos1330
            // 
            TxtFotos1330.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            TxtFotos1330.Location = new System.Drawing.Point(12, 972);
            TxtFotos1330.Margin = new System.Windows.Forms.Padding(6);
            TxtFotos1330.Multiline = true;
            TxtFotos1330.Name = "TxtFotos1330";
            TxtFotos1330.Size = new System.Drawing.Size(576, 180);
            TxtFotos1330.TabIndex = 5;
            TxtFotos1330.Text = "¡Hola!\r\nTe paso el enlace a las fotos de la ruta:\r\n\r\nRuta Miércoles 16.08.23 13.30h - Juanda\r\nhttps://photos.app.goo.gl/8pt4HWFqB9NUWhyg6\r\n\r\n¡Muchas gracias!";
            // 
            // TxtFotos1400
            // 
            TxtFotos1400.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            TxtFotos1400.Location = new System.Drawing.Point(12, 1164);
            TxtFotos1400.Margin = new System.Windows.Forms.Padding(6);
            TxtFotos1400.Multiline = true;
            TxtFotos1400.Name = "TxtFotos1400";
            TxtFotos1400.Size = new System.Drawing.Size(576, 180);
            TxtFotos1400.TabIndex = 6;
            TxtFotos1400.Text = "¡Hola!\r\nTe paso el enlace a las fotos de la ruta:\r\n\r\nRuta Miércoles 16.08.23 13.30h - Juanda\r\nhttps://photos.app.goo.gl/8pt4HWFqB9NUWhyg6\r\n\r\n¡Muchas gracias!";
            // 
            // TxtFotos1530
            // 
            TxtFotos1530.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            TxtFotos1530.Location = new System.Drawing.Point(12, 1356);
            TxtFotos1530.Margin = new System.Windows.Forms.Padding(6);
            TxtFotos1530.Multiline = true;
            TxtFotos1530.Name = "TxtFotos1530";
            TxtFotos1530.Size = new System.Drawing.Size(576, 180);
            TxtFotos1530.TabIndex = 7;
            TxtFotos1530.Text = "¡Hola!\r\nTe paso el enlace a las fotos de la ruta:\r\n\r\nRuta Miércoles 16.08.23 13.30h - Juanda\r\nhttps://photos.app.goo.gl/8pt4HWFqB9NUWhyg6\r\n\r\n¡Muchas gracias!";
            // 
            // TxtFotos1615
            // 
            TxtFotos1615.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            TxtFotos1615.Location = new System.Drawing.Point(12, 1548);
            TxtFotos1615.Margin = new System.Windows.Forms.Padding(6);
            TxtFotos1615.Multiline = true;
            TxtFotos1615.Name = "TxtFotos1615";
            TxtFotos1615.Size = new System.Drawing.Size(576, 180);
            TxtFotos1615.TabIndex = 8;
            TxtFotos1615.Text = "¡Hola!\r\nTe paso el enlace a las fotos de la ruta:\r\n\r\nRuta Miércoles 16.08.23 13.30h - Juanda\r\nhttps://photos.app.goo.gl/8pt4HWFqB9NUWhyg6\r\n\r\n¡Muchas gracias!";
            // 
            // TxtFotos1630
            // 
            TxtFotos1630.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            TxtFotos1630.Location = new System.Drawing.Point(12, 1740);
            TxtFotos1630.Margin = new System.Windows.Forms.Padding(6);
            TxtFotos1630.Multiline = true;
            TxtFotos1630.Name = "TxtFotos1630";
            TxtFotos1630.Size = new System.Drawing.Size(576, 180);
            TxtFotos1630.TabIndex = 9;
            TxtFotos1630.Text = "¡Hola!\r\nTe paso el enlace a las fotos de la ruta:\r\n\r\nRuta Miércoles 16.08.23 13.30h - Juanda\r\nhttps://photos.app.goo.gl/8pt4HWFqB9NUWhyg6\r\n\r\n¡Muchas gracias!";
            // 
            // TxtFotos1745
            // 
            TxtFotos1745.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            TxtFotos1745.Location = new System.Drawing.Point(12, 1932);
            TxtFotos1745.Margin = new System.Windows.Forms.Padding(6);
            TxtFotos1745.Multiline = true;
            TxtFotos1745.Name = "TxtFotos1745";
            TxtFotos1745.Size = new System.Drawing.Size(576, 180);
            TxtFotos1745.TabIndex = 10;
            TxtFotos1745.Text = "¡Hola!\r\nTe paso el enlace a las fotos de la ruta:\r\n\r\nRuta Miércoles 16.08.23 13.30h - Juanda\r\nhttps://photos.app.goo.gl/8pt4HWFqB9NUWhyg6\r\n\r\n¡Muchas gracias!";
            // 
            // TxtFotos1800
            // 
            TxtFotos1800.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            TxtFotos1800.Location = new System.Drawing.Point(12, 2124);
            TxtFotos1800.Margin = new System.Windows.Forms.Padding(6);
            TxtFotos1800.Multiline = true;
            TxtFotos1800.Name = "TxtFotos1800";
            TxtFotos1800.Size = new System.Drawing.Size(576, 180);
            TxtFotos1800.TabIndex = 11;
            TxtFotos1800.Text = "¡Hola!\r\nTe paso el enlace a las fotos de la ruta:\r\n\r\nRuta Miércoles 16.08.23 13.30h - Juanda\r\nhttps://photos.app.goo.gl/8pt4HWFqB9NUWhyg6\r\n\r\n¡Muchas gracias!";
            // 
            // CboHoras
            // 
            CboHoras.FormattingEnabled = true;
            CboHoras.Items.AddRange(new object[] { "09:30 (Ruta Corta)", "10:30 (Ruta Larga)", "11:00 (Ruta Corta)", "11:05 (Ruta Tablas)", "11:45 (Ruta Corta)", "13:15 (Ruta Corta)", "13:30 (Ruta Larga)", "14:00 (Ruta Corta)", "15:30 (Ruta Corta)", "16:15 (Ruta Corta)", "16:30 (Ruta Larga)", "17:45 (Ruta Corta)", "18:00 (Ruta Corta)" });
            CboHoras.Location = new System.Drawing.Point(817, 12);
            CboHoras.Name = "CboHoras";
            CboHoras.Size = new System.Drawing.Size(251, 33);
            CboHoras.TabIndex = 4;
            CboHoras.SelectedIndexChanged += CboHoras_SelectedIndexChanged;
            // 
            // BtnEnviarFotos
            // 
            BtnEnviarFotos.BackColor = System.Drawing.Color.Honeydew;
            BtnEnviarFotos.Location = new System.Drawing.Point(633, 510);
            BtnEnviarFotos.Name = "BtnEnviarFotos";
            BtnEnviarFotos.Size = new System.Drawing.Size(654, 60);
            BtnEnviarFotos.TabIndex = 8;
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
            DateTimePickerGYG.TabIndex = 5;
            DateTimePickerGYG.ValueChanged += DateTimePickerFotos_ValueChanged;
            // 
            // BtnComprobarEmails
            // 
            BtnComprobarEmails.BackColor = System.Drawing.Color.MistyRose;
            BtnComprobarEmails.Location = new System.Drawing.Point(633, 420);
            BtnComprobarEmails.Name = "BtnComprobarEmails";
            BtnComprobarEmails.Size = new System.Drawing.Size(654, 60);
            BtnComprobarEmails.TabIndex = 7;
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
            label1.TabIndex = 9;
            label1.Text = "Si 'Enviar las fotos...' no está seleccionado, pulsa en 'Comprobar si faltan emails' y asegúrate de elegir una hora con contenido y una fecha anterior a hoy.";
            label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBox1
            // 
            textBox1.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            textBox1.Location = new System.Drawing.Point(21, 15);
            textBox1.Margin = new System.Windows.Forms.Padding(12, 6, 12, 12);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.Size = new System.Drawing.Size(597, 303);
            textBox1.TabIndex = 0;
            textBox1.Text = resources.GetString("textBox1.Text");
            // 
            // BtnExtraerHoras
            // 
            BtnExtraerHoras.BackColor = System.Drawing.Color.AliceBlue;
            BtnExtraerHoras.Location = new System.Drawing.Point(21, 333);
            BtnExtraerHoras.Name = "BtnExtraerHoras";
            BtnExtraerHoras.Size = new System.Drawing.Size(597, 60);
            BtnExtraerHoras.TabIndex = 1;
            BtnExtraerHoras.Text = "Extraer las horas de las fotos";
            BtnExtraerHoras.UseVisualStyleBackColor = false;
            // 
            // TimerColorearHoras
            // 
            TimerColorearHoras.Interval = 300;
            TimerColorearHoras.Tick += TimerColorearHoras_Tick;
            // 
            // FormEnviarFotos
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1308, 869);
            Controls.Add(BtnExtraerHoras);
            Controls.Add(textBox1);
            Controls.Add(label1);
            Controls.Add(BtnComprobarEmails);
            Controls.Add(DateTimePickerGYG);
            Controls.Add(BtnEnviarFotos);
            Controls.Add(FlowLayoutPanelFotos);
            Controls.Add(CboHoras);
            Controls.Add(label8);
            Controls.Add(TxtFotosSeleccionada);
            Name = "FormEnviarFotos";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Enviar Fotos de las Rutas";
            Load += FormEnviarFotos_Load;
            FlowLayoutPanelFotos.ResumeLayout(false);
            FlowLayoutPanelFotos.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private System.Windows.Forms.TextBox TxtFotos0930;
        private System.Windows.Forms.TextBox TxtFotosSeleccionada;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.FlowLayoutPanel FlowLayoutPanelFotos;
        private System.Windows.Forms.ComboBox CboHoras;
        private System.Windows.Forms.Button BtnEnviarFotos;
        private System.Windows.Forms.DateTimePicker DateTimePickerGYG;
        private System.Windows.Forms.Button BtnComprobarEmails;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TxtFotos1030;
        private System.Windows.Forms.TextBox txtFotos1100;
        private System.Windows.Forms.TextBox txtFotos1145;
        private System.Windows.Forms.TextBox TxtFotos1315;
        private System.Windows.Forms.TextBox TxtFotos1330;
        private System.Windows.Forms.TextBox TxtFotos1400;
        private System.Windows.Forms.TextBox TxtFotos1530;
        private System.Windows.Forms.TextBox TxtFotos1615;
        private System.Windows.Forms.TextBox TxtFotos1630;
        private System.Windows.Forms.TextBox TxtFotos1745;
        private System.Windows.Forms.TextBox TxtFotos1800;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button BtnExtraerHoras;
        private System.Windows.Forms.Timer TimerColorearHoras;
    }
}