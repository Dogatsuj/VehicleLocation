# VehicleRental
# Entity Framework Core Commands

This document summarises the main commands used to manage **migrations** and the **database** with **Entity Framework Core**.

---

## Add a migration
Creates a new migration to record changes to the data model.
```bash
dotnet ef migrations add [MigrationName]
```

---

## Update the database
Applies migrations to the database.
```bash
dotnet ef database update
```

---

## Install migration tools
Installs the `dotnet-ef` CLI tool globally on your system.
```bash
dotnet tool install --global dotnet-ef
```

---

## Drop the database
Drops the current database without confirmation.
```bash
dotnet ef database drop --force
```

---

## View the list of migrations
Displays all existing migrations.
```bash
dotnet ef migrations list
```

---

## Revert to a previous migration
Restores the database to a specific migration.
```bash
dotnet ef database update [MigrationName]
```

---

## Remove the last migration
Removes the last migration file (to be done after reverting to a previous version).
```bash
dotnet ef migrations remove
```

---


## Notes
- These commands must be executed in the back folder
- If dotnet ef is not recognised despite installation, check that the file is present in C:/users/[user]/.dotnet/tools/. If so, execute the following command in the back folder
```bash
$env:PATH += ";$env:USERPROFILE\.dotnet\tools"
```
