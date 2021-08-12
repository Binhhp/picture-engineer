Build dotnet: dotnet build
Run dotnet: dotnet run
Create Controller: 
1. Install aspnet-codegenerator
2. Package: dotnet aspnet-codegenerator controller -name [NameController] -api -actions -outDir Controllers


Migrations
Create migration: 
Package EntityFrameworks
Add-Migration create-table -StartupProject PictureEngineer.Core
Update-Database -StartupProject PictureEngineer.Core

Install Package: dotnet add <ProjectName> package <PackageName>