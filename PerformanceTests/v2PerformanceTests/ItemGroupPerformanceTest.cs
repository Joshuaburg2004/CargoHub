using static Abstracta.JmeterDsl.JmeterDsl;

namespace PerformanceTests
{
    public class ItemGroupPerformanceTestV2 : BaseTest
    {
        public string requestUri = $"{RequestUri}/api/v2/item_groups";
        [Fact]
        public void GetAllPerformanceTest()
        {
            var stats = TestPlan(
                ThreadGroup(2, 10,
                    HttpSampler(requestUri).Method(HttpMethod.Get.Method).Header("API_KEY", API_KEY)
                ),
                //this is just to log details of each request stats
                JtlWriter("itemgroup.jtls")
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
                JtlWriter("itemgroup.jtls")
            // for more information
            ).Run();
            Assert.True(stats.Overall.SampleTimePercentile99 < TimeSpan.FromSeconds(5));
        }

        [Fact]
        public void GetLocation10Users2RequestsPerformanceTest()
        {
            var stats = TestPlan(
                ThreadGroup(10, 2,
                    HttpSampler($"{requestUri}/1/items").Method(HttpMethod.Get.Method).Header("API_KEY", API_KEY)
                ),
                //this is just to log details of each request stats
                JtlWriter("itemgroup.jtls")
            ).Run();
            Assert.True(stats.Overall.SampleTimePercentile99 < TimeSpan.FromSeconds(5));
        }
    }
}