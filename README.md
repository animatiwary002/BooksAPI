# BooksAPI
Web API developed using VS 2019 and .Net5 to Create, Update, Search, Retrieve and Delete Books from the SQLite Database
![image](https://user-images.githubusercontent.com/27967325/160909451-7be02504-0c21-41ff-a216-5bdb4e6902aa.png)

This API is developed to 
-Add/Create Book to the DB using "HTTP POST /api/books/v1/addBooks" route
-Get/Retrieve All Books stored in the DB using "HTTP GET /api/books/v1/getAllBooks" route
-Get/Retrieve Book by ID from DB using "HTTP GET /api/books/v1/{id}" route
-Get/Retrieve Book by search parameters like Authors, ISBN, Title using "HTTP GET /api/books/v1/getBookBySearchParam" route
-Update Book stored in the DB using "HTTP PUT /api/books/v1/updateBook" route
-Delete Book stored in the DB using "HTTP DELETE /api/books/v1/deleteBook/{id}" route
