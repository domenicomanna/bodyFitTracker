using Api.Domain.Models;
using Api.Domain.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ApiTests.Domain.ServiceTests
{
    [TestClass]
    public class MeasurementConversionTests
    {
        [TestMethod]
        public void ImperialToMetricLengthTests()
        {
            Assert.AreEqual(12.7, MeasurementConverter.ConvertLength(5, MeasurementSystem.Imperial, MeasurementSystem.Metric), .001);

            Assert.AreEqual(8.89, MeasurementConverter.ConvertLength(3.5, MeasurementSystem.Imperial, MeasurementSystem.Metric), .001);

            Assert.AreEqual(10.16, MeasurementConverter.ConvertLength(4, MeasurementSystem.Imperial, MeasurementSystem.Metric), .001);
        }

        [TestMethod]
        public void MetricToImperialLengthTests()
        {
            Assert.AreEqual(5, MeasurementConverter.ConvertLength(12.7, MeasurementSystem.Metric, MeasurementSystem.Imperial), .001);

            Assert.AreEqual(3.5, MeasurementConverter.ConvertLength(8.89, MeasurementSystem.Metric, MeasurementSystem.Imperial), .001);

            Assert.AreEqual(4, MeasurementConverter.ConvertLength(10.16, MeasurementSystem.Metric, MeasurementSystem.Imperial), .001);
        }

        [TestMethod]
        public void ImperialToMetricWeightTests()
        {
            Assert.AreEqual(6.577, MeasurementConverter.ConvertWeight(14.5, MeasurementSystem.Imperial, MeasurementSystem.Metric), .01);

            Assert.AreEqual(8.164, MeasurementConverter.ConvertWeight(18, MeasurementSystem.Imperial, MeasurementSystem.Metric), .01);

            Assert.AreEqual(44.724, MeasurementConverter.ConvertWeight(98.6, MeasurementSystem.Imperial, MeasurementSystem.Metric), .01);
        }

        [TestMethod]
        public void MetricToImperialWeightTests()
        {
            Assert.AreEqual(14.5, MeasurementConverter.ConvertWeight(6.577, MeasurementSystem.Metric, MeasurementSystem.Imperial), .01);

            Assert.AreEqual(18, MeasurementConverter.ConvertWeight(8.164, MeasurementSystem.Metric, MeasurementSystem.Imperial), .01);

            Assert.AreEqual(98.6, MeasurementConverter.ConvertWeight(44.724, MeasurementSystem.Metric, MeasurementSystem.Imperial), .01);
        }

        [TestMethod]
        public void ConversionBetweenSameSourceAndDestinationTests()
        {
            Assert.AreEqual(18, MeasurementConverter.ConvertWeight(18, MeasurementSystem.Imperial, MeasurementSystem.Imperial), .01);

            Assert.AreEqual(6.577, MeasurementConverter.ConvertWeight(6.577, MeasurementSystem.Metric, MeasurementSystem.Metric), .01);

            Assert.AreEqual(14, MeasurementConverter.ConvertLength(14, MeasurementSystem.Imperial, MeasurementSystem.Imperial), .001);

            Assert.AreEqual(10.16, MeasurementConverter.ConvertLength(10.16, MeasurementSystem.Metric, MeasurementSystem.Metric), .001);
        }
    }
}