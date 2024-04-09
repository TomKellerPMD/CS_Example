//Limits of Liability - Under no circumstances shall Performance Motion Devices, Inc. or its affiliates, partners, or suppliers be liable for any indirect
// incidental, consequential, special or exemplary damages arising out or in connection with the use this example,
// whether or not the damages were foreseeable and whether or not Performance Motion Devices, Inc. was advised of the possibility of such damages.
// Determining the suitability of this example is the responsibility of the user and subsequent usage is at their sole risk and discretion.
// There are no licensing restrictions associated with this example.
//
// PMDMemory.cs
// TLK 9/28/21
// Allocates memory buffers for both SPRAM devices and DPRAM devices.  The Device Type (communication protocol) associated with the BUFFER instance is used to select the memory type.
// Motion Processor is Single Port RAM in almost all cases
// Resource Protocol is Dual Port RAM in all case.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PMDLibrary;

class Memory
    {
    public PMD.PMDMemory memptr;
        public void Close()
        {
            memptr.Close();
        }
    
    }

class NVRAM : Memory
    {
        public NVRAM(PMD.PMDDevice dev)
        {
            PMD.PMDDataSize datasize = PMD.PMDDataSize.Size16Bit;
            PMD.PMDMemoryType MemType = PMD.PMDMemoryType.NVRAM;
            memptr = new PMD.PMDMemory(dev, datasize, MemType);
        }
    }

class SPRAM : Memory
{
    public SPRAM(PMD.PMDDevice dev)
    {
        // SPRAM uses an axis handle intead of a memory handle for writes and reads.
        // The SPRAM is creates just for constistacy.
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

class BUFFER
{
    private static uint next_avl_address=0x1000;
    uint start_address = next_avl_address;
   // uint globalwriteindex = 0;
   // uint globalreadindex = 0;
    public short bufferID = 0;
    uint length = 0;
    public DPRAM DPmemory;
    public SPRAM SPmemory;
    readonly PMD.PMDAxis axis;
    public BUFFER(PMD.PMDDevice dev, PMD.PMDAxis m_axis, short m_bufferID, uint m_length)
    {

        if (dev.DeviceType == PMD.PMDDeviceType.ResourceProtocol)
        {
            try { DPmemory = new DPRAM(dev); }
            catch
            {
                if (DPmemory == null) SPmemory = new SPRAM(dev);
            }
        }
        
        else if (dev.DeviceType == PMD.PMDDeviceType.MotionProcessor) SPmemory = new SPRAM(dev);
        
        bufferID = m_bufferID;
        length = m_length;  //dwords
        axis = m_axis;
        next_avl_address += length;
        axis.SetBufferStart(bufferID,(int)start_address);
        axis.SetBufferLength(bufferID, (int)length);
    }

    public void Write(Int32[] data, uint length)
    {
        if (this.DPmemory != null)
        {
            UInt32[] temp = new uint[length];
            ConvertToUInt32(data, temp, length);
            DPmemory.memptr.Write(ref temp, start_address, length);
        }

        if (this.SPmemory != null)
        {
            int i;
            for (i = 0; i < length; i++) axis.WriteBuffer(bufferID, data[i]);
        }
     //   globalwriteindex += length;
    }

    public void Read(ref Int32[] data, uint length)
    {

        if (this.DPmemory != null)
        {
            UInt32[] temp = new uint[length];
            DPmemory.memptr.Read(ref temp, start_address, length);
       //     data = temp as int[];
            //ConvertToUInt32(tempdata, temp, length);
        }

        if (this.SPmemory != null)
        {
            int i;
            for (i = 0; i < length; i++) data[i]=axis.ReadBuffer(bufferID);
        }
   //     globalreadindex += length;
    }

    UInt32[] ConvertToUInt32(Int32[] data, UInt32[]temp, uint length)
    {
        int i;
        for (i = 0; i < length; i++) temp[i] = unchecked((uint)data[i]);
        return temp;
    }

   //other stuff here
}


