# LogistiQ — przewodnik i audyt edukacyjny

**Stan przeglądu:** 29 października 2026 r.  
**Gałąź bazowa:** `master`  
**Analizowany commit:** `bc8faebc1798348961675a118e7f089649f8b50c`  
**Mapa wszystkich plików śledzonych przez Git:** [1](AUDYT_MAPA_01.md)

> To jest przegląd reprezentatywnych części projektu z inwentaryzacją wszystkich śledzonych plików, a nie pełna, ręczna weryfikacja każdego wiersza ani certyfikat bezpieczeństwa. Mapa jest automatyczna; wymienienie pliku nie oznacza, że został przetestowany lub dokładnie przeczytany. Wyniki odnoszą się do powyższego commita. Inne gałęzie mogą zawierać inne wersje.

## Co robi projekt

Aplikacja WPF/.NET Framework do obsługi procesów logistycznych, z bazą SQL Server.

## Jak czytać kod i jak przepływają dane

[`LogistiQ/Views/`](LogistiQ/Views) zawiera widoki XAML, [`LogistiQ/ViewModels/`](LogistiQ/ViewModels) ich stan i komendy, a [`LogistiQ/Models/BusinessLogic/`](LogistiQ/Models/BusinessLogic) używa encji i kontekstu Entity Framework do operacji biznesowych. [`LogistiQ/Helper/BaseCommand.cs`](LogistiQ/Helper/BaseCommand.cs) wiąże przyciski z akcją.

Podczas czytania zacznij od punktu wejścia, przejdź do funkcji odpowiadającej za żądanie lub działanie użytkownika, a potem do operacji na danych i wyniku. Mapa plików podaje ścieżki i automatycznie rozpoznane nazwy wybranych deklaracji.

## Co jest wartościowe w obecnym rozwiązaniu

Struktura MVVM ułatwia zrozumienie relacji między ekranem, stanem widoku i danymi biznesowymi.

## Ustalenia, luki i proponowany rozwój

- **Potwierdzone — komendy:** [`LogistiQ/Helper/BaseCommand.cs`](LogistiQ/Helper/BaseCommand.cs) deklaruje `CanExecuteChanged`, ale nie udostępnia w tej klasie mechanizmu jego zgłaszania. Stan aktywności przycisków może wymagać jawnego odświeżenia; warto przetestować to w UI.

- **Do rozwoju:** [`LogistiQ/Models/BusinessLogic/OrderB.cs`](LogistiQ/Models/BusinessLogic/OrderB.cs) materializuje zapytanie przed zwróceniem wyniku. Dla większej liczby zamówień warto sprawdzić, czy filtrowanie i paginację można wykonać w SQL.

- **Do sprawdzenia:** dane osobowe i połączenie SQL wymagają ról, ochrony konfiguracji i kontroli dostępu na komputerze; nie zweryfikowano środowiska wdrożenia.

## Sprawdzenia i ich granice

Nie zbudowano aplikacji WPF ani nie uruchomiono SQL Server: brak systemu Windows i wymaganego SDK. Oceniono reprezentatywną logikę i strukturę.

W kopii roboczej dostępne pliki Pythona sprawdzono dodatkowo parserem składni; sprawdzono też składnię odczytanych plików `package.json` i `composer.json`. Nie zastępuje to uruchomienia programu. Kod źródłowy tej gałęzi jest taki sam jak w commicie bazowym; dodane są wyłącznie dokumenty audytu i mapy.

## Jak zaplanować kolejne prace

Najpierw zajmij się punktami oznaczonymi jako krytyczne lub wysokie, następnie ustal, które uwagi wymagają testu na pełnym środowisku. Dla każdej zmiany przygotuj osobną gałąź, konkretny przypadek odtwarzający problem i test rezultatu. Nie traktuj propozycji jako potwierdzenia, że poprawka została już wdrożona.
