using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Primitives;
using Prometheus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WhyNotEarth.Meredith.App.Prometheus
{
    public class PrometheusMetrics
    {
        public static void SetupMetrics(IApplicationBuilder app)
        {
            var counter = Metrics.CreateCounter("PathCounter", "Counts requests to endpoints", new CounterConfiguration
            {
                LabelNames = new[] { "method", "endpoint", "referer" }
            });

            app.Use((context, next) =>
            {
                context.Request.Headers.TryGetValue("Referer", out StringValues values);
                counter.WithLabels(context.Request.Method, context.Request.Path, values.FirstOrDefault()).Inc();

                return next();
            });
        }
    }
}
