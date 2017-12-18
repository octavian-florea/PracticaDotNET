using NUnit.Framework;
using NSubstitute;
using System;
using Practica.Core;
using Practica.Service;
using System.Collections.Generic;

namespace Practica.Service.Tests
{
    [TestFixture]
    public class ActivityServiceTests
    {
        private IActivityQueryRepository _activityQueryRepository;

        [SetUp]
        public void SetUp()
        {
            _activityQueryRepository = Substitute.For<IActivityQueryRepository>();
        }

        [Test]
        public void TestActivityFilter()
        {
            // arrange
            var activityService = new ActivityService(_activityQueryRepository);
            ActivityFilter activityFilter = new ActivityFilter("");
            var activityQueryRepositoryResult = getActivityQueryRepositoryResult();
            _activityQueryRepository.Find(activityFilter).Returns(activityQueryRepositoryResult);

            // act
            var result = activityService.Find(activityFilter);

            // assert
            Assert.AreEqual(2, result.Count);
        }

        private List<Activity> getActivityQueryRepositoryResult()
        {
            return new List<Activity>
            {
                new Activity("1", "Junior Java", "This is a junior java position", new DateTime(), new DateTime()),
                new Activity("2", "Junior C#", "This is a junior c# position", new DateTime(), new DateTime())
            };
        }

    }
}