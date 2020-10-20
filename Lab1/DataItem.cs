using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Lab1 {
    struct DataItem {
        public Vector2 Coord { get; set; }
        public Complex EM_field { get; set; }

        public DataItem(Vector2 x, Complex y) {
            Coord = x;
            EM_field = y;
        }

        public override string ToString() {
            return $"Coord = ({Coord.X}, {Coord.Y})\nEM_field = {EM_field}\n";
        }

    }
}
