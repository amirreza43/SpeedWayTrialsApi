SELECT "r"."Id", "r"."CarType", "r"."Model", "r"."Nickname", "r"."OwnerId", "r"."Status", "r"."TopSpeed", "r"."Year", "d"."Id", "d"."Age", "d"."BirthDate", "d"."FirstName", "d"."LastName", "d"."Losses", "d"."Nickname", "d"."Wins"
FROM "RaceCars" AS "r"
LEFT JOIN "Drivers" AS "d" ON "r"."OwnerId" = "d"."Id"
WHERE "r"."Id" = "D49A32C7-402F-458C-9A2F-1C41E57C57FD"
LIMIT 1;

SELECT * FROM RaceCars Where Id = "D49A32C7-402F-458C-9A2F-1C41E57C57FD";