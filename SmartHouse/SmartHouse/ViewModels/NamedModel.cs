using System;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Text;
using SmartHouse.Models.Storage;
using System.ComponentModel;
using SmartHouse.Models;

namespace SmartHouse.ViewModels
{

    public class NamedModel: ViewModel
    {
        public object Target { get; set; }
        protected string name;
        public string Name
        {
            get { return name; }
            set
            {
                CheckIsDirty(name, value, "Name", () => { name = value; });
            }
        }


        public NamedModel(string name, object target): base(target)
        {
            Name = name;
        }

        public override void Apply()
        {
            if (Target is NamedEntity)
                (Target as NamedEntity).Name = Name;
        }
    }
}
