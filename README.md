# Hotel reservation system

Projekt utworzony na podstawie https://github.com/jasontaylordev/CleanArchitecture
informacje o wzorcu: https://www.youtube.com/watch?v=dK4Yb6-LxAk
setup: 0.

1. instalacja .NET 5 https://dotnet.microsoft.com/download/dotnet/5.0 2. instalacja nodejs https://nodejs.org/en/

1. baza danych
   instalacja postgresql 13: https://www.postgresql.org/download
   nalezy utworzyc uzytkownika test_user, haslo: test (albo dowolnego innego, wazne zeby byl taki sam jak w connection stringu)
   i nadac mu uprawnienia do tworzenia baz danych (mozna to zrobic przez pgadmin)

1. narzędzia ef

   1. wchodzimy do folderu Infrastructure
   2. wykonujemy dotnet tool restore

1. migracje
   Wszystkie operacje na migracjach wykonujemy z wiersza polecen z poziomu projektu Infrastructure:
   dodawanie migracji:
   dotnet ef migrations add "SampleMigration" --startup-project ../WebUI
   usuwanie ostatniej migracji:
   dotnet ef migrations remove --startup-project ../WebUI

1. uruchomienie projektu (zmieni się po dodaniu 2 aplikacji frontendowych)
   Aplikacja kliencka uruchamia się na na porcie 4200, a hotelowa na 4201
   1. w WebUI/(ClientApp|HotelApp) npm install i npm start (uruchomienie frontendu)
   2. w WebUI dotnet run (uruchomienie serwera)


Access Token do testów hotelu:
$2a$11$M3hY1eNjsXD4PDEuoJGrSOJLLdvfBvTOo3M0SFurlni7GiQVoHMRS
