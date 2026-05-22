# FitAura
W dzisiejszych czasach bardzo popularne jest prowadzenie odpowiedniej diety oraz reguralne
sprawdzanie podstawowych pomiarów medycznych. 

Głównym celem projektu jest udostępnienie platformy, która pozwoli w prosty sposób
zapisywać posiłki, które spożyliśmy w ciągu dnia oraz zapisywać nasze aktywności w ciągu
dnia tj. spalone kalorie, liczba kroków, jakość snu oraz pomiary medyczne.



## Uruchomienie projektu
| Technologia / Pakiet | Wersja | Oficjalna strona |
| :--- | :--- | :--- |
| **.NET MAUI** | 10.0.20 | [dotnet.microsoft.com](https://dotnet.microsoft.com/apps/maui) |
| **Entity Framework Core** | 10.0.7 | [learn.microsoft.com](https://learn.microsoft.com/ef/core/) |
| **MudBlazor** | 9.4.0 | [mudblazor.com](https://mudblazor.com/) |
| **Npgsql (EF Core Provider)** | 10.0.1 | [npgsql.org](https://www.npgsql.org/efcore/) |
| **SkiaSharp** | 3.119.2 | [skiasharp.com](https://github.com/mono/SkiaSharp) |
| **ZXing.Net** | 0.16.11 | [github.com/micjahn/ZXing.Net](https://github.com/micjahn/ZXing.Net) |
| **ZXing.Net.Bindings.SkiaSharp**| 0.16.22 | [github.com/Redth/ZXing.Net.Bindings.SkiaSharp](https://github.com/Redth/ZXing.Net.Bindings.SkiaSharp) |


Podaj użyte technologie w przejrzystej postaci (np. tabelka) wraz z linkami do oficjalnych stron. **Kluczowe:** Zawsze podawaj dokładne wersje technologii (np. `.NET 8.0`, `React 18`, `Python 3.12`). Brak konkretnej wersji to częsty powód problemów z uruchomieniem starszych projektów na nowych maszynach.### Wymagania programowe



Wypisz narzędzia niezbędne do zbudowania i uruchomienia projektu w trybie deweloperskim na czystym komputerze. Zwróć uwagę na:

- **System operacyjny** (np. Windows 11, macOS, Linux).- **Środowisko uruchomieniowe / SDK** (np. .NET SDK 8.0, Node.js v20+, JDK 17).- **Silnik bazy danych** (np. PostgreSQL 16, MySQL, SQL Server 2022).- **Dodatkowe narzędzia** (np. Docker, Docker Compose, konkretny menedżer pakietów jak `npm` czy `yarn`).### Proces instalacji



Instrukcja "krok po kroku", która prowadzi za rękę. Zakładaj, że osoba uruchamiająca projekt nie zna użytego frameworka.1. Jak pobrać projekt (np. `git clone <url>`).2. Jak zainstalować zależności (np. `npm install`, `dotnet restore`, `pip install -r requirements.txt`).### Proces konfiguracji



Opisz wszystko to, co programista musi ustawić na swojej maszynie przed pierwszym uruchomieniem:1. **Zmienne środowiskowe:** np. utwórz plik `.env` na podstawie `.env.example` i wpisz tam klucze API.2. **Baza danych:** jak skonfigurować *connection string* (połączenie z bazą).3. **Migracje:** komenda do stworzenia struktury bazy (np. `dotnet ef database update`, `python manage.py migrate`).4. **Dane początkowe (Seed):** jak wygenerować dane testowe i jakie są domyślne dane logowania dla konta administratora (login/hasło).**Uruchomienie projektu w terminalu:** Podaj dokładną komendę, która uruchomi aplikację (np. `dotnet run`, `npm run dev`) wraz z informacją, pod jakim adresem aplikacja będzie dostępna w przeglądarce (np. `http://localhost:5000`).## Uruchomienie projektu (user)



Ta sekcja jest przeznaczona dla użytkownika końcowego, którego nie interesuje kod.- Jeśli to aplikacja webowa: podaj link do opublikowanej aplikacji (zdeployowanej w sieci).- Jeśli to aplikacja desktopowa/mobilna: opisz skąd pobrać gotowy instalator (np. plik `.exe`, `.apk` w zakładce Releases na GitHubie) i jak go zainstalować.- Jakie są wymagania sprzętowe, aby aplikacja działała płynnie?## Podręcznik użytkownika



W tej części skup się na **biznesowej stronie aplikacji** (najlepiej wspomagając się zrzutami ekranu z działającego projektu).- Pokaż ścieżki użytkownika (tzw. *user flow*): "Jak dodać nowy produkt", "Jak opłacić zamówienie", "Jak wygenerować raport".- Wyjaśnij zasady działania najważniejszych funkcji.- Opisz role w systemie (co może zwykły klient, a jakie dodatkowe zakładki widzi administrator).- Opisz przypadki brzegowe jakie system obsługuje np. wpisanie tekstu w pole przeznaczone dla liczb.- Opisz jakie dane system przechowuje i udostępnia.- Pokaż (np. na zrzutach ekranu), jak interfejs dostosowuje się do mniejszych ekranów (responsywność / wersja mobilna).- Wyjaśnij, jak działa najważniejszy mechanizm aplikacji, np. filtrowanie danych, obliczanie sum, czy proces wysyłki formularza.



Każde zdjęcie powinno mieć opis, który wyjaśnia, co jest na zdjęciu.## Plany rozbudowy

- Czego zabrakło w pierwszej wersji projektu?- Jakie funkcjonalności mogłyby powstać w "v2.0" (np. integracja z płatnościami, system powiadomień mailowych)?- Gdzie dostrzegacie potencjał na optymalizację (np. dodanie cache'owania, zmiana bazy danych)?
