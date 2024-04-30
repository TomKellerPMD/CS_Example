//Limits of Liability - Under no circumstances shall Performance Motion Devices, Inc. or its affiliates, partners, or suppliers be liable for any indirect
// incidental, consequential, special or exemplary damages arising out or in connection with the use this example,
// whether or not the damages were foreseeable and whether or not Performance Motion Devices, Inc. was advised of the possibility of such damages.
// Determining the suitability of this example is the responsibility of the user and subsequent usage is at their sole risk and discretion.
// There are no licensing restrictions associated with this example.


///  CS_Example TLK 11/09/2021 
/// 
///   This example demonstrates connencting to a PMD controller.  Status windows are created to display the commanded and actual position.
///   The Setup Axis function is used to configure the Axis.  These values should come from the Pro-Motion GUI.
///   The user then has the optoin of commanding a move.   
///    
///   Additionaly optional functionality includes homing, GPIO, and memory access.
///  .  





using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using PMDLibrary;
using System.Runtime.Remoting.Messaging;
using static PMDLibrary.PMD;

namespace CS_Example
{
    public partial class Form1 : Form
    {
        static PMD.PMDPeripheral CommHandle;
        static PMD.PMDDevice devMC;
        public static volatile PMD.PMDAxis Axis1, Axis2;
        public bool timerstop = false;
        static NVRAM nvramobj;
        static IO IOobject;
        static PMDHoming homeobject;
        delegate void SetCmdPosTextCallback(string text);
        delegate void SetActPosTextCallback(string text);
        delegate void SetEventLabelTextCallback(string text);

        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private int numberToCompute = 0;
        String ipaddress = "192.168.2.2";
        public Form1()
        {
            InitializeComponent();
                                    
            try
            {
                PMDInterfaceType Interface = PMDInterfaceType.CAN;

                if (Interface == PMD.PMDInterfaceType.TCP)
                {
                    CommHandle = new PMD.PMDPeripheralTCP(System.Net.IPAddress.Parse(ipaddress), 40100, 1000);

                }
                else if (Interface == PMD.PMDInterfaceType.Serial)
                {
                    CommHandle = new PMD.PMDPeripheralSerial(2, 57600, PMD.PMDSerialParity.None, PMD.PMDSerialStopBits.SerialStopBits1);


                }
                else if (Interface == PMD.PMDInterfaceType.CAN)
                {
                    uint nodeid = 0;
                    CommHandle = new PMD.PMDPeripheralCANFD(PMDCANPort.CANPort1, (uint)PMDCANBaud.CANBaud20000, 0x600 + nodeid, 0x580 + nodeid, 0); 
                }

                //devMC = new PMD.PMDDevice(CommHandle, PMD.PMDDeviceType.ResourceProtocol);   // For N-series and Prodigy 
                devMC = new PMD.PMDDevice(CommHandle, PMD.PMDDeviceType.MotionProcessor);       // For all Magellan and Juno ICs

                Axis1 = new PMD.PMDAxis(devMC, PMD.PMDAxisNumber.Axis1);
                Axis2 = new PMD.PMDAxis(devMC, PMD.PMDAxisNumber.Axis2);

                ushort gvfamily = 0, gvnaxes = 0, gvnchips = 0, gvcustom = 0, gvmajor = 0, gvminor = 0;
                PMD.PMDMotorTypeVersion mtype = 0;
                                
                Axis1.GetVersion(ref gvfamily, ref mtype, ref gvnaxes, ref gvnchips, ref gvcustom, ref gvmajor, ref gvminor);

                PMDSetup mtrsetup = new PMDSetup();
                //      mtrsetup.DoStepAtlasSetup(Axis2);
                //     mtrsetup.DoBLDCAtlasSetup(Axis1);
                //mtrsetup.DoNSeriesBLDCSetup(Axis1);
                mtrsetup.DoNSeriesStepSetup(Axis1);
                int test=Axis1.CommandedPosition;


           //     homeobject = new PMDHoming();
            //    homeobject.HomeSwitch(Axis1);
                
                
                StateObjClass StateObj = new StateObjClass();
                StateObj.TimerCanceled = false;
                StateObj.SomeValue = 1;
                System.Threading.TimerCallback TimerDelegate = new System.Threading.TimerCallback(TimerTask);

                // Create a timer that calls a procedure every 500 milliseconds. 
                // Note: There is no Start method; the timer starts running as soon as  
                // the instance is created.
                System.Threading.Timer TimerItem = new System.Threading.Timer(TimerDelegate, StateObj, 500, 500);
                // Save a reference for Dispose.
                StateObj.TimerReference = TimerItem;

                // Used to access IO space
                // IO IOobject = new IO();
                // IOobject.ReadDigitalInputs(devMC);
                // IOobject.ReadAnalagInputs(devMC);

             
                // Used to access Memory space
                // nvramobj = new NVRAM(devMC);
                // nvramobj.Write();
                // nvramobj.Read();

               
                
                           
               // This is an optional general purpose back ground task 
               //this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
               // InitializeBackgoundWorker();

            
           }

            catch (Exception e)
            {
                MessageBox.Show(e.Message);

            }


        }

        private void SetCmdPosText(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.CmdPosBox.InvokeRequired)
            {
                SetCmdPosTextCallback d = new SetCmdPosTextCallback(SetCmdPosText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.CmdPosBox.Text = text;
            }
        }

        private void SetActPosText(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.CmdPosBox.InvokeRequired)
            {
                SetActPosTextCallback d = new SetActPosTextCallback(SetActPosText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.ActPosBox.Text = text;
            }
        }

        private void SetEventLabelText(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.EventLabel.InvokeRequired)
            {
                SetEventLabelTextCallback f = new SetEventLabelTextCallback(SetEventLabelText);
                this.Invoke(f, new object[] { text });
            }
            else
            {
                this.EventLabel.Text = text;
            }
        }



        private class StateObjClass
        {
            // Used to hold parameters for calls to TimerTask. 
            public int SomeValue;
            public System.Threading.Timer TimerReference;
            public bool TimerCanceled;
        }

        public void RunTimer()
        {

            StateObjClass StateObj = new StateObjClass();
            StateObj.TimerCanceled = false;
            StateObj.SomeValue = 1;
            System.Threading.TimerCallback TimerDelegate = new System.Threading.TimerCallback(TimerTask);

            // Create a timer that calls a procedure every 2 seconds. 
            // Note: There is no Start method; the timer starts running as soon as  
            // the instance is created.
            System.Threading.Timer TimerItem = new System.Threading.Timer(TimerDelegate, StateObj, 500, 500);

            // Save a reference for Dispose.
            StateObj.TimerReference = TimerItem;
                      
        }

        private void TimerTask(object StateObj)
        {
            StateObjClass State = (StateObjClass)StateObj;
            Int32 CmdPos = 0,ActPos=0; //, tester = 0;
            UInt16 AxisEvent = 0;

            // Use the interlocked class to increment the counter variable.
            //      System.Threading.Interlocked.Increment(ref State.SomeValue);
            System.Diagnostics.Debug.WriteLine("Launched new thread  " + DateTime.Now.ToString());
           // if (State.TimerCanceled)
            // Dispose Requested.
            if (timerstop)
            {
                State.TimerReference.Dispose();
                System.Diagnostics.Debug.WriteLine("Done  " + DateTime.Now.ToString());
            }


            // Update Commanded Position Text Box
            CmdPos = Axis1.CommandedPosition;
            this.SetCmdPosText(CmdPos.ToString());

            // Update Commanded Position Text Box
            ActPos = Axis1.ActualPosition;
            this.SetActPosText(ActPos.ToString());


            if (!EventInt.Checked)
            // Check Events and update Event Label is neccessary
            {
                AxisEvent = Axis1.EventStatus;
                if (AxisEvent != 0) this.SetEventLabelText(ProcessEvent(AxisEvent));
            }
        }

        private string ProcessEvent(UInt16 Aevent)
        {
            string EventString = "";
            // check for MotionError
            if ((Aevent & 0x0010) != 0)
                EventString = "Motion Error";
            else if ((Aevent & 0x0020) != 0)
                EventString = "Postive Limit Switch Reaced";
            else if ((Aevent & 0x0030) != 0)
                EventString = "Negative Limit Switch Reached";
            else if ((Aevent & 0x001) != 0)
                EventString = "Motion Compelete";

            return EventString;
        }

        private void StopButton_Click(object sender, EventArgs e)
        {

            Axis1.StopMode = PMD.PMDStopMode.Abrupt;
            Axis1.Update();

        }

                   
        private void HandleMagellanEvent(object sender, PMDEventArgs a)
        {
          
            String EventString;
           
            PMD.PMDAxis axis = new PMD.PMDAxis(devMC, ((PMD.PMDAxisNumber)a.EventAxis));
            if ((a.EventData & (int)PMD.PMDEventStatus.MotionError) == (int)PMD.PMDEventStatus.MotionError)
            {
                axis.ClearPositionError();
                axis.Update();
            } 
            
            
            // perform any event specific tasks to clear the event before calling ResetEventStatus.
            // such as certain motion error event actions that do not clear the position error 
            // so we must do it here to prevent repeat notifications
            EventString = ProcessEvent((ushort)a.EventData);
            MessageBox.Show(EventString + " on axis " + a.EventAxis);

       //     if ((a.EventData & (int)PMD.PMDEventMask.MotionErrorMask) == (int)PMD.PMDEventMask.MotionErrorMask)
        //    {
        //        axis.ClearPositionError();
        //        axis.Update();
        //    }
            axis.ResetEventStatus((ushort)~a.EventData);
            axis.ClearInterrupt();
        }

        private void EventInt_CheckedChanged(object sender, EventArgs e)
        {

            PMD.PMDTaskState taskstatus;
            if (EventInt.Checked)
            {
                taskstatus = devMC.TaskState;
                if (taskstatus == PMD.PMDTaskState.NoCode)
                {
                    MessageBox.Show("Use of Magellan Event Interupts requires User Code running on CME");
                    EventInt.Checked = false;
                }
                else if (taskstatus != PMD.PMDTaskState.Running)
                {
                    MessageBox.Show("The User Code will now be started.");
                    devMC.TaskStart();
                }

                if (EventInt.Checked)
                {
                    PMDMagellanEventHandler evMagellan = new PMDMagellanEventHandler();

                    evMagellan.PMDEvent += HandleMagellanEvent;
                    try
                    {
                        evMagellan.Connect(ipaddress);
                        
                        
                        // setup interrupt mask for Axis1
                        Axis1.InterruptMask = 0x4EF1;
                        SetEventLabelText("");

                    }
                    catch
                    {
                        MessageBox.Show("Could not connect to Noitification socket at port 40200!!");
                        EventInt.Checked = false;
                    }
                }
            
            }
        }

        private void Move_Click(object sender, EventArgs e)
        {

           UInt16 mode;
           mode = Axis1.ActiveOperatingMode;
           if ((mode & 0x0020) != 0)
           {
               Axis1.ProfileMode = PMD.PMDProfileMode.Trapezoidal;
               Axis1.Position = Convert.ToInt32(DestPosBox.Text);
               Axis1.Acceleration = (UInt32) Convert.ToInt32(AccelBox.Text);
               Axis1.Velocity = Convert.ToInt32(VelocityBox.Text);
               Axis1.ResetEventStatus(0);
               
               if(!EventInt.Checked) SetEventLabelText("Event:");

               //Disable MotionError for test purposes
               Axis1.SetEventAction(PMD.PMDEventActionEvent.MotionError, PMD.PMDEventAction.None);
               Axis1.Update();
           }
           else MessageBox.Show("Operating Mode not Enabled!!");
             
        }

        private void Stopbutton_Click_1(object sender, EventArgs e)
        {
            Axis1.StopMode = PMD.PMDStopMode.Abrupt;
            Axis1.Update();

        }

        private void Disablebutton_Click(object sender, EventArgs e)
        {
            Axis1.OperatingMode = (ushort) PMD.PMDOperatingMode.AxisEnabled;
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
           
            timerstop=true;
            if ((IOobject == null) == false) IOobject.Close();
            if((nvramobj==null)==false) nvramobj.Close();
            if((devMC==null)==false) devMC.Close();
            if((CommHandle == null) == false) CommHandle.Close();
            this.Close();
        }



        private void InitializeBackgoundWorker()
        {
            backgroundWorker1.DoWork += new DoWorkEventHandler(backgroundWorker1_DoWork);

            backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorker1_RunWorkerCompleted);
            backgroundWorker1.ProgressChanged += new ProgressChangedEventHandler(backgroundWorker1_ProgressChanged);
        }

        // This event handler is where the actual,
        // potentially time-consuming work is done.
        private void backgroundWorker1_DoWork(object sender,
            DoWorkEventArgs e)
        {
            // Get the BackgroundWorker that raised this event.
            BackgroundWorker worker = sender as BackgroundWorker;

            // Assign the result of the computation
            // to the Result property of the DoWorkEventArgs
            // object. This is will be available to the 
            // RunWorkerCompleted eventhandler.
            //  e.Result = ComputeFibonacci((int)e.Argument, worker, e);
        }

        // This event handler deals with the results of the
        // background operation.
        private void backgroundWorker1_RunWorkerCompleted(
            object sender, RunWorkerCompletedEventArgs e)
        {
            // First, handle the case where an exception was thrown.
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message);
            }
            else if (e.Cancelled)
            {
                // Next, handle the case where the user canceled 
                // the operation.
                // Note that due to a race condition in 
                // the DoWork event handler, the Cancelled
                // flag may not have been set, even though
                // CancelAsync was called.
                //          resultLabel.Text = "Canceled";
            }
            else
            {
                // Finally, handle the case where the operation 
                // succeeded.
                //       resultLabel.Text = e.Result.ToString();
                //           this.SetText(e.Result.ToString());
            }

            // Enable the UpDown control.
            //   this.numericUpDown1.Enabled = true;

            // Enable the Start button.
            //   startAsyncButton.Enabled = true;

            // Disable the Cancel button.
            //   cancelAsyncButton.Enabled = false;
        }

        // This event handler updates the progress bar.
        private void backgroundWorker1_ProgressChanged(object sender,
            ProgressChangedEventArgs e)
        {
       //     this.progressBar1.Value = e.ProgressPercentage;
        }
    
      
    
    }



}
