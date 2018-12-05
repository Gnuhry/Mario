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
            this.pcBStar1 = new System.Windows.Forms.PictureBox();
            this.pcBStar2 = new System.Windows.Forms.PictureBox();
            this.pcBStar3 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pcBStar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcBStar2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcBStar3)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(105, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 29);
            this.label1.TabIndex = 0;
            this.label1.Text = "Pause";
            // 
            // btnMenue
            // 
            this.btnMenue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(171)))), ((int)(((byte)(119)))), ((int)(((byte)(78)))));
            this.btnMenue.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMenue.Location = new System.Drawing.Point(11, 194);
            this.btnMenue.Name = "btnMenue";
            this.btnMenue.Size = new System.Drawing.Size(284, 23);
            this.btnMenue.TabIndex = 3;
            this.btnMenue.Text = "Zum Weltenmenü";
            this.btnMenue.UseVisualStyleBackColor = false;
            this.btnMenue.Click += new System.EventHandler(this.btnMenue_Click);
            // 
            // btnSettings
            // 
            this.btnSettings.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(171)))), ((int)(((byte)(119)))), ((int)(((byte)(78)))));
            this.btnSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSettings.Location = new System.Drawing.Point(11, 223);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(284, 23);
            this.btnSettings.TabIndex = 4;
            this.btnSettings.Text = "Einstellungen";
            this.btnSettings.UseVisualStyleBackColor = false;
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
            // 
            // btnRestart
            // 
            this.btnRestart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(171)))), ((int)(((byte)(119)))), ((int)(((byte)(78)))));
            this.btnRestart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRestart.Location = new System.Drawing.Point(11, 165);
            this.btnRestart.Name = "btnRestart";
            this.btnRestart.Size = new System.Drawing.Size(284, 23);
            this.btnRestart.TabIndex = 2;
            this.btnRestart.Text = "Neustart";
            this.btnRestart.UseVisualStyleBackColor = false;
            this.btnRestart.Click += new System.EventHandler(this.btnRestart_Click);
            // 
            // btnGoOn
            // 
            this.btnGoOn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(171)))), ((int)(((byte)(119)))), ((int)(((byte)(78)))));
            this.btnGoOn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGoOn.Location = new System.Drawing.Point(11, 136);
            this.btnGoOn.Name = "btnGoOn";
            this.btnGoOn.Size = new System.Drawing.Size(284, 23);
            this.btnGoOn.TabIndex = 1;
            this.btnGoOn.Text = "Weiter";
            this.btnGoOn.UseVisualStyleBackColor = false;
            this.btnGoOn.Click += new System.EventHandler(this.btnGoOn_Click);
            // 
            // pcBStar1
            // 
            this.pcBStar1.BackColor = System.Drawing.Color.Transparent;
            this.pcBStar1.Location = new System.Drawing.Point(39, 69);
            this.pcBStar1.Name = "pcBStar1";
            this.pcBStar1.Size = new System.Drawing.Size(50, 50);
            this.pcBStar1.TabIndex = 5;
            this.pcBStar1.TabStop = false;
            // 
            // pcBStar2
            // 
            this.pcBStar2.BackColor = System.Drawing.Color.Transparent;
            this.pcBStar2.Location = new System.Drawing.Point(130, 69);
            this.pcBStar2.Name = "pcBStar2";
            this.pcBStar2.Size = new System.Drawing.Size(50, 50);
            this.pcBStar2.TabIndex = 6;
            this.pcBStar2.TabStop = false;
            // 
            // pcBStar3
            // 
            this.pcBStar3.BackColor = System.Drawing.Color.Transparent;
            this.pcBStar3.Location = new System.Drawing.Point(222, 69);
            this.pcBStar3.Name = "pcBStar3";
            this.pcBStar3.Size = new System.Drawing.Size(50, 50);
            this.pcBStar3.TabIndex = 7;
            this.pcBStar3.TabStop = false;
            // 
            // pause
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Mario.Properties.Resources.Pause;
            this.ClientSize = new System.Drawing.Size(307, 287);
            this.Controls.Add(this.pcBStar3);
            this.Controls.Add(this.pcBStar2);
            this.Controls.Add(this.pcBStar1);
            this.Controls.Add(this.btnGoOn);
            this.Controls.Add(this.btnRestart);
            this.Controls.Add(this.btnSettings);
            this.Controls.Add(this.btnMenue);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "pause";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "pause";
            ((System.ComponentModel.ISupportInitialize)(this.pcBStar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcBStar2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcBStar3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnMenue;
        private System.Windows.Forms.Button btnSettings;
        private System.Windows.Forms.Button btnRestart;
        private System.Windows.Forms.Button btnGoOn;
        private System.Windows.Forms.PictureBox pcBStar1;
        private System.Windows.Forms.PictureBox pcBStar2;
        private System.Windows.Forms.PictureBox pcBStar3;
    }
}