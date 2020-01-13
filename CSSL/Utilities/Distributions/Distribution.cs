﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSSL.Utilities.Distributions
{
    public abstract class Distribution
    {
        protected ExtendedRandom rnd;

        public double Mean { get; }

        public double Variance { get; }

        public Distribution(double mean, double variance)
        {
            Mean = mean;
            Variance = variance;
        }

        public abstract double Next();
    }
}