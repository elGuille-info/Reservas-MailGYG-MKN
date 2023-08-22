namespace ReservasGYG
{
    partial class FormTextosEmail
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
            RtfImportantEN0930 = new System.Windows.Forms.RichTextBox();
            SuspendLayout();
            // 
            // RtfImportantEN0930
            // 
            RtfImportantEN0930.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            RtfImportantEN0930.Location = new System.Drawing.Point(12, 12);
            RtfImportantEN0930.Name = "RtfImportantEN0930";
            RtfImportantEN0930.Size = new System.Drawing.Size(1429, 816);
            RtfImportantEN0930.TabIndex = 0;
            RtfImportantEN0930.Text = "IMPORTANT EN 09.30";
            RtfImportantEN0930.WordWrap = false;
            // 
            // FormTextosEmail
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1453, 840);
            Controls.Add(RtfImportantEN0930);
            Name = "FormTextosEmail";
            Text = "FormTextosEmail";
            Load += FormTextosEmail_Load;
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.RichTextBox RtfImportantEN0930;
    }
}