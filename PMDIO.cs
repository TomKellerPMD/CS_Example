using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PMDLibrary;

class IO
{
        
        uint PMDMachineIO_DI        = 0x200;
        uint PMDMachineIO_DOMask    = 0x210;
        uint PMDMachineIO_DO        = 0x212;
        uint PMDMachineIO_DORead    = 0x214;
        uint PMDMachineIO_DODirMask = 0x220;
        uint PMDMachineIO_DODir     = 0x222;
        uint PMDMachineIO_DODirRead = 0x224;
        uint PMDMachineIO_AmpEnaMask= 0x230;
        uint PMDMachineIO_AmpEna    = 0x232;
        uint PMDMachineIO_AmpEnaRead= 0x234;
        uint PMDMachineIO_IntPending= 0x240;
        uint PMDMachineIO_IntMask   = 0x250;
        uint PMDMachineIO_IntPosEdge= 0x260;
        uint PMDMachineIO_IntNegEdge= 0x270;
        uint PMDMachineIO_AOCh1     = 0x300;
        uint PMDMachineIO_AOCh2     = 0x302;
        uint PMDMachineIO_AOCh3     = 0x304;
        uint PMDMachineIO_AOCh4     = 0x306;
        uint PMDMachineIO_AOCh5     = 0x308;
        uint PMDMachineIO_AOCh6     = 0x30A;
        uint PMDMachineIO_AOCh7     = 0x30C;
        uint PMDMachineIO_AOCh8     = 0x30E;
        uint PMDMachineIO_AOCh1Ena  = 0x310;
        uint PMDMachineIO_AOCh2Ena  = 0x312;
        uint PMDMachineIO_AOCh3Ena  = 0x314;
        uint PMDMachineIO_AOCh4Ena  = 0x316;
        uint PMDMachineIO_AOCh5Ena  = 0x318;
        uint PMDMachineIO_AOCh6Ena  = 0x31A;
        uint PMDMachineIO_AOCh7Ena  = 0x31C;
        uint PMDMachineIO_AOCh8Ena  = 0x31E;
        uint PMDMachineIO_AOEna     = 0x320;
        uint PMDMachineIO_AICh1     = 0x340;
        uint PMDMachineIO_AICh2     = 0x342;
        uint PMDMachineIO_AICh3     = 0x344;
        uint PMDMachineIO_AICh4     = 0x346;
        uint PMDMachineIO_AICh5     = 0x348;
        uint PMDMachineIO_AICh6     = 0x34A;
        uint PMDMachineIO_AICh7     = 0x34C;
        uint PMDMachineIO_AICh8     = 0x34E;

        public void Close()
        {
            return;
          
        }


        public double ReadAnalagInputs(PMD.PMDDevice dev)
        {
            UInt16 raw_adcvalue=0;
            double adc_float = 0;
            PMD.PMDPeripheral perIO = new PMD.PMDPeripheralPIO(dev, 0, 0, PMD.PMDDataSize.Size16Bit);
            raw_adcvalue=perIO.Read(PMDMachineIO_AICh1);
            perIO.Close();
            
            // Machine controller scaling is +/-10V
            adc_float = Convert.ToSingle((Int16)raw_adcvalue)*10.0/32767.0;
            return adc_float;
        }


        public UInt16 ReadDigitalInputs(PMD.PMDDevice dev)
        {
            UInt16 value;
            PMD.PMDPeripheral perIO = new PMD.PMDPeripheralPIO(dev, 0, 0, PMD.PMDDataSize.Size16Bit);
            
            // Bits for PMDMachineIO_DI
            //    DI1   Bit 0
            //    DI2   Bit 1
            //    DI3   Bit 2
            //    DI4   Bit 3
            //    DIO1  Bit 8
            //    DIO2  Bit 9
            //    DIO3  Bit 10
            //    DIO4  Bit 11
            //    DIO5  Bit 12
            //    DIO6  Bit 13
            //    DIO7  Bit 14
            //    DIO8  Bit 15
            value = perIO.Read(PMDMachineIO_DI);
            perIO.Close();
            return value;
        }

        public UInt16 ReadDigitalIOdirection(PMD.PMDDevice dev)
        {
            UInt16 value;
            PMD.PMDPeripheral perIO = new PMD.PMDPeripheralPIO(dev, 0, 0, PMD.PMDDataSize.Size16Bit);
            value = perIO.Read(PMDMachineIO_DODirRead);
            perIO.Close();
            return value;
        }


        public void WriteDigitalOut(PMD.PMDDevice dev, UInt16 data)
        {

            PMD.PMDPeripheral perIO = new PMD.PMDPeripheralPIO(dev, 0, 0, PMD.PMDDataSize.Size16Bit);

            // first make all DIO outputs.            
            perIO.Write(0x0F00, PMDMachineIO_DODir);

            
            // Bits for PMDMachineIO_DO
            //    DO1   Bit 0
            //    DO2   Bit 1
            //    DO3   Bit 2
            //    DO4   Bit 3
            //    DIO1  Bit 8
            //    DIO2  Bit 9
            //    DIO3  Bit 10
            //    DIO4  Bit 11
            //    DIO5  Bit 12
            //    DIO6  Bit 13
            //    DIO7  Bit 14
            //    DIO8  Bit 15

            perIO.Write(data, PMDMachineIO_DO);
            perIO.Close();
        }

           
}

