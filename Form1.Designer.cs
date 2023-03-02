namespace CompareTwoModels
{
    partial class CompareTwoModels
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
            this.Export = new System.Windows.Forms.Button();
            this.Import = new System.Windows.Forms.Button();
            this.DataGrid = new System.Windows.Forms.DataGridView();
            this.Compare = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.DataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // Export
            // 
            this.Export.Location = new System.Drawing.Point(1128, 12);
            this.Export.Name = "Export";
            this.Export.Size = new System.Drawing.Size(264, 59);
            this.Export.TabIndex = 0;
            this.Export.Text = "Export Data";
            this.Export.UseVisualStyleBackColor = true;
            this.Export.Click += new System.EventHandler(this.Export_Click);
            // 
            // Import
            // 
            this.Import.Location = new System.Drawing.Point(1128, 77);
            this.Import.Name = "Import";
            this.Import.Size = new System.Drawing.Size(264, 59);
            this.Import.TabIndex = 1;
            this.Import.Text = "Import Data";
            this.Import.UseVisualStyleBackColor = true;
            this.Import.Click += new System.EventHandler(this.Import_Click);
            // 
            // dataGrid
            // 
            this.DataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGrid.Location = new System.Drawing.Point(13, 12);
            this.DataGrid.Name = "dataGrid";
            this.DataGrid.Size = new System.Drawing.Size(1109, 404);
            this.DataGrid.TabIndex = 2;
            // 
            // Compare
            // 
            this.Compare.Location = new System.Drawing.Point(1128, 142);
            this.Compare.Name = "Compare";
            this.Compare.Size = new System.Drawing.Size(264, 59);
            this.Compare.TabIndex = 3;
            this.Compare.Text = "Compare";
            this.Compare.UseVisualStyleBackColor = true;
            this.Compare.Click += new System.EventHandler(this.Compare_Click);
            // 
            // CompareTwoModels
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1404, 427);
            this.Controls.Add(this.DataGrid);
            this.Controls.Add(this.Compare);
            this.Controls.Add(this.Import);
            this.Controls.Add(this.Export);
            this.Name = "CompareTwoModels";
            this.Text = "CompareTwoModels";
            ((System.ComponentModel.ISupportInitialize)(this.DataGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Export;
        private System.Windows.Forms.Button Import;
        private System.Windows.Forms.DataGridView DataGrid;
        private System.Windows.Forms.Button Compare;
    }
}

