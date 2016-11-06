SELECT s.Name, COUNT(ps.ProductId) AS [Products] FROM Species s
	JOIN Product_Species ps
	ON s.Id = ps.SpeciesId
	JOIN Products p
	ON p.Id = ps.ProductId
GROUP BY s.Id, s.Name
ORDER BY [Products] 