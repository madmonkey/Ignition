using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ignition.Common.Data
{
    static public class GuidComb
    {
        static public Guid GenerateSequential()
        {
            byte[] destinationArray = Guid.NewGuid().ToByteArray();
            var time = new DateTime(0x76c, 1, 1);
            DateTime now = DateTime.Now;
            var span = new TimeSpan(now.Ticks - time.Ticks);
            TimeSpan timeOfDay = now.TimeOfDay;
            byte[] bytes = BitConverter.GetBytes(span.Days);
            byte[] array = BitConverter.GetBytes((long)(timeOfDay.TotalMilliseconds / 3.333333));
            Array.Reverse(bytes);
            Array.Reverse(array);
            Array.Copy(bytes, bytes.Length - 2, destinationArray, destinationArray.Length - 6, 2);
            Array.Copy(array, array.Length - 4, destinationArray, destinationArray.Length - 4, 4);
            return new Guid(destinationArray);
        }
    }
}
