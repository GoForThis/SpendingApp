
#Projekt - Aplikacja do zarządzania wydatkami

## Spis treści
* [Ogólne](#ogolne)
* [Technologie](#technologie)
* [Uruchomienie](#uruchomienie)
* [Plan projektu](#plan-projektu)
* [API End Points](#api-end-points)
## Ogólne <a name="ogolne"></a>
Projekt na zaliczenie przedmiotu "Programowanie aplikacji backendowych"
## Technologie <a name="technologie"></a>
Do projektu zostały użyte technologie:

* Framework : DOTNET 6.0 WEB API
* ORM : Entity Framework 6.0
* Dokumentacja: Swagger
* Baza danych: SQLServer 19

## Uruchomienie <a name="uruchomienie"></a>
###Utworzenie bazy danych:
```
$ sqllocaldb create Expenses
$ sqllocaldb start Expenses
$ sqlcmd -S ("localdb)\Expenses"
Po tej komendzie otworzy się cmd shell SQLServera w którym należy wpisać:
$ create database ExpensesDB
$ go
```
## Plan projektu <a name="plan-projektu"></a>
1. Utworzenie aplikacji WEB API 
2. Utworzenie repozytorium git
3. Dodanie potrzebnych paczek nuget do aplikacji
4. Utworzenie lokalnej bazy danych
5. Stworzenie klasy DbContext oraz podłączenie bazy danych do aplikacji za pomocą ORM
6. Dodanie tabel w bazie danych za pomocą ORM
   1. Wydatek
   2. Użytkownik
   3. Rola użytkownika
   4. Kategoria wydatku
7. Dodanie seeda bazy danych z danymi
8. Dodanie modeli dto oraz vm do wyświetlania oraz tworzenia 
9. Dodanie middleware oraz własnych Exception w celu obsługi kodów HTTP
10. Dodanie autoryzacji za pomocą tokenów JWT oraz ich konfiguracja 
11. Dodanie serwisów oraz dodanie interfejsów serwisów do kontenera zależności
12. Dodanie controllerów oraz przetestowanie API
13. Dokumentacja - README
14. Przetestowanie całego projektu 
15. Prezentacja projektu



## API End Points <a name="api-end-points"></a>
#### Wydatek
    1.Dodaj wydatek - POST
    2.Edytuj wydatek - PUT
    3.Usuń wydatek - DELETE
    4.Pokaż wydatek - GET
    5.Pokaż wszystkie wydatki dla danego użytkownika - GET

#### Użytkownik
    1.Rejestracja - POST
    2.Logowanie - POST
    3.Wyświetlenie użytkowników - GET

#### Kategoria wydatku
    1.Dodaj kategorie - POST
    2.Edytuj kategorie - PUT
    3.Usuń kategorie - DELETE
    4.Pokaż kategorie - GET