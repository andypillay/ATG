using ATG.Core.Services;
using ATG.Domain.Models;
using ATG.Infrastructure.Data.Repositories;
using ATG.Tests.TestHelper;
using Moq;
using NUnit.Framework;

namespace ATG.Tests.Services
{
    [TestFixture]
    public class LotServiceTests
    {
        private ILotService _sut;

        private Mock<IArchivedRepository> _mockArchivedRepo;
        private Mock<IFailoverLotRepository> _mockFailoverLotRepo;
        private Mock<ILotRepository> _mockLotRepo;
        private Mock<IFailoverService> _failoverService;
        private int lotId = It.IsAny<int>();

        [SetUp]
        public void Setup()
        {
            _mockArchivedRepo = new Mock<IArchivedRepository>();
            _mockFailoverLotRepo = new Mock<IFailoverLotRepository>();
            _mockLotRepo = new Mock<ILotRepository>();
            _failoverService = new Mock<IFailoverService>();

            _sut = new LotService(_mockArchivedRepo.Object,
                _mockFailoverLotRepo.Object,
                _mockLotRepo.Object,
                _failoverService.Object);
        }

        [TestCase]
        public void Get_ArchivedLot_When_IsLotArchived_True()
        {
            var expected = new Lot { Id = lotId, IsArchived = true, Description = LotTypeDescription.Archived };
            _mockArchivedRepo.Setup(repo => repo.GetLot(lotId)).Returns(expected);

            var actual = _sut.GetLot(lotId, true);

            _mockArchivedRepo.Verify(repo => repo.GetLot(lotId), Times.Once());

            Assert.IsNotNull(actual);
            Assert.AreEqual(expected, actual);
        }

        [TestCase]
        public void Get_NonArchivedLot_When_FailoverEnabled()
        {
            var expected = new Lot { Id = lotId, IsArchived = false, Description = LotTypeDescription.Datastore };
            _failoverService.Setup(repo => repo.ShouldProvideFailoverLots()).Returns(true);
            _mockFailoverLotRepo.Setup(repo => repo.GetLot(lotId)).Returns(expected);

            var actual = _sut.GetLot(lotId, false);

            _mockFailoverLotRepo.Verify(repo => repo.GetLot(lotId), Times.Once());
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected, actual);
        }

        [TestCase]
        public void Get_FailoverLot_When_FailoverEnabled()
        {
            var expected = new Lot { Id = lotId, IsArchived = false, Description = LotTypeDescription.Failover };
            _failoverService.Setup(repo => repo.ShouldProvideFailoverLots()).Returns(true);
            _mockFailoverLotRepo.Setup(repo => repo.GetLot(lotId)).Returns(expected);

            var actual = _sut.GetLot(lotId, false);

            _mockFailoverLotRepo.Verify(repo => repo.GetLot(lotId), Times.Once());
            _mockArchivedRepo.Verify(repo => repo.GetLot(lotId), Times.Never());

            Assert.IsNotNull(actual);
            Assert.AreEqual(expected, actual);
        }

        [TestCase]
        public void Get_ArchivedLot_When_FailoverEnabled()
        {
            var expected = new Lot { Id = lotId, IsArchived = true, Description = LotTypeDescription.Archived };
            _failoverService.Setup(repo => repo.ShouldProvideFailoverLots()).Returns(true);
            _mockFailoverLotRepo.Setup(repo => repo.GetLot(lotId)).Returns(new Lot { Id = lotId, IsArchived = true, Description = LotTypeDescription.Failover });
            _mockArchivedRepo.Setup(repo => repo.GetLot(lotId)).Returns(expected);

            var actual = _sut.GetLot(lotId, false);

            _mockFailoverLotRepo.Verify(repo => repo.GetLot(lotId), Times.Once());
            _mockArchivedRepo.Verify(repo => repo.GetLot(lotId), Times.Once());

            Assert.IsNotNull(actual);
            Assert.AreEqual(expected, actual);
        }


        [TestCase]
        public void Get_ArchivedLot_When_FailoverDisabled()
        {
            var expected = new Lot { Id = lotId, IsArchived = true, Description = LotTypeDescription.Archived };
            _failoverService.Setup(repo => repo.ShouldProvideFailoverLots()).Returns(false);
            _mockLotRepo.Setup(repo => repo.LoadCustomer()).Returns(new Lot { IsArchived = true });
            _mockArchivedRepo.Setup(repo => repo.GetLot(lotId)).Returns(expected);

            var actual = _sut.GetLot(lotId, false);

            _mockLotRepo.Verify(repo => repo.LoadCustomer(), Times.Once());
            _mockArchivedRepo.Verify(repo => repo.GetLot(lotId), Times.Once());
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected, actual);
        }

        [TestCase]
        public void Get_NonArchivedLot_When_FailoverDisabled()
        {
            var expected = new Lot { Id = lotId, IsArchived = false, Description = LotTypeDescription.Datastore };
            _failoverService.Setup(repo => repo.ShouldProvideFailoverLots()).Returns(false);
            _mockLotRepo.Setup(repo => repo.LoadCustomer()).Returns(expected);

            var actual = _sut.GetLot(lotId, false);

            _mockLotRepo.Verify(r => r.LoadCustomer(), Times.Once());
            _mockArchivedRepo.Verify(repo => repo.GetLot(lotId), Times.Never());
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected, actual);

        }
    }


}
