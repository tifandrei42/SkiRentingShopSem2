using BusinessLogic.Entities;
using BusinessLogic.Enums;
using BusinessLogic.Managers;
using SolutionTest.Utils;

namespace SolutionTest
{
    [TestClass]
    public class AvailabilityManagerTests
    {
        private AvailabilityManager? _availabilityManager;
        private FakeReservationMediator? _fakeMediator;
        private ReservationManager? _reservationManager;

        [TestInitialize]
        public void Setup()
        {
            _fakeMediator = new FakeReservationMediator();
            _reservationManager = new ReservationManager(_fakeMediator);
            _availabilityManager = new AvailabilityManager(_reservationManager);
        }

        [TestMethod]
        public void CheckAvailability_ShouldReturnTrue_WhenStockIsAvailable()
        {
            var equipment = new Equipment
            {
                EquipmentId = 1,
                Name = "Helmet",
                Brand = "Generic Brand",
                Size = "Medium",
                PricePerDay = 15.99m,
                Category = EquipmentCategory.Helmet,
                ImagePath = "helmet.jpg",
                Quantity = 10
            };

            var basket = new List<Reservation>();
            bool result = _availabilityManager.CheckAvailability(DateTime.Today, equipment, basket, 2);

            Assert.IsTrue(result, "Stock was incorrectly marked as unavailable.");
        }

        [TestMethod]
        public void CheckAvailability_ShouldReturnFalse_WhenStockIsInsufficient()
        {
            var equipment = new Equipment
            {
                EquipmentId = 1,
                Name = "Helmet",
                Brand = "Generic Brand",
                Size = "Large",
                PricePerDay = 15.99m,
                Category = EquipmentCategory.Helmet,
                ImagePath = "helmet_large.jpg",
                Quantity = 3
            };

            var reservation = new Reservation
            {
                Equipment = equipment,
                Quantity = 3,
                ReservationDate = DateTime.Today,
                Status = Status.Pending
            };

            _reservationManager.CreateReservation(reservation);
            var basket = new List<Reservation>();
            bool result = _availabilityManager.CheckAvailability(DateTime.Today, equipment, basket, 1);

            Assert.IsFalse(result, "Stock was incorrectly marked as available.");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CheckAvailability_ShouldThrowException_WhenEquipmentIsNull()
        {
            _availabilityManager.CheckAvailability(DateTime.Today, null, new List<Reservation>(), 1);
        }

        [TestMethod]
        public void GetUnavailableDates_ShouldReturnUnavailableDates()
        {
            var equipment = new Equipment
            {
                EquipmentId = 1,
                Name = "Helmet",
                Brand = "Generic Brand",
                Size = "Small",
                PricePerDay = 10.00m,
                Category = EquipmentCategory.Helmet,
                ImagePath = "helmet_small.jpg",
                Quantity = 2
            };

            var reservation = new Reservation
            {
                Equipment = equipment,
                Quantity = 2,
                ReservationDate = DateTime.Today.AddDays(1),
                Status = Status.Pending
            };

            _reservationManager.CreateReservation(reservation);
            var basket = new List<Reservation>();
            var unavailableDates = _availabilityManager.GetUnavailableDates(equipment, basket, 1);

            Assert.AreEqual(1, unavailableDates.Count, "Unavailable dates count mismatch.");
            Assert.AreEqual(DateTime.Today.AddDays(1), unavailableDates[0], "Unavailable date mismatch.");
        }

        [TestMethod]
        public void GetUnavailableDates_ShouldIncludeBasketDates()
        {
            var equipment = new Equipment
            {
                EquipmentId = 1,
                Name = "Helmet",
                Brand = "Generic Brand",
                Size = "Large",
                PricePerDay = 10.00m,
                Category = EquipmentCategory.Helmet,
                ImagePath = "helmet_large.jpg",
                Quantity = 2
            };

            var basket = new List<Reservation>
        {
            new Reservation
            {
                Equipment = equipment,
                Quantity = 2,
                ReservationDate = DateTime.Today.AddDays(2),
                Status = Status.Pending
            }
        };

            var unavailableDates = _availabilityManager.GetUnavailableDates(equipment, basket, 1);

            Assert.AreEqual(1, unavailableDates.Count, "Unavailable dates count mismatch.");
            Assert.AreEqual(DateTime.Today.AddDays(2), unavailableDates[0], "Unavailable date mismatch.");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetUnavailableDates_ShouldThrowException_WhenEquipmentHasNoQuantity()
        {
            var equipment = new Equipment
            {
                EquipmentId = 1,
                Name = "Helmet",
                Brand = "Generic Brand",
                Size = "Small",
                PricePerDay = 10.00m,
                Category = EquipmentCategory.Helmet,
                ImagePath = "helmet_small.jpg",
                Quantity = 0
            };

            _availabilityManager.GetUnavailableDates(equipment, new List<Reservation>(), 1);
        }
    }

}