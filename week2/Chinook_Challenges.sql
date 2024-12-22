-- On the Chinook DB, practice writing queries with the following exercises

-- BASIC CHALLENGES
-- List all customers (full name, customer id, and country) who are not in the USA
SELECT FirstName + ' ' + LastName as FullName, CustomerId, Country
FROM Customer
WHERE Country != 'USA'
    
-- List all customers from Brazil
SELECT FirstName, LastName, CustomerId, Country
FROM Customer
WHERE Country = 'Brazil';
    
-- List all sales agents
SELECT DISTINCT FirstName, LastName, EmployeeId, Title
FROM Employee
WHERE Title = 'Sales Support Agent';

-- Retrieve a list of all countries in billing addresses on invoices
SELECT DISTINCT BillingCountry
FROM Invoice;

-- Retrieve how many invoices there were in 2009, and what was the sales total for that year?
SELECT COUNT(*) AS InvoiceCount, SUM(Total) AS SalesTotal
FROM Invoice
WHERE InvoiceDate BETWEEN '2009-01-01' AND '2009-12-31';

    -- (challenge: find the invoice count sales total for every year using one query)


-- how many line items were there for invoice #37
SELECT COUNT(*) AS ItemCount
FROM InvoiceLine
WHERE InvoiceId = 37;

-- how many invoices per country? BillingCountry  # of invoices -
SELECT BillingCountry, COUNT(*) AS InvoiceCount
FROM Invoice
GROUP BY BillingCountry
ORDER BY InvoiceCount DESC;

-- Retrieve the total sales per country, ordered by the highest total sales first.
SELECT BillingCountry, SUM(Total) AS TotalSales
FROM Invoice
GROUP BY BillingCountry
ORDER BY TotalSales DESC;


-- JOINS CHALLENGES
-- Every Album by Artist
SELECT Artist.Name AS ArtistName, Album.Title AS AlbumTitle
FROM Album
JOIN Artist ON Album.ArtistId = Artist.ArtistId
ORDER BY AlbumTitle;

-- All songs of the rock genre
SELECT Track.Name AS SongName, Genre.Name AS Genre
FROM Track
JOIN Genre ON Track.GenreId = Genre.GenreId
WHERE Genre.Name = 'Rock';

-- Show all invoices of customers from brazil (mailing address not billing)
SELECT Invoice.InvoiceId, Invoice.Total, Customer.FirstName, Customer.LastName, Customer.Country
FROM Invoice
JOIN Customer ON Invoice.CustomerId = Customer.CustomerId
WHERE Customer.Country = 'Brazil';

-- Show all invoices together with the name of the sales agent for each one


-- Which sales agent made the most sales in 2009?

-- How many customers are assigned to each sales agent?

-- Which track was purchased the most ing 20010?

-- Show the top three best selling artists.

-- Which customers have the same initials as at least one other customer?



-- ADVACED CHALLENGES
-- solve these with a mixture of joins, subqueries, CTE, and set operators.
-- solve at least one of them in two different ways, and see if the execution
-- plan for them is the same, or different.

-- 1. which artists did not make any albums at all?

-- 2. which artists did not record any tracks of the Latin genre?

-- 3. which video track has the longest length? (use media type table)

-- 4. find the names of the customers who live in the same city as the
--    boss employee (the one who reports to nobody)

-- 5. how many audio tracks were bought by German customers, and what was
--    the total price paid for them?

-- 6. list the names and countries of the customers supported by an employee
--    who was hired younger than 35.


-- DML exercises

-- 1. insert two new records into the employee table.

-- 2. insert two new records into the tracks table.

-- 3. update customer Aaron Mitchell's name to Robert Walter

-- 4. delete one of the employees you inserted.

-- 5. delete customer Robert Walter.