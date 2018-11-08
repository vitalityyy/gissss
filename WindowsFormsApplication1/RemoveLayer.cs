using System;
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Carto;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    /// <summary>
    /// Command that works in ArcMap/Map/PageLayout
    /// </summary>
    [Guid("69ed5ce0-efe6-491e-b21b-3b5dd93e2697")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("WindowsFormsApplication1.RemoveLayer")]
    public sealed class RemoveLayer : BaseCommand
    {
        #region COM Registration Function(s)
        [ComRegisterFunction()]
        [ComVisible(false)]
        static void RegisterFunction(Type registerType)
        {
            // Required for ArcGIS Component Category Registrar support
            ArcGISCategoryRegistration(registerType);

            //
            // TODO: Add any COM registration code here
            //
        }

        [ComUnregisterFunction()]
        [ComVisible(false)]
        static void UnregisterFunction(Type registerType)
        {
            // Required for ArcGIS Component Category Registrar support
            ArcGISCategoryUnregistration(registerType);

            //
            // TODO: Add any COM unregistration code here
            //
        }

        #region ArcGIS Component Category Registrar generated code
        /// <summary>
        /// Required method for ArcGIS Component Category registration -
        /// Do not modify the contents of this method with the code editor.
        /// </summary>
        private static void ArcGISCategoryRegistration(Type registerType)
        {
            string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
            MxCommands.Register(regKey);
            ControlsCommands.Register(regKey);
        }
        /// <summary>
        /// Required method for ArcGIS Component Category unregistration -
        /// Do not modify the contents of this method with the code editor.
        /// </summary>
        private static void ArcGISCategoryUnregistration(Type registerType)
        {
            string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
            MxCommands.Unregister(regKey);
            ControlsCommands.Unregister(regKey);
        }

        #endregion
        #endregion
        private IHookHelper m_hookHelper = null;
        IMapControl3 m_mapcontrol = null;
        IFeatureLayer currentLayer = null;
        IActiveView m_activeView = null;
        IMap m_map = null;
        public RemoveLayer()
        {
            base.m_category = "Remove Layer"; //localizable text
            base.m_caption = "Remove Layer";  //localizable text 
            base.m_message = "Remove Layer";  //localizable text
            base.m_toolTip = "Remove Layer";  //localizable text
            base.m_name = "Remove Layer";   //unique id, non-localizable (e.g. "MyCategory_MyCommand")

            try
            {

                //string path = GetApplicationPath.ApplicationPath;
               // base.m_bitmap = new Bitmap(path + "\\removelayer_16.png");
            }
            catch (Exception ex)
            {
                MessageBox.Show("无效位图！！" + ex.Message, "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        #region Overridden Class Methods

       
        public override void OnCreate(object hook)
        {
          
            if (hook == null)
                return;
            m_hookHelper = new HookHelperClass();
            m_hookHelper.Hook = hook;


            
        }

       
        public override void OnClick()
        {
            if (m_hookHelper.Hook is IMapControl3)
            {
                m_mapcontrol = m_hookHelper.Hook as IMapControl3;
                currentLayer = m_mapcontrol.CustomProperty as IFeatureLayer;
                m_map = m_mapcontrol.Map;
                m_activeView = m_map as IActiveView;
            }

            if (m_map == null || currentLayer == null) return;
            m_map.DeleteLayer(currentLayer);
            m_activeView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, m_activeView.Extent);

        }

        #endregion
    }
}
