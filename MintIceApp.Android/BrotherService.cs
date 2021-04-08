using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MintIceApp.Services;
using Com.Brother.Ptouch.Sdk;
using Xamarin.Forms;
using Android.Bluetooth;
using System.IO;
using System.Threading.Tasks;
using Plugin.Toast;

[assembly: Dependency(typeof(MintIceApp.Droid.BrotherService))]
namespace MintIceApp.Droid
{
    class BrotherService : IBrotherService
    {
        public string PrintLabelAsync(string text)
        {
            try
            {


                Printer p = new Printer();
                PrinterInfo pi = new PrinterInfo();
                pi.PrinterModel = PrinterInfo.Model.Ql820nwb;
                pi.EsPort = PrinterInfo.Port.Bluetooth;
                pi.MacAddress = "80:6F:B0:02:AF:71";
                p.SetPrinterInfo(pi);

                p.SetBluetooth(BluetoothAdapter.DefaultAdapter);
                p.SetBluetoothLowEnergy(Android.App.Application.Context, BluetoothAdapter.DefaultAdapter, 10000);

                
                    if (p.StartCommunication())
                    {

                        var val = p.StartPTTPrint(160, null);
                        p.ReplaceText(ChangePLtoENG(text));
                        p.ReplaceText(DateTime.Now.ToString("d"));
                        PrinterStatus status = p.FlushPTTPrint();
                        p.EndCommunication();
                        if (status.ErrorCode.ToString() != "ERROR_NONE")
                        {
                            Console.WriteLine(status.ErrorCode);
                            CrossToastPopUp.Current.ShowToastMessage(status.ErrorCode.ToString());
                            return status.ErrorCode.ToString();
                        }
                    }
               
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
            return string.Empty;
        }

        public static string ChangePLtoENG(string text)
        {
            string tmp = text.Replace("ą", "a");
            tmp = tmp.Replace("ć", "c");
            tmp = tmp.Replace("ę", "e");
            tmp = tmp.Replace("ł", "l");
            tmp = tmp.Replace("ń", "n");
            tmp = tmp.Replace("ó", "o");
            tmp = tmp.Replace("ś", "s");
            tmp = tmp.Replace("ź", "z");
            tmp = tmp.Replace("ż", "z");
            tmp = tmp.Replace("Ą", "A");
            tmp = tmp.Replace("Ć", "C");
            tmp = tmp.Replace("Ę", "E");
            tmp = tmp.Replace("Ł", "L");
            tmp = tmp.Replace("Ń", "N");
            tmp = tmp.Replace("Ó", "O");
            tmp = tmp.Replace("Ś", "S");
            tmp = tmp.Replace("Ź", "Z");
            tmp = tmp.Replace("Ż", "Z");

            return tmp;
        }

    }
}