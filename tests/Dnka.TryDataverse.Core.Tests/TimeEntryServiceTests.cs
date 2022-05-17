using Dnka.TryDataverse.Core.Contract;
using Dnka.TryDataverse.Core.Model;
using Dnka.TryDataverse.Core.Service;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Dnka.TryDataverse.Core.Tests
{
    public class TimeEntryServiceTests
    {
        private readonly Mock<ITimeEntryRepository> _timeEntryRepo = new Mock<ITimeEntryRepository>();
        static TimeEntryEntity newTimeEntryEntity = new TimeEntryEntity { Start = new DateTime(2023, 1, 2, 12, 12, 12) };

        private readonly TimeEntryService _timeEntryService;

        public TimeEntryServiceTests()
        {
            _timeEntryRepo
                .Setup(x => x.GetAllAsync())
                .ReturnsAsync(GetExistingItems());

            _timeEntryRepo
                .Setup(x => x.CreateAsync(It.IsAny<TimeEntryEntity>()))
                .ReturnsAsync(new TimeEntryEntity());

            _timeEntryService = new TimeEntryService(_timeEntryRepo.Object);
        }

        [Fact]
        public async Task CreateTimeEntriesAsync_WhenExistOneDuplidate_ShouldReturnOneId()
        {
            //Arrange
            List<TimeEntryEntity> newEntities = GetNewItems();

            //Act
            IEnumerable<Guid> actualIds = await _timeEntryService.CreateTimeEntriesAsync(newEntities);

            //Act
            var expectedItemCount = 1;
            Assert.Equal(expectedItemCount, actualIds.Count());
        }

        [Fact]
        public async Task CreateTimeEntriesAsync_WhenExistOneDuplidate_ShouldCreateOneItem()
        {
            //Arrange
            List<TimeEntryEntity> newEntities = GetNewItems();

            //Act
            IEnumerable<Guid> actualIds = await _timeEntryService.CreateTimeEntriesAsync(newEntities);

            //Act
            _timeEntryRepo.Verify(x => 
                x.CreateAsync(It.Is<TimeEntryEntity>(entity => entity.Start == newTimeEntryEntity.Start))
            );
        }


        private static List<TimeEntryEntity> GetExistingItems()
        {
            return new List<TimeEntryEntity>
                {
                    new TimeEntryEntity
                    {
                        Start = new DateTime(2023, 1, 1, 1 , 1, 1),
                    }
                };
        }

        private static List<TimeEntryEntity> GetNewItems()
        {
            return new List<TimeEntryEntity>
            {
                new TimeEntryEntity { Start = new DateTime(2023, 1, 1, 13, 13, 0) },
                newTimeEntryEntity,
            };
        }
    }
}
