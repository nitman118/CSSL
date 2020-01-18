﻿using CSSL.Modeling;
using CSSL.Modeling.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSSL.Examples.DataCenterSimulation
{
    public class DataCenter : ModelElement
    {
        public JobGenerator JobGenerator { get; private set; }

        public  Dispatcher Dispatcher { get; private set; }

        public List<Serverpool> ServerPools { get; private set; }

        public DataCenter(ModelElement parent, string name) : base(parent, name)
        {
        }

        public void SetJobGenerator(JobGenerator jobGenerator)
        {
            JobGenerator = jobGenerator;
        }

        public void SetDispatcher(Dispatcher dispatcher)
        {
            Dispatcher = dispatcher;
        }

        public void AddServerpool(Serverpool serverPool)
        {
            if (ServerPools == null)
            {
                ServerPools = new List<Serverpool>();
            }
            ServerPools.Add(serverPool);
        }
    }
}