using ATG.Core.Services;
using ATG.Domain.Models;
using ATG.Infrastructure.Data.Repositories;
using Moq;
using NUnit.Framework;
using System;
using System.Linq;

namespace ATG.Tests.Services
{
    [TestFixture]
    public class FailoverLotServiceTests
    {
        private IFailoverService _sut;

        private Mock<IFailoverLotRepository> _mockFailoverLotRepo;
        private Mock<IConfig> _config;

        [SetUp]
        public void Setup()
        {
            _mockFailoverLotRepo = new Mock<IFailoverLotRepository>();
            _config = new Mock<IConfig>();

            _sut = new FailoverService(_mockFailoverLotRepo.Object, _config.Object);
        }

        [TestCase]
        public void IsFailOverMode_Disabled()
        {
            _config.Setup(config => config.IsFailoverMode).Returns(false);

            var expected = _sut.ShouldProvideFailoverLots();
            
            Assert.IsFalse(expected);

        }

        [TestCase(0, ExpectedResult = false)]
        [TestCase(1, ExpectedResult = false)]
        [TestCase(49, ExpectedResult = false)]
        [TestCase(50, ExpectedResult = false)]
        [TestCase(51, ExpectedResult = true)]
        public bool IsFailOverMode_Enabled(int numberOfEntries)
        {

            _config.Setup(config => config.IsFailoverMode).Returns(true);
            _config.Setup(config => config.FailoverTimeOutPeriodInMinutes).Returns(10);
            _config.Setup(config => config.MaxFailedRequest).Returns(50);
            GenerateFailoverLotEntries(numberOfEntries);

            var expected = _sut.ShouldProvideFailoverLots();

            return expected;

        }

        private void GenerateFailoverLotEntries(int count, double failOverTimeOut = 10)
        {
            var limit = failOverTimeOut + 2;
            _mockFailoverLotRepo.Setup(repo => repo.GetFailOverLotEntries())
                .Returns(Enumerable.Range(0, count).Select(p => new FailoverLots()
                    {
                        DateTime = DateTime.Now.AddMinutes(limit)
                    }).ToList());
        }
    }
}
