namespace Mario
{
    partial class pause
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
            this.label1 = new System.Windows.Forms.Label();
            this.btnMenue = new System.Windows.Forms.Button();
            this.btnSettings = new System.Windows.Forms.Button();
            this.btnRestart = new System.Windows.Forms.Button();
            this.btnGoOn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(105, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 29);
            this.label1.TabIndex = 0;
            this.label1.Text = "Pause";
            // 
            // btnMenue
            // 
            this.btnMenue.Location = new System.Drawing.Point(12, 136);
            this.btnMenue.Name = "btnMenue";
            this.btnMenue.Size = new System.Drawing.Size(284, 23);
            this.btnMenue.TabIndex = 3;
            this.btnMenue.Text = "Zum Weltenmenü";
            this.btnMenue.UseVisualStyleBackColor = true;
            this.btnMenue.Click += new System.EventHandler(this.btnMenue_Click);
            // 
            // btnSettings
            // 
            this.btnSettings.Location = new System.Drawing.Point(12, 165);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(284, 23);
            this.btnSettings.TabIndex = 4;
            this.btnSettings.Text = "Einstellungen";
            this.btnSettings.UseVisualStyleBackColor = true;
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
            // 
            // btnRestart
            // 
            this.btnRestart.Location = new System.Drawing.Point(12, 107);
            this.btnRestart.Name = "btnRestart";
            this.btnRestart.Size = new System.Drawing.Size(284, 23);
            this.btnRestart.TabIndex = 2;
            this.btnRestart.Text = "Neustart";
            this.btnRestart.UseVisualStyleBackColor = true;
            this.btnRestart.Click += new System.EventHandler(this.btnRestart_Click);
            // 
            // btnGoOn
            // 
            this.btnGoOn.Location = new System.Drawing.Point(12, 78);
            this.btnGoOn.Name = "btnGoOn";
            this.btnGoOn.Size = new System.Drawing.Size(284, 23);
            this.btnGoOn.TabIndex = 1;
            this.btnGoOn.Text = "Weiter";
            this.btnGoOn.UseVisualStyleBackColor = true;
            this.btnGoOn.Click += new System.EventHandler(this.btnGoOn_Click);
            // 
            // pause
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(308, 215);
            this.Controls.Add(this.btnGoOn);
            this.Controls.Add(this.btnRestart);
            this.Controls.Add(this.btnSettings);
            this.Controls.Add(this.btnMenue);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "pause";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "pause";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnMenue;
        private System.Windows.Forms.Button btnSettings;
        private System.Windows.Forms.Button btnRestart;
        private System.Windows.Forms.Button btnGoOn;
    }
}