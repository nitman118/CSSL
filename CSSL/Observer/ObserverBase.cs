﻿using CSSL.Modeling;
using CSSL.Modeling.Elements;
using CSSL.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CSSL.Observer
{
    /// <summary>
    /// A base class for all CSSL observers
    /// </summary>
    /// <typeparam name="T">The observable object that is passed for notification information.</typeparam>
    public abstract class ObserverBase : IIdentity, IName, IObserver<object>, IDisposable
    {
        /// <summary>
        /// Incremented to store the total number of observers.
        /// </summary>
        private static int observerCounter;

        public ObserverBase(Simulation mySimulation)
        {
            Id = observerCounter++;
            Name = GetType().Name + "_" + Id;
            this.mySimulation = mySimulation;
            mySimulation.MyObservers.Add(this);
            cancellations = new List<Unsubscriber>();
        }

        private readonly Simulation mySimulation;

        internal void StrictlyDoBeforeReplication()
        {
            Writer = new StreamWriter(Path.Combine(mySimulation.MyExperiment.ReplicationOutputDirectory, Name + ".txt"));

            DoBeforeReplication();
        }

        protected virtual void DoBeforeReplication()
        {
        }

        protected StreamWriter Writer { get; private set; }

        protected List<Unsubscriber> cancellations;

        /// <summary>
        /// Subscribes the observer to an observable.
        /// </summary>
        /// <param name="observable">The model element to observe.</param>
        public void Subscribe(IObservable<object> observable)
        {
            cancellations.Add((Unsubscriber)observable.Subscribe(this));
        }

        /// <summary>
        /// Unsubscribe observer from all subscribed observables.
        /// </summary>
        public void UnsubscribeAll()
        {
            foreach (IDisposable cancellation in cancellations)
            {
                cancellation.Dispose();
            }
        }

        /// <summary>
        /// Unsubsribe from specific observable.
        /// </summary>
        /// <param name="observable">The observable to unsubscribe from.</param>
        public void Unsubscribe(IObservable<object> observable)
        {
            cancellations.Where(x => x.observable == observable).First().Dispose();
        }

        public int Id { get; }

        public string Name { get; }

        public virtual void OnCompleted()
        {
            throw new NotImplementedException();
        }

        public virtual void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public virtual void OnNext(object value)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            Writer.Dispose();
        }
    }
}