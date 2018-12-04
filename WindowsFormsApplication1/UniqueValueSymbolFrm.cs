using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
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
    public partial class UniqueValueSymbolFrm : Form
    {
        private IMap m_map;
        private IFeatureLayer pLayer2S;
        private string strRendererField;
        public UniqueValueSymbolFrm()
        {
            InitializeComponent();
        }

        public UniqueValueSymbolFrm(IMap map)
        {
            InitializeComponent();
            this.m_map = map;
        }
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void UniqueValueSymbolFrm_Load(object sender, EventArgs e)
        {
            //            ComboBoxLayerAddItems();

            //        }
            //        private void ComboBoxLayerAddItems(IFeatureLayer pLayer2S);
            //        {
            //         IFields fields = pLayer2S.FeatureClass.Fields;
            //        IField field = null;
            //        comboBox_Fields.Items.Clear();
            //            for (int i = 0; i<fields.FieldCount; i++)
            //            {
            //                field = fields.get_Field(i);
            //                if (field.Type != esriFieldType.esriFieldTypeGeometry)
            //                    comboBox_Fields.Items.Add(field.Name);
            //            }
            //    comboBox_Fields.Update();
            //        }
            //            private void ComboBoxLayerAddItems()
            //        {
            //            for (int i = 0; i < m_map.LayerCount; i++)
            //            {
            //                ILayer ply = m_map.get_Layer(i);
            //                if (ply is IFeatureLayer)
            //                {
            //                    comboBox_Layer.Items.Add(ply.Name);
            //                }
            //            }
            //            //IEnumLayer layers = GetLayers();
            //            //if (GetLayers() == null)
            //            //    return;

            //            //layers.Reset();
            //            //ILayer layer = layers.Next();

            //            //while (layer != null)
            //            //{
            //            //    comboBox_Layer.Items.Add(layer.Name);
            //            //    layer = layers.Next();
            //            //}
            //            if (comboBox_Layer.Items.Count != 0)
            //                comboBox_Layer.SelectedIndex = 0;
            //            if (comboBox_Fields.Items.Count != 0)
            //                comboBox_Fields.SelectedIndex = 0;
            //        }
            //    }
            //}
        }
    }
}
