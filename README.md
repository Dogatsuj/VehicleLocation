# VehiculeLocation
# Commandes Entity Framework Core

Ce document récapitule les principales commandes utilisées pour gérer les **migrations** et la **base de données** avec **Entity Framework Core**.

---

## Ajouter une migration
Crée une nouvelle migration pour enregistrer les modifications du modèle de données.
```bash
dotnet ef migrations add [NomMigration]
```

---

## Mettre à jour la base de données
Applique les migrations à la base de données.
```bash
dotnet ef database update
```

---

## Installer les outils de migrations
Installe l’outil CLI `dotnet-ef` globalement sur votre système.
```bash
dotnet tool install --global dotnet-ef
```

---

## Supprimer (drop) la base de données
Supprime la base de données actuelle sans confirmation.
```bash
dotnet ef database drop --force
```

---

## Voir la liste des migrations
Affiche toutes les migrations existantes.
```bash
dotnet ef migrations list
```

---

## Revenir à une migration antérieure
Restaure la base à une migration spécifique.
```bash
dotnet ef database update [NomMigration]
```

---

## Supprimer la dernière migration
Supprime le dernier fichier de migration (à faire après être revenu sur une version antérieure).
```bash
dotnet ef migrations remove
```

---

## Notes
- Ces commandes doivent être exécutées dans le dossier back
- Si malgré l'installation dotnet ef n'est pas reconu, vérifier que le fichier est bien présent dans C:/users/[utilisateur]/.dotnet/tools/, si oui executé dans le back la commande suivante
```bash
$env:PATH += ";$env:USERPROFILE\.dotnet\tools"
```
