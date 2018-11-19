namespace Mario
{
    partial class Players
    {
        /// <summary> 
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Komponenten-Designer generierter Code

        /// <summary> 
        /// Erforderliche Methode für die Designerunterstützung. 
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.pcBPlayer = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pcBPlayer)).BeginInit();
            this.SuspendLayout();
            // 
            // pcBPlayer
            // 
            this.pcBPlayer.Location = new System.Drawing.Point(0, 0);
            this.pcBPlayer.Name = "pcBPlayer";
            this.pcBPlayer.Size = new System.Drawing.Size(50, 100);
            this.pcBPlayer.TabIndex = 0;
            this.pcBPlayer.TabStop = false;
            // 
            // Players
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.pcBPlayer);
            this.DoubleBuffered = true;
            this.Name = "Players";
            this.Size = new System.Drawing.Size(50, 100);
            ((System.ComponentModel.ISupportInitialize)(this.pcBPlayer)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pcBPlayer;
    }
}
