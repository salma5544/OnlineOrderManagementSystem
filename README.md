# Order Management API

## Project Overview
This is an API for managing customer orders in an e-commerce system. The API allows users to create orders, update order status, and track order details.

## Prerequisites
- ## Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0) — Version: 8.0.201
> Note: A `global.json` file is included to ensure the correct .NET SDK version is used.
- SQL Server
- Entity Framework Core

## Setup Instructions
1. Clone this repository:
    ```bash
    git clone https://github.com/yourusername/order-management-api.git
    ```
2. Install dependencies:
    ```bash
    dotnet restore
    ```
3. Apply database migrations:
    ```bash
    dotnet ef database update
    ```
4. Run the API:
    ```bash
    dotnet run
    ```

## API Endpoints

### POST /orders
Creates a new order.
- Request Body:
    ```json
    {
        "customerId": 1,
        "items": [
            { "productId": 1, "quantity": 2 }
        ]
    }
    ```
- Response:
    ```json
    {
        "id": 1,
        "status": "Pending",
        "orderDate": "2025-04-12T12:00:00Z"
    }
    ```

### PUT /orders/{id}/status
Updates the status of an order.
- Request Body:
    ```json
    {
        "status": "Shipped"
    }
    ```
- Response:
    ```json
    {
        "message": "Order status updated to Shipped."
    }
    ```

## Dependencies
- .NET 8
- Entity Framework Core
- SQL Server

## Notes
- The API uses Entity Framework Core for database interaction.
- API responses are in JSON format.
