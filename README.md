# Web Store
Web application for managing store online and place to buy products by clients.

Functions
--
* create an account, login, log out
* as Admin: manage products & orders (CRUD), manage users and role, generate an invoice, send email to client
* as User: search products, add them to shopping cart and buy, see order history, add review with rating, send email and get invoice
* as guest: search products

Getting started
--
* Get copy of the project
* Open MS SQL Server Management Studio and create new database called WebStore
* Open WebStore.sln in MS Visual Studio and in Package Manager Console write:
```sh
PM> add-migration Initial
PM> update-database
```
* Open project in MS Visual Studio (it will generate admin account with Admin role)
* Login as admin:
```sh
admin
Admin-369*
```
* Go to: 
```sh
/admin/index
```
and click button "Initialize database"

Now you can change password for admin, create new users and set them to User role.

Used technologies
--
* ASP.NET Core Mvc 2.0
* Entity Framework Core
* ASP.NET Core Identity
* HTML
* CSS 
* JavaScript 
* jQuery
* Bootstrap
