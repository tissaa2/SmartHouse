using System;
using System.Collections.Generic;
using System.Text;
using SmartHouse.Services;
using System.Xml.Serialization;
using SmartHouse.Models.Logic;
using System.Collections.ObjectModel;
using SmartHouse.Models.Packets;
using System.Threading.Tasks;
using System.Threading;
using System.Reflection;
using System.Linq;

namespace SmartHouse.Models.Physics
{
    public class PDevice : IconEntity<UID>
    {
        private static Dictionary<byte, Type> deviceTypes = null;
        public static Dictionary<byte, Type> DeviceTypes
        {
            get
            {
                if (deviceTypes == null)
                    LoadDeviceTypes();
                return deviceTypes;
            }
        }

        public static void LoadDeviceTypes()
        {
            Type p = typeof(PDevice);
            deviceTypes = new Dictionary<byte, Type>();
            foreach (Type t in Assembly.GetExecutingAssembly().GetTypes())
            {
                if (t.IsSubclassOf(p))
                {
                    var a = t.GetCustomAttribute<DeviceTypeAttribute>();
                    if (a != null)
                    {
                        deviceTypes.Add(a.Type, t);
                    }
                }
            }
        }


        // public static Dictionary<UID, Device> Devices { get; set; } = new Dictionary<UID, Device>();
        private static ObservableCollection<PDevice> all = null;
        public static ObservableCollection<PDevice> All
        {
            get
            {
                if (all == null)
                {
                    all = new ObservableCollection<PDevice>();
                    LoadAllAsync();
                }
                return all;
            }
        }



        public static async void Init()
        {

        }

        public static void AddDevice(Packet packet, DuplexStream stream)
        {
            AutodetectResponse r = AutodetectResponse.Read(stream);
            if (PDevice.DeviceTypes.ContainsKey(r.DeviceType))
            {
                var id = new UID(r.UID[3], r.UID[2], r.UID[1]);
                var d = All.FirstOrDefault(e => e.ID == id);
                if (d == null)
                {
                    d = Activator.CreateInstance(PDevice.DeviceTypes[r.DeviceType]) as PDevice;
                    d.Init(id, r.InputsCount, r.OutputsCount, r.ScenesCount);
                    all.Add(d);
                    Log.Write("Device {0} found ", d);
                }
            }
            // Log.Write("Command {0} executed: result = {1}", message, (commandResponsePacket.Result == 0) ? "success" : "failure");
        }

        public static async Task LoadAllAsync()
        {
            await Task.Run(() =>
            {
                while (!Client.Instance.Initialized)
                    Thread.Sleep(100);
                Client.Instance.SendAndProcessResponses(Client.CurrentServer, Packet.AutodetectRequest, 20000, "autodetect", AddDevice);
            });
        }

        /* public static Device GetDevice(UID id)
        {
            if (Devices.ContainsKey(id))
                return Devices[id];
            else
                return null;
        } */

        public static Server CAN = null;

        public override string Icon { get => "pdevice_device.png"; }
        public virtual string TypeName { get => "Устройство"; }

        public List<InputPort> Inputs { get; set; }
        public List<OutputPort> Outputs { get; set; }
        public byte ScenesCount { get; set; }

        public virtual void Init(UID id, int inputsCount, int outputsCount, int scenesCount, bool randomValues = false)
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

        public virtual PDevice Assign(PDevice source)
        {
            Inputs = CloneList<InputPort>(source.Inputs);
            Outputs = CloneList<OutputPort>(source.Outputs);
            ID = source.ID;
            return this;
        }

        public virtual PDevice Clone()
        {
            throw new NotImplementedException();
        }

        public PDevice()
        {

        }

        /// public void SendCommand(Server can, )

        public void SendCommand()
        {

        }

        public override string ToString()
        {
            return String.Format("{0} {1}(in: {2}, out: {3}, scenes: {4})", this.ID, this.GetType().Name, Inputs.Count, Outputs.Count, ScenesCount);
        }

    }
}
