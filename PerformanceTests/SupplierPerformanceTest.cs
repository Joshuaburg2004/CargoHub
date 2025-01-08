using static Abstracta.JmeterDsl.JmeterDsl;

namespace PerformanceTests
{
    public class SupplierPerformanceTest : BaseTest
    {
        public string requestUri = $"{RequestUri}/api/v1/suppliers";
        [Fact]
        public void GetAllPerformanceTest()
        {
            var stats = TestPlan(
                ThreadGroup(2, 10,
                    HttpSampler(requestUri).Method(HttpMethod.Get.Method).Header("API_KEY", API_KEY)
                ),
                //this is just to log details of each request stats
                JtlWriter("supplier.jtls")
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
                JtlWriter("supplier.jtls")
            // .WriteAllFields() for more information

            ).Run();
            Assert.True(stats.Overall.SampleTimePercentile99 < TimeSpan.FromSeconds(5));
        }

        [Fact]
        public void GetSupplierItems10Users2RequestsPerformanceTest()
        {
            var stats = TestPlan(
                ThreadGroup(10, 2,
                    HttpSampler($"{requestUri}/1/items").Method(HttpMethod.Get.Method).Header("API_KEY", API_KEY)
                ),
                //this is just to log details of each request stats
                JtlWriter("supplier.jtls")
            // .WriteAllFields() for more information

            ).Run();
            Assert.True(stats.Overall.SampleTimePercentile99 < TimeSpan.FromSeconds(5));
        }
    }
}