<h1 align="center" id="title">Infinion Coding Challenge</h1>

<p id="description">This project is a demonstration of a robust web application built using ASP.NET Core API (C#). The application allows users to register, login, and perform CRUD operations on products with pagination and filter support. The project adheres to the principles of Earth Layer architecture and includes essential features such as user authentication, authorization, and email notifications.</p>

<h2>Screenshoot of the API ENDPOINTS</h2>  


![image](https://github.com/dejidee0/User-Management-Profile/assets/107705210/4677f6b7-6db9-447d-ae53-70545053f479)
![image](https://github.com/dejidee0/User-Management-Profile/assets/107705210/24d10be0-7fa3-4e45-92ae-46c5f8ec9813)


Below is mail verification working
![WhatsApp Image 2024-06-17 at 10 56 39_c770860e](https://github.com/dejidee0/User-Management-Profile/assets/107705210/5fdbfe6a-8bad-465b-8db9-3b432af54394)




  
<h2> Features</h2>

Here're some of the project's best features:

User Registration

    Endpoint: POST /api/User/register
    Parameters: email, password, first name, last name
    Validations: email format, strong password criteria
    Confirmation email sent to the user
    User details saved in the database

User Login

    Endpoint: POST /api/User/login
    Parameters: email, password
    Validations: authenticate user credentials against the database
    Token (JWT) generation upon successful login
    Token included in the Authorization header for subsequent requests

Product CRUD Operations

    Endpoints:
        GET /api/Products - Retrieve all products with pagination and filtering
        GET /api/Products/{id} - Retrieve a product by its ID
        POST /api/Products - Create a new product
        PUT /api/Products/{id} - Update an existing product
        DELETE /api/Products/{id} - Delete a product

Prerequisites

    .NET 6 SDK
    SQL Server
    SMTP server for email service (MAILTRAP) please note this smtp only allows sending mail to IFEMICHEAL2@GMAIL.COM this is the condition giving by the free smtp for the purpose of this task. 

  ![image](https://github.com/dejidee0/User-Management-Profile/assets/107705210/6da16fc5-2970-49a0-bf5f-13ddda24f4ff)


Installation

    Clone the Repository 

git clone https://github.com/dejidee0/User-Management-Profile.git


Restore Dependencies
dotnet restore

Database

Smarterasp.net was used for the database to keep records online below is the connection settings

"ConnectionStrings": {
  "NewDatabase": "Data Source=SQL8006.site4now.net;Initial Catalog=db_aa9ff7_user1;User Id=db_aa9ff7_user1_admin;Password=Nisotgreg0"
}

Database Setup

    Add-Migration InitialMigration
    Update-Database

 

Running the Application

    Start the Application  
    dotnet run

    Access Swagger UI
        Navigate to https://localhost:7071/swagger in your web browser to view and interact with the API endpoints
