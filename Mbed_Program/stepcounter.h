#ifndef __STEPCOUNTER_H__
#define __STEPCOUNTER_H__
 
#include "stdint.h"
#include "math.h"  
 
#define FILTER_CNT 3
 
#define MAX(a, b) ((a) > (b) ? (a) : (b))
#define MIN(a, b) ((a) < (b) ? (a) : (b))
#define SAMPLE_SIZE 10
 
#define ABS(a) (0 - (a)) > 0 ? (-(a)) : (a)
#define DYNAMIC_PRECISION 60
 
#define MOST_ACTIVE_NULL 0
#define MOST_ACTIVE_X 1
#define MOST_ACTIVE_Y 2
#define MOST_ACTIVE_Z 3
 
#define ACTIVE_PRECISION 120
 
#define DATA_FACTOR 1000
 
typedef struct
{
    short x;
    short y;
    short z;
} axis_info_t;
 
typedef struct
{
    axis_info_t newmax;
    axis_info_t newmin;
    axis_info_t oldmax;
    axis_info_t oldmin;
} peak_value_t;
 
typedef struct filter_avg
{
    axis_info_t info[FILTER_CNT];
    unsigned char count;
} filter_avg_t;
 
 
 
typedef struct slid_reg
{
    axis_info_t new_sample;
    axis_info_t old_sample;
} slid_reg_t;
 
 
 
void filter_calculate(filter_avg_t *filter, axis_info_t *sample);
void peak_value_init(peak_value_t *peak);
void peak_update(peak_value_t *peak, axis_info_t *cur_sample);
char slid_update(slid_reg_t *slid, axis_info_t *cur_sample);
char is_most_active(peak_value_t *peak);
void detect_step(peak_value_t *peak, slid_reg_t *slid, axis_info_t *cur_sample);
uint8_t get_step();
 
#endif