using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class WelcomeFrm : Form
    {
        public WelcomeFrm()
        {
            InitializeComponent();
        }
        public void KillMe(object o, EventArgs e)
        {
            this.Close();
        }//关闭自身

      public static void LoadAndRun(Form form)
        {
            //订阅主窗体的创建事件
            form.HandleCreated += delegate
             {
                //启动新线程来显示welcomefrm窗体
                new Thread(new ThreadStart(delegate
                 {
                     WelcomeFrm welcomefrm = new WelcomeFrm();
                    //订阅窗体 的shown事件
                    welcomefrm.Shown += delegate
                     {
                        //通知窗体关闭自身

                        welcomefrm.Invoke(new EventHandler(welcomefrm.KillMe));
                         welcomefrm.Dispose();
                     };
                    //显示welcomefrm窗体
                    Application.Run(welcomefrm);

                 })).Start();
             };
            //显示主窗体
            Application.Run(form);

        }

        private void WelcomeFrm_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;//设置窗体为无标题栏
            //this.BackgroundImage = Image.FromFile("sadf.jpj");//图片背景

        }
    }
}
