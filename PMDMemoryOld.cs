using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PMDLibrary;

class Memory
    {
        public PMD.PMDMemory memptr;
        public void Write()
        {
            
            UInt32[] data = new UInt32[] { 0x01234567, 0x89ABCDEF, 0x7654321, 0xFEDCBA98 };
            //UInt32[] data = new UInt32[] { 0x0, 0x0, 0x0, 0x0 };
            memptr.Write(ref data, 0, 4);
        }

        public void Read()
        {

            UInt32[] data = new UInt32[8];
            memptr.Read(ref data, 0, 4);
        }

        public void Close()
        {
            memptr.Close();
        }
    
    }

    class NVRAM : Memory
    {
        public NVRAM(PMD.PMDDevice dev)
        {
            PMD.PMDDataSize datasize = PMD.PMDDataSize.Size32Bit;
            PMD.PMDMemoryType MemType = PMD.PMDMemoryType.NVRAM;
            memptr = new PMD.PMDMemory(dev, datasize, MemType);
        }
    }

    class DPRAM : Memory
    {
        public DPRAM(PMD.PMDDevice dev)
        {
            PMD.PMDDataSize datasize = PMD.PMDDataSize.Size32Bit;
            PMD.PMDMemoryType MemType = PMD.PMDMemoryType.DPRAM;
            memptr = new PMD.PMDMemory(dev, datasize, MemType);
        }
    }


