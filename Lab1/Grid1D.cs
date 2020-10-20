using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1 {
    struct Grid1D {
        public float Step { get; set; }
        public int NodeNum { get; set; }

        public Grid1D(float x, int y) {
            Step = x;
            NodeNum = y;
        }

        public override string ToString() {
            return $"Step = {Step}\nNumber of nodes = {NodeNum}\n";
        }
    }
}
