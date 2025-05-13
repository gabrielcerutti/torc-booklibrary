# **Torc Book Library API**

The **Torc Book Library API** is a backend service built with `.NET 9` and Entity Framework Core. It provides endpoints to manage and search for books by author, ISBN, or ownership status. The API is designed to work with SQL Server as the database and Seq for centralized logging.

---

## **Features**
- Search for books by author, ISBN, or ownership status.
- Automatically applies database migrations and seeds initial data.
- Centralized logging using Serilog with Seq integration.
- Swagger UI for API documentation and testing.

---

## **Prerequisites**
Before running the backend, ensure you have the following installed:
- [Docker](https://www.docker.com/)
- [.NET SDK 9](https://dotnet.microsoft.com/)

---

## **Running the Backend**

### **1. Start Infrastructure Services**
The backend requires the following infrastructure services:
- **SQL Server**: For the database.
- **Seq**: For centralized logging.

To start these services, use the `docker-compose.infrastructure.yml` file:

```bash
docker-compose -f docker-compose.infrastructure.yml up -d
```
This command will start the SQL Server and Seq containers.

### **2. Start the Backend API**
The backend API will:
- Automatically apply migrations to create the database.
- Seed the database with initial data.

To start the API, use the `docker-compose.yml` file:
```bash
docker-compose up -d
```
This command will build the API image and start the container.
### **3. Access the API**
Once the API is running, you can access it at:
```
http://localhost:8010
```
### **4. Access Swagger UI**
The API provides a Swagger UI for testing and exploring the endpoints. You can access it at:
```
http://localhost:8010/index.html
```
### **5. Access Seq for Logging**
Seq provides a web interface for viewing logs. You can access it at:
```
http://localhost:5341
```
### **6. Stop the Services**
To stop the API and infrastructure services, run:
```bash
docker-compose down
```
This command will stop and remove the containers defined in the `docker-compose.yml` file.
