using Newtonsoft.Json;
using SmartHouse.Models.Storage;
using SmartHouse.ViewModels;
using System.Collections.Generic;

namespace SmartHouse.ViewModels
{

    public class GroupSourceModel : DeviceModel
    {
        public override ViewModel Clone()
        {
            var m = new DeviceModel();
            m.Assign(this);
            return m;
        }

        public GroupSourceModel()
        {
            Device = new Device() {
                Icon = "device_groupsource.png",
                Type = DeviceType.Group,
                Name = "Группа"
            };
        }
    }
}
