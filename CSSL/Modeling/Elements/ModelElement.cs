﻿using CSSL.Observer;
using CSSL.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSSL.Modeling.Elements
{
    public abstract class ModelElement : IIdentity, IName, IObservable<object>
    {
        /// <summary>
        /// Incremented to store the total number of created model elements.
        /// </summary>
        private static int modelElementCounter;

        public int Id { get; private set; }

        public string Name { get; private set; }

        /// <summary>
        /// This constuctor called to construct any ModelElement.
        /// </summary>
        /// <param name="parent">A reference to the parent model element.</param>
        /// <param name="name">The name of the model element.</param>
        public ModelElement(ModelElement parent, string name)
        {
            ConstructorCalls(name);
            this.parent = parent ?? throw new ArgumentNullException($"Tried to construct ModelElement with name \"{name}\" but the parent ModelElement is null.");
            parent.AddModelElement(this);
            myModel = parent.GetModel();
        }

        /// <summary>
        /// This constuctor is only called by Model so that Model does not require a parent model element.
        /// </summary>
        /// <param name="name">The name of the model.</param>
        public ModelElement(string name)
        {
            ConstructorCalls(name);
        }

        /// <summary>
        /// Default constructor calls.
        /// </summary>
        /// <param name="name">The name of the model.</param>
        private void ConstructorCalls(string name)
        {
            Name = name;
            Id = modelElementCounter++;
            modelElements = new List<ModelElement>();
        }

        /// <summary>
        /// A reference to the parent model element.
        /// </summary>
        protected ModelElement parent;

        /// <summary>
        /// Retrieves the main model. The highest container for all model elements.
        /// </summary>
        /// <returns></returns>
        protected virtual Model GetModel()
        {
            return parent.GetModel();
        }

        /// <summary>
        /// A reference to the overall model.
        /// </summary>
        protected Model myModel;

        /// <summary>
        /// Retrieves the executive.
        /// </summary>
        /// <returns></returns>
        protected Executive GetExecutive()
        {
            return myModel.Executive;
        }

        /// <summary>
        /// Retrieves the current simulation time.
        /// </summary>
        /// <returns></returns>
        protected double GetTime()
        {
            return GetExecutive().Time;
        }

        /// <summary>
        /// 
        /// </summary>
        private List<ModelElement> modelElements;

        /// <summary>
        /// Adds the supplied model element as a child to this model element. 
        /// </summary>
        /// <param name="modelElement">The model element to be added.</param>
        private void AddModelElement(ModelElement modelElement)
        {
            modelElements.Add(modelElement);
        }

        /// <summary>
        /// Removes the supplied child model element from this model element.
        /// </summary>
        /// <param name="modelElement">The model element to be removed.</param>
        private void RemoveModelElement(ModelElement modelElement)
        {
            modelElements.Remove(modelElement);
        }

        /// <summary>
        /// Changes the parent model element of this model element to the supplied model element. 
        /// </summary>
        /// <param name="newParent">The parent for this model element.</param>
        protected void ChangeParentModelElement(ModelElement newParent)
        {
            ModelElement oldParent = parent;
            if (oldParent != newParent)
            {
                oldParent.RemoveModelElement(this);
                newParent.AddModelElement(this);
                myModel = newParent.GetModel();
            }
        }

        /// <summary>
        /// This method should be overridden by derived classes that need to perform initialization actions. It is called once before each replication. 
        /// </summary>
        protected virtual void Initialize()
        {
        }

        private List<IObserver<object>> observers;

        public IDisposable Subscribe(IObserver<object> observer)
        {
            // Check whether observer is already registered. If not, add it.
            if (!observers.Contains(observer))
            {
                observers.Add(observer);
            }
            return new Unsubscriber<IObserver<object>>(observers, observer);
        }

        private void NotifyObservers(object info)
        {
            foreach (IObserver<object> observer in observers)
            {
                observer.OnNext(info);
            }
        }

        private class Unsubscriber<IObserver> : IDisposable
        {
            private List<IObserver> observers;
            private IObserver observer;

            internal Unsubscriber(List<IObserver> observers, IObserver observer)
            {
                this.observers = observers;
                this.observer = observer;
            }

            public void Dispose()
            {
                if (observers.Contains(observer))
                {
                    observers.Remove(observer);
                }
            }
        }
    }
}