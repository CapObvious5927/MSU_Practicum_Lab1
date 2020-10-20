using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Lab1 {
    class V2DataOnGrid : V2Data {
        public Grid1D[] GridData { get; set; }
        public Complex[,] NodeValue { get; set; }

        private int firstDimLen = 0;
        private int secDimLen = 0;

        public V2DataOnGrid(string x, double y, Grid1D param1, Grid1D param2) : base(x, y) {
            GridData = new Grid1D[2];
            GridData[0] = param1;
            GridData[1] = param2;
            NodeValue = new Complex[param1.NodeNum, param2.NodeNum];
            firstDimLen = NodeValue.GetLength(0);
            secDimLen = NodeValue.GetLength(1);
        }

        public void InitRandom(double minValue, double maxValue) {
            Random rnd = new Random();

            for (int i = 0; i < firstDimLen; i++) {
                for (int j = 0; j < secDimLen; j++) {
                    double real = rnd.NextDouble() * (maxValue - minValue) + minValue;
                    double imag = rnd.NextDouble() * (maxValue - minValue) + minValue;
                    NodeValue[i, j] = new Complex(real, imag);
                }
            }
        }

        public override Complex[] NearAverage(float eps) {
            double average = 0;
            int num = 0;

            for (int i = 0; i < firstDimLen; i++) {
                for (int j = 0; j < secDimLen; j++) {
                    num++;
                    average += NodeValue[i, j].Real;
                }
            }
            average /= num;

            int counter = 0;
            int capacity = 16;
            Complex[] result = new Complex[capacity];

            for (int i = 0; i < firstDimLen; i++) {
                for (int j = 0; j < secDimLen; j++) {
                    if (Math.Abs(NodeValue[i, j].Real - average) <= eps) {
                        result[counter] = NodeValue[i, j];
                        counter++;
                    }

                    if (counter == capacity - 2) {
                        capacity *= 2;
                        Array.Resize(ref result, capacity);
                    }
                }

            }
            Array.Resize(ref result, counter);

            return result;
        }

        public override string ToString() {
            return $"Type = V2DataOnGrid\nInfo = {Info}\nElectromagnetic field frequency = {EM_freq}\n" +
                   $"OX GridData: Step = {GridData[0].Step}, Number of Nodes = {GridData[0].NodeNum}\n" +
                   $"OY GridData: Step = {GridData[1].Step}, Number of Nodes = {GridData[1].NodeNum}\n";
        }

        public override string ToLongString() {
            string res = this.ToString();

            for (int i = 0; i < firstDimLen; i++) {
                for (int j = 0; j < secDimLen; j++) {
                    res += $"coord = ({i}, {j}), value = {NodeValue[i, j]}\n";
                }
            }

            return res;
        }

        public static explicit operator V2DataCollection(V2DataOnGrid obj) {
            V2DataCollection res = new V2DataCollection(obj.Info, obj.EM_freq);

            for (int i = 0; i < obj.firstDimLen; i++) {
                for (int j = 0; j < obj.secDimLen; j++) {
                    Vector2 vec = new Vector2((float)i * obj.GridData[0].Step, (float)j * obj.GridData[1].Step);
                    Complex em_field = obj.NodeValue[i, j];
                    res.ListData.Add(new DataItem(vec, em_field));
                }
            }

            return res;
        }
    }
}
