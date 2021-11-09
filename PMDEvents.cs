
using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;


public class PMDEventArgs : EventArgs {
    
    private int axis;
    
    private int data;
    
    public PMDEventArgs(int axis, int eventdata) {
        this.axis = axis;
        this.data = eventdata;
    }
    
    public int EventAxis {
        get {
            return axis;
        }
    }
    
    public int EventData {
        get {
            return data;
        }
    }
}
public class PMDMagellanEventHandler : PMDEventHandler {
    
    public PMDMagellanEventHandler() {
        this.TCPport = 40200;
    }
}
public class PMDIOEventHandler : PMDEventHandler {
    
    public PMDIOEventHandler() {
        this.TCPport = 40300;
    }
}
//  Delegate declaration.
public delegate void PMDEventHandlerDelegate(object sender, PMDEventArgs e);
public class PMDEventHandler {
    
    private TcpClient client;
    
    private NetworkStream stream;
    
    private ManualResetEvent allDone = new ManualResetEvent(true);
    
    private Thread newThread;
      
    protected int TCPport;
    
    public event PMDEventHandlerDelegate PMDEvent;
    
    protected virtual void OnPMDEvent(PMDEventArgs e) {
        PMDEvent(this, e);
    }
    
    public void Close() {
        if (!(stream == null)) {
            stream.Close();
        }
        if (!(client == null)) {
            client.Close();
        }
        allDone.WaitOne(1000);
    }
    
    public void Connect(string ipaddress) {
        // byte[,] data;
        client = new TcpClient();
        client.Connect(ipaddress, TCPport);
        newThread = new Thread(new ThreadStart(ReadThread));
        stream = client.GetStream();
        allDone.Reset();
        newThread.Start();
    }
    
    //  Thread that waits for incoming TCP data.
    //  it ends the TCP connection is closed 
    public void ReadThread() {
        byte[] ReadBuffer=new Byte[20];
       // ReadBuffer[0] = 0;
        int numberOfBytesRead;
        int axis;
        int eventdata;
        ReadBuffer[0] = 0;
        
        try {
            stream.ReadTimeout = -1;
            while (true) {
                numberOfBytesRead = stream.Read(ReadBuffer, 0, 4);
                if ((numberOfBytesRead == 4)) {
                    axis = ReadBuffer[1];
                    //eventdata = ShiftLeft(int.Parse(ReadBuffer[2]), 8);
                    eventdata = ReadBuffer[2] << 8;
                    eventdata = (eventdata + ReadBuffer[3]);
                    PMDEventArgs e = new PMDEventArgs(axis, eventdata);
                    OnPMDEvent(e);
                }
            }
        }
        catch (Exception e) {
            // Console.WriteLine(e.Message)
        }
        allDone.Set();
    }
}


