namespace Mario
{
    partial class score
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbCoin = new System.Windows.Forms.Label();
            this.pcBrCoin3 = new System.Windows.Forms.PictureBox();
            this.pcBrCoin2 = new System.Windows.Forms.PictureBox();
            this.pcBrCoin1 = new System.Windows.Forms.PictureBox();
            this.lbNewRecord = new System.Windows.Forms.Label();
            this.lbTime = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pcBrCoin3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcBrCoin2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcBrCoin1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lbCoin);
            this.panel1.Controls.Add(this.pcBrCoin3);
            this.panel1.Controls.Add(this.pcBrCoin2);
            this.panel1.Controls.Add(this.pcBrCoin1);
            this.panel1.Controls.Add(this.lbNewRecord);
            this.panel1.Controls.Add(this.lbTime);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(546, 362);
            this.panel1.TabIndex = 0;
            // 
            // lbCoin
            // 
            this.lbCoin.AutoSize = true;
            this.lbCoin.Font = new System.Drawing.Font("November", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCoin.ForeColor = System.Drawing.Color.White;
            this.lbCoin.Location = new System.Drawing.Point(144, 167);
            this.lbCoin.Name = "lbCoin";
            this.lbCoin.Size = new System.Drawing.Size(222, 74);
            this.lbCoin.TabIndex = 5;
            this.lbCoin.Text = "0 Coins";
            // 
            // pcBrCoin3
            // 
            this.pcBrCoin3.Image = global::Mario.Properties.Resources.ricecoin_not;
            this.pcBrCoin3.Location = new System.Drawing.Point(379, 262);
            this.pcBrCoin3.Name = "pcBrCoin3";
            this.pcBrCoin3.Size = new System.Drawing.Size(92, 81);
            this.pcBrCoin3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pcBrCoin3.TabIndex = 4;
            this.pcBrCoin3.TabStop = false;
            // 
            // pcBrCoin2
            // 
            this.pcBrCoin2.Image = global::Mario.Properties.Resources.ricecoin_not;
            this.pcBrCoin2.Location = new System.Drawing.Point(221, 262);
            this.pcBrCoin2.Name = "pcBrCoin2";
            this.pcBrCoin2.Size = new System.Drawing.Size(92, 81);
            this.pcBrCoin2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pcBrCoin2.TabIndex = 3;
            this.pcBrCoin2.TabStop = false;
            // 
            // pcBrCoin1
            // 
            this.pcBrCoin1.Image = global::Mario.Properties.Resources.ricecoin_not;
            this.pcBrCoin1.Location = new System.Drawing.Point(75, 262);
            this.pcBrCoin1.Name = "pcBrCoin1";
            this.pcBrCoin1.Size = new System.Drawing.Size(92, 81);
            this.pcBrCoin1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pcBrCoin1.TabIndex = 2;
            this.pcBrCoin1.TabStop = false;
            // 
            // lbNewRecord
            // 
            this.lbNewRecord.AutoSize = true;
            this.lbNewRecord.Font = new System.Drawing.Font("November", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbNewRecord.ForeColor = System.Drawing.Color.Yellow;
            this.lbNewRecord.Location = new System.Drawing.Point(103, 19);
            this.lbNewRecord.Name = "lbNewRecord";
            this.lbNewRecord.Size = new System.Drawing.Size(347, 74);
            this.lbNewRecord.TabIndex = 1;
            this.lbNewRecord.Text = "New Record";
            this.lbNewRecord.Visible = false;
            // 
            // lbTime
            // 
            this.lbTime.AutoSize = true;
            this.lbTime.Font = new System.Drawing.Font("November", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTime.ForeColor = System.Drawing.Color.White;
            this.lbTime.Location = new System.Drawing.Point(158, 93);
            this.lbTime.Name = "lbTime";
            this.lbTime.Size = new System.Drawing.Size(165, 74);
            this.lbTime.TabIndex = 0;
            this.lbTime.Text = "0 sek";
            // 
            // score
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1075, 534);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "score";
            this.ShowInTaskbar = false;
            this.Text = "score";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.score_KeyDown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pcBrCoin3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcBrCoin2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcBrCoin1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lbCoin;
        private System.Windows.Forms.PictureBox pcBrCoin3;
        private System.Windows.Forms.PictureBox pcBrCoin2;
        private System.Windows.Forms.PictureBox pcBrCoin1;
        private System.Windows.Forms.Label lbNewRecord;
        private System.Windows.Forms.Label lbTime;
    }
}