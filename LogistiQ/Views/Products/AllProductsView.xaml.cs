using System; // Podstawowe typy danych oraz obsługa wyjątków
using System.Collections.Generic; // Kolekcje generyczne, np. List<>
using System.Linq; // Funkcje LINQ do zapytań do kolekcji
using System.Text; // Manipulacja tekstem
using System.Windows; // Główne klasy WPF, w tym `Window`, `Application`
using System.Windows.Controls; // Kontrolki WPF, np. `Button`, `Label`
using System.Windows.Data; // Wiązanie danych (Data Binding)
using System.Windows.Documents; // Obsługa dokumentów w WPF, np. `FlowDocument`
using System.Windows.Input; // Obsługa wejścia, np. mysz, klawiatura
using System.Windows.Media; // Grafika i kolory w WPF
using System.Windows.Media.Imaging; // Bitmapy i obrazy w WPF
using System.Windows.Navigation; // Nawigacja w aplikacji WPF
using System.Windows.Shapes; // Podstawowe kształty, np. linie, prostokąty
using LogistiQ.Views.BaseWorkspace;

namespace LogistiQ.Views.Products
{
    /// <summary>
    /// Klasa logiczna dla pliku XAML: WszystkieTowaryView.xaml - definiuje logikę za kodem dla widoku 
    /// 'Wszystkie Towary'
    /// </summary>
    public partial class AllProductsView : AllViewBase
    {
        // Konstruktor klasy WszystkieTowaryView, inicjalizujący komponenty widoku
        public AllProductsView()
        {
            InitializeComponent(); // Inicjalizacja komponentów zdefiniowanych w pliku XAML
        }
    }
}

/*
Wyjaśnienie w kontekście projektu:

1. **Klasa `WszystkieTowaryView`** - Jest to klasa reprezentująca widok użytkownika, który wyświetla 
wszystkie towary. Dziedziczy po `UserControl`, co oznacza, że jest to kontrolka użytkownika, którą można 
osadzić w innych częściach aplikacji.

2. **Metoda `InitializeComponent()`** - Ta metoda jest automatycznie generowana przez WPF i odpowiedzialna 
za inicjalizację wszystkich elementów interfejsu użytkownika zdefiniowanych w pliku XAML 
(`WszystkieTowaryView.xaml`). Dzięki temu wszystkie kontrolki, które są zdefiniowane w XAML, są gotowe 
do użycia w kodzie.

3. **Przestrzenie nazw** - Importowane przestrzenie nazw (`using System.Windows.Controls`, itp.) są 
niezbędne do korzystania z różnych komponentów WPF, takich jak kontrolki (`UserControl`, `Label`), obsługa 
wejścia (`Input`), czy grafika (`Media`). Te przestrzenie zapewniają dostęp do klas i metod, które 
pozwalają tworzyć i zarządzać interfejsem użytkownika.

4. **Komentarz XML (`/// <summary>`)** - Komentarze dokumentacyjne (`///`) są używane do opisania klasy 
oraz jej funkcjonalności, co ułatwia zrozumienie celu klasy i jej działania dla innych programistów.

5. **Architektura MVVM** - `WszystkieTowaryView` jest częścią warstwy View we wzorcu MVVM, co oznacza, że 
odpowiada za prezentację danych użytkownikowi. Logika i dane są zarządzane przez odpowiedni `ViewModel`, 
co zapewnia separację odpowiedzialności i pozwala na lepszą organizację kodu oraz jego łatwiejsze 
testowanie.
*/
