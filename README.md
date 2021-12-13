# Wearable-Wireless-Bio-sensor-on-MBed
Gatech ECE 4180 Final Project


Windows Application
We developed the GUI via Winform application based on the Microsoft .NET Framework 4.7.2. The entire GUI based application is coded using C#. 



We use a multithreading approach to implement our application. Basically we have two threads. One handles the GUI (data monitoring and visualization) and its user interaction activities (user-friendly buttons, dropdown boxes, graphs, etc.) The other thread is the serial port handler which sets up the serial port, polls and manipulates the data in parallel in a while loop.


CustomBluetooth Communication Protocol
We designed a custom communication protocol over Bluetooth. A header flag and a tailor flag are implemented to differentiate each data packet (a set of measurements). In the case of incomplete packets, we just discard them and move forward. Data type for each measurement is byte.

At last, we implemented try & catch blocks in multiple sensitive places to ensure the program would not crash when expected exceptions occur. Notice that the serial port thread needs to be killed gracefully to avoid problems, including the port being in use even after the program exits, stopping us from reusing the serial port in the same or different program. 
