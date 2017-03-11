using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefSharp;
using CefSharp.OffScreen;

namespace ODOAI
{
    public partial class LoginForm : Form
    {
        private ChromiumWebBrowser m_Browser { get; set; }

        public LoginForm()
        {
            InitializeComponent();

            CefSettings CS = new CefSettings();
            CS.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/56.0.2924.87 Safari/537.36";
            CS.WindowlessRenderingEnabled = true;
            //CS.CefCommandLineArgs.Add("disable-gpu", "1");
            CS.CefCommandLineArgs.Add("disable-gpu-vsync", "1");
            CS.LogSeverity = LogSeverity.Disable;
            CS.CefCommandLineArgs.Add("no-proxy-server", "1");
            CS.CefCommandLineArgs.Add("log-level", "3");
            Cef.Initialize(CS);

            BrowserSettings BS = new BrowserSettings();
            //BS.WindowlessFrameRate = 30;
            m_Browser = new ChromiumWebBrowser("http://lp.darkorbit.com/", BS);
            m_Browser.FrameLoadEnd += FrameLoadEnd;
        }

        private void FrameLoadEnd(object sender, FrameLoadEndEventArgs e)
        {
            #if DEBUG
            Console.WriteLine($"[{e.HttpStatusCode.ToString()}] {e.Url}");
            #endif

            if (!e.Url.Contains("darkorbit.com"))
            {
                MessageBox.Show("Failed to load DO website!");
                #if DEBUG
                MessageBox.Show($"URL: {e.Url}");
                #endif
                return;
            }

            if (e.HttpStatusCode == 200)    //HTTP OK
            {
                if (e.Url.Equals("https://lp.darkorbit.com/"))
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        btnSubmit.Enabled = true;
                    });
                }

                // Logged in...
                if (e.Url.Contains(".darkorbit.com/indexInternal.es?action=internalStart"))
                {

                }
            }
        }

        private void BtnSubmit_Click(object sender, EventArgs e)
        {
            if (!m_Browser.Address.Equals("https://lp.darkorbit.com/"))
            {
                btnSubmit.Enabled = false;
                return;
            }

            m_Browser.EvaluateScriptAsync("document.body.style.overflow ='hidden'");
            var JS = m_Browser.EvaluateScriptAsync("document.getElementById(\"bgcdw_login_form_username\").value = \""
                        + tbUsername.Text
                        + "\";\ndocument.getElementById(\"bgcdw_login_form_password\").value = \""
                        + tbPassword.Text
                        + "\";");
            JS.ContinueWith(task =>
            {
                m_Browser.EvaluateScriptAsync("document.getElementsByClassName('bgcdw_login_form')[0].submit();");
            });
        }
    }
}
