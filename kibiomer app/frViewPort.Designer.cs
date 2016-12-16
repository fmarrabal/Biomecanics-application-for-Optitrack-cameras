namespace kibiomer_app
{
    partial class frViewPort
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
            this.eh = new System.Windows.Forms.Integration.ElementHost();
            this.kibiomerviewport1 = new kibiomer_app.wpf.kibiomerviewport();
            this.SuspendLayout();
            // 
            // eh
            // 
            this.eh.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.eh.Dock = System.Windows.Forms.DockStyle.Fill;
            this.eh.Font = new System.Drawing.Font("Segoe UI Light", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.eh.Location = new System.Drawing.Point(0, 0);
            this.eh.Name = "eh";
            this.eh.Size = new System.Drawing.Size(591, 350);
            this.eh.TabIndex = 0;
            this.eh.Child = this.kibiomerviewport1;
            // 
            // frViewPort
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(591, 350);
            this.Controls.Add(this.eh);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frViewPort";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Visor";
            this.Load += new System.EventHandler(this.frViewPort_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Integration.ElementHost eh;
        private wpf.kibiomerviewport kibiomerviewport1;
    }
}