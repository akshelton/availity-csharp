-- Task 7 B
-- Write a SQL query that will show the total value of all orders each customer has placed
-- in the past six months. Any customer without any orders should show a $0 value.

SELECT DISTINCT
    c.CustID
    ,c.FirstName
    ,c.LastName
    ,CONCAT('$', IFNULL(SUM(ol.Cost * ol.Quantity) over (partition by c.CustID), 0)) as Total
FROM Customer as c
    LEFT JOIN `Order` o on c.CustID = o.CustomerID
    LEFT JOIN `OrderLine` as ol on o.OrderId = ol.OrdID
WHERE o.OrderDate > DATE_ADD(NOW(), INTERVAL -6 MONTH);