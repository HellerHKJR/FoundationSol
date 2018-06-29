namespace MelTest
{
    partial class Form1
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
            this.btnConnect = new System.Windows.Forms.Button();
            this.txtConnection = new System.Windows.Forms.TextBox();
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.btnReadM = new System.Windows.Forms.Button();
            this.txtAddrM = new System.Windows.Forms.TextBox();
            this.txtDataM = new System.Windows.Forms.TextBox();
            this.btnWriteM = new System.Windows.Forms.Button();
            this.btnWriteD = new System.Windows.Forms.Button();
            this.txtDataD = new System.Windows.Forms.TextBox();
            this.txtAddrD = new System.Windows.Forms.TextBox();
            this.btnReadD = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(198, 12);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(74, 26);
            this.btnConnect.TabIndex = 0;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // txtConnection
            // 
            this.txtConnection.Location = new System.Drawing.Point(133, 18);
            this.txtConnection.Name = "txtConnection";
            this.txtConnection.Size = new System.Drawing.Size(44, 20);
            this.txtConnection.TabIndex = 1;
            this.txtConnection.Text = "8";
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.Location = new System.Drawing.Point(198, 44);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(74, 26);
            this.btnDisconnect.TabIndex = 2;
            this.btnDisconnect.Text = "Disconnect";
            this.btnDisconnect.UseVisualStyleBackColor = true;
            this.btnDisconnect.Click += new System.EventHandler(this.btnDisconnect_Click);
            // 
            // btnReadM
            // 
            this.btnReadM.Location = new System.Drawing.Point(198, 89);
            this.btnReadM.Name = "btnReadM";
            this.btnReadM.Size = new System.Drawing.Size(74, 26);
            this.btnReadM.TabIndex = 4;
            this.btnReadM.Text = "Read M";
            this.btnReadM.UseVisualStyleBackColor = true;
            this.btnReadM.Click += new System.EventHandler(this.btnReadM_Click);
            // 
            // txtAddrM
            // 
            this.txtAddrM.Location = new System.Drawing.Point(7, 92);
            this.txtAddrM.Multiline = true;
            this.txtAddrM.Name = "txtAddrM";
            this.txtAddrM.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtAddrM.Size = new System.Drawing.Size(170, 53);
            this.txtAddrM.TabIndex = 5;
            // 
            // txtDataM
            // 
            this.txtDataM.Location = new System.Drawing.Point(7, 151);
            this.txtDataM.Multiline = true;
            this.txtDataM.Name = "txtDataM";
            this.txtDataM.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDataM.Size = new System.Drawing.Size(170, 53);
            this.txtDataM.TabIndex = 6;
            // 
            // btnWriteM
            // 
            this.btnWriteM.Location = new System.Drawing.Point(198, 151);
            this.btnWriteM.Name = "btnWriteM";
            this.btnWriteM.Size = new System.Drawing.Size(74, 26);
            this.btnWriteM.TabIndex = 7;
            this.btnWriteM.Text = "Write M";
            this.btnWriteM.UseVisualStyleBackColor = true;
            this.btnWriteM.Click += new System.EventHandler(this.btnWriteM_Click);
            // 
            // btnWriteD
            // 
            this.btnWriteD.Location = new System.Drawing.Point(198, 269);
            this.btnWriteD.Name = "btnWriteD";
            this.btnWriteD.Size = new System.Drawing.Size(74, 26);
            this.btnWriteD.TabIndex = 11;
            this.btnWriteD.Text = "Write D";
            this.btnWriteD.UseVisualStyleBackColor = true;
            this.btnWriteD.Click += new System.EventHandler(this.btnWriteD_Click);
            // 
            // txtDataD
            // 
            this.txtDataD.Location = new System.Drawing.Point(7, 269);
            this.txtDataD.Multiline = true;
            this.txtDataD.Name = "txtDataD";
            this.txtDataD.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDataD.Size = new System.Drawing.Size(170, 53);
            this.txtDataD.TabIndex = 10;
            // 
            // txtAddrD
            // 
            this.txtAddrD.Location = new System.Drawing.Point(7, 210);
            this.txtAddrD.Multiline = true;
            this.txtAddrD.Name = "txtAddrD";
            this.txtAddrD.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtAddrD.Size = new System.Drawing.Size(170, 53);
            this.txtAddrD.TabIndex = 9;
            // 
            // btnReadD
            // 
            this.btnReadD.Location = new System.Drawing.Point(198, 207);
            this.btnReadD.Name = "btnReadD";
            this.btnReadD.Size = new System.Drawing.Size(74, 26);
            this.btnReadD.TabIndex = 8;
            this.btnReadD.Text = "Read D";
            this.btnReadD.UseVisualStyleBackColor = true;
            this.btnReadD.Click += new System.EventHandler(this.btnReadD_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(17, 26);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(70, 43);
            this.button1.TabIndex = 12;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(355, 387);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnWriteD);
            this.Controls.Add(this.txtDataD);
            this.Controls.Add(this.txtAddrD);
            this.Controls.Add(this.btnReadD);
            this.Controls.Add(this.btnWriteM);
            this.Controls.Add(this.txtDataM);
            this.Controls.Add(this.txtAddrM);
            this.Controls.Add(this.btnReadM);
            this.Controls.Add(this.btnDisconnect);
            this.Controls.Add(this.txtConnection);
            this.Controls.Add(this.btnConnect);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.TextBox txtConnection;
        private System.Windows.Forms.Button btnDisconnect;
        private System.Windows.Forms.Button btnReadM;
        private System.Windows.Forms.TextBox txtAddrM;
        private System.Windows.Forms.TextBox txtDataM;
        private System.Windows.Forms.Button btnWriteM;
        private System.Windows.Forms.Button btnWriteD;
        private System.Windows.Forms.TextBox txtDataD;
        private System.Windows.Forms.TextBox txtAddrD;
        private System.Windows.Forms.Button btnReadD;
        private System.Windows.Forms.Button button1;
    }
}

