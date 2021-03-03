using Microsoft.AspNetCore.Mvc;
using Prometheus.Client;

namespace CoreWebWithoutExtensions_3._1.Controllers
{
    [Route("[controller]")]
    public class CounterController : Controller
    {
        private readonly ICounter _counter;
        private readonly IMetricFamily<ICounter> _counterFamily;
        private readonly IMetricFamily<ICounter, (string Controller, string Action)> _counterFamilyTuple;

        #region my test code
        private readonly IMetricFactory _metricFactory;
        private readonly IMetricFamily<ICounter, (string numberOne, string numberTwo, string numberThree)> _counterFamilyTuple3;
        #endregion my test code

        public CounterController(IMetricFactory metricFactory)
        {
            _counter = metricFactory.CreateCounter("my_counter", "some help about this");
            _counterFamily = metricFactory.CreateCounter("my_counter_ts", "some help about this", true, "label1", "label2");
            _counterFamilyTuple = metricFactory.CreateCounter("my_counter_tuple", "some help about this", ("Controller", "Action"), true);

            #region my test code
            _counterFamilyTuple3 = metricFactory.CreateCounter("my_3_part_tuple", "3 part tuple test", ("firstLabel", "secondLabel", "thirdLabel"), true);
            _metricFactory = metricFactory;
            #endregion my test code
        }

        [HttpGet]
        public IActionResult Get()
        {
            _counter.Inc();
            _counterFamily.WithLabels("value1", "value2").Inc(3);
            _counterFamilyTuple.WithLabels(("Counter", "Get")).Inc(5);

            #region my test code
            _metricFactory.CreateGauge("gauge_without_timestamp", "A gauge WITHOUT a timestamp", false)
                .Set(17);

            _metricFactory.CreateGauge("gauge_with_timestamp", "A gauge WITH a timestimestamp", true)
                .Set(31);

            _counterFamilyTuple3.RemoveLabelled(("removeThree", "removeTwo", "removeOne"));
            #endregion my test code

            return Ok();
        }
    }
}
