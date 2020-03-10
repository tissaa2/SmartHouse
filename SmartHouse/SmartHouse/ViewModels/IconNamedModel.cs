using System;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Text;
using SmartHouse.Models.Logic;
using System.ComponentModel;
using SmartHouse.Models;

namespace SmartHouse.ViewModels
{

    public class IconNamedModel: NamedModel
    {
        private string icon;
        public string Icon
        {
            get => icon;
            set
            {
                CheckIsDirty(icon, value, "Icon", () => icon = value);
            }
        }

        public IconNamedModel()
        {
        }

        public IconNamedModel(string name, string icon)
        {

        }

        public override void Apply()
        {
            base.Apply();
            if (Target is IconEntity)
                (Target as IconEntity).Icon = Icon;
        }

    }
}
