﻿using CSSL.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSSL.Modeling.Elements
{
    public class CSSLEvent : IComparable<CSSLEvent>
    {
        public delegate void CSSLEventAction(CSSLEvent csslevent);

        public double Time { get; }

        private CSSLEventAction action { get; }

        public CSSLEvent(double time, CSSLEventAction action)
        {
            Time = time;
            this.action = action;
        }

        public void Execute()
        {
            action.Invoke(this);
        }

        public int CompareTo(CSSLEvent other)
        {
            if (Time < other.Time)
            {
                return -1;
            }

            if (Time > other.Time)
            {
                return 1;
            }

            if (id < other.id)
            {
                return -1;
            }

            if (id > other.id)
            {
                return 1;
            }

            if (ReferenceEquals(this, other))
            {
                return 0;
            }
            else
            {
                throw new Exception("Times and ids were equal, but references were not, in CSSLEvent compareTo.");
            }
        }

        private long id;
    }
}

