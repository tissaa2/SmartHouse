using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace SmartHouse.Models
{
    public struct UID
    {
        public byte B0 { get { return (byte)(Hash & 0xFF); } }
        public byte B1 { get { return (byte)((Hash >> 8) & 0xFF); } }
        public byte B2 { get { return (byte)((Hash >> 8) & 0xFF); } }
        // public byte B3 { get; private set; }

        public int Hash { get; private set; }

        public string Text
        {
            get { return ToString(); }
        }

        public override bool Equals(object obj)
        {
            if (!(obj is UID))
                return false;
            UID u = (UID)obj;
            return Hash == u.Hash;
        }

        public override int GetHashCode()
        {
            return Hash;
        }

        public override string ToString()
        {
            // return string.Format("{0:x} {0:x} {0:x} {0:x}", B0, B1, B2, B3);
            return string.Format("{0:X2}{1:X2}{2:X2}", B0, B1, B2);
        }


        // public UID(byte b0, byte b1, byte b2, byte b3)
        public UID(byte b0, byte b1, byte b2)
        {
            Hash = b0 | (b1 << 8) | (b2 << 16);
        }

        public UID(string txt)
        {
            Hash = 0;
            Assign(txt);
        }

        public UID(int v)
        {
            Hash = v;
        }

        public bool Assign(string txt)
        {
            int v;
            if (int.TryParse(txt, NumberStyles.HexNumber, null, out v))
            {
                Hash = v;                
                return true;
            }
            return false;
        }

        public static bool TryParse(string txt, out UID value)
        {
            value = new UID();
            return value.Assign(txt);
        }

        public static implicit operator UID(int v)
        {
            return new UID(v);
        }

        public static explicit operator int(UID v)
        {
            return v.Hash;
        }

        public static explicit operator string(UID v)
        {
            return v.ToString();
        }

        /*  public static explicit operator string(UID v)
        {
            return v.ToString();
        } */

        public static bool operator ==(UID a, UID b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(UID a, UID b)
        {
            return !a.Equals(b);
        }

    }
}
