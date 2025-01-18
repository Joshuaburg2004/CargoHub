using static Abstracta.JmeterDsl.JmeterDsl;

namespace PerformanceTests
{
    public class ClientPerformanceTestV2 : BaseTest
    {
        public string requestUri = $"{RequestUri}/api/v2/clients";
        [Fact]
        public void GetAllPerformanceTest()
        {
            var stats = TestPlan(
                ThreadGroup(2, 10,
                    HttpSampler(requestUri).Method(HttpMethod.Get.Method).Header("API_KEY", API_KEY)
                ),
                //this is just to log details of each request stats
                JtlWriter("client.jtls")
            // .WriteAllFields() for more information

            ).Run();
            Assert.True(stats.Overall.SampleTimePercentile99 < TimeSpan.FromSeconds(5));
        }

        [Fact]
        public void GetOne10Users2RequestsPerformanceTest()
        {
            var stats = TestPlan(
                ThreadGroup(10, 2,
                    HttpSampler(requestUri).Method(HttpMethod.Get.Method).Header("API_KEY", API_KEY).Param("id", "1")
                ),
                //this is just to log details of each request stats
                JtlWriter("client.jtls")
            // .WriteAllFields() for more information

            ).Run();
            Assert.True(stats.Overall.SampleTimePercentile99 < TimeSpan.FromSeconds(5));
        }

        [Fact]
        public void GetOrders10Users2RequestsPerformanceTest()
        {
            var stats = TestPlan(
                ThreadGroup(10, 2,
                    HttpSampler($"{requestUri}/1/orders").Method(HttpMethod.Get.Method).Header("API_KEY", API_KEY)
                ),
                //this is just to log details of each request stats
                JtlWriter("client.jtls")
            // .WriteAllFields() for more information

            ).Run();
            Assert.True(stats.Overall.SampleTimePercentile99 < TimeSpan.FromSeconds(5));
        }
    }
}