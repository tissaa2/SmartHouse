using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHouse.ViewModels.Devices.Physic
{
    public class InputPortModel: PortModel
    {
        public InputPortModel(int id) : base(id)
        {
        }

        public override string Name { get => String.Format("Вход {0}", ID); }
        public override string Icon { get => "inputport.png"; }

        public override ViewModel Clone()
        {
            return new InputPortModel(this.ID) { Value = this.Value };
        }


    }
}
