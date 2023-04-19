using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace STMTests.Controllers
{
    [TestClass]
    internal class STMControllerTests
    {
        STM.Controllers.STMController? _controller;
        STM.STM _stm = new STM.STM()
        {
            DurationTotal = 0,
            DurationWalk = 0,
            DurationBus = 0,
            DurationMetro = 0,
            DurationTrain = 0
        };
        String origin = "";
        String destination = "";
            [TestMethod]
            public void Get_WhenCalled_ReturnsNotFound()
            {
                // Act
                var notFoundResult = _controller.Get(origin, destination);
            // Assert
            Assert.Inconclusive();
            }
            [TestMethod]
            public void Get_ResultsWhenCalled_ReturnsZeroOrGreater()
            {
                // Act
                var zeroResult = _controller.Get(origin, destination);
                // Assert
                Assert.Equals(_stm.DurationTotal, zeroResult.Result.DurationTotal);
                Assert.Equals(_stm.DurationWalk, zeroResult.Result.DurationWalk);
                Assert.Equals(_stm.DurationBus, zeroResult.Result.DurationBus);
                Assert.Equals(_stm.DurationMetro, zeroResult.Result.DurationMetro);
                Assert.Equals(_stm.DurationTrain, zeroResult.Result.DurationTrain);
            }
    }

}
