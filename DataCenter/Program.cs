﻿using CSSL.Examples.DataCenterSimulation;
using CSSL.Examples.DataCenterSimulation.DataCenterObservers;
using CSSL.Modeling;
using CSSL.Reporting;
using CSSL.Utilities.Distributions;
using System;
using System.IO;
using System.Linq;

namespace DataCenterSimulation
{
    class Program
    {
        static void Main(string[] args)
        {
            Simulation sim = new Simulation("SomeSimulation", @"C:\CSSLtest");

            // Parameters...

            double dispatchTime = 1E-3;
            double lambda = 100;
            int numberServerpools = 10;
            int numberServerpoolsToChooseFrom = 10;

            // The model part...

            DataCenter dataCenter = new DataCenter(sim.MyModel, "DataCenter");

            for (int i = 0; i < numberServerpools; i++)
            {
                dataCenter.AddServerpool(new ServerPool(dataCenter, $"Serverpool_{i}"));
            }

            Dispatcher dispatcher = new Dispatcher(dataCenter, "Dispatcher", new ExponentialDistribution(1, 1), 2, dataCenter.ServerPools, dispatchTime, numberServerpoolsToChooseFrom);
            dataCenter.SetDispatcher(dispatcher);

            JobGenerator jobGenerator = new JobGenerator(dataCenter, "JobGenerator", new ExponentialDistribution(1 / lambda, 1 / lambda / lambda), dispatcher);
            dataCenter.SetJobGenerator(jobGenerator);

            // The experiment part...

            sim.MyExperiment.NumberOfReplications = 3;
            sim.MyExperiment.MaxComputationalTimePerReplication = 10;

            // The observer part...
            DispatcherObserver dispatcherObserver = new DispatcherObserver(sim);
            dispatcherObserver.Subscribe(dataCenter.Dispatcher);

            DataCenterObserver dataCenterObserver = new DataCenterObserver(sim);
            dataCenterObserver.Subscribe(dataCenter.Dispatcher);

            foreach(ServerPool serverpool in dataCenter.ServerPools)
            {
                ServerPoolObserver serverpoolObserver = new ServerPoolObserver(sim);
                serverpoolObserver.Subscribe(serverpool);
            }

            sim.TryRun();

            SimulationReporter reporter = sim.MakeSimulationReporter();

            reporter.PrintSummaryToFile();
            reporter.PrintSummaryToConsole();

            Console.WriteLine("Test");
        }
    }
}
