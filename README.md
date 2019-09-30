# CustomHzFont
PC software only support HZ16*16,change it as you wish.

mcu test code
```
typedef struct
{
    uint8_t Index[2];   //汉字内码索引
    uint8_t Code[32];   //点阵数据
}GB16_Font_t;

const GB16_Font_t GB_16[] = 
{
/*插入字库数据*/
};

int16_t findHzIndex(char *hz)                     /* 在自定义汉字库在查找所要显示的汉字的位置 */                                                    
{
    for(uint8_t i = 0; i < sizeof(GB_16)/sizeof(GB_16[0]); i ++)
    {
        //先用遍历
        if( (hz[0] == GB_16[i].Index[0])&&
            (hz[1] == GB_16[i].Index[1]))
        {
            return i;
        }
    }
    
    return -1;
}
```
