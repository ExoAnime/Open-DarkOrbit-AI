using CefSharp;
using System.Windows.Forms;

namespace ODOAI
{
    public class DOLifeSpanHandler : ILifeSpanHandler
    {
        bool ILifeSpanHandler.OnBeforePopup(IWebBrowser browserControl, IBrowser browser, IFrame frame, string targetUrl, string targetFrameName, WindowOpenDisposition targetDisposition, bool userGesture, IPopupFeatures popupFeatures, IWindowInfo windowInfo, IBrowserSettings browserSettings, ref bool noJavascriptAccess, out IWebBrowser newBrowser)
        {
            //Default behaviour
            newBrowser = null;
            return false;
        }

        void ILifeSpanHandler.OnAfterCreated(IWebBrowser browserControl, IBrowser browser)
        {

        }

        public void OnBeforeClose(IWebBrowser browserControl, IBrowser browser)
        {

        }

        bool ILifeSpanHandler.DoClose(IWebBrowser browserControl, IBrowser browser)
        {
            //TODO: Handle correctly and reconnect properly :)
            MessageBox.Show("DarkOrbit client has closed.");
            return true;
        }
    }
}
