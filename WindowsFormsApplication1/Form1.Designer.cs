namespace WindowsFormsApplication1
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.NewMapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenMapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveMapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveMapAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.操作ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ZoomInToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ZoomOutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MapPanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SelectByToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ClearSlelectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ClassZoomInToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ClassZoomOutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MapFullToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Eagle_eyeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.编辑ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DrawPolygonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.axToolbarControl1 = new ESRI.ArcGIS.Controls.AxToolbarControl();
            this.axTOCControl1 = new ESRI.ArcGIS.Controls.AxTOCControl();
            this.axMapControl1 = new ESRI.ArcGIS.Controls.AxMapControl();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.axLicenseControl1 = new ESRI.ArcGIS.Controls.AxLicenseControl();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.MessageLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.Blank = new System.Windows.Forms.ToolStripStatusLabel();
            this.ScaleLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.CoordinateLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.axLicenseControl2 = new ESRI.ArcGIS.Controls.AxLicenseControl();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.axPageLayoutControl1 = new ESRI.ArcGIS.Controls.AxPageLayoutControl();
            this.contextMenuStrip1_Layer = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.OpenAttributeTableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RemoveLayerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axToolbarControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axTOCControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axMapControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).BeginInit();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl2)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axPageLayoutControl1)).BeginInit();
            this.contextMenuStrip1_Layer.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.文件ToolStripMenuItem,
            this.操作ToolStripMenuItem,
            this.编辑ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(879, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 文件ToolStripMenuItem
            // 
            this.文件ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NewMapToolStripMenuItem,
            this.OpenMapToolStripMenuItem,
            this.SaveMapToolStripMenuItem,
            this.SaveMapAsToolStripMenuItem});
            this.文件ToolStripMenuItem.Name = "文件ToolStripMenuItem";
            this.文件ToolStripMenuItem.Size = new System.Drawing.Size(69, 24);
            this.文件ToolStripMenuItem.Text = "文件(&F)";
            // 
            // NewMapToolStripMenuItem
            // 
            this.NewMapToolStripMenuItem.Name = "NewMapToolStripMenuItem";
            this.NewMapToolStripMenuItem.Size = new System.Drawing.Size(136, 26);
            this.NewMapToolStripMenuItem.Text = "新建(&V)";
            this.NewMapToolStripMenuItem.Click += new System.EventHandler(this.NewMapToolStripMenuItem_Click_1);
            // 
            // OpenMapToolStripMenuItem
            // 
            this.OpenMapToolStripMenuItem.Name = "OpenMapToolStripMenuItem";
            this.OpenMapToolStripMenuItem.Size = new System.Drawing.Size(136, 26);
            this.OpenMapToolStripMenuItem.Text = "打开(&O)";
            this.OpenMapToolStripMenuItem.Click += new System.EventHandler(this.OpenMapToolStripMenuItem_Click);
            // 
            // SaveMapToolStripMenuItem
            // 
            this.SaveMapToolStripMenuItem.Name = "SaveMapToolStripMenuItem";
            this.SaveMapToolStripMenuItem.Size = new System.Drawing.Size(136, 26);
            this.SaveMapToolStripMenuItem.Text = "保存(&S)";
            this.SaveMapToolStripMenuItem.Click += new System.EventHandler(this.SaveMapToolStripMenuItem_Click);
            // 
            // SaveMapAsToolStripMenuItem
            // 
            this.SaveMapAsToolStripMenuItem.Name = "SaveMapAsToolStripMenuItem";
            this.SaveMapAsToolStripMenuItem.Size = new System.Drawing.Size(136, 26);
            this.SaveMapAsToolStripMenuItem.Text = "另存()";
            this.SaveMapAsToolStripMenuItem.Click += new System.EventHandler(this.SaveMapAsToolStripMenuItem_Click);
            // 
            // 操作ToolStripMenuItem
            // 
            this.操作ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ZoomInToolStripMenuItem,
            this.ZoomOutToolStripMenuItem,
            this.MapPanToolStripMenuItem,
            this.SelectByToolStripMenuItem,
            this.ClearSlelectionToolStripMenuItem,
            this.ClassZoomInToolStripMenuItem,
            this.ClassZoomOutToolStripMenuItem,
            this.MapFullToolStripMenuItem,
            this.Eagle_eyeToolStripMenuItem});
            this.操作ToolStripMenuItem.Name = "操作ToolStripMenuItem";
            this.操作ToolStripMenuItem.Size = new System.Drawing.Size(51, 24);
            this.操作ToolStripMenuItem.Text = "操作";
            this.操作ToolStripMenuItem.Click += new System.EventHandler(this.操作ToolStripMenuItem_Click);
            // 
            // ZoomInToolStripMenuItem
            // 
            this.ZoomInToolStripMenuItem.Name = "ZoomInToolStripMenuItem";
            this.ZoomInToolStripMenuItem.Size = new System.Drawing.Size(159, 26);
            this.ZoomInToolStripMenuItem.Text = "放大";
            this.ZoomInToolStripMenuItem.Click += new System.EventHandler(this.ZoomInToolStripMenuItem_Click);
            // 
            // ZoomOutToolStripMenuItem
            // 
            this.ZoomOutToolStripMenuItem.Name = "ZoomOutToolStripMenuItem";
            this.ZoomOutToolStripMenuItem.Size = new System.Drawing.Size(159, 26);
            this.ZoomOutToolStripMenuItem.Text = "缩小";
            this.ZoomOutToolStripMenuItem.Click += new System.EventHandler(this.ZoomOutToolStripMenuItem_Click);
            // 
            // MapPanToolStripMenuItem
            // 
            this.MapPanToolStripMenuItem.Name = "MapPanToolStripMenuItem";
            this.MapPanToolStripMenuItem.Size = new System.Drawing.Size(159, 26);
            this.MapPanToolStripMenuItem.Text = "移动";
            this.MapPanToolStripMenuItem.Click += new System.EventHandler(this.MapPanToolStripMenuItem_Click);
            // 
            // SelectByToolStripMenuItem
            // 
            this.SelectByToolStripMenuItem.Name = "SelectByToolStripMenuItem";
            this.SelectByToolStripMenuItem.Size = new System.Drawing.Size(159, 26);
            this.SelectByToolStripMenuItem.Text = "选择";
            this.SelectByToolStripMenuItem.Click += new System.EventHandler(this.SelectByToolStripMenuItem_Click);
            // 
            // ClearSlelectionToolStripMenuItem
            // 
            this.ClearSlelectionToolStripMenuItem.Name = "ClearSlelectionToolStripMenuItem";
            this.ClearSlelectionToolStripMenuItem.Size = new System.Drawing.Size(159, 26);
            this.ClearSlelectionToolStripMenuItem.Text = "清除";
            this.ClearSlelectionToolStripMenuItem.Click += new System.EventHandler(this.ClearSlelectionToolStripMenuItem_Click);
            // 
            // ClassZoomInToolStripMenuItem
            // 
            this.ClassZoomInToolStripMenuItem.Name = "ClassZoomInToolStripMenuItem";
            this.ClassZoomInToolStripMenuItem.Size = new System.Drawing.Size(159, 26);
            this.ClassZoomInToolStripMenuItem.Text = "调用类放大";
            this.ClassZoomInToolStripMenuItem.Click += new System.EventHandler(this.ClassZoomInToolStripMenuItem_Click);
            // 
            // ClassZoomOutToolStripMenuItem
            // 
            this.ClassZoomOutToolStripMenuItem.Name = "ClassZoomOutToolStripMenuItem";
            this.ClassZoomOutToolStripMenuItem.Size = new System.Drawing.Size(159, 26);
            this.ClassZoomOutToolStripMenuItem.Text = "调用类缩小";
            this.ClassZoomOutToolStripMenuItem.Click += new System.EventHandler(this.ClassZoomOutToolStripMenuItem_Click);
            // 
            // MapFullToolStripMenuItem
            // 
            this.MapFullToolStripMenuItem.Name = "MapFullToolStripMenuItem";
            this.MapFullToolStripMenuItem.Size = new System.Drawing.Size(159, 26);
            this.MapFullToolStripMenuItem.Text = "调用类全图";
            this.MapFullToolStripMenuItem.Click += new System.EventHandler(this.MapFullToolStripMenuItem_Click);
            // 
            // Eagle_eyeToolStripMenuItem
            // 
            this.Eagle_eyeToolStripMenuItem.Name = "Eagle_eyeToolStripMenuItem";
            this.Eagle_eyeToolStripMenuItem.Size = new System.Drawing.Size(159, 26);
            this.Eagle_eyeToolStripMenuItem.Text = "鹰眼视图";
            this.Eagle_eyeToolStripMenuItem.Click += new System.EventHandler(this.Eagle_eyeToolStripMenuItem_Click);
            // 
            // 编辑ToolStripMenuItem
            // 
            this.编辑ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DrawPolygonToolStripMenuItem});
            this.编辑ToolStripMenuItem.Name = "编辑ToolStripMenuItem";
            this.编辑ToolStripMenuItem.Size = new System.Drawing.Size(51, 24);
            this.编辑ToolStripMenuItem.Text = "编辑";
            // 
            // DrawPolygonToolStripMenuItem
            // 
            this.DrawPolygonToolStripMenuItem.Name = "DrawPolygonToolStripMenuItem";
            this.DrawPolygonToolStripMenuItem.Size = new System.Drawing.Size(159, 26);
            this.DrawPolygonToolStripMenuItem.Text = "绘制多边形";
            this.DrawPolygonToolStripMenuItem.Click += new System.EventHandler(this.DrawPolygonToolStripMenuItem_Click);
            // 
            // axToolbarControl1
            // 
            this.axToolbarControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.axToolbarControl1.Location = new System.Drawing.Point(0, 28);
            this.axToolbarControl1.Name = "axToolbarControl1";
            this.axToolbarControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axToolbarControl1.OcxState")));
            this.axToolbarControl1.Size = new System.Drawing.Size(879, 28);
            this.axToolbarControl1.TabIndex = 1;
            this.axToolbarControl1.OnMouseDown += new ESRI.ArcGIS.Controls.IToolbarControlEvents_Ax_OnMouseDownEventHandler(this.axToolbarControl1_OnMouseDown);
            this.axToolbarControl1.OnMouseMove += new ESRI.ArcGIS.Controls.IToolbarControlEvents_Ax_OnMouseMoveEventHandler(this.axToolbarControl1_OnMouseMove);
            // 
            // axTOCControl1
            // 
            this.axTOCControl1.Dock = System.Windows.Forms.DockStyle.Left;
            this.axTOCControl1.Location = new System.Drawing.Point(0, 56);
            this.axTOCControl1.Name = "axTOCControl1";
            this.axTOCControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axTOCControl1.OcxState")));
            this.axTOCControl1.Size = new System.Drawing.Size(172, 407);
            this.axTOCControl1.TabIndex = 2;
            this.axTOCControl1.OnMouseDown += new ESRI.ArcGIS.Controls.ITOCControlEvents_Ax_OnMouseDownEventHandler(this.axTOCControl1_OnMouseDown);
            // 
            // axMapControl1
            // 
            this.axMapControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axMapControl1.Location = new System.Drawing.Point(3, 3);
            this.axMapControl1.Name = "axMapControl1";
            this.axMapControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axMapControl1.OcxState")));
            this.axMapControl1.Size = new System.Drawing.Size(693, 372);
            this.axMapControl1.TabIndex = 3;
            this.axMapControl1.OnMouseDown += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnMouseDownEventHandler(this.axMapControl1_OnMouseDown);
            this.axMapControl1.OnMouseMove += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnMouseMoveEventHandler(this.axMapControl1_OnMouseMove);
            this.axMapControl1.OnViewRefreshed += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnViewRefreshedEventHandler(this.axMapControl1_OnViewRefreshed);
            this.axMapControl1.OnAfterScreenDraw += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnAfterScreenDrawEventHandler(this.axMapControl1_OnAfterScreenDraw);
            this.axMapControl1.OnExtentUpdated += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnExtentUpdatedEventHandler(this.axMapControl1_OnExtentUpdated);
            this.axMapControl1.OnMapReplaced += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnMapReplacedEventHandler(this.axMapControl1_OnMapReplaced);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // axLicenseControl1
            // 
            this.axLicenseControl1.Enabled = true;
            this.axLicenseControl1.Location = new System.Drawing.Point(178, 62);
            this.axLicenseControl1.Name = "axLicenseControl1";
            this.axLicenseControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axLicenseControl1.OcxState")));
            this.axLicenseControl1.Size = new System.Drawing.Size(32, 32);
            this.axLicenseControl1.TabIndex = 4;
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MessageLabel,
            this.Blank,
            this.ScaleLabel,
            this.CoordinateLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 463);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(879, 25);
            this.statusStrip1.TabIndex = 5;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // MessageLabel
            // 
            this.MessageLabel.Name = "MessageLabel";
            this.MessageLabel.Size = new System.Drawing.Size(129, 20);
            this.MessageLabel.Text = "当前所用工具信息";
            // 
            // Blank
            // 
            this.Blank.Name = "Blank";
            this.Blank.Size = new System.Drawing.Size(39, 20);
            this.Blank.Text = "占位";
            // 
            // ScaleLabel
            // 
            this.ScaleLabel.Name = "ScaleLabel";
            this.ScaleLabel.Size = new System.Drawing.Size(54, 20);
            this.ScaleLabel.Text = "比例尺";
            // 
            // CoordinateLabel
            // 
            this.CoordinateLabel.Name = "CoordinateLabel";
            this.CoordinateLabel.Size = new System.Drawing.Size(69, 20);
            this.CoordinateLabel.Text = "当前坐标";
            // 
            // axLicenseControl2
            // 
            this.axLicenseControl2.Enabled = true;
            this.axLicenseControl2.Location = new System.Drawing.Point(31, 54);
            this.axLicenseControl2.Name = "axLicenseControl2";
            this.axLicenseControl2.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axLicenseControl2.OcxState")));
            this.axLicenseControl2.Size = new System.Drawing.Size(32, 32);
            this.axLicenseControl2.TabIndex = 6;
            this.axLicenseControl2.Enter += new System.EventHandler(this.axLicenseControl2_Enter);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(172, 56);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(707, 407);
            this.tabControl1.TabIndex = 7;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.axLicenseControl2);
            this.tabPage1.Controls.Add(this.axMapControl1);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(699, 378);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "数据视图";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.axPageLayoutControl1);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(699, 378);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "布局视图";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // axPageLayoutControl1
            // 
            this.axPageLayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axPageLayoutControl1.Location = new System.Drawing.Point(3, 3);
            this.axPageLayoutControl1.Name = "axPageLayoutControl1";
            this.axPageLayoutControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axPageLayoutControl1.OcxState")));
            this.axPageLayoutControl1.Size = new System.Drawing.Size(693, 372);
            this.axPageLayoutControl1.TabIndex = 0;
            // 
            // contextMenuStrip1_Layer
            // 
            this.contextMenuStrip1_Layer.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1_Layer.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenAttributeTableToolStripMenuItem,
            this.RemoveLayerToolStripMenuItem});
            this.contextMenuStrip1_Layer.Name = "contextMenuStrip1_Layer";
            this.contextMenuStrip1_Layer.Size = new System.Drawing.Size(184, 52);
            // 
            // OpenAttributeTableToolStripMenuItem
            // 
            this.OpenAttributeTableToolStripMenuItem.Name = "OpenAttributeTableToolStripMenuItem";
            this.OpenAttributeTableToolStripMenuItem.Size = new System.Drawing.Size(183, 24);
            this.OpenAttributeTableToolStripMenuItem.Text = "打开图层属性表";
            this.OpenAttributeTableToolStripMenuItem.Click += new System.EventHandler(this.OpenAttributeTableToolStripMenuItem_Click);
            // 
            // RemoveLayerToolStripMenuItem
            // 
            this.RemoveLayerToolStripMenuItem.Name = "RemoveLayerToolStripMenuItem";
            this.RemoveLayerToolStripMenuItem.Size = new System.Drawing.Size(183, 24);
            this.RemoveLayerToolStripMenuItem.Text = "移除当前图层";
            this.RemoveLayerToolStripMenuItem.Click += new System.EventHandler(this.RemoveLayerToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(879, 488);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.axTOCControl1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.axLicenseControl1);
            this.Controls.Add(this.axToolbarControl1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axToolbarControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axTOCControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axMapControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl2)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axPageLayoutControl1)).EndInit();
            this.contextMenuStrip1_Layer.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 文件ToolStripMenuItem;
        private ESRI.ArcGIS.Controls.AxToolbarControl axToolbarControl1;
        private ESRI.ArcGIS.Controls.AxTOCControl axTOCControl1;
        private ESRI.ArcGIS.Controls.AxMapControl axMapControl1;
        private System.Windows.Forms.ToolStripMenuItem OpenMapToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem SaveMapToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem NewMapToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SaveMapAsToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem 操作ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ZoomInToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ZoomOutToolStripMenuItem;
        private ESRI.ArcGIS.Controls.AxLicenseControl axLicenseControl1;
        private System.Windows.Forms.ToolStripMenuItem MapPanToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private ESRI.ArcGIS.Controls.AxLicenseControl axLicenseControl2;
        private System.Windows.Forms.ToolStripMenuItem 编辑ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DrawPolygonToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SelectByToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ClearSlelectionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ClassZoomInToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ClassZoomOutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MapFullToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem Eagle_eyeToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel MessageLabel;
        private System.Windows.Forms.ToolStripStatusLabel Blank;
        private System.Windows.Forms.ToolStripStatusLabel ScaleLabel;
        private System.Windows.Forms.ToolStripStatusLabel CoordinateLabel;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private ESRI.ArcGIS.Controls.AxPageLayoutControl axPageLayoutControl1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1_Layer;
        private System.Windows.Forms.ToolStripMenuItem OpenAttributeTableToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem RemoveLayerToolStripMenuItem;
    }
}

