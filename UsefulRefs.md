# EF. Работа с миграциями в DAL: как получить контекст
Наименование | Ссылка
-------|-----
Основа | https://medium.com/oppr/net-core-using-entity-framework-core-in-a-separate-project-e8636f9dc9e5
Доп. 1 | https://docs.microsoft.com/en-us/ef/core/cli/dbcontext-creation?tabs=dotnet-core-cli
Доп. 2 | https://docs.microsoft.com/en-us/ef/core/managing-schemas/migrations/projects?tabs=dotnet-core-cli
Доп. 3 | https://docs.microsoft.com/ru-ru/ef/core/cli/dbcontext-creation?tabs=dotnet-core-cli
Доп. 4 | https://docs.microsoft.com/en-us/aspnet/mvc/overview/getting-started/getting-started-with-ef-using-mvc/migrations-and-deployment-with-the-entity-framework-in-an-asp-net-mvc-application

# Proplems
Topic           |Question       | Reference
----------------|---------------------------|-----------
Front           | проблема синего select    | https://stackoverflow.com/questions/10484053/change-select-list-option-background-colour-on-hover
С#. Аннотации   | Display (аннотации)       | https://metanit.com/sharp/aspnet5/19.6.php
С#. Аннотации   | description               | https://metanit.com/sharp/aspnet5/10.4.php
ASP. Enums      | вывод списка              | https://stackoverflow.com/questions/388483/how-do-you-create-a-dropdownlist-from-an-enum-in-asp-net-mvc/8375647#8375647
Identity        | Смена пароля1             |  https://metanit.com/sharp/aspnet5/16.8.php
Identity        | Смена пароля2             |  https://docs.microsoft.com/ru-ru/aspnet/web-forms/overview/older-versions-security/admin/recovering-and-changing-passwords-cs
Identity        | Смена пароля3             |  https://docs.microsoft.com/en-us/aspnet/core/security/authentication/accconfirm?view=aspnetcore-5.0&tabs=visual-studio
Logging         | Логирование ASP               |  https://blog.zwezdin.com/2017/asp-net-core-logging/
Logging         | настройка фильтров для своего провайдера  |  https://blog.zwezdin.com/2017/asp-net-core-custom-logging-provider/ 
Logging         | структурированное введение журнала   |  https://docs.microsoft.com/ru-ru/aspnet/core/fundamentals/logging/?view=aspnetcore-5.0
Logging         | ElmahCore       |  https://github.com/ElmahCore/ElmahCore
Logging         | Критика Serilog  |  https://habr.com/ru/post/550582/
Logging         | ElmahCore        |  https://github.com/ElmahCore/ElmahCore
Validation       | FluentValidations     |   https://docs.fluentvalidation.net/en/latest/aspnet.html
Validation         | FormHelper        |  https://github.com/sinanbozkus/FormHelper



# Заметки
Логгирование:
1)appsettings.json - настройка правил
2)подключение провайдеров и настроек в Program (IWebHostBuilder)
3)создание провайдера и пр. (filelogger) и подключение в Startup
4)настройка logger в контроллерах (выбор действий)