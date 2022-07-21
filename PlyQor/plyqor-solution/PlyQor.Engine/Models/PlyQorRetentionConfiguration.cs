using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlyQor.Engine.Models
{
    public class PlyQorRetentionConfiguration
    {
        public int Day { get; }

        public int Size { get; }

        public int Cooldown { get; }

        public int Trace { get; }

        public PlyQorRetentionConfiguration(int day, int size, int cooldown, int trace)
        {
            Day = day;
            Size = size;
            Cooldown = cooldown;
            Trace = trace;
        }
    }
}
