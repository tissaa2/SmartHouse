using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHouse.Models
{
    public struct UID
    {
        public byte B0 { get; private set; }
        public byte B1 { get; private set; }
        public byte B2 { get; private set; }
        // public byte B3 { get; private set; }

        private int hash;

        public override bool Equals(object obj)
        {
            if (!(obj is UID))
                return false;
            UID u = (UID)obj;
            // return (B0 == u.B0) && (B1 == u.B1) && (B2 == u.B2) && (B3 == u.B3);
            return (B0 == u.B0) && (B1 == u.B1) && (B2 == u.B2);
        }

        public override int GetHashCode()
        {
            return hash;
        }

        public override string ToString()
        {
            // return string.Format("{0:x} {0:x} {0:x} {0:x}", B0, B1, B2, B3);
            return string.Format("{0:x} {0:x} {0:x}", B0, B1, B2);
        }

        // public UID(byte b0, byte b1, byte b2, byte b3)
        public UID(byte b0, byte b1, byte b2)
        {
            B0 = b0;
            B1 = b1;
            B2 = b2;
            // B3 = b3;
            // hash = B0 | (B1 << 8) | (B2 << 16) | (B3 << 24);
            hash = B0 | (B1 << 8) | (B2 << 16);
        }

        public UID(int v)
            : this((byte)(v & 0xFF), (byte)((v >> 8) & 0xFF), (byte)((v >> 16) & 0xFF))
        {
        }

        public static implicit operator UID(int v)
        {
            return new UID(v);
        }

        public static explicit operator int(UID v)
        {
            return v.hash;
        }

    }
}
