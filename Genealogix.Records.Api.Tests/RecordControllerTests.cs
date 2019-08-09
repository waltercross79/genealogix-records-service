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
        private const string RECORD_ID = "ABCD";
        private const string RECORD_ID_MISSING_FROM_DB = "XYZ";
        private const string INVALID_RECORD_ID = "";
        private const string NEW_ID = "KLMN";

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

            _recordService
                .Setup(x => x.Create(It.IsAny<Record>()))
                .Returns<Record>(r =>
                {
                    r.ID = NEW_ID;
                    return r;
                });

            _recordService
                .Setup(x => x.GetById(It.IsAny<string>()))
                .Returns((string id) => _records.Where(r => r.ID == id).SingleOrDefault());

            _recordService
                .Setup(x => x.Update(It.IsAny<string>(), It.IsAny<Record>()));
        }

        private IEnumerable<Record> GetTestRecords() {
            return new List<Record>{
                new Record{ ID = RECORD_ID },
                new Record{ ID = "EFGH" }
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
        public void test_GetMany_ReturnsEmptyListIfNoRecordTypeSpecified()
        {
            var result = _controller.Get(new SearchFilters { IncludeBirths = false, IncludeDeaths = false, IncludeMarriages = false });

            // Verify that the process is short-circuited.
            _recordService.Verify(x => x.Search(It.IsAny<SearchFilters>()), Times.Never());
            Assert.AreEqual(0, result.Value.Count());
        }

        [TestMethod]
        public void test_Get_CallsRecordServiceGetById() {
            _controller.Get(RECORD_ID);

            _recordService.Verify(x => x.GetById(RECORD_ID));
        }

        [TestMethod]
        public void test_Get_ReturnsFoundRecord()
        {
            var result = _controller.Get(RECORD_ID);

            Assert.AreEqual(result.Value, _records.FirstOrDefault(r => r.ID == RECORD_ID));
        }

        [TestMethod]
        public void test_Get_Returns404ForNonPositiveId() {
            var result = _controller.Get(INVALID_RECORD_ID);

            _recordService.Verify(x => x.GetById(It.IsAny<string>()), Times.Never());

            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void test_Get_Returns404WhenNothingFound() {
            var result = _controller.Get(RECORD_ID_MISSING_FROM_DB);

            _recordService.Verify(x => x.GetById(It.IsAny<string>()), Times.Once());

            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void test_Create_CallsRecordServiceCreate()
        {
            Record r = new Record { RecordType = RecordType.Birth, RecordDate = new System.DateTime(2000, 1, 1) };

            _controller.Create(r);

            _recordService.Verify(x => x.Create(r));
        }

        [TestMethod]
        public void test_Create_ReturnsInsertedRecordWithUpdatedID()
        {
            Record r = new Record { RecordType = RecordType.Birth, RecordDate = new System.DateTime(2000, 1, 1) };

            var result = _controller.Create(r);

            Assert.AreEqual(NEW_ID, result.Value.ID);
            Assert.AreEqual(r.RecordDate, result.Value.RecordDate);
            Assert.AreEqual(r.RecordType, result.Value.RecordType);
        }

        [TestMethod]
        public void test_Update_CallsRecordServiceUpdate()
        {
            Record r = new Record { ID = RECORD_ID, RecordType = RecordType.Birth, RecordDate = new System.DateTime(2000, 1, 1) };

            _controller.Update(RECORD_ID, r);

            _recordService.Verify(x => x.Update(RECORD_ID, r));
        }

        [TestMethod]
        public void test_Update_ReturnsUpdatedRecord()
        {
            Record r = new Record { ID = RECORD_ID, RecordType = RecordType.Birth, RecordDate = new System.DateTime(2000, 1, 1) };

            var result = _controller.Update(RECORD_ID, r);

            Assert.AreEqual(RECORD_ID, result.Value.ID);
            Assert.AreEqual(r.RecordDate, result.Value.RecordDate);
            Assert.AreEqual(r.RecordType, result.Value.RecordType);
        }

        [TestMethod]
        public void test_Update_Returns404WhenRecordNotFound()
        {
            Record r = new Record { ID = RECORD_ID_MISSING_FROM_DB, RecordType = RecordType.Birth, RecordDate = new System.DateTime(2000, 1, 1) };

            var result = _controller.Update(RECORD_ID_MISSING_FROM_DB, r);

            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void test_Delete_CallsRecordServiceRemove()
        {
            _controller.Delete(RECORD_ID);

            _recordService.Verify(x => x.Remove(RECORD_ID));
        }

        [TestMethod]
        public void test_Delete_Returns404WhenRecordNotFound()
        {
            var result = _controller.Delete(RECORD_ID_MISSING_FROM_DB);

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void test_Delete_Returns204WhenSuccess()
        {
            var result = _controller.Delete(RECORD_ID);

            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public void test_AddPerson_CallsRecordServiceToFindAndUpdateRecord()
        {
            PersonInRecord personToAdd = new PersonInRecord();
            _controller.AddPerson(RECORD_ID, personToAdd);

            _recordService.Verify(x => x.GetById(RECORD_ID));
            _recordService.Verify(x => x.Update(RECORD_ID, It.Is<Record>(r =>
                r.Persons.Count() == 1 &&
                r.Persons.Single().Equals(personToAdd)
                )));
        }

        [TestMethod]
        public void test_AddPerson_ReturnsNoContent()
        {
            PersonInRecord personToAdd = new PersonInRecord();
            var result = _controller.AddPerson(RECORD_ID, personToAdd);

            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public void test_AddPerson_ReturnsNotFoundIfNoMatchingRecord()
        {
            PersonInRecord personToAdd = new PersonInRecord();
            var result = _controller.AddPerson(RECORD_ID_MISSING_FROM_DB, personToAdd);

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
    }
}
