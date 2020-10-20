using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Lab1 {
    abstract class V2Data {
        public string Info { get; set; }
        public double EM_freq { get; set; }

        public V2Data(string x, double y) {
            Info = x;
            EM_freq = y;
        }

        public abstract Complex[] NearAverage(float eps);
        public abstract string ToLongString();

        public override string ToString() {
            return $"Info\nElectromagnetic field frequency = {EM_freq}\n";
        }
    }
}
