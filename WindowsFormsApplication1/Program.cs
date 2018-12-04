using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()


        {
            ESRI.ArcGIS.RuntimeManager.Bind(ESRI.ArcGIS.ProductCode.EngineOrDesktop);//许可
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
            // WelcomeFrm.LoadAndRun(new Form1());
            //Form1 form1 = new Form1();
            //if(form1.ShowDialog()==DialogResult.OK)
            //{
            //    Application.Run(new Eagle_eye());
            //}
            
        }
    }
}
