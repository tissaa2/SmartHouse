using System;
using System.Collections.ObjectModel;
using System.Text;
using SmartHouse.Models.Logic;
using Xamarin.Forms;

namespace SmartHouse
{
    public class ViewEditTemplateSelector : DataTemplateSelector
    {
        public object EditedItem { get; set; }
        public DataTemplate ViewTemplate { get; set; }
        public DataTemplate EditTemplate { get; set; }
        public void SetEditedItem(object item)
        {
            EditedItem = item;
            
        }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            return EditedItem == item ? EditTemplate : ViewTemplate;
        }

        public ViewEditTemplateSelector(DataTemplate viewTemplate, DataTemplate editTemplate)
        {
            ViewTemplate = viewTemplate;
            EditTemplate = editTemplate;
        }

        public ViewEditTemplateSelector()
        {

        }

    }
}
