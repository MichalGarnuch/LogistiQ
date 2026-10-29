# LogistiQ: komenda WPF respektująca warunek wykonania

Branch: `wersja-inzynierska-2026`. Stan bazowy: `master` / `bc8faebc1798348961675a118e7f089649f8b50c`. Data wykonania: 29.10.2026.

To autorski wariant rozwojowy konkretnego fragmentu projektu. Branch zachowuje pełne drzewo kodu z wersji bazowej i zawiera opisane poniżej zmiany. [Wcześniejszy audyt](https://github.com/MichalGarnuch/LogistiQ/tree/audyt-edukacyjny-2026) jest na osobnym branchu. Nie wykonywano merge ani wdrożenia.

## Co było niespójne

Komenda w MVVM ma dwa kontrakty: `CanExecute` mówi, czy operacja jest dostępna, a `Execute` ją wykonuje. Jeśli `Execute` ignoruje pierwszy kontrakt, bezpośrednie wywołanie może ominąć stan przycisku. Jeśli `CanExecuteChanged` nigdy nie dociera do WPF, interfejs może pokazywać nieaktualną dostępność.

W `LogistiQ/Helper/BaseCommand.cs` wykonanie sprawdza teraz `CanExecute(parameter)` przed wywołaniem delegata. Zdarzenie jest przekazywane do `CommandManager.RequerySuggested`; dodanie i usunięcie handlera są symetryczne. Poprawiłem opis mechanizmu oraz zapisałem zmieniany plik w UTF-8, aby polskie komentarze były czytelne dla współczesnych narzędzi.

## Jak to działa w WPF

WPF podczas swoich cykli wejścia pyta ponownie o możliwość wykonania. Jeżeli stan zmieni się poza takim cyklem, kod modelu widoku może użyć `CommandManager.InvalidateRequerySuggested()`. Nie należy wywoływać go stale przy każdym odświeżeniu całego okna. Przy komendach wymagających precyzyjnych powiadomień rozważyłbym własne `RaiseCanExecuteChanged`, wywoływane przy zmianie właściwych właściwości.

To nie jest zabezpieczenie operacji bazodanowej: uprawnienia i walidacja muszą istnieć również w warstwie usługi. Stan UI może zmienić się pomiędzy oceną komendy a faktycznym zapisem. W przyszłych komendach asynchronicznych potrzebne będą blokada ponownego wykonania, anulowanie, jawny stan błędu i powrót na dispatcher. `async void` w delegacie Action nie jest docelową implementacją tego kontraktu.

## Weryfikacja docelowa

Projekt wymaga Windows, .NET Framework 4.8 i WPF. Na tym systemie należy zbudować solucję i sprawdzić: komenda z warunkiem false nie wykonuje delegata, przejście false→true odświeża przycisk, usunięcie subskrypcji nie pozostawia reakcji oraz zmiana stanu z operacji tła wraca przez dispatcher.

Nie dodaję imitacji `CommandManager` jako „przechodzącego testu WPF” na Linuksie. Taki test sprawdzałby atrapę, a nie mechanizm frameworka. Zakres tej wersji jest niewielki i czytelny w diffie; pełny kontrakt UI wymaga natywnego uruchomienia.

## Weryfikacja wykonana w tej sesji

| Sprawdzenie | Wynik i znaczenie |
|---|---|
| Przegląd ścieżek Execute/CanExecute i symetrii zdarzeń | WYKONANY: zmiana źródła i komentarzy |
| Build i test WPF na Windows/.NET Framework 4.8 | NIEWYKONANY: brak natywnego środowiska |

### Granice weryfikacji

Nie ma wykonanego testu runtime WPF. Wersja nie rozwiązuje asynchronicznego wykonywania komend ani autoryzacji operacji w usługach; wymaga weryfikacji na Windows przed użyciem.

## Mapa zmian

- [`LogistiQ/Helper/BaseCommand.cs`](LogistiQ/Helper/BaseCommand.cs)
