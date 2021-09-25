using System;
using System.Collections.Generic;
using SmartHouse.Services;
using SmartHouse.Models.Storage;
using System.Collections.ObjectModel;
using SmartHouse.Models.Packets;
using System.Threading.Tasks;
using System.Threading;
using System.Reflection;
using SmartHouse.Helpers;
using Newtonsoft.Json;
using SmartHouse.Models;
using System.Linq;
using SmartHouse.Models.Packets.Processors.CAN;

namespace SmartHouse.ViewModels.Devices.Physic
{
    public class PhysicDeviceModel : IconNamedModel
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
            Type p = typeof(PhysicDeviceModel);
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


        private static ObservableCollection<PhysicDeviceModel> all = null;
        public static ObservableCollection<PhysicDeviceModel> All
        {
            get
            {
                if (all == null)
                {
                    all = new ObservableCollection<PhysicDeviceModel>();
                    LoadAll();
                }
                return all;
            }
        }

        private static ObservableCollection<OutputPortModel> allOutputs = new ObservableCollection<OutputPortModel>();
        public static ObservableCollection<OutputPortModel> AllOutputs
        {
            get
            {
                if (allOutputs == null)
                {
                    allOutputs = new ObservableCollection<OutputPortModel>();
                    LoadAll();
                }
                return allOutputs;
            }
        }

        private static ObservableCollection<InputPortModel> allInputs = new ObservableCollection<InputPortModel>();
        public static ObservableCollection<InputPortModel> AllInputs
        {
            get
            {
                if (allInputs == null)
                {
                    allInputs = new ObservableCollection<InputPortModel>();
                    LoadAll();
                }
                return allInputs;
            }
        }

        public static PhysicDeviceModel Get(UID id)
        {
            return PhysicDeviceModel.All.FirstOrDefault(e => e.ID == id);
        }

        public static void Add(PhysicDeviceModel device)
        {
            if (all != null)
            {
                all.Add(device);
                /// Временная заплатка, ибо поиск устройств отрубает реле, на котором висит роутер
                if (!(device is RelayModel))
                    if (allOutputs != null)
                        foreach (var i in device.Outputs)
                            allOutputs.Add(i);
                if (allInputs != null)
                    foreach (var i in device.Inputs)
                        allInputs.Add(i);
            }
        }

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


        public static void LoadAll()
        {
            if (all != null)
                all.Clear();
            if (Utils.EmulateCAN)
            {
                Fk(1, 0x01, 8, 8);
                Thread.Sleep(100);
                Fk(2, 0x01, 0, 16);
                Thread.Sleep(100);
                Fk(3, 0x01, 4, 4);
                Thread.Sleep(100);
                Fk(4, 0x01, 0, 8);
                Thread.Sleep(100);
                Fk(5, 0x01, 8, 8);
                Thread.Sleep(100);
                Fk(6, 0x58, 1, 1);
                Thread.Sleep(100);
                Fk(7, 0x08, 1, 1);
                Thread.Sleep(100);
                Fk(8, 0x70, 1, 1);
                Thread.Sleep(100);
                Fk(9, 0x02, 8, 8);
                Thread.Sleep(100);
                Fk(10, 0x02, 8, 8);
                Thread.Sleep(100);
                Fk(11, 0x02, 8, 8);
            }
            else
            {
                while (!Client.Instance.Initialized)
                    Thread.Sleep(100);
                Client.CurrentServer.Send(Packet.AutodetectRequest);
                Thread.Sleep(2000);
                Client.CurrentServer.Send(Packet.AutodetectRequest);
                Thread.Sleep(2000);
                Client.CurrentServer.Send(Packet.AutodetectRequest);
                Thread.Sleep(2000);
            }

        }
        public static Server CAN = null;
        public static int ROW_HEIGHT = 32;

        public override string Icon { get => "pdevice_device.png"; }
        public virtual string TypeName { get => "Устройство"; }

        [JsonProperty(PropertyName = "UID")]
        // public virtual IDType ID
        public virtual UID UID { get; set; }

        public List<InputPortModel> Inputs { get; set; }
        public List<OutputPortModel> Outputs { get; set; }
        public byte ScenesCount { get; set; }

        private bool fold = true;
        public bool Fold
        {
            get => fold;
            set
            {
                fold = value;
                //OnPropertyChanged("Fold");
                //OnPropertyChanged("Height");
                //OnPropertyChanged("Unfold");
            }
        }

        public bool Unfold
        {
            get => !fold;
        }

        public int Height { get => ((Fold ? (Inputs.Count + Outputs.Count) : 0) + 1) * ROW_HEIGHT; }

        // public virtual void Init(UID id, int inputsCount, int outputsCount, int scenesCount, bool randomValues = false)
        public virtual void Init(int id, UID uid, int inputsCount, int outputsCount, int scenesCount, bool randomValues = false)
        {
            double v;
            ID = id;
            UID = uid;
            Random rnd = new Random();
            Inputs = new List<InputPortModel>();
            Outputs = new List<OutputPortModel>();
            for (int i = 0; i < inputsCount; i++)
            {
                v = randomValues ? rnd.NextDouble() : 0;
                Inputs.Add(new InputPortModel(i){ Value = v, Parent = this });
            }
            for (int i = 0; i < outputsCount; i++)
            {
                v = randomValues ? rnd.NextDouble() : 0;
                Outputs.Add(new OutputPortModel(i) {Value = v, Parent = this });
            }
        }

        private List<T> CloneList<T>(List<T> source) where T : PortModel
        {
            List<T> rl = new List<T>();
            foreach (T i in source)
            {
                rl.Add(i.Clone() as T);
            }
            return rl;
        }

        public override void Assign(ViewModel source)
        {
            if (source is PhysicDeviceModel)
            {
                var s = source as PhysicDeviceModel;
                Inputs = CloneList<InputPortModel>(s.Inputs);
                Outputs = CloneList<OutputPortModel>(s.Outputs);
                ID = source.ID;
            }
        }

        //public virtual byte[] CreateWriteScenePacket(UID uid, byte sceneNumber, byte quadNum, byte intensity0, byte intensity1, byte intensity2, byte intensity3)
        //{
        //    return null;
        //}

        public virtual byte[] CreateWriteScenePacket(UID uid, byte sceneNumber, byte offset, bool isNight, byte[] intensity)
        {
            return null;
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
