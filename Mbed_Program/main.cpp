#include "mbed.h"
#include "rtos.h"
#include "LSM9DS1.h"
#include "PulseSensor.h"
#define PI 3.14159
// Earth's magnetic field varies by location. Add or subtract
// a declination to get a more accurate heading. Calculate
// your's here:
// http://www.ngdc.noaa.gov/geomag-web/#declination
#define DECLINATION -4.94 // Declination (degrees) in Atlanta, GA.
LSM9DS1 IMU(p28, p27, 0xD6, 0x3C);
Timer timer;

/*
Skin temp
Heart beat
Human resistance
Step count
*/

// IMU
//filter_avg_t acc_data;
//axis_info_t acc_sample;
//peak_value_t acc_peak;
//slid_reg_t acc_slid;
uint8_t step = 1;
float threshold = 1.8;

// skin temp
AnalogIn skinTemp(p19);
uint8_t temp_skin = 25;

//Heart beat
uint8_t bpm = 70;

//Human resistance
AnalogIn gsr(p17);
AnalogIn sig(p18);
 
uint8_t Human_Resistance = 128;
float gsrValue  = 0;
float phasic = 0;
float baseline = 0;
int on = 1, off = 0;

Serial  pc(USBTX, USBRX);
RawSerial  dev(p9,p10);
DigitalOut led1(LED1);
DigitalOut led4(LED4);

struct gyro {
    float x;
    float y;
    float z;
};

struct acce {
    float x;
    float y;
    float z;
};

char imu_data[24];
    
void sendDataToProcessing(int data)
{
    pc.printf("%d\r\n", data);
    bpm = data;
}


void step_counter(void const *arg) {
    
    //peak_value_init(&acc_peak);
    struct acce ac2;
    
    float mag;
    
    timer.start();
    
    while(1) {
        while(!IMU.accelAvailable());
        //pc.printf("11\n");
        IMU.readAccel();
        ac2.x = IMU.calcAccel(IMU.ax);
        ac2.y = IMU.calcAccel(IMU.ay);
        ac2.z = IMU.calcAccel(IMU.az);
        mag = sqrt(pow(ac2.x, 2) + pow(ac2.y, 2) + pow(ac2.z, 2));
        
        if (mag > threshold && timer.read_ms() > 300) {
            //pc.printf("%d \n\r", timer.read_ms());
            step += 1;
            timer.stop();
            timer.reset();
            timer.start();
        }
        Thread::wait(5);
    }
    
            
    //while (1) {
//        uint16_t i = 0;
//        float temp = 0;
//
//        for (i = 0; i < FILTER_CNT; i++)
//        {
//            while(!IMU.accelAvailable());
//            //pc.printf("11\n");
//            IMU.readAccel();
//            ac2.x = IMU.calcAccel(IMU.ax);
//            ac2.y = IMU.calcAccel(IMU.ay);
//            ac2.z = IMU.calcAccel(IMU.az);
//            
//            temp = ac2.x * DATA_FACTOR;
//            acc_data.info[i].x = (short)(temp);
//
//            temp = ac2.y * DATA_FACTOR;
//            acc_data.info[i].y = (short)temp;
//
//            temp = ac2.z * DATA_FACTOR;
//            acc_data.info[i].z = (short)temp;
//            
//            Thread::wait(5);
//        }
//
//        filter_calculate(&acc_data, &acc_sample);
//
//        peak_update(&acc_peak, &acc_sample);
//
//        slid_update(&acc_slid, &acc_sample);
//
//        detect_step(&acc_peak, &acc_slid, &acc_sample);
//        
//        timer.stop();
//        if(timer.read_ms() <= 20)
//            Thread::wait(20 - timer.read_ms());
//        //Thread::wait(5);
//        
//    }
    
}

void skin_temp(void const *arg) {
     float R1 = 4700*1.4773; //thermistor resistance at 15C
    float R2 = 4700; //thermistor resistance at 25C
    float R3 = 4700*0.69105; //thermistor resistance at 35C

    float T1 = 288.15; //15C
    float T2 = 298.15; //25C
    float T3 = 308.15; //35C

    float L1 = log(R1);
    float L2 = log(R2);
    float L3 = log(R3);
    float Y1 = 1/T1;
    float Y2 = 1/T2;
    float Y3 = 1/T3;

    float g2 = (Y2-Y1)/(L2-L1);
    float g3 = (Y3-Y1)/(L3-L1);

    float C = (g3-g2)/(L3-L2)*(1/(L1+L2+L3));
    float B = g2 - C*(L1*L1 + L1*L2 + L2*L2);
    float A = Y1 - L1*(B + L1*L1*C);
    
    float Vt;
    while(1) {
        Vt = skinTemp;
        //float R = 9900*(1/Vt - 1); //9900 is the resistance of R1 in voltage divider
        float R = 4900 * Vt / (1 - Vt);

        float T = 1/(A + B*log(R) + C*log(R)*log(R)*log(R));
        //pc.printf("Vt: %f\n\r", Vt);
        //pc.printf("R: %f\n\r", R);
        
        temp_skin=(uint8_t)(T-273.15); 
        //pc.printf("temp: %d\n", temp_skin);
        //pc.printf("Skin temp: %f C\n\r", T-273.15);
        Thread::wait(100);      
    }
    
}
 
// calculate baseline to compare against
void Get_Baseline(void)
{
    double sum = 0;
    wait(1);
    for(int i=0; i<500; i++) {
        gsrValue  = sig;
        sum += gsrValue ;
        wait(0.005);
    }
    baseline = sum/500;
    //printf("baseline = %f\n\r", baseline);
}
 
// main loop, compare against baseline
// sound buzzer if a >5% change happens
void hum_R(void const *arg)
{
    float delta;
    float Serial_Port_Reading;
    int hr;
    Get_Baseline();
   
    while(1) {
        gsrValue  = gsr;
        phasic = sig;
        delta = gsrValue  - phasic;
        if(abs(delta) > 0.05) {
            gsrValue = gsr;
            delta = baseline - gsrValue;
        }
        hr = 254* (phasic);
        //pc.printf("HR!!: %f\n", phasic);
        //Human_Resistance = ((1024+2*Serial_Port_Reading)*10000)/(512-Serial_Port_Reading);
        Human_Resistance = hr;
        Thread::wait(100);
    }
}

//void heartRate(void const *arg) {
//    while (1) {
//        bpm = rand() % 21 + 70;
//        //bpm += (x - 5);
//        Thread::wait(2000);
//    }
//}

int main()
{
    
    //pc.baud(9600);
    //dev.baud(9600);
    
    IMU.begin();
    if (!IMU.begin()) {
        pc.printf("Failed to communicate with LSM9DS1.\n");
    }
    IMU.calibrate(1);
     pc.printf("IMU end\n");
    
    PulseSensor sensor(p15, sendDataToProcessing);
    sensor.start();
   
    Thread t1(step_counter);
    Thread t2(skin_temp);
    Thread t3(hum_R);
    //Thread t4(heartRate);
    
    pc.printf("Main Loop\n");
    while(1) {
        
        dev.putc(0xff);
        dev.putc(temp_skin);
        dev.putc(bpm);
        dev.putc(Human_Resistance);
        dev.putc(step);
        
        dev.putc(0xff);
        //pc.printf("%d\n\r", rand() % 31 + 60);
        Thread::wait(200);
        
    }
//    struct acce ac2;
//    while(1) {
//        
//        IMU.readAccel();
//        ac2.x = IMU.calcAccel(IMU.ax);
//        ac2.y = IMU.calcAccel(IMU.ay);
//        ac2.z = IMU.calcAccel(IMU.az);
//        pc.printf("%f \n\r", sqrt(pow(ac2.x, 2) + pow(ac2.y, 2) + pow(ac2.z, 2)));
//        wait(0.1);
//    }
}
