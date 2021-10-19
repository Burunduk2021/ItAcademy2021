# Entity Framework Core tip-list 
- Add migrations
```EF-console
dotnet ef migrations add [migration Name]
```
```EF-console
dotnet ef migrations add DbInit --output-dir "Database/Migrations
```
- Remove Migration
- Create / Update Database
```EF-console
dotnet ef database update
```
asd ```abc``` ads

CSS: проблема синего select https://stackoverflow.com/questions/10484053/change-select-list-option-background-colour-on-hover

Статьи по атрибутам enums:
display: https://metanit.com/sharp/aspnet5/19.6.php
description: https://metanit.com/sharp/aspnet5/10.4.php
https://stackoverflow.com/questions/388483/how-do-you-create-a-dropdownlist-from-an-enum-in-asp-net-mvc/8375647#8375647
Смена пароля: https://metanit.com/sharp/aspnet5/16.8.php
              https://docs.microsoft.com/ru-ru/aspnet/web-forms/overview/older-versions-security/admin/recovering-and-changing-passwords-cs
Статья по логированию: https://blog.zwezdin.com/2017/asp-net-core-logging/
                       https://blog.zwezdin.com/2017/asp-net-core-custom-logging-provider/   (настройка фильтров для своего провайдера!!! category - при необходимости можно сохранять контроллер)
Структурированное ведение журнала: https://docs.microsoft.com/ru-ru/aspnet/core/fundamentals/logging/?view=aspnetcore-5.0
ElmahCore: https://github.com/ElmahCore/ElmahCore
Критика Serilog: https://habr.com/ru/post/550582/

Логгирование:
1)appsettings.json - настройка правил
2)подключение провайдеров и настроек в Program (IWebHostBuilder)
3)создание провайдера и пр. (filelogger) и подключение в Startup
4)настройка logger в контроллерах (выбор действий)