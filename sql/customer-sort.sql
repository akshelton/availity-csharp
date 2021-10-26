-- Task 7 A
-- Write a SQL query that will produce a reverse-sorted list (alphabetically by name) of customers
-- (first and last names) whose last name begins with the letter 'S'
SELECT *
FROM Customer as c
WHERE c.LastName like 's%'
ORDER BY c.LastName DESC;