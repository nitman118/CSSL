﻿using CSSL.Examples.DataCenterSimulation;
using CSSL.Examples.DataCenterSimulation.DataCenterObservers;
using CSSL.Modeling;
using CSSL.Utilities.Distributions;
using System;
using System.IO;

namespace DataCenterSimulation
{
    class Program
    {
        static void Main(string[] args)
        {
            Simulation sim = new Simulation("SomeSimulation", @"C:\CSSLtest");

            // The model part...

            DataCenter dataCenter = new DataCenter(sim.MyModel, "DataCenter");

            int numberServerpools = 10;
            for (int i = 0; i < numberServerpools; i++)
            {
                dataCenter.AddServerpool(new Serverpool(dataCenter, $"Serverpool_{i}"));
            }

            double dispatchTime = 1E-3;
            Dispatcher dispatcher = new Dispatcher(dataCenter, "Dispatcher", new ExponentialDistribution(1, 1), 2, dataCenter.ServerPools, dispatchTime);
            dataCenter.SetDispatcher(dispatcher);

            double lambda = 100;
            JobGenerator jobGenerator = new JobGenerator(dataCenter, "JobGenerator", new ExponentialDistribution(1 / lambda, 1 / lambda / lambda), dispatcher);
            dataCenter.SetJobGenerator(jobGenerator);

            // The experiment part...

            sim.MyExperiment.NumberOfReplications = 1;
            sim.MyExperiment.MaxComputationalTimeTotal = 60;

            // The observer part...
            DispatcherObserver dispatcherObserver = new DispatcherObserver();
            dispatcherObserver.Subscribe(dataCenter.Dispatcher);

            dispatcherObserver

            sim.Run();

            Console.WriteLine("Test");
        }
    }
}
