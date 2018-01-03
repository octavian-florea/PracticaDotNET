using NUnit.Framework;
using NSubstitute;
using System;
using Practica.Core;
using Practica.Service;
using System.Collections.Generic;

namespace Practica.Service.Tests
{
    [TestFixture]
    public class IntershipServiceTests
    {
        private IIntershipRepository _intershipRepository;

        [SetUp]
        public void SetUp()
        {
            _intershipRepository = Substitute.For<IIntershipRepository>();
        }

        [Test]
        public void TestGetIntershipById()
        {
            // arrange
            var id = 1;
            var intershipService = new InternshipService(_intershipRepository);
            var intershipRepositoryResult = getIntershipRepositoryResult();
            _intershipRepository.Get(id).Returns(intershipRepositoryResult);

            // act
            var result = intershipService.GetById(id);

            // assert
            Assert.AreEqual(expected: intershipRepositoryResult, actual: result);
        }

        private Internship getIntershipRepositoryResult()
        {
            //return new Internship("1", "Junior Java", "This is a junior java position", new DateTime(), new DateTime());
            return new Internship();
        }

    }
}