# MT5 Account Dashboard

A modern ASP.NET web application that displays a dashboard of MT5 client account data.

## Features

- Account overview with balance, equity, and margin level
- Open trades count and status
- Last login timestamp
- Responsive design with Tailwind CSS
- Mocked API endpoints for development
- Clean architecture implementation
- Unit tests for core functionality

## Asumptions

- The Account Currency is set to USD

## Technologies

- ASP.NET Core (.NET 9)
- Tailwind CSS
- Chart.js
- Font Awesome
- xUnit for testing
- Moq for test mocking

## Getting Started

### Prerequisites

- .NET 9 SDK

### Installation

### 1. Clone the repository:

```bash
git clone https://github.com/your-repo/mt5-dashboard.git
cd mt5-dashboard
```

### 2. Restore dependencies:

```bash
dotnet restore
```

### 3. Run the application:

```bash
dotnet run --project Mt5Dashboard.Web
Open your browser to:
```

```text
https://localhost:PORT
```

## Project Structure

```text
mt5-dashboard/
├── Mt5Dashboard.Core/       # Core domain models
├── Mt5Dashboard.Services/   # Business logic and services
├── Mt5Dashboard.Web/        # ASP.NET Core web application
├── Mt5Dashboard.Tests/      # Unit tests
├── README.md                # This file
└── .gitignore               # Git ignore rules
```

## Testing

Run the unit tests with:

```bash
dotnet test
```
