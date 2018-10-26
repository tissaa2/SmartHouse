using System.Linq;
using System;
using SmartHouse.Models.Logic;
using SmartHouse.Models;
using Newtonsoft.Json;
using Xamarin.Forms;
using Device = SmartHouse.Models.Logic.Device;
using System.Collections.Generic;

namespace SmartHouse.Models
{
    public class EntityInfo
    {
        public IconNameAttribute Props { get; set; }
        public Type Type { get; set; } 
        public int ID { get; set; }

        public EntityInfo()
        {
        }

        public override string ToString()
        {
            return Props.Name;
        }

        public object CreateEntity()
        {
            return Activator.CreateInstance(Type);
        }

    }
}