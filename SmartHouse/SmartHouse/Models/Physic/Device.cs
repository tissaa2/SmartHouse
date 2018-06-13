using System;
using System.Collections.Generic;
using System.Text;
using SmartHouse.Services;
using System.Xml.Serialization;
using SmartHouse.Models.Logic;

namespace SmartHouse.Models.Physics
{
    [XmlInclude(typeof(Dimmer))]
    [XmlInclude(typeof(Panel))]
    public class Device : BaseEntity<UID>
    {
        private static void Add(Device d)
        {
            Devices.Add(d.ID, d);
        }

        public static void Populate()
        {
            Devices.Clear();
            Add(new Dimmer(0x010001, 8));
            Add(new Dimmer(0x010002, 8));
            Add(new Dimmer(0x010003, 4));
            Add(new Dimmer(0x010004, 8));
            Add(new Dimmer(0x010005, 4));
            Add(new Dimmer(0x010006, 4));
            Add(new Dimmer(0x010007, 6));
            Add(new Dimmer(0x010008, 8));
            Add(new Dimmer(0x010009, 4));
            Add(new Panel(0x020001, 10, 8));
            Add(new Panel(0x020002, 10, 4));
            Add(new Panel(0x020003, 10, 6));
            Add(new Panel(0x020004, 10, 6));
            Add(new Panel(0x020005, 10, 4));
            Add(new Panel(0x020006, 10, 8));
            Add(new Panel(0x020007, 10, 4));
            Add(new Panel(0x020008, 10, 2));
            Add(new Panel(0x020009, 10, 4));
        }

        public static Dictionary<UID, Device> Devices { get; set; } = new Dictionary<UID, Device>();
        public static Device GetDevice(UID id)
        {
            if (Devices.ContainsKey(id))
                return Devices[id];
            else
                return null;
        }

        public static Server CAN = null;

        public List<InputPort> Inputs { get; set; }
        public List<OutputPort> Outputs { get; set; }

        public virtual void Init(UID id, int inputsCount, int outputsCount, bool randomValues = false)
        {
            double v;
            ID = id;
            Random rnd = new Random();
            Inputs = new List<InputPort>();
            Outputs = new List<OutputPort>();
            for (int i = 0; i < inputsCount; i++)
            {
                v = randomValues ? rnd.NextDouble() : 0;
                Inputs.Add(new InputPort() { ID = i, Value = v });
            }
            for (int i = 0; i < outputsCount; i++)
            {
                v = randomValues ? rnd.NextDouble() : 0;
                Outputs.Add(new OutputPort() { ID = i, Value = v });
            }
        }

        private List<T> CloneList<T>(List<T> source) where T: Port
        {
            List<T> rl = new List<T>();
            foreach(T i in source)
            {
                rl.Add(i.Clone() as T);
            }
            return rl;
        }

        public virtual Device Assign(Device source)
        {
            Inputs = CloneList<InputPort>(source.Inputs);
            Outputs = CloneList<OutputPort>(source.Outputs);
            ID = source.ID;
            return this;
        }

        public virtual Device Clone()
        {
            throw new NotImplementedException();
        }

        public Device()
        {

        }

        /// public void SendCommand(Server can, )

        public void SendCommand()
        {

        }

        public override string ToString()
        {
            return String.Format("{0} {1}(in: {2}, out: {3})", this.ID, this.GetType().Name, Inputs.Count, Outputs.Count);
        }

    }
}
