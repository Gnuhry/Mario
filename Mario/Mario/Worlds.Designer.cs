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
            this.player = new System.Windows.Forms.PictureBox();
            this.pcB4 = new System.Windows.Forms.PictureBox();
            this.pcB1 = new System.Windows.Forms.PictureBox();
            this.pcB3 = new System.Windows.Forms.PictureBox();
            this.pcB2 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.player)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcB4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcB1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcB3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcB2)).BeginInit();
            this.SuspendLayout();
            // 
            // player
            // 
            this.player.BackColor = System.Drawing.Color.Black;
            this.player.Location = new System.Drawing.Point(175, 153);
            this.player.Name = "player";
            this.player.Size = new System.Drawing.Size(60, 50);
            this.player.TabIndex = 0;
            this.player.TabStop = false;
            // 
            // pcB4
            // 
            this.pcB4.BackColor = System.Drawing.Color.Lime;
            this.pcB4.Location = new System.Drawing.Point(885, 248);
            this.pcB4.Name = "pcB4";
            this.pcB4.Size = new System.Drawing.Size(60, 50);
            this.pcB4.TabIndex = 4;
            this.pcB4.TabStop = false;
            // 
            // pcB1
            // 
            this.pcB1.BackColor = System.Drawing.Color.Yellow;
            this.pcB1.Location = new System.Drawing.Point(81, 46);
            this.pcB1.Name = "pcB1";
            this.pcB1.Size = new System.Drawing.Size(60, 50);
            this.pcB1.TabIndex = 5;
            this.pcB1.TabStop = false;
            // 
            // pcB3
            // 
            this.pcB3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.pcB3.Location = new System.Drawing.Point(149, 342);
            this.pcB3.Name = "pcB3";
            this.pcB3.Size = new System.Drawing.Size(60, 50);
            this.pcB3.TabIndex = 6;
            this.pcB3.TabStop = false;
            // 
            // pcB2
            // 
            this.pcB2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.pcB2.Location = new System.Drawing.Point(621, 46);
            this.pcB2.Name = "pcB2";
            this.pcB2.Size = new System.Drawing.Size(60, 50);
            this.pcB2.TabIndex = 7;
            this.pcB2.TabStop = false;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(919, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(145, 39);
            this.label1.TabIndex = 8;
            this.label1.Text = "Menü = ESC Taste\r\nStart=Enter oder Leerzeichen\r\nBewegen=Links oder Rechts";
            // 
            // Worlds
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1076, 511);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pcB2);
            this.Controls.Add(this.pcB3);
            this.Controls.Add(this.pcB1);
            this.Controls.Add(this.pcB4);
            this.Controls.Add(this.player);
            this.Name = "Worlds";
            this.Text = "Worlds";
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
        private System.Windows.Forms.Label label1;
    }
}