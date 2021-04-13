using System;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Text;
using SmartHouse.Models.Logic;
using System.ComponentModel;
using SmartHouse.Models;

namespace SmartHouse.ViewModels
{

    public class NamedModel: ViewModel
    {
        protected string name;
        public string Name
        {
            get { return name; }
            set
            {
                CheckIsDirty(name, value, "Name", () => { name = value; });
            }
        }


        public NamedModel()
        {
        }

        public NamedModel(string name, string icon)
        {

        }

        public override void Apply(object target)
        {
            base.Apply(target);
            if (target is NamedEntity)
                (target as NamedEntity).Name = Name;
        }
    }
}
