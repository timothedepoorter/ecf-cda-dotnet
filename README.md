# EcfCdaDotNet - Application de gestion d'événements

## Description

**EcfCdaDotNet** est une application web de gestion d'événements développée avec ASP.NET. Elle permet de créer, afficher, modifier et supprimer des événements, ainsi que de gérer les participants.

## Fonctionnalités

- Gestion des événements (création, modification, suppression).
- Gestion des participants pour chaque événement.
- Affichage des événements sous forme de tableau avec des options de modification, suppression et visualisation.

*Partie statistiques avec MongoDB non incorporée par manque de temps*

## Technologies utilisées

- **ASP.NET Core** pour la partie backend.
- **Bootstrap** pour la mise en page et l'interface utilisateur.
- **Entity Framework Core** pour la gestion de la base de données relationnelle (SQL Server).

## Lancer le projet 🚀
1. ```git clone https://github.com/timothedepoorter/ecf-cda-dotnet.git```
2. ```cd ecf-cda-dotnet```
3. ```dotnet restore```
4. ```dotnet ef database update```
5. ```dotnet run```

## Diagramme de classe PlantUML :
```
@startuml
entity "Participant" {
  *Id : INT
  Lastname : VARCHAR
  Firstname : VARCHAR
  Age: INT
  Email: VARCHAR
  Phone: VARCHAR
  Gender: VARCHAR
}

entity "Event" {
  *Id : INT
  Start_Date: DATETIME
  End_Date: DATETIME
  Creation_Date: DATETIME
  Location: VARCHAR
}

entity "Participation" {
  *Id_Participant : INT
  *Id_Event : INT
  Registration_Date: DATETIME
}

entity "Event_Statistics" {
   *Id_Event : INT
   Total_Participants : INT
   Average_Age : FLOAT
   Male_Participants : INT
   Female_Participants : INT
}

' Relations
Participant -- "0..*" Participation : ""
Event -- "0..*" Participation : ""
Event -- "1..1" Event_Statistics : ""
Participation -- "1..1" Event_Statistics : ""

@enduml
```
## Wireframes :
Fichier .fig disponible à la racine du repo : ecf-cda-dotnet.fig
