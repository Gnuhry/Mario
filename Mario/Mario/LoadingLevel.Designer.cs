namespace Mario
{
    partial class LoadingLevel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoadingLevel));
            this.lbLevel = new System.Windows.Forms.Label();
            this.lbLevelName = new System.Windows.Forms.Label();
            this.pcBStar1 = new System.Windows.Forms.PictureBox();
            this.pcBStar2 = new System.Windows.Forms.PictureBox();
            this.pcBStar3 = new System.Windows.Forms.PictureBox();
            this.lbHigscore = new System.Windows.Forms.Label();
            this.panel = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.pcBStar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcBStar2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcBStar3)).BeginInit();
            this.panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbLevel
            // 
            this.lbLevel.BackColor = System.Drawing.Color.Transparent;
            this.lbLevel.Font = new System.Drawing.Font("Monotype Corsiva", 26.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbLevel.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lbLevel.Location = new System.Drawing.Point(9, 17);
            this.lbLevel.Name = "lbLevel";
            this.lbLevel.Size = new System.Drawing.Size(171, 87);
            this.lbLevel.TabIndex = 0;
            this.lbLevel.Text = "Level 1-1";
            this.lbLevel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbLevelName
            // 
            this.lbLevelName.BackColor = System.Drawing.Color.Transparent;
            this.lbLevelName.Font = new System.Drawing.Font("Monotype Corsiva", 20.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbLevelName.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lbLevelName.Location = new System.Drawing.Point(200, 21);
            this.lbLevelName.Name = "lbLevelName";
            this.lbLevelName.Size = new System.Drawing.Size(250, 83);
            this.lbLevelName.TabIndex = 1;
            this.lbLevelName.Text = "Test Level";
            this.lbLevelName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pcBStar1
            // 
            this.pcBStar1.Image = global::Mario.Properties.Resources.ricecoin;
            this.pcBStar1.Location = new System.Drawing.Point(67, 173);
            this.pcBStar1.Name = "pcBStar1";
            this.pcBStar1.Size = new System.Drawing.Size(90, 78);
            this.pcBStar1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pcBStar1.TabIndex = 2;
            this.pcBStar1.TabStop = false;
            // 
            // pcBStar2
            // 
            this.pcBStar2.Image = global::Mario.Properties.Resources.ricecoin;
            this.pcBStar2.Location = new System.Drawing.Point(178, 173);
            this.pcBStar2.Name = "pcBStar2";
            this.pcBStar2.Size = new System.Drawing.Size(90, 78);
            this.pcBStar2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pcBStar2.TabIndex = 3;
            this.pcBStar2.TabStop = false;
            // 
            // pcBStar3
            // 
            this.pcBStar3.Image = global::Mario.Properties.Resources.ricecoin;
            this.pcBStar3.Location = new System.Drawing.Point(296, 173);
            this.pcBStar3.Name = "pcBStar3";
            this.pcBStar3.Size = new System.Drawing.Size(90, 78);
            this.pcBStar3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pcBStar3.TabIndex = 4;
            this.pcBStar3.TabStop = false;
            // 
            // lbHigscore
            // 
            this.lbHigscore.BackColor = System.Drawing.Color.Transparent;
            this.lbHigscore.Font = new System.Drawing.Font("Monotype Corsiva", 26.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbHigscore.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lbHigscore.Location = new System.Drawing.Point(99, 91);
            this.lbHigscore.Name = "lbHigscore";
            this.lbHigscore.Size = new System.Drawing.Size(286, 51);
            this.lbHigscore.TabIndex = 5;
            this.lbHigscore.Text = "Higscoore: 120sek";
            this.lbHigscore.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel
            // 
            this.panel.Controls.Add(this.pcBStar1);
            this.panel.Controls.Add(this.lbHigscore);
            this.panel.Controls.Add(this.lbLevel);
            this.panel.Controls.Add(this.pcBStar3);
            this.panel.Controls.Add(this.lbLevelName);
            this.panel.Controls.Add(this.pcBStar2);
            this.panel.Location = new System.Drawing.Point(12, 12);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(466, 290);
            this.panel.TabIndex = 6;
            // 
            // LoadingLevel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1266, 534);
            this.Controls.Add(this.panel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LoadingLevel";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "LoadingLevel";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.pcBStar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcBStar2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcBStar3)).EndInit();
            this.panel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbLevel;
        private System.Windows.Forms.Label lbLevelName;
        private System.Windows.Forms.PictureBox pcBStar1;
        private System.Windows.Forms.PictureBox pcBStar2;
        private System.Windows.Forms.PictureBox pcBStar3;
        private System.Windows.Forms.Label lbHigscore;
        private System.Windows.Forms.Panel panel;
    }
}