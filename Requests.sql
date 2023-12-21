--1. Вывести менеджеров у которых имеется номер телефона

SELECT * FROM Managers WHERE Phone IS NOT NULL;


--2. Вывести кол-во продаж за 20 июня 2021

SELECT COUNT(*) AS SalesCount
FROM Sells
WHERE Date = '2021-06-20';


--3. Вывести среднюю сумму продажи с товаром 'Фанера'

SELECT AVG(Sum) AS AverageSaleAmount
FROM Sells
WHERE ID_Prod = (SELECT ID FROM Products WHERE Name = 'Фанера');


--4. Вывести фамилии менеджеров и общую сумму продаж для каждого с товаром 'ОСБ'

SELECT m.Fio AS ManagerName, SUM(s.Sum) AS TotalSalesAmount
FROM Managers m
JOIN Sells s ON m.ID = s.ID_Manag
WHERE s.ID_Prod = (SELECT ID FROM Products WHERE Name = 'ОСБ')
GROUP BY m.Fio;


--5. Вывести менеджера и товар, который продали 22 августа 2021

SELECT m.Fio AS ManagerName, p.Name AS SoldProduct 
FROM Managers m
JOIN Sells s ON m.ID = s.ID_Manag
JOIN Products p ON s.ID_Prod = p.ID
WHERE s.Date = '2021-08-22';


--6. Вывести все товары, у которых в названии имеется 'Фанера' и цена не ниже 1750

SELECT * FROM Products WHERE Name LIKE '%Фанера%' AND Cost >= 1750;


--7. Вывести историю продаж товаров, группируя по месяцу продажи и наименованию товара

SELECT MONTH(Date) AS SaleMonth, Name AS ProductName, SUM(Count) AS TotalCount
FROM Sells
JOIN Products ON Sells.ID_Prod = Products.ID
GROUP BY MONTH(Date), Name;


--8. Вывести количество повторяющихся значений и сами значения из таблицы 'Товары', где количество повторений больше 1.

SELECT Name, COUNT(*) AS Repetitions
FROM Products
GROUP BY Name
HAVING COUNT(*) > 1;
