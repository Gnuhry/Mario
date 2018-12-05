namespace Mario
{
    partial class Enemy
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
            this.pcBEnemy = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pcBEnemy)).BeginInit();
            this.SuspendLayout();
            // 
            // pcBEnemy
            // 
            this.pcBEnemy.Image = global::Mario.Properties.Resources.grammy_left;
            this.pcBEnemy.Location = new System.Drawing.Point(0, 0);
            this.pcBEnemy.Name = "pcBEnemy";
            this.pcBEnemy.Size = new System.Drawing.Size(50, 50);
            this.pcBEnemy.TabIndex = 0;
            this.pcBEnemy.TabStop = false;
            // 
            // Enemy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Controls.Add(this.pcBEnemy);
            this.DoubleBuffered = true;
            this.Name = "Enemy";
            this.Size = new System.Drawing.Size(50, 50);
            ((System.ComponentModel.ISupportInitialize)(this.pcBEnemy)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pcBEnemy;
    }
}
