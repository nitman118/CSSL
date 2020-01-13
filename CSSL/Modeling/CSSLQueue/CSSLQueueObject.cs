﻿using CSSL.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSSL.Modeling.CSSLQueue
{
    /// <summary>
    /// The CSSLQueueObject serves as a base class for object that need to be placed in a CSSLQueue. A CSSLQueueObject can be in one and only one CSSLQueue at the time. 
    /// </summary>
    public class CSSLQueueObject : IIdentity, IComparable<CSSLQueueObject>
    {
        /// <summary>
        /// The simulation time at which the CSSLQueueObject was created.
        /// </summary>
        public double CreationTime { get; }

        public CSSLQueueObject(double creationTime)
        {
            CreationTime = creationTime;
        }

        /// <summary>
        /// The current CSSLQueue that the CSSLQueueObject is in. Is null when the CSSLQueueObject is not in a queue.
        /// </summary>
        public CSSLQueue<CSSLQueueObject> MyQueue { get; }

        /// <summary>
        /// The identity of the CSSLQueueObject.
        /// </summary>
        public int Id { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(CSSLQueueObject other)
        {
            throw new NotImplementedException();
        }
    }
}