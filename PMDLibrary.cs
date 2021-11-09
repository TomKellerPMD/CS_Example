using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices; 


namespace PMDLibrary
{
  
    
    public class PMD 
    {

        
        public const UInt32 PMD_WAITFOREVER = 0xFFFFFFFF;
        public const UInt32 DEFAULT_ETHERNET_PORT = 40100;

        public enum PMDDeviceType { None = 0, ResourceProtocol = 1, MotionProcessor = 2 };
        public enum PMDInterfaceType { None = 0, Parallel = 1, PCI = 2, ISA = 3, Serial = 4, CAN = 5, TCP = 6, UDP = 7, USB = 8}
        public enum PMDInstructionError{
            NoError = 0,
            ProcessorReset = 1,
            InvalidInstruction = 2,
            InvalidAxis = 3,
            InvalidParameter = 4,
            TraceRunning = 5,
            BlockOutOfBounds = 7,
            TraceBufferZero = 8,
            BadSerialChecksum = 9,
            InvalidNegativeValue = 11,
            InvalidParameterChange = 12,
            InvalidMoveAfterEventTriggeredStop = 13,
            InvalidMoveIntoLimit = 14,
            InvalidOperatingModeRestoreAfterEventTriggeredChange = 16,
            InvalidOperatingModeForCommand = 17,
        }

        public enum PMDDataSize{ Size8Bit = 1, Size16Bit = 2, Size32Bit = 4 }
    
        public enum PMDMemoryType { DPRAM = 0, NVRAM = 1 }
        
        public enum PMDTaskState {NoCode = 1, NotStarted = 2, Running = 3, Aborted = 4 }
    
        public enum PMDDefault {
            CP_Serial = 0x101,
            CP_MotorType = 0x102,
            IPAddress = 0x303,
            NetMask = 0x304,
            Gateway = 0x305,
            IPPort = 0x106,
            MACH = 0x307,
            MACL = 0x308,
            COM1 = 0x10E,
            COM2 = 0x10F,
            RS485Duplex = 0x110,
            CAN = 0x111,
            AutoStartMode = 0x114,
            DebugSource = 0x117,
            ConsoleIntfType = 0x118,
            ConsoleIntfAddr = 0x319,
            ConsoleIntfPort = 0x11A,
            Factory = 0xFFFF
        }

        public enum PMDVoltageLimit {OvervoltageLimit = 0, UndervoltageLimit = 1 }
        
        public enum PMDPositionLoop {
            ProportionalGain = 0,
            IntegratorGain = 1,
            IntegratorLimit = 2,
            DerivativeGain = 3,
            DerivativeTime = 4,
            OutputGain = 5,
            VelocityFeedforwardGain = 6,
            AccelerationFeedforwardGain = 7,
            Biquad1EnableFilter = 8,
            Biquad1B0 = 9,
            Biquad1B1 = 10,
            Biquad1B2 = 11,
            Biquad1A1 = 12,
            Biquad1A2 = 13,
            Biquad1K = 14,
            Biquad2EnableFilter = 15,
            Biquad2B0 = 16,
            Biquad2B1 = 17,
            Biquad2B2 = 18,
            Biquad2A1 = 19,
            Biquad2A2 = 20,
            Biquad2K = 21
        }


        public enum PMDPositionLoopValueNode { IntegratorSum = 0, IntegralContribution = 1, Derivative = 2, Biquad1Input = 3, Biquad2Input = 4 }
    
        public enum PMDUpdateMask { TrajectoryMask = 0x1, PositionLoopMask = 0x2, CurrentLoopMask = 0x8 }
    
        public enum PMDDriveFaultStatusMask {
            ShortCircuitFaultMask = 0x1,
            GroundFaultMask = 0x2,
            ExternalLogicFaultMask = 0x4,
            OpModeMismatchMask = 0x8,
            OvervoltageFaultMask = 0x20,
            UndervoltageFaultMask = 0x40,
            DisabledMask = 0x80,
            FoldbackMask = 0x100,
            OverTemperatureMask = 0x200,
            AtlasDetectedSPIChecksumMask = 0x400,
            WatchdogMask = 0x800,
            MagellanDetectedSPIChecksumMask = 0x4000,
            MotorTypeMismatchMask = 0x8000,
        }

        public enum PMDDriveFaultParameter { OverVoltageLimit = 0, UnderVoltageLimit = 1, RecoveryMode = 2, WatchdogLimit = 3, TemperatureLimit = 4, TemperatureHysteresis = 5}
        
        public enum PMDNVRAM { NVRAMMode = 0, EraseNVRAM = 1, Write = 2, BlockWriteBegin = 3, BlockWriteEnd = 4, Skip = 8 }
        
        public enum PMDDrivePWM { PWMLimit = 0 }
    
        public enum PMDEventAction {
            None =0,
            AbruptStop = 2,
            SmoothStop = 3,
            DisablePositionLoopAndHigherModules = 5,
            DisableCurrentLoopAndHigherModules = 6,
            DisableMotorOutputAndHigherModules = 7,
            AbruptStopWithPositionErrorClear = 8,
        }
           
        public enum PMDEventActionEvent { Immediate = 0, PositiveLimit = 1, NegativeLimit = 2, MotionError = 3, CurrentFoldback = 4}
    
        public enum PMDOperatingModeMask {
            AxisEnabledMask = 0x1,
            MotorOutputEnabledMask = 0x2,
            CurrentControlEnabledMask = 0x4,
            PositionLoopEnabledMask = 0x10,
            TrajectoryEnabledMask = 0x20,
            AllEnabledMask = 0x37
        }

        public enum PMDFoldbackCurrent { ContinuousCurrentLimit = 0 }
    
        public enum PMDHoldingCurrent { MotorLimit = 0, Delay = 1 }
    
        public enum PMDCurrent { HoldingCurrent = 0, Delay = 1, DriveCurrent = 2 }
   
        public enum PMDDriveStatusMask {
            InFoldbackMask = 0x2,
            Overtemperature = 0x4,
            InHolding = 0x10,
            Overvoltage = 0x20,
            Undervoltage = 0x40,
            Disabled = 0x80,
            OutputClipped = 0x1000,
            DriveNotInitialized = 0x8000
        }
    
        public enum PMDFOCValueNode {
            Reference = 0,
            Feedback = 1,
            FOCError = 2,
            IntegratorSum = 3,
            IntegralContribution = 5,
            Output = 6,
            FOCOutput = 7,
            ActualCurrent = 8
        }
   
        public enum PMDCurrentLoopParameter { ProportionalGain = 0, IntegralGain = 1, IntegralSumLimit = 2 }
    
        public enum PMDCurrentLoopNumber { PhaseA = 0, PhaseB = 1, Both = 2 }
    
        public enum PMDCurrentLoopValueNode {
            Reference = 0,
            ActualCurrent = 1,
            CurrentError = 2,
            IntegratorSum = 3,
            IntegralContribution = 5,
            Output = 6
        }
    
        public enum PMDFOCLoopParameter { ProportionalGain = 0, IntegralGain = 1, IntegralSumLimit = 2 }
    
        public enum PMDFOC_LoopNumber { Direct = 0, Quadrature = 1, Both = 2 }
    
        public enum PMDCurrentControlMode { CurrentLoop = 0, FOC = 1 }
    
        public enum PMDMotorTypeVersion { 
            BrushedServo = 1,
            BrushlessServo = 3,
            MicroStepping = 4,
            Stepping = 5,
            AllMotor = 8,
            IONMotor = 9
        }
    
        public enum PMDCANBaud {
            Baud1000000 = 0,
            Baud800000 = 1,
            Baud500000 = 2,
            Baud250000 = 3,
            Baud125000 = 4,
            Baud50000 = 5,
            Baud20000 = 6,
            Baud10000 = 7
        }
   
        public enum PMDSerialParity { None = 0, Odd = 1, Even = 2 }
    
        public enum PMDSerialProtocol { Point2Point = 0, MultiDropUsingIdleLineDetection = 1 }
    
        public enum PMDSerialStopBits { Bits1 = 0, Bits2 = 1 }
    
        public enum PMDSerialBaud {
            Baud1200 = 0,
            Baud2400 = 1,
            Baud9600 = 2,
            Baud19200 = 3,
            Baud57600 = 4,
            Baud115200 = 5,
            Baud230400 = 6,
            Baud460800 = 7
        }
    
        public enum PMDSPIMode {
            RisingEdge = 0,
            RisingEdgeDelay = 1,
            FallingEdge = 2,
            FallingEdgeDelay = 3
        }
    
        public enum PMDSynchronizationMode { Disabled = 0, Master = 1, Slave = 2 }
    
        public enum PMDAxisMode { ModeOff = 0, ModeOn = 1}
   
        public enum PMDAxisOutRegister { None = 0, EventStatus = 1, ActivityStatus = 2, SignalStatus = 3, DriveStatus = 4 }
   
        public enum PMDTraceStatusMask { Mode = 0x1, Activity = 0x2, DataWrap = 0x4 }
    
        public enum PMDTraceTriggerState { Low = 0, High = 1 }
    
        public enum PMDTraceCondition {
            Immediate = 0,
            NextUpdate = 1,
            EventStatus = 2,
            ActivityStatus = 3,
            SignalStatus = 4,
            DriveStatus = 5,
        }

        public enum PMDTraceVariable {
            None = 0,
            PositionError = 1,
            CommandedPosition = 2,
            CommandedVelocity = 3,
            CommandedAcceleration = 4,
            ActualPosition = 5,
            ActualVelocity = 6,
            ActiveMotorCommand = 7,
            MotionProcessorTime = 8,
            CaptureRegister = 9,
            PositionLoopIntegralSum = 10,
            PositionLoopIntegralContribution = 57,
            PositionLoopDerivative = 11,
            PIDOutput = 64,
            Biquad1Output = 65,
            EventStatusRegister = 12,
            ActivityStatusRegister = 13,
            SignalStatusRegister = 14,
            PhaseAngle = 15,
            PhaseOffset = 16,
            PhaseACommand = 17,
            PhaseBCommand = 18,
            PhaseCCommand = 19,
            AnalogInput0 = 20,
            AnalogInput1 = 21,
            AnalogInput2 = 22,
            AnalogInput3 = 23,
            AnalogInput4 = 24,
            AnalogInput5 = 25,
            AnalogInput6 = 26,
            AnalogInput7 = 27,
            PhaseAngleScaled = 29,
            CurrentLoopAReference = 66,
            CurrentLoopAError = 30,
            CurrentLoopActualCurrentA = 31,
            CurrentLoopAIntegratorSum = 32,
            CurrentLoopAIntegralContribution = 33,
            CurrentLoopAOutput = 34,
            CurrentLoopBReference = 67,
            CurrentLoopBError = 35,
            CurrentLoopActualCurrentB = 36,
            CurrentLoopBIntegratorSum = 37,
            CurrentLoopBIntegralContribution = 38,
            CurrentLoopBOutput = 39,
            FOCDReference = 40,
            FOCDError = 41,
            FOCDFeedback = 42,
            FOCDIntegratorSum = 43,
            FOCDIntegralContribution = 44,
            FOCDOutput = 45,
            FOCQReference = 46,
            FOCQError = 47,
            FOCQFeedback = 48,
            FOCQIntegratorSum = 49,
            FOCQIntegralContribution = 50,
            FOCQOutput = 51,
            FOCAOutput = 52,
            FOCBOutput = 53,
            FOCActualCurrentA = 31,
            FOCActualCurrentB = 36,
            BusVoltage = 54,
            Temperature = 55,
            DriveStatus = 56
        }
    

        public enum PMDTraceMode { OneTime = 0, RollingBuffer = 1 }
    
        public enum PMDTraceVariableNumber { Variable1 = 0, Variable2 = 1, Variable3 = 2, Variable4 = 3 }
    
        public enum PMDPhaseCommand { A = 0, B = 1, C = 2 }
        
        public enum PMDPhasePrescaleMode { PrescaleOff = 0, Prescale64 = 1, Prescale128 = 2, Prescale256 = 3 }
    

        public enum PMDPhaseInitializeMode { Algorithmic = 0, HallBased = 1 }
        
        public enum PMDPhaseCorrectionMode { Disable = 0, Enable = 1 }
    
        public enum PMDCommutationMode { Sinusoidal = 0, HallBased = 1 }
    
        public enum PMDMotorMode { ModeOff = 0, ModeOn = 1 }

        public enum PMDMotorOutputMode
        {
            BipolarDAC = 0,
            PWMSignMagnitude = 1,
            PWM5050Magnitude = 2,
            OffsetSPIDAC = 3,
            UnipolarDAC = 4,
            SPIDAC2sComplement = 5,
            Atlas = 6,
            PulseAndDirection = 8
        }

        public enum PMDEncoderSource {
           Incremental = 0,
            Parallel = 1,
            None = 2,
            Loopback = 3,
            Parallel32 = 6,
        }

        public enum PMDCaptureSource { Index = 0, Home = 1, HSI = 2 }
   
        public enum PMDSignalMask {
            EncoderAMask = 0x1,
            EncoderBMask = 0x2,
            EncoderIndexMask = 0x4,
            EncoderHomeMask = 0x8,
            PositiveLimitMask = 0x10,
            NegativeLimitMask = 0x20,
            AxisInMask = 0x40,
            HallAMask = 0x80,
            HallBMask = 0x100,
            HallCMask = 0x200,
            AxisOutMask = 0x400,
            StepOutputInvertMask = 0x800,
            MotorDirectionMask = 0x1000,
            EnableIn = 0x2000,
            FaultOut = 0x4000
        }
    

        public enum PMDEventMask {
            MotionCompleteMask = 0x1,
            WrapAroundMask = 0x2,
            Breakpoint1Mask = 0x4,
            CaptureReceivedMask = 0x8,
            MotionErrorMask = 0x10,
            InPositiveLimitMask = 0x20,
            InNegativeLimitMask = 0x40,
            InstructionErrorMask = 0x80,
            DriveDisabledMask = 0x100,
            OvertemperatureFaultMask = 0x200,
            BusVoltageFaultMask = 0x400,
            CommutationErrorMask = 0x800,
            CurrentFoldbackMask = 0x1000,
        }
            

        public enum PMDActivityMask {
            PhasingInitializedMask = 0x1,
            AtMaximumVelocityMask = 0x2,
            TrackingMask = 0x4,
            ProfileModeMask = 0x38,
            AxisSettledMask = 0x80,
            MotorOnMask = 0x100,
            PositionCaptureMask = 0x200,
            InMotionMask = 0x400,
            InPositiveLimitMask = 0x800,
            InNegativeLimitMask = 0x1000,
            ProfileSegmentMask = 0xE000
         }

        public enum PMDActualPositionUnits { Counts = 0, Steps = 1 }
    
        public enum PMDBreakpointAction {
            NoAction = 0,
            Update = 1,
            AbruptStop = 2,
            SmoothStop = 3,
            MotorOff = 4,
            DisablePositionLoopAndHigherModules = 5,
            DisableCurrentLoopAndHigherModules = 6,
            DisableMotorOutputAndHigherModules = 7,
            AbruptStopWithPositionErrorClear = 8
        }
   
        public enum PMDBreakpointTrigger {
            Disable = 0,
            GreaterOrEqualCommandedPosition = 1,
            LessOrEqualCommandedPosition = 2,
            GreaterOrEqualActualPosition = 3,
            LessOrEqualActualPosition = 4,
            CommandedPositionCrossed = 5,
            ActualPositionCrossed = 6,
            Time = 7,
            EventStatus = 8,
            ActivityStatus = 9,
            SignalStatus = 10,
            DriveStatus = 11,
        }

        public enum PMDBreakpoint { Breakpoint1 = 0, Breakpoint2 = 1 }
   
        public enum PMDMotionCompleteMode { CommandedPosition = 0, ActualPosition = 1 }
    
        public enum PMDStopMode { NoStop = 0, Abrupt = 1, Smooth = 2 }
    
        public enum PMDProfileMode { Trapezoidal = 0, VelocityContouring = 1, SCurve = 2, ElectronicGear = 3 }
    
        public enum PMDMotorType {
            BrushlessDC_3Phase = 0,
            BrushlessDC_2Phase = 1,
            Microstepping_3Phase = 2,
            Microstepping_2Phase = 3,
            Stepper = 4,
            DCBrush = 7
        }
    
        public const Int16 AtlasAxisMask = 0x20;

        public enum PMDAxisNumber {
            Axis1 = 0,
            Axis2 = 1,
            Axis3 = 2,
            Axis4 = 3,
            AtlasAxis1 = Axis1 + AtlasAxisMask,
            AtlasAxis2 = Axis2 + AtlasAxisMask,
            AtlasAxis3 = Axis3 + AtlasAxisMask,
            AtlasAxis4 = Axis4 + AtlasAxisMask
        }

        public enum PMDAxisMask { NoAxis = 0x0, Axis1 = 0x1, Axis2 = 0x2, Axis3 = 0x4, Axis4 = 0x8 }
   

        public enum PMDAuxiliaryEncoderMode { Disable = 0, Enable = 1 }
    

        public enum PMDGearMasterSource { Actual = 0, Commanded = 1 }
    
        public enum PMDFeedbackParameter { EncoderModulus = 0 }
        
        public enum PMDresult{
            NOERROR = 0x0,
            ERR_OK =  0x0,
            ERR_Reset = 0x1,
            ERR_InvalidInstruction = 0x2,
            ERR_InvalidAxis = 0x3,
            ERR_InvalidParameter = 0x4,
            ERR_TraceRunning = 0x5,
            ERR_BlockOutOfBounds = 0x7,
            ERR_TraceBufferZero = 0x8,
            ERR_BadSerialChecksum = 0x9,
            ERR_InvalidNegativeValue = 0xB,
            ERR_InvalidParameterChange = 0xC,
            ERR_LimitEventPending = 0xD,
            ERR_InvalidMoveIntoLimit = 0xE,
            ERR_InvalidOperatingModeRestore = 0x10,
            ERR_InvalidOperatingModeForCommand = 0x11,
            ERR_Version = 0x1002,
            ERR_Cancelled = 0x1007,
            ERR_CommunicationsError = 0x1008,
            ERR_InsufficientDataReceived = 0x100A,
            ERR_UnexpectedDataReceived = 0x100B,
            ERR_Memory = 0x100C,
            ERR_Timeout = 0x100D,
            ERR_Checksum = 0x100E,
            ERR_CommandError = 0x100F,
            ERR_NotSupported = 0x1101,
            ERR_InvalidOperation = 0x1102,
            ERR_InvalidInterface = 0x1103,
            ERR_InvalidPort = 0x1104,
            ERR_InvalidBaud = 0x1105,
            ERR_InvalidHandle = 0x1106,
            ERR_ParameterOutOfRange = 0x110A,
            ERR_ParameterAlignment = 0x110B,
            ERR_NotConnected = 0x1201,
            ERR_NotResponding = 0x1202,
            ERR_PortRead = 0x1203,
            ERR_PortWrite = 0x1204,
            ERR_OpeningPort = 0x1205,
            ERR_ConfiguringPort = 0x1206,
            ERR_InterfaceNotInitialized = 0x1207,
            ERR_Driver = 0x1208,
            ERR_AddressInUse = 0x1209,
            ERR_IPRouting = 0x120A,
            ERR_RP_Reset = 0x2001,
            ERR_RP_InvalidVersion = 0x2002,
            ERR_RP_InvalidResource = 0x2003,
            ERR_RP_InvalidAddress = 0x2004,
            ERR_RP_InvalidAction = 0x2005,
            ERR_RP_InvalidSubAction = 0x2006,
            ERR_RP_InvalidCommand = 0x2007,
            ERR_RP_InvalidParameter = 0x2008,
            ERR_RP_InvalidPacket = 0x2009,
            ERR_RP_OutOfHandles = 0x200A,
            ERR_RP_Checksum = 0x200E,
            ERR_UC_Signature = 0x2101,
            ERR_UC_Version = 0x2102,
            ERR_UC_FileSize = 0x2103,
            ERR_UC_Checksum = 0x2104,
            ERR_UC_WriteError = 0x2105,
            ERR_UC_NotProgrammed = 0x2106,
            ERR_UC_TaskNotCreated = 0x2107,
            ERR_UC_TaskAlreadyRunning = 0x2108,
            ERR_UC_TaskNotFound = 0x2109,
            ERR_UC_StartupCode = 0x210A
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct PMDEvent_internal {
                    [MarshalAs(UnmanagedType.U2)]
            public PMDAxisNumber axis;
            public UInt16 EventMask;
        }

        public struct PMDEvent {
            public PMDAxisNumber axis;
            public UInt16 EventMask;
        }
    
        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDAdjustActualPosition(IntPtr hAxis, Int32 position);

        [DllImport("C-Motion.dll")]
        internal static extern IntPtr PMDAxisAlloc();

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDAxisClose(IntPtr hAxis);

        [DllImport("C-Motion.dll")]
        internal static extern void PMDAxisFree(IntPtr hAxis);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDAxisOpen(IntPtr hAxis, IntPtr hDev, UInt16 axis_number);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDAtlasAxisOpen(IntPtr hSourceAxis, IntPtr hAtlasAxis);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDTaskGetState(IntPtr hDev, ref PMDTaskState state);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDTaskStart(IntPtr hDev);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDTaskStop(IntPtr hDev);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDClearDriveFaultStatus(IntPtr hAxis);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDClearInterrupt(IntPtr hAxis);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDClearPositionError(IntPtr hAxis);

        [DllImport("C-Motion.dll")]
        internal static extern IntPtr PMDDeviceAlloc();

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDDeviceClose(IntPtr hDev);

        [DllImport("C-Motion.dll")]
        internal static extern void PMDDeviceFree(IntPtr hDev);

        //  The C function uses a pointer to void for the return value
        [DllImport("C-Motion.dll", EntryPoint = "PMDDeviceGetDefault")]
        internal static extern PMDresult PMDDeviceGetDefaultUInt32(IntPtr hDev, PMDDefault code, ref UInt32 value, UInt32 ValueSize);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDDeviceGetVersion(IntPtr hDev, ref UInt32 major, ref UInt32 minor);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDDeviceReset(IntPtr hDev);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDDeviceStoreUserCode(IntPtr hDev, IntPtr pflashBuff, UInt16 filesize);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDGetUserCodeFileVersion(IntPtr hDev, ref UInt32 versione);


        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDGetUserCodeFileName(IntPtr hDev, StringBuilder tmp);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDGetUserCodeFileDate(IntPtr hDev, StringBuilder tmp);
        
        [DllImport("C-Motion.dll", EntryPoint = "PMDDeviceSetDefault")]
        internal static extern PMDresult PMDDeviceSetDefaultUInt32(IntPtr hDev, PMDDefault code, ref UInt32 value, UInt32 ValueSize);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDDriveNVRAM(IntPtr hAxis, UInt16 parameter, UInt16 value);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDGetAcceleration(IntPtr hAxis, ref UInt32 acceleration);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDGetActiveMotorCommand(IntPtr hAxis, ref Int16 command);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDGetActiveOperatingMode(IntPtr hAxis, ref UInt16 mode);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDGetActivityStatus(IntPtr hAxis, ref UInt16 status);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDGetActualPosition(IntPtr hAxis, ref Int32 position);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDGetActualPositionUnits(IntPtr hAxis, ref UInt16 ARG2);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDGetActualVelocity(IntPtr hAxis, ref Int32 ARG2);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDGetAuxiliaryEncoderSource(IntPtr hAxis, ref byte mode, ref UInt16 AuxAxis);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDGetAxisOutMask(IntPtr hAxis, ref UInt16 SourceAxis, ref byte SourceRegister, ref UInt16 SelectionMask, ref UInt16 SenseMask);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDGetBreakpoint(IntPtr hAxis, UInt16 BreakpointId, ref UInt16 SourceAxis, ref byte Action, ref byte Trigger);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDGetBreakpointUpdateMask(IntPtr hAxis, UInt16 ARG2, ref UInt16 ARG3);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDGetBreakpointValue(IntPtr hAxis, UInt16 ARG2, ref Int32 ARG3);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDGetBufferLength(IntPtr hAxis, UInt16 BufferId, ref UInt32 ARG3);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDGetBufferReadIndex(IntPtr hAxis, UInt16 BufferId, ref UInt32 ARG3);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDGetBufferStart(IntPtr hAxis, UInt16 BufferId, ref UInt32 ARG3);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDGetBufferWriteIndex(IntPtr hAxis, UInt16 BufferId, ref UInt32 ARG3);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDGetBusVoltage(IntPtr hAxis, ref UInt16 ARG2);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDGetBusVoltageLimits(IntPtr hAxis, UInt16 parameter, ref UInt16 ARG3);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDGetCANMode(IntPtr hAxis, ref byte NodeId, ref byte TransmissionRate);

        [DllImport("C-Motion.dll")]
        internal static extern void PMDGetCMotionVersion(ref UInt32 ARG1, ref UInt32 ARG2);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDGetCaptureSource(IntPtr hAxis, ref UInt16 ARG2);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDGetCaptureValue(IntPtr hAxis, ref Int32 ARG2);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDGetChecksum(IntPtr hAxis, ref UInt32 ARG2);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDGetCommandedAcceleration(IntPtr hAxis, ref Int32 ARG2);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDGetCommandedPosition(IntPtr hAxis, ref Int32 ARG2);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDGetCommandedVelocity(IntPtr hAxis, ref Int32 ARG2);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDGetCommutationMode(IntPtr hAxis, ref UInt16 ARG2);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDGetCurrent(IntPtr hAxis, UInt16 parameter, ref UInt16 value);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDGetCurrentControlMode(IntPtr hAxis, ref UInt16 ARG2);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDGetCurrentFoldback(IntPtr hAxis, UInt16 parameter, ref UInt16 value);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDGetCurrentLoop(IntPtr hAxis, byte phase, byte parameter, ref UInt16 value);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDGetCurrentLoopValue(IntPtr hAxis, byte ARG2, byte ARG3, ref Int32 ARG4);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDGetDeceleration(IntPtr hAxis, ref UInt32 ARG2);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDGetDefault(IntPtr hAxis, UInt16 ARG2, ref UInt32 ARG3);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDGetDriveFaultStatus(IntPtr hAxis, ref UInt16 ARG2);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDGetDriveCommandMode(IntPtr hAxis, ref byte transport, ref byte format);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDGetDriveFaultParameter(IntPtr hAxis, UInt16 parameter, ref UInt16 value);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDGetDrivePWM(IntPtr hAxis, UInt16 parameter, ref UInt16 value);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDGetDriveStatus(IntPtr hAxis, ref UInt16 ARG2);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDGetEncoderModulus(IntPtr hAxis, ref UInt16 ARG2);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDGetEncoderSource(IntPtr hAxis, ref UInt16 ARG2);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDGetEncoderToStepRatio(IntPtr hAxis, ref UInt16 counts, ref UInt16 steps);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDGetEventAction(IntPtr hAxis, UInt16 ARG2, ref UInt16 ARG3);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDGetEventStatus(IntPtr hAxis, ref UInt16 ARG2);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDGetFeedbackParameter(IntPtr hAxis, UInt16 parameter, ref UInt32 value);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDGetFOC(IntPtr hAxis, byte ARG2, byte ARG3, ref UInt16 ARG4);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDGetFOCValue(IntPtr hAxis, byte ARG2, byte ARG3, ref Int32 ARG4);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDGetFaultOutMask(IntPtr hAxis, ref UInt16 ARG2);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDGetGearMaster(IntPtr hAxis, ref UInt16 MasterAxis, ref UInt16 source);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDGetGearRatio(IntPtr hAxis, ref Int32 ARG2);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDGetHoldingCurrent(IntPtr hAxis, UInt16 ARG2, ref UInt16 ARG3);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDGetInstructionError(IntPtr hAxis, ref UInt16 ARG2);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDGetInterruptAxis(IntPtr hAxis, ref UInt16 ARG2);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDGetInterruptMask(IntPtr hAxis, ref UInt16 ARG2);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDGetJerk(IntPtr hAxis, ref UInt32 ARG2);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDGetMotionCompleteMode(IntPtr hAxis, ref UInt16 ARG2);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDGetMotorBias(IntPtr hAxis, ref Int16 ARG2);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDGetMotorCommand(IntPtr hAxis, ref Int16 ARG2);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDGetMotorLimit(IntPtr hAxis, ref UInt16 ARG2);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDGetMotorType(IntPtr hAxis, ref UInt16 ARG2);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDGetOperatingMode(IntPtr hAxis, ref UInt16 ARG2);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDGetOutputMode(IntPtr hAxis, ref UInt16 ARG2);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDGetOverTemperatureLimit(IntPtr hAxis, ref UInt16 ARG2);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDGetPWMFrequency(IntPtr hAxis, ref UInt16 ARG2);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDGetPhaseAngle(IntPtr hAxis, ref UInt16 ARG2);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDGetPhaseCommand(IntPtr hAxis, UInt16 ARG2, ref Int16 ARG3);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDGetPhaseCorrectionMode(IntPtr hAxis, ref UInt16 ARG2);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDGetPhaseCounts(IntPtr hAxis, ref UInt16 ARG2);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDGetPhaseInitializeMode(IntPtr hAxis, ref UInt16 ARG2);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDGetPhaseInitializeTime(IntPtr hAxis, ref UInt16 ARG2);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDGetPhaseOffset(IntPtr hAxis, ref UInt16 ARG2);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDGetPhasePrescale(IntPtr hAxis, ref UInt16 ARG2);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDGetPosition(IntPtr hAxis, ref Int32 ARG2);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDGetPositionError(IntPtr hAxis, ref Int32 ARG2);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDGetPositionErrorLimit(IntPtr hAxis, ref UInt32 ARG2);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDGetPositionLoop(IntPtr hAxis, UInt16 ARG2, ref Int32 ARG3);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDGetPositionLoopValue(IntPtr hAxis, UInt16 ARG2, ref Int32 ARG3);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDGetProfileMode(IntPtr hAxis, ref UInt16 ARG2);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDGetSPIMode(IntPtr hAxis, ref UInt16 ARG2);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDGetSampleTime(IntPtr hAxis, ref UInt32 ARG2);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDGetSerialPortMode(IntPtr hAxis, ref byte baud, ref byte parity, ref byte StopBits, ref byte protocol, ref byte MultiDropId);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDGetSettleTime(IntPtr hAxis, ref UInt16 ARG2);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDGetSettleWindow(IntPtr hAxis, ref UInt16 ARG2);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDGetSignalSense(IntPtr hAxis, ref UInt16 ARG2);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDGetSignalStatus(IntPtr hAxis, ref UInt16 ARG2);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDGetStartVelocity(IntPtr hAxis, ref UInt32 ARG2);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDGetStepRange(IntPtr hAxis, ref UInt16 ARG2);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDGetStopMode(IntPtr hAxis, ref UInt16 ARG2);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDGetSynchronizationMode(IntPtr hAxis, ref UInt16 ARG2);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDGetTemperature(IntPtr hAxis, ref UInt16 ARG2);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDGetTime(IntPtr hAxis, ref UInt32 ARG2);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDGetTraceCount(IntPtr hAxis, ref UInt32 ARG2);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDGetTraceMode(IntPtr hAxis, ref UInt16 ARG2);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDGetTracePeriod(IntPtr hAxis, ref UInt16 ARG2);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDGetTraceStart(IntPtr hAxis, ref UInt16 ARG2, ref byte ARG3, ref byte ARG4, ref byte ARG5);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDGetTraceStatus(IntPtr hAxis, ref UInt16 ARG2);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDGetTraceStop(IntPtr hAxis, ref UInt16 ARG2, ref byte ARG3, ref byte ARG4, ref byte ARG5);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDGetTraceVariable(IntPtr hAxis, UInt16 ARG2, ref UInt16 ARG3, ref byte ARG4);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDGetTrackingWindow(IntPtr hAxis, ref UInt16 ARG2);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDGetUpdateMask(IntPtr hAxis, ref UInt16 ARG2);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDGetVelocity(IntPtr hAxis, ref Int32 ARG2);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDGetVersion(IntPtr hAxis, ref UInt16 family, ref UInt16 motorType, ref UInt16 numberAxes, ref UInt16 special_and_chip_count, ref UInt16 custom, ref UInt16 major, ref UInt16 minor);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDInitializePhase(IntPtr hAxis);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDMPDeviceOpen(IntPtr hDev, IntPtr hPeriph);

        [DllImport("C-Motion.dll")]
        internal static extern IntPtr PMDMemoryAlloc();

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDMemoryClose(IntPtr hMem);

        [DllImport("C-Motion.dll")]
        internal static extern void PMDMemoryFree(IntPtr hMem);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDMemoryOpen(IntPtr hMem, IntPtr hDev, PMDDataSize DataSize, PMDMemoryType MemType);

        //  Note that the ByRef data argument is the first element of an array.
        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDMemoryRead(IntPtr hMem, ref UInt32 data, UInt32 offset, UInt32 length);

        // Note that the ByRef data argument is the first element of an array
        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDMemoryWrite(IntPtr hMem, ref UInt32 data, UInt32 offset, UInt32 length);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDMultiUpdate(IntPtr hAxis, UInt16 ARG2);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDNoOperation(IntPtr hAxis);

        [DllImport("C-Motion.dll")]
        internal static extern IntPtr PMDPeriphAlloc();

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDPeriphClose(IntPtr hPeriph);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDPeriphFlush(IntPtr hPeriph);

        [DllImport("C-Motion.dll")]
        internal static extern void PMDPeriphFree(IntPtr hPeriph);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDPeriphOpenCAN(IntPtr hPeriph, IntPtr hDev, UInt32 transmitid, UInt32 receiveid, UInt32 eventid);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDPeriphOpenCME(IntPtr hPeriph, IntPtr hDev);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDPeriphOpenCOM(IntPtr hPeriph, IntPtr hDev, UInt32 portnum, UInt32 baud, PMDSerialParity parity, PMDSerialStopBits StopBits);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDPeriphOpenMultiDrop(IntPtr hPeriphParent, IntPtr hPeriph, UInt32 ARG3);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDPeriphOpenPCI(IntPtr hPeriph, UInt32 ARG2);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDPeriphOpenTCP(IntPtr hPeriph, IntPtr hDev, UInt32 IPAddress, UInt32 portnum, UInt32 timeout);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDPeriphOpenPIO(IntPtr hPeriph, IntPtr hDev, UInt16 address, byte eventIRQ, PMDDataSize datasize);

        //  These functions are for reading and writing 8 or 16 bit data over a parallel bus peripheral.
        //  The width of the data has to be correct for the peripheral type.
        //  The data argument should be the first member of an array of Byte or UInt16.
        [DllImport("C-Motion.dll", EntryPoint = "PMDPeriphRead")]
        internal static extern PMDresult PMDPeriphReadUInt8(IntPtr hPeriph, ref byte data, UInt32 offset, UInt32 length);

        [DllImport("C-Motion.dll", EntryPoint = "PMDPeriphRead")]
        internal static extern PMDresult PMDPeriphReadUInt16(IntPtr hPeriph, ref UInt16 data, UInt32 offset, UInt32 length);

        [DllImport("C-Motion.dll", EntryPoint = "PMDPeriphWrite")]
        internal static extern PMDresult PMDPeriphWriteUInt8(IntPtr hPeriph, ref byte data, UInt32 offset, UInt32 length);

        [DllImport("C-Motion.dll", EntryPoint = "PMDPeriphWrite")]
        internal static extern PMDresult PMDPeriphWriteUInt16(IntPtr hPeriph, ref UInt16 data, UInt32 offset, UInt32 length);

        //  data should be an array of unsigned bytes, the first element should be passed.
        //  strings could be supported as well.
        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDPeriphReceive(IntPtr hPeriph, ref byte data, ref UInt32 nReceived, UInt32 nExpected, UInt32 timeout);

        //  data should be an array of unsigned bytes, the first element should be passed.
        //  strings could be supported as well.
        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDPeriphSend(IntPtr hPeriph, ref byte data, UInt32 nCount, UInt32 timeout);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDRPDeviceOpen(IntPtr hDev, IntPtr hPeriph);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDReadAnalog(IntPtr hAxis, UInt16 ARG2, ref UInt16 ARG3);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDReadBuffer(IntPtr hAxis, UInt16 bufferID, ref Int32 data);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDReadBuffer16(IntPtr hAxis, UInt16 bufferID, ref Int16 data);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDReadIO(IntPtr hAxis, UInt16 ARG2, ref UInt16 ARG3);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDReset(IntPtr hAxis);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDResetEventStatus(IntPtr hAxis, UInt16 ARG2);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDRestoreOperatingMode(IntPtr hAxis);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDSetAcceleration(IntPtr hAxis, UInt32 ARG2);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDSetActualPosition(IntPtr hAxis, Int32 ARG2);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDSetActualPositionUnits(IntPtr hAxis, UInt16 ARG2);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDSetAuxiliaryEncoderSource(IntPtr hAxis, byte ARG2, UInt16 ARG3);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDSetAxisOutMask(IntPtr hAxis, UInt16 ARG2, byte ARG3, UInt16 ARG4, UInt16 ARG5);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDSetBreakpoint(IntPtr hAxis, UInt16 ARG2, UInt16 ARG3, byte ARG4, byte ARG5);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDSetBreakpointUpdateMask(IntPtr hAxis, UInt16 ARG2, UInt16 ARG3);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDSetBreakpointValue(IntPtr hAxis, UInt16 ARG2, Int32 ARG3);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDSetBufferLength(IntPtr hAxis, UInt16 ARG2, UInt32 ARG3);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDSetBufferReadIndex(IntPtr hAxis, UInt16 ARG2, UInt32 ARG3);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDSetBufferStart(IntPtr hAxis, UInt16 ARG2, UInt32 ARG3);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDSetBufferWriteIndex(IntPtr hAxis, UInt16 ARG2, UInt32 ARG3);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDSetBusVoltageLimits(IntPtr hAxis, UInt16 ARG2, UInt16 ARG3);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDSetCANMode(IntPtr hAxis, byte ARG2, byte ARG3);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDSetCaptureSource(IntPtr hAxis, UInt16 ARG2);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDSetCommutationMode(IntPtr hAxis, UInt16 ARG2);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDSetCurrent(IntPtr hAxis, UInt16 parameter, UInt16 value);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDSetCurrentControlMode(IntPtr hAxis, UInt16 ARG2);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDSetCurrentFoldback(IntPtr hAxis, UInt16 parameter, UInt16 value);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDSetCurrentLoop(IntPtr hAxis, byte phase, byte parameter, UInt16 value);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDSetDeceleration(IntPtr hAxis, UInt32 ARG2);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDSetDefault(IntPtr hAxis, UInt16 ARG2, UInt32 ARG3);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDSetDriveCommandMode(IntPtr hAxis, byte transport, byte format);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDSetDriveFaultParameter(IntPtr hAxis, UInt16 parameter, UInt16 value);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDSetDrivePWM(IntPtr hAxis, UInt16 parameter, UInt16 value);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDSetEncoderModulus(IntPtr hAxis, UInt16 ARG2);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDSetEncoderSource(IntPtr hAxis, UInt16 ARG2);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDSetEncoderToStepRatio(IntPtr hAxis, UInt16 counts, UInt16 steps);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDSetEventAction(IntPtr hAxis, UInt16 ARG2, UInt16 ARG3);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDSetFeedbackParameter(IntPtr hAxis, UInt16 parameter, UInt32 value);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDSetFOC(IntPtr hAxis, byte ARG2, byte ARG3, UInt16 ARG4);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDSetFaultOutMask(IntPtr hAxis, UInt16 ARG2);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDSetGearMaster(IntPtr hAxis, UInt16 MasterAxis, UInt16 source);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDSetGearRatio(IntPtr hAxis, Int32 ARG2);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDSetHoldingCurrent(IntPtr hAxis, UInt16 ARG2, UInt16 ARG3);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDSetInterruptMask(IntPtr hAxis, UInt16 ARG2);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDSetJerk(IntPtr hAxis, UInt32 ARG2);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDSetMotionCompleteMode(IntPtr hAxis, UInt16 ARG2);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDSetMotorBias(IntPtr hAxis, Int16 ARG2);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDSetMotorCommand(IntPtr hAxis, Int16 ARG2);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDSetMotorLimit(IntPtr hAxis, UInt16 ARG2);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDSetMotorType(IntPtr hAxis, UInt16 ARG2);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDSetOperatingMode(IntPtr hAxis, UInt16 ARG2);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDSetOutputMode(IntPtr hAxis, UInt16 ARG2);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDSetOverTemperatureLimit(IntPtr hAxis, UInt16 ARG2);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDSetPWMFrequency(IntPtr hAxis, UInt16 ARG2);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDSetPhaseAngle(IntPtr hAxis, UInt16 ARG2);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDSetPhaseCorrectionMode(IntPtr hAxis, UInt16 ARG2);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDSetPhaseCounts(IntPtr hAxis, UInt16 ARG2);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDSetPhaseInitializeMode(IntPtr hAxis, UInt16 ARG2);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDSetPhaseInitializeTime(IntPtr hAxis, UInt16 ARG2);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDSetPhaseOffset(IntPtr hAxis, UInt16 ARG2);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDSetPhasePrescale(IntPtr hAxis, UInt16 ARG2);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDSetPosition(IntPtr hAxis, Int32 ARG2);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDSetPositionErrorLimit(IntPtr hAxis, UInt32 ARG2);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDSetPositionLoop(IntPtr hAxis, UInt16 ARG2, Int32 ARG3);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDSetProfileMode(IntPtr hAxis, UInt16 ARG2);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDSetSPIMode(IntPtr hAxis, UInt16 ARG2);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDSetSampleTime(IntPtr hAxis, UInt32 ARG2);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDSetSerialPortMode(IntPtr hAxis, byte ARG2, byte ARG3, byte ARG4, byte ARG5, byte ARG6);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDSetSettleTime(IntPtr hAxis, UInt16 ARG2);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDSetSettleWindow(IntPtr hAxis, UInt16 ARG2);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDSetSignalSense(IntPtr hAxis, UInt16 ARG2);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDSetStartVelocity(IntPtr hAxis, UInt32 ARG2);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDSetStepRange(IntPtr hAxis, UInt16 ARG2);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDSetStopMode(IntPtr hAxis, UInt16 ARG2);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDSetSynchronizationMode(IntPtr hAxis, UInt16 ARG2);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDSetTraceMode(IntPtr hAxis, UInt16 ARG2);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDSetTracePeriod(IntPtr hAxis, UInt16 ARG2);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDSetTraceStart(IntPtr hAxis, UInt16 ARG2, byte ARG3, byte ARG4, byte ARG5);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDSetTraceStop(IntPtr hAxis, UInt16 ARG2, byte ARG3, byte ARG4, byte ARG5);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDSetTraceVariable(IntPtr hAxis, UInt16 ARG2, UInt16 ARG3, byte ARG4);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDSetTrackingWindow(IntPtr hAxis, UInt16 ARG2);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDSetUpdateMask(IntPtr hAxis, UInt16 ARG2);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDSetVelocity(IntPtr hAxis, Int32 ARG2);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDUpdate(IntPtr hAxis);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDWaitForEvent(IntPtr hDev, ref PMDEvent_internal EventStruct, UInt32 timeout);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDWriteBuffer(IntPtr hAxis, UInt16 ARG2, Int32 ARG3);

        [DllImport("C-Motion.dll")]
        internal static extern PMDresult PMDWriteIO(IntPtr hAxis, UInt16 ARG2, UInt16 ARG3);

 // *** Utility functions
        public string PMDresultString(PMDresult status) {
            switch (status) {
                case PMDresult.NOERROR:
                    return "0: No error";
                  //  break;
                case PMDresult.ERR_Reset:
                    return "1: Reset";
                //   break;
                case PMDresult.ERR_InvalidInstruction:
                    return "2: Invalid Instruction";
                //    break;
                case PMDresult.ERR_InvalidAxis:
                    return "3: Invalid Axis";
                //    break;
                case PMDresult.ERR_InvalidParameter:
                    return "4: Invalid Parameter";
                //    break;
                case PMDresult.ERR_TraceRunning:
                    return "5: Trace Running";
                //    break;
                case PMDresult.ERR_BlockOutOfBounds:
                    return "7: Block Out Of Bounds";
                //    break;
                case PMDresult.ERR_TraceBufferZero:
                    return "8: Trace Buffer Zero";
                //    break;
                case PMDresult.ERR_BadSerialChecksum:
                    return "9: Bad Serial Checksum";
                //    break;
                case PMDresult.ERR_InvalidNegativeValue:
                    return "11: Invalid Negative Value";
                //    break;
                case PMDresult.ERR_InvalidParameterChange:
                    return "12: Invalid Parameter Change";
               //     break;
                case PMDresult.ERR_LimitEventPending:
                    return "13: Limit Event Pending";
                 //   break;
                case PMDresult.ERR_InvalidMoveIntoLimit:
                    return "14: Invalid Move Into Limit";
                //    break;
                case PMDresult.ERR_InvalidOperatingModeRestore:
                    return "16: Invalid Operating Mode Restore";
                  //  break;
                case PMDresult.ERR_InvalidOperatingModeForCommand:
                    return "17: Invalid Operating Mode For Command";
                 //   break;
                case PMDresult.ERR_Version:
                    return "4098: Version";
                 //   break;
                case PMDresult.ERR_Cancelled:
                    return "4103: Cancelled";
                //    break;
                case PMDresult.ERR_CommunicationsError:
                    return "4104: Communications Error";
                //    break;
                case PMDresult.ERR_InsufficientDataReceived:
                    return "4106: Insufficient Data Received";
                //    break;
                case PMDresult.ERR_UnexpectedDataReceived:
                    return "4107: Unexpected Data Received";
                //    break;
                case PMDresult.ERR_Memory:
                    return "4108: Memory";
                //    break;
                case PMDresult.ERR_Timeout:
                    return "4109: Timeout";
                //    break;
                case PMDresult.ERR_Checksum:
                    return "4110: Checksum";
                //    break;
                case PMDresult.ERR_CommandError:
                    return "4111: Command Error";
                //    break;
                case PMDresult.ERR_NotSupported:
                    return "4353: Not Supported";
                //    break;
                case PMDresult.ERR_InvalidOperation:
                    return "4354: Invalid Operation";
                //    break;
                case PMDresult.ERR_InvalidInterface:
                    return "4355: Invalid Interface";
                //    break;
                case PMDresult.ERR_InvalidPort:
                    return "4356: Invalid Port";
                //    break;
                case PMDresult.ERR_InvalidBaud:
                    return "4357: Invalid Baud";
                //    break;
                case PMDresult.ERR_InvalidHandle:
                    return "4358: Invalid Handle";
                //    break;
                case PMDresult.ERR_ParameterOutOfRange:
                    return "4362: Parameter Out Of Range";
                //    break;
                case PMDresult.ERR_ParameterAlignment:
                    return "4363: Parameter Alignment";
                //    break;
                case PMDresult.ERR_NotConnected:
                    return "4609: Not Connected";
                //    break;
                case PMDresult.ERR_NotResponding:
                    return "4610: Not Responding";
                //    break;
                case PMDresult.ERR_PortRead:
                    return "4611: Port Read";
                //    break;
                case PMDresult.ERR_PortWrite:
                    return "4612: Port Write";
                //    break;
                case PMDresult.ERR_OpeningPort:
                    return "4613: Opening Port";
               //     break;
                case PMDresult.ERR_ConfiguringPort:
                    return "4614: Configuring Port";
               //     break;
                case PMDresult.ERR_InterfaceNotInitialized:
                    return "4615: Interface Not Initialized";
                //    break;
                case PMDresult.ERR_Driver:
                    return "4616: Driver";
                //    break;
                case PMDresult.ERR_AddressInUse:
                    return "4617: Address In Use";
                //    break;
                case PMDresult.ERR_IPRouting:
                    return "4618: IPRouting";
               //     break;
                case PMDresult.ERR_RP_Reset:
                    return "8193: RP Reset";
                //    break;
                case PMDresult.ERR_RP_InvalidVersion:
                    return "8194: RP Invalid Version";
               //     break;
                case PMDresult.ERR_RP_InvalidResource:
                    return "8195: RP Invalid Resource";
                //    break;
                case PMDresult.ERR_RP_InvalidAddress:
                    return "8196: RP Invalid Address";
                //    break;
                case PMDresult.ERR_RP_InvalidAction:
                    return "8197: RP Invalid Action";
                //    break;
                case PMDresult.ERR_RP_InvalidSubAction:
                    return "8198: RP Invalid Sub Action";
                //    break;
                case PMDresult.ERR_RP_InvalidCommand:
                    return "8199: RP Invalid Command";
                //    break;
                case PMDresult.ERR_RP_InvalidParameter:
                    return "8200: RP Invalid Parameter";
                //    break;
                case PMDresult.ERR_RP_InvalidPacket:
                    return "8201: RP Invalid Packet";
                //    break;
                case PMDresult.ERR_RP_OutOfHandles:
                    return "8202: RP Out Of Handles";
               //     break;
                case PMDresult.ERR_RP_Checksum:
                    return "8206: RP Checksum";
                //    break;
                case PMDresult.ERR_UC_Signature:
                    return "8449: UC Signature";
                //    break;
                case PMDresult.ERR_UC_Version:
                    return "8450: UC Version";
                //    break;
                case PMDresult.ERR_UC_FileSize:
                    return "8451: UC File Size";
               //     break;
                case PMDresult.ERR_UC_Checksum:
                    return "8452: UC Checksum";
               //     break;
                case PMDresult.ERR_UC_WriteError:
                    return "8453: UC Write Error";
                //    break;
                case PMDresult.ERR_UC_NotProgrammed:
                    return "8454: UC Not Programmed";
                //    break;
                case PMDresult.ERR_UC_TaskNotCreated:
                    return "8455: UC Task Not Created";
                //    break;
                case PMDresult.ERR_UC_TaskAlreadyRunning:
                    return "8456: UC Task Already Running";
                //    break;
                case PMDresult.ERR_UC_TaskNotFound:
                    return "8457: UC Task Not Found";
                //    break;
                case PMDresult.ERR_UC_StartupCode:
                    return "8458: UC Startup Code";
                //    break;
                default:
                    return (status + ": ??");
                //    break;
            }
        }

        
        public class PMDPeripheral {
    
            // *** Private data and utility functions ***
            // hPeriph is a pointer to the C-allocated peripheral handle, it is not user-visible in VB
            internal IntPtr hPeriph;
    
            internal PMDresult status;
    
            internal void CheckResult(PMDresult status) {
                this.status = status;
                if (!(status == PMDresult.NOERROR)) {
                    Exception e = new Exception(("ERROR: PMDPeripheral " + status));
                    e.Data.Add("PMDresult", status);
                    throw e;
                }
            }
    
            // *** Public Methods ***
            public PMDPeripheral() {
                hPeriph = PMDPeriphAlloc();
                if ((hPeriph == null)) {
                    throw new Exception("ERROR: PMD library: could not allocate peripheral object");
                }
            }
    
 
             public void Close() {
                if (!(hPeriph == null))
                {
                    PMDPeriphClose(hPeriph);
                    PMDPeriphFree(hPeriph);
                    hPeriph = (System.IntPtr)0;

                }
             
            }
    
            public PMDresult LastError {
                get {
                    return status;
                }
            }
    
            public void Flush() {
                CheckResult(PMDPeriphFlush(hPeriph));
            }
    
            public void Send(ref byte[] data, UInt32 nCount, UInt32 timeout) {
                if ((nCount > data.Length)) {
                    throw new Exception("PMDPeripheral.Send bad nCount");
                }
                CheckResult(PMDPeriphSend(hPeriph, ref data[0], nCount, timeout));
            }
    
            public void Receive(ref byte[] data, ref UInt32 nReceived, UInt32 nExpected, UInt32 timeout) {
                UInt32 nrecv=0;
                if ((nExpected > data.Length)) {
                    throw new Exception("PMDPeripheral.Receive bad nExpected");
                }
                CheckResult(PMDPeriphReceive(hPeriph, ref data[0], ref nrecv, nExpected, timeout));
                nReceived = nrecv;
            }
    
            public void Read(ref byte[] data, UInt32 offset, UInt32 length) {
                if ((length > data.Length)) {
                    throw new Exception("PMDPeripheral.Read bad length");
                }
                CheckResult(PMDPeriphReadUInt8(hPeriph, ref data[0], offset, length));
            }
    
            public void Read(ref UInt16[] data, UInt32 offset, UInt32 length) {
                if ((length > data.Length)) {
                    throw new Exception("PMDPeripheral.Read bad length");
                }
                CheckResult(PMDPeriphReadUInt16(hPeriph, ref data[0], offset, length));
            }
    
            public UInt16 Read(UInt32 offset) {
                UInt16 data=0;
                CheckResult(PMDPeriphReadUInt16(hPeriph, ref data, offset, 1));
                return data;
            }
    
            public void Write(ref byte[] data, UInt32 offset, UInt32 length) {
                if ((length > data.Length)) {
                    throw new Exception("PMDPeripheral.Write bad length");
                }
                CheckResult(PMDPeriphWriteUInt8(hPeriph, ref data[0], offset, length));
            }
    
            public void Write(ref UInt16[] data, UInt32 offset, UInt32 length) {
                if ((length > data.Length)) {
                    throw new Exception("PMDPeripheral.Write bad length");
                }
                CheckResult(PMDPeriphWriteUInt16(hPeriph, ref data[0], offset, length));
            }
    
            public void Write(UInt16 data, UInt32 offset) {
                CheckResult(PMDPeriphWriteUInt16(hPeriph, ref data, offset, 1));
            }
        }
        // PMDPeripheral

        
        public class PMDDevice {
    
            // *** Private data and utility functions ***
            internal IntPtr hDev;
    
            public PMDDeviceType DeviceType;
           // this.PMDDeviceType             
    
            internal PMDresult status;
    
            internal void CheckResult(PMDresult status) {
                this.status = status;
                if (!(status == PMDresult.NOERROR)) {
                    Exception e = new Exception(("ERROR: PMDDevice " + status));
                    e.Data.Add("PMDresult", status);
                    throw e;
                }
             }
    
            // *** Public Methods ***
            public PMDDevice(PMDPeripheral periph, PMDDeviceType dtype) {
                hDev = PMDDeviceAlloc();
                if ((hDev == null)) {
                    throw new Exception("ERROR: PMD library: could not allocate device object");
                }
                switch (dtype) {
                case PMDDeviceType.MotionProcessor:
                    CheckResult(PMDMPDeviceOpen(hDev, periph.hPeriph));
                    break;
                case PMDDeviceType.ResourceProtocol:
                    CheckResult(PMDRPDeviceOpen(hDev, periph.hPeriph));
                    // *** Check for ERR_RP_Reset
                    // Remove the following section if you want to be informed of unexpected resets
                    System.UInt32 major=0;
                    System.UInt32 minor=0;
                    this.status = PMDDeviceGetVersion(this.hDev, ref major, ref minor);
                    if ((PMDresult.ERR_RP_Reset == this.status)) {
                        this.status = PMDDeviceGetVersion(this.hDev, ref major, ref minor);
                    }
                    CheckResult(this.status);
                    // End check for ERR_RP_Reset
                    break;
                }
                DeviceType = dtype;
            }
    
              public void Close() {
                if (!(hDev == null))
                {
                    PMDDeviceClose(hDev);
                    PMDDeviceFree(hDev);
                    hDev = (IntPtr)0;
                } 
           
            }
    
            public PMDresult LastError {
                get {
                    return status;
                }
            }
    
            public void Reset() {
                PMDDeviceReset(hDev);
            }
    
            
            public void StoreUserCode(IntPtr pdata, UInt16 length) {
                CheckResult(PMDDeviceStoreUserCode(hDev, pdata, length));
            }

            public UInt32 UserCodeVersion {
                get {
                    UInt32 tmp=0;
                    CheckResult(PMDGetUserCodeFileVersion(hDev, ref tmp));
                    return tmp;
                }
            }

            public StringBuilder UserFile {
                get {
                    StringBuilder tmp = new StringBuilder(new String(' ', 25));
                    CheckResult(PMDGetUserCodeFileName(hDev, tmp));
                    return tmp;
                }
            }

            public StringBuilder UserFileDate{
                get {
                    StringBuilder tmp = new StringBuilder(new String(' ', 25));
                    CheckResult(PMDGetUserCodeFileDate(hDev, tmp));
                    return tmp;
                }

            }
            
            public void Version(ref UInt32 major, ref UInt32 minor)
            {
                CheckResult(PMDDeviceGetVersion(hDev, ref major, ref minor));
            }
    
            public PMDresult WaitForEvent(ref PMDEvent EventStruct, UInt32 timeout) {
                PMDresult r;
                PMDEvent_internal ev;
                ev.axis = EventStruct.axis;
                ev.EventMask = EventStruct.EventMask;
                r = PMDWaitForEvent(hDev, ref ev, timeout);
                CheckResult(r);
                EventStruct.axis = ev.axis;
                EventStruct.EventMask = ev.EventMask;
                return r;
            }
    
            public UInt32 GetDefault(PMDDefault code) {
                UInt32 value=0;
                UInt32 datasize;
                datasize = 4;
                CheckResult(PMDDeviceGetDefaultUInt32(hDev, code, ref value, datasize));
                return value;
            }
    
            public void SetDefault(PMDDefault code, UInt32 value) {
                CheckResult(PMDDeviceSetDefaultUInt32(hDev, code,  ref value, 4));
            }
    
            // Task control methods
            public PMDTaskState TaskState {
                get {
                    PMDTaskState state=0;
                    CheckResult(PMDTaskGetState(hDev, ref state));
                    return state;
                }
            }
    
            public void TaskStart() {
                CheckResult(PMDTaskStart(hDev));
            }
    
            public void TaskStop() {
                CheckResult(PMDTaskStop(hDev));
            }
        }
        
        
        
        
        
        
        public class PMDPeripheralCAN : PMDPeripheral
        {

            public PMDPeripheralCAN(UInt32 transmitid, UInt32 receiveid, UInt32 eventid)
            {
                PMDresult r;
                IntPtr test=(IntPtr) 0;
                r = PMDPeriphOpenCAN(hPeriph, test, transmitid, receiveid, eventid);
                if (!(r == PMDresult.NOERROR))
                {
                    base.Close();
                    CheckResult(r);
                }
            }
        }
        // PMDPeripheralCAN
        public class PMDPeripheralCOM : PMDPeripheral
        {

            public PMDPeripheralCOM(UInt32 portnum, UInt32 baud, PMDSerialParity parity, PMDSerialStopBits StopBits)
            {
                PMDresult r;
                IntPtr test = (IntPtr)0;
                r = PMDPeriphOpenCOM(hPeriph, test, portnum, baud, parity, StopBits);
                if (!(r == PMDresult.NOERROR))
                {
                    base.Close();
                    CheckResult(r);
                }
            }
        }
        // PMDPeripheralCOM


        public class PMDPeripheralMultiDrop : PMDPeripheral
        {
    
            public PMDPeripheralMultiDrop(ref PMDPeripheral parent, Int16 address) {
                PMDresult r;
                r = PMDPeriphOpenMultiDrop(hPeriph, parent.hPeriph, (uint) address);
                if (!(r == PMDresult.NOERROR)) {
                    base.Close();
                    CheckResult(r);
                }
            }
        }
        // PMDPeripheralMultiDrop
        public class PMDPeripheralCME : PMDPeripheral {
    
            public PMDPeripheralCME(ref PMDDevice device) {
                PMDresult r;
                r = PMDPeriphOpenCME(hPeriph, device.hDev);
                if (!(r == PMDresult.NOERROR)) {
                    base.Close();
                    CheckResult(r);
                }
            }
        }
        // PMDPeripheralCME
        public class PMDPeripheralPCI : PMDPeripheral {
    
            public PMDPeripheralPCI(Int16 boardnum) {
                PMDresult r;
                r = PMDPeriphOpenPCI(hPeriph, (uint) (boardnum));
                if (!(r == PMDresult.NOERROR)) {
                    base.Close();
                    CheckResult(r);
                }
            }
        }
        // PMDPeripheralPCI
        public class PMDPeripheralTCP : PMDPeripheral {
    
            public PMDPeripheralTCP(System.Net.IPAddress address, UInt32 portnum, UInt32 timeout) {
                PMDresult r;
                UInt32 uaddr;
                byte[] addr_bytes;
                int i;
                if (!address.AddressFamily.Equals(System.Net.Sockets.AddressFamily.InterNetwork)) {
                    throw new Exception("PMDPeripheralTCP supports only IPV4");
                }
                //  High byte is first, low byte is last
                addr_bytes = address.GetAddressBytes();
                uaddr = 0;
                for (i = 0; (i<= (addr_bytes.Length - 1)); i++) {
                    uaddr = (uaddr<<8) + addr_bytes[i];
                }
                IntPtr test = (IntPtr)0;
                r = PMDPeriphOpenTCP(hPeriph, test, uaddr, portnum, timeout);
                if (!(r == PMDresult.NOERROR)) {
                    base.Close();
                    CheckResult(r);
                }
            }
        }
        // PMDPeripheralTCP

        public class PMDPeripheralPIO : PMDPeripheral
        {

            public PMDPeripheralPIO(PMDDevice device, Int16 address, byte eventIRQ, PMDDataSize datasize)
            {
                PMDresult r;
                r = PMDPeriphOpenPIO(hPeriph, device.hDev, (ushort) address, eventIRQ, datasize);
                if (!(r == PMDresult.NOERROR))
                {
                    base.Close();
                    CheckResult(r);
                }
            }
        }
       
       
        public class PMDAxis {
    
            // *** Private data and utility functions ***
            internal IntPtr hAxis;
    
            internal PMDresult status;
    
            internal void CheckResult(PMDresult status) {
                this.status = status;
                if (!(status == PMDresult.NOERROR)) {
                    Exception e = new Exception(("ERROR: PMDAxis " + status));
                    e.Data.Add("PMDresult", status);
                    throw e;
                }
            }
    
            internal PMDAxis() {
                hAxis = PMDAxisAlloc();
                if ((hAxis == null)) {
                    throw new Exception("ERROR: PMD library: could not allocate axis object");
                }
            }
    
            // *** Public Methods ***
            public PMDAxis(PMDDevice device, PMDAxisNumber AxisNumber) {
                hAxis = PMDAxisAlloc();
                if ((hAxis == null)) {
                    throw new Exception("ERROR: PMD library: could not allocate axis object");
                }
                CheckResult(PMDAxisOpen(hAxis, device.hDev, (ushort) AxisNumber));
            }
    
            public PMDAxis AtlasAxis() {
                PMDAxis Atlas;
                Atlas = new PMDAxis();
                Atlas.hAxis = PMDAxisAlloc();
                if ((Atlas.hAxis == null)) {
                    throw new Exception("ERROR: PMD library: could not allocate axis object");
                }
                CheckResult(PMDAtlasAxisOpen(hAxis, Atlas.hAxis));
                return Atlas;
            }
    
              public void Close() {
                if (!(hAxis == null))
                {
                    PMDAxisClose(hAxis);
                    PMDAxisFree(hAxis);
                    hAxis = (IntPtr)0;
                } 
            }
    
            public PMDresult LastError {
                get {
                    return status;
                }
            }
    
            public void Reset() {
                PMDReset(hAxis);
            }
    
            //  Axis Class Properties and Methods
            public UInt32 Acceleration {
                get {
                    UInt32 tmp=0;
                    CheckResult(PMDGetAcceleration(hAxis, ref tmp));
                    return tmp;
                }
                set {
                    CheckResult(PMDSetAcceleration(hAxis, value));
                }
            }
    
            public Int16 ActiveMotorCommand {
                get {
                    Int16 tmp=0;
                    CheckResult(PMDGetActiveMotorCommand(hAxis, ref tmp));
                    return tmp;
                }
            }
    
            public UInt16 ActiveOperatingMode {
                get {
                    UInt16 tmp=0;
                    CheckResult(PMDGetActiveOperatingMode(hAxis, ref tmp));
                    return tmp;
                }
            }
    
            public UInt16 ActivityStatus {
                get {
                    UInt16 tmp=0;
                    CheckResult(PMDGetActivityStatus(hAxis, ref tmp));
                    return tmp;
                }
            }
    
            public Int32 ActualPosition {
                get {
                    Int32 tmp=0;
                    CheckResult(PMDGetActualPosition(hAxis, ref tmp));
                    return tmp;
                }
                set {
                    CheckResult(PMDSetActualPosition(hAxis, value));
                }
            }
    
            public void AdjustActualPosition(Int32 increment) {
                CheckResult(PMDAdjustActualPosition(hAxis, increment));
            }
    
            public PMDActualPositionUnits ActualPositionUnits {
                get {
                    UInt16 tmp=0;
                    CheckResult(PMDGetActualPositionUnits(hAxis, ref tmp));
                    return ((PMDActualPositionUnits)(tmp));
                }
                set {
                    CheckResult(PMDSetActualPositionUnits(hAxis, ((UInt16)(value))));
                }
            }
    
            public Int32 ActualVelocity {
                get {
                    Int32 tmp=0;
                    CheckResult(PMDGetActualVelocity(hAxis, ref tmp));
                    return tmp;
                }
            }
    
            public UInt16 ReadAnalog(Int16 portID) {
                UInt16 tmp=0;
                CheckResult(PMDReadAnalog(hAxis, (ushort) portID, ref tmp));
                return tmp;
            }
    
            public void GetAuxiliaryEncoderSource(ref PMDAuxiliaryEncoderMode mode, ref PMDAxisNumber AuxAxis) {
                byte tmp1=0;
                UInt16 tmp2=0;
                CheckResult(PMDGetAuxiliaryEncoderSource(hAxis, ref tmp1, ref tmp2));
                mode = ((PMDAuxiliaryEncoderMode)(tmp1));
                AuxAxis = ((PMDAxisNumber)(tmp2));
            }
    
            public void SetAuxiliaryEncoderSource(PMDAuxiliaryEncoderMode mode, PMDAxisNumber AuxAxis) {
                CheckResult(PMDSetAuxiliaryEncoderSource(hAxis, ((byte)(mode)), ((UInt16)(AuxAxis))));
            }
    
            public void GetAxisOutMask(ref PMDAxisNumber sourceAxis, ref PMDAxisOutRegister sourceRegister, ref UInt16 selectionMask, ref UInt16 senseMask) {
                UInt16 tmp1=0;
                byte tmp2=0;
                UInt16 tmp3=0;
                UInt16 tmp4=0;
                CheckResult(PMDGetAxisOutMask(hAxis, ref tmp1, ref tmp2, ref tmp3, ref tmp4));
                sourceAxis = ((PMDAxisNumber)(tmp1));
                sourceRegister = ((PMDAxisOutRegister)(tmp2));
                selectionMask = tmp3;
                senseMask = tmp4;
            }
    
            public void SetAxisOutMask(PMDAxisNumber sourceAxis, PMDAxisOutRegister sourceRegister, UInt16 selectionMask, UInt16 senseMask) {
                CheckResult(PMDSetAxisOutMask(hAxis, ((UInt16)(sourceAxis)), ((byte)(sourceRegister)), selectionMask, senseMask));
            }
    
            public void GetBreakpoint(Int16 BreakpointId, ref PMDAxisNumber SourceAxis, ref PMDBreakpointAction Action, ref PMDBreakpointTrigger Trigger) {
                UInt16 tmp2=0;
                byte tmp3=0;
                byte tmp4=0;
                CheckResult(PMDGetBreakpoint(hAxis, (byte) BreakpointId, ref tmp2, ref tmp3, ref tmp4));
                SourceAxis = ((PMDAxisNumber)(tmp2));
                Action = ((PMDBreakpointAction)(tmp3));
                Trigger = ((PMDBreakpointTrigger)(tmp4));
            }
    
            public void SetBreakpoint(PMDBreakpoint BreakpointId, PMDAxisNumber SourceAxis, PMDBreakpointAction Action, PMDBreakpointTrigger Trigger) {
                CheckResult(PMDSetBreakpoint(hAxis, (byte) BreakpointId, ((UInt16)(SourceAxis)), ((byte)(Action)), ((byte)(Trigger))));
            }
    
            public void GetBreakpointUpdateMask(Int16 BreakpointId, ref Int16 Mask) {
                ushort tempMask=0;
                CheckResult(PMDGetBreakpointUpdateMask(hAxis, (ushort) BreakpointId, ref tempMask));
                Mask = (Int16) tempMask;
            }
    
            public void SetBreakpointUpdateMask(Int16 BreakpointId, Int16 Mask) {
                CheckResult(PMDSetBreakpointUpdateMask(hAxis, (ushort) BreakpointId, (ushort) Mask));
            }
    
            public Int32 GetBreakpointValue(Int16 BreakpointId) {
                Int32 tmp=0;
                CheckResult(PMDGetBreakpointValue(hAxis, (byte) BreakpointId, ref tmp));
                return tmp;
            }
    
            public void SetBreakpointValue(PMD.PMDBreakpoint BreakpointId, Int32 value) {
                CheckResult(PMDSetBreakpointValue(hAxis, (ushort) BreakpointId, (int) value));
            }
    
            public Int32 ReadBuffer(Int16 BufferId) {
                Int32 tmp=0;
                CheckResult(PMDReadBuffer(hAxis, (ushort) BufferId, ref tmp));
                return tmp;
            }
    
            public Int16 ReadBuffer16(Int16 BufferId) {
                Int32 tmp=0;
                CheckResult(PMDReadBuffer(hAxis, (ushort) BufferId, ref tmp));
                return (short) tmp;
            }
    
            public void WriteBuffer(Int16 BufferId, Int32 value) {
                CheckResult(PMDWriteBuffer(hAxis, (ushort) BufferId, value));
            }
    
            public Int32 GetBufferLength(Int16 BufferId) {
                UInt32 tmp=0;
                CheckResult(PMDGetBufferLength(hAxis, (ushort) BufferId, ref tmp));
                return (int) tmp;
            }
    
            public void SetBufferLength(Int16 BufferId, Int32 value) {
                CheckResult(PMDSetBufferLength(hAxis, (ushort) BufferId, (uint) value));
            }
    
            public Int32 GetBufferReadIndex(Int16 BufferId) {
                UInt32 tmp=0;
                CheckResult(PMDGetBufferReadIndex(hAxis, (ushort) BufferId, ref tmp));
                return (int) tmp;
            }
    
            public void SetBufferReadIndex(Int16 BufferId, Int32 value) {
                CheckResult(PMDSetBufferReadIndex(hAxis, (ushort) BufferId, (uint) value));
            }
    
            public Int32 GetBufferStart(Int16 BufferId) {
                UInt32 tmp=0;
                CheckResult(PMDGetBufferStart(hAxis, (ushort) BufferId, ref tmp));
                return (int) tmp;
            }
    
            public void SetBufferStart(Int16 BufferId, Int32 value) {
                CheckResult(PMDSetBufferStart(hAxis, (ushort) BufferId, (uint) value));
            }
    
            public Int32 GetBufferWriteIndex(Int16 BufferId) {
                UInt32 tmp=0;
                CheckResult(PMDGetBufferWriteIndex(hAxis, (ushort) BufferId, ref tmp));
                return (int) tmp;
            }
    
            public void SetBufferWriteIndex(Int16 BufferId, Int32 value) {
                CheckResult(PMDSetBufferWriteIndex(hAxis, (ushort) BufferId, (uint) value));
            }
    
            public UInt16 BusVoltage {
                get {
                    UInt16 tmp=0;
                    CheckResult(PMDGetBusVoltage(hAxis, ref tmp));
                    return tmp;
                }
            }
    
            public UInt16 GetBusVoltageLimits(Int16 parameter) {
                UInt16 tmp=0;
                CheckResult(PMDGetBusVoltageLimits(hAxis, (ushort) parameter, ref tmp));
                return tmp;
            }
    
            public void SetBusVoltageLimits(Int16 parameter, UInt16 value) {
                CheckResult(PMDSetBusVoltageLimits(hAxis, (ushort) parameter, (ushort) value));
            }
    
            public void GetCANMode(ref byte NodeId, ref PMDCANBaud TransmissionRate) {
                byte tmp2=0;
                byte tempNodeId;
                tempNodeId = NodeId;
                //Byte tmp2;
                CheckResult(PMDGetCANMode(hAxis, ref tempNodeId, ref tmp2));
                TransmissionRate = ((PMDCANBaud)(tmp2));
            }
    
            public void SetCANMode(byte NodeId, PMDCANBaud TransmissionRate) {
                CheckResult(PMDSetCANMode(hAxis, NodeId, ((byte)(TransmissionRate))));
            }
    
            public PMDCaptureSource CaptureSource {
                get {
                    UInt16 tmp=0;
                    CheckResult(PMDGetCaptureSource(hAxis, ref tmp));
                    return ((PMDCaptureSource)(tmp));
                }
                set {
                    CheckResult(PMDSetCaptureSource(hAxis, ((UInt16)(value))));
                }
            }
    
            public Int32 CaptureValue {
                get {
                    Int32 tmp=0;
                    CheckResult(PMDGetCaptureValue(hAxis, ref tmp));
                    return tmp;
                }
            }
    
            public UInt32 Checksum {
                get {
                    UInt32 tmp=0;
                    CheckResult(PMDGetChecksum(hAxis, ref tmp));
                    return tmp;
                }
            }
    
            public Int32 CommandedAcceleration {
                get {
                    Int32 tmp=0;
                    CheckResult(PMDGetCommandedAcceleration(hAxis, ref tmp));
                    return tmp;
                }
            }
    
            public Int32 CommandedPosition {
                get {
                    Int32 tmp=0;
                    CheckResult(PMDGetCommandedPosition(hAxis, ref tmp));
                    return tmp;
                }
            }
    
            public Int32 CommandedVelocity {
                get {
                    Int32 tmp=0;
                    CheckResult(PMDGetCommandedVelocity(hAxis, ref tmp));
                    return tmp;
                }
            }
    
            public PMDCommutationMode CommutationMode {
                get {
                    UInt16 tmp=0;
                    CheckResult(PMDGetCommutationMode(hAxis, ref tmp));
                    return ((PMDCommutationMode)(tmp));
                }
                set {
                    CheckResult(PMDSetCommutationMode(hAxis, ((UInt16)(value))));
                }
            }
    
            public UInt16 GetCurrent(PMDCurrent parameter) {
                UInt16 tmp=0;
                CheckResult(PMDGetCurrent(hAxis, ((UInt16)(parameter)), ref tmp));
                return tmp;
            }
    
            public void SetCurrent(PMDCurrent parameter, UInt16 value) {
                CheckResult(PMDSetCurrent(hAxis, ((UInt16)(parameter)), value));
            }
    
            public PMDCurrentControlMode CurrentControlMode {
                get {
                    UInt16 tmp=0;
                    CheckResult(PMDGetCurrentControlMode(hAxis, ref tmp));
                    return ((PMDCurrentControlMode)(tmp));
                }
                set {
                    CheckResult(PMDSetCurrentControlMode(hAxis, ((ushort)(value))));
                }
            }
    
  
            public UInt16 GetCurrentFoldback(Int16 parameter) {
                UInt16 tmp=0;
                CheckResult(PMDGetCurrentFoldback(hAxis, (ushort) parameter, ref tmp));
                return tmp;
            }
    
            public void SetCurrentFoldback(Int16 parameter, UInt16 value) {
                CheckResult(PMDSetCurrentFoldback(hAxis, (ushort) parameter, value));
            }
    
            public UInt16 GetCurrentLoop(PMDCurrentLoopNumber phase, PMDCurrentLoopParameter parameter) {
                UInt16 tmp=0;
                CheckResult(PMDGetCurrentLoop(hAxis, ((byte)(phase)), ((byte)(parameter)), ref tmp));
                return tmp;
            }
    
            public void SetCurrentLoop(PMDCurrentLoopNumber phase, PMDCurrentLoopParameter parameter, UInt16 value) {
                CheckResult(PMDSetCurrentLoop(hAxis, ((byte)(phase)), ((byte)(parameter)), value));
            }
    
            public Int32 GetCurrentLoopValue(PMDCurrentLoopNumber phase, PMDCurrentLoopValueNode node) {
                Int32 tmp=0;
                CheckResult(PMDGetCurrentLoopValue(hAxis, ((byte)(phase)), ((byte)(node)), ref tmp));
                return tmp;
            }
    
            public UInt32 Deceleration {
                get {
                    UInt32 tmp=0;
                    CheckResult(PMDGetDeceleration(hAxis, ref tmp));
                    return tmp;
                }
                set {
                    CheckResult(PMDSetDeceleration(hAxis, value));
                }
            }
    
            public UInt32 GetMPDefault(Int16 variable) {
                UInt32 tmp=0;
                CheckResult(PMDGetDefault(hAxis, (ushort) variable, ref tmp));
                return tmp;
            }
    
            public void SetMPDefault(Int16 variable, UInt32 value) {
                CheckResult(PMDSetDefault(hAxis, (ushort) variable, value));
            }
    
            public UInt16 DriveFaultStatus {
                get {
                    UInt16 tmp=0;
                    CheckResult(PMDGetDriveFaultStatus(hAxis, ref tmp));
                    return tmp;
                }
            }
    
            public void ClearDriveFaultStatus() {
                CheckResult(PMDClearDriveFaultStatus(hAxis));
            }
    
            public void GetDriveCommandMode(ref byte transport, ref byte format) {
                byte temptransport;
                temptransport = transport;
                byte tempformat;
                tempformat = format;
                CheckResult(PMDGetDriveCommandMode(hAxis, ref temptransport, ref tempformat));
            }
    
            public void SetDriveCommandMode(byte transport, byte format) {
                CheckResult(PMDSetDriveCommandMode(hAxis, transport, format));
            }
    
            public UInt16 GetDriveFaultParameter(PMDDriveFaultParameter parameter) {
                UInt16 tmp=0;
                CheckResult(PMDGetDriveFaultParameter(hAxis, ((UInt16)(parameter)), ref tmp));
                return tmp;
            }
    
            public void SetDriveFaultParameter(PMDDriveFaultParameter parameter, UInt16 value) {
                CheckResult(PMDSetDriveFaultParameter(hAxis, ((UInt16)(parameter)), value));
            }
    
            public void DriveNVRAM(PMDNVRAM parameter, UInt16 value) {
                CheckResult(PMDDriveNVRAM(hAxis, ((UInt16)(parameter)), value));
            }
    
            public UInt16 GetDrivePWM(PMDDrivePWM parameter) {
                UInt16 tmp=0;
                CheckResult(PMDGetDrivePWM(hAxis, ((UInt16)(parameter)), ref tmp));
                return tmp;
            }
    
            public void SetDrivePWM(PMDDrivePWM parameter, UInt16 value) {
                CheckResult(PMDSetDrivePWM(hAxis, ((UInt16)(parameter)), value));
            }
    
            public void ClearInterrupt() {
                CheckResult(PMDClearInterrupt(hAxis));
            }
    
            public void ClearPositionError() {
                CheckResult(PMDClearPositionError(hAxis));
            }
    
            public UInt16 DriveStatus {
                get {
                    UInt16 tmp=0;
                    CheckResult(PMDGetDriveStatus(hAxis, ref tmp));
                    return tmp;
                }
            }
    
            public UInt16 EncoderModulus {
                get {
                    UInt16 tmp=0;
                    CheckResult(PMDGetEncoderModulus(hAxis, ref tmp));
                    return tmp;
                }
                set {
                    CheckResult(PMDSetEncoderModulus(hAxis, value));
                }
            }
    
            public PMDEncoderSource EncoderSource {
                get {
                    UInt16 tmp=0;
                    CheckResult(PMDGetEncoderSource(hAxis, ref tmp));
                    return ((PMDEncoderSource)(tmp));
                }
                set {
                    CheckResult(PMDSetEncoderSource(hAxis, ((UInt16)(value))));
                }
            }
    
            public void GetEncoderToStepRatio(ref UInt16 counts, ref UInt16 steps) {
                  CheckResult(PMDGetEncoderToStepRatio(hAxis, ref counts, ref steps));
             
            }
    
            public void SetEncoderToStepRatio(UInt16 counts, UInt16 steps) {
                CheckResult(PMDSetEncoderToStepRatio(hAxis, counts, steps));
            }
    
            public PMDEventAction GetEventAction(PMDEventActionEvent ActionEvent) {
                UInt16 tmp=0;
                CheckResult(PMDGetEventAction(hAxis, ((UInt16)(ActionEvent)), ref tmp));
                return ((PMDEventAction)(tmp));
            }
    
            public void SetEventAction(PMDEventActionEvent ActionEvent, PMDEventAction Action) {
                CheckResult(PMDSetEventAction(hAxis, ((UInt16)(ActionEvent)), ((UInt16)(Action))));
            }
    
            public UInt16 EventStatus {
                get {
                    UInt16 tmp=0;
                    CheckResult(PMDGetEventStatus(hAxis,ref tmp));
                    return tmp;
                }
            }
    
            public void ResetEventStatus(UInt16 StatusMask) {
                CheckResult(PMDResetEventStatus(hAxis, StatusMask));
            }
    
            public UInt32 GetFeedbackParameter(PMDFeedbackParameter parameter) {
                UInt32 tmp=0;
                CheckResult(PMDGetFeedbackParameter(hAxis, ((UInt16)(parameter)), ref tmp));
                return tmp;
            }
    
            public void SetFeedbackParameter(PMDFeedbackParameter parameter, UInt32 value) {
                CheckResult(PMDSetFeedbackParameter(hAxis, ((UInt16)(parameter)), value));
            }
    
            public UInt16 GetFOC(PMDFOC_LoopNumber ControlLoop, PMDFOCLoopParameter parameter) {
                UInt16 tmp=0;
                CheckResult(PMDGetFOC(hAxis, ((byte)(ControlLoop)), ((byte)(parameter)), ref tmp));
                return tmp;
            }
    
            public void SetFOC(PMDFOC_LoopNumber ControlLoop, PMDFOCLoopParameter parameter, UInt16 value) {
                CheckResult(PMDSetFOC(hAxis, ((byte)(ControlLoop)), ((byte)(parameter)), value));
            }
    
            public Int32 GetFOCValue(PMDFOC_LoopNumber ControlLoop, PMDFOCValueNode node) {
                Int32 tmp=0;
                CheckResult(PMDGetFOCValue(hAxis, ((byte)(ControlLoop)), ((byte)(node)), ref tmp));
                return tmp;
            }
    
            public UInt16 FaultOutMask {
                get {
                    UInt16 tmp=0;
                    CheckResult(PMDGetFaultOutMask(hAxis, ref tmp));
                    return tmp;
                }
                set {
                    CheckResult(PMDSetFaultOutMask(hAxis, value));
                }
            }
    
            public void GetGearMaster(ref PMDAxisNumber MasterAxis, ref PMDGearMasterSource source) {
                UInt16 tmp1=0;
                UInt16 tmp2=0;
                CheckResult(PMDGetGearMaster(hAxis, ref tmp1, ref tmp2));
                MasterAxis = ((PMDAxisNumber)(tmp1));
                source = ((PMDGearMasterSource)(tmp2));
            }
    
            public void SetGearMaster(PMDAxisNumber MasterAxis, PMDGearMasterSource source) {
                CheckResult(PMDSetGearMaster(hAxis, ((UInt16)(MasterAxis)), ((UInt16)(source))));
            }
    
            public Int32 GearRatio {
                get {
                    Int32 tmp=0;
                    CheckResult(PMDGetGearRatio(hAxis, ref tmp));
                    return tmp;
                }
                set {
                    CheckResult(PMDSetGearRatio(hAxis, value));
                }
            }
    
            public UInt16 GetHoldingCurrent(PMDHoldingCurrent parameter) {
                UInt16 tmp=0;
                CheckResult(PMDGetHoldingCurrent(hAxis, ((UInt16)(parameter)), ref tmp));
                return tmp;
            }
    
            public void SetHoldingCurrent(PMDHoldingCurrent parameter, UInt16 value) {
                CheckResult(PMDSetHoldingCurrent(hAxis, ((UInt16)(parameter)), value));
            }
    
            public void InitializePhase() {
                CheckResult(PMDInitializePhase(hAxis));
            }
    
            public PMDInstructionError InstructionError {
                get {
                    UInt16 tmp=0;
                    CheckResult(PMDGetInstructionError(hAxis, ref tmp));
                    return ((PMDInstructionError)(tmp));
                }
            }
    
            public UInt16 InterruptAxis {
                get {
                    UInt16 tmp=0;
                    CheckResult(PMDGetInterruptAxis(hAxis, ref tmp));
                    return tmp;
                }
            }
    
            public UInt16 InterruptMask {
                get {
                    UInt16 tmp=0;
                    CheckResult(PMDGetInterruptMask(hAxis, ref tmp));
                    return tmp;
                }
                set {
                    CheckResult(PMDSetInterruptMask(hAxis, value));
                }
            }
    
            public UInt16 ReadIO(UInt16 address) {
                UInt16 tmp=0;
                CheckResult(PMDReadIO(hAxis, address, ref tmp));
                return tmp;
            }
    
            public void WriteIO(UInt16 address, UInt16 value) {
                CheckResult(PMDWriteIO(hAxis, address, value));
            }
    
            public UInt32 Jerk {
                get {
                    UInt32 tmp=0;
                    CheckResult(PMDGetJerk(hAxis, ref tmp));
                    return tmp;
                }
                set {
                    CheckResult(PMDSetJerk(hAxis, value));
                }
            }
    
            public PMDMotionCompleteMode MotionCompleteMode {
                get {
                    UInt16 tmp=0;
                    CheckResult(PMDGetMotionCompleteMode(hAxis, ref tmp));
                    return ((PMDMotionCompleteMode)(tmp));
                }
                set {
                    CheckResult(PMDSetMotionCompleteMode(hAxis, ((UInt16)(value))));
                }
            }
    
            public Int16 MotorBias {
                get {
                    Int16 tmp=0;
                    CheckResult(PMDGetMotorBias(hAxis, ref tmp));
                    return tmp;
                }
                set {
                    CheckResult(PMDSetMotorBias(hAxis, value));
                }
            }
    
            public Int16 MotorCommand {
                get {
                    Int16 tmp=0;
                    CheckResult(PMDGetMotorCommand(hAxis, ref tmp));
                    return tmp;
                }
                set {
                    CheckResult(PMDSetMotorCommand(hAxis, value));
                }
            }
    
            public UInt16 MotorLimit {
                get {
                    UInt16 tmp=0;
                    CheckResult(PMDGetMotorLimit(hAxis, ref tmp));
                    return tmp;
                }
                set {
                    CheckResult(PMDSetMotorLimit(hAxis, value));
                }
            }
    
            public PMDMotorType MotorType {
                get {
                    UInt16 tmp=0;
                    CheckResult(PMDGetMotorType(hAxis, ref tmp));
                    return ((PMDMotorType)(tmp));
                }
                set {
                    CheckResult(PMDSetMotorType(hAxis, ((UInt16)(value))));
                }
            }
    
            public void MultiUpdate(UInt16 AxisMask) {
                CheckResult(PMDMultiUpdate(hAxis, AxisMask));
            }
    
            public void NoOperation() {
                CheckResult(PMDNoOperation(hAxis));
            }
    
            public UInt16 OperatingMode {
                get {
                    UInt16 tmp=0;
                    CheckResult(PMDGetOperatingMode(hAxis, ref tmp));
                    return tmp;
                }
                set {
                    CheckResult(PMDSetOperatingMode(hAxis, value));
                }
            }
    
            public PMDMotorOutputMode OutputMode {
                get {
                    UInt16 tmp=0;
                    CheckResult(PMDGetOutputMode(hAxis, ref tmp));
                    return ((PMDMotorOutputMode)(tmp));
                }
                set {
                    CheckResult(PMDSetOutputMode(hAxis, ((UInt16)(value))));
                }
            }
    
            public UInt16 OverTemperatureLimit {
                get {
                    UInt16 tmp=0;
                    CheckResult(PMDGetOverTemperatureLimit(hAxis, ref tmp));
                    return tmp;
                }
                set {
                    CheckResult(PMDSetOverTemperatureLimit(hAxis, value));
                }
            }
    
            public UInt16 PWMFrequency {
                get {
                    UInt16 tmp=0;
                    CheckResult(PMDGetPWMFrequency(hAxis, ref tmp));
                    return tmp;
                }
                set {
                    CheckResult(PMDSetPWMFrequency(hAxis, value));
                }
            }
    
            public UInt16 PhaseAngle {
                get {
                    UInt16 tmp=0;
                    CheckResult(PMDGetPhaseAngle(hAxis, ref tmp));
                    return tmp;
                }
                set {
                    CheckResult(PMDSetPhaseAngle(hAxis, value));
                }
            }
    
            public Int16 GetPhaseCommand(PMDPhaseCommand phase) {
                Int16 tmp=0;
                CheckResult(PMDGetPhaseCommand(hAxis, ((UInt16)(phase)), ref tmp));
                return tmp;
            }
    
            public PMDPhaseCorrectionMode PhaseCorrectionMode {
                get {
                    UInt16 tmp=0;
                    CheckResult(PMDGetPhaseCorrectionMode(hAxis, ref tmp));
                    return ((PMDPhaseCorrectionMode)(tmp));
                }
                set {
                    CheckResult(PMDSetPhaseCorrectionMode(hAxis, ((UInt16)(value))));
                }
            }
    
            public UInt16 PhaseCounts {
                get {
                    UInt16 tmp=0;
                    CheckResult(PMDGetPhaseCounts(hAxis, ref tmp));
                    return tmp;
                }
                set {
                    CheckResult(PMDSetPhaseCounts(hAxis, value));
                }
            }
    
            public PMDPhaseInitializeMode PhaseInitializeMode {
                get {
                    UInt16 tmp=0;
                    CheckResult(PMDGetPhaseInitializeMode(hAxis, ref tmp));
                    return ((PMDPhaseInitializeMode)(tmp));
                }
                set {
                    CheckResult(PMDSetPhaseInitializeMode(hAxis, (ushort) value));
                }
            }
    
            public UInt16 PhaseInitializeTime {
                get {
                    UInt16 tmp=0;
                    CheckResult(PMDGetPhaseInitializeTime(hAxis, ref tmp));
                    return tmp;
                }
                set {
                    CheckResult(PMDSetPhaseInitializeTime(hAxis, value));
                }
            }
    
            public UInt16 PhaseOffset {
                get {
                    UInt16 tmp=0;
                    CheckResult(PMDGetPhaseOffset(hAxis, ref tmp));
                    return tmp;
                }
                set {
                    CheckResult(PMDSetPhaseOffset(hAxis, value));
                }
            }
    
            public PMDPhasePrescaleMode PhasePrescale {
                get {
                    UInt16 tmp=0;
                    CheckResult(PMDGetPhasePrescale(hAxis, ref tmp));
                    return ((PMDPhasePrescaleMode)(tmp));
                }
                set {
                    CheckResult(PMDSetPhasePrescale(hAxis, ((UInt16)(value))));
                }
            }
    
            public Int32 Position {
                get {
                    Int32 tmp=0;
                    CheckResult(PMDGetPosition(hAxis, ref tmp));
                    return tmp;
                }
                set {
                    CheckResult(PMDSetPosition(hAxis, value));
                }
            }
    
            public Int32 PositionError {
                get {
                    Int32 tmp=0;
                    CheckResult(PMDGetPositionError(hAxis, ref tmp));
                    return tmp;
                }
            }
    
            public UInt32 PositionErrorLimit {
                get {
                    UInt32 tmp=0;
                    CheckResult(PMDGetPositionErrorLimit(hAxis, ref tmp));
                    return tmp;
                }
                set {
                    CheckResult(PMDSetPositionErrorLimit(hAxis, value));
                }
            }
    
            public Int32 GetPositionLoop(PMDPositionLoop parameter) {
                Int32 tmp=0;
                CheckResult(PMDGetPositionLoop(hAxis, ((UInt16)(parameter)), ref tmp));
                return tmp;
            }
    
            public void SetPositionLoop(PMDPositionLoop parameter, Int32 value) {
                CheckResult(PMDSetPositionLoop(hAxis, ((UInt16)(parameter)), value));
            }
    
            public Int32 GetPositionLoopValue(PMDPositionLoopValueNode parameter) {
                Int32 tmp=0;
                CheckResult(PMDGetPositionLoopValue(hAxis, ((UInt16)(parameter)), ref tmp));
                return tmp;
            }
    
            public PMDProfileMode ProfileMode {
                get {
                    UInt16 tmp=0;
                    CheckResult(PMDGetProfileMode(hAxis, ref tmp));
                    return ((PMDProfileMode)(tmp));
                }
                set {
                    CheckResult(PMDSetProfileMode(hAxis, ((ushort)(value))));
                }
            }
    
            public void RestoreOperatingMode() {
                CheckResult(PMDRestoreOperatingMode(hAxis));
            }
    
            public PMDSPIMode SPIMode {
                get {
                    UInt16 tmp=0;
                    CheckResult(PMDGetSPIMode(hAxis, ref tmp));
                    return ((PMDSPIMode)(tmp));
                }
                set {
                    CheckResult(PMDSetSPIMode(hAxis, ((UInt16)(value))));
                }
            }
    
            public UInt32 SampleTime {
                get {
                    UInt32 tmp=0;
                    CheckResult(PMDGetSampleTime(hAxis, ref tmp));
                    return tmp;
                }
                set {
                    CheckResult(PMDSetSampleTime(hAxis, value));
                }
            }
    
            public void GetSerialPortMode(ref PMDSerialBaud baud, ref PMDSerialParity parity, ref PMDSerialStopBits StopBits, ref PMDSerialProtocol protocol, ref byte MultiDropId) {
                byte tmp1=0;
                byte tmp2=0;
                byte tmp3=0;
                byte tmp4=0;
                CheckResult(PMDGetSerialPortMode(hAxis, ref tmp1, ref tmp2, ref tmp3, ref tmp4,ref MultiDropId));
                baud = ((PMDSerialBaud)(tmp1));
                parity = ((PMDSerialParity)(tmp2));
                StopBits = ((PMDSerialStopBits)(tmp3));
                protocol = ((PMDSerialProtocol)(tmp4));
            }
    
            public void SetSerialPortMode(PMDSerialBaud baud, PMDSerialParity parity, PMDSerialStopBits StopBits, PMDSerialProtocol protocol, byte MultiDropId) {
                CheckResult(PMDSetSerialPortMode(hAxis, ((byte)(baud)), ((byte)(parity)), ((byte)(StopBits)), ((byte)(protocol)), MultiDropId));
            }
    
            public UInt16 SettleTime {
                get {
                    UInt16 tmp=0;
                    CheckResult(PMDGetSettleTime(hAxis, ref tmp));
                    return tmp;
                }
                set {
                    CheckResult(PMDSetSettleTime(hAxis, value));
                }
            }
    
            public UInt16 SettleWindow {
                get {
                    UInt16 tmp=0;
                    CheckResult(PMDGetSettleWindow(hAxis, ref tmp));
                    return tmp;
                }
                set {
                    CheckResult(PMDSetSettleWindow(hAxis, value));
                }
            }
    
            public UInt16 SignalSense {
                get {
                    UInt16 tmp=0;
                    CheckResult(PMDGetSignalSense(hAxis, ref tmp));
                    return tmp;
                }
                set {
                    CheckResult(PMDSetSignalSense(hAxis, value));
                }
            }
    
            public UInt16 SignalStatus {
                get {
                    UInt16 tmp=0;
                    CheckResult(PMDGetSignalStatus(hAxis, ref tmp));
                    return tmp;
                }
            }
    
            public UInt32 StartVelocity {
                get {
                    UInt32 tmp=0;
                    CheckResult(PMDGetStartVelocity(hAxis, ref tmp));
                    return tmp;
                }
                set {
                    CheckResult(PMDSetStartVelocity(hAxis, value));
                }
            }
    
            public UInt16 StepRange {
                get {
                    UInt16 tmp=0;
                    CheckResult(PMDGetStepRange(hAxis, ref tmp));
                    return tmp;
                }
                set {
                    CheckResult(PMDSetStepRange(hAxis, value));
                }
            }
    
            public PMDStopMode StopMode {
                get {
                    UInt16 tmp=0;
                    CheckResult(PMDGetStopMode(hAxis, ref tmp));
                    return ((PMDStopMode)(tmp));
                }
                set {
                    CheckResult(PMDSetStopMode(hAxis, ((UInt16)(value))));
                }
            }
    
            public PMDSynchronizationMode SynchronizationMode {
                get {
                    UInt16 tmp=0;
                    CheckResult(PMDGetSynchronizationMode(hAxis, ref tmp));
                    return ((PMDSynchronizationMode)(tmp));
                }
                set {
                    CheckResult(PMDSetSynchronizationMode(hAxis, ((UInt16)(value))));
                }
            }
    
            public UInt16 Temperature {
                get {
                    UInt16 tmp=0;
                    CheckResult(PMDGetTemperature(hAxis, ref tmp));
                    return tmp;
                }
            }
    
            public UInt32 Time {
                get {
                    UInt32 tmp=0;
                    CheckResult(PMDGetTime(hAxis, ref tmp));
                    return tmp;
                }
            }
    
            public UInt32 TraceCount {
                get {
                    UInt32 tmp=0;
                    CheckResult(PMDGetTraceCount(hAxis, ref tmp));
                    return tmp;
                }
            }
    
            public PMDTraceMode TraceMode {
                get {
                    UInt16 tmp=0;
                    CheckResult(PMDGetTraceMode(hAxis, ref tmp));
                    return ((PMDTraceMode)(tmp));
                }
                set {
                    CheckResult(PMDSetTraceMode(hAxis, ((UInt16)(value))));
                }
            }
    
            public UInt16 TracePeriod {
                get {
                    UInt16 tmp=0;
                    CheckResult(PMDGetTracePeriod(hAxis, ref tmp));
                    return tmp;
                }
                set {
                    CheckResult(PMDSetTracePeriod(hAxis, value));
                }
            }
    
            public void GetTraceStart(ref PMDAxisNumber triggerAxis, ref PMDTraceCondition condition, ref byte bit, ref PMDTraceTriggerState state) {
                UInt16 tmp1=0;
                byte tmp2=0;
                byte tmp4=0;
                CheckResult(PMDGetTraceStart(hAxis, ref tmp1, ref tmp2, ref bit, ref tmp4));
                triggerAxis = ((PMDAxisNumber)(tmp1));
                condition = ((PMDTraceCondition)(tmp2));
                state = ((PMDTraceTriggerState)(tmp4));
            }
    
            public void SetTraceStart(PMDAxisNumber triggerAxis, PMDTraceCondition condition, byte bit, PMDTraceTriggerState state) {
                CheckResult(PMDSetTraceStart(hAxis, ((UInt16)(triggerAxis)), ((byte)(condition)), bit, ((byte)(state))));
            }
    
            public UInt16 TraceStatus {
                get {
                    UInt16 tmp=0;
                    CheckResult(PMDGetTraceStatus(hAxis, ref tmp));
                    return tmp;
                }
            }
    
            public void GetTraceStop(ref PMDAxisNumber triggerAxis, ref PMDTraceCondition condition, ref byte bit, ref PMDTraceTriggerState state) {
                UInt16 tmp1=0;
                byte tmp2=0;
                byte tmp4=0;
                CheckResult(PMDGetTraceStop(hAxis, ref tmp1, ref tmp2, ref bit, ref tmp4));
                triggerAxis = ((PMDAxisNumber)(tmp1));
                condition = ((PMDTraceCondition)(tmp2));
                state = ((PMDTraceTriggerState)(tmp4));
            }
    
            public void SetTraceStop(PMDAxisNumber triggerAxis, PMDTraceCondition condition, byte bit, PMDTraceTriggerState state) {
                CheckResult(PMDSetTraceStop(hAxis, ((UInt16)(triggerAxis)), ((byte)(condition)), bit, ((byte)(state))));
            }
    
            public void GetTraceVariable(PMDTraceVariableNumber VariableNumber, ref PMDAxisNumber TraceAxis, ref PMDTraceVariable variable) {
                UInt16 tmp3=0;
                byte tmp4=0;
                CheckResult(PMDGetTraceVariable(hAxis, ((UInt16)(VariableNumber)), ref tmp3, ref tmp4));
                TraceAxis = ((PMDAxisNumber)(tmp3));
                variable = ((PMDTraceVariable)(tmp4));
            }
    
            public void SetTraceVariable(PMDTraceVariableNumber VariableNumber, PMDAxisNumber TraceAxis, PMDTraceVariable variable) {
                CheckResult(PMDSetTraceVariable(hAxis, ((UInt16)(VariableNumber)), ((UInt16)(TraceAxis)), ((byte)(variable))));
            }
    
            public UInt16 TrackingWindow {
                get {
                    UInt16 tmp=0;
                    CheckResult(PMDGetTrackingWindow(hAxis, ref tmp));
                    return tmp;
                }
                set {
                    CheckResult(PMDSetTrackingWindow(hAxis, value));
                }
            }
    
            public void Update() {
                CheckResult(PMDUpdate(hAxis));
            }
    
            public UInt16 UpdateMask {
                get {
                    UInt16 tmp=0;
                    CheckResult(PMDGetUpdateMask(hAxis, ref tmp));
                    return tmp;
                }
                set {
                    CheckResult(PMDSetUpdateMask(hAxis, value));
                }
            }
    
            public Int32 Velocity {
                get {
                    Int32 tmp=0;
                    CheckResult(PMDGetVelocity(hAxis, ref tmp));
                    return tmp;
                }
                set {
                    CheckResult(PMDSetVelocity(hAxis, value));
                }
            }
    
            public void GetVersion(ref UInt16 family, ref PMDMotorTypeVersion MotorType, ref UInt16 NumberAxes, ref UInt16 special_and_chip_count, ref UInt16 custom, ref UInt16 major, ref UInt16 minor) {
                UInt16 mtype=0;
                CheckResult(PMDGetVersion(hAxis, ref family, ref mtype, ref NumberAxes, ref special_and_chip_count, ref custom, ref major, ref minor));
                MotorType = ((PMDMotorTypeVersion)(mtype));
            }
        }
        // Axis

        
        public class PMDMemory {
    
            // *** Private data and utility functions ***
            internal IntPtr hMem;
    
            internal PMDDataSize DataSize;
    
            internal PMDresult status;
    
            internal void CheckResult(PMDresult status) {
                this.status = status;
                if (!(status == PMDresult.NOERROR)) {
                    Exception e = new Exception(("ERROR: PMDMemory " + status));
                    e.Data.Add("PMDresult", status);
                    throw e;
                }
            }
    
            // *** Public Methods ***
            public PMDMemory(PMDDevice device, PMDDataSize DataSize, PMDMemoryType MemType) {
                hMem = PMDMemoryAlloc();
                if ((hMem == null)) {
                    throw new Exception("ERROR: PMD library: could not allocate memory object");
                }
                this.DataSize = DataSize;
                CheckResult(PMDMemoryOpen(hMem, device.hDev, DataSize, MemType));
            }
    
 
            public void Close() {
                if (!(hMem == null))
                {
                    PMDMemoryClose(hMem);
                    PMDMemoryFree(hMem);
                    hMem = (IntPtr)0;
                }
               // this.Close();
                //this.Finalize();
            }
    
            public PMDresult LastError {
                get {
                    return status;
                }
            }
    
            public void Read(ref UInt32[] data, UInt32 offset, UInt32 length) {
                CheckResult(PMDMemoryRead(hMem, ref data[0], offset, length));
            }
    
            public void Write(ref UInt32[] data, UInt32 offset, UInt32 length) {
                CheckResult(PMDMemoryWrite(hMem, ref data[0], offset, length));
            }
        }
// PMDMemory

    }
}
