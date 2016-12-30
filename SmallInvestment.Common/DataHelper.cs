using Coding4Fun.Toolkit.Controls;
using Microsoft.Phone.Net.NetworkInformation;
using SmallInvestment.Dal;
using System;
using System.Windows;
using System.Windows.Media;

namespace SmallInvestment.Common
{
    public static class DataHelper
    {
        public static int GetInteger(object value)
        {
            int integerValue = 0;

            if (value != null && !Convert.IsDBNull(value))
                int.TryParse(value.ToString(), out integerValue);

            return integerValue;
        }

        public static decimal GetDecimal(object value)
        {
            decimal decimalValue = 0;

            if (value != null && !Convert.IsDBNull(value))
                decimal.TryParse(value.ToString(), out decimalValue);

            return decimalValue;
        }

        public static float GetFloat(object value)
        {
            float floatValue = 0;

            if (value != null && !Convert.IsDBNull(value))
                float.TryParse(value.ToString(), out floatValue);

            return floatValue;
        }


        public static string WbsPassStr()
        {
            return "A6708DB9B33909950CB20BFAFE887245";
        }

        public static object ConvertToRGB(object value)
        {
            byte alpha;
            byte pos = 0;

            string hex = value.ToString().Replace("#", "");

            if (hex.Length == 8)
            {
                alpha = System.Convert.ToByte(hex.Substring(pos, 2), 16);
                pos = 2;
            }
            else
            {
                alpha = System.Convert.ToByte("ff", 16);
            }

            byte red = System.Convert.ToByte(hex.Substring(pos, 2), 16);

            pos += 2;
            byte green = System.Convert.ToByte(hex.Substring(pos, 2), 16);

            pos += 2;
            byte blue = System.Convert.ToByte(hex.Substring(pos, 2), 16);

            return new SolidColorBrush(Color.FromArgb(alpha, red, green, blue));
        }

        public static void PromptTip()
        {
            if (!NetworkInterface.GetIsNetworkAvailable())
            {
                var repository = new EfHelper();
                if (repository.IsExistLocalDb() == false)
                {
                    ToastPrompt toastPrompt1 = new ToastPrompt()
                    {
                        Height = 55,
                        Message = "请您连接 Wi-Fi，小投资需要同步银行利率数据。",
                        VerticalContentAlignment = VerticalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Top,
                        FontSize = 16,
                        TextOrientation = System.Windows.Controls.Orientation.Vertical,
                        Background = (SolidColorBrush)DataHelper.ConvertToRGB("#007ACC"),//#58A9DE
                        Foreground = new SolidColorBrush(Colors.White)

                    };
                    toastPrompt1.Show();
                }
            }
        }




    }
}
