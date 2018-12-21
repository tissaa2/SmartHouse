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
using SmartHouse.Models.Packets.Processors.CAN;

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

        private static ObservableCollection<OutputPort> allOutputs = new ObservableCollection<OutputPort>();
        public static ObservableCollection<OutputPort> AllOutputs
        {
            get
            {
                if (allOutputs == null)
                {
                    allOutputs = new ObservableCollection<OutputPort>();
                    LoadAllAsync();
                }
                return allOutputs;
            }
        }

        private static ObservableCollection<InputPort> allInputs = new ObservableCollection<InputPort>();
        public static ObservableCollection<InputPort> AllInputs
        {
            get
            {
                if (allInputs == null)
                {
                    allInputs = new ObservableCollection<InputPort>();
                    LoadAllAsync();
                }
                return allInputs;
            }
        }

        /* public static void LoadInputsAsync()
        {
            foreach (var d in all)
                foreach (var i in d.Inputs)
                    allInputs.Add(i);
        } */

        public static PDevice Get(UID id)
        {
            return PDevice.All.FirstOrDefault(e => e.ID == id);
        }

        public static void Add(PDevice device)
        {
            if (all != null)
            {
                all.Add(device);
                /// Временная заплатка, ибо поиск устройств отрубает реле, на котором висит роутер
                if (!(device is Relay))
                    if (allOutputs != null)
                        foreach (var i in device.Outputs)
                            allOutputs.Add(i);
                if (allInputs != null)
                    foreach (var i in device.Inputs)
                        allInputs.Add(i);
            }
        }

        public static async void Init()
        {

        }

        // 2DO: Сделать постоянное чтение и обработку пакетов в сервере
        //public static void AddDevice(CANResponse source, byte[] data, ref int pos)
        //{
        //    AutodetectResponse r = AutodetectResponse.CreateFrom(source, data, ref pos);
        //    if (PDevice.DeviceTypes.ContainsKey(r.DeviceType))
        //    {
        //        var id = new UID(r.UID[2], r.UID[1], r.UID[0]);
        //        var d = all.FirstOrDefault(e => e.ID == id);
        //        if (d == null)
        //        {
        //            d = Activator.CreateInstance(PDevice.DeviceTypes[r.DeviceType]) as PDevice;
        //            d.Init(id, r.InputsCount, r.OutputsCount, r.ScenesCount);
        //            all.Add(d);
        //            Log.Write("Device {0} found ", d);
        //        }
        //    }
        //    // Log.Write("Command {0} executed: result = {1}", message, (commandResponsePacket.Result == 0) ? "success" : "failure");
        //}

        //public static void ProcessAutodetectPacket(Packet packet)
        //{
        //    if (packet.StartSequence[0] == '$')
        //        if (packet.Command == 0x031)
        //        {
        //            int p = 0;
        //            var cr = CANResponse.Read(packet.Data, ref p);
        //            if (cr.StartByte == 0xFF && cr.Command == 0x04)
        //            {
        //                AddDevice(cr, packet.Data, ref p);
        //            }
        //        }

        //}

        private static void Fk(byte uid, byte type, byte inpts, byte outpts)
        {
            var proc = CANDataProcessor.Processors[0x04];
            var p = (new Packet()
            {
                StartSequence = new byte[] { (byte)'#', (byte)'H', (byte)'L' },
                Command = 0x031,
                DataSize = 11,
                //                  conf     UID     strt  cmd   type   in     out    sc gm
                Data = new PacketDataStream(new byte[] { 0x05, 0, 0, uid, 0xFF, 0x04, type, inpts, outpts, 2, 0 })
            });
            CANPacket cp = CANPacket.Read(p.Data);
            proc.ProcessData(p.Data, cp);
        }


        public static async void LoadAllAsync()
        {
            if (all != null)
                all.Clear();
            //Fk(1, 0x01, 8, 8);
            //Fk(2, 0x01, 0, 16);
            //Fk(3, 0x01, 4, 4);
            //Fk(4, 0x01, 0, 8);
            //Fk(5, 0x01, 8, 8);
            //Fk(6, 0x58, 1, 1);
            //Fk(7, 0x08, 1, 1);
            //Fk(8, 0x70, 1, 1);
            //Fk(9, 0x02, 8, 8);
            //Fk(10, 0x02, 8, 8);
            //Fk(11, 0x02, 8, 8); 

            await Task.Run(() =>
            {
                while (!Client.Instance.Initialized)
                    Thread.Sleep(100);
                Client.CurrentServer.Send(Packet.AutodetectRequest);
                // Client.Instance.SendAndProcessResponses(Client.CurrentServer, Packet.AutodetectRequest, 20000, "autodetect", ProcessAutodetectPacket);
            });

            //await Task.Run(() =>
            //{
            //    Fk(1, 0x01, 8, 8);
            //    Thread.Sleep(500);
            //    Fk(2, 0x01, 0, 16);
            //    Thread.Sleep(500);
            //    Fk(3, 0x01, 4, 4);
            //    Thread.Sleep(500);
            //    Fk(4, 0x01, 0, 8);
            //    Thread.Sleep(500);
            //    Fk(5, 0x01, 8, 8);
            //    Thread.Sleep(500);
            //    Fk(6, 0x58, 1, 1);
            //    Thread.Sleep(500);
            //    Fk(7, 0x08, 1, 1);
            //    Thread.Sleep(500);
            //    Fk(8, 0x70, 1, 1);
            //    Thread.Sleep(500);
            //    Fk(9, 0x02, 8, 8);
            //    Thread.Sleep(500);
            //    Fk(10, 0x02, 8, 8);
            //    Thread.Sleep(500);
            //    Fk(11, 0x02, 8, 8);
            //});
        }

        /* public static Device GetDevice(UID id)
        {
            if (Devices.ContainsKey(id))
                return Devices[id];
            else
                return null;
        } */

        public static Server CAN = null;
        public static int ROW_HEIGHT = 32;

        public override string Icon { get => "pdevice_device.png"; }
        public virtual string TypeName { get => "Устройство"; }

        public string StringID { get => ID.ToString(); }

        public List<InputPort> Inputs { get; set; }
        public List<OutputPort> Outputs { get; set; }
        public byte ScenesCount { get; set; }

        private bool fold = true;
        public bool Fold
        {
            get => fold;
            set
            {
                fold = value;
                OnPropertyChanged("Fold");
                OnPropertyChanged("Height");
                OnPropertyChanged("Unfold");
            }
        }

        public bool Unfold
        {
            get => !fold;
        }

        public int Height { get => ((Fold ? (Inputs.Count + Outputs.Count) : 0) + 1) * ROW_HEIGHT; }

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
                Inputs.Add(new InputPort() { ID = i, Value = v, Name = String.Format("Вход {0}", i), Parent = this });
            }
            for (int i = 0; i < outputsCount; i++)
            {
                v = randomValues ? rnd.NextDouble() : 0;
                Outputs.Add(new OutputPort() { ID = i, Value = v, Name = String.Format("Выход {0}", i), Parent = this });
            }
        }

        private List<T> CloneList<T>(List<T> source) where T : Port
        {
            List<T> rl = new List<T>();
            foreach (T i in source)
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
