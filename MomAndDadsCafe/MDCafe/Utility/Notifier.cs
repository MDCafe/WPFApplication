using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using WhatsAppApi;

namespace MDCafe.Utility
{
    class Notifier
    {
        public void SendMessage()
        {
            ////WhatsApp wa = new WhatsApp("918971503323", "865980025606754", "Goku", true);
            //WhatsApp wa = new WhatsApp("918105446053", Base64Encode("v2") , "Goku", true);
            ////eb+rLdDbcsKrfsodefDuqRSRxhU=
            ////Base64Encode("867802024224934")

            //wa.OnConnectSuccess += () =>
            //{
            //    //wa.SendMessage("8105446053", "Hello");

            //    wa.OnLoginSuccess += (phno, data) =>
            //    {
            //        wa.SendMessage("8105446053", "Hello");
            //    };

            //    wa.OnLoginFailed += (data) =>
            //    {
            //        System.Diagnostics.Debug.Write("Login failed");
            //    };
            //    wa.Login();
            //};

            //wa.Connect();
            
            //wa.Disconnect();

            //wa.OnConnectFailed += (ex) =>
            //{
            //    System.Diagnostics.Debug.Write("Login failed" + ex.Message);
            //};
        }

        public static string Base64Encode(string text)
        {
            return System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(text));
        }

        //WhatsApp wa = new WhatsApp(from, Base64Encode("password"), "abc", false, false);


    }
}
