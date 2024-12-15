# LogistiQ - Michał Garnuch
# MyCSharpProject

## Opis projektu
Aplikacja w C# z bazą danych SQL.

## Wymagania
- Visual Studio 2022 (lub nowsze)
- SQL Server Management Studio (SSMS) lub inny serwer SQL

## Instalacja
1. Pobierz repozytorium.
2. Wykonaj plik `script.sql` w SQL Server, aby utworzyć bazę danych.
3. Ustaw połączenie do bazy danych w pliku `appsettings.json`:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=NAZWA_SERWERA;Database=NAZWA_BAZY;User Id=USER;Password=HASLO;"
     }
   }
