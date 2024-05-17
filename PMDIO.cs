//Limits of Liability - Under no circumstances shall Performance Motion Devices, Inc. or its affiliates, partners, or suppliers be liable for any indirect
// incidental, consequential, special or exemplary damages arising out or in connection with the use this example,
// whether or not the damages were foreseeable and whether or not Performance Motion Devices, Inc. was advised of the possibility of such damages.
// Determining the suitability of this example is the responsibility of the user and subsequent usage is at their sole risk and discretion.
// There are no licensing restrictions associated with this example.
//
// PMDIO.cs
// TLK 5/17/24
// Methods for reading and writing DIO on the N-series and Machine Controller.


/// Notes on N-series DIO
/// DIO 1 to 4 are push/pull type and therefore the direction (input or output) needs to be specified.
/// DIO 5 to 8 are open collector
/// Use Pro-Motion to configure the pin(s) being used as DIO.  Then configure DIO 1 to 4 as intputs or outputs as needed.

/// Bits for N-series are 1 for 1:
//  DIO1 Bit 0
//  DIO2 Bit 1
//  DIO3 Bit 2
//  DIO4 Bit 3
//  DIO5 Bit 4
//  DIO6 Bit 5
//  DIO7 Bit 6
//  DIO8 Bit 7

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


using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using PMDLibrary;


class PMDIO
{

    PMD.PMDDevice dev;
    public PMDIO(PMD.PMDDevice mdev)
    {
        dev = mdev;
    }
    public double ReadAnalagInputs()
    {
        UInt16 raw_adcvalue = 0;
        double adc_float = 0;
        PMD.PMDPeripheral perIO = new PMD.PMDPeripheralPIO(dev, 0, 0, PMD.PMDDataSize.Size16Bit);
        raw_adcvalue = perIO.Read((uint)IO_offset.AI1);
        perIO.Close();
        adc_float = Convert.ToSingle((Int16)raw_adcvalue) * 10.0 / 32767.0;
        return adc_float;
    }

    public UInt16 ReadDigitalInputs()
    {
        UInt16 value = 0;
        PMD.PMDPeripheral perIO = new PMD.PMDPeripheralPIO(dev, 0, 0, PMD.PMDDataSize.Size16Bit);
        value = perIO.Read((uint)IO_offset.DI);
        perIO.Close();
        return value;
    }

    public UInt16 ReadDigitalIOdirection(PMD.PMDDevice dev)
    {
        UInt16 value;
        PMD.PMDPeripheral perIO = new PMD.PMDPeripheralPIO(dev, 0, 0, PMD.PMDDataSize.Size16Bit);
        value = perIO.Read((uint)IO_offset.DODirRead);
        perIO.Close();
        return value;
    }

    public void SetDigitalIOdirection(UInt16 direction)
    {
        PMD.PMDPeripheral perIO = new PMD.PMDPeripheralPIO(dev, 0, 0, PMD.PMDDataSize.Size16Bit);
        perIO.Write(direction,(uint)IO_offset.DODir);
        perIO.Close();
    }

        
    // Make sure DIO has been set as output before invoking this method.
    public void WriteDigitalOut(UInt16 data)
    {

        PMD.PMDPeripheral perIO = new PMD.PMDPeripheralPIO(dev, 0, 0, PMD.PMDDataSize.Size16Bit);
        perIO.Write(data, (uint)IO_offset.DO);
        perIO.Close();
    }

    public void Close()
    {
        return;

    }
    enum IO_offset
    {
        DI = 0x200,
        DOMask = 0x210,
        DO = 0x212,
        DORead = 0x214,
        DODirMask = 0x220,
        DODir = 0x222,
        DODirRead = 0x224,
        AmpEnaMask = 0x230,
        AmpEna = 0x232,
        AmpEnaRead = 0x234,
        AO1 = 0x300,
        AO2 = 0x302,
        AO3 = 0x304,
        AO4 = 0x306,
        AO5 = 0x308,
        AO6 = 0x30A,
        AO7 = 0x30C,
        AO8 = 0x30E,
        AOCh1Ena = 0x310,
        AOCh2Ena = 0x312,
        AOCh3Ena = 0x314,
        AOCh4Ena = 0x316,
        AOCh5Ena = 0x318,
        AOCh6Ena = 0x31A,
        AOCh7Ena = 0x31C,
        AOCh8Ena = 0x31E,
        AOEna = 0x320,
        AI1 = 0x340,
        AI2 = 0x342,
        AI3 = 0x344,
        AI4 = 0x346,
        AI5 = 0x348,
        AI6 = 0x34A,
        AI7 = 0x34C,
        AI8 = 0x34E,
    }
}
    
 



