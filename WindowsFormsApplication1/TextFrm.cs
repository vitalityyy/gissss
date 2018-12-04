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
    public partial class TextFrm : Form
    {
        ITextElement pTelement = null;
        public TextFrm()
        {
            InitializeComponent();
        }
        public TextFrm(ITextElement pte)
        {
            InitializeComponent();
            pTelement = pte;

        }
        private void TextFrm_Load(object sender, EventArgs e)
        {
            this.comboAccross.Items.Add("左对齐");
            this.comboAccross.Items.Add("右对齐");
            this.comboAccross.Items.Add("居中");
            this.comboAccross.Items.Add("两边对齐");

            this.comboVertical.Items.Add("上对齐");
            this.comboVertical.Items.Add("下对齐");
            this.comboVertical.Items.Add("居中");
            this.comboVertical.Items.Add("两边对齐");

            if (pTelement != null)
            {
                this.richTextBox1.Text = pTelement.Text;
                switch (pTelement.Symbol.HorizontalAlignment)
                {
                    case esriTextHorizontalAlignment.esriTHALeft:
                        this.comboAccross.SelectedIndex = 0;
                        break;
                    case esriTextHorizontalAlignment.esriTHARight:
                        this.comboAccross.SelectedIndex = 1;
                        break;
                    case esriTextHorizontalAlignment.esriTHACenter:
                        this.comboAccross.SelectedIndex = 2;
                        break;
                    case esriTextHorizontalAlignment.esriTHAFull:
                        this.comboAccross.SelectedIndex = 3;
                        break;
                    default:
                        break;
                }
                switch (pTelement.Symbol.VerticalAlignment)
                {
                    case esriTextVerticalAlignment.esriTVATop:
                        this.comboVertical.SelectedIndex = 0;
                        break;
                    case esriTextVerticalAlignment.esriTVABottom:
                        this.comboVertical.SelectedIndex = 1;
                        break;
                    case esriTextVerticalAlignment.esriTVACenter:
                        this.comboVertical.SelectedIndex = 2;
                        break;
                    case esriTextVerticalAlignment.esriTVABaseline:
                        this.comboVertical.SelectedIndex = 3;
                        break;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ITextSymbol pTxtSymbol = new TextSymbolClass();
        }
    }
}