# Application de Gestion de Stock

Ceci est une application de bureau pour la gestion de stock, développée en Windows Forms.

## Structure du projet

- `StockApp.csproj`: Fichier projet C#.
- `Program.cs`: Point d'entrée de l'application Windows Forms.
- `MainForm.cs`: Formulaire principal de l'application, contenant la navigation par onglets et la barre de recherche.
- `Data/Entities`: Contient les classes du modèle de données (Client, Piece, Facture, etc.).
- `Views/ClientForms`: Contient les formulaires pour la gestion des clients:
  - `ClientManagementForm.cs`: Formulaire principal avec la liste des clients et les boutons d'action
  - `ClientDetailsForm.cs`: Formulaire de détail pour ajouter ou modifier un client
- Autres dossiers de formulaires à créer: `FournisseurForms`, `PieceForms`, etc.

## Diagramme de classes (Mermaid)

(Le diagramme fourni par l'utilisateur sera intégré ou référencé ici ultérieurement)

```mermaid
classDiagram
    class Role {
        <<enumeration>>
        ADMIN
        OPERATEUR
    }

    class Facture {
        <<abstract>>
        +UUID id
        +LocalDateTime date
        +totalHT() Decimal
        +totalTTC() Decimal
    }

    Facture <|-- FactureVente
    FactureVente "1" -- "1" Client
    FactureVente "1" *-- "*" LigneFacture

    Facture <|-- FactureAchat
    FactureAchat "1" -- "1" Fournisseur
    FactureAchat "1" *-- "*" LigneFacture

    class LigneFacture {
        +UUID id
        +int quantite
        +Decimal prixUnitaireHT
        +Decimal remisePct
    }
    LigneFacture "*" -- "1" Piece

    class Client {
        +UUID id
        +String nom
        +String prenom
        +String matFiscal
        +String adresse
        +String telephone
        +Decimal credit
    }

    class Fournisseur {
        +UUID id
        +String nom
        +String prenom
        +String matFiscal
        +String adresse
        +String telephone
        +Decimal credit
    }

    class Piece {
        +UUID id
        +String marque
        +String reference
        +Decimal prixAchatHT
        +Decimal prixVenteHT
        +int stock
        +int seuilAlerte
        +Decimal tvaPct
    }

    class MouvementStock {
        +UUID id
        +LocalDateTime date
        +String type
        +int quantite
        +note "type peut être ENTREE ou SORTIE"
    }
    MouvementStock "*" -- "1" Piece
    MouvementStock "*" -- "0..1" Facture : refFacture

    class User {
        +UUID id
        +String username
        +String passwordHash
        +String prenom
        +String nom
        +Role role
        +boolean actif
    }
```

## Fonctionnalités implémentées

- Affichage de la liste des clients
- Ajout d'un nouveau client
- Modification d'un client existant
- Suppression d'un client

## Prochaines étapes

1. Implémenter les formulaires pour les autres entités (Fournisseurs, Pièces, etc.)
2. Ajouter la persistance des données (base de données)
3. Mettre en place les règles métier (calculs, validations, etc.) 