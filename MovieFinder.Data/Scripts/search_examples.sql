-- Все фильмы с Леонардо
SELECT Movies.*
FROM Movies
JOIN ActorMovie ON Movies.Id = ActorMovie.MoviesId
JOIN Actors ON Actors.Id = ActorMovie.ActorsId
WHERE Actors.FirstName LIKE '%Леонардо%';

-- Получить все фильмы с жанром, продолжительностью и описанием
SELECT Id, Title, Genre, Duration, Description
FROM Movies;

--Фильмы по жанру
SELECT *
FROM Movies
WHERE Genre = 'SciFi';

--Поиск по имени и жанру
SELECT Movies.*
FROM Movies
JOIN ActorMovie ON Movies.Id = ActorMovie.MoviesId
JOIN Actors ON Actors.Id = ActorMovie.ActorsId
WHERE Actors.FirstName LIKE '%Леонардо%'
  AND Movies.Genre = 'SciFi';

--Фильмы с более чем одним актером
SELECT Movies.Id, Movies.Title, COUNT(ActorMovie.ActorsId) AS ActorCount
FROM Movies
JOIN ActorMovie ON Movies.Id = ActorMovie.MoviesId
GROUP BY Movies.Id
HAVING COUNT(ActorMovie.ActorsId) > 1;



