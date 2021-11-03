# Entity Framework Core tip-list 
- Add migrations
```EF-console
dotnet ef migrations add [migration Name]
```
```EF-console
dotnet ef migrations add DbInit --output-dir "Database/Migrations"
```
- Remove Migration
- Create / Update Database
```EF-console
dotnet ef database update
```