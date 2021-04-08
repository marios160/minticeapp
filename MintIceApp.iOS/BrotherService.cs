using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using MintIceApp.Services;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(MintIceApp.iOS.BrotherService))]
namespace MintIceApp.iOS
{
    class BrotherService : IBrotherService
    {
        public string PrintLabelAsync(string text)
        {
            throw new NotImplementedException();
        }
    }
}