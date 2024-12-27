using BusinessLogic.Entities;
using BusinessLogic.Enums;
using BusinessLogic.Managers;
using SolutionTest.Utils;

namespace SolutionTest;

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
        // Arrange
        var equipment = new Equipment
        {
            EquipmentId = 1,
            Name = "Helmet",
            Quantity = 10,
            PricePerDay = 15.00m,
            Brand = "Atomic"
        };

        var basket = new List<Reservation>();

        // Act
        bool result = _availabilityManager.CheckAvailability(DateTime.Today, equipment, basket, 2);

        // Assert
        Assert.IsTrue(result, "Availability check failed when stock is available.");
    }

    [TestMethod]
    public void CheckAvailability_ShouldReturnFalse_WhenStockIsInsufficient()
    {
        // Arrange
        var equipment = new Equipment
        {
            EquipmentId = 1,
            Name = "Helmet",
            Quantity = 3,
            PricePerDay = 15.00m,
            Brand = "Atomic"
        };

        // Add existing reservations
        var reservation = new Reservation
        {
            Equipment = equipment,
            Quantity = 3,
            ReservationDate = DateTime.Today,
            Status = Status.Pending
        };
        _reservationManager.CreateReservation(reservation);

        var basket = new List<Reservation>();

        // Act
        bool result = _availabilityManager.CheckAvailability(DateTime.Today, equipment, basket, 1);

        // Assert
        Assert.IsFalse(result, "Availability check failed when stock is insufficient.");
    }

    [TestMethod]
    public void GetUnavailableDates_ShouldReturnUnavailableDates()
    {
        // Arrange
        var equipment = new Equipment
        {
            EquipmentId = 1,
            Name = "Helmet",
            Quantity = 2,
            PricePerDay = 10.00m,
            Brand = "Atomic"
        };

        // Add reservations
        var reservation1 = new Reservation
        {
            Equipment = equipment,
            Quantity = 2,
            ReservationDate = DateTime.Today.AddDays(1),
            Status = Status.Pending
        };
        _reservationManager.CreateReservation(reservation1);

        var basket = new List<Reservation>();

        // Act
        var unavailableDates = _availabilityManager.GetUnavailableDates(equipment, basket, 1);

        // Assert
        Assert.AreEqual(1, unavailableDates.Count, "Unavailable dates count mismatch.");
        Assert.AreEqual(DateTime.Today.AddDays(1), unavailableDates[0], "Unavailable date mismatch.");
    }

    [TestMethod]
    public void GetUnavailableDates_ShouldIncludeBasketDates()
    {
        // Arrange
        var equipment = new Equipment
        {
            EquipmentId = 1,
            Name = "Helmet",
            Quantity = 2,
            PricePerDay = 10.00m,
            Brand = "Atomic"
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
}
