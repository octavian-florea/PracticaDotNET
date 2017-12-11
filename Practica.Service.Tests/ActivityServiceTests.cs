using System;
using NSubstitute;
using NUnit.Framework;
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
        public void Filter()
        {
            // arrange
            var activityService = new ActivityService(_activityQueryRepository);
            Dictionary<String, String> filters = new Dictionary<String, String>();
            var activityQueryRepositoryResult = getActivityQueryRepositoryResult();
            _activityQueryRepository.Find(filters).Returns(activityQueryRepositoryResult);

            // act
            var result = activityService.Find(filters);

            // assert
            Assert.AreEqual(2, result.Count);
        }

        private List<Activity> getActivityQueryRepositoryResult()
        {
            return new List<Activity>
            {
                new Activity(1, "Junior Java", "This is a junior java position", new DateTime(), new DateTime()),
                new Activity(2, "Junior C#", "This is a junior c# position", new DateTime(), new DateTime())
            };
        }

    }
}