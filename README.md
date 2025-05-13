# Application de Gestion de Stock

Application de bureau pour la gestion de stock, développée en C# .NET Windows Forms et utilisant Entity Framework Core avec SQLite comme base de données.

## Table des matières

1. [Vue d'ensemble](#vue-densemble)
2. [Architecture du projet](#architecture-du-projet)
3. [Modèle de données](#modèle-de-données)
4. [Entity Framework Core et accès aux données](#entity-framework-core-et-accès-aux-données)
5. [Authentification et sécurité](#authentification-et-sécurité)
6. [Interface utilisateur](#interface-utilisateur)
7. [Guide d'utilisation](#guide-dutilisation)
8. [Diagrammes](#diagrammes)

## Vue d'ensemble

Cette application permet la gestion complète d'un stock de pièces automobiles, incluant :
- Gestion des clients et fournisseurs
- Gestion du catalogue de pièces
- Gestion des factures d'achat et de vente
- Suivi des mouvements de stock
- Gestion des utilisateurs avec différents niveaux d'accès

L'application utilise une architecture moderne basée sur:
- C# .NET 8.0
- Windows Forms avec la bibliothèque d'interface Krypton pour un design moderne
- Entity Framework Core 8 pour l'accès aux données
- SQLite comme moteur de base de données locale

## Architecture du projet

```mermaid
graph TD
    A[Program.cs] --> B[MainForm.cs]
    A --> C[Configuration des services]
    C --> D[DbContext et Repositories]
    B --> E[Formulaires spécifiques]
    E --> F[Clients]
    E --> G[Fournisseurs]
    E --> H[Pièces]
    E --> I[Factures]
    E --> J[Mouvements de stock]
    E --> K[Utilisateurs]
    D --> L[Base de données SQLite]
    
    style A fill:#f9f,stroke:#333,stroke-width:2px
    style B fill:#bbf,stroke:#333,stroke-width:2px
    style C fill:#dfd,stroke:#333,stroke-width:2px
    style D fill:#dfd,stroke:#333,stroke-width:2px
    style L fill:#fdd,stroke:#333,stroke-width:2px
```

Le projet est structuré comme suit :

- **Program.cs** : Point d'entrée de l'application, configure les services (DI) et lance l'application
- **MainForm.cs** : Formulaire principal avec navigation par onglets vers les différentes fonctionnalités
- **Data/** : Contient tout ce qui est lié à l'accès aux données
  - **Entities/** : Classes des entités du modèle de données
  - **Repositories/** : Implémentation du pattern Repository pour l'accès aux données
  - **Services/** : Services métier
  - **StockContext.cs** : Classe DbContext d'Entity Framework
  - **ServiceCollectionExtensions.cs** : Configuration de l'injection de dépendances
- **Views/** : Formulaires Windows Forms organisés par fonctionnalité
  - **ClientForms/** : Gestion des clients
  - **FournisseurForms/** : Gestion des fournisseurs
  - **PieceForms/** : Gestion des pièces
  - **FactureForms/** : Gestion des factures (achat et vente)
  - **MouvementStockForms/** : Gestion des mouvements de stock
  - **UtilisateurForms/** : Gestion des utilisateurs
  - **LoginForm.cs** : Écran de connexion

## Modèle de données

Le modèle de données est représenté par les classes d'entités suivantes :

```mermaid
classDiagram
    class Role {
        <<enumeration>>
        ADMIN
        OPERATEUR
    }

    class Facture {
        <<abstract>>
        +string Id
        +DateTime Date
        +DateTime DateEcheance
        +List~LigneFacture~ LignesFacture
        +decimal TotalHT()
        +decimal TotalTTC()
    }

    Facture <|-- FactureVente
    FactureVente "1" -- "1" Client : client
    FactureVente "1" *-- "*" LigneFacture

    Facture <|-- FactureAchat
    FactureAchat "1" -- "1" Fournisseur : fournisseur
    FactureAchat "1" *-- "*" LigneFacture

    class LigneFacture {
        +string Id
        +string FactureId
        +string PieceId
        +int Quantite
        +decimal PrixUnitaireHT
        +decimal RemisePct
        +decimal MontantHT()
        +decimal MontantTTC()
    }
    LigneFacture "*" -- "1" Piece

    class Client {
        +string Id
        +string Nom
        +string Prenom
        +string MatFiscal
        +string Adresse
        +string Telephone
        +decimal Credit
        +List~FactureVente~ FacturesVente
    }

    class Fournisseur {
        +string Id
        +string Nom
        +string Prenom
        +string MatFiscal
        +string Adresse
        +string Telephone
        +decimal Credit
        +List~FactureAchat~ FacturesAchat
    }

    class Piece {
        +string Id
        +string Marque
        +string Reference
        +decimal PrixAchatHT
        +decimal PrixVenteHT
        +int Stock
        +int SeuilAlerte
        +decimal TvaPct
        +List~MouvementStock~ MouvementsStock
        +List~LigneFacture~ LignesFacture
    }

    class MouvementStock {
        +string Id
        +string PieceId
        +string? FactureId
        +DateTime Date
        +TypeMouvement Type
        +int Quantite
        +string? Note
    }
    MouvementStock "*" -- "1" Piece
    MouvementStock "*" -- "0..1" Facture : facture associée

    class User {
        +string Id
        +string Username
        +string Password
        +string? PasswordHash
        +string? Salt
        +string Nom
        +string Prenom
        +Role Role
        +bool Actif
        +DateTime? CreatedAt
    }
    
    class TypeMouvement {
        <<enumeration>>
        ENTREE
        SORTIE
    }
```

## Entity Framework Core et accès aux données

### Configuration de la base de données

L'application utilise Entity Framework Core avec SQLite. La base de données est automatiquement créée si elle n'existe pas au démarrage de l'application.

```mermaid
sequenceDiagram
    participant App as Application
    participant SC as ServiceCollection
    participant SCE as ServiceCollectionExtensions
    participant DB as SQLite Database
    participant SC_DB as StockContext
    
    App->>SC: ConfigureServices()
    SC->>SCE: RegisterDataServices()
    SCE->>SC: AddDbContext<StockContext>
    App->>SC: BuildServiceProvider()
    App->>SCE: EnsureDatabaseCreated()
    SCE->>DB: Vérifier si la base existe
    alt Base non existante
        SCE->>SC_DB: Database.EnsureCreated()
        SC_DB->>DB: Créer les tables
        SCE->>DB: Créer l'utilisateur admin
    else Base existante
        SCE->>DB: Vérifier la présence d'utilisateurs test
        opt Utilisateurs manquants
            SCE->>DB: Créer utilisateurs test (admin et opérateur)
        end
    end
```

Le fichier de base de données est stocké dans le dossier AppData de l'utilisateur pour assurer la persistance entre les sessions et faciliter les mises à jour.

### Pattern Repository

L'accès aux données est implémenté en utilisant le pattern Repository :

```mermaid
classDiagram
    class IRepository~T~ {
        <<interface>>
        +Task~IEnumerable~T~~ GetAllAsync()
        +Task~IEnumerable~T~~ FindAsync(Expression predicate)
        +Task~T~ GetByIdAsync(string id)
        +Task AddAsync(T entity)
        +Task UpdateAsync(T entity)
        +Task DeleteAsync(string id)
    }
    
    class Repository~T~ {
        #StockContext _context
        #DbSet~T~ _dbSet
        #IIdGeneratorService _idGenerator
        +Repository(StockContext context, IIdGeneratorService idGenerator)
        +Task~IEnumerable~T~~ GetAllAsync()
        +Task~IEnumerable~T~~ FindAsync(Expression predicate)
        +Task~T~ GetByIdAsync(string id)
        +Task AddAsync(T entity)
        +Task UpdateAsync(T entity)
        +Task DeleteAsync(string id)
    }
    
    IRepository~T~ <|.. Repository~T~
    
    class IClientRepository {
        <<interface>>
        +Task~Client~ GetWithFacturesAsync(string id)
    }
    
    class ClientRepository {
        +ClientRepository(StockContext context, IIdGeneratorService idGenerator)
        +Task~Client~ GetWithFacturesAsync(string id)
    }
    
    Repository~Client~ <|-- ClientRepository
    IClientRepository <|.. ClientRepository
    IRepository~Client~ <|-- IClientRepository
```

Chaque entité possède son propre repository qui hérite de la classe générique `Repository<T>` avec des méthodes spécifiques si nécessaire.

### Génération d'identifiants

L'application utilise un service de génération d'identifiants (`IdGeneratorService`) qui crée des IDs au format `AATCNNNNNN` où :
- `AA` : Année sur 2 chiffres
- `TC` : Code de type d'entité (CL: Client, FR: Fournisseur, PC: Pièce, etc.)
- `NNNNNN` : Numéro séquentiel

```mermaid
sequenceDiagram
    participant R as Repository
    participant IG as IdGeneratorService
    participant E as Entity
    
    R->>IG: GenerateId("CLIENT")
    IG->>IG: GetYearPrefix() -> "23"
    IG->>IG: GetEntityTypeCode("CLIENT") -> "CL"
    IG->>IG: Incrémenter compteur
    IG-->>R: "23CL000001"
    R->>E: Affecter ID
    R->>R: Sauvegarder en base
```

## Authentification et sécurité

L'application implémente un système d'authentification avec gestion des utilisateurs:

```mermaid
flowchart TD
    A[Utilisateur] -->|Saisie identifiants| B[LoginForm]
    B -->|user/user| C[Connexion directe\nRôle OPERATEUR]
    B -->|admin/admin| D[Connexion directe\nRôle ADMIN]
    B -->|Autres identifiants| E{Vérification\nbase de données}
    E -->|OK| F[MainForm]
    E -->|Échec| G[Message d'erreur]
    C -->F
    D -->F
    
    style C fill:#bbf,stroke:#333
    style D fill:#fbb,stroke:#333
    style F fill:#bfb,stroke:#333
    style G fill:#fbb,stroke:#333
```

Sécurité des mots de passe:
- Les mots de passe sont stockés avec un hash SHA-256 et un sel unique par utilisateur
- La classe `AuthService` gère l'authentification et l'enregistrement des utilisateurs
- Support pour les utilisateurs hérités (sans hash) pour assurer la compatibilité

## Interface utilisateur

L'application utilise la bibliothèque Krypton UI pour créer une interface moderne et intuitive:

```mermaid
graph TD
    A[LoginForm] --> B[MainForm]
    B --> C[Navigation par onglets]
    C --> D[ClientManagementForm]
    C --> E[FournisseurManagementForm]
    C --> F[PieceManagementForm]
    C --> G[FactureVenteManagementForm]
    C --> H[FactureAchatManagementForm]
    C --> I[MouvementStockManagementForm]
    C --> J[UtilisateurManagementForm]
    
    D --> D1[ClientDetailsForm]
    E --> E1[FournisseurDetailsForm]
    F --> F1[PieceDetailsForm]
    G --> G1[FactureVenteDetailsForm]
    H --> H1[FactureAchatDetailsForm]
    I --> I1[MouvementStockDetailsForm]
    J --> J1[UtilisateurDetailsForm]
    
    G1 --> K[LigneFactureForm]
    H1 --> K
```

## Guide d'utilisation

### Connexion

L'application dispose de deux comptes prédéfinis :
- **Administrateur** : 
  - Identifiant : `admin`
  - Mot de passe : `admin`
  - Accès complet à toutes les fonctionnalités
- **Opérateur** :
  - Identifiant : `user`
  - Mot de passe : `user`
  - Accès limité (pas de suppression ni gestion des utilisateurs)

### Fonctionnalités principales

- **Clients et Fournisseurs** : Gestion des données de contact et suivi des crédits
- **Pièces** : Catalogue des pièces avec gestion du stock et seuil d'alerte
- **Factures** : Création de factures d'achat (fournisseurs) et de vente (clients)
- **Mouvements de stock** : Suivi des entrées et sorties, avec ou sans facture associée
- **Utilisateurs** : Gestion des comptes et droits d'accès (admin uniquement)

## Diagrammes

### Flux de travail typique

```mermaid
sequenceDiagram
    actor U as Utilisateur
    participant A as Application
    participant DB as Base de données
    
    U->>A: Connexion
    A->>DB: Vérifier identifiants
    DB-->>A: Utilisateur authentifié
    A-->>U: Afficher MainForm
    
    alt Création d'une facture de vente
        U->>A: Sélectionner onglet Factures Vente
        U->>A: Cliquer sur Nouveau
        A-->>U: Ouvrir FactureVenteDetailsForm
        U->>A: Sélectionner un client
        U->>A: Ajouter une ligne de facture
        A-->>U: Ouvrir LigneFactureForm
        U->>A: Sélectionner une pièce
        U->>A: Définir quantité et remise
        U->>A: Valider ligne
        A-->>U: Retour au formulaire facture
        U->>A: Ajouter d'autres lignes si nécessaire
        U->>A: Enregistrer la facture
        A->>DB: Sauvegarder facture
        A->>DB: Mettre à jour stock des pièces
        A->>DB: Créer mouvements de stock
        DB-->>A: Confirmation
        A-->>U: Afficher liste des factures mise à jour
    end
```

### Architecture de l'accès aux données

```mermaid
flowchart TD
    A[Forms] --> B[Services]
    B --> C[Repositories]
    C --> D[StockContext]
    D --> E[(SQLite DB)]
    
    style A fill:#bbf,stroke:#333
    style B fill:#dfd,stroke:#333
    style C fill:#dfd,stroke:#333
    style D fill:#fdd,stroke:#333
    style E fill:#ddd,stroke:#333
```

Cette architecture en couches assure une séparation claire des responsabilités et facilite la maintenance de l'application.

## Migrations de base de données

L'application utilise la fonctionnalité `EnsureCreated()` d'Entity Framework pour créer la base de données au premier lancement, puis vérifie et ajoute des colonnes manquantes (comme `Salt` et `CreatedAt`) pour les bases existantes.

Pour les modifications plus importantes du schéma, il faudrait implémenter des migrations complètes via la commande:
```
dotnet ef migrations add [NomMigration]
``` 