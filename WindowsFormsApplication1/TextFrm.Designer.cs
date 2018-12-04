namespace WindowsFormsApplication1
{
    partial class TextFrm
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
            this.comboAccross = new System.Windows.Forms.ComboBox();
            this.comboVertical = new System.Windows.Forms.ComboBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // comboAccross
            // 
            this.comboAccross.FormattingEnabled = true;
            this.comboAccross.Location = new System.Drawing.Point(38, 65);
            this.comboAccross.Name = "comboAccross";
            this.comboAccross.Size = new System.Drawing.Size(121, 23);
            this.comboAccross.TabIndex = 0;
            // 
            // comboVertical
            // 
            this.comboVertical.FormattingEnabled = true;
            this.comboVertical.Location = new System.Drawing.Point(38, 139);
            this.comboVertical.Name = "comboVertical";
            this.comboVertical.Size = new System.Drawing.Size(121, 23);
            this.comboVertical.TabIndex = 0;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(267, 65);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(145, 126);
            this.richTextBox1.TabIndex = 1;
            this.richTextBox1.Text = "";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(173, 294);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // TextFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(460, 403);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.comboVertical);
            this.Controls.Add(this.comboAccross);
            this.Name = "TextFrm";
            this.Text = "TextFrm";
            this.Load += new System.EventHandler(this.TextFrm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox comboAccross;
        private System.Windows.Forms.ComboBox comboVertical;
        private System.Windows.Forms.Button button1;
        public System.Windows.Forms.RichTextBox richTextBox1;
    }
}