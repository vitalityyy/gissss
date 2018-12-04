namespace WindowsFormsApplication1
{
    partial class LabelFrm
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
            this.radio_weirao_point = new System.Windows.Forms.RadioButton();
            this.comboBox1_fields = new System.Windows.Forms.ComboBox();
            this.radio_horizontal = new System.Windows.Forms.RadioButton();
            this.radio_top_point = new System.Windows.Forms.RadioButton();
            this.radio_perpendcular = new System.Windows.Forms.RadioButton();
            this.radio_shuiping_polygon = new System.Windows.Forms.RadioButton();
            this.radio_curve = new System.Windows.Forms.RadioButton();
            this.radio_mixed_polygon = new System.Windows.Forms.RadioButton();
            this.radio_yaosu_polygon = new System.Windows.Forms.RadioButton();
            this.radio_angle_point = new System.Windows.Forms.RadioButton();
            this.radio_tongming = new System.Windows.Forms.RadioButton();
            this.radio_onlyone = new System.Windows.Forms.RadioButton();
            this.radio_part = new System.Windows.Forms.RadioButton();
            this.btn_style = new System.Windows.Forms.Button();
            this.LabelName = new System.Windows.Forms.RichTextBox();
            this.btn_ok = new System.Windows.Forms.Button();
            this.buttonclose = new System.Windows.Forms.Button();
            this.numeric_point = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numeric_point)).BeginInit();
            this.SuspendLayout();
            // 
            // radio_weirao_point
            // 
            this.radio_weirao_point.AutoSize = true;
            this.radio_weirao_point.Location = new System.Drawing.Point(37, 82);
            this.radio_weirao_point.Name = "radio_weirao_point";
            this.radio_weirao_point.Size = new System.Drawing.Size(55, 19);
            this.radio_weirao_point.TabIndex = 0;
            this.radio_weirao_point.TabStop = true;
            this.radio_weirao_point.Text = "围绕";
            // 
            // comboBox1_fields
            // 
            this.comboBox1_fields.FormattingEnabled = true;
            this.comboBox1_fields.Location = new System.Drawing.Point(37, 26);
            this.comboBox1_fields.Name = "comboBox1_fields";
            this.comboBox1_fields.Size = new System.Drawing.Size(176, 23);
            this.comboBox1_fields.TabIndex = 1;
            // 
            // radio_horizontal
            // 
            this.radio_horizontal.AutoSize = true;
            this.radio_horizontal.Location = new System.Drawing.Point(37, 123);
            this.radio_horizontal.Name = "radio_horizontal";
            this.radio_horizontal.Size = new System.Drawing.Size(55, 19);
            this.radio_horizontal.TabIndex = 0;
            this.radio_horizontal.TabStop = true;
            this.radio_horizontal.Text = "水平";
            this.radio_horizontal.UseVisualStyleBackColor = true;
            // 
            // radio_top_point
            // 
            this.radio_top_point.AutoSize = true;
            this.radio_top_point.Location = new System.Drawing.Point(194, 82);
            this.radio_top_point.Name = "radio_top_point";
            this.radio_top_point.Size = new System.Drawing.Size(55, 19);
            this.radio_top_point.TabIndex = 0;
            this.radio_top_point.TabStop = true;
            this.radio_top_point.Text = "点上";
            this.radio_top_point.UseVisualStyleBackColor = true;
            // 
            // radio_perpendcular
            // 
            this.radio_perpendcular.AutoSize = true;
            this.radio_perpendcular.Location = new System.Drawing.Point(194, 123);
            this.radio_perpendcular.Name = "radio_perpendcular";
            this.radio_perpendcular.Size = new System.Drawing.Size(55, 19);
            this.radio_perpendcular.TabIndex = 0;
            this.radio_perpendcular.TabStop = true;
            this.radio_perpendcular.Text = "垂直";
            this.radio_perpendcular.UseVisualStyleBackColor = true;
            // 
            // radio_shuiping_polygon
            // 
            this.radio_shuiping_polygon.AutoSize = true;
            this.radio_shuiping_polygon.Location = new System.Drawing.Point(37, 163);
            this.radio_shuiping_polygon.Name = "radio_shuiping_polygon";
            this.radio_shuiping_polygon.Size = new System.Drawing.Size(201, 19);
            this.radio_shuiping_polygon.TabIndex = 0;
            this.radio_shuiping_polygon.TabStop = true;
            this.radio_shuiping_polygon.Text = "radio_shuiping_polygon";
            this.radio_shuiping_polygon.UseVisualStyleBackColor = true;
            // 
            // radio_curve
            // 
            this.radio_curve.AutoSize = true;
            this.radio_curve.Location = new System.Drawing.Point(194, 163);
            this.radio_curve.Name = "radio_curve";
            this.radio_curve.Size = new System.Drawing.Size(55, 19);
            this.radio_curve.TabIndex = 0;
            this.radio_curve.TabStop = true;
            this.radio_curve.Text = "曲线";
            this.radio_curve.UseVisualStyleBackColor = true;
            // 
            // radio_mixed_polygon
            // 
            this.radio_mixed_polygon.AutoSize = true;
            this.radio_mixed_polygon.Location = new System.Drawing.Point(350, 82);
            this.radio_mixed_polygon.Name = "radio_mixed_polygon";
            this.radio_mixed_polygon.Size = new System.Drawing.Size(55, 19);
            this.radio_mixed_polygon.TabIndex = 0;
            this.radio_mixed_polygon.TabStop = true;
            this.radio_mixed_polygon.Text = "混合";
            this.radio_mixed_polygon.UseVisualStyleBackColor = true;
            // 
            // radio_yaosu_polygon
            // 
            this.radio_yaosu_polygon.AutoSize = true;
            this.radio_yaosu_polygon.Location = new System.Drawing.Point(350, 123);
            this.radio_yaosu_polygon.Name = "radio_yaosu_polygon";
            this.radio_yaosu_polygon.Size = new System.Drawing.Size(100, 19);
            this.radio_yaosu_polygon.TabIndex = 0;
            this.radio_yaosu_polygon.TabStop = true;
            this.radio_yaosu_polygon.Text = "按要素方向";
            this.radio_yaosu_polygon.UseVisualStyleBackColor = true;
            // 
            // radio_angle_point
            // 
            this.radio_angle_point.AutoSize = true;
            this.radio_angle_point.Location = new System.Drawing.Point(350, 163);
            this.radio_angle_point.Name = "radio_angle_point";
            this.radio_angle_point.Size = new System.Drawing.Size(55, 19);
            this.radio_angle_point.TabIndex = 0;
            this.radio_angle_point.TabStop = true;
            this.radio_angle_point.Text = "角度";
            this.radio_angle_point.UseVisualStyleBackColor = true;
            // 
            // radio_tongming
            // 
            this.radio_tongming.AutoSize = true;
            this.radio_tongming.Location = new System.Drawing.Point(37, 262);
            this.radio_tongming.Name = "radio_tongming";
            this.radio_tongming.Size = new System.Drawing.Size(115, 19);
            this.radio_tongming.TabIndex = 0;
            this.radio_tongming.TabStop = true;
            this.radio_tongming.Text = "移除同名标注";
            this.radio_tongming.UseVisualStyleBackColor = true;
            // 
            // radio_onlyone
            // 
            this.radio_onlyone.AutoSize = true;
            this.radio_onlyone.Location = new System.Drawing.Point(189, 262);
            this.radio_onlyone.Name = "radio_onlyone";
            this.radio_onlyone.Size = new System.Drawing.Size(145, 19);
            this.radio_onlyone.TabIndex = 0;
            this.radio_onlyone.TabStop = true;
            this.radio_onlyone.Text = "每个要素放置一个";
            this.radio_onlyone.UseVisualStyleBackColor = true;
            // 
            // radio_part
            // 
            this.radio_part.AutoSize = true;
            this.radio_part.Location = new System.Drawing.Point(348, 262);
            this.radio_part.Name = "radio_part";
            this.radio_part.Size = new System.Drawing.Size(145, 19);
            this.radio_part.TabIndex = 0;
            this.radio_part.TabStop = true;
            this.radio_part.Text = "每个要素放置部分";
            this.radio_part.UseVisualStyleBackColor = true;
            // 
            // btn_style
            // 
            this.btn_style.Location = new System.Drawing.Point(76, 358);
            this.btn_style.Name = "btn_style";
            this.btn_style.Size = new System.Drawing.Size(137, 42);
            this.btn_style.TabIndex = 2;
            this.btn_style.Text = "更改样式";
            this.btn_style.UseVisualStyleBackColor = true;
            this.btn_style.Click += new System.EventHandler(this.btn_style_Click);
            // 
            // LabelName
            // 
            this.LabelName.Location = new System.Drawing.Point(273, 358);
            this.LabelName.Name = "LabelName";
            this.LabelName.Size = new System.Drawing.Size(132, 42);
            this.LabelName.TabIndex = 3;
            this.LabelName.Text = "";
            // 
            // btn_ok
            // 
            this.btn_ok.Location = new System.Drawing.Point(163, 453);
            this.btn_ok.Name = "btn_ok";
            this.btn_ok.Size = new System.Drawing.Size(75, 23);
            this.btn_ok.TabIndex = 4;
            this.btn_ok.Text = "确定";
            this.btn_ok.UseVisualStyleBackColor = true;
            this.btn_ok.Click += new System.EventHandler(this.btn_ok_Click);
            // 
            // buttonclose
            // 
            this.buttonclose.Location = new System.Drawing.Point(330, 453);
            this.buttonclose.Name = "buttonclose";
            this.buttonclose.Size = new System.Drawing.Size(75, 23);
            this.buttonclose.TabIndex = 4;
            this.buttonclose.Text = "取消";
            this.buttonclose.UseVisualStyleBackColor = true;
            this.buttonclose.Click += new System.EventHandler(this.buttonclose_Click);
            // 
            // numeric_point
            // 
            this.numeric_point.Location = new System.Drawing.Point(411, 163);
            this.numeric_point.Name = "numeric_point";
            this.numeric_point.Size = new System.Drawing.Size(120, 25);
            this.numeric_point.TabIndex = 5;
            // 
            // LabelFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(562, 488);
            this.Controls.Add(this.numeric_point);
            this.Controls.Add(this.buttonclose);
            this.Controls.Add(this.btn_ok);
            this.Controls.Add(this.LabelName);
            this.Controls.Add(this.btn_style);
            this.Controls.Add(this.comboBox1_fields);
            this.Controls.Add(this.radio_part);
            this.Controls.Add(this.radio_onlyone);
            this.Controls.Add(this.radio_angle_point);
            this.Controls.Add(this.radio_curve);
            this.Controls.Add(this.radio_tongming);
            this.Controls.Add(this.radio_shuiping_polygon);
            this.Controls.Add(this.radio_perpendcular);
            this.Controls.Add(this.radio_yaosu_polygon);
            this.Controls.Add(this.radio_horizontal);
            this.Controls.Add(this.radio_mixed_polygon);
            this.Controls.Add(this.radio_top_point);
            this.Controls.Add(this.radio_weirao_point);
            this.Name = "LabelFrm";
            this.Text = "LabelFrm";
            this.Load += new System.EventHandler(this.LabelFrm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numeric_point)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton radio_weirao_point;
        private System.Windows.Forms.ComboBox comboBox1_fields;
        private System.Windows.Forms.RadioButton radio_horizontal;
        private System.Windows.Forms.RadioButton radio_top_point;
        private System.Windows.Forms.RadioButton radio_perpendcular;
        private System.Windows.Forms.RadioButton radio_shuiping_polygon;
        private System.Windows.Forms.RadioButton radio_curve;
        private System.Windows.Forms.RadioButton radio_mixed_polygon;
        private System.Windows.Forms.RadioButton radio_yaosu_polygon;
        private System.Windows.Forms.RadioButton radio_angle_point;
        private System.Windows.Forms.RadioButton radio_tongming;
        private System.Windows.Forms.RadioButton radio_onlyone;
        private System.Windows.Forms.RadioButton radio_part;
        private System.Windows.Forms.Button btn_style;
        private System.Windows.Forms.RichTextBox LabelName;
        private System.Windows.Forms.Button btn_ok;
        private System.Windows.Forms.Button buttonclose;
        private System.Windows.Forms.NumericUpDown numeric_point;
    }
}