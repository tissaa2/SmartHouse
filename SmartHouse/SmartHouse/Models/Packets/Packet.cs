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

        public static byte[] DiscoverRequest = new byte[]
        {
            36,
            72,
            76,
            0,
            0,
            0
        };

        public static byte READ_ALIASFILE_COMMAND = 0x51;

        public static byte[] ReadAliasFileRequest = new byte[]
        {
            36,
            72,
            76,
            0,
            READ_ALIASFILE_COMMAND,
            0
        };

        public static byte READ_FILECHUNK_COMMAND = 0x58;

        public static byte[] ReadFileChunkRequest = new byte[]
        {
            36,
            72,
            76,
            0,
            READ_FILECHUNK_COMMAND,
            4,
            0,
            0,
            0,
            0
        };

        public static byte FINISH_READ_ALIASFILE_COMMAND = 0x59;

        public static byte[] FinishAliasFileReadRequest = new byte[]
        {
            36,
            72,
            76,
            0,
            FINISH_READ_ALIASFILE_COMMAND,
            0
        };

        public static byte WRITE_ALIASFILE_COMMAND = 0x41;

        public static byte[] WriteAliasFileRequest = new byte[]
        {
            36,
            72,
            76,
            0,
            WRITE_ALIASFILE_COMMAND,
            0
        };

        public static byte WRITE_FILECHUNK_COMMAND = 0x48;

        public static byte[] WriteFileChunkRequest = new byte[]
        {
            36,
            72,
            76,
            0,
            0x48,
            0
        };

        public static byte FINISH_WRITE_ALIASFILE_COMMAND = 0x49;

        public static byte[] WriteFileEndRequest = new byte[]
        {
            36,
            72,
            76,
            0,
            FINISH_WRITE_ALIASFILE_COMMAND,
            0
        };

        public static byte[] AutodetectRequest = new byte[]
        {
            36,
            72,
            76,
            0,
            0x30,
            6,
            0x05,
            0,
            0,
            0,
            0x04,
            01
        };

        public static byte[] GetOutputStatesRequest = new byte[]
        {
            36,     // 0
            72,     // 1
            76,     // 2
            0,      // 3
            0x30,   // 4
            12,     // 5 data size
            0x05,   // 6 config byte
            0,      // 7 UID3
            0,      // 8 UID2
            0,      // 9 UID1
            0x5A,   // 10 command 
            01
        };

        public static byte[] SetOutputValueRequest = new byte[]
        {
            36,     // 0
            72,     // 1
            76,     // 2
            0,      // 3
            0x30,   // 4
            9,      // 5 data size
            0x04,   // 6 config byte
            0,      // 7 UID3
            0,      // 8 UID2
            0,      // 9 UID1
            0x54,   // 10 command 
            0,      // 11 channel
            0,      // 12 level (value)
            0,      // 13 delay (in sec/4)
            0,      // 14 fade time (in sec/4)
            0,      // 15 GN (group number)
            0       // 16 BN (button number)
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

        public PacketDataStream Data = null;

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

        /* public static Packet DeriveAndLoadData(Packet p, DuplexStream stream)
        {
            Packet packet = Packet.CreatePacketOfType((int)p.Command);
            packet.Assign(p);
            packet.ReadData(stream);
            return packet;
        } */

        /* protected virtual int CalculateSize()
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
        } */

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
            Data = new PacketDataStream(stream.ReadBytes(DataSize));
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
