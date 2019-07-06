namespace ListViewTest
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
            this.btnUp = new System.Windows.Forms.Button();
            this.btnDown = new System.Windows.Forms.Button();
            this.lstViewLot = new System.Windows.Forms.ListView();
            this.colLotID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colTotal = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colInput = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colFinished = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colJobType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colRecipe = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colProduct = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colSegment = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colLotStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnUp
            // 
            this.btnUp.Location = new System.Drawing.Point(600, 23);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(99, 78);
            this.btnUp.TabIndex = 1;
            this.btnUp.Text = "Up";
            this.btnUp.UseVisualStyleBackColor = true;
            this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
            // 
            // btnDown
            // 
            this.btnDown.Location = new System.Drawing.Point(600, 190);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(99, 78);
            this.btnDown.TabIndex = 2;
            this.btnDown.Text = "Down";
            this.btnDown.UseVisualStyleBackColor = true;
            this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
            // 
            // lstViewLot
            // 
            this.lstViewLot.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colLotID,
            this.colTotal,
            this.colInput,
            this.colFinished,
            this.colJobType,
            this.colRecipe,
            this.colProduct,
            this.colSegment,
            this.colLotStatus});
            this.lstViewLot.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstViewLot.FullRowSelect = true;
            this.lstViewLot.GridLines = true;
            this.lstViewLot.Location = new System.Drawing.Point(8, 8);
            this.lstViewLot.MultiSelect = false;
            this.lstViewLot.Name = "lstViewLot";
            this.lstViewLot.ShowItemToolTips = true;
            this.lstViewLot.Size = new System.Drawing.Size(552, 278);
            this.lstViewLot.TabIndex = 3;
            this.lstViewLot.UseCompatibleStateImageBehavior = false;
            this.lstViewLot.View = System.Windows.Forms.View.Details;
            // 
            // colLotID
            // 
            this.colLotID.Tag = "LotID";
            this.colLotID.Text = "Lot ID";
            this.colLotID.Width = 113;
            // 
            // colTotal
            // 
            this.colTotal.Tag = "TotalCount";
            this.colTotal.Text = "Total";
            // 
            // colInput
            // 
            this.colInput.Tag = "ProcessingCount";
            this.colInput.Text = "Input";
            // 
            // colFinished
            // 
            this.colFinished.Tag = "FinishedCount";
            this.colFinished.Text = "Finished";
            // 
            // colJobType
            // 
            this.colJobType.Tag = "JobType";
            this.colJobType.Text = "Job Type";
            // 
            // colRecipe
            // 
            this.colRecipe.Tag = "Recipe";
            this.colRecipe.Text = "Recipe ID";
            // 
            // colProduct
            // 
            this.colProduct.Tag = "Product";
            this.colProduct.Text = "Product";
            // 
            // colSegment
            // 
            this.colSegment.Tag = "Segment";
            this.colSegment.Text = "Segement";
            // 
            // colLotStatus
            // 
            this.colLotStatus.Tag = "LotStatus";
            this.colLotStatus.Text = "Lot Status";
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(72, 307);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(131, 47);
            this.btnAdd.TabIndex = 4;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(288, 307);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(120, 46);
            this.btnRemove.TabIndex = 5;
            this.btnRemove.Text = "Remove";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.lstViewLot);
            this.Controls.Add(this.btnDown);
            this.Controls.Add(this.btnUp);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnUp;
        private System.Windows.Forms.Button btnDown;
        private System.Windows.Forms.ListView lstViewLot;
        private System.Windows.Forms.ColumnHeader colLotID;
        private System.Windows.Forms.ColumnHeader colTotal;
        private System.Windows.Forms.ColumnHeader colInput;
        private System.Windows.Forms.ColumnHeader colFinished;
        private System.Windows.Forms.ColumnHeader colJobType;
        private System.Windows.Forms.ColumnHeader colRecipe;
        private System.Windows.Forms.ColumnHeader colProduct;
        private System.Windows.Forms.ColumnHeader colSegment;
        private System.Windows.Forms.ColumnHeader colLotStatus;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnRemove;
    }
}

