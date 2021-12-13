using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;
using System.Diagnostics;

namespace BTSerial_Interface
{
    public partial class Form1 : Form
    {
        string Selected_Port;
        string Selected_Baudrate;
        SerialPort COMport;
        Thread ReadData;
        ManualResetEvent threadStop;

        public Form1()
        {
            InitializeComponent();
            Available_Ports_Dropdown.Items.AddRange(SerialPort.GetPortNames());
            Serial_Receive_Groupbox.Enabled = false;
            Baudrate_Dropdown.Enabled = false;
            Temperature_Graph.Enabled = false;
            Heartrate_Graph.Enabled = false;
            SpO2_Graph.Enabled = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Serial_Settings_Enter(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Available_Ports_Dropdown_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Selected_Port = Available_Ports_Dropdown.SelectedItem.ToString();
            Baudrate_Dropdown.Enabled = true;
        }

        private void Baudrate_Dropdown_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Selected_Baudrate = Baudrate_Dropdown.SelectedItem.ToString();
            Serial_Receive_Groupbox.Enabled = true;
        }

        private void Start_Receive_Button_Click(object sender, EventArgs e)
        {
            // disable selection dropdown boxes
            Available_Ports_Dropdown.Enabled = false;
            Baudrate_Dropdown.Enabled = false;
            Start_Receive_Button.Enabled = false;
            Stop_Receive_Button.Enabled = true;
            threadStop = new ManualResetEvent(false);

            string Port_Name = Available_Ports_Dropdown.SelectedItem.ToString();      // PortName
            int Baud_Rate = Convert.ToInt32(Baudrate_Dropdown.SelectedItem);          // BaudRate

            Temperature_Graph.Series["Temperature"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            Temperature_Graph.Enabled = true;
            Temperature_Graph.Series["Temperature"].Points.Clear();
            Temperature_Graph.ChartAreas[0].AxisX.Maximum = 30;
            Temperature_Graph.ChartAreas[0].AxisX.Minimum = 0;
            Heartrate_Graph.Series["Heart Rate"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            Heartrate_Graph.Enabled = true;
            Heartrate_Graph.Series["Heart Rate"].Points.Clear();
            Heartrate_Graph.ChartAreas[0].AxisX.Maximum = 30;
            Heartrate_Graph.ChartAreas[0].AxisX.Minimum = 0;
            SpO2_Graph.Series["GSR"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            SpO2_Graph.Enabled = true;
            SpO2_Graph.Series["GSR"].Points.Clear();
            SpO2_Graph.ChartAreas[0].AxisX.Maximum = 30;
            SpO2_Graph.ChartAreas[0].AxisX.Minimum = 0;
            Step_Graph.Series["Step Count"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            Step_Graph.Enabled = true;
            Step_Graph.Series["Step Count"].Points.Clear();
            Step_Graph.ChartAreas[0].AxisX.Maximum = 30;
            Step_Graph.ChartAreas[0].AxisX.Minimum = 0;
            TextBox_System_Log.Text = "Port " + Port_Name + " is opened " + "with baud rate of " + Baud_Rate;
            TextBox.CheckForIllegalCrossThreadCalls = false;
            COMport = new SerialPort(Port_Name, Baud_Rate);                // open a new serial port

            ReadData = new Thread(() =>
            {
                Start_Receive_Button_Handler(Port_Name, Baud_Rate);
                Console.WriteLine("Hello, world");
            });
            ReadData.Start();
            

        }

        delegate void SetGraphCallback(int counter, float temp, float heartrate, float bloodOX, float step);

        private void SetGraph(int counter, float temp, float heartrate, float bloodOX, float step)
        {
           
            if (this.Temperature_Graph.InvokeRequired)
            {
                SetGraphCallback d = new SetGraphCallback(SetGraph);
                this.BeginInvoke(d, new object[] { counter, temp, heartrate, bloodOX, step});
            }
            else
            {
                if (counter > 30)
                {
                    // Temperature_Graph.Series["Series1"].Points.RemoveAt(0);
                    Temperature_Graph.ChartAreas[0].AxisX.Maximum = counter;
                    Temperature_Graph.ChartAreas[0].AxisX.Minimum = counter - 30;
                    Heartrate_Graph.ChartAreas[0].AxisX.Maximum = counter;
                    Heartrate_Graph.ChartAreas[0].AxisX.Minimum = counter - 30;
                    SpO2_Graph.ChartAreas[0].AxisX.Maximum = counter;
                    SpO2_Graph.ChartAreas[0].AxisX.Minimum = counter - 30;
                    Step_Graph.ChartAreas[0].AxisX.Maximum = counter;
                    Step_Graph.ChartAreas[0].AxisX.Minimum = counter - 30;
                }
                Temperature_Graph.Series["Temperature"].Points.AddXY(counter, temp);
                Temperature_Graph.Update();
                Heartrate_Graph.Series["Heart Rate"].Points.AddXY(counter, heartrate);
                Heartrate_Graph.Update();
                SpO2_Graph.Series["GSR"].Points.AddXY(counter, bloodOX);
                SpO2_Graph.Update();
                Step_Graph.Series["Step Count"].Points.AddXY(counter, step);
                Step_Graph.Update();
                Received_Data_Textbox.AppendText(counter.ToString() + Environment.NewLine);
                Received_Data_Textbox.AppendText("Temperature: " + temp.ToString() + Environment.NewLine);
                Received_Data_Textbox.AppendText("Heart rate: " + heartrate.ToString() + Environment.NewLine);
                Received_Data_Textbox.AppendText("GSR: " + bloodOX.ToString() + Environment.NewLine);
                Received_Data_Textbox.AppendText("Step count: " + step.ToString() + Environment.NewLine);
                Received_Data_Textbox.AppendText(Environment.NewLine);
            }
        }

        private void Start_Receive_Button_Handler(string Port_Name, int Baud_Rate)
        {

            byte data = 0;                                                                // Declare data type

            // SerialPortFixer.Execute(Port_Name);

            // try opening the serial port
            try
            {
                COMport.Open();
            }
            catch (UnauthorizedAccessException SerialException) //exception that is thrown when the operating system denies access 
            {
                MessageBox.Show(SerialException.ToString());
                TextBox_System_Log.Text = Port_Name + Baud_Rate;
                TextBox_System_Log.Text = TextBox_System_Log.Text + SerialException.ToString();
                COMport.Close();                                  // Close the Port
            }

            catch (System.IO.IOException SerialException)     // An attempt to set the state of the underlying port failed
            {
                MessageBox.Show(SerialException.ToString());
                TextBox_System_Log.Text = Port_Name + Baud_Rate;
                TextBox_System_Log.Text = TextBox_System_Log.Text + SerialException.ToString();
                COMport.Close();                                  // Close the Port
            }

            catch (InvalidOperationException SerialException) // The specified port on the current instance of the SerialPort is already open
            {
                MessageBox.Show(SerialException.ToString());
                TextBox_System_Log.Text = Port_Name + Baud_Rate;
                TextBox_System_Log.Text = TextBox_System_Log.Text + SerialException.ToString();
                COMport.Close();                                  // Close the Port
            }


            catch //Any other ERROR
            {
                MessageBox.Show("ERROR in Opening Serial PORT -- UnKnown ERROR");
                COMport.Close();                                  // Close the Port
            }


            // Start Reading data
            if (COMport.IsOpen == true)
            {
                COMport.ReadTimeout = 50;
                System.Threading.Thread.Sleep(100);
                // Temperature_Graph.Series["Series1"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
                // Temperature_Graph.Enabled = true;
                ArrayList dataArrayList = new ArrayList();


                // Declare utility variables
                int counter = 0;
                
                // Declare measurement data
                float temp = 0;             // body temperature
                float heartrate = 0;        // heart rate
                float bloodOX = 0;          // GSR
                float step = 0;             // step count

                #region Data parsing, storing and graphing
                // Loop to receive data until flag
                while (true)
                {
                    // Looking for the first 0xFF flag
                    Stopwatch sw = new Stopwatch();
                    sw.Start();
                    try
                    {
                        do
                        {
                            if (sw.ElapsedMilliseconds > 5000) throw new TimeoutException();
                            try
                            {
                                data = (byte)COMport.ReadByte();
                            }
                            catch (System.IO.IOException error)
                            {
                                MessageBox.Show(error.ToString());
                                COMport.Close();
                                return;
                            }
                            catch (System.InvalidOperationException error)
                            {
                                MessageBox.Show(error.ToString());
                                COMport.Close();
                                return;
                            }
                            catch (System.TimeoutException error)
                            {
                                continue;
                            }
                            
                        } while (data != 0xFF);
                    }
                    catch (TimeoutException e)
                    {
                        MessageBox.Show("Timeout waiting for new data");
                        Stop_Receive_Button_Handler();
                        break;
                    }

                    // We received the first 0xFF flag

                    // Looking for the second 0xFF flag
                    try
                    {
                        data = (byte)COMport.ReadByte();
                    }
                    catch (System.IO.IOException error)
                    {
                        MessageBox.Show(error.ToString());
                        COMport.Close();
                        return;
                    }
                    catch (System.InvalidOperationException error)
                    {
                        MessageBox.Show(error.ToString());
                        COMport.Close();
                        return;
                    }
                    catch (System.TimeoutException error)
                    {
                        continue;
                    }
                    // Did not find the second consecutive 0xFF flag
                    if (data != 0xFF)
                    {
                        continue;
                    }

                    // Found the second consecutive 0xFF flag and start transmitting data
                    do
                    {
                        try
                        {
                            data = (byte)COMport.ReadByte();
                        }
                        catch (System.IO.IOException error)
                        {
                            MessageBox.Show(error.ToString());
                            COMport.Close();
                            return;
                        }
                        catch (System.InvalidOperationException error)
                        {
                            MessageBox.Show(error.ToString());
                            COMport.Close();
                            return;
                        }
                        catch (System.TimeoutException error)
                        {
                            continue;
                        }
                        if (data == 0)
                        {
                            continue;
                        }
                        else if (data != 0xFF)
                        {
                            #region debugging only
                            //MessageBox.Show("hi");
                            //char ASCII_data = (char)data;
                            //Received_Data_Textbox.AppendText(ASCII_data.ToString());
                            #endregion
                            
                            dataArrayList.Add(data);

                        }
                        else // reaching the end of the data set
                        {
                            #region debugging only
                            //Received_Data_Textbox.AppendText(Environment.NewLine);
                            #endregion

                            // ------------- start to process and graph the data ---------------

                            // Convert to byte array
                            byte[] dataArray = (byte[])dataArrayList.ToArray(typeof(byte));
                            
                            #region Process data
                            // Process byte and float
                            if (dataArrayList.Count != 4)
                            {
                                TextBox_System_Log.Text = "Packet Lost!";
                                dataArrayList.Clear();
                                break;
                            }
                            TextBox_System_Log.Text = "Packet Complete!";
                            dataArrayList.Clear();
                            try
                            {
                                temp = dataArray[0];
                                heartrate = dataArray[1];
                                bloodOX = dataArray[2];
                                step = dataArray[3];
                                
                            } catch (Exception error)
                            {
                                MessageBox.Show("Array Error!");
                            }
                            
                            

                            // Process float
                            // --------- refer to 

                            #endregion

                            #region Print data to textbox
                            // Print data to textbox

                            // Received_Data_Textbox.AppendText("Temperature: " + temp.ToString() + Environment.NewLine);
                            // Received_Data_Textbox.AppendText("Heart rate: " + heartrate.ToString() + Environment.NewLine);
                            // Received_Data_Textbox.AppendText("GSR: " + bloodOX.ToString() + Environment.NewLine);
                            // Received_Data_Textbox.AppendText(Environment.NewLine);

                            #endregion

                            #region Graph data (max 6 sets)

                            // Graph data (6 sets)
                            if (counter > 6) // graph exceeding 6 sets  
                            {
                                // Temperature_Graph.Series["Series1"].Points.Clear(); // clear temperature graph
                                // clear heart rate graph
                                // clear GSR graph
                                // counter = 0;                                        // reset counter
                            }
                            SetGraph(counter, temp, heartrate, bloodOX, step);
                            // graph temperature
                            // Temperature_Graph.Series["Series1"].Points.AddXY(counter, temp);
                            // Temperature_Graph.Update();

                            // graph heart rate

                            // graph GSR

                            counter++;
                            
                            #endregion

                        }
                    } while (data != 0xFF);
                if (threadStop.WaitOne(0))
                    break;
                System.Threading.Thread.Sleep(200);
                }
                #endregion
            }
        }

        private void Stop_Receive_Button_Click(object sender, EventArgs e)
        {
            Stop_Receive_Button_Handler();

        }

        private void Stop_Receive_Button_Handler()
        {
            // Stop receive data and close COM port
            MessageBox.Show("Closing the serial port!");
            Available_Ports_Dropdown.Enabled = true;
            Baudrate_Dropdown.Enabled = true;
            Start_Receive_Button.Enabled = true;
            Stop_Receive_Button.Enabled = false;

            threadStop.Set();
            // ReadData.Abort();
            System.Threading.Thread.Sleep(150);
            ReadData.Join();
            COMport.Close(); // fix this!!!!!!!!!!!!!!!!

        }

        private void Data_Label_Click(object sender, EventArgs e)
        {

        }

        private void Temperature_Graph_Click(object sender, EventArgs e)
        {

        }

        private void Heartrate_Graph_Click(object sender, EventArgs e)
        {

        }

        private void SpO2_Graph_Click(object sender, EventArgs e)
        {

        }

        private void Serial_Receive_Groupbox_Enter(object sender, EventArgs e)
        {

        }
    }
}



// Copyright 2010-2014 Zach Saw
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

public class SerialPortFixer : IDisposable
{
    public static void Execute(string portName)
    {
        using (new SerialPortFixer(portName))
        {
        }
    }
    #region IDisposable Members

    public void Dispose()
    {
        if (m_Handle != null)
        {
            m_Handle.Close();
            m_Handle = null;
        }
    }

    #endregion

    #region Implementation

    private const int DcbFlagAbortOnError = 14;
    private const int CommStateRetries = 10;
    private SafeFileHandle m_Handle;

    private SerialPortFixer(string portName)
    {
        const int dwFlagsAndAttributes = 0x40000000;
        const int dwAccess = unchecked((int)0xC0000000);

        if ((portName == null) || !portName.StartsWith("COM", StringComparison.OrdinalIgnoreCase))
        {
            throw new ArgumentException("Invalid Serial Port", "portName");
        }
        SafeFileHandle hFile = CreateFile(@"\\.\" + portName, dwAccess, 0, IntPtr.Zero, 3, dwFlagsAndAttributes,
                                          IntPtr.Zero);
        if (hFile.IsInvalid)
        {
            WinIoError();
        }
        try
        {
            int fileType = GetFileType(hFile);
            if ((fileType != 2) && (fileType != 0))
            {
                throw new ArgumentException("Invalid Serial Port", "portName");
            }
            m_Handle = hFile;
            InitializeDcb();
        }
        catch
        {
            hFile.Close();
            m_Handle = null;
            throw;
        }
    }

    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern int FormatMessage(int dwFlags, HandleRef lpSource, int dwMessageId, int dwLanguageId,
                                            StringBuilder lpBuffer, int nSize, IntPtr arguments);

    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern bool GetCommState(SafeFileHandle hFile, ref Dcb lpDcb);

    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern bool SetCommState(SafeFileHandle hFile, ref Dcb lpDcb);

    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern bool ClearCommError(SafeFileHandle hFile, ref int lpErrors, ref Comstat lpStat);

    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern SafeFileHandle CreateFile(string lpFileName, int dwDesiredAccess, int dwShareMode,
                                                    IntPtr securityAttrs, int dwCreationDisposition,
                                                    int dwFlagsAndAttributes, IntPtr hTemplateFile);

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern int GetFileType(SafeFileHandle hFile);

    private void InitializeDcb()
    {
        Dcb dcb = new Dcb();
        GetCommStateNative(ref dcb);
        dcb.Flags &= ~(1u << DcbFlagAbortOnError);
        SetCommStateNative(ref dcb);
    }

    private static string GetMessage(int errorCode)
    {
        StringBuilder lpBuffer = new StringBuilder(0x200);
        if (
            FormatMessage(0x3200, new HandleRef(null, IntPtr.Zero), errorCode, 0, lpBuffer, lpBuffer.Capacity,
                          IntPtr.Zero) != 0)
        {
            return lpBuffer.ToString();
        }
        return "Unknown Error";
    }

    private static int MakeHrFromErrorCode(int errorCode)
    {
        return (int)(0x80070000 | (uint)errorCode);
    }

    private static void WinIoError()
    {
        int errorCode = Marshal.GetLastWin32Error();
        throw new IOException(GetMessage(errorCode), MakeHrFromErrorCode(errorCode));
    }

    private void GetCommStateNative(ref Dcb lpDcb)
    {
        int commErrors = 0;
        Comstat comStat = new Comstat();

        for (int i = 0; i < CommStateRetries; i++)
        {
            if (!ClearCommError(m_Handle, ref commErrors, ref comStat))
            {
                WinIoError();
            }
            if (GetCommState(m_Handle, ref lpDcb))
            {
                break;
            }
            if (i == CommStateRetries - 1)
            {
                WinIoError();
            }
        }
    }

    private void SetCommStateNative(ref Dcb lpDcb)
    {
        int commErrors = 0;
        Comstat comStat = new Comstat();

        for (int i = 0; i < CommStateRetries; i++)
        {
            if (!ClearCommError(m_Handle, ref commErrors, ref comStat))
            {
                WinIoError();
            }
            if (SetCommState(m_Handle, ref lpDcb))
            {
                break;
            }
            if (i == CommStateRetries - 1)
            {
                WinIoError();
            }
        }
    }

    #region Nested type: COMSTAT

    [StructLayout(LayoutKind.Sequential)]
    private struct Comstat
    {
        public readonly uint Flags;
        public readonly uint cbInQue;
        public readonly uint cbOutQue;
    }

    #endregion

    #region Nested type: DCB

    [StructLayout(LayoutKind.Sequential)]
    private struct Dcb
    {
        public readonly uint DCBlength;
        public readonly uint BaudRate;
        public uint Flags;
        public readonly ushort wReserved;
        public readonly ushort XonLim;
        public readonly ushort XoffLim;
        public readonly byte ByteSize;
        public readonly byte Parity;
        public readonly byte StopBits;
        public readonly byte XonChar;
        public readonly byte XoffChar;
        public readonly byte ErrorChar;
        public readonly byte EofChar;
        public readonly byte EvtChar;
        public readonly ushort wReserved1;
    }

    #endregion

    #endregion
}
