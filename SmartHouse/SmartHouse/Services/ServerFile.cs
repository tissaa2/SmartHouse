using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHouse.Services
{
    public delegate void FileOperationDelegate(ServerFile sender);
    public class ServerFile
    {
        public bool Complete { get; set; } = false;
        public string FileName { get; set; } = null;
        public byte[] Data { get; set; }
        public event FileOperationDelegate OnComplete;

        public void Write(int offset, byte[] chunk)
        {
            // Data.AddRange(chunk);
            Array.Copy(chunk, 0, Data, offset, chunk.Length);
        }

        public ServerFile(int size)
        {
            Data = new byte[size];
        }
    }
}
