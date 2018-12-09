namespace Mario
{
    partial class Worlds
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Worlds));
            this.player = new System.Windows.Forms.PictureBox();
            this.pcB4 = new System.Windows.Forms.PictureBox();
            this.pcB1 = new System.Windows.Forms.PictureBox();
            this.pcB3 = new System.Windows.Forms.PictureBox();
            this.pcB2 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.player)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcB4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcB1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcB3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcB2)).BeginInit();
            this.SuspendLayout();
            // 
            // player
            // 
            this.player.BackColor = System.Drawing.Color.Transparent;
            this.player.Image = global::Mario.Properties.Resources.player_small_stay_right_0;
            this.player.Location = new System.Drawing.Point(120, 159);
            this.player.Name = "player";
            this.player.Size = new System.Drawing.Size(41, 39);
            this.player.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.player.TabIndex = 0;
            this.player.TabStop = false;
            // 
            // pcB4
            // 
            this.pcB4.BackColor = System.Drawing.Color.Transparent;
            this.pcB4.Location = new System.Drawing.Point(621, 303);
            this.pcB4.Name = "pcB4";
            this.pcB4.Size = new System.Drawing.Size(60, 50);
            this.pcB4.TabIndex = 4;
            this.pcB4.TabStop = false;
            this.pcB4.Tag = "0";
            // 
            // pcB1
            // 
            this.pcB1.BackColor = System.Drawing.Color.Transparent;
            this.pcB1.Location = new System.Drawing.Point(184, 261);
            this.pcB1.Name = "pcB1";
            this.pcB1.Size = new System.Drawing.Size(60, 50);
            this.pcB1.TabIndex = 5;
            this.pcB1.TabStop = false;
            this.pcB1.Tag = "1";
            // 
            // pcB3
            // 
            this.pcB3.BackColor = System.Drawing.Color.Transparent;
            this.pcB3.Location = new System.Drawing.Point(862, 316);
            this.pcB3.Name = "pcB3";
            this.pcB3.Size = new System.Drawing.Size(60, 50);
            this.pcB3.TabIndex = 6;
            this.pcB3.TabStop = false;
            this.pcB3.Tag = "0";
            // 
            // pcB2
            // 
            this.pcB2.BackColor = System.Drawing.Color.Transparent;
            this.pcB2.Location = new System.Drawing.Point(503, 159);
            this.pcB2.Name = "pcB2";
            this.pcB2.Size = new System.Drawing.Size(60, 50);
            this.pcB2.TabIndex = 7;
            this.pcB2.TabStop = false;
            this.pcB2.Tag = "0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(137, 212);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 13);
            this.label2.TabIndex = 9;
            this.label2.Tag = "1-1";
            this.label2.Text = "1-1 Test Level 12sek";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(456, 123);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 13);
            this.label3.TabIndex = 10;
            this.label3.Tag = "1-2";
            this.label3.Text = "1-1 Test Level 12sek";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Location = new System.Drawing.Point(815, 273);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(107, 13);
            this.label5.TabIndex = 11;
            this.label5.Tag = "1-3";
            this.label5.Text = "1-1 Test Level 12sek";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Location = new System.Drawing.Point(574, 273);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(107, 13);
            this.label6.TabIndex = 12;
            this.label6.Tag = "1-4";
            this.label6.Text = "1-1 Test Level 12sek";
            // 
            // Worlds
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Mario.Properties.Resources.lvl_wahl;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1076, 511);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pcB2);
            this.Controls.Add(this.pcB3);
            this.Controls.Add(this.pcB4);
            this.Controls.Add(this.player);
            this.Controls.Add(this.pcB1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Worlds";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Worlds_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.player)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcB4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcB1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcB3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcB2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox player;
        private System.Windows.Forms.PictureBox pcB4;
        private System.Windows.Forms.PictureBox pcB1;
        private System.Windows.Forms.PictureBox pcB3;
        private System.Windows.Forms.PictureBox pcB2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
    }
}