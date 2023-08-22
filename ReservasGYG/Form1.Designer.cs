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
            GrbBuscarEmails = new GroupBox();
            ChkLeerNuevos = new CheckBox();
            BtnBuscar = new Button();
            TxtFechaHasta = new TextBox();
            label2 = new Label();
            TxtFechaDesde = new TextBox();
            label1 = new Label();
            toolTip1 = new ToolTip(components);
            GrbOpciones = new GroupBox();
            BtnFotos = new Button();
            BtnHoyEs = new Button();
            BtnMostrarReservas = new Button();
            BtnAnalizarEmail = new Button();
            BtnComprobarSinMail = new Button();
            GrbBuscarEmails.SuspendLayout();
            GrbOpciones.SuspendLayout();
            SuspendLayout();
            // 
            // GrbBuscarEmails
            // 
            GrbBuscarEmails.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            GrbBuscarEmails.Controls.Add(ChkLeerNuevos);
            GrbBuscarEmails.Controls.Add(BtnBuscar);
            GrbBuscarEmails.Controls.Add(TxtFechaHasta);
            GrbBuscarEmails.Controls.Add(label2);
            GrbBuscarEmails.Controls.Add(TxtFechaDesde);
            GrbBuscarEmails.Controls.Add(label1);
            GrbBuscarEmails.Location = new Point(12, 353);
            GrbBuscarEmails.Name = "GrbBuscarEmails";
            GrbBuscarEmails.Size = new Size(776, 155);
            GrbBuscarEmails.TabIndex = 1;
            GrbBuscarEmails.TabStop = false;
            GrbBuscarEmails.Text = "Buscar mensajes (NO VISIBLE)";
            GrbBuscarEmails.Visible = false;
            // 
            // ChkLeerNuevos
            // 
            ChkLeerNuevos.AutoSize = true;
            ChkLeerNuevos.Location = new Point(6, 105);
            ChkLeerNuevos.Name = "ChkLeerNuevos";
            ChkLeerNuevos.Size = new Size(199, 29);
            ChkLeerNuevos.TabIndex = 4;
            ChkLeerNuevos.Text = "Leer solo los nuevos";
            toolTip1.SetToolTip(ChkLeerNuevos, "Leer solo los nuevos mensajes");
            ChkLeerNuevos.UseVisualStyleBackColor = true;
            ChkLeerNuevos.CheckedChanged += ChkLeerNuevos_CheckedChanged;
            // 
            // BtnBuscar
            // 
            BtnBuscar.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            BtnBuscar.Location = new Point(658, 101);
            BtnBuscar.Name = "BtnBuscar";
            BtnBuscar.Size = new Size(112, 34);
            BtnBuscar.TabIndex = 5;
            BtnBuscar.Text = "Buscar emails";
            BtnBuscar.UseVisualStyleBackColor = true;
            BtnBuscar.Click += BtnBuscar_Click;
            // 
            // TxtFechaHasta
            // 
            TxtFechaHasta.Location = new Point(200, 67);
            TxtFechaHasta.Name = "TxtFechaHasta";
            TxtFechaHasta.Size = new Size(204, 31);
            TxtFechaHasta.TabIndex = 3;
            TxtFechaHasta.Text = "dd/MM/yyyy HH:mm";
            TxtFechaHasta.EnabledChanged += TxtFechas_EnabledChanged;
            TxtFechaHasta.KeyPress += TxtFechas_KeyPress;
            TxtFechaHasta.KeyUp += TxtFechas_KeyUp;
            // 
            // label2
            // 
            label2.Location = new Point(6, 70);
            label2.Margin = new Padding(3);
            label2.Name = "label2";
            label2.Size = new Size(188, 28);
            label2.TabIndex = 2;
            label2.Text = "Fecha/hora hasta:";
            // 
            // TxtFechaDesde
            // 
            TxtFechaDesde.Location = new Point(200, 30);
            TxtFechaDesde.Name = "TxtFechaDesde";
            TxtFechaDesde.Size = new Size(204, 31);
            TxtFechaDesde.TabIndex = 1;
            TxtFechaDesde.Text = "dd/MM/yyyy HH:mm";
            TxtFechaDesde.EnabledChanged += TxtFechas_EnabledChanged;
            TxtFechaDesde.KeyPress += TxtFechas_KeyPress;
            TxtFechaDesde.KeyUp += TxtFechas_KeyUp;
            // 
            // label1
            // 
            label1.Location = new Point(6, 33);
            label1.Margin = new Padding(3);
            label1.Name = "label1";
            label1.Size = new Size(188, 28);
            label1.TabIndex = 0;
            label1.Text = "Fecha/hora desde:";
            // 
            // GrbOpciones
            // 
            GrbOpciones.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            GrbOpciones.Controls.Add(BtnComprobarSinMail);
            GrbOpciones.Controls.Add(BtnFotos);
            GrbOpciones.Controls.Add(BtnHoyEs);
            GrbOpciones.Controls.Add(BtnMostrarReservas);
            GrbOpciones.Controls.Add(BtnAnalizarEmail);
            GrbOpciones.Location = new Point(12, 12);
            GrbOpciones.Name = "GrbOpciones";
            GrbOpciones.Size = new Size(776, 335);
            GrbOpciones.TabIndex = 0;
            GrbOpciones.TabStop = false;
            GrbOpciones.Text = "Opciones";
            // 
            // BtnFotos
            // 
            BtnFotos.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            BtnFotos.BackColor = Color.LightCoral;
            BtnFotos.Location = new Point(6, 210);
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
            BtnHoyEs.Location = new Point(6, 150);
            BtnHoyEs.Name = "BtnHoyEs";
            BtnHoyEs.Size = new Size(764, 40);
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
            // BtnComprobarSinMail
            // 
            BtnComprobarSinMail.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            BtnComprobarSinMail.BackColor = Color.FloralWhite;
            BtnComprobarSinMail.Location = new Point(0, 270);
            BtnComprobarSinMail.Name = "BtnComprobarSinMail";
            BtnComprobarSinMail.Size = new Size(764, 40);
            BtnComprobarSinMail.TabIndex = 4;
            BtnComprobarSinMail.Text = "Comprobar si hay clientes sin mail";
            BtnComprobarSinMail.UseVisualStyleBackColor = false;
            BtnComprobarSinMail.Click += BtnComprobarSinMail_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 520);
            Controls.Add(GrbOpciones);
            Controls.Add(GrbBuscarEmails);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form1";
            Load += Form1_Load;
            GrbBuscarEmails.ResumeLayout(false);
            GrbBuscarEmails.PerformLayout();
            GrbOpciones.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private GroupBox GrbBuscarEmails;
        private TextBox TxtFechaDesde;
        private Label label1;
        private Button BtnBuscar;
        private TextBox TxtFechaHasta;
        private Label label2;
        private CheckBox ChkLeerNuevos;
        private ToolTip toolTip1;
        private GroupBox GrbOpciones;
        private Button BtnAnalizarEmail;
        private Button BtnFotos;
        private Button BtnHoyEs;
        private Button BtnMostrarReservas;
        private Button BtnComprobarSinMail;
    }
}