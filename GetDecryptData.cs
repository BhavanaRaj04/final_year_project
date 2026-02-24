using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Collections;

namespace CloudBasedEncryptedCrime
{
    public class GetDecryptData
    {
        static ArrayList xList = null;
        static ArrayList yList = null;
        static ArrayList lagrangeList = null;

        double result = 0.0;
        string K, N;
        ArrayList valrnd = new ArrayList();
        public string GetData(string datakey)
        {
            xList = new ArrayList();
            yList = new ArrayList();
            lagrangeList = new ArrayList();
            Shamir objShamir = new Shamir();
            string[] attval = datakey.Split(',');
            Random rnd = new Random();
            for (int i = 0; i <= 2; )
            {
                int v = rnd.Next(1, 6);
                int p = valrnd.IndexOf(v);
                if (p < 0)
                {
                    valrnd.Add(v);
                    i++;

                }
            }
            for (int i = 0; i <= 2; i++)
            {
                xList.Add(valrnd[i]);
                yList.Add(attval[int.Parse(valrnd[i].ToString()) - 1].ToString());

            }
            K = "3";
            for (int i = 0; i < int.Parse(K); i++)
            {
                lagrangeList.Add(Lagarange(i));
            }
            double result = 0.0;
            for (int i = 0; i < int.Parse(K); i++)
            {
                result = result + (double.Parse(yList[i].ToString()) * double.Parse(lagrangeList[i].ToString()));
            }
            if (result < 0)
            {
                result = result * -1.0;
            }
            result = Math.Round(result, MidpointRounding.AwayFromZero);
            return result.ToString();
        }
        public double Lagarange(int index)
        {
            return numerator(index) / denominator(index);
        }

        public double numerator(int index)
        {
            double num = 1.0;
            K = "3";
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
            K = "3";
            for (int i = 0; i < int.Parse(K); i++)
            {
                if (i != index)
                {
                    den = den * (double.Parse(xList[index].ToString()) - double.Parse(xList[i].ToString()));
                }
            }
            return den;
        }
    }
}