# FitAura
W dzisiejszych czasach bardzo popularne jest prowadzenie odpowiedniej diety oraz reguralne
sprawdzanie podstawowych pomiarów medycznych. 

Głównym celem projektu jest udostępnienie platformy, która pozwoli w prosty sposób
zapisywać posiłki, które spożyliśmy w ciągu dnia oraz zapisywać nasze aktywności w ciągu
dnia tj. spalone kalorie, liczba kroków, jakość snu oraz pomiary medyczne.

## Główne funkcjonalności
- rejestracja / logowanie
- zarządzanie naszym dniem:
  - zapisywanie zjedzonych przez nas posiłków (produktów / przepisów)
  - możliwość prowadzenia dzienniczka aktywność (spalone kalorie, jakość snu, liczba kroków, podstawowe pomiary medyczne)
- obliczanie deficytu kalorycznego na podstawie bieżącej wagi oraz wzrostu
- automatycznie zliczanie spożytej liczby kalorii wciągu dnia oraz makro składników (białka, węgli oraz tłuszczy)
- statystyki z danego okresu:
  - średnie (jakości snu, zjedzonych kalorii, liczby kroków, tętna, cukru)
  - wykres bmi
- posiłki:
  - mogą składać się z przepisu lub produktu
  - możemy podać ile gramów zjedliśmy danego produktu i przeliczone zostaną odpowiednio kalorie oraz makroskładniki
  - mozemy podać ile gramów / bądź kcal chcemy zjeść w danym przepisie i zostanie odpowiednio przeliczone ile składniku danego uzyć
  - podgląd posiłku, nazywanie posiłku
- przepisy
  - tworzenie własnych przepisów
  - edytowanie przepisów
  - zaawansowane wyszukiwanie przepisów poprzez (kategorie, zakres kcal, rosnąco / malejąco, autora przepisu, po nazwie)
- profil użytkownika
  - zmiana danych typu waga, wzrost, hasło

## Innowacyjność
- Przepis przelicza nam składniki potrzebne do ugotowania dania względem spożytych kalorii.
- Ocena jakości snu w danym okresie (statystyka).
 
## Technologie

| Technologia / Pakiet | Wersja | Oficjalna strona |
| :--- | :--- | :--- |
| **.NET MAUI** | 10.0.20 | [dotnet.microsoft.com](https://dotnet.microsoft.com/apps/maui) |
| **Entity Framework Core** | 10.0.7 | [learn.microsoft.com](https://learn.microsoft.com/ef/core/) |
| **.ASP NET CORE** | 10.0.20 | [dotnet.microsoft.com](https://dotnet.microsoft.com/en-us/apps/aspnet) |
| **MudBlazor** | 9.4.0 | [mudblazor.com](https://mudblazor.com/) |
| **Npgsql** | 10.0.1 | [npgsql.org](https://www.npgsql.org/efcore/) |
| **SkiaSharp** | 3.119.2 | [skiasharp.com](https://github.com/mono/SkiaSharp) |
| **ZXing.Net** | 0.16.11 | [github.com/micjahn/ZXing.Net](https://github.com/micjahn/ZXing.Net) |
| **ZXing.Net.Bindings.SkiaSharp**| 0.16.22 | [github.com/Redth/ZXing.Net.Bindings.SkiaSharp](https://github.com/Redth/ZXing.Net.Bindings.SkiaSharp) |
| **PostgreSQL** | 17 | [postgresql.org.pl](https://www.postgresql.org.pl)


## Wymagania do uruchomienia projektu

- **System operacyjny**: Windows 11
- **SDK**: .NET 10
- **Baza Danych**: PostgreSQL 17

## Instrukcja uruchomienia

1. Sklonowanie obu repozytoriów
- git clone https://github.com/xserafineq/FitAura
- git clone https://github.com/xserafineq/FitAuraApi

2. Migracja bazy danych
- dotnet ef database update fitauradb

3. Uruchomienie obu programów za pomocą komend
- (**FitAura**) dotnet run --framework net10.0-windows10.0.19041.0 
- (**FitAuraApi**) dotnet run --launch-profile https                     

  
## Podręcznik użytkownika


### Funkcjonalności

1. Rejestracja
   
![Opis zdjęcia](assets/rejestracja.png)



Każde zdjęcie powinno mieć opis, który wyjaśnia, co jest na zdjęciu.## Plany rozbudowy

- Czego zabrakło w pierwszej wersji projektu?- Jakie funkcjonalności mogłyby powstać w "v2.0" (np. integracja z płatnościami, system powiadomień mailowych)?- Gdzie dostrzegacie potencjał na optymalizację (np. dodanie cache'owania, zmiana bazy danych)?
