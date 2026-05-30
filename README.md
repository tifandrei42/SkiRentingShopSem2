# SkiRentingShopSem2

A ski & snowboard equipment **rental shop** system built as a Semester 2 software
project. Customers can browse and reserve gear through a web app, while staff
manage the equipment catalogue and stock through a dedicated Windows desktop app.

The whole thing is one Visual Studio solution that shares a single business-logic /
data-access layer (`BusinessLogic`) between two front-ends, backed by a Microsoft
SQL Server database.

## Features

### Customer web app (`Renting-WebApp`)
- Register, log in / log out, and manage a personal profile
- Browse the equipment catalogue and open a detailed product view
- Reserve equipment and view your own reservations

### Staff desktop app (`Renting-DesktopApp`)
- Staff login
- Equipment management (add / view equipment)
- Stock management
- Central menu to navigate the management screens

## Architecture

The solution follows a layered design so both front-ends reuse the same logic:

```
UI layer        Renting-WebApp (Razor Pages)   Renting-DesktopApp (WinForms)
                              \                 /
Business layer                 Managers   (ClassLibrary/Managers)
Data-access layer              Mediators  (ClassLibrary/DataAccess)
Domain layer                   Entities   (ClassLibrary/Entities)
Database                       Microsoft SQL Server
```

- **Entities** – domain classes: an abstract `User` base with `Customer` and
  `StaffMember` derivatives, plus `Equipment`, `Reservation`, and `Stock`.
  (e.g. `Equipment` holds `Name`, `Brand`, `Size`, `PricePerDay`,
  `EquipmentType`, `ImagePath`; `Reservation` links a customer to equipment over
  a date range with a `TotalPrice` and `Status`.)
- **Mediators** (`DataAccess`) – handle all SQL access (`CustomerMediator`,
  `EquipmentMediator`, `ReservationMediator`, `StaffMediator`, `StockMediator`),
  with the connection created in `DbAccess`.
- **Managers** – business logic between the UI and the mediators
  (`CustomerManager`, `EquipmentManager`, `ReservationManager`, `StaffManager`,
  `StockManager`).
- **Interfaces** – abstractions such as `IEquipmentMediator` that let the
  managers be unit-tested with fakes instead of a live database.

## Project structure

```
SkiRentingShopSem2/
├── Documents/                 # ERD, class diagram, project plan, URS, ideation docs
└── Solution/
    ├── Basic-Renting.sln      # Visual Studio solution
    ├── ClassLibrary/          # Shared business logic (BusinessLogic.csproj)
    │   ├── Entities/
    │   ├── DataAccess/        # Mediators + DbAccess
    │   ├── Managers/
    │   └── Interfaces/
    ├── Renting-WebApp/        # ASP.NET Core Razor Pages (customer-facing)
    ├── Renting-DesktopApp/    # Windows Forms (staff-facing)
    ├── SolutionTest/          # Unit tests (uses fake mediators)
    ├── UnitTests/             # Additional unit tests
    └── TestDesktop/           # Desktop test project
```

> The `Basic-Renting.sln` solution contains `BusinessLogic`, `Renting-WebApp`,
> `Renting-DesktopApp`, and `SolutionTest`. `UnitTests` and `TestDesktop` also
> live in the repository as separate test projects.

## Tech stack

- **Language / runtime:** C# on .NET 8 (web targets `net8.0`, desktop targets `net8.0-windows`)
- **Web:** ASP.NET Core Razor Pages, Bootstrap, jQuery (+ jQuery Validation)
- **Desktop:** Windows Forms
- **Data access:** Microsoft SQL Server via `System.Data.SqlClient`
- **Security:** password hashing with `BCrypt.Net-Next`
- **Testing:** MSTest
- **IDE:** Visual Studio 2022

## Getting started

### Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/download) and Visual Studio 2022 with
  the **.NET desktop development** and **ASP.NET and web development** workloads
- Access to a Microsoft SQL Server instance with the rental-shop schema
- Windows is required to build and run the Windows Forms desktop app

### Build & run

Open `Solution/Basic-Renting.sln` in Visual Studio, then:

- **Web app:** set `Renting-WebApp` as the startup project and run (F5).
- **Desktop app:** set `Renting-DesktopApp` as the startup project and run (F5).

Or from the command line:

```bash
cd Solution
dotnet build Basic-Renting.sln

# customer web app
dotnet run --project Renting-WebApp

# staff desktop app (Windows only)
dotnet run --project Renting-DesktopApp
```

### Configuration

The database connection string is defined in
`Solution/ClassLibrary/DataAccess/DbAccess.cs`. Update it to point at your own
SQL Server instance before running.

## Running the tests

```bash
cd Solution
dotnet test SolutionTest/SolutionTest.csproj
```

The `SolutionTest` project uses fake mediators (e.g. `FakeEquipmentMediator`) so
the managers can be tested without a real database connection.

## Documentation

The `Documents/` folder contains the supporting project documentation:

- Ideation document
- User Requirements Specification (URS)
- Project plan
- Entity-Relationship Diagram (ERD)
- Class diagram
