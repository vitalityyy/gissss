using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
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
    public partial class LabelFrm : Form
    {

        ITextElement pTextelement = new TextElementClass();
        private IGeoFeatureLayer pGeoFeatureL;
        public LabelFrm(IGeoFeatureLayer pGeoFeatureL)
        {
            InitializeComponent();
            this.pGeoFeatureL = pGeoFeatureL;
        }

        private void LabelFrm_Load(object sender, EventArgs e)
        {
            IFeatureLayer pFl = pGeoFeatureL as IFeatureLayer;
            for (int i = 0; i < pFl.FeatureClass.Fields.FieldCount; i++)
            {
                if (pFl.FeatureClass.Fields.get_Field(i).Name != pFl.FeatureClass.ShapeFieldName)
                {
                    this.comboBox1_fields.Items.Add(pFl.FeatureClass.Fields.get_Field(i).Name.ToString());
                }
            }
            this.comboBox1_fields.SelectedIndex = 0;
            pTextelement.Text = "这是标注的字体样式";
        }

        private void btn_style_Click(object sender, EventArgs e)
        {
            TextFrm child = new TextFrm(pTextelement);
            child.richTextBox1.ReadOnly = true;
            child.ShowDialog();
            ITextSymbol pts = pTextelement.Symbol;
            string ziti = "";
            if (pts.Font.Bold)
                ziti = "粗体";
            if (pts.Font.Italic)
                ziti = "倾斜";
            if (pts.Font.Strikethrough)
                ziti = "中划线";
            if (pts.Font.Underline)
                ziti = "下划线";

            ziti += " " + pts.Font.Size.ToString() + "号 R" + (pts.Color as IRgbColor).Red.ToString() + "G" + (pts.Color as IRgbColor).Green.ToString() + "B" + (pts.Color as IRgbColor).Blue.ToString();
            LabelName.Text = ziti;

        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            string LabelName = comboBox1_fields.Text;
            ILabelEngineLayerProperties pLabelEngineLP = new LabelEngineLayerPropertiesClass();
            pGeoFeatureL.AnnotationProperties.Clear();
            switch (pGeoFeatureL.FeatureClass.ShapeType)
            {
                case ESRI.ArcGIS.Geometry.esriGeometryType.esriGeometryPolyline:
                    ILineLabelPosition pLineLableP = new LineLabelPositionClass();
                    if (radio_horizontal.Checked)
                    {
                        pLineLableP.Horizontal = true;
                        pLineLableP.Parallel = false;
                        pLineLableP.Perpendicular = false;
                        pLineLableP.OnTop = true;
                    }
                    else if (radio_perpendcular.Checked)
                    {
                        pLineLableP.Horizontal = false;
                        pLineLableP.Parallel = false;
                        pLineLableP.Perpendicular = true;
                        pLineLableP.Above = true;
                    }
                    else if (radio_curve.Checked)
                    {
                        pLineLableP.ProduceCurvedLabels = true;
                        pLineLableP.Parallel = true;
                        pLineLableP.Above = true;
                    }
                    pLabelEngineLP.BasicOverposterLayerProperties.LineLabelPosition = pLineLableP;
                    break;
                case ESRI.ArcGIS.Geometry.esriGeometryType.esriGeometryPoint:
                    if (radio_weirao_point.Checked)
                    {
                        pLabelEngineLP.BasicOverposterLayerProperties.PointPlacementMethod =
                            esriOverposterPointPlacementMethod.esriAroundPoint;
                    }
                    else if (radio_top_point.Checked)
                    {
                        pLabelEngineLP.BasicOverposterLayerProperties.PointPlacementMethod =
                            esriOverposterPointPlacementMethod.esriOnTopPoint;
                        pLabelEngineLP.BasicOverposterLayerProperties.PointPlacementOnTop = true;
                    }
                    else if (radio_angle_point.Checked)
                    {
                        pLabelEngineLP.BasicOverposterLayerProperties.PointPlacementMethod =
                            esriOverposterPointPlacementMethod.esriSpecifiedAngles;
                        pLabelEngineLP.BasicOverposterLayerProperties.PointPlacementAngles = new double[] { (double)numeric_point.Value };
                    }
                    break;
                case ESRI.ArcGIS.Geometry.esriGeometryType.esriGeometryPolygon:
                    (pLabelEngineLP.BasicOverposterLayerProperties as
                        IBasicOverposterLayerProperties4).PlaceOnlyInsidePolygon = true;
                    if (radio_shuiping_polygon.Checked)
                    {
                        (pLabelEngineLP.BasicOverposterLayerProperties as
                            IBasicOverposterLayerProperties4).PolygonPlacementMethod =
                            esriOverposterPolygonPlacementMethod.esriAlwaysHorizontal;
                    }
                    else if (radio_yaosu_polygon.Checked)
                    {
                        (pLabelEngineLP.BasicOverposterLayerProperties as
                            IBasicOverposterLayerProperties4).PolygonPlacementMethod =
                            esriOverposterPolygonPlacementMethod.esriAlwaysStraight;
                    }
                    else if (radio_mixed_polygon.Checked)
                    {
                        (pLabelEngineLP.BasicOverposterLayerProperties as
                            IBasicOverposterLayerProperties4).PolygonPlacementMethod =
                            esriOverposterPolygonPlacementMethod.esriMixedStrategy;
                    }
                    break;
                default:
                    break;
            }
            if (radio_tongming.Checked)
                pLabelEngineLP.BasicOverposterLayerProperties.NumLabelsOption = esriBasicNumLabelsOption.esriOneLabelPerName;
            else if (radio_onlyone.Checked)
            {
                pLabelEngineLP.BasicOverposterLayerProperties.NumLabelsOption = esriBasicNumLabelsOption.esriOneLabelPerShape;
            }
            else if (radio_part.Checked)
            {
                pLabelEngineLP.BasicOverposterLayerProperties.NumLabelsOption = esriBasicNumLabelsOption.esriOneLabelPerPart;
            }
            IAnnotateLayerProperties pAnnotateLayerP;
            if (LabelName != null)
            {
                pLabelEngineLP.Symbol = pTextelement.Symbol;
                pLabelEngineLP.Expression = "[" + LabelName + "]";
                pAnnotateLayerP = pLabelEngineLP as IAnnotateLayerProperties;
                pAnnotateLayerP.DisplayAnnotation = true;
                pAnnotateLayerP.FeatureLayer = pGeoFeatureL;
                pGeoFeatureL.AnnotationProperties.Add(pAnnotateLayerP);
                pGeoFeatureL.DisplayAnnotation = true;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void buttonclose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
