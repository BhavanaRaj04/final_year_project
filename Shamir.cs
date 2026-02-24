using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;

namespace CloudBasedEncryptedCrime
{
    public class Shamir
    {
        Dictionary<double, double> dir = new Dictionary<double, double>();
        ArrayList randomList = null;
        ArrayList xList = null;
        string[] Attribute = null;
        string K;
        public int Polynomial(int x, int SensorId)
        {
            int pol = SensorId;
            for (int i = 1; i < 3; i++)
            {
                pol = pol + (int.Parse(randomList[i - 1].ToString()) * (int.Parse(Math.Pow(double.Parse(x.ToString()), double.Parse(i.ToString())).ToString())));
            }
            return pol;
        }

        public double Lagarange(int index)
        {
            return numerator(index) / denominator(index);
        }

        public double numerator(int index)
        {
            double num = 1.0;
            for (int i = 0; i < int.Parse(K); i++)
            {
                if (i != index)
                {
                    num = num * double.Parse(xList[i].ToString());
                }
            }
            return num;
        }
        public double denominator(int index)
        {
            double den = 1.0;
            for (int i = 0; i < int.Parse(K); i++)
            {
                if (i != index)
                {
                    den = den * (double.Parse(xList[index].ToString()) - double.Parse(xList[i].ToString()));
                }
            }
            return den;
        }

        public string AttributeValue(int key)
        {
            randomList = new ArrayList();
            for (int i = 0; i <= 1; i++)
            {
                Random rnd = new Random();
                int lrnd = rnd.Next(0, 1000);
                randomList.Add(lrnd);
            }
            Attribute = new string[6];
            for (int i = 1; i <= 5; i++)
            {
                Attribute[i] = Polynomial(i, key).ToString();
            }
            string attdata = string.Join(",", Attribute);
            return attdata;
        }
    }
}