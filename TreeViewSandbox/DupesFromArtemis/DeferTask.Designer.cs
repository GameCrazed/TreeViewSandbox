namespace TreeViewSandbox.DupesFromArtemis
{
    partial class DeferTask
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.DueDate = new System.Windows.Forms.DateTimePicker();
            this.ResultBody = new System.Windows.Forms.RichTextBox();
            this.ResultTitle = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 6);
            this.label1.Margin = new System.Windows.Forms.Padding(6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Title";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 43);
            this.label2.Margin = new System.Windows.Forms.Padding(6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 25);
            this.label2.TabIndex = 1;
            this.label2.Text = "Body";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 215);
            this.label3.Margin = new System.Windows.Forms.Padding(6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 25);
            this.label3.TabIndex = 2;
            this.label3.Text = "Due";
            // 
            // DueDate
            // 
            this.DueDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DueDate.Location = new System.Drawing.Point(72, 209);
            this.DueDate.Name = "DueDate";
            this.DueDate.Size = new System.Drawing.Size(200, 33);
            this.DueDate.TabIndex = 3;
            // 
            // ResultBody
            // 
            this.ResultBody.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ResultBody.Location = new System.Drawing.Point(72, 40);
            this.ResultBody.Margin = new System.Windows.Forms.Padding(6);
            this.ResultBody.Name = "ResultBody";
            this.ResultBody.Size = new System.Drawing.Size(322, 160);
            this.ResultBody.TabIndex = 4;
            this.ResultBody.Text = "";
            // 
            // ResultTitle
            // 
            this.ResultTitle.AutoSize = true;
            this.ResultTitle.Location = new System.Drawing.Point(67, 6);
            this.ResultTitle.Name = "ResultTitle";
            this.ResultTitle.Size = new System.Drawing.Size(98, 25);
            this.ResultTitle.TabIndex = 5;
            this.ResultTitle.Text = "ResultTitle";
            // 
            // DeferTask
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.ResultTitle);
            this.Controls.Add(this.ResultBody);
            this.Controls.Add(this.DueDate);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Name = "DeferTask";
            this.Size = new System.Drawing.Size(400, 250);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.DateTimePicker DueDate;
        public System.Windows.Forms.RichTextBox ResultBody;
        public System.Windows.Forms.Label ResultTitle;
    }
}
