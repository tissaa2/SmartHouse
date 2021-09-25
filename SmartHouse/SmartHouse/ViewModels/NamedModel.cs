using System;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Text;
using SmartHouse.Models.Storage;
using System.ComponentModel;
using SmartHouse.Models;

namespace SmartHouse.ViewModels
{

    public class NamedModel : ViewModel
    {

        protected string name;
        public virtual string Name
        {
            get { return name; }
            set
            {
                CheckIsDirty(name, value, "Name", () => { name = value; });
            }
        }

        public override void Setup(params object[] args)
        {
            base.Setup(args);
            var t = Target as NamedEntity;
            if (t != null)
            {
                name = t.Name;
            }
        }

        public override void Apply()
        {
            if (Target is NamedEntity)
                (Target as NamedEntity).Name = Name;
        }

        public NamedModel()
        {

        }

        public NamedModel(params object[] args) : base(args)
        {

        }
    }
}
