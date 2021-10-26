-- Task 7 C
-- Amend the query from the previous question (see customer-query-6-months.sql) to only
-- show customers who have a total order value of more than $100 and less than $500
-- in the past six months.

SELECT distinct
    custId
    ,firstName
    ,lastName
FROM(
    SELECT
        result_custid as custId
        ,result_firstname as firstName
        ,result_lastname as lastName
        ,result_total as total
    FROM(
        SELECT
            c.CustID as result_custid
            ,c.FirstName as result_firstname
            ,c.LastName as result_lastname
            ,IFNULL(SUM(ol.Cost * ol.Quantity) over (partition by o.OrderId) ,0) as result_total
        FROM Customer as c
            LEFT JOIN `Order` o on c.CustID = o.CustomerID
            LEFT JOIN `OrderLine` as ol on o.OrderId = ol.OrdID

        WHERE o.OrderDate > DATE_ADD(NOW(), INTERVAL -6 MONTH)
        ) as innerquery

    HAVING result_total BETWEEN 100 AND 500
    ) as outerquery



