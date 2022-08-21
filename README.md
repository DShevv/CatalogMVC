# CatalogMVC

База данных: MS SQL
Язык программирования: C# ASP.Net Core
WEB интерфейс: HTML, CSS, JavaScript (Bootstrap, JQuery)

Приготовления перед запуском:
  1) Создать пустую базу данных;
  2) Скопировать ConnectionString;
  3) Вставить её в поле "DefaultConnection" файла "appsettings.json";
  4) В консоли диспетчера пакетов NuGet ввести 
  ```
  Update-Database
  ```
  5) Для генерации данных в консоли разработчика вводим следующую строку 
  ```
  dotnet run seeddata
  ```
  
Данные для авторизации:
  Простой пользователь - Логин: user, Пароль: Coding@1234?
  Продвинутый пользователь - Логин: advanced, Пароль: Coding@1234?
  Администратор - Логин: admin, Пароль: Coding@1234?
  
  
