﻿using MintIceApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace MintIceApp.Services
{
    public class AlternateColorDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate EvenTemplate { get; set; }
        public DataTemplate UnevenTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            // TODO: Maybe some more error handling here
            return ((ObservableCollection<Ingredient>)((ListView)container).ItemsSource).IndexOf(item as Ingredient) % 2 == 0 ? EvenTemplate : UnevenTemplate;
        }
    }
}
