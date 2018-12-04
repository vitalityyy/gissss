namespace WindowsFormsApplication1
{
    partial class UniqueValueSymbolFrm
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox_Layer = new System.Windows.Forms.ComboBox();
            this.comboBox_Fields = new System.Windows.Forms.ComboBox();
            this.Button_Ok = new System.Windows.Forms.Button();
            this.button_cancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(73, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "选择图层：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(73, 113);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 15);
            this.label2.TabIndex = 0;
            this.label2.Text = "选择字段：";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // comboBox_Layer
            // 
            this.comboBox_Layer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Layer.FormattingEnabled = true;
            this.comboBox_Layer.Location = new System.Drawing.Point(169, 35);
            this.comboBox_Layer.Name = "comboBox_Layer";
            this.comboBox_Layer.Size = new System.Drawing.Size(180, 23);
            this.comboBox_Layer.TabIndex = 1;
            // 
            // comboBox_Fields
            // 
            this.comboBox_Fields.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Fields.FormattingEnabled = true;
            this.comboBox_Fields.Location = new System.Drawing.Point(169, 105);
            this.comboBox_Fields.Name = "comboBox_Fields";
            this.comboBox_Fields.Size = new System.Drawing.Size(180, 23);
            this.comboBox_Fields.TabIndex = 1;
            // 
            // Button_Ok
            // 
            this.Button_Ok.Location = new System.Drawing.Point(274, 183);
            this.Button_Ok.Name = "Button_Ok";
            this.Button_Ok.Size = new System.Drawing.Size(75, 23);
            this.Button_Ok.TabIndex = 2;
            this.Button_Ok.Text = "确定";
            this.Button_Ok.UseVisualStyleBackColor = true;
            // 
            // button_cancel
            // 
            this.button_cancel.Location = new System.Drawing.Point(274, 232);
            this.button_cancel.Name = "button_cancel";
            this.button_cancel.Size = new System.Drawing.Size(75, 23);
            this.button_cancel.TabIndex = 2;
            this.button_cancel.Text = "取消";
            this.button_cancel.UseVisualStyleBackColor = true;
            // 
            // UniqueValueSymbolFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(463, 280);
            this.Controls.Add(this.button_cancel);
            this.Controls.Add(this.Button_Ok);
            this.Controls.Add(this.comboBox_Fields);
            this.Controls.Add(this.comboBox_Layer);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "UniqueValueSymbolFrm";
            this.Text = "UniqueValueSymbolFrm";
            this.Load += new System.EventHandler(this.UniqueValueSymbolFrm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox_Layer;
        private System.Windows.Forms.ComboBox comboBox_Fields;
        private System.Windows.Forms.Button Button_Ok;
        private System.Windows.Forms.Button button_cancel;
    }
}