--1 列出各產品的供應商名稱
SELECT
	p.ProductName,
	(SELECT
		s.CompanyName
	FROM Suppliers s
	WHERE p.SupplierID = s.SupplierID
	) AS SupplierCompanyName
FROM Products p

--2 列出各產品的種類名稱
SELECT
	p.ProductName,
	(SELECT
		c.CategoryName
	FROM Categories c
	WHERE p.CategoryID = c.CategoryID
	) AS CategoryName
FROM Products p

--3 列出各訂單的顧客名字
SELECT
	OrderID,
	CustomerID
FROM Orders

--4 列出各訂單的所負責的物流商名字以及員工名字
SELECT
	OrderID,
	(
		SELECT
			e.FirstName + ' ' + e.LastName
		FROM Employees e
		WHERE e.EmployeeID = o.EmployeeID
	) AS EmployeeName,
	(
		SELECT
			s.CompanyName
		FROM Shippers s
		WHERE s.ShipperID = o.ShipVia
	) AS ShipperCompanyName
FROM Orders o
ORDER BY o.OrderID

--5 列出1998年的訂單
SELECT
	*
FROM Orders
WHERE YEAR(OrderDate) = 1998

--6 各產品，UnitsInStock < UnitsOnOrder 顯示'供不應求'，否則顯示'正常'
SELECT
	IIF(UnitsInStock <= UnitsOnOrder,'供不應求','正常') AS 供需狀態,
	*
FROM Products

--7 取得訂單日期最新的9筆訂單
SELECT TOP 9
	*
FROM Orders
ORDER BY  OrderDate DESC

--8 產品單價最便宜的第4~8名
SELECT
	*
FROM Products
ORDER BY UnitPrice
OFFSET 3 ROWS
FETCH NEXT 5 ROWS ONLY

--9 列出每種類別中最高單價的商品
SELECT
	*
FROM Products p
WHERE UnitPrice =
(
	SELECT
		MAX(UnitPrice)
	FROM Products
	WHERE CategoryID = p.CategoryID 
)
ORDER BY UnitPrice DESC

--10 列出每個訂單的總金額
SELECT
	*,
	(UnitPrice * Quantity * ( 1 - Discount)) AS TotalMoney
FROM [Order Details]

--11 列出每個物流商送過的訂單筆數
SELECT
	o1.ShipVia,
	(
	SELECT
		COUNT(OrderID)
	FROM Orders o2
	WHERE o2.ShipVia = o1.ShipVia
	) AS OrderShipCount
FROM Orders o1
GROUP BY o1.ShipVia

--12 列出被下訂次數小於9次的產品
SELECT
	od.ProductID,
	(
	SELECT
		ProductName
	FROM Products p
	WHERE p.ProductID = od.ProductID
	) AS ProductName
FROM [Order Details] od
GROUP BY od.ProductID
HAVING COUNT(od.ProductID) < 9

-- (13、14、15請一起寫)
--13 新增物流商(Shippers)：
-- 公司名稱: 青杉人才，電話: (02)66057606
-- 公司名稱: 青群科技，電話: (02)14055374

--14 方才新增的兩筆資料，電話都改成(02)66057606，公司名稱結尾加'有限公司'

--15 刪除剛才兩筆資料

--13~15題答案請詳homework_0712_131415.sql檔案
CREATE OR ALTER PROC AddShippers(
	@companyName nvarchar(40),
	@phone nvarchar(24)
	)
AS
BEGIN
	INSERT INTO Shippers(CompanyName,Phone)
	VALUES (@companyName,@phone)

	SELECT * FROM Shippers
	WHERE ShipperID = @@IDENTITY

	SELECT * FROM Shippers
END
GO


EXEC AddShippers '青杉人才','(02)66057606'
EXEC AddShippers '青群科技','(02)14055374'

UPDATE Shippers
SET
	CompanyName = '青杉人才有限公司',
	Phone = '(02)66057606'
WHERE CompanyName = '青杉人才'

UPDATE Shippers
SET
	CompanyName = '青群科技有限公司',
	Phone = '(02)66057606'
WHERE CompanyName = '青群科技'

SELECT * FROM Shippers

DELETE FROM Shippers
WHERE CompanyName LIKE '%有限公司'
