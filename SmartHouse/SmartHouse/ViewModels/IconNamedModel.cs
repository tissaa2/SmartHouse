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
        public virtual string Icon
        {
            get => icon;
            set
            {
                CheckIsDirty(icon, value, "Icon", () => icon = value);
            }
        }

        public override void Setup(params object[] args)
        {
            base.Setup(args);
            var t = Target as IconNamedEntity;
            if (t != null)
            {
                icon = t.Icon;
            }
        }

        public override void Apply()
        {
            base.Apply();
            if (Target is IconNamedEntity)
                (Target as IconNamedEntity).Icon = Icon;
        }

        public IconNamedModel()
        {

        }

        public IconNamedModel(params object[] args): base(args)
        {

        }

    }
}
