using System;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Text;
using SmartHouse.Models.Storage;
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

        public override void Apply(object target)
        {
            base.Apply(target);
            if (target is IconNamedEntity)
                (target as IconNamedEntity).Icon = Icon;
        }

    }
}
