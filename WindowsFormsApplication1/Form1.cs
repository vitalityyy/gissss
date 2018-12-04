using ESRI.ArcGIS.Analyst3D;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.GeoAnalyst;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.SpatialAnalyst;
using ESRI.ArcGIS.SystemUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
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
        private string page_currenttool;



        IRubberBand pRubberBand;
        IGraphicsContainer pGraphicsContainer;

        public static bool overview = false;
        Eagle_eye frm_overview = null;

        IToolbarMenu m_TocLayerMenu = new ToolbarMenuClass();//图层右键
        IToolbarMenu m_TocMapMenu = new ToolbarMenuClass();//地图右键

        esriTOCControlItem itemType = esriTOCControlItem.esriTOCControlItemNone;
        IBasicMap basicMap = null;
        ILayer layer = null;
        object unk = null;
        object data = null;

        public Form1()
        {

          
            WelcomeFrm fw = new WelcomeFrm();
            fw.Show();//show出欢迎窗口
            System.Threading.Thread.Sleep(2000);//欢迎窗口停留时间2s
            fw.Close();
            InitializeComponent();
        }



        private void Form1_Load(object sender, EventArgs e)
        {
            
            
            //new System.Threading.Thread(new System.Threading.ThreadStart(delegate
            //{
            //    WelcomeFrm welcomefrm = new WelcomeFrm();
            //    FrmLoading frmLoading = new FrmLoading();
            //    this.Shown += delegate
            //    {
            //        welcomefrm.Invoke(new EventHandler(welcomefrm.KillMe));
            //        welcomefrm.Dispose();
            //    };
            //    frmLoading.Show();
            //    Application.Run(welcomefrm);
            //})).Start();
            //构造窗体函数，比较耗时
            //BuildForm();
            //base.OnLoad(e);
            //主线程休息会儿
            //System.Threading.Thread.Sleep(5000);
            // base.OnLoad(e);

            this.Text = "HeJiaqing&arcgis";
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
            else if (currentT == "Points")
            {
                IMarkerElement pMarkerElement;//对于点，线，面的element定义这里都不一样，他是可实例化的类，而IElement是实例化的类，必须通过IMarkerElement 初始化负值给 IElement 。
                IElement pMElement;
                IPoint pPoint;//你画的图形式什么就是什么，特别的是LINE则需要定义为POLYLINE
                pMarkerElement = new MarkerElementClass();
                pMElement = pMarkerElement as IElement;


                pRubberBand = new RubberPointClass();//你的RUBBERBAND随着你的图形而变
                pPoint = pRubberBand.TrackNew(axMapControl1.ActiveView.ScreenDisplay, null) as IPoint;

                pMElement.Geometry = pPoint;//把你在屏幕中画好的图形付给 IElement 储存
                pGraphicsContainer = axMapControl1.ActiveView as IGraphicsContainer;//把地图的当前view作为图片的容器

                pGraphicsContainer.AddElement(pMElement, 0);//显示储存在 IElement 中图形，这样就持久化了。
                axMapControl1.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
            }
            else if (currentT == "polyline")
            {
                ILineElement pLineElement;
                IElement pLElement;

                IPolyline pLine;

                pLineElement = new LineElementClass();
                pLElement = pLineElement as IElement;

                pRubberBand = new RubberLineClass();
                pLine = pRubberBand.TrackNew(axMapControl1.ActiveView.ScreenDisplay, null) as IPolyline;

                pLElement.Geometry = pLine;

                pGraphicsContainer = axMapControl1.ActiveView as IGraphicsContainer;//把地图的当前view作为容器


                pGraphicsContainer.AddElement(pLElement, 0);//把刚刚的element转到容器上 
                axMapControl1.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);

            }
            else if (currentT == "SpatialFilter")
            {
                IPoint point = new PointClass();
                point.PutCoords(e.mapX, e.mapY);
                IFeatureLayer pfl = layer as IFeatureLayer;
                IFeature pf = Map_Functions.SeartchFeature(axMapControl1.Map, point, pfl);
                axMapControl1.Refresh(esriViewDrawPhase.esriViewGeoSelection, null, null);
                MessageBox.Show(pf.OID.ToString());
            }
            else if (currentT == "Identify")
            {
                IMap pamp = axMapControl1.Map;
                IIdentify pIdentify = pamp.get_Layer(5) as IIdentify;
                IPoint pPoint = new PointClass();
                pPoint.PutCoords(e.mapX, e.mapY);
                pPoint.SpatialReference = pamp.SpatialReference;
                IArray pArray = pIdentify.Identify(pPoint);
                if (pArray != null)
                {
                    IFeatureIdentifyObj pFIO = pArray.get_Element(0) as IFeatureIdentifyObj;
                    IRowIdentifyObject pRIO = pFIO as IRowIdentifyObject;
                    IRow prow = pRIO.Row;
                    axMapControl1.FlashShape((prow as IFeature).Shape, 3, 300, Type.Missing);
                    MessageBox.Show(prow.get_Value(prow.Fields.FindField("code_1")).ToString());

                }
               
                //IMap pmap = axMapControl1.Map;
                //for (int i = 0; i < pmap.LayerCount; i++)
                //{
                //    IIdentify pIdentify = pmap.get_Layer(i) as IIdentify;

                //    IPoint pPoint = new PointClass();
                //    pPoint.PutCoords(e.x, e.y);
                //    pPoint.SpatialReference = pmap.SpatialReference;

                //    IArray pArray = pIdentify.Identify(pPoint);                 //返回一个几何体集合，试一下用面呢？ trackline,trackpolygon
                //    if (pArray != null)
                //    {
                //        for (int j = 0; j < pArray.Count; j++)
                //        {
                //            IFeatureIdentifyObj pFIO = pArray.get_Element(j) as IFeatureIdentifyObj;        //扩充为遍历每一个几何体
                //            IRowIdentifyObject pRIO = pFIO as IRowIdentifyObject;
                //            IRow prow = pRIO.Row;

                //            axMapControl1.FlashShape((prow as IFeature).Shape, 3, 300, Type.Missing);
                //            MessageBox.Show(prow.get_Value(prow.Fields.FindField("OBJECTID")).ToString());
                //        }
                //    }
                //}


            }
            else if (currentT == "Draw_point_project")
            {
                IPoint point = PRJtoGCS(e.mapX, e.mapY);
                double x, y;
                point.QueryCoords(out x, out y);
                MessageBox.Show("平面坐标是：" + e.mapX.ToString() + ";  " + e.mapY.ToString() + "\n\n" + "地理坐标是：" + x.ToString() + ";  " + y.ToString(), "提示");
            }
        }//鼠标在地图上发生事件

        private IPoint PRJtoGCS(double x, double y)
        {
            IPoint point = new PointClass();
            point.PutCoords(x, y);
            ISpatialReferenceFactory pSRF = new SpatialReferenceEnvironmentClass();
            point.SpatialReference = pmapC.SpatialReference;

            point.Project(pSRF.CreateGeographicCoordinateSystem((int)esriSRGeoCSType.esriSRGeoCS_Beijing1954));
            return point;
        }
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
            ScaleLabel.Text = "比例尺1：" + axMapControl1.MapScale.ToString();
            // CoordinateLabel .Text="当前坐标 X="+e.mapY.ToString() + " Y = " + e.mapY.ToString() + " " + sMapUnits;
            CoordinateLabel.Text = "当前坐标 X=" + e.mapX.ToString() + " Y = " + e.mapY.ToString();
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

        private void 点ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            axMapControl1.CurrentTool = null;
            currentT = "Points";
            axMapControl1.MousePointer = esriControlsMousePointer.esriPointerCrosshair;
        }

        private void 线ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            axMapControl1.CurrentTool = null;
            currentT = "polyline";
            axMapControl1.MousePointer = esriControlsMousePointer.esriPointerCrosshair;
        }

        private void axPageLayoutControl1_OnMouseDown(object sender, IPageLayoutControlEvents_OnMouseDownEvent e)
        {
            IActiveView pActiveViewzjf;
            IRubberBand pRubberBand;
            IEnvelope pEnve;
            IGraphicsContainer pGraphicsC;

            switch (page_currenttool)
            {
                case "NorthArrow":
                    pActiveViewzjf = (IActiveView)axPageLayoutControl1.PageLayout;
                    pGraphicsC = axPageLayoutControl1.GraphicsContainer;
                    pRubberBand = new RubberEnvelopeClass();
                    pEnve = (IEnvelope)pRubberBand.TrackNew(pActiveViewzjf.ScreenDisplay, null);
                    CreateNorthArrow(pEnve, pActiveViewzjf);
                    (axPageLayoutControl1.GraphicsContainer as IGraphicsContainerSelect).UnselectAllElements();
                    pActiveViewzjf.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
                    page_currenttool = null;
                    axPageLayoutControl1.CurrentTool = null;

                    break;
                case "Legend":
                    pActiveViewzjf = (IActiveView)axPageLayoutControl1.PageLayout;
                    pGraphicsC = axPageLayoutControl1.GraphicsContainer;
                    pRubberBand = new RubberEnvelopeClass();
                    pEnve = (IEnvelope)pRubberBand.TrackNew(pActiveViewzjf.ScreenDisplay, null);
                    CreateLegend(pEnve, pActiveViewzjf);
                    (axPageLayoutControl1.GraphicsContainer as IGraphicsContainerSelect).UnselectAllElements();
                    pActiveViewzjf.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
                    page_currenttool = null;
                    axPageLayoutControl1.CurrentTool = null;

                    break;
                case "Text":
                    pActiveViewzjf = (IActiveView)axPageLayoutControl1.PageLayout;
                    pGraphicsC = axPageLayoutControl1.GraphicsContainer;
                    pRubberBand = new RubberEnvelopeClass();
                    pEnve = (IEnvelope)pRubberBand.TrackNew(pActiveViewzjf.ScreenDisplay, null);
                    CreateText(e.x, e.y, pEnve, pActiveViewzjf);
                    (axPageLayoutControl1.GraphicsContainer as IGraphicsContainerSelect).UnselectAllElements();
                    pActiveViewzjf.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
                    page_currenttool = null;
                    axPageLayoutControl1.CurrentTool = null;

                    break;
                case "Scalebar":
                    pActiveViewzjf = (IActiveView)axPageLayoutControl1.PageLayout;
                    pGraphicsC = axPageLayoutControl1.GraphicsContainer;
                    pRubberBand = new RubberEnvelopeClass();
                    pEnve = (IEnvelope)pRubberBand.TrackNew(pActiveViewzjf.ScreenDisplay, null);
                    CreateScalebar(pEnve, pActiveViewzjf);
                    (axPageLayoutControl1.GraphicsContainer as IGraphicsContainerSelect).UnselectAllElements();
                    pActiveViewzjf.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
                    page_currenttool = null;
                    axPageLayoutControl1.CurrentTool = null;

                    break;
                case "Scale":
                    pActiveViewzjf = (IActiveView)axPageLayoutControl1.PageLayout;
                    pGraphicsC = axPageLayoutControl1.GraphicsContainer;
                    pRubberBand = new RubberEnvelopeClass();
                    pEnve = (IEnvelope)pRubberBand.TrackNew(pActiveViewzjf.ScreenDisplay, null);
                    CreateScale(pEnve, pActiveViewzjf);
                    (axPageLayoutControl1.GraphicsContainer as IGraphicsContainerSelect).UnselectAllElements();
                    pActiveViewzjf.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
                    page_currenttool = null;
                    axPageLayoutControl1.CurrentTool = null;

                    break;


            }
        }
        private void CreateNorthArrow(IEnvelope pEnve, IActiveView pActiveViewzjf)
        {
            IMapFrame pMapFrame;
            IMapSurroundFrame pMapSurroundF;
            IMapSurround pMapsurround;
            IElement pElement;
            IMarkerNorthArrow pMarkNorthA;
            ICharacterMarkerSymbol pCharcterMarkerS;
            UID puid;
            puid = new UIDClass();
            puid.Value = "{7A3F91DD-B9E3-11d1-8756-0000F8751720}";
            pMapFrame = (IMapFrame)pActiveViewzjf.GraphicsContainer.FindFrame(pActiveViewzjf.FocusMap);
            pMapSurroundF = pMapFrame.CreateSurroundFrame(puid, null);
            pMapsurround = pMapSurroundF.MapSurround;
            pMarkNorthA = (IMarkerNorthArrow)pMapsurround;
            pCharcterMarkerS = (ICharacterMarkerSymbol)pMarkNorthA.MarkerSymbol;
            pCharcterMarkerS.CharacterIndex = 173;
            pElement = (IElement)pMapSurroundF;
            pElement.Geometry = pEnve;
            pActiveViewzjf.GraphicsContainer.AddElement(pElement, 0);


        }
        private void CreateLegend(IEnvelope pEnve, IActiveView pActiveViewzjf)
        {
            IMapFrame pMapFrame;
            IMapSurroundFrame pMapSurroundF;
            IMapSurround pMapsurround;
            IElement pElement;
            ILegend pLegend;
            ILegendItem pLegendItem;
            UID uid;
            uid = new UIDClass();
            uid.Value = "{7A3F91E4-B9E3-11d1-8756-0000F8751720}";
            IFillSymbol pFillSymbol;
            ILineSymbol pLineSymbol;
            IRgbColor pColor;
            ISymbolBackground pSymbolBG;
            pMapFrame = (IMapFrame)pActiveViewzjf.GraphicsContainer.FindFrame(pActiveViewzjf.FocusMap);
            pMapSurroundF = pMapFrame.CreateSurroundFrame(uid, null);
            pSymbolBG = new SymbolBackgroundClass();
            pFillSymbol = new SimpleFillSymbolClass();
            pLineSymbol = new SimpleLineSymbolClass();
            pColor = new RgbColorClass();
            pColor.Red = 255;
            pColor.Blue = 255;
            pColor.Green = 255;
            pLineSymbol.Color = pColor as IColor;
            pFillSymbol.Color = pColor as IColor;
            pFillSymbol.Outline = pLineSymbol;
            pSymbolBG.FillSymbol = pFillSymbol;
            pMapSurroundF.Background = pSymbolBG;
            pMapsurround = pMapSurroundF.MapSurround;
            pLegend = (ILegend)pMapsurround;
            pLegend.AutoAdd = true;
            pLegend.AutoReorder = true;
            pLegend.AutoVisibility = true;
            pLegend.Title = "图 例";
            pLegend.ClearItems();
            IEnumLayer pEnumerlayer;
            ILayer ply;
            pEnumerlayer = pLegend.Map.get_Layers(null, false);
            pEnumerlayer.Reset();
            ply = pEnumerlayer.Next();
            while (ply != null)
            {
                if (ply is IFeatureLayer)
                {
                    if ((ply as IFeatureLayer).FeatureClass.FeatureType == esriFeatureType.esriFTAnnotation)
                    {
                        ply = pEnumerlayer.Next();
                        continue;

                    }
                }
                pLegendItem = new HorizontalLegendItemClass();
                pLegendItem.Layer = ply;
                pLegendItem.Columns = 1;
                pLegendItem.ShowLayerName = true;
                pLegendItem.ShowHeading = true;
                pLegendItem.ShowLabels = true;
                pLegend.AddItem(pLegendItem);
                ply = (pEnumerlayer.Next());

            }
            pElement = (IElement)pMapSurroundF;
            pElement.Geometry = pEnve;
            pActiveViewzjf.GraphicsContainer.AddElement(pElement, 0);
            pActiveViewzjf.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, pEnve);


        }
        private void CreateText(double x, double y, IEnvelope pEnve, IActiveView pActiveViewzjf)
        {
            ITextElement pTextelement;
            IElement pElement;
            IGraphicsContainer pGraphicsC = axPageLayoutControl1.GraphicsContainer;
            pTextelement = new TextElementClass();
            pElement = (IElement)pTextelement;
            pTextelement.ScaleText = false;
            pTextelement.Symbol.Size = 50;
            pTextelement.Text = "这是一个文本元素";
            pElement.Geometry = pActiveViewzjf.ScreenDisplay.DisplayTransformation.ToMapPoint(Convert.ToInt32(x), Convert.ToInt32(y));
            pGraphicsC.AddElement(pElement, 0);
            pActiveViewzjf.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);

        }
        private void CreateScalebar(IEnvelope pEnve, IActiveView pActiveViewzjf)
        {
            IMapFrame pMapFrame;
            IMapSurroundFrame pMapSurroundF;
            IMapSurround pMapsurround;
            IElement pElement;
            IScaleBar pScaleBar;
            UID puid;
            puid = new UIDClass();
            puid.Value = "{6589F147-F7F7-11d2-B872-00600802E603}";
            pMapFrame = (IMapFrame)pActiveViewzjf.GraphicsContainer.FindFrame(pActiveViewzjf.FocusMap);
            pMapSurroundF = pMapFrame.CreateSurroundFrame(puid, null);
            IFillSymbol pFillSymbol;
            ILineSymbol pLineSymbol;
            IRgbColor pColor;
            ISymbolBackground pSymbolBG;
            pSymbolBG = new SymbolBackgroundClass();
            pFillSymbol = new SimpleFillSymbolClass();
            pLineSymbol = new SimpleLineSymbolClass();
            pColor = new RgbColorClass();
            pColor.Red = 255;
            pColor.Blue = 255;
            pColor.Green = 255;
            pLineSymbol.Color = pColor as IColor;
            pFillSymbol.Color = pColor as IColor;
            pFillSymbol.Outline = pLineSymbol;
            pSymbolBG.FillSymbol = pFillSymbol;
            pMapSurroundF.Background = pSymbolBG;
            pMapsurround = pMapSurroundF.MapSurround;
            pScaleBar = pMapsurround as IScaleBar;
            pScaleBar.DivisionsBeforeZero = 1;
            pScaleBar.Divisions = 3;
            pScaleBar.Subdivisions = 10;
            pScaleBar.Units = esriUnits.esriKilometers;
            pScaleBar.UnitLabel = "千米";
            //pScaleBar.LabelSymbol.Size = 2;

            //为什么不能更改大小
            pScaleBar.UnitLabelPosition = esriScaleBarPos.esriScaleBarAbove;
            pScaleBar.LabelPosition = esriVertPosEnum.esriBelow;
            pScaleBar.LabelFrequency = esriScaleBarFrequency.esriScaleBarDivisionsAndFirstMidpoint;
            pElement = (IElement)pMapSurroundF;
            pElement.Geometry = pEnve;
            pActiveViewzjf.GraphicsContainer.AddElement(pElement, 0);
            pActiveViewzjf.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, pEnve);
        }
        private void CreateScale(IEnvelope pEnve, IActiveView pActiveViewzjf)
        {
            throw new NotImplementedException();
        }

        private void 指北针ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string str_tab = tabControl1.SelectedTab.Text;
            if (tabControl1.TabIndex != 1)
            {
                MessageBox.Show("该功能仅能在出图视图中可用！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            axPageLayoutControl1.CurrentTool = null;
            page_currenttool = "NorthArrow";
            axPageLayoutControl1.MousePointer = esriControlsMousePointer.esriPointerCrosshair;
        }
     
        private void 切换到布局视图ToolStripMenuItem_Click(object sender, EventArgs e)
        {
          //  this.DialogResult = DialogResult.OK;
           // this.Close();
            if (tabControl1.TabIndex == 0)
            {
                tabControl1.TabIndex = 1;
            }
            else
            {
                tabControl1.TabIndex = 1;
            }
        }

        private void LEGENDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            axPageLayoutControl1.CurrentTool = null;
            page_currenttool = "Legend";
            axPageLayoutControl1.MousePointer = esriControlsMousePointer.esriPointerCrosshair;
        }//添加图例

        private void NAMEToolStripMenuItem_Click(object sender, EventArgs e)
        {

            axPageLayoutControl1.CurrentTool = null;
            page_currenttool = "Text";
            axPageLayoutControl1.MousePointer = esriControlsMousePointer.esriPointerCrosshair;
        }

        private void 比例尺ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            axPageLayoutControl1.CurrentTool = null;
            page_currenttool = "Scalebar";
            axPageLayoutControl1.MousePointer = esriControlsMousePointer.esriPointerCrosshair;
        }

        private void 数字比例尺ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            axPageLayoutControl1.CurrentTool = null;
            page_currenttool = "Scale";
            axPageLayoutControl1.MousePointer = esriControlsMousePointer.esriPointerCrosshair;
        }



        private void TaggingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (TaggingToolStripMenuItem.Text == "取消标注")
            {
                (layer as IGeoFeatureLayer).DisplayAnnotation = false;

                axMapControl1.Refresh();

                return;
            }
            LabelFrm frm = new LabelFrm(layer as IGeoFeatureLayer);
            //添加一个frm label 用来控制标注

            if (frm.ShowDialog() == DialogResult.OK)
            {
                axMapControl1.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
            }
        }



        private void SlopeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ILayer pl = axMapControl1.get_Layer(0);
            if (pl is IRasterLayer)
            {
                IRasterLayer pRasterLayer = pl as IRasterLayer;
                IRaster pIRaster = pRasterLayer.Raster;
                ISurfaceOp pSufaceOp = new RasterSurfaceOpClass();
                object zFactor = 1;
                IGeoDataset pGeoDataset = pSufaceOp.Aspect((IGeoDataset)pIRaster);
                IRasterLayer pRasterLayer_new = new RasterLayerClass();
                //IRasterLayer pRasterLayer_new = pl as IRasterLayer;
                IRaster praster;
                praster = (IRaster)pGeoDataset;
                pRasterLayer_new.CreateFromRaster(praster);
                pRasterLayer_new.Name = "Aspect";
                axMapControl1.AddLayer(pRasterLayer_new);
                axMapControl1.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
            }
        }//生成坡度

        private void HillShadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ILayer pl = axMapControl1.get_Layer(0);
            if (pl is IRasterLayer)
            {
                IRasterLayer pRasterLayer = pl as IRasterLayer;
                IRaster pIRaster = pRasterLayer.Raster;
                ISurfaceOp pSufaceOp = new RasterSurfaceOpClass();
                object zFactor = 2;
                IGeoDataset pGeoDataset = pSufaceOp.HillShade((IGeoDataset)pIRaster, 135, 45, true, ref zFactor);
                //IRasterLayer pRasterLayer_new = pl as IRasterLayer;
                IRasterLayer pRasterLayer_new = new RasterLayerClass();
                //IRaster praster = new RasterClass();
                IRaster praster;
                praster = (IRaster)pGeoDataset;
                pRasterLayer_new.CreateFromRaster(praster);
                pRasterLayer_new.Name = "HillShade";
                axMapControl1.AddLayer(pRasterLayer_new);
                axMapControl1.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);


            }

        }//山体阴影

        private void AspectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ILayer pl = axMapControl1.get_Layer(0);
            if (pl is IRasterLayer)
            {
                IRasterLayer pRasterLayer = pl as IRasterLayer;
                IRaster pIRaster = pRasterLayer.Raster;
                ISurfaceOp pSufaceOp = new RasterSurfaceOpClass();
                object zFactor = 1;
                IGeoDataset pGeoDataset = pSufaceOp.Aspect((IGeoDataset)pIRaster);
                IRasterLayer pRasterLayer_new = new RasterLayerClass();
                //IRasterLayer pRasterLayer_new = pl as IRasterLayer;
                IRaster praster;
                praster = (IRaster)pGeoDataset;
                pRasterLayer_new.CreateFromRaster(praster);
                pRasterLayer_new.Name = "Aspect";
                axMapControl1.AddLayer(pRasterLayer_new);
                axMapControl1.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);

            }
        }//生成坡向

        private void 栅格技算ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            IRasterLayer pRl1 = axMapControl1.get_Layer(0) as IRasterLayer;
            IRasterLayer pRl2 = axMapControl1.get_Layer(1) as IRasterLayer;
            IMapAlgebraOp pMAO = new RasterMapAlgebraOpClass();
            pMAO.BindRaster(pRl1 as IGeoDataset, "raster1");
            pMAO.BindRaster(pRl2 as IGeoDataset, "raster2");
            IGeoDataset poutGeods = pMAO.Execute("[raster1] * 0.6 + [raster2] * 0.4");
            IRasterLayer prs = new RasterLayerClass();
            prs.CreateFromRaster(poutGeods as IRaster);
            axMapControl1.AddLayer(prs);
        }

        private void ContourToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ContourFrm frm = new ContourFrm();
            frm.Show();
        }

        private void 空间查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            axMapControl1.CurrentTool = null;
            currentT = "SpatialFilter";
            axMapControl1.MousePointer = esriControlsMousePointer.esriPointerCrosshair;
        }
        public class Map_Functions
        {
            public static IFeature SeartchFeature(IMap pMap, IPoint point, IFeatureLayer pFeatureLayer)
            {
                IFeature pFeature = null;

                IFeatureClass pFeatureClass = pFeatureLayer.FeatureClass;
                //IFeatureClass pFeatureClass = pFeature as  IFeatureClass;

                ITopologicalOperator pTopo = point as ITopologicalOperator;
                IGeometry pBuffer = pTopo.Buffer(200);
                IGeometry pGeometry = pBuffer.Envelope;

                ISpatialFilter pSpatialFillter = new SpatialFilterClass();
                pSpatialFillter.Geometry = pGeometry;
                switch (pFeatureClass.ShapeType)
                {
                    case esriGeometryType.esriGeometryPoint:
                        pSpatialFillter.SpatialRel = esriSpatialRelEnum.esriSpatialRelContains;
                        break;
                    case esriGeometryType.esriGeometryPolyline:
                        pSpatialFillter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
                        break;
                    case esriGeometryType.esriGeometryPolygon:
                        pSpatialFillter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
                        break;
                }
                IFeatureSelection pFSelection = pFeatureLayer as IFeatureSelection;
                pFSelection.SelectFeatures(pSpatialFillter, esriSelectionResultEnum.esriSelectionResultNew, false);
                ISelectionSet pselectionset = pFSelection.SelectionSet;
                IEnumIDs pEids = pselectionset.IDs;
                pEids.Reset();
                int id = pEids.Next();
                while (id != -1)
                {
                    pFeature = pFeatureClass.GetFeature(id);
                    break;
                }
                return pFeature;
            }

            internal static IFeatureClass Featureclass_Intersect(IFeatureClass pfc_input, bool p, IFeatureClass pfc_overlap, bool p_2, string intersect_or_union)
            {
                IFeatureClass pfc_result = null;
                try
                {
                    IWorkspaceFactory pwsf = new InMemoryWorkspaceFactoryClass();
                    IWorkspaceName pWSName = pwsf.Create("", "Temp", null, 0);
                    IFeatureClassName ResultFeatureClassName = new FeatureClassNameClass();
                    IDatasetName ResultDatasetName = (IDatasetName)ResultFeatureClassName;
                    ResultDatasetName.Name = "layer_interset";
                    ResultDatasetName.WorkspaceName = pWSName;
                    IBasicGeoprocessor bgp = new BasicGeoprocessorClass();
                    double wc = 0.0;
                    switch (intersect_or_union)
                    {
                        case "intersect":
                            pfc_result = bgp.Intersect((ITable)pfc_input, true, (ITable)pfc_overlap, false, wc, ResultFeatureClassName);
                            break;
                        case "union":
                            pfc_result = bgp.Union((ITable)pfc_input, true, (ITable)pfc_overlap, false, wc, ResultFeatureClassName);
                            break;
                        default:
                            break;
                    }
                }
                catch
                {

                }
                return pfc_result;

            }

            public static IFeatureClass buffer(IFeatureClass pFeatureClass, IQueryFilter filter, IMap pMap, string fieldname)
            {
                IFeatureClass feacls = null;
                try
                {
                    IWorkspaceFactory pWSF = new InMemoryWorkspaceFactoryClass();
                    IWorkspaceName pWSName = pWSF.Create("", "Temp", null, 0);
                    IFeatureClassName sourceFeatureClassName = new FeatureClassNameClass();
                    IDatasetName sourceDataSetName = (IDatasetName)sourceFeatureClassName;
                    sourceDataSetName.Name = "缓冲区";
                    sourceDataSetName.WorkspaceName = pWSName;
                    IFeatureCursorBuffer2 featurecusbuf = new FeatureCursorBufferClass();
                    featurecusbuf.FeatureCursor = pFeatureClass.Search(filter, false);
                    featurecusbuf.Dissolve = true;
                    featurecusbuf.ValueDistance = 100;
                    featurecusbuf.SpatialReference = pMap.SpatialReference;
                    featurecusbuf.BufferSpatialReference = pMap.SpatialReference;
                    featurecusbuf.SourceSpatialReference = pMap.SpatialReference;
                    featurecusbuf.TargetSpatialReference = pMap.SpatialReference;
                    featurecusbuf.DataFrameSpatialReference = pMap.SpatialReference;
                    featurecusbuf.Buffer(sourceFeatureClassName);
                    feacls = (sourceFeatureClassName as IName).Open() as IFeatureClass;
                }
                catch
                {

                }
                return feacls;
            }
        }

        private void 空间叠置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IFeatureClass pfc1 = (axMapControl1.get_Layer(4) as IFeatureLayer).FeatureClass;
            IFeatureClass pfc2 = (axMapControl1.get_Layer(5) as IFeatureLayer).FeatureClass;
            IFeatureLayer pfl = new FeatureLayerClass();
            pfl.FeatureClass = Map_Functions.Featureclass_Intersect(pfc1, false, pfc2, false, "intersect");
            pfl.Name = "空间叠置";
            axMapControl1.AddLayer(pfl);
            axMapControl1.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
        }

        private void 建立缓冲区ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IFeatureClass pfc = (axMapControl1.get_Layer(0) as IFeatureLayer).FeatureClass;
            IFeatureLayer pfl = new FeatureLayerClass();
            pfl.FeatureClass = Map_Functions.buffer(pfc, null, axMapControl1.Map, "");
            pfl.Name = "缓冲区";
            axMapControl1.AddLayer(pfl);
            axMapControl1.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
        }

        private void 元素特征ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            axMapControl1.CurrentTool = null;
            if (MessageBox.Show
                ("需要先合并两个面元素，再执行该操作（为了实验效果，最好要两个离散元素)。", "提示",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }
            IGraphicsContainerSelect pGC = axMapControl1.Map as IGraphicsContainerSelect;
            //IGraphicsContainerSelect：由Map转换，用于管理所选元素
            if (pGC.ElementSelectionCount != 0)
            {
                IEnumElement pEelement = pGC.SelectedElements;
                //IEnumElement：一个元素枚举，提供reset（）和next（）方法
                //IEnumFeature：用于要素枚举
                pEelement.Reset();
                IElement pelement = pEelement.Next();
                while (pelement != null)
                {
                    IGeometry pG = pelement.Geometry;
                    int pointcount = (pG as IPointCollection).PointCount;
                    int GeometryCount = (pG as IGeometryCollection).GeometryCount;
                    int segmentcount = (pG as ISegmentCollection).SegmentCount;
                    MessageBox.Show("Ipointcollection:" + pointcount.ToString() +
                        ";IGeometryCollection" + GeometryCount.ToString() +
                        ";ISegementCollection:" + segmentcount.ToString(), "注意个数");
                    pelement = pEelement.Next();
                }
            }
        }

        private void 交ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show
                 ("需要两个元素，准备好了吗?", "提示",
                 MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;
            IGraphicsContainerSelect pGCS = axMapControl1.Map as IGraphicsContainerSelect;
            //IGraphicsContainerSelect接口由MAP接口转换而来，用于管理所选元素
            if (pGCS.ElementSelectionCount == 2)
            {
                IEnumElement pEnumE = pGCS.SelectedElements;
                pEnumE.Reset();

                IElement pE1 = pEnumE.Next();
                IGeometry pG1 = pE1.Geometry;

                ITopologicalOperator2 pTopo = pG1 as ITopologicalOperator2;
                //ITopologicalOperator拓扑接口，交并补差，通常下一次操作两个元素
                //注：一般不用ITopologicalOperator接口，改用2甚至更高，因为1容易报错
                if (!pTopo.IsKnownSimple)
                    pTopo.Simplify();



                IElement pE2 = pEnumE.Next();
                IGeometry pG2 = pE2.Geometry;

                IGeometry pGeoOut = null;

                if (pE2 != null)
                {
                    (pG2 as ITopologicalOperator2).Simplify();

                    IRelationalOperator pRoper = pG1 as IRelationalOperator;
                    if (pRoper.Overlaps(pG2))
                        pGeoOut = pTopo.Intersect(pG2, esriGeometryDimension.esriGeometry2Dimension);
                }
                if (pGeoOut != null)
                {
                    IRgbColor rgbcolor = new RgbColorClass();
                    rgbcolor.Blue = 255;
                    ISimpleFillSymbol fillsymbol = new SimpleFillSymbolClass();
                    fillsymbol.Color = rgbcolor;
                    fillsymbol.Style = esriSimpleFillStyle.esriSFSForwardDiagonal;
                    IElement pelement = new PolygonElementClass();
                    pelement.Geometry = pGeoOut;
                    IFillShapeElement pfillelement = pelement as IFillShapeElement;
                    pfillelement.Symbol = fillsymbol as IFillSymbol;
                    (axMapControl1.Map as IGraphicsContainer).AddElement(pelement, 0);
                    axMapControl1.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
                }
            }
        }

        private void 并ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show
               ("需要两个元素，准备好了吗?", "提示",
               MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;
            IGraphicsContainerSelect pGCS = axMapControl1.Map as IGraphicsContainerSelect;
            //IGraphicsContainerSelect接口由MAP接口转换而来，用于管理所选元素
            if (pGCS.ElementSelectionCount == 2)
            {
                IEnumElement pEnumE = pGCS.SelectedElements;
                pEnumE.Reset();

                IElement pE1 = pEnumE.Next();
                IGeometry pG1 = pE1.Geometry;

                ITopologicalOperator2 pTopo = pG1 as ITopologicalOperator2;
                //ITopologicalOperator拓扑接口，交并补差，通常下一次操作两个元素
                //注：一般不用ITopologicalOperator接口，改用2甚至更高，因为1容易报错
                if (!pTopo.IsKnownSimple)
                    pTopo.Simplify();


                IElement pE2 = pEnumE.Next();
                IGeometry pG2 = pE2.Geometry;

                IGeometry pGeoOut = null;

                if (pE2 != null)
                {
                    (pG2 as ITopologicalOperator2).Simplify();

                    //1.求并
                    pGeoOut = pTopo.Union(pG2);


                }
                if (pGeoOut != null)
                {
                    IRgbColor rgbcolor = new RgbColorClass();
                    rgbcolor.Blue = 255;
                    ISimpleFillSymbol fillsymbol = new SimpleFillSymbolClass();
                    fillsymbol.Color = rgbcolor;
                    fillsymbol.Style = esriSimpleFillStyle.esriSFSForwardDiagonal;
                    IElement pelement = new PolygonElementClass();
                    pelement.Geometry = pGeoOut;
                    IFillShapeElement pfillelement = pelement as IFillShapeElement;
                    pfillelement.Symbol = fillsymbol as IFillSymbol;
                    (axMapControl1.Map as IGraphicsContainer).AddElement(pelement, 0);
                    axMapControl1.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
                }
            }
        }

        private void 差ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show
               ("需要两个元素，准备好了吗?", "提示",
               MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;
            IGraphicsContainerSelect pGCS = axMapControl1.Map as IGraphicsContainerSelect;
            //IGraphicsContainerSelect接口由MAP接口转换而来，用于管理所选元素
            if (pGCS.ElementSelectionCount == 2)
            {
                IEnumElement pEnumE = pGCS.SelectedElements;
                pEnumE.Reset();

                IElement pE1 = pEnumE.Next();
                IGeometry pG1 = pE1.Geometry;

                ITopologicalOperator2 pTopo = pG1 as ITopologicalOperator2;
                //ITopologicalOperator拓扑接口，交并补差，通常下一次操作两个元素
                //注：一般不用ITopologicalOperator接口，改用2甚至更高，因为1容易报错
                if (!pTopo.IsKnownSimple)
                    pTopo.Simplify();

                //真jb煞笔，眼睛长这么大 瞎啊！！！

                IElement pE2 = pEnumE.Next();
                IGeometry pG2 = pE2.Geometry;

                IGeometry pGeoOut = null;

                if (pE2 != null)
                {
                    (pG2 as ITopologicalOperator2).Simplify();

                    //3.求差
                    IRelationalOperator pRoper = pG1 as IRelationalOperator;
                    if (pRoper.Overlaps(pG2))
                        pGeoOut = pTopo.Difference(pG2);
                }
                if (pGeoOut != null)
                {
                    IRgbColor rgbcolor = new RgbColorClass();
                    rgbcolor.Blue = 255;
                    ISimpleFillSymbol fillsymbol = new SimpleFillSymbolClass();
                    fillsymbol.Color = rgbcolor;
                    fillsymbol.Style = esriSimpleFillStyle.esriSFSForwardDiagonal;
                    IElement pelement = new PolygonElementClass();
                    pelement.Geometry = pGeoOut;
                    IFillShapeElement pfillelement = pelement as IFillShapeElement;
                    pfillelement.Symbol = fillsymbol as IFillSymbol;
                    (axMapControl1.Map as IGraphicsContainer).AddElement(pelement, 0);
                    axMapControl1.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
                }
            }
        }

        private void 异或ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show
                 ("需要两个元素，准备好了吗?", "提示",
                 MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;
            IGraphicsContainerSelect pGCS = axMapControl1.Map as IGraphicsContainerSelect;
            //IGraphicsContainerSelect接口由MAP接口转换而来，用于管理所选元素
            if (pGCS.ElementSelectionCount == 2)
            {
                IEnumElement pEnumE = pGCS.SelectedElements;
                pEnumE.Reset();

                IElement pE1 = pEnumE.Next();
                IGeometry pG1 = pE1.Geometry;

                ITopologicalOperator2 pTopo = pG1 as ITopologicalOperator2;
                //ITopologicalOperator拓扑接口，交并补差，通常下一次操作两个元素
                //注：一般不用ITopologicalOperator接口，改用2甚至更高，因为1容易报错
                if (!pTopo.IsKnownSimple)
                    pTopo.Simplify();

                IElement pE2 = pEnumE.Next();
                IGeometry pG2 = pE2.Geometry;

                IGeometry pGeoOut = null;

                if (pE2 != null)
                {
                    (pG2 as ITopologicalOperator2).Simplify();
                    //4.求异或
                    IRelationalOperator pRoper = pG1 as IRelationalOperator;
                    if (pRoper.Overlaps(pG2))
                        pGeoOut = pTopo.SymmetricDifference(pG2);


                }
                if (pGeoOut != null)
                {
                    IRgbColor rgbcolor = new RgbColorClass();
                    rgbcolor.Blue = 255;
                    ISimpleFillSymbol fillsymbol = new SimpleFillSymbolClass();
                    fillsymbol.Color = rgbcolor;
                    fillsymbol.Style = esriSimpleFillStyle.esriSFSForwardDiagonal;
                    IElement pelement = new PolygonElementClass();
                    pelement.Geometry = pGeoOut;
                    IFillShapeElement pfillelement = pelement as IFillShapeElement;
                    pfillelement.Symbol = fillsymbol as IFillSymbol;
                    (axMapControl1.Map as IGraphicsContainer).AddElement(pelement, 0);
                    axMapControl1.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
                }
            }

        }

        private void 缓冲ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show
                           ("需要两个元素，准备好了吗?", "提示",
                           MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;
            IGraphicsContainerSelect pGCS = axMapControl1.Map as IGraphicsContainerSelect;
            //IGraphicsContainerSelect接口由MAP接口转换而来，用于管理所选元素
            if (pGCS.ElementSelectionCount == 2)
            {
                IEnumElement pEnumE = pGCS.SelectedElements;
                pEnumE.Reset();

                IElement pE1 = pEnumE.Next();
                IGeometry pG1 = pE1.Geometry;

                ITopologicalOperator2 pTopo = pG1 as ITopologicalOperator2;
                //ITopologicalOperator拓扑接口，交并补差，通常下一次操作两个元素
                //注：一般不用ITopologicalOperator接口，改用2甚至更高，因为1容易报错
                if (!pTopo.IsKnownSimple)
                    pTopo.Simplify();

                IElement pE2 = pEnumE.Next();
                IGeometry pG2 = pE2.Geometry;

                IGeometry pGeoOut = null;

                if (pE2 != null)
                {
                    (pG2 as ITopologicalOperator2).Simplify();
                    //5.求缓冲
                    pGeoOut = pTopo.Buffer(20);
                }
                if (pGeoOut != null)
                {
                    IRgbColor rgbcolor = new RgbColorClass();
                    rgbcolor.Blue = 255;
                    ISimpleFillSymbol fillsymbol = new SimpleFillSymbolClass();
                    fillsymbol.Color = rgbcolor;
                    fillsymbol.Style = esriSimpleFillStyle.esriSFSForwardDiagonal;
                    IElement pelement = new PolygonElementClass();
                    pelement.Geometry = pGeoOut;
                    IFillShapeElement pfillelement = pelement as IFillShapeElement;
                    pfillelement.Symbol = fillsymbol as IFillSymbol;
                    (axMapControl1.Map as IGraphicsContainer).AddElement(pelement, 0);
                    axMapControl1.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
                }
            }
        }

        private void 凸包ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show
               ("需要两个元素，准备好了吗?", "提示",
               MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;
            IGraphicsContainerSelect pGCS = axMapControl1.Map as IGraphicsContainerSelect;
            //IGraphicsContainerSelect接口由MAP接口转换而来，用于管理所选元素
            if (pGCS.ElementSelectionCount == 2)
            {
                IEnumElement pEnumE = pGCS.SelectedElements;
                pEnumE.Reset();

                IElement pE1 = pEnumE.Next();
                IGeometry pG1 = pE1.Geometry;

                ITopologicalOperator2 pTopo = pG1 as ITopologicalOperator2;
                //ITopologicalOperator拓扑接口，交并补差，通常下一次操作两个元素
                //注：一般不用ITopologicalOperator接口，改用2甚至更高，因为1容易报错
                if (!pTopo.IsKnownSimple)
                    pTopo.Simplify();


                IElement pE2 = pEnumE.Next();
                IGeometry pG2 = pE2.Geometry;

                IGeometry pGeoOut = null;

                if (pE2 != null)
                {
                    (pG2 as ITopologicalOperator2).Simplify();
                    //6.求凸包（最短暴露面）
                    IGeometryCollection pGc = new PolygonClass();
                    pGc.AddGeometry((pG1 as IGeometryCollection).get_Geometry(0));
                    pGc.AddGeometry((pG2 as IGeometryCollection).get_Geometry(0));
                    (pGc as ITopologicalOperator2).Simplify();
                    pGeoOut = (pGc as ITopologicalOperator2).ConvexHull();
                }
                if (pGeoOut != null)
                {
                    IRgbColor rgbcolor = new RgbColorClass();
                    rgbcolor.Blue = 255;
                    ISimpleFillSymbol fillsymbol = new SimpleFillSymbolClass();
                    fillsymbol.Color = rgbcolor;
                    fillsymbol.Style = esriSimpleFillStyle.esriSFSForwardDiagonal;
                    IElement pelement = new PolygonElementClass();
                    pelement.Geometry = pGeoOut;
                    IFillShapeElement pfillelement = pelement as IFillShapeElement;
                    pfillelement.Symbol = fillsymbol as IFillSymbol;
                    (axMapControl1.Map as IGraphicsContainer).AddElement(pelement, 0);
                    axMapControl1.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
                }
            }
        }

        private void 图层名称ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            //清空下拉框中的元素
            IMap pMap = axMapControl1.Map;
            for (int i = 0; i < pMap.LayerCount; i++)
            {
                string name = pMap.get_Layer(i).Name;
                //将图层名称赋值给name
                comboBox1.Items.Add(name);
                //将name值输入到下拉框中
                comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
                //设置comboBox格式为DropDownList，为了防止出现可以对下拉框值进行修改的情况
                comboBox1.SelectedIndex = 0;
            }
        }

        private void 属性查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            axMapControl1.CurrentTool = null;
            currentT = "Identify";
            axMapControl1.MousePointer = esriControlsMousePointer.esriPointerIdentify;
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void 编辑ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 投影信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            axMapControl1.CurrentTool = null;
            currentT = "Draw_point_project";
            axMapControl1.MousePointer = esriControlsMousePointer.esriPointerCrosshair;
        }

        private void 唯一值符号化ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ////UniqueValueSymbolFrm frm = new UniqueValueSymbolFrm(axMapControl1);
            //frm.ShowDialog();
            //axMapControl1.Refresh(esriViewDrawPhase.esriViewGeography, null, axMapControl1.Extent);
            //axTOCControl1.Update();
        }
    }
}



    
