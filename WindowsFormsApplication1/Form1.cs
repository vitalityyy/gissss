using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.SystemUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        private IMapControlDefault pmapC;
        private string currentT;

        public static bool overview = false;
        Eagle_eye frm_overview = null;

        IToolbarMenu m_TocLayerMenu = new ToolbarMenuClass();
        IToolbarMenu m_TocMapMenu = new ToolbarMenuClass();

        esriTOCControlItem itemType = esriTOCControlItem.esriTOCControlItemNone;
        IBasicMap basicMap = null;
        ILayer layer = null;
        object unk = null;
        object data = null;

        public Form1()
        {
            InitializeComponent();
        }



        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "hehehe&arcgis";
            pmapC = axMapControl1.Object as IMapControlDefault;                 //接口转接
            m_TocMapMenu.AddItem(new ControlsAddDataCommandClass(), 0, -1, false, esriCommandStyles.esriCommandStyleIconAndText);
            m_TocMapMenu.SetHook(axMapControl1);
        }

        private void OpenMapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //  OpenFileDialog OpenFileDialog = new OpenFileDialog();

            this.openFileDialog1.Title = "选择地图文件";
            this.openFileDialog1.Filter = "MXD地图文件|*.mxd";
            this.openFileDialog1.FileName = "";
            this.openFileDialog1.Multiselect = false;
            this.openFileDialog1.RestoreDirectory = true;

            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string docName = this.openFileDialog1.FileName;
                //法一
                // axMapControl1.LoadMxFile(docName);
                //效果相同
                //(axMapControl1.Object as IMapControlDefault).LoadMxFile(docName);
                //方法二

                IMapDocument pMapDocument = new MapDocumentClass();
                pMapDocument.Open(docName, null);
                axMapControl1.Map = pMapDocument.get_Map(0);
                axMapControl1.DocumentFilename = docName;//不可少
                pMapDocument.Close();


            }


        }                              //打开地图文档

        private void SaveMapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IMapDocument pmd = new MapDocumentClass();
            if (pmapC.LayerCount == 0)//如果没有图层不用保存
                return;
            if (pmapC.DocumentFilename != null)//已经加载过数据
            {
                pmd.Open(pmapC.DocumentFilename);
                pmd.ReplaceContents(pmapC as IMxdContents);//用新的内容替换原有的内容（当前内容发生了改变
                pmd.Save(true, true);//相对路径缩略图
                pmd.Close();//必须添加 否则锁定冲突
                MessageBox.Show("保存成功", "提示");

            }

            else//相当于添加数据 而不是添加地图文档
                SaveMapAsToolStripMenuItem_Click(null, null);

        }                               //保存地图

        private void NewMapToolStripMenuItem_Click_1(object sender, EventArgs e)
        {

            IMap map = new MapClass();
            map.Name = "map";
            this.pmapC.Map = map;
            pmapC.DocumentFilename = null;
            axMapControl1.Refresh();
            axTOCControl1.Update();

        }                               //新建地图

        private void SaveMapAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pmapC.LayerCount == 0)
                return;
            this.saveFileDialog1.Filter = "MXD地图文档(*.mxd)|*.mxd";
            this.saveFileDialog1.Title = "另存地图文档";

            if (this.saveFileDialog1.ShowDialog() == DialogResult.OK)//不可缺少
            {
                IMapDocument pMapDocument = new MapDocumentClass();
                pMapDocument.New(this.saveFileDialog1.FileName);//用给定名字新建一个mxd文档
                pMapDocument.ReplaceContents((IMxdContents)this.pmapC);
                pMapDocument.Save(true, true);
                pmapC.DocumentFilename = this.saveFileDialog1.FileName;
                pMapDocument.Close();
                MessageBox.Show("另存成功", "提示");

            }

        }                             //另存地图

        private void ZoomInToolStripMenuItem_Click(object sender, EventArgs e)
        {
            axMapControl1.CurrentTool = null;
            currentT = "track_zoomin";
            axMapControl1.MousePointer = esriControlsMousePointer.esriPointerZoomIn; //改变鼠标样式
        }                                 //放大地图

        private void axMapControl1_OnMouseDown(object sender, IMapControlEvents2_OnMouseDownEvent e)
        {
            if (currentT == "track_zoomin")                                     //放大
            {//改变地图控件显示范围为当前拖拽的区域
                axMapControl1.Extent = axMapControl1.TrackRectangle();
                //刷新地图
                axMapControl1.Refresh(esriViewDrawPhase.esriViewGeography, null, null);
            }
            else if (currentT == "track_MapPan")                                 //平移
            {
                axMapControl1.Pan();  //平移地图
            }
            else if (currentT == "track_zoomout")                                //缩小
            {
                axMapControl1.CurrentTool = null;
                IEnvelope pEnvelop_track = axMapControl1.TrackRectangle();
                IEnvelope pEnvelop_current = axMapControl1.Extent;
                if (pEnvelop_track.IsEmpty)
                    return;
                Double magnification = (pEnvelop_current.Width / pEnvelop_track.Height
                    + pEnvelop_current.Height / pEnvelop_track.Height) / 2;

                pEnvelop_current.Expand(magnification, magnification, true);
                axMapControl1.Extent = pEnvelop_current;

                double center_x = (pEnvelop_track.XMax + pEnvelop_track.XMin) / 2;
                double center_y = (pEnvelop_track.YMax + pEnvelop_track.YMin) / 2;
                IPoint ppoint = new PointClass();
                ppoint.PutCoords(center_x, center_y);
                axMapControl1.CenterAt(ppoint);//将中心定位于拉框中心
            }
            else if (currentT == "Draw_polygon")
            {//产生拖拽多边形
             //IGeometry pGeom = axMapControl1.TrackPolygon();
             //  DrawMapShape(pGeom);
             //刷新地图
             // axMapControl1.Refresh(esriViewDrawPhase.esriViewGeography, null, null);
             //绘制临时多边形
             //边线样式
             //ISimpleLineSymbol psl = new SimpleLineSymbolClass();
             //IRgbColor rgbColor = new RgbColorClass();
             //rgbColor.Blue = 255;
             //psl.Color = rgbColor;
             //psl.Width = 1;
             //psl.Style = esriSimpleLineStyle.esriSLSDash;
             //2、绘制驻留多边形
                IGeometry pGeo = axMapControl1.TrackPolygon();

                //设置元素的几何形状
                IElement pElement = new PolygonElementClass();
                pElement.Geometry = pGeo;

                //填充元素的填充样式
                IRgbColor rgbColor = new RgbColorClass();
                rgbColor.Red = 255;
                ISimpleFillSymbol fillSymbol = new SimpleFillSymbolClass();

                fillSymbol.Color = rgbColor;

                IFillShapeElement pFillelement = pElement as IFillShapeElement;
                pFillelement.Symbol = fillSymbol;
                (axMapControl1.Map as IGraphicsContainer).AddElement(pElement, 0);
                axMapControl1.Refresh(esriViewDrawPhase.esriViewGraphics, null, axMapControl1.Extent);
            }
            else if (currentT == "track_select")                                    //选择地图要素
            {
                //产生多边形
                IGeometry pGeom = axMapControl1.TrackPolygon();
                axMapControl1.Map.SelectByShape(pGeom, null, false);
                //刷新地图
                axMapControl1.Refresh(esriViewDrawPhase.esriViewGeography, null, null);
            }

        }            //鼠标在地图上发生事件
        private void DrawMapShape(IGeometry pGeom)
        {
            IRgbColor pColor = new RgbColorClass();
            pColor.Red = 220;
            pColor.Green = 112;
            pColor.Blue = 60;
            //新建一个绘制图形的填充符号
            ISimpleFillSymbol pFillsyl = new SimpleFillSymbolClass();
            pFillsyl.Color = pColor;
            object oFillsyl = pFillsyl;
            axMapControl1.DrawShape(pGeom, ref oFillsyl);
        }    //drawshape 和 drawtext这两种绘制方法绘制图形都是缓存不能真正保存一旦窗口重绘图形就会消失
        private void axTOCControl1_OnMouseDown(object sender, ITOCControlEvents_OnMouseDownEvent e)
        {
            //esriTOCControlItem itemType = esriTOCControlItem.esriTOCControlItemNone;
            //IBasicMap basicMap = null;
            //ILayer layer = null;
            //object unk = null;
            //object data = null;


            this.axTOCControl1.HitTest(e.x, e.y, ref itemType, ref basicMap, ref layer, ref unk, ref data);
            if (e.button == 1)
            {
                //左键修改图例
                /* if (layer == null) return;
                 IFeatureLayer featureLayer = layer as IFeatureLayer;
                 if (featureLayer == null) return;
                 IGeoFeatureLayer geoFeatureLayer = (IGeoFeatureLayer)featureLayer;
                 ILegendClass legendClass = new LegendClass();
                 ISymbol symbol = null;
                 if(unk is ILegendGroup&&(int)data!=-1)
                 {
                     legendClass = ((ILegendGroup)unk).get_Class((int)data);
                     symbol = legendClass.Symbol;
                 }
                 if (symbol == null) return;
                 symbol = GetSymbolByControl(symbol);//弹出符号选择对话框让用户选择新的符号
                 if (symbol == null) return;
                 legendClass.Symbol = symbol;
                 this.Activate();
                 axMapControl1.ActiveView.ContentsChanged();
                 axMapControl1.Refresh(esriViewDrawPhase.esriViewGeography, null, null);
                 axTOCControl1.Update();*/

            }

            if (e.button == 2)
            {//右键
                if (itemType == esriTOCControlItem.esriTOCControlItemMap)
                {
                    m_TocMapMenu.PopupMenu(e.x, e.y, axTOCControl1.hWnd);
                }

                if (itemType == esriTOCControlItem.esriTOCControlItemLayer)
                {
                    //this.TOCRightlayer = layer;
                    this.contextMenuStrip1_Layer.Show(this.axTOCControl1, e.x, e.y);//弹出相应的右键菜单
                }
            }
        }

        /* private ISymbol GetSymbolByControl(ISymbol symbolType)
         {
             ////throw new NotImplementedException();
             ISymbol symbol = null;
             IStyleGalleryItem styleGalleryItem = null;
             esriSymbologyStyleClass styleClass = esriSymbologyStyleClass.esriStyleClassMarkerSymbols;
             if (symbolType is IMarkerSymbol)
             { styleClass = esriSymbologyStyleClass.esriStyleClassMarkerSymbols; }
             if (symbolType is ILineSymbol)
             { styleClass = esriSymbologyStyleClass.esriStyleClassMarkerSymbols; }
             if (symbolType is IFillSymbol)
             { styleClass = esriSymbologyStyleClass.esriStyleClassMarkerSymbols; }
             GetSymbolByControlForm symbolForm = new GetSymbolByControlForm(styleClass);
             symbolForm.ShowDialog();
             styleGalleryItem = symbolForm.m_styleGalleryItem;
             if (styleGalleryItem == null) return null;
             symbol = styleGalleryItem.Item as ISymbol;
             symbolForm.Dispose();
             this.Activate();
             return symbol;

         }*/   //符号选择

        private void ZoomOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            axMapControl1.CurrentTool = null;
            currentT = "track_zoomout";
            axMapControl1.MousePointer = esriControlsMousePointer.esriPointerZoomOut;
        }                           //缩小地图

        private void MapPanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            axMapControl1.CurrentTool = null;
            currentT = "track_MapPan";
            axMapControl1.MousePointer = esriControlsMousePointer.esriPointerPagePan;
        }                           //移动地图

        private void axLicenseControl2_Enter(object sender, EventArgs e)
        {

        }

        private void DrawPolygonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            axMapControl1.CurrentTool = null;
            currentT = "Draw_polygon";
            axMapControl1.MousePointer = esriControlsMousePointer.esriPointerPencil;
        }                   //绘画多边形

        private void SelectByToolStripMenuItem_Click(object sender, EventArgs e)
        {
            axMapControl1.CurrentTool = null;
            currentT = "track_select";
            axMapControl1.MousePointer = esriControlsMousePointer.esriPointerCrosshair;
        }                         //选择地图要素

        private void ClearSlelectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            axMapControl1.MousePointer = esriControlsMousePointer.esriPointerDefault;
            axMapControl1.CurrentTool = null;
            currentT = "";
            axMapControl1.Map.ClearSelection();//清除地图选集
            axMapControl1.Refresh(esriViewDrawPhase.esriViewGeography, null, axMapControl1.Extent);

        }                  //清除选择的地图要素

        private void ClassZoomInToolStripMenuItem_Click(object sender, EventArgs e)
        {
            axMapControl1.CurrentTool = null;
            currentT = "";
            //  axMapControl1.MousePointer = esriControlsMousePointer.esriPointerZoomIn;
            ESRI.ArcGIS.SystemUI.ICommand pCommand = new ControlsMapZoomInToolClass();
            pCommand.OnCreate(pmapC);
            {
                if (pCommand.Enabled == true)
                {
                    pmapC.CurrentTool = (ITool)pCommand;

                }
            }
        }                     //调用类放大

        private void ClassZoomOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            axMapControl1.CurrentTool = null;
            currentT = "";
            //  axMapControl1.MousePointer = esriControlsMousePointer.esriPointerZoomIn;
            ESRI.ArcGIS.SystemUI.ICommand pCommand = new ControlsMapZoomOutToolClass();
            pCommand.OnCreate(pmapC);
            {
                if (pCommand.Enabled == true)
                {
                    pmapC.CurrentTool = (ITool)pCommand;

                }
            }

        }                      //调用类缩小

        private void MapFullToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ESRI.ArcGIS.SystemUI.ICommand pCommand = new ControlsMapFullExtentCommandClass();
            pCommand.OnCreate(pmapC);
            pCommand.OnClick();

        }                           //调用类全图

        private void Eagle_eyeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (overview == false)
            {
                frm_overview = new Eagle_eye();
                frm_overview.Show(this);

                for (int i = axMapControl1.LayerCount - 1; i >= 0; i--)
                {
                    frm_overview.MapC_OverView.AddLayer(axMapControl1.get_Layer(i));
                }

                frm_overview.MapC_OverView.Extent = frm_overview.MapC_OverView.FullExtent;

                IRgbColor rgbColor = new RgbColorClass();
                rgbColor.Red = 255;
                rgbColor.Blue = 0;
                rgbColor.Green = 0;

                IEnvelope pEnvelop = axMapControl1.Extent;
                ISimpleLineSymbol pLineS = new SimpleLineSymbolClass();

                pLineS.Color = rgbColor;
                pLineS.Width = 2;

                ISimpleFillSymbol fillSymbol = new SimpleFillSymbolClass();
                fillSymbol.Outline = pLineS;
                fillSymbol.Style = esriSimpleFillStyle.esriSFSForwardDiagonal;

                IFillShapeElement pElement = new RectangleElementClass();
                (pElement as IElement).Geometry = pEnvelop;
                pElement.Symbol = fillSymbol as IFillSymbol;

                (frm_overview.MapC_OverView.Map as IGraphicsContainer).AddElement(pElement as IElement, 0);
                frm_overview.MapC_OverView.Refresh(esriViewDrawPhase.esriViewGraphics, null, null);
                overview = true;
            }
        }             //鹰眼视图

        private void axToolbarControl1_OnMouseMove(object sender, IToolbarControlEvents_OnMouseMoveEvent e)
        {
            // axMapControl1.CurrentTool = null;
            // currentT = "";
            int index = axToolbarControl1.HitTest(e.x, e.y, false); //    
            if (index != -1) { ESRI.ArcGIS.Controls.IToolbarItem toolBarItem = axToolbarControl1.GetItem(index); MessageLabel.Text = toolBarItem.Command.Message; }
            else
            {
                MessageLabel.Text = "就绪";
            }



        }      //鼠标单击工具栏


        private void axMapControl1_OnMouseMove(object sender, IMapControlEvents2_OnMouseMoveEvent e)   //鼠标位置
        {
            ScaleLabel.Text = "比例尺1：" + ((long)this.axMapControl1.MapScale).ToString();
            // CoordinateLabel .Text="当前坐标 X="+e.mapY.ToString() + " Y = " + e.mapY.ToString() + " " + sMapUnits;
            CoordinateLabel.Text = "当前坐标 X=" + e.mapY.ToString() + " Y = " + e.mapY.ToString();
        }

        private void axMapControl1_OnMapReplaced(object sender, IMapControlEvents2_OnMapReplacedEvent e)
        {    //axMapControl加载的数据发生变化时，需要联动，所以在axMapControl的OnMapReplaced事件中需要调用数据拷贝的方法
            //GeoMapLoad.CopyAndOverwriteMap(axMapControl1, axPageLayoutControl1);
            //修改坐标显示的单位名称  ESRI.ArcGIS.esriSystem.esriUnits mapUnits = mainMapControl.MapUnits;             switch (mapUnits)            {                 case ESRI.ArcGIS.esriSystem.esriUnits.esriCentimeters:                     sMapUnits = "Centimeters";                     break;                 case ESRI.ArcGIS.esriSystem.esriUnits.esriDecimalDegrees:                     sMapUnits = "Decimal Degrees";                     break;                 case ESRI.ArcGIS.esriSystem.esriUnits.esriDecimeters:                     sMapUnits = "Decimeters";                     break;                 case ESRI.ArcGIS.esriSystem.esriUnits.esriFeet:                     sMapUnits = "Feet";                     break;                 case ESRI.ArcGIS.esriSystem.esriUnits.esriInches:                     sMapUnits = "Inches";                     break;                 case ESRI.ArcGIS.esriSystem.esriUnits.esriKilometers:                     sMapUnits = "Kilometers";                     break;                 case ESRI.ArcGIS.esriSystem.esriUnits.esriMeters:                     sMapUnits = "Meters";                     break;                 case ESRI.ArcGIS.esriSystem.esriUnits.esriMiles:                     sMapUnits = "Miles";                     break;                 case ESRI.ArcGIS.esriSystem.esriUnits.esriMillimeters:                     sMapUnits = "Millimeters";                     break;                 case ESRI.ArcGIS.esriSystem.esriUnits.esriNauticalMiles:                     sMapUnits = "NauticalMiles";                     break;                 case ESRI.ArcGIS.esriSystem.esriUnits.esriPoints:                     sMapUnits = "Points";                     break;                 case ESRI.ArcGIS.esriSystem.esriUnits.esriUnknownUnits:                     sMapUnits = "Unknown";                     break;                 case ESRI.ArcGIS.esriSystem.esriUnits.esriYards:                     sMapUnits = "Yards";                     break;


        }
        public class GeoMapLoad
        {
            //public static void CopyAndOverwriteMap(AxMapControl axMapControl, AxPageLayoutControl axPageLayoutControl)
            //{
            //    IObjectCopy objectCopy = new ObjectCopyClass();
            //    object toCopyMap = axMapControl.Map;
            //    object copiedMap = objectCopy.Copy(toCopyMap);
            //    object overwriteMap = axPageLayoutControl.ActiveView.FocusMap;
            //    objectCopy.Overwrite(toCopyMap, ref overwriteMap);
            //}
        }                                                             //-----封装数据拷贝函数方便多处调用


        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str_tab = tabControl1.SelectedTab.Text;
            if (str_tab == "数据视图")
            {
                axTOCControl1.SetBuddyControl(pmapC);

                IMap map = axMapControl1.Map;
                int layercount = map.LayerCount;
            }
            else if (str_tab == "布局视图")
            {
                //AxPageLayoutControl PaxPageLayoutControl1 = new AxPageLayoutControl();
                axToolbarControl1.SetBuddyControl(axPageLayoutControl1);
                //---仅说明可以转换
                IActiveView pactiveV = axPageLayoutControl1.ActiveView;
                IMap map = pactiveV.FocusMap;
                int layercount = map.LayerCount;
                IObjectCopy pObjectCopy = new ObjectCopyClass();
                object pCopyMap = pObjectCopy.Copy(pmapC.Map);//复制
                object pToOnerwriteMap = axPageLayoutControl1.ActiveView.FocusMap;
                pObjectCopy.Overwrite(pCopyMap, ref pToOnerwriteMap);
                axPageLayoutControl1.Refresh(esriViewDrawPhase.esriViewGeography, null, null);


            }
        }          //视图选项卡切换
        private void axMapControl1_OnAfterScreenDraw(object sender, IMapControlEvents2_OnAfterScreenDrawEvent e)
        {
            //.axMapControl加载的数据发生重绘时，需要联动，所以在axMapControl的OnAfterScreenDraw事件中，需添加获取axMapControl控件中当前所显示的地理范围代码，并将当前显示范围传给axPageLayoutControl控件ActiveView对象的FocusMap中，同时调用拷贝
            //IActiveView pAcv = axPageLayoutControl1.ActiveView.FocusMap as IActiveView;
            //IDisplayTransformation displayTransformation = pAcv.ScreenDisplay.DisplayTransformation;
            //displayTransformation.VisibleBounds = axMapControl1.Extent;//设置焦点地图的可视范围
            //axPageLayoutControl1.ActiveView.Refresh();
            //GeoMapLoad.CopyAndOverwriteMap(axMapControl1, axPageLayoutControl1);

        }

        private void axMapControl1_OnViewRefreshed(object sender, IMapControlEvents2_OnViewRefreshedEvent e)
        {
            //xMapControl中的数据显示状况发生变化时，需要联动，所以在axMapControl的OnViewRefreshed事件中需要调用数据拷贝和图层刷新的方法；
            //// axTOCControl1.Update();
            // GeoMapLoad.CopyAndOverwriteMap(axMapControl1, axPageLayoutControl1);
        }

        private void axMapControl1_OnExtentUpdated(object sender, IMapControlEvents2_OnExtentUpdatedEvent e)
        {
            if (overview)
            {
                (frm_overview.MapC_OverView.Map as IGraphicsContainer).DeleteAllElements();

                IEnvelope pEnvelop = axMapControl1.Extent;
                IRgbColor rgbColor = new RgbColorClass();
                rgbColor.Red = 255;
                rgbColor.Blue = 0;
                rgbColor.Green = 0;

                ISimpleLineSymbol pLineS = new SimpleLineSymbolClass();

                pLineS.Color = rgbColor;
                pLineS.Width = 2;

                ISimpleFillSymbol fillSymbol = new SimpleFillSymbolClass();
                fillSymbol.Outline = pLineS;
                fillSymbol.Style = esriSimpleFillStyle.esriSFSForwardDiagonal;

                IFillShapeElement pElement = new RectangleElementClass();
                (pElement as IElement).Geometry = pEnvelop;
                pElement.Symbol = fillSymbol as IFillSymbol;

                (frm_overview.MapC_OverView.Map as IGraphicsContainer).AddElement(pElement as IElement, 0);
                frm_overview.MapC_OverView.Refresh(esriViewDrawPhase.esriViewGraphics, null, null);
            }
        }

        private void axToolbarControl1_OnMouseDown(object sender, IToolbarControlEvents_OnMouseDownEvent e)
        {

            currentT = "";//使得清除上一步操作
        }//鼠标点击工具栏


        private void OpenAttributeTableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ICommand command = new OpenAttributeTable(layer);
            command.OnCreate(layer);
            command.OnClick();
        }
        private void 操作ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ///
        }

        private void RemoveLayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            axMapControl1.Map.DeleteLayer(layer);
            
        }//移除图层

        private void ZoomToLaryerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (layer == null) return;
            (axMapControl1.Map as IActiveView).Extent = layer.AreaOfInterest;
            (axMapControl1.Map as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
        }
    }
}



    
