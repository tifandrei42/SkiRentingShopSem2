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
                Category = EquipmentCategory.Helmet,
                ImagePath = "test_image.jpg", 
                PricePerDay = 15.99m,
                Size = "Medium"
            };

            _equipmentManager.AddEquipment(equipment);

            Assert.AreEqual(1, _equipmentManager.GetAllEquipment().Count);
            var addedEquipment = _equipmentManager.GetEquipmentById(equipment.EquipmentId);
            Assert.IsNotNull(addedEquipment);
            Assert.AreEqual("Test Equipment", addedEquipment.Name);
            Assert.AreEqual("test_image.jpg", addedEquipment.ImagePath);
        }

        [TestMethod]
        public void GetAllEquipment_ShouldReturnAllEquipment()
        {
            _equipmentManager.AddEquipment(new Equipment
            {
                Name = "Equipment 1",
                Brand = "Brand A",
                Category = EquipmentCategory.Helmet,
                ImagePath = "image1.jpg",
                PricePerDay = 10.0m
            });

            _equipmentManager.AddEquipment(new Equipment
            {
                Name = "Equipment 2",
                Brand = "Brand B",
                Category = EquipmentCategory.Helmet,
                ImagePath = "image2.jpg",
                PricePerDay = 20.0m
            });

            var equipmentList = _equipmentManager.GetAllEquipment();

            Assert.AreEqual(2, equipmentList.Count);
            Assert.AreEqual("Equipment 1", equipmentList[0].Name);
            Assert.AreEqual("Equipment 2", equipmentList[1].Name);
        }

        [TestMethod]
        public void UpdateEquipment_ShouldModifyEquipment()
        {
            var equipment = new Equipment { Name = "Old Name", PricePerDay = 10.0m, Brand= "Old Brand", Category = EquipmentCategory.Helmet};
            _equipmentManager.AddEquipment(equipment);

            equipment.Name = "New Name";

            _equipmentManager.UpdateEquipment(equipment);

            var updatedEquipment = _equipmentManager.GetEquipmentById(equipment.EquipmentId);
            Assert.AreEqual("New Name", updatedEquipment.Name);
        }

        [TestMethod]
        public void DeleteEquipment_ShouldRemoveEquipment()
        {
            var equipment = new Equipment { Name = "EquipmenttoDelete", PricePerDay = 10.0m, Brand = "Something", Category = EquipmentCategory.Helmet};
            _equipmentManager.AddEquipment(equipment);

            _equipmentManager.DeleteEquipment(equipment.EquipmentId);

            Assert.IsNull(_equipmentManager.GetEquipmentById(equipment.EquipmentId));
        }
    }
}