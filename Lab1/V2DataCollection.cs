using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Lab1 {
    class V2DataCollection : V2Data {
        public List<DataItem> ListData { get; set; }

        public V2DataCollection(string x, double y) : base(x, y) {
            ListData = new List<DataItem>();
        }

        public void InitRandom(int nItems, float xmax, float ymax, double minValue, double maxValue) {
            Random rnd = new Random();

            for (int i = 0; i < nItems; i++) {
                float x = (float)rnd.NextDouble() * xmax;
                float y = (float)rnd.NextDouble() * (float)ymax;
                double real = rnd.NextDouble() * (maxValue - minValue) + minValue;
                double imag = rnd.NextDouble() * (maxValue - minValue) + minValue;
                Vector2 vec = new Vector2(x, y);
                Complex comp = new Complex(real, imag);
                DataItem tmp = new DataItem(vec, comp);
                ListData.Add(tmp);
            }
        }

        public override Complex[] NearAverage(float eps) {
            double average = 0;
            int num = 0;

            foreach (var item in ListData) {
                num++;
                average += item.EM_field.Real;
            }

            average /= num;

            int counter = 0;
            int capacity = 16;
            Complex[] result = new Complex[capacity];
            foreach (var item in ListData) {
                if (Math.Abs(item.EM_field.Real - average) <= eps) {
                    result[counter] = item.EM_field;
                    counter++;
                }

                if (counter == capacity - 2) {
                    capacity *= 2;
                    Array.Resize(ref result, capacity);
                }
            }

            Array.Resize(ref result, counter);

            return result;
        }


        public override string ToString() {
            return $"Type = V2DataCollection\nInfo = {Info}\nElectromagnetic field frequency = {EM_freq}\n" +
                   $"Number of elems in the List<DataItem> = {ListData.Count}\n";
        }

        public override string ToLongString() {
            string res = this.ToString();

            foreach (var item in ListData) {
                res += $"coord = ({item.Coord.X}, {item.Coord.Y}), value = {item.EM_field}\n";
            }

            return res;
        }
    }

}
