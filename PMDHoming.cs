using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PMDLibrary;


class PMDHoming
{
        public void HomeSwitch(PMD.PMDAxis axis)
        {
            //  This assumes it is desirable to approach the home switch from its postive side.
	        //  Also assumes current position is anywhere on the postive side of NegativeLimit.  
	        //  The HomeSwitch will become the zero position.
            //  1. First check to see if HomeSwitch is already active, if so move away.
	        //  2. Move toward HomeSwitch
	        //  3. Stop after HomeSwitch or NegativeLimit is detected. Skip to step #7 if HomeSwitch was found.
	        //  4. Move in positive direction until HomeSwitch is seen.
	        //  5. Continue until out of HomeSwitch.
	        //  6. Move back into HomeSwitch (from positive side)
	        //  7. Request Capture Value and use that to "zero" the HomeSwitch
	        //  8. Move to HomeSwitch and stop
	
	        UInt16 status=0;
            Int32 positioncapture;
	        Int32 HomeVelocity;
            UInt32 HomeAcceleration;
            
      	    axis.CaptureSource=PMD.PMDCaptureSource.Home;
            axis.ProfileMode=PMD.PMDProfileMode.Velocity;

	        // TO DO:	Update with the appropriate parameter values
	        //			before executing this code
	
	        HomeVelocity=3000;
            HomeAcceleration=10;

            // make sure sense of limit switches and home switches are proper.
            // this should have already been done by the Setup fucntion by they 
            // are shown hear for clarity.

	        //for active high limit switches
	        //status|=0x0030;
	
	        //for active high home switch
	        //status|=0x0008;
	
	        //axis.SignalSense=status;
            
	        axis.Acceleration=HomeAcceleration;
            axis.Update();
	
	        // First check state of HomeSwitch
	        status=axis.SignalStatus;
	        if((status & (ushort) PMD.PMDSignalStatusBit.EncoderHome)==0)
	        {
			    // Home switch is already active so we need to move away.
		        //printf("Home switch is already active.  Moving away...\n");
		        axis.Velocity=HomeVelocity;  //move in positive direction
			    axis.Update();
                status=0;
			    while(status==0)   // wait for home switch to go inactive
                {
				    status=axis.SignalStatus;
				    status&= (ushort) PMD.PMDSignalStatusBit.EncoderHome;
                }
		
            //  Since the home switch is no longer active we can add a stop here but it is not necessary.
		    //	PMDSetStopMode(&hAxis1, PMDSmoothStopMode);
		    //	PMDUpdate(&hAxis1);
	       }
		
	        int ResetMask;  //=(int) PMD.PMDEventMask.CaptureReceivedMask;
            ResetMask=(~(int)PMD.PMDEventStatus.CaptureReceived & ~(int)PMD.PMDEventStatus.MotionComplete);
            axis.ResetEventStatus((ushort)ResetMask);   // Reset CaptureRecevied and MotionComplete
	
	        // Need to clear out any previous captures to rearm the capture mechanism
	        positioncapture=axis.CaptureValue;
            // By default Home Capture occurs on falling edge.  Use SetSignalSense if raising edge is needed.

	        //printf("Looking for Home switch...\n");

	        // TO DO:	Update with the appropriate parameter values
	        //			before executing this code
	        axis.Velocity=-HomeVelocity;
	        axis.Update();

            axis.SetBreakpointValue((short)PMD.PMDBreakpoint.Breakpoint1,0x00080008);
            axis.SetBreakpoint((short)PMD.PMDBreakpoint.Breakpoint1, PMD.PMDAxisNumber.Axis1,PMD.PMDBreakpointAction.Update,PMD.PMDBreakpointTrigger.EventStatus);
	        axis.StopMode=PMD.PMDStopMode.Smooth;
	        // Breakpoints will stop motion when home switch is active.
	
	
	        // Since we are doing a SmoothStop the motion will stop some time after the home switch goes active
	        WaitForHomingEvent( axis, (ushort) (PMD.PMDEventStatus.CaptureReceived|PMD.PMDEventStatus.InNegativeLimit));
            WaitForHomingEvent( axis, (ushort) PMD.PMDEventStatus.MotionComplete);
	
	
	        // Check to see if NegativeLimit was reached without seeing home switch.
	        status=axis.SignalStatus;
            if ((status & (ushort) PMD.PMDSignalStatus.NegativeLimit)==0)
	        {
                //printf("\nIn Negative Limit, Home switch not found.\nNow moving in positve direction looking for home switch...\n\n");
	            axis.StopMode=PMD.PMDStopMode.None;  // this is to dis-arm previous SetStopMode command.
	            axis.Velocity=HomeVelocity;
	            axis.Update();
	            WaitForHomingEvent( axis, (ushort)PMD.PMDEventStatus.CaptureReceived );
	            positioncapture=axis.CaptureValue;   // only for re-arming capture, throw away value
                //printf("\nHome switch found, moving to positve side....\n\n");
	            status=0;
	            while(status==0)   // wait for home switch to go inactive
                {
				    status=axis.SignalStatus;
				    status&= (ushort) PMD.PMDSignalStatus.EncoderHome;
                }
                            
                //printf("Re-entering Home switch from positve side\n\n");
	            axis.SetBreakpoint((short)PMD.PMDBreakpoint.Breakpoint1, PMD.PMDAxisNumber.Axis1,PMD.PMDBreakpointAction.Update,PMD.PMDBreakpointTrigger.EventStatus);
	            axis.Velocity=-HomeVelocity;
	            axis.Update();
	            axis.StopMode=PMD.PMDStopMode.Smooth;
                WaitForHomingEvent(axis, (ushort) PMD.PMDEventStatus.CaptureReceived );
    
	        }

    

            //printf("\nHome switch found and motion is stopped.\n");

	        positioncapture=axis.CaptureValue;
	        axis.AdjustActualPosition(-positioncapture);
    
            //	axis.ClearPositionError();  // useful for Stepper/Encoder systems
            //	axis.Update();

	        //printf("\nMove to Home Switch.....\n");
            ResetMask=(~(int)PMD.PMDEventStatus.CaptureReceived & ~(int)PMD.PMDEventStatus.MotionComplete);
            axis.ResetEventStatus((ushort)ResetMask);   // Reset CaptureRecevied and MotionComplete
            axis.ProfileMode=PMD.PMDProfileMode.Trapezoidal;
	        axis.Position=0;
	        axis.Velocity=HomeVelocity;
	        axis.Update();  // Move to home switch (position=0)
            WaitForHomingEvent(axis, (ushort) PMD.PMDEventStatus.MotionComplete);
	        //printf("\nHoming Complete.\n");

        }
    


        //*****************************************************************************
        // WaitForHomingEvent 
        //
        //   Waits for the specified event to be set in the EventStatus register.
        //
        //*****************************************************************************
        bool WaitForHomingEvent(PMD.PMDAxis phAxis, UInt16 eventmask)
        {
	        bool eventoccured = false;
	        UInt16 status = 0;
	        PMD.PMDResult result = PMD.PMDResult.ERR_OK;

	        while (!eventoccured && result == PMD.PMDResult.ERR_OK)
	        {
                status = phAxis.EventStatus;
                if ((status & eventmask) != 0)
                {
			        eventoccured=true;
		        //	printf("Event(s) %04X Set.\n", status);
		        }
	        }
            if ((status & (ushort)PMD.PMDEventStatus.MotionError)!=0)
	        {
                return eventoccured;
            }
	       
            // clear the event for the next time
	        phAxis.ResetEventStatus((UInt16)~eventmask );

	        return eventoccured;
        }

}

