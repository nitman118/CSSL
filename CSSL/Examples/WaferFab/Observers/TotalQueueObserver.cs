﻿using CSSL.Modeling;
using CSSL.Modeling.Elements;
using CSSL.Observer;
using CSSL.Utilities.Statistics;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSSL.Examples.WaferFab.Observers
{
    public class TotalQueueObserver : ModelElementObserverBase
    {
        public TotalQueueObserver(Simulation mySimulation) : base(mySimulation)
        {
            queueLength = new Variable<int>(this);
            queueLengthStatistic = new WeightedStatistic("QueueLength");
        }

        private Variable<int> queueLength;

        private WeightedStatistic queueLengthStatistic;

        protected override void OnUpdate(ModelElementBase modelElement)
        {
            WorkCenter workCenter = (WorkCenter)modelElement;
            queueLength.UpdateValue(workCenter.TotalQueueLength);
            queueLengthStatistic.Collect(queueLength.PreviousValue, queueLength.Weight);

            Writer.Write(workCenter.GetTime + "\t" + workCenter.GetWallClockTime + "\t" + queueLength.Value);
        }

        protected override void OnWarmUp(ModelElementBase modelElement)
        {
        }

        protected override void OnInitialized(ModelElementBase modelElement)
        {
            Writer.Write("Simulation Time\tComputational Time\tQueue Length");

            WorkCenter workCenter = (WorkCenter)modelElement;
            queueLength.UpdateValue(workCenter.TotalQueueLength);
        }

        protected override void OnReplicationStart(ModelElementBase modelElement)
        {
            queueLength.Reset();
            // Uncomment below if one want to save across replication statistics
            queueLengthStatistic.Reset();
        }

        protected override void OnReplicationEnd(ModelElementBase modelElement)
        {
        }

        public override void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        protected override void OnExperimentStart(ModelElementBase modelElement)
        {
        }

        protected override void OnExperimentEnd(ModelElementBase modelElement)
        {
        }
    }
}
