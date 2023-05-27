﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MicroNano3DPrinter
{
    class Utilities
    {
        public static double currentToPressure(double current)
        {
            return (current * 0.04987 - 0.1841);
        }

        public static double pressureTocurrent(double pressure)
        {
            return (pressure + 0.1841) / 0.04987;
        }

        public static UploadInfo hexStrToInfo(string str)//将16进制的帧字符串转换为UploadInfo
        {
            string[] tempData = str.Split(' ');

            int[] intData = new int[tempData.Length - 3];
            for (int i = 0; i < tempData.Length - 3; i++)
            {
                intData[i] = ushort.Parse(Convert.ToInt32(tempData[i], 16).ToString());
            }

            UploadInfo info = new UploadInfo();
            info.CurChannelNum = int.Parse(tempData[1], System.Globalization.NumberStyles.HexNumber);
            info.xLocation = int.Parse(tempData[5] + tempData[4] + tempData[3] + tempData[2], System.Globalization.NumberStyles.HexNumber);
            info.yLocation = int.Parse(tempData[9] + tempData[8] + tempData[7] + tempData[6], System.Globalization.NumberStyles.HexNumber);
            info.zLocation = int.Parse(tempData[13] + tempData[12] + tempData[11] + tempData[10], System.Globalization.NumberStyles.HexNumber);
            info.sysStatus = tempData[17];//14、15、16没用上
            info.Speed = int.Parse(tempData[19] + tempData[18], System.Globalization.NumberStyles.HexNumber);
            info.OpenTime = int.Parse(tempData[21] + tempData[20], System.Globalization.NumberStyles.HexNumber);
            info.PrintDistance = int.Parse(tempData[23] + tempData[22], System.Globalization.NumberStyles.HexNumber);
            info.AirPress = int.Parse(tempData[25] + tempData[24], System.Globalization.NumberStyles.HexNumber);
            info.Temperature = int.Parse(tempData[27] + tempData[26], System.Globalization.NumberStyles.HexNumber);
            info.crcByte = tempData[29] + tempData[28];

            int tempCrc = Convert.ToInt32(info.crcByte, 16);
            int crcResult = Crc16_Calc(intData);

            return info;
        }

        public static double[] gcodeToPoint(string gcode)//将GCode指令解析成坐标
        {
            double[] result = new double[2];
            string strX = gcode.Substring(3, 6);
            string strY = gcode.Substring(10, 6);
            result[0] = double.Parse(strX) / 1000;
            result[1] = double.Parse(strY) / 1000;
            return result;
        }

        public static string pointToGCode(string head, double x, double y, double z)//将一个坐标转换成G1指令
        {
            return head + "X" + numToSixString(x * 1000) + "Y" + numToSixString(y * 1000) + "Z" + numToSixString(z * 1000);
        }

        public static string numToSixString(double num)//数字转6位字符
        {
            string str = (int)num + "";
            //保证为6位数
            if (str.Length > 6)
            {
                str = str.Substring(0, 6);
            }
            if (str.Length == 5)
            {
                str = "0" + str;
            }
            else if (str.Length == 4)
            {
                str = "00" + str;
            }
            else if (str.Length == 3)
            {
                str = "000" + str;
            }
            else if (str.Length == 2)
            {
                str = "0000" + str;
            }
            else if (str.Length == 1)
            {
                str = "00000" + str;
            }
            return str;
        }

        static int[] auchCRCHi =
{
0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81,
0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0,
0x80, 0x41, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01,
0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41,
0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81,
0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0,
0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01,
0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40,
0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81,
0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0,
0x80, 0x41, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01,
0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41,
0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81,
0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0,
0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01,
0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41,
0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81,
0x40,
};

        static int[] auchCRCLo =
        {
0x00, 0xC0, 0xC1, 0x01, 0xC3, 0x03, 0x02, 0xC2, 0xC6, 0x06, 0x07, 0xC7, 0x05, 0xC5, 0xC4,
0x04, 0xCC, 0x0C, 0x0D, 0xCD, 0x0F, 0xCF, 0xCE, 0x0E, 0x0A, 0xCA, 0xCB, 0x0B, 0xC9, 0x09,
0x08, 0xC8, 0xD8, 0x18, 0x19, 0xD9, 0x1B, 0xDB, 0xDA, 0x1A, 0x1E, 0xDE, 0xDF, 0x1F, 0xDD,
0x1D, 0x1C, 0xDC, 0x14, 0xD4, 0xD5, 0x15, 0xD7, 0x17, 0x16, 0xD6, 0xD2, 0x12, 0x13, 0xD3,
0x11, 0xD1, 0xD0, 0x10, 0xF0, 0x30, 0x31, 0xF1, 0x33, 0xF3, 0xF2, 0x32, 0x36, 0xF6, 0xF7,
0x37, 0xF5, 0x35, 0x34, 0xF4, 0x3C, 0xFC, 0xFD, 0x3D, 0xFF, 0x3F, 0x3E, 0xFE, 0xFA, 0x3A,
0x3B, 0xFB, 0x39, 0xF9, 0xF8, 0x38, 0x28, 0xE8, 0xE9, 0x29, 0xEB, 0x2B, 0x2A, 0xEA, 0xEE,
0x2E, 0x2F, 0xEF, 0x2D, 0xED, 0xEC, 0x2C, 0xE4, 0x24, 0x25, 0xE5, 0x27, 0xE7, 0xE6, 0x26,
0x22, 0xE2, 0xE3, 0x23, 0xE1, 0x21, 0x20, 0xE0, 0xA0, 0x60, 0x61, 0xA1, 0x63, 0xA3, 0xA2,
0x62, 0x66, 0xA6, 0xA7, 0x67, 0xA5, 0x65, 0x64, 0xA4, 0x6C, 0xAC, 0xAD, 0x6D, 0xAF, 0x6F,
0x6E, 0xAE, 0xAA, 0x6A, 0x6B, 0xAB, 0x69, 0xA9, 0xA8, 0x68, 0x78, 0xB8, 0xB9, 0x79, 0xBB,
0x7B, 0x7A, 0xBA, 0xBE, 0x7E, 0x7F, 0xBF, 0x7D, 0xBD, 0xBC, 0x7C, 0xB4, 0x74, 0x75, 0xB5,
0x77, 0xB7, 0xB6, 0x76, 0x72, 0xB2, 0xB3, 0x73, 0xB1, 0x71, 0x70, 0xB0, 0x50, 0x90, 0x91,
0x51, 0x93, 0x53, 0x52, 0x92, 0x96, 0x56, 0x57, 0x97, 0x55, 0x95, 0x94, 0x54, 0x9C, 0x5C,
0x5D, 0x9D, 0x5F, 0x9F, 0x9E, 0x5E, 0x5A, 0x9A, 0x9B, 0x5B, 0x99, 0x59, 0x58, 0x98, 0x88,
0x48, 0x49, 0x89, 0x4B, 0x8B, 0x8A, 0x4A, 0x4E, 0x8E, 0x8F, 0x4F, 0x8D, 0x4D, 0x4C, 0x8C,
0x44, 0x84, 0x85, 0x45, 0x87, 0x47, 0x46, 0x86, 0x82, 0x42, 0x43, 0x83, 0x41, 0x81, 0x80,
0x40
};

        public static int Crc16_Calc(int[] updata)
        {
            int uchCRCHi = 0xff;
            int uchCRCLo = 0xff;
            for (int i = 0; i < updata.Length; i++)
            {
                int index = uchCRCHi ^ updata[i];
                uchCRCHi = uchCRCLo ^ auchCRCHi[index];
                uchCRCLo = auchCRCLo[index];
            }
            return uchCRCHi << 8 | uchCRCLo;
        }

        public static void SaveObject(string path, object obj)
        {
            IFormatter formatter = new BinaryFormatter();
            try
            {
                Stream stream = new FileStream(@"" + path, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
                formatter.Serialize(stream, obj);
                stream.Close();
                Console.WriteLine("[" + path + "]文件保存成功...");
                //将对象写入到本地
            }
            catch (Exception)
            {
                Console.WriteLine("[" + path + "]文件保存失败...");
            }

        }

        public static T ReadObject<T>(string path)
        {
            try
            {
                IFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream(@"" + path, FileMode.Open, FileAccess.Read, FileShare.None);
                T myObj = (T)formatter.Deserialize(stream);
                stream.Close();
                return myObj;
            }
            catch (Exception)
            {
                if (File.Exists(path))//如果文件不存在，创建文件
                {
                }
                else
                {
                    File.Create(path).Dispose();
                }
            }
            //catch 

            T t = default(T);
            return t;
        }

        public static void SaveText(string path, string content, bool append = false)
        {
            if (append)
            {
                //追加到原来内容的后面
                File.AppendAllText(path, content);
            }
            else
            {
                //覆盖原来的内容
                File.WriteAllText(path, content);
            }
        }

        public static string ReadText(string path)
        {
            string str = File.ReadAllText(path);
            return str;
        }
    }
}