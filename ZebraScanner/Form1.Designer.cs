namespace ZebraScanner
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
            this.button1 = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnTriggerOn = new System.Windows.Forms.Button();
            this.btnTriggerOff = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(0, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Open";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(0, 57);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnTriggerOn
            // 
            this.btnTriggerOn.Location = new System.Drawing.Point(107, 0);
            this.btnTriggerOn.Name = "btnTriggerOn";
            this.btnTriggerOn.Size = new System.Drawing.Size(75, 23);
            this.btnTriggerOn.TabIndex = 1;
            this.btnTriggerOn.Text = "TriggerOn";
            this.btnTriggerOn.UseVisualStyleBackColor = true;
            this.btnTriggerOn.Click += new System.EventHandler(this.btnTriggerOn_Click);
            // 
            // btnTriggerOff
            // 
            this.btnTriggerOff.Location = new System.Drawing.Point(107, 57);
            this.btnTriggerOff.Name = "btnTriggerOff";
            this.btnTriggerOff.Size = new System.Drawing.Size(75, 23);
            this.btnTriggerOff.TabIndex = 2;
            this.btnTriggerOff.Text = "TriggerOff";
            this.btnTriggerOff.UseVisualStyleBackColor = true;
            this.btnTriggerOff.Click += new System.EventHandler(this.btnTriggerOff_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.btnTriggerOff);
            this.Controls.Add(this.btnTriggerOn);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnTriggerOn;
        private System.Windows.Forms.Button btnTriggerOff;
    }
}

