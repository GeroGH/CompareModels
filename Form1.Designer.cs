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
            this.DataGridView = new System.Windows.Forms.DataGridView();
            this.Compare = new System.Windows.Forms.Button();
            this.DeltaBox = new System.Windows.Forms.TextBox();
            this.DeltaLabel = new System.Windows.Forms.Label();
            this.Legend = new System.Windows.Forms.Label();
            this.GreenLabel = new System.Windows.Forms.Label();
            this.YellowLabel = new System.Windows.Forms.Label();
            this.RedLabel = new System.Windows.Forms.Label();
            this.BlueLabel = new System.Windows.Forms.Label();
            this.MagentaLabel = new System.Windows.Forms.Label();
            this.VersionLabel = new System.Windows.Forms.Label();
            this.buttonHighlightElements = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // Export
            // 
            this.Export.Location = new System.Drawing.Point(1030, 12);
            this.Export.Name = "Export";
            this.Export.Size = new System.Drawing.Size(237, 59);
            this.Export.TabIndex = 0;
            this.Export.Text = "Export Data";
            this.Export.UseVisualStyleBackColor = true;
            this.Export.Click += new System.EventHandler(this.Export_Click);
            // 
            // Import
            // 
            this.Import.Location = new System.Drawing.Point(1030, 85);
            this.Import.Name = "Import";
            this.Import.Size = new System.Drawing.Size(237, 59);
            this.Import.TabIndex = 1;
            this.Import.Text = "Import Data";
            this.Import.UseVisualStyleBackColor = true;
            this.Import.Click += new System.EventHandler(this.Import_Click);
            // 
            // DataGridView
            // 
            this.DataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridView.Location = new System.Drawing.Point(10, 14);
            this.DataGridView.Name = "DataGridView";
            this.DataGridView.Size = new System.Drawing.Size(1012, 425);
            this.DataGridView.TabIndex = 2;
            // 
            // Compare
            // 
            this.Compare.Location = new System.Drawing.Point(1030, 192);
            this.Compare.Name = "Compare";
            this.Compare.Size = new System.Drawing.Size(237, 59);
            this.Compare.TabIndex = 3;
            this.Compare.Text = "Compare";
            this.Compare.UseVisualStyleBackColor = true;
            this.Compare.Click += new System.EventHandler(this.Compare_Click);
            // 
            // DeltaBox
            // 
            this.DeltaBox.Location = new System.Drawing.Point(1160, 158);
            this.DeltaBox.Name = "DeltaBox";
            this.DeltaBox.Size = new System.Drawing.Size(107, 20);
            this.DeltaBox.TabIndex = 4;
            this.DeltaBox.Text = "100";
            this.DeltaBox.TextChanged += new System.EventHandler(this.Delta_TextChanged);
            // 
            // DeltaLabel
            // 
            this.DeltaLabel.AutoSize = true;
            this.DeltaLabel.Location = new System.Drawing.Point(1094, 161);
            this.DeltaLabel.Name = "DeltaLabel";
            this.DeltaLabel.Size = new System.Drawing.Size(60, 13);
            this.DeltaLabel.TabIndex = 5;
            this.DeltaLabel.Text = "Delta (mm):";
            // 
            // Legend
            // 
            this.Legend.AutoSize = true;
            this.Legend.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Legend.Location = new System.Drawing.Point(1068, 299);
            this.Legend.Name = "Legend";
            this.Legend.Size = new System.Drawing.Size(53, 13);
            this.Legend.TabIndex = 6;
            this.Legend.Text = "Legend:";
            // 
            // GreenLabel
            // 
            this.GreenLabel.AutoSize = true;
            this.GreenLabel.BackColor = System.Drawing.Color.LimeGreen;
            this.GreenLabel.Location = new System.Drawing.Point(1039, 339);
            this.GreenLabel.Name = "GreenLabel";
            this.GreenLabel.Size = new System.Drawing.Size(163, 13);
            this.GreenLabel.TabIndex = 7;
            this.GreenLabel.Text = "- Green (Beam Without Changes)";
            // 
            // YellowLabel
            // 
            this.YellowLabel.AutoSize = true;
            this.YellowLabel.BackColor = System.Drawing.Color.Yellow;
            this.YellowLabel.Location = new System.Drawing.Point(1039, 402);
            this.YellowLabel.Name = "YellowLabel";
            this.YellowLabel.Size = new System.Drawing.Size(150, 13);
            this.YellowLabel.TabIndex = 8;
            this.YellowLabel.Text = "- Yellow (Beam With Changes)";
            // 
            // RedLabel
            // 
            this.RedLabel.AutoSize = true;
            this.RedLabel.BackColor = System.Drawing.Color.Red;
            this.RedLabel.Location = new System.Drawing.Point(1039, 360);
            this.RedLabel.Name = "RedLabel";
            this.RedLabel.Size = new System.Drawing.Size(109, 13);
            this.RedLabel.TabIndex = 9;
            this.RedLabel.Text = "- Red (Deleted Beam)";
            // 
            // BlueLabel
            // 
            this.BlueLabel.AutoSize = true;
            this.BlueLabel.BackColor = System.Drawing.Color.DodgerBlue;
            this.BlueLabel.Location = new System.Drawing.Point(1039, 381);
            this.BlueLabel.Name = "BlueLabel";
            this.BlueLabel.Size = new System.Drawing.Size(104, 13);
            this.BlueLabel.TabIndex = 10;
            this.BlueLabel.Text = "- Blue (Added Beam)";
            // 
            // MagentaLabel
            // 
            this.MagentaLabel.AutoSize = true;
            this.MagentaLabel.BackColor = System.Drawing.Color.Magenta;
            this.MagentaLabel.Location = new System.Drawing.Point(1039, 423);
            this.MagentaLabel.Name = "MagentaLabel";
            this.MagentaLabel.Size = new System.Drawing.Size(167, 13);
            this.MagentaLabel.TabIndex = 11;
            this.MagentaLabel.Text = "- Magenta (Beam Changed Fields)";
            // 
            // VersionLabel
            // 
            this.VersionLabel.AutoSize = true;
            this.VersionLabel.Location = new System.Drawing.Point(1227, 423);
            this.VersionLabel.Name = "VersionLabel";
            this.VersionLabel.Size = new System.Drawing.Size(34, 13);
            this.VersionLabel.TabIndex = 12;
            this.VersionLabel.Text = "v2.21";
            // 
            // buttonHighlightElements
            // 
            this.buttonHighlightElements.Location = new System.Drawing.Point(1030, 265);
            this.buttonHighlightElements.Name = "buttonHighlightElements";
            this.buttonHighlightElements.Size = new System.Drawing.Size(237, 59);
            this.buttonHighlightElements.TabIndex = 13;
            this.buttonHighlightElements.Text = "Highlight Elements";
            this.buttonHighlightElements.UseVisualStyleBackColor = true;
            this.buttonHighlightElements.Click += new System.EventHandler(this.buttonHighlightElements_Click);
            // 
            // CompareTwoModels
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1277, 451);
            this.Controls.Add(this.buttonHighlightElements);
            this.Controls.Add(this.VersionLabel);
            this.Controls.Add(this.MagentaLabel);
            this.Controls.Add(this.BlueLabel);
            this.Controls.Add(this.RedLabel);
            this.Controls.Add(this.YellowLabel);
            this.Controls.Add(this.GreenLabel);
            this.Controls.Add(this.Legend);
            this.Controls.Add(this.DeltaLabel);
            this.Controls.Add(this.DeltaBox);
            this.Controls.Add(this.DataGridView);
            this.Controls.Add(this.Compare);
            this.Controls.Add(this.Import);
            this.Controls.Add(this.Export);
            this.Name = "CompareTwoModels";
            this.Text = "CompareTwoModels";
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Export;
        private System.Windows.Forms.Button Import;
        private System.Windows.Forms.DataGridView DataGridView;
        private System.Windows.Forms.Button Compare;
        private System.Windows.Forms.TextBox DeltaBox;
        private System.Windows.Forms.Label DeltaLabel;
        private System.Windows.Forms.Label Legend;
        private System.Windows.Forms.Label GreenLabel;
        private System.Windows.Forms.Label YellowLabel;
        private System.Windows.Forms.Label RedLabel;
        private System.Windows.Forms.Label BlueLabel;
        private System.Windows.Forms.Label MagentaLabel;
        private System.Windows.Forms.Label VersionLabel;
        private System.Windows.Forms.Button buttonHighlightElements;
    }
}

