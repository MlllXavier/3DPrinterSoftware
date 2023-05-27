using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroNano3DPrinter
{
    class UploadInfo
    {
        public int CurChannelNum; // 当前打印通道编号
        public double xLocation; // x 轴当前位置
        public double yLocation; // y 轴当前位置
        public double zLocation; // z 轴当前位置
        public string sysStatus; // 打印机状态
        public double Speed; // 当前通道打印机移动 1um 需要的时间
        public double OpenTime; // 当前通道喷头每次打印开启的时间 PulseWidth
        public double PrintDistance; // 当前通道打印间距（分辨率）Resolution
        public double AirPress; // 当前通道比例阀气压电流值
        public double Temperature; // 当前通道温度
        public string crcByte; // 十六位 CRC 值
    }
}
