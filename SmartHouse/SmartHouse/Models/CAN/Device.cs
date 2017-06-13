using System;
using System.Collections.Generic;
using System.Text;
using SmartHouse.Services;

namespace SmartHouse.Models.CAN
{
    public class Device
    {
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

        public virtual void Init(int inputsCount, int outputsCount)
        {
            Inputs = new List<InputPort>();
            Outputs = new List<OutputPort>();
            for (int i = 0; i < inputsCount; i++)
                Inputs.Add(new InputPort() {ID = i, Value = 0 });
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
    }
}
