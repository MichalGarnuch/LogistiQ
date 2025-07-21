# LogistiQ

## Wprowadzenie
Aplikacja WPF (.NET Framework 4.8) służąca do zarządzania procesami logistycznymi.  
Dane przechowywane są w bazie SQL Server, a komunikację zapewnia Entity Framework.

## Struktura projektu
- **Helper/** – klasy pomocnicze, m.in. `BaseCommand` do obsługi komend MVVM.
- **Models/**
  - **Entities/** – encje i wygenerowany kontekst `LogistiQ_Entities`.
  - **EntitiesForView/** – modele widokowe (DTO) wykorzystywane w interfejsie.
  - **BusinessLogic/** – logika biznesowa poszczególnych modułów.
- **ViewModels/** – modele widoków zarządzające interakcją między widokiem a logiką.
- **Views/** – pliki XAML tworzące interfejs użytkownika.
- **Raporty/** – przykładowe pliki CSV z generowanymi raportami.
- **script.sql** – skrypt tworzący bazę `LogistiQ_DB`.

## Wymagania
- Visual Studio 2022 lub nowszy
- .NET Framework 4.8 Developer Pack
- SQL Server (np. Express) wraz z SSMS

## Instalacja
1. Sklonuj repozytorium:
   ```bash
   git clone https://github.com/MichalGarnuch/LogistiQ.git
   ```
2. Otwórz `LogistiQ.sln` w Visual Studio.
3. Przy pierwszym kompilowaniu NuGet przywróci wymagane paczki.
4. Za pomocą SSMS uruchom `LogistiQ/script.sql`, aby utworzyć bazę `LogistiQ_DB`.
5. W pliku `LogistiQ/app.config` zmodyfikuj parametr `data source` w connection stringu:
   ```xml
   <add name="LogistiQ_Entities"
        connectionString="... data source=YOUR_SERVER;initial catalog=LogistiQ_DB; ..."
        providerName="System.Data.EntityClient" />
   ```
6. Zbuduj i uruchom aplikację (F5).

## Zrzuty ekranu

![Zrzut ekranu 2025-02-25 223501](https://github.com/user-attachments/assets/b5e1fc2c-6ef0-40b1-b377-6e909df04d1c)

![Zrzut ekranu 2025-02-25 223643](https://github.com/user-attachments/assets/70e26645-33e7-4ac8-832a-9490c9f6d85c)

![Zrzut ekranu 2025-02-25 223718](https://github.com/user-attachments/assets/3f4d2404-4c20-42c7-a1ad-061965aeb37b)

![Zrzut ekranu 2025-02-25 223808](https://github.com/user-attachments/assets/2fd52389-d452-4505-93bf-e0c606263095)

![Zrzut ekranu 2025-02-25 223842](https://github.com/user-attachments/assets/187b3a77-5b7c-42e7-88d8-88658fc4d8a7)

![Zrzut ekranu 2025-02-25 223920](https://github.com/user-attachments/assets/360540ab-0dd8-4ff7-9d2f-de4392e58f51)

## Raporty
Przykładowe raporty w formacie CSV znajdują się w folderze `Raporty/`.  
Możesz wygenerować własne zestawienia w aplikacji i zapisać je w tym samym miejscu.



