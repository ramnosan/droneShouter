using System;
using System.Security.Cryptography;

namespace NeuralNet.NeuralNet
{
    public class CryptoRandom
    {
        public double RandomValue { get; set; }
        Random random = new Random();
        public CryptoRandom()
        {
            using (RNGCryptoServiceProvider p = new RNGCryptoServiceProvider())
            {
                Random r = new Random(p.GetHashCode());
                //this.RandomValue = r.NextDouble();
                this.RandomValue = r.Next(-1000, 1000);
                
                //this.RandomValue = random.Next(-2000, 2000);
            }
        }

    }
}
