using SmartHouse.Models.Storage;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHouse.ViewModels.Helpers
{
    public class TypeInfo
    {
        public static Dictionary<DeviceType, TypeInfo> List = new Dictionary<DeviceType, TypeInfo>() {
            {DeviceType.Fan, new TypeInfo("Вентилятор", "device_fan.png", false)},
            {DeviceType.Lamp, new TypeInfo("Светильник", "device_lamp.png", false)},
            {DeviceType.MotionSensor, new TypeInfo("Датчик движения", "device_motionsensor.png", true)},
            {DeviceType.Panel, new TypeInfo("Кнопочная панель", "device_panel.png", true)},
            {DeviceType.Socket, new TypeInfo("Розетка", "device_socket.png", false)},
            {DeviceType.Switch, new TypeInfo("Выключатель", "device_switch.png", true)},
            {DeviceType.Group, new TypeInfo("Группа", "device_groupsource.png", true)},
        };

        public string Name { get; set; }
        public string Icon { get; set; }

        public bool IsInput { get; set; }
        public TypeInfo(string name, string icon, bool isInput)
        {
            Name = name;
            Icon = icon;
            IsInput = isInput;
        }
    }

}
