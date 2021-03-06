﻿using System;
using Prometheus.Client;
using Prometheus.Client.Collectors;
using Prometheus.Client.MetricServer;

namespace ConsoleMetricServer_3._1
{
    class Program
    {
        static void Main()
        {
            var options = new MetricServerOptions
            {
                Port = 9091
            };
            var registry = new CollectorRegistry();
            var factory = new MetricFactory(registry);


            IMetricServer metricServer = new MetricServer(registry, options);
            metricServer.Start();

            var counter = factory.CreateCounter("test_count", "helptext");
            counter.Inc();

            Console.WriteLine("Press any key..");
            Console.ReadKey();
            metricServer.Stop();
        }
    }
}
