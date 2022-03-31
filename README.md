# BooksAPI
Web API developed using VS 2019 and .Net5 to Create, Update, Search, Retrieve and Delete Books from the SQLite Database
![image](https://user-images.githubusercontent.com/27967325/161097501-ffdaeb20-445e-40f1-8a4d-1b558c05f6cd.png)


This API is developed to 

-Add/Create Book to the DB using "HTTP POST /v1/Books/add" route

-Get/Retrieve All Books stored in the DB using "HTTP GET /v1/Books" route

-Get/Retrieve Book by ID from DB using "HTTP GET /v1/Books/{id}" route

-Get/Retrieve Book by search parameters like Authors, ISBN, Title using "HTTP GET /v1/Books/query" route

-Update Book stored in the DB using "HTTP PUT /v1/Books/update" route

-Delete Book stored in the DB using "HTTP DELETE /v1/Books/delete/{id}" route

For running the API, Download the source code from BooksAPI repository, Open the solution file in Visual Studio 2019 and click on the IIS EXPRESS button in the tool bar or Press CTRL+F5(to run the solution without debugging)/Press F5(to run the solution with debugging).
