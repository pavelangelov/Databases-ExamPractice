SELECT TOP(5) p.Price, YEAR(p.BirthDate) AS [BirthYear], p.Breed FROM Pets p
	WHERE YEAR(p.BirthDate) >= 2012
ORDER BY p.Price DESC