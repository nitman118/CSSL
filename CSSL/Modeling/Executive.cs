﻿using CSSL.Calendar;
using CSSL.Modeling.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CSSL.Modeling.Elements.CSSLEvent;

namespace CSSL.Modeling
{
    public class Executive
    {
        public double Time { get; private set; }

        private ICalendar calendar;

        private EventExecutionProcess eventExecutionProcess;

        public Executive()
        {
            calendar = new SimpleCalendar();
        }

        public Executive(ICalendar calendar)
        {
            this.calendar = calendar;
        }

        public void TryInitialize()
        {
            eventExecutionProcess.TryInitialize();
        }

        public void Execute(CSSLEvent e)
        {
            Time = e.Time;
            e.Execute();
        }

        internal void ScheduleEvent(double time, CSSLEventAction action)
        {
            CSSLEvent e = new CSSLEvent(time, action);
            calendar.Add(e);
        }

        private class EventExecutionProcess : IterativeProcess<CSSLEvent>
        {
            private Executive executive;

            public EventExecutionProcess(Executive executive) : base()
            {
                this.executive = executive;
            }

            protected override bool HasNext => executive.calendar.HasNext();

            protected sealed override void DoInitialize()
            {
                base.DoInitialize();
                executive.calendar.CancelAll();
                executive.Time = 0;
            }

            protected sealed override CSSLEvent NextIteration()
            {
                return executive.calendar.Next();
            }

            protected sealed override void RunIteration()
            {
                CSSLEvent e = NextIteration();
                executive.Execute(e);
            }
        }
    }
}
