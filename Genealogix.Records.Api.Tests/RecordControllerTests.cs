using System.Collections.Generic;
using Genealogix.Records.Api.Controllers;
using Genealogix.Records.Api.Models;
using Genealogix.Records.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace Genealogix.Records.Api.Tests
{
    [TestClass]
    public class RecordControllerTests
    {
        private RecordController _controller;
        private Mock<IRecordService> _recordService;

        private IEnumerable<Record> _records;
        private const int RECORD_ID = 1;
        private const int RECORD_ID_MISSING = 99;
        private const int INVALID_RECORD_ID = -1;

        [TestInitialize]
        public void Init() {

            SetUpData();
            SetUpMocks();

            _controller = new RecordController(_recordService.Object);
        }

        private void SetUpData() 
        {
            _records = GetTestRecords();
        }

        private void SetUpMocks() 
        {
            _recordService = new Mock<IRecordService>();

            _recordService
                .Setup(x => x.Search(It.IsAny<SearchFilters>()))
                .Returns(_records);
        }

        private IEnumerable<Record> GetTestRecords() {
            return new List<Record>{
                new Record{ ID = RECORD_ID },
                new Record{ ID = 2 }
            };
        }

        [TestMethod]
        public void test_GetMany_CallsRecordServiceSearchWithCriteria()
        {
            SearchFilters f = new SearchFilters();

            _controller.Get(f);

            _recordService.Verify(x => x.Search(It.Is<SearchFilters>(sf => f.Equals(sf))));
        }

        [TestMethod]
        public void test_GetMany_ReturnsListOfFoundRecords() 
        {
            var result = _controller.Get(new SearchFilters());

            Assert.IsTrue(result.Value.SequenceEqual(_records));
        }

        [TestMethod]
        public void test_Get_CallsRecordServiceGetById() {
            _controller.Get(RECORD_ID);

            _recordService.Verify(x => x.GetById(RECORD_ID));
        }

        [TestMethod]
        public void test_Get_Returns404ForNonPositiveId() {
            var result = _controller.Get(INVALID_RECORD_ID);

            _recordService.Verify(x => x.GetById(It.IsAny<int>()), Times.Never());

            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void test_Get_Returns404WhenNothingFound() {
            var result = _controller.Get(RECORD_ID_MISSING);

            _recordService.Verify(x => x.GetById(It.IsAny<int>()), Times.Once());

            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }
    }
}
