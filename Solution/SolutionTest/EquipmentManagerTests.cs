using BusinessLogic.Entities;
using BusinessLogic.Enums;
using BusinessLogic.Managers;
using SolutionTest.Utils;

namespace SolutionTest
{
    [TestClass]
    public class EquipmentManagerTests
    {
        private EquipmentManager? _equipmentManager;
        private FakeEquipmentMediator? _fakeMediator;

        [TestInitialize]
        public void Setup()
        {
            _fakeMediator = new FakeEquipmentMediator();
            _equipmentManager = new EquipmentManager(_fakeMediator);
        }

        [TestMethod]
        public void AddEquipment_ShouldAddEquipment()
        {
            var equipment = new Equipment
            {
                Name = "Test Equipment",
                Brand = "Test Brand",
                Size = "Medium",
                PricePerDay = 15.99m,
                Category = EquipmentCategory.Helmet,
                ImagePath = "test_image.jpg",
                Quantity = 10
            };

            _equipmentManager.AddEquipment(equipment);
            var allEquipment = _equipmentManager.GetAllEquipment();

            Assert.AreEqual(1, allEquipment.Count);
            Assert.AreEqual("Test Equipment", allEquipment[0].Name);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddEquipment_ShouldThrowException_WhenEquipmentNameIsNull()
        {
            var equipment = new Equipment
            {
                Name = null,
                Brand = "Test Brand",
                Size = "Medium",
                PricePerDay = 15.99m,
                Category = EquipmentCategory.Helmet,
                ImagePath = "test_image.jpg",
                Quantity = 10
            };

            _equipmentManager.AddEquipment(equipment);
        }

        [TestMethod]
        public void UpdateEquipment_ShouldModifyEquipment()
        {
            var equipment = new Equipment
            {
                Name = "Old Name",
                Brand = "Old Brand",
                Size = "Large",
                PricePerDay = 10.00m,
                Category = EquipmentCategory.Helmet,
                ImagePath = "old_image.jpg",
                Quantity = 5
            };

            _equipmentManager.AddEquipment(equipment);
            equipment.Name = "New Name";

            _equipmentManager.UpdateEquipment(equipment);
            var updatedEquipment = _equipmentManager.GetEquipmentById(equipment.EquipmentId);

            Assert.AreEqual("New Name", updatedEquipment.Name);
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void UpdateEquipment_ShouldThrowException_WhenEquipmentDoesNotExist()
        {
            var equipment = new Equipment
            {
                EquipmentId = 999,
                Name = "Nonexistent Equipment",
                Brand = "Nonexistent Brand",
                Size = "Small",
                PricePerDay = 20.00m,
                Category = EquipmentCategory.Helmet,
                ImagePath = "nonexistent.jpg",
                Quantity = 0
            };

            _equipmentManager.UpdateEquipment(equipment);
        }

        [TestMethod]
        public void DeleteEquipment_ShouldRemoveEquipment()
        {
            var equipment = new Equipment
            {
                Name = "EquipmentToDelete",
                Brand = "Test Brand",
                Size = "Small",
                PricePerDay = 10.00m,
                Category = EquipmentCategory.Helmet,
                ImagePath = "delete_image.jpg",
                Quantity = 1
            };

            _equipmentManager.AddEquipment(equipment);
            _equipmentManager.DeleteEquipment(equipment.EquipmentId);

            Assert.IsNull(_equipmentManager.GetEquipmentById(equipment.EquipmentId));
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void DeleteEquipment_ShouldThrowException_WhenEquipmentDoesNotExist()
        {
            _equipmentManager.DeleteEquipment(999);
        }

        [TestMethod]
        public void GetAllEquipment_ShouldReturnAllEquipment()
        {
            _equipmentManager.AddEquipment(new Equipment
            {
                Name = "Equipment 1",
                Brand = "Brand A",
                Size = "Small",
                PricePerDay = 10.00m,
                Category = EquipmentCategory.Helmet,
                ImagePath = "image1.jpg",
                Quantity = 5
            });

            _equipmentManager.AddEquipment(new Equipment
            {
                Name = "Equipment 2",
                Brand = "Brand B",
                Size = "Large",
                PricePerDay = 20.00m,
                Category = EquipmentCategory.Skis,
                ImagePath = "image2.jpg",
                Quantity = 8
            });

            var equipmentList = _equipmentManager.GetAllEquipment();

            Assert.AreEqual(2, equipmentList.Count);
            Assert.AreEqual("Equipment 1", equipmentList[0].Name);
            Assert.AreEqual("Equipment 2", equipmentList[1].Name);
        }
    }
}