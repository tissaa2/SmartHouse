using System;
using System.Collections.Generic;
using System.Text;
using SmartHouse.Services;

namespace SmartHouse.Models.CAN
{
    public class Device
    {
        private static void Add(int uid, Device d)
        {
            d.ID = uid;
            Devices.Add(d.ID, d);
        }

        public static Device Dimmer(int outputs)
        {
            Dimmer d = new Models.CAN.Dimmer();
            d.Init(0, outputs, true);
            return d;
        }

        public static Device Panel(int inputs, int outputs)
        {
            Panel p = new Models.CAN.Panel();
            p.Init(inputs, outputs, true);
            return p;
        }

        public static void Polpulate()
        {
            Devices.Clear();
            Add(1, Dimmer(8));
            Add(2, Dimmer(8));
            Add(3, Dimmer(4));
            Add(4, Dimmer(8));
            Add(5, Dimmer(4));
            Add(6, Dimmer(4));
            Add(7, Dimmer(6));
            Add(8, Dimmer(8));
            Add(9, Dimmer(4));
            Add(10, Panel(10, 8));
            Add(11, Panel(10, 4));
            Add(12, Panel(10, 6));
            Add(13, Panel(10, 6));
            Add(14, Panel(10, 4));
            Add(15, Panel(10, 8));
            Add(16, Panel(10, 4));
            Add(17, Panel(10, 2));
            Add(18, Panel(10, 4));
        }

        public static Dictionary<UID, Device> Devices = new Dictionary<UID, Device>();
        public static Device GetDevice(UID id)
        {
            if (Devices.ContainsKey(id))
                return Devices[id];
            else
                return null;
        }

        public static Server CAN = null;

        public UID ID;
        public List<InputPort> Inputs;
        public List<OutputPort> Outputs;

        public virtual void Init(int inputsCount, int outputsCount, bool randomValues = false)
        {
            double v;
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
