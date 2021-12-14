#include "stepcounter.h"
uint8_t step_cnt = 1;
/*
filter_avg_t acc_data;
axis_info_t acc_sample;
peak_value_t acc_peak;
slid_reg_t acc_slid;
*/


void filter_calculate(filter_avg_t *filter, axis_info_t *sample)
{
    unsigned int i;
    short x_sum = 0, y_sum = 0, z_sum = 0;
    for (i = 0; i < FILTER_CNT; i++)
    {
        x_sum += filter->info[i].x;
        y_sum += filter->info[i].y;
        z_sum += filter->info[i].z;
    }
    sample->x = x_sum / FILTER_CNT;
    sample->y = y_sum / FILTER_CNT;
    sample->z = z_sum / FILTER_CNT;
}

void peak_value_init(peak_value_t *peak)
{
    peak->newmax.x = -1000;
    peak->newmax.y = -1000;
    peak->newmax.z = -1000;

    peak->newmin.x = 1000;
    peak->newmin.y = 1000;
    peak->newmin.z = 1000;

    peak->oldmax.x = 0;
    peak->oldmax.y = 0;
    peak->oldmax.z = 0;
    peak->oldmin.x = 0;
    peak->oldmin.y = 0;
    peak->oldmin.z = 0;
}

void peak_update(peak_value_t *peak, axis_info_t *cur_sample)
{
    static unsigned int sample_size = 0;
    sample_size++;
    if (sample_size > SAMPLE_SIZE)
    {
        /* 50 samples for an update*/
        sample_size = 1;
        peak->oldmax = peak->newmax;
        peak->oldmin = peak->newmin;

        peak->newmax.x = -1000;
        peak->newmax.y = -1000;
        peak->newmax.z = -1000;

        peak->newmin.x = 1000;
        peak->newmin.y = 1000;
        peak->newmin.z = 1000;
        // peak_value_init(peak);
    }
    peak->newmax.x = MAX(peak->newmax.x, cur_sample->x);
    peak->newmax.y = MAX(peak->newmax.y, cur_sample->y);
    peak->newmax.z = MAX(peak->newmax.z, cur_sample->z);

    peak->newmin.x = MIN(peak->newmin.x, cur_sample->x);
    peak->newmin.y = MIN(peak->newmin.y, cur_sample->y);
    peak->newmin.z = MIN(peak->newmin.z, cur_sample->z);
}

char slid_update(slid_reg_t *slid, axis_info_t *cur_sample)
{
    char res = 0;
    if (ABS((cur_sample->x - slid->new_sample.x)) > DYNAMIC_PRECISION)
    {
        slid->old_sample.x = slid->new_sample.x;
        slid->new_sample.x = cur_sample->x;
        res = 1;
    }
    else
    {
        slid->old_sample.x = slid->new_sample.x;
    }
    if (ABS((cur_sample->y - slid->new_sample.y)) > DYNAMIC_PRECISION)
    {
        slid->old_sample.y = slid->new_sample.y;
        slid->new_sample.y = cur_sample->y;
        res = 1;
    }
    else
    {
        slid->old_sample.y = slid->new_sample.y;
    }

    if (ABS((cur_sample->z - slid->new_sample.z)) > DYNAMIC_PRECISION)
    {
        slid->old_sample.z = slid->new_sample.z;
        slid->new_sample.z = cur_sample->z;
        res = 1;
    }
    else
    {
        slid->old_sample.z = slid->new_sample.z;
    }
    return res;
}

/* the most active axis */
 char is_most_active(peak_value_t *peak)
{
    char res = MOST_ACTIVE_NULL;
    short x_change = ABS((peak->newmax.x - peak->newmin.x));
    short y_change = ABS((peak->newmax.y - peak->newmin.y));
    short z_change = ABS((peak->newmax.z - peak->newmin.z));

    if (x_change > y_change && x_change > z_change && x_change >= ACTIVE_PRECISION)
    {
        res = MOST_ACTIVE_X;
    }
    else if (y_change > x_change && y_change > z_change && y_change >= ACTIVE_PRECISION)
    {
        res = MOST_ACTIVE_Y;
    }
    else if (z_change > x_change && z_change > y_change && z_change >= ACTIVE_PRECISION)
    {
        res = MOST_ACTIVE_Z;
    }
    return res;
}

void detect_step(peak_value_t *peak, slid_reg_t *slid, axis_info_t *cur_sample)
{
    // static step_cnt = 0;
    //step_cnt++;
    char res = is_most_active(peak);
    switch (res)
    {
    case MOST_ACTIVE_NULL:
    {
        // fix
        break;
    }
    case MOST_ACTIVE_X:
    {
        short threshold_x = (peak->oldmax.x + peak->oldmin.x) / 2;
        // short shreshold_x =
        if (slid->old_sample.x > threshold_x && slid->new_sample.x < threshold_x)
        {
            step_cnt++;
        }
        break;
    }
    case MOST_ACTIVE_Y:
    {
        short threshold_y = (peak->oldmax.y + peak->oldmin.y) / 2;

        //  short threshold_y = 1050;
        if (slid->old_sample.y > threshold_y && slid->new_sample.y < threshold_y)
        {
            step_cnt++;
        }
        break;
    }
    case MOST_ACTIVE_Z:
    {
        short threshold_z = (peak->oldmax.z + peak->oldmin.z) / 2;
        if (slid->old_sample.z > threshold_z && slid->new_sample.z < threshold_z)
        {
            step_cnt++;
        }
        break;
    }
    default:
        break;
    }
}

uint8_t get_step(){
    return step_cnt;
}