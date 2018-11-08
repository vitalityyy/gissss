namespace WindowsFormsApplication1
{
    partial class Eagle_eye
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Eagle_eye));
            this.MapC_OverView = new ESRI.ArcGIS.Controls.AxMapControl();
            ((System.ComponentModel.ISupportInitialize)(this.MapC_OverView)).BeginInit();
            this.SuspendLayout();
            // 
            // MapC_OverView
            // 
            this.MapC_OverView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MapC_OverView.Location = new System.Drawing.Point(0, 0);
            this.MapC_OverView.Name = "MapC_OverView";
            this.MapC_OverView.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("MapC_OverView.OcxState")));
            this.MapC_OverView.Size = new System.Drawing.Size(282, 254);
            this.MapC_OverView.TabIndex = 0;
            // 
            // Eagle_eye
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(282, 254);
            this.Controls.Add(this.MapC_OverView);
            this.Name = "Eagle_eye";
            this.Text = "Eagle_eye";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Eagle_eye_FormClosed);
            this.Load += new System.EventHandler(this.Eagle_eye_Load);
            ((System.ComponentModel.ISupportInitialize)(this.MapC_OverView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public ESRI.ArcGIS.Controls.AxMapControl MapC_OverView;
    }
    
}
