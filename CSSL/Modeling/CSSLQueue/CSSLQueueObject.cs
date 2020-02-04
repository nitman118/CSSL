﻿using CSSL.Modeling.Elements;
using CSSL.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSSL.Modeling.CSSLQueue
{
    /// <summary>
    /// The CSSLQueueObject serves as a class for objects that need to be placed in a CSSLQueue. A CSSLQueueObject can be in one and only one CSSLQueue at the time. 
    /// </summary>
    /// <typeparam name="T">Refers to its own type. E.g. when a class "job" extends CSSLQueueObject then T is "job".</typeparam>
    public class CSSLQueueObject<T> : IIdentity, IComparable<T> where T : CSSLQueueObject<T>
    {
        /// <summary>
        /// The simulation time at which the CSSLQueueObject was created.
        /// </summary>
        public double CreationTime { get; }

        public CSSLQueueObject(double creationTime)
        {
            CreationTime = creationTime;
        }

        public CSSLQueueObject(double creationTime, int id)
        {
            CreationTime = creationTime;
            Id = id;
        }

        /// <summary>
        /// The current CSSLQueue that the CSSLQueueObject is in. Is null when the CSSLQueueObject is not in a queue.
        /// </summary>
        public CSSLQueue<T> MyQueue { get; internal set; } 

        /// <summary>
        /// The identity of the CSSLQueueObject.
        /// </summary>
        public int Id { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(T other)
        {
            throw new NotImplementedException();
        }
    }
}
