# Wearable-Wireless-Bio-sensor-on-MBed
Gatech ECE 4180 Final Project

## This repo contains two parts

* Mbed_Program (Mbed program)  
* Win_APP/BTSerial_Interface (Winform Application)


## To Run this Demo

### Step 1: Set up Mbed Program
* Go to the Mbed online compiler at https://os.mbed.com/  
* Create an empty project on platform Mbed *LPC1768*  (this code is only tested on *LPC1768*, other versions of Mbed are not guranteed to work)  
* Upload all the files in the ``` Mbed_Program ``` and compile the program  
* Download the **.bin** file and upload it onto the mbed  

### Step 2: Set up Winform Application
* Download the ``` Win_APP/BTSerial_Interface ``` folder
* Open the **BTSerial_Interface.sln** with Visual Studio IDE (only Microsoft .NET Framework 4.7.2 and newer version will be supported)  
* Connect Windows to the Bluetooth deivce *HC-05* (default code is **1234**)  
* Click ``` Start ``` buttom in the Visual Studio IDE  
* Select ``` Port Number ``` (serial port that the Bluetooth is connected to in this case)  
* Select ``` Baudrate ``` to be 9600
* Click ``` Start Receive ``` to begin transmitting the measurement data
* Click ``` Stop Receive ``` to stop transmitting the measurement data
