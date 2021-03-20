Projekt utworzony na podstawie https://github.com/jasontaylordev/CleanArchitecture
informacje o wzorcu: https://www.youtube.com/watch?v=dK4Yb6-LxAk
setup:
1. baza danych
instalacja postgresql 13: https://www.postgresql.org/download
nalezy utworzyc uzytkownika test_user, haslo: test (albo dowolnego innego, wazne zeby byl taki sam jak w connection stringu)
i nadac mu uprawnienia do tworzenia baz danych (mozna to zrobic przez pgadmin)

2. narzędzia ef
    1. wchodzimy do folderu Infrastructure 
    2. wykonujemy dotnet tool restore

3. migracje
Wszystkie operacje na migracjach wykonujemy z wiersza polecen z poziomu projektu Infrastructure
dodawanie migracji:
dotnet ef migrations add "SampleMigration" --startup-project ../WebUI
usuwanie ostatniej migracji:
dotnet ef migrations remove --startup-project ../WebUI

4. uruchomienie projektu (zmieni się po dodaniu 2 aplikacji frontendowych)
    1. w WebUI/ClientApp npm install i npm start (uruchomienie frontendu)
    2.w WebUI dotnet run (uruchomienie serwera)
