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

        //сделать пакеты для записи сцен в диммеры и реле
        //    почему под ID сцены отведен только 1 байт?

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
            // 12,     // 5 data size
            6,     // 5 data size
            0x05,   // 6 config byte
            0,      // 7 UID3
            0,      // 8 UID2
            0,      // 9 UID1
            0x5A,   // 10 command 
            01
        };

        public static byte[] CaptureDeviceModeRequest = new byte[]
        {
            36,     // 0
            72,     // 1
            76,     // 2
            0,      // 3
            0x30,   // 4
            6,     // 5 data size
            0x05,   // 6 config byte
            0,      // 7 UID3
            0,      // 8 UID2
            0,      // 9 UID1
            0xAA,   // 10 command 
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

        /// <summary>
        /// Creates set output value request
        /// </summary>
        /// <param name="uid">Device id</param>
        /// <param name="portNumber">Port number</param>
        /// <param name="value">Port value</param>
        /// <param name="delay">Delay before setting value</param>
        /// <param name="fade">Smooth fade time</param>
        /// <param name="groupNumber">Group number</param>
        /// <param name="buttonNumber">Button number</param>
        /// <returns></returns>
        public static byte[] CreateSetOutputValueRequest(UID uid, byte portNumber, byte value, byte delay, byte fade, byte groupNumber, byte buttonNumber)
        {
            var p = SetOutputValueRequest.Clone() as byte[];
            p[7] = uid.B2;
            p[8] = uid.B1;
            p[9] = uid.B0;
            p[11] = portNumber;
            p[12] = value;
            p[13] = delay;
            p[14] = fade;
            p[15] = groupNumber;
            p[16] = buttonNumber;
            return p;
        }

        public static byte GetControllerCommand(byte[] data)
        {
            // return data[10];
            return data[4];
        }

        public static byte GetCANCommand(byte[] data)
        {
            return data[10];
        }

        public static byte[] DeviceFlashRequest = new byte[]
        {
            36,     // 0
            72,     // 1
            76,     // 2
            0,      // 3
            0x30,   // 4 command (30 - send command to CAN)
            8,      // 5 data size
            0x04,   // 6 config byte
            0,      // 7 UID3
            0,      // 8 UID2
            0,      // 9 UID1
            0x6d,   // 10 command 
            0,      // 11 0 - write scenes to device
            0,      // 12 0/1 - start/stop procedure
            0,      // 13 0/1 - disable/eable indication
        };

        /// <summary>
        /// Prepares device for flashing
        /// </summary>
        /// <param name="uid">Device to flash</param>
        /// <param name="operationType">0 - write scenes to device</param>
        /// <param name="startstopFlag">0/1 - start/stop procedure</param>
        /// <param name="indicationFlag">0/1 - disable/eable indication</param>
        /// <returns></returns>
        public static byte[] CreateDeviceFlashRequest(UID uid, byte operationType, byte startstopFlag, byte indicationFlag)
        {
            var p = DeviceFlashRequest.Clone() as byte[];
            p[7] = uid.B2;
            p[8] = uid.B1;
            p[9] = uid.B0;
            p[11] = operationType;
            p[12] = startstopFlag;
            p[13] = indicationFlag;
            return p;
        }

        public static byte[] SceneWriteRequest = new byte[]
        {
            36,     // 0
            72,     // 1
            76,     // 2
            0,      // 3
            0x30,   // 4 command (30 - send command to CAN)
            8,      // 5 data size
            0x04,   // 6 config byte
            0,      // 7 UID3
            0,      // 8 UID2
            0,      // 9 UID1
            0x6c,   // 10 command 
            0,      // 11 scene number in device
            0,      // 12 0/1 - start/stop write scene
            0      // 13 если 1 - стереть остальные сцены в устройстве
        };

        /// <summary>
        /// Start scene writing to device procedure (once per scene)
        /// </summary>
        /// <param name="uid">Device to flash</param>
        /// <param name="sceneNumber">Scene number in device</param>
        /// <param name="startstopFlag">Start/stop flag</param>
        /// <param name="eraseFlag">если 1 - стереть остальные сцены в устройстве</param>
        /// <returns></returns>
        public static byte[] CreateSceneWriteRequest(UID uid, byte sceneNumber, byte startstopFlag, byte eraseFlag)
        {
            var p = SceneWriteRequest.Clone() as byte[];
            p[7] = uid.B2;
            p[8] = uid.B1;
            p[9] = uid.B0;
            p[11] = sceneNumber;
            p[12] = startstopFlag;
            p[13] = eraseFlag;
            return p;
        }

        public static byte[] SceneSettingsWriteRequest = new byte[]
        {
            36,     // 0
            72,     // 1
            76,     // 2
            0,      // 3
            0x30,   // 4 command (30 - send command to CAN)
            11,      // 5 data size
            0x04,   // 6 config byte
            0,      // 7 UID3
            0,      // 8 UID2
            0,      // 9 UID1
            0x62,   // 10 command 
            0,      // 11 scene number in device
            0,      // 12 UID3
            0,      // 13 UID2
            0,      // 14 UID1
            0,      // 15 номер входа
            0       // 16 параметры сцены
        };

        /// <summary>
        /// Create scene settings write request (once per scene)
        /// </summary>
        /// <param name="uid">Device to flash</param>
        /// <param name="sceneNumber">Scene number in device</param>
        /// <param name="sourceUID">Event source ID</param>
        /// <param name="inputNumber">Event source input number</param>
        /// <param name="sceneParams">Scene params</param>
        /// <returns></returns>
        public static byte[] CreateSceneSettingsWriteRequest(UID uid, byte sceneNumber, UID sourceUID, byte inputNumber, byte sceneParams)
        {
            var p = SceneSettingsWriteRequest.Clone() as byte[];
            p[7] = uid.B2;
            p[8] = uid.B1;
            p[9] = uid.B0;
            p[11] = sceneNumber;
            p[12] = sourceUID.B2;
            p[13] = sourceUID.B1;
            p[14] = sourceUID.B0;
            p[15] = inputNumber;
            p[16] = sceneParams;
            return p;
        }

        public static byte[] DimmerSceneIntensityWriteRequest = new byte[]
        {
            36,     // 0
            72,     // 1
            76,     // 2
            0,      // 3
            0x30,   // 4 command (30 - send command to CAN)
            8,      // 5 data size
            0x04,   // 6 config byte
            0,      // 7 UID3
            0,      // 8 UID2
            0,      // 9 UID1
            0x6c,   // 10 command 
            0,      // 11 scene number in device
            0,      // 12 номер четверки 0..3
            0,      // 13 яркость 0 (0..100%)
            0,      // 14 яркость 1
            0,      // 15 яркость 2
            0       // 16 яркость 3
        };

        /// <summary>
        /// Create Dimmer Scene Intensity Write Request
        /// </summary>
        /// <param name="uid">Device to flash</param>
        /// <param name="sceneNumber">Scene number in device</param>
        /// <param name="quadNum"> номер четверки 0..3</param>
        /// <param name="intensity0">яркость 0 (0..100%)</param>
        /// <param name="intensity1">яркость 1 (0..100%)</param>
        /// <param name="intensity2">яркость 2 (0..100%)</param>
        /// <param name="intensity3">яркость 3 (0..100%)</param>
        /// <returns></returns>
        public static byte[] CreateDimmerSceneIntensityWriteRequest(UID uid, byte sceneNumber, byte quadNum, byte intensity0, byte intensity1, byte intensity2, byte intensity3)
        {
            var p = DimmerSceneIntensityWriteRequest.Clone() as byte[];
            p[7] = uid.B2;
            p[8] = uid.B1;
            p[9] = uid.B0;
            p[11] = sceneNumber;
            p[12] = quadNum;
            p[13] = intensity0;
            p[14] = intensity1;
            p[15] = intensity2;
            p[16] = intensity3;
            return p;
        }

        public static byte[] RelaySceneIntensityWriteRequest = new byte[]
        {
            36,     // 0
            72,     // 1
            76,     // 2
            0,      // 3
            0x30,   // 4 command (30 - send command to CAN)
            8,      // 5 data size
            0x04,   // 6 config byte
            0,      // 7 UID3
            0,      // 8 UID2
            0,      // 9 UID1
            0x6c,   // 10 command 
            0,      // 11 scene number in device
            0,      // 12 номер четверки 0..3
            0,      // 13 яркость 0 (0..100%)
            0,      // 14 яркость 1
            0,      // 15 яркость 2
            0       // 16 яркость 3
        };

        /// <summary>
        /// Create Relay Scene Intensity Write Request
        /// </summary>
        /// <param name="uid">Device to flash</param>
        /// <param name="sceneNumber">Scene number in device</param>
        /// <param name="quadNum"> номер четверки 0..3</param>
        /// <param name="intensity0">яркость 0 (0..100%)</param>
        /// <param name="intensity1">яркость 1 (0..100%)</param>
        /// <param name="intensity2">яркость 2 (0..100%)</param>
        /// <param name="intensity3">яркость 3 (0..100%)</param>
        /// <returns></returns>
        public static byte[] CreateRelaySceneIntensityWriteRequest(UID uid, byte sceneNumber, byte quadNum, byte intensity0, byte intensity1, byte intensity2, byte intensity3)
        {
            var p = RelaySceneIntensityWriteRequest.Clone() as byte[];
            p[7] = uid.B2;
            p[8] = uid.B1;
            p[9] = uid.B0;
            p[11] = sceneNumber;
            p[12] = quadNum;
            p[13] = intensity0;
            p[14] = intensity1;
            p[15] = intensity2;
            p[16] = intensity3;
            return p;
        }

        public static byte[] ActivateSceneRequest = new byte[] {
            36,     // 0
            72,     // 1
            76,     // 2
            0,      // 3
            0x30,   // 4 command (30 - send command to CAN)

            //  убрал поле предыдущего активированного входа 10,     // 5 data size
            9,     // 5 data size
            0xd,    // 6 config byte
            0,      // 7 UID3
            0,      // 8 UID2
            0,      // 9 UID1
            0x50,   // 10 command 
            0,      // 11 input number
            0,      // 12 event type
            0,      // 13 arg HI
            0,      // 14 arg LO
            0       // 15 previous input number 
        };

        /// <summary>
        /// Create activate Scene Request
        /// </summary>
        /// <param name="uid">Event source</param>
        /// <param name="sceneNumber">Scene number in device</param>
        public static byte[] CreateActivateSceneRequest(UID uid, byte inputID, byte eventTypeID, short arg, byte prevInputID)
        {
            var p = ActivateSceneRequest.Clone() as byte[];
            p[7] = uid.B2;
            p[8] = uid.B1;
            p[9] = uid.B0;
            p[11] = inputID;
            p[12] = eventTypeID;
            p[13] = (byte) (arg >> 8);
            p[14] = (byte) (arg & 0xFF);
            p[15] = prevInputID;
            return p;
        }



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
