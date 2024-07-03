# ASPEKT-Application

# Introduction

ASPEKT-Application is a project that is working with Companies, Countries and Contacts. API that will return each contact with the company that is working and the country name.

# Technologies Used
# Backend
.NET 6: The application is built using .NET 6, which is a cross-platform framework for building high-performance applications.
ASP.NET Core: A framework for building web APIs and web applications in .NET. It provides features for routing, model binding, and more.
Entity Framework Core: An Object-Relational Mapper (ORM) for .NET that simplifies database access by allowing you to work with data using .NET objects.

# Testing
Moq: A widely used mocking framework for .NET that allows you to create mock objects for unit testing.
MSTest: The testing framework that comes with Visual Studio. It is integrated with the IDE and supports unit tests, integration tests, and more.

# Logging
Serilog: Used for logging errors and critical issues to help diagnose and trace problems.

# Validations
FluentValidation is a popular library for building strongly-typed validation rules in .NET applications. 

# Frontend
Swagger: Tool for generating interactive API documentation and testing endpoints directly from the browser.

# Database
SQL Server: A relational database management system used for storing application data.

# Development Tools
Visual Studio 2022: An integrated development environment (IDE) for developing .NET applications.


# CompanyController
The CompanyController provides endpoints for managing company resources. It allows for the adding, creation, update, and deletion of companies.

# Endpoints
# 1.Get
Route: GET api/company
Description: Retrievs a list of all companies

Response:
200 OK: A list of CompanyDto objects.
404 Not Found: If no companies are found
500 Internal Server Error: For any server-side errors.

[
  {
    "companyId": 1,
    "companyName": "ASPEKT"
  }
]

# 2. GetCompanyById
Route: GET api/company/{id}
Description: Retrieves a specific company by its ID.
Parameters:
id (path): The ID of the company to retrieve.

Request:
GET /api/company/1

Response:
200 OK: A CompanyDto object representing the company.
404 Not Found: If the company with the specified ID does not exist.
500 Internal Server Error: For any server-side errors.

{
  "companyId": 1,
  "companyName": "AppleMacedonia"
}

# 3. AddingCompany
Route: POST api/company
Description: Creates a new company.
Request Body: A CompanyDto object representing the company to create.

Request:

{
  "CompanyName": "New Company"
}

Response:
201 Created: If the company is created successfully.
400 Bad Request: If the provided data is invalid.
500 Internal Server Error: For any server-side errors.

{
  "message": "Company created successfully"
}

# 4. Update
Route: PUT api/company
Description: Updates an existing company
Request Body: A CompanyDto object representing the company with updated data.

Request:

{
  "companyId": 0,
  "companyName": "string"
}

Response:
204 No Content: If the company is updated successfully.
400 Bad Request: If the provided data is invalid or if the company is not found.
500 Internal Server Error: For any server-side errors.

# 5. DeleteCompany
Route: DELETE api/company
Description: Deletes a company by ID.
Parameters:
id (query): The ID of the company to delete.

Request:
DELETE api/Company?id=5

Response:
204 No Content: If the company is deleted successfully.
400 Bad Request: If the provided ID is invalid or if the company is not found.
500 Internal Server Error: For any server-side errors.


# CountryController
The CountryController provides endpoints for managing country resources. It allows you to add, create, update, and delete country records.

# Endpoints

# 1.GetAllCountry
Route: GET api/country
Description: Retrieves a list of all countries.

Response:
200 OK: A list of CountryDto objects.
404 Not Found: If no countries are found.
500 Internal Server Error: For any server-side errors

[
  {
    "countryId": 1,
    "countryName": "USA"
  },
  {
    "countryId": 2,
    "countryName": "Macedonia"
  }
]

# 2.GetCountryById
Route: GET api/country/{id}
Description: Retrieves a specific country by its ID.

Parameters:
id (path): The ID of the country to retrieve.

Request:
GET /api/country/1

Response:
200 OK: A CountryDto object representing the country.
404 Not Found: If the country with the specified ID does not exist.
500 Internal Server Error: For any server-side errors.

{
    "countryId": 1,
    "countryName": "USA"
}

# 3. AddingCountry
Route: POST api/country
Description: Creates a new country.
Request Body: A CountryDto object representing the country to create.
  {
    "countryName": "Macedonia"
  }

Response:
201 Created: If the country is created successfully.
400 Bad Request: If the provided data is invalid.
500 Internal Server Error: For any server-side errors.

# 4.UpdateCountry
Route: PUT api/country
Description: Updates an existing country.
Request Body: A CountryDto object representing the country with updated data.

Request:
{
  "countryId": 1,
  "countryName": "string"
}

Response:
204 No Content: If the country is updated successfully.
400 Bad Request: If the provided data is invalid or if the country is not found.
500 Internal Server Error: For any server-side errors.

# 5.DeleteCountryById
Route: DELETE api/country/{id}
Description: Deletes a country by ID.

Parameters:
id (path): The ID of the country to delete.

Request:
DELETE /api/country/1

Response:
204 No Content: If the country is deleted successfully.
400 Bad Request: If the provided ID is invalid or if the country is not found.
500 Internal Server Error: For any server-side errors.

# ContactController
The ContactController provides endpoints for managing contact resources. It allows you to add, create, update, filter and delete contact records.

# Endpoints

# 1.GetAll
Route: GET api/contact
Description: Retrieves a list of all contacts.

Response:
200 OK: A list of ContactDto objects.
404 Not Found: If no contacts are found.
500 Internal Server Error: For any server-side errors.

[
  {
    "contactId": 1,
    "companyId": 1,
    "countryId": 1,
    "contactName": "Aleksandar"
  },
  {
    "contactId": 3,
    "companyId": 1,
    "countryId": 2,
    "contactName": "Marko"
  },
  {
    "contactId": 6,
    "companyId": 2,
    "countryId": 3,
    "contactName": "Dushko"
  }
]

# 2.GetById
Route: GET api/contact/{id}
Description: Retrieves a specific contact by its ID.

Parameters:
id (path): The ID of the contact to retrieve.

Request:
GET /api/contact/1


Response:
200 OK: A ContactDto object representing the contact.
404 Not Found: If the contact with the specified ID does not exist.
500 Internal Server Error: For any server-side errors.

  {
    "contactId": 1,
    "companyId": 1,
    "countryId": 1,
    "contactName": "Aleksandar"
  }

# 3.AddContact
Route: POST api/contact
Description: Creates a new contact.
Request Body: A ContactDto object representing the contact to create.

Request:
{
    "companyId": 1,
    "countryId": 1,
    "contactName": "Aleksandar"
}


Response:
201 Created: If the contact is created successfully.
400 Bad Request: If the provided data is invalid.
500 Internal Server Error: For any server-side errors.

# 4.UpdateContact
Route: PUT api/contact
Description: Updates an existing contact.
Request Body: A ContactDto object representing the contact with updated data.

Request:
{
    "contactId": 1,
    "companyId": 1,
    "countryId": 1,
    "contactName": "Update"
}


Response:
204 No Content: If the contact is updated successfully.
400 Bad Request: If the provided data is invalid or if the contact is not found.
500 Internal Server Error: For any server-side errors.

# 5. Delete
Route: DELETE api/contact/{id}
Description: Deletes a contact by ID.

Parameters:
id (path): The ID of the contact to delete.

Request:
DELETE /api/contact/1

Response:
204 No Content: If the contact is deleted successfully.
400 Bad Request: If the provided ID is invalid or if the contact is not found.
500 Internal Server Error: For any server-side errors.

# 6. GetContactWithCompanyAndCountry
Route: GET api/contact/GetContacts
Description: Retrieves contacts along with their associated company and country information.


Response:
200 OK: A list of FilterContactDto objects.
404 Not Found: If no contacts are found.
500 Internal Server Error: For any server-side errors.

[
    {
    "contactId": 1,
    "companyId": 1,
    "countryId": 1,
    "contactName": "Aleksandar",
    "countryName": "USA",
    "companyName": "AppleMacedonia"
  }
]

# 7. GetFilteredByCompanyAndCountryId
Route: GET api/contact/FilterContacts
Description: Retrieves contacts filtered by company ID and country ID.

Parameters:
companyId (query): The ID of the company to filter contacts by.
countryId (query): The ID of the country to filter contacts by.

Request:
GET /api/contact/FilterContacts?companyId=1&countryId=1
GET /api/Contact/FilterContacts?companyId=2
GET /api/Contact/FilterContacts?countryId=2

Response:
200 OK: A list of FilterContactDto objects.
404 Not Found: If no contacts are found.
500 Internal Server Error: For any server-side errors.

[
  {
    "contactId": 1,
    "companyId": 1,
    "countryId": 1,
    "contactName": "Aleksandar",
    "countryName": "USA",
    "companyName": "AppleMacedonia"
  }
]

[
  {
    "contactId": 6,
    "companyId": 2,
    "countryId": 0,
    "contactName": "Dushko",
    "countryName": null,
    "companyName": "Aspekt"
  }
]

[
  {
    "contactId": 3,
    "companyId": 1,
    "countryId": 0,
    "contactName": "Trpe",
    "countryName": "Macedonia",
    "companyName": null
  }
]