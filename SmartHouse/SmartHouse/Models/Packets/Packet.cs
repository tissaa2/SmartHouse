using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using SmartHouse.Services;

namespace SmartHouse.Models.Packets
{
    public class Packet
    {
        protected static Dictionary<int, Type> packetTypes = null;

        public static byte[] DiscoverRequest = new byte[]
        {
            36,
            72,
            76,
            0,
            0,
            0
        };

        public static byte[] AutodetectRequest = new byte[]
        {
            36,
            72,
            76,
            0x30,
            6,
            0x05,
            0,
            0,
            0,
            0x04,
            01
        };

        public static byte[] PortSelectRequest = new byte[]
        {
            36,
            72,
            76,
            0,
            16,
            1,
            0
        };

        public static byte[] PortCloseRequest = new byte[]
        {
            36,
            72,
            76,
            0,
            17,
            0
        };

        public static byte[] CheckConnectionRequest = new byte[] {36, 72, 76, 0, 18, 0};

        public static byte[] InitCANTranslationRequest = new byte[]
        {
            36,
            72,
            76,
            0,
            32,
            0
        };

        // public static byte[] ActivateSceneCANRequest = new byte[] { 36, 72, 76, 0, 48, 10, 13, 1, 0, 0, 80, 0, 5, 0, 4, 0 };
        public static byte[] ActivateSceneCANRequest = new byte[] { 0x24, 0x48, 0x4c, 0, 0x30, 0xa, 0xd, 0, 0x3, 0x84, 0x50, 0, 5, 0, 4, 0 };

        public static byte START_SEQUENCE_SIZE = 3;

        public byte[] StartSequence = new byte[(int)Packet.START_SEQUENCE_SIZE];

        public short Command;

        public byte DataSize;

        public PacketData Data;

        protected int headerSize = 6;

        private int sizeOf = -1;
        // protected int SizeOf
        // {
        //    get
        //    {
        //        bool flag = this.sizeOf < 0;
        //        if (flag)
        //        {
        //            this.sizeOf = this.CalculateSize();
        //        }
        //        return this.sizeOf;
        //    }
        // }

        protected static List<Type> FindAllDerivedTypes<T>()
        {
            return Packet.FindAllDerivedTypes<T>(Assembly.GetAssembly(typeof(T)));
        }

        protected static List<Type> FindAllDerivedTypes<T>(Assembly assembly)
        {
            Type derivedType = typeof(T);
            return Enumerable.ToList<Type>(Enumerable.Where<Type>(assembly.GetTypes(), (Type t) => t != derivedType && derivedType.IsAssignableFrom(t)));
        }

        public static Packet CreatePacketOfType(int packetType)
        {
            if (Packet.packetTypes == null)
            {
                Packet.packetTypes = new Dictionary<int, Type>();
                List<Type> list = Packet.FindAllDerivedTypes<Packet>();

                foreach(Type t in list)
                    {
                        PacketTypeAttribute packetTypeAttribute = Enumerable.FirstOrDefault<object>(t.GetCustomAttributes(typeof(PacketTypeAttribute), true)) as PacketTypeAttribute;
                        if (packetTypeAttribute != null)
                        {
                            if (!Packet.packetTypes.ContainsKey(packetTypeAttribute.Type))
                            {
                                Packet.packetTypes.Add(packetTypeAttribute.Type, t);
                            }
                            else
                            {
                                Log.Write("Error adding type {0} to packetTypes: type key already exists. Check PaketTypeAttribte value");
                            }
                        }
                    }
                }
            Packet result;
            if (Packet.packetTypes.ContainsKey(packetType))
            {
                result = (Activator.CreateInstance(Packet.packetTypes[packetType])) as Packet;
            }
            else
            {
                result = null;
            }
            return result;
        }

        /* public static Packet DeriveAndLoadData(Packet p, DuplexStream stream)
        {
            Packet packet = Packet.CreatePacketOfType((int)p.Command);
            packet.Assign(p);
            packet.ReadData(stream);
            return packet;
        } */

        protected virtual int CalculateSize()
        {
            Type type = base.GetType();
            int num = 0;
            FieldInfo[] fields = type.GetFields();
            FieldInfo[] array = fields;
            for (int i = 0; i < array.Length; i++)
            {
                FieldInfo fieldInfo = array[i];
                object value = fieldInfo.GetValue(this);
                if (value is byte)
                {
                    num++;
                }
                else
                {
                    if (value is short)
                    {
                        num += 2;
                    }
                    else
                    {
                        if (value is int)
                        {
                            num += 4;
                        }
                        else
                        {
                            if (value is long)
                            {
                                num += 8;
                            }
                            else
                            {
                                if (value is byte[])
                                {
                                    num += (value as byte[]).Length;
                                }
                            }
                        }
                    }
                }
            }
            return num;
        }

        public virtual int ReadHeader(DuplexStream stream)
        {
            int result;
            try
            {
                this.StartSequence = stream.ReadBytes((int)Packet.START_SEQUENCE_SIZE);
                this.Command = stream.ReadInt16();
                this.DataSize = stream.ReadByte();
            }
            catch (Exception ex)
            {
                Log.Write(ex);
                result = -1;
                return result;
            }
            result = 0;
            return result;
        }

        public virtual int ReadData(DuplexStream stream)
        {
            stream.WaitForAvailable(this.DataSize, -1);
            return 0;
        }

        public virtual int ReadFrom(DuplexStream stream)
        {
            int num = this.ReadHeader(stream);
            bool flag = num < 0;
            int result;
            if (flag)
            {
                result = -1;
            }
            else
            {
                num = this.ReadData(stream);
                bool flag2 = num < 0;
                if (flag2)
                {
                    result = -2;
                }
                else
                {
                    result = 0;
                }
            }
            return result;
        }

        public static Packet Read(DuplexStream stream)
        {
            Packet p = null;
            try
            {
                p = new Packet();
                p.ReadFrom(stream);
            }
            catch (Exception ex)
            {
                Log.Write(ex);
                p = null;
            }
            return p;
        }

        public virtual void Assign(Packet p)
        {
            this.StartSequence = (p.StartSequence.Clone() as byte[]);
            this.Command = p.Command;
            this.DataSize = p.DataSize;
        }

        public virtual int Process()
        {
            return 0;
        }

        public override string ToString()
        {
            return string.Format("{0}: Header='{1}', Type={2} DataSize={3}", new object[]
            {
                base.GetType(),
                (this.StartSequence == null) ? " " : Encoding.UTF8.GetString(this.StartSequence),
                this.Command,
                this.DataSize
            });
        }
    }
}
