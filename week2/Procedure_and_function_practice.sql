CREATE PROCEDURE GetSongOfGenre
    @Genre VARCHAR(20)
AS
BEGIN
    SELECT Track.Name AS SongName, Genre.Name AS Genre
    FROM Track
    JOIN Genre ON Track.GenreId = Genre.GenreId
    WHERE Genre.Name = @Genre;
END;

EXEC GetSongOfGenre @Genre = 'Jazz';

GO
ALTER FUNCTION dbo.GetInvoiceTotalFromCountryForYear
(
    @Country VARCHAR(20),
    @Year INT
)
RETURNS TABLE
AS
RETURN
(
    SELECT SUM(total) AS InvoiceTotal, BillingCountry
    FROM Invoice
    WHERE BillingCountry = @Country AND YEAR(Invoice.InvoiceDate) = @Year
    GROUP BY BillingCountry
);
GO

SELECT * FROM dbo.GetInvoiceTotalFromCountryForYear('Germany', 2009)

