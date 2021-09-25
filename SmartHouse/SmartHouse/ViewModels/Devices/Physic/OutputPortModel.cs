using SmartHouse.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHouse.ViewModels.Devices.Physic
{
    public class OutputPortModel: PortModel
    {
        public override string Icon { get => "outputport.png"; }
        // public override string Name { get => "outputport.png"; }
        public override string Name { get => String.Format("Выход {0}", ID); }

        public OutputPortModel(int id) : base(id)
        {
            this.ID = id;
        }

        public override ViewModel Clone()
        {
            return new OutputPortModel(this.ID) {Value = this.Value };
        }
    }
}
