using static Abstracta.JmeterDsl.JmeterDsl;

namespace PerformanceTests
{
    public class LocationPerformanceTest : BaseTest
    {
        public string requestUri = $"{RequestUri}/api/v1/locations";
        [Fact]
        public void GetAllPerformanceTest()
        {
            var stats = TestPlan(
                ThreadGroup(2, 10,
                    HttpSampler(requestUri).Method(HttpMethod.Get.Method).Header("API_KEY", API_KEY)
                ),
                //this is just to log details of each request stats
                JtlWriter("location.jtls")
            // .WriteAllFields() for more information

            ).Run();
            Assert.True(stats.Overall.SampleTimePercentile99 < TimeSpan.FromSeconds(5));
        }

        [Fact]
        public void GetOne50Users10RequestsPerformanceTest()
        {
            var stats = TestPlan(
                ThreadGroup(50, 10,
                    HttpSampler(requestUri).Method(HttpMethod.Get.Method).Header("API_KEY", API_KEY).Param("id", "1")
                ),
                //this is just to log details of each request stats
                JtlWriter("location.jtls")
            // .WriteAllFields() for more information

            ).Run();
            Assert.True(stats.Overall.SampleTimePercentile99 < TimeSpan.FromSeconds(5));
        }
    }
}