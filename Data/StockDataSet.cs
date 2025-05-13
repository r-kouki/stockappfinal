using System;
using System.Data;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Schema;
using System.Diagnostics.CodeAnalysis;

namespace StockApp.Data
{
    public partial class StockDataSet : DataSet
    {
        private ClientsDataTable tableClients;        private FournisseursDataTable tableFournisseurs;        private PiecesDataTable tablePieces;        private FacturesVenteDataTable tableFacturesVente;        private UsersDataTable tableUsers;

        public StockDataSet()
        {
            this.BeginInit();
            this.InitClass();
            this.EndInit();
        }

        [SuppressMessage("Trimming", "IL2026:Members annotated with 'RequiresUnreferencedCodeAttribute' require dynamic access otherwise can break functionality when trimming application code", Justification = "DataSet serialization is required for the application")]
        protected StockDataSet(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public ClientsDataTable Clients => this.tableClients;        public FournisseursDataTable Fournisseurs => this.tableFournisseurs;        public PiecesDataTable Pieces => this.tablePieces;        public FacturesVenteDataTable FacturesVente => this.tableFacturesVente;        public UsersDataTable Users => this.tableUsers;

        private void InitClass()
        {
            this.DataSetName = "StockDataSet";
            this.Prefix = "";
            this.Namespace = "http://tempuri.org/StockDataSet.xsd";
            this.EnforceConstraints = true;
            this.SchemaSerializationMode = SchemaSerializationMode.IncludeSchema;

            // Initialize tables
            this.tableClients = new ClientsDataTable();
            this.Tables.Add(this.tableClients);
            
            this.tableFournisseurs = new FournisseursDataTable();
            this.Tables.Add(this.tableFournisseurs);
            
            this.tablePieces = new PiecesDataTable();
            this.Tables.Add(this.tablePieces);
            
            this.tableFacturesVente = new FacturesVenteDataTable();            this.Tables.Add(this.tableFacturesVente);                        this.tableUsers = new UsersDataTable();            this.Tables.Add(this.tableUsers);

            // Define relations
            if (this.tableClients.Columns["Id"] != null && this.tableFacturesVente.Columns["ClientId"] != null)
            {
                this.Relations.Add("Clients_FacturesVente",
                    this.tableClients.Columns["Id"],
                    this.tableFacturesVente.Columns["ClientId"]);
            }
        }

        #region ClientsDataTable
        public partial class ClientsDataTable : DataTable
        {
            internal DataColumn columnId;
            internal DataColumn columnNom;
            internal DataColumn columnAdresse;
            internal DataColumn columnTelephone;
            internal DataColumn columnEmail;

            public ClientsDataTable()
            {
                this.TableName = "Clients";
                this.BeginInit();
                this.InitClass();
                this.EndInit();
            }

            internal ClientsDataTable(DataTable table)
            {
                this.TableName = table.TableName;
                this.BeginInit();
                this.InitClass();
                
                if (table.CaseSensitive != table.DataSet.CaseSensitive)
                {
                    this.CaseSensitive = table.CaseSensitive;
                }
                
                if (table.Locale.ToString() != table.DataSet.Locale.ToString())
                {
                    this.Locale = table.Locale;
                }
                
                if (table.Namespace != table.DataSet.Namespace)
                {
                    this.Namespace = table.Namespace;
                }
                
                this.Prefix = table.Prefix;
                this.MinimumCapacity = table.MinimumCapacity;
                
                this.EndInit();
            }

            private void InitClass()
            {
                this.columnId = new DataColumn("Id", typeof(string), null, MappingType.Element);
                this.Columns.Add(this.columnId);
                
                this.columnNom = new DataColumn("Nom", typeof(string), null, MappingType.Element);
                this.Columns.Add(this.columnNom);
                
                this.columnAdresse = new DataColumn("Adresse", typeof(string), null, MappingType.Element);
                this.Columns.Add(this.columnAdresse);
                
                this.columnTelephone = new DataColumn("Telephone", typeof(string), null, MappingType.Element);
                this.Columns.Add(this.columnTelephone);
                
                this.columnEmail = new DataColumn("Email", typeof(string), null, MappingType.Element);
                this.Columns.Add(this.columnEmail);
                
                // Set primary key
                this.Constraints.Add(new UniqueConstraint("Constraint1", new DataColumn[] { this.columnId }, true));
                this.columnId.AllowDBNull = false;
                this.columnId.Unique = true;
            }

            public ClientsRow NewClientsRow()
            {
                return (ClientsRow)this.NewRow();
            }

            protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
            {
                return new ClientsRow(builder);
            }

            protected override Type GetRowType()
            {
                return typeof(ClientsRow);
            }

            public ClientsRow FindById(string id)
            {
                return (ClientsRow)this.Rows.Find(id);
            }

            public void AddClientsRow(ClientsRow row)
            {
                this.Rows.Add(row);
            }

            public ClientsRow AddClientsRow(string id, string nom, string adresse, string telephone, string email)
            {
                ClientsRow rowClientsRow = (ClientsRow)this.NewRow();
                rowClientsRow.Id = id;
                rowClientsRow.Nom = nom;
                rowClientsRow.Adresse = adresse;
                rowClientsRow.Telephone = telephone;
                rowClientsRow.Email = email;
                this.Rows.Add(rowClientsRow);
                return rowClientsRow;
            }
        }

        public partial class ClientsRow : DataRow
        {
            private ClientsDataTable tableClients;

            internal ClientsRow(DataRowBuilder rb) : base(rb)
            {
                this.tableClients = (ClientsDataTable)this.Table;
            }

            public string Id
            {
                get => (string)this[this.tableClients.columnId];
                set => this[this.tableClients.columnId] = value;
            }

            public string Nom
            {
                get => this.IsNomNull() ? string.Empty : (string)this[this.tableClients.columnNom];
                set => this[this.tableClients.columnNom] = value;
            }

            public string Adresse
            {
                get => this.IsAdresseNull() ? string.Empty : (string)this[this.tableClients.columnAdresse];
                set => this[this.tableClients.columnAdresse] = value;
            }

            public string Telephone
            {
                get => this.IsTelephoneNull() ? string.Empty : (string)this[this.tableClients.columnTelephone];
                set => this[this.tableClients.columnTelephone] = value;
            }

            public string Email
            {
                get => this.IsEmailNull() ? string.Empty : (string)this[this.tableClients.columnEmail];
                set => this[this.tableClients.columnEmail] = value;
            }

            public bool IsNomNull() => this.IsNull(this.tableClients.columnNom);
            public void SetNomNull() => this[this.tableClients.columnNom] = Convert.DBNull;

            public bool IsAdresseNull() => this.IsNull(this.tableClients.columnAdresse);
            public void SetAdresseNull() => this[this.tableClients.columnAdresse] = Convert.DBNull;

            public bool IsTelephoneNull() => this.IsNull(this.tableClients.columnTelephone);
            public void SetTelephoneNull() => this[this.tableClients.columnTelephone] = Convert.DBNull;

            public bool IsEmailNull() => this.IsNull(this.tableClients.columnEmail);
            public void SetEmailNull() => this[this.tableClients.columnEmail] = Convert.DBNull;
        }
        #endregion

        #region FournisseursDataTable
        public partial class FournisseursDataTable : DataTable
        {
            internal DataColumn columnId;
            internal DataColumn columnNom;
            internal DataColumn columnAdresse;
            internal DataColumn columnTelephone;
            internal DataColumn columnEmail;

            public FournisseursDataTable()
            {
                this.TableName = "Fournisseurs";
                this.BeginInit();
                this.InitClass();
                this.EndInit();
            }

            internal FournisseursDataTable(DataTable table)
            {
                this.TableName = table.TableName;
                this.BeginInit();
                this.InitClass();
                
                if (table.CaseSensitive != table.DataSet.CaseSensitive)
                {
                    this.CaseSensitive = table.CaseSensitive;
                }
                
                if (table.Locale.ToString() != table.DataSet.Locale.ToString())
                {
                    this.Locale = table.Locale;
                }
                
                if (table.Namespace != table.DataSet.Namespace)
                {
                    this.Namespace = table.Namespace;
                }
                
                this.Prefix = table.Prefix;
                this.MinimumCapacity = table.MinimumCapacity;
                
                this.EndInit();
            }

            private void InitClass()
            {
                this.columnId = new DataColumn("Id", typeof(string), null, MappingType.Element);
                this.Columns.Add(this.columnId);
                
                this.columnNom = new DataColumn("Nom", typeof(string), null, MappingType.Element);
                this.Columns.Add(this.columnNom);
                
                this.columnAdresse = new DataColumn("Adresse", typeof(string), null, MappingType.Element);
                this.Columns.Add(this.columnAdresse);
                
                this.columnTelephone = new DataColumn("Telephone", typeof(string), null, MappingType.Element);
                this.Columns.Add(this.columnTelephone);
                
                this.columnEmail = new DataColumn("Email", typeof(string), null, MappingType.Element);
                this.Columns.Add(this.columnEmail);
                
                // Set primary key
                this.Constraints.Add(new UniqueConstraint("Constraint1", new DataColumn[] { this.columnId }, true));
                this.columnId.AllowDBNull = false;
                this.columnId.Unique = true;
            }

            public FournisseursRow NewFournisseursRow()
            {
                return (FournisseursRow)this.NewRow();
            }

            protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
            {
                return new FournisseursRow(builder);
            }

            protected override Type GetRowType()
            {
                return typeof(FournisseursRow);
            }

            public FournisseursRow FindById(string id)
            {
                return (FournisseursRow)this.Rows.Find(id);
            }

            public void AddFournisseursRow(FournisseursRow row)
            {
                this.Rows.Add(row);
            }

            public FournisseursRow AddFournisseursRow(string id, string nom, string adresse, string telephone, string email)
            {
                FournisseursRow rowFournisseursRow = (FournisseursRow)this.NewRow();
                rowFournisseursRow.Id = id;
                rowFournisseursRow.Nom = nom;
                rowFournisseursRow.Adresse = adresse;
                rowFournisseursRow.Telephone = telephone;
                rowFournisseursRow.Email = email;
                this.Rows.Add(rowFournisseursRow);
                return rowFournisseursRow;
            }
        }

        public partial class FournisseursRow : DataRow
        {
            private FournisseursDataTable tableFournisseurs;

            internal FournisseursRow(DataRowBuilder rb) : base(rb)
            {
                this.tableFournisseurs = (FournisseursDataTable)this.Table;
            }

            public string Id
            {
                get => (string)this[this.tableFournisseurs.columnId];
                set => this[this.tableFournisseurs.columnId] = value;
            }

            public string Nom
            {
                get => this.IsNomNull() ? string.Empty : (string)this[this.tableFournisseurs.columnNom];
                set => this[this.tableFournisseurs.columnNom] = value;
            }

            public string Adresse
            {
                get => this.IsAdresseNull() ? string.Empty : (string)this[this.tableFournisseurs.columnAdresse];
                set => this[this.tableFournisseurs.columnAdresse] = value;
            }

            public string Telephone
            {
                get => this.IsTelephoneNull() ? string.Empty : (string)this[this.tableFournisseurs.columnTelephone];
                set => this[this.tableFournisseurs.columnTelephone] = value;
            }

            public string Email
            {
                get => this.IsEmailNull() ? string.Empty : (string)this[this.tableFournisseurs.columnEmail];
                set => this[this.tableFournisseurs.columnEmail] = value;
            }

            public bool IsNomNull() => this.IsNull(this.tableFournisseurs.columnNom);
            public void SetNomNull() => this[this.tableFournisseurs.columnNom] = Convert.DBNull;

            public bool IsAdresseNull() => this.IsNull(this.tableFournisseurs.columnAdresse);
            public void SetAdresseNull() => this[this.tableFournisseurs.columnAdresse] = Convert.DBNull;

            public bool IsTelephoneNull() => this.IsNull(this.tableFournisseurs.columnTelephone);
            public void SetTelephoneNull() => this[this.tableFournisseurs.columnTelephone] = Convert.DBNull;

            public bool IsEmailNull() => this.IsNull(this.tableFournisseurs.columnEmail);
            public void SetEmailNull() => this[this.tableFournisseurs.columnEmail] = Convert.DBNull;
        }
        #endregion

        #region PiecesDataTable
        public partial class PiecesDataTable : DataTable
        {
            internal DataColumn columnId;
            internal DataColumn columnReference;
            internal DataColumn columnDescription;
            internal DataColumn columnPrixAchatHT;
            internal DataColumn columnPrixVenteHT;
            internal DataColumn columnStock;
            internal DataColumn columnTvaPct;

            public PiecesDataTable()
            {
                this.TableName = "Pieces";
                this.BeginInit();
                this.InitClass();
                this.EndInit();
            }

            internal PiecesDataTable(DataTable table)
            {
                this.TableName = table.TableName;
                this.BeginInit();
                this.InitClass();
                
                if (table.CaseSensitive != table.DataSet.CaseSensitive)
                {
                    this.CaseSensitive = table.CaseSensitive;
                }
                
                if (table.Locale.ToString() != table.DataSet.Locale.ToString())
                {
                    this.Locale = table.Locale;
                }
                
                if (table.Namespace != table.DataSet.Namespace)
                {
                    this.Namespace = table.Namespace;
                }
                
                this.Prefix = table.Prefix;
                this.MinimumCapacity = table.MinimumCapacity;
                
                this.EndInit();
            }

            private void InitClass()
            {
                this.columnId = new DataColumn("Id", typeof(string), null, MappingType.Element);
                this.Columns.Add(this.columnId);
                
                this.columnReference = new DataColumn("Reference", typeof(string), null, MappingType.Element);
                this.Columns.Add(this.columnReference);
                
                this.columnDescription = new DataColumn("Description", typeof(string), null, MappingType.Element);
                this.Columns.Add(this.columnDescription);
                
                this.columnPrixAchatHT = new DataColumn("PrixAchatHT", typeof(decimal), null, MappingType.Element);
                this.Columns.Add(this.columnPrixAchatHT);
                
                this.columnPrixVenteHT = new DataColumn("PrixVenteHT", typeof(decimal), null, MappingType.Element);
                this.Columns.Add(this.columnPrixVenteHT);
                
                this.columnStock = new DataColumn("Stock", typeof(int), null, MappingType.Element);
                this.Columns.Add(this.columnStock);
                
                this.columnTvaPct = new DataColumn("TvaPct", typeof(decimal), null, MappingType.Element);
                this.Columns.Add(this.columnTvaPct);
                
                // Set primary key
                this.Constraints.Add(new UniqueConstraint("Constraint1", new DataColumn[] { this.columnId }, true));
                this.columnId.AllowDBNull = false;
                this.columnId.Unique = true;
            }

            public PiecesRow NewPiecesRow()
            {
                return (PiecesRow)this.NewRow();
            }

            protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
            {
                return new PiecesRow(builder);
            }

            protected override Type GetRowType()
            {
                return typeof(PiecesRow);
            }

            public PiecesRow FindById(string id)
            {
                return (PiecesRow)this.Rows.Find(id);
            }

            public void AddPiecesRow(PiecesRow row)
            {
                this.Rows.Add(row);
            }

            public PiecesRow AddPiecesRow(string id, string reference, string description, decimal prixAchatHT, decimal prixVenteHT, int stock, decimal tvaPct)
            {
                PiecesRow rowPiecesRow = (PiecesRow)this.NewRow();
                rowPiecesRow.Id = id;
                rowPiecesRow.Reference = reference;
                rowPiecesRow.Description = description;
                rowPiecesRow.PrixAchatHT = prixAchatHT;
                rowPiecesRow.PrixVenteHT = prixVenteHT;
                rowPiecesRow.Stock = stock;
                rowPiecesRow.TvaPct = tvaPct;
                this.Rows.Add(rowPiecesRow);
                return rowPiecesRow;
            }
        }

        public partial class PiecesRow : DataRow
        {
            private PiecesDataTable tablePieces;

            internal PiecesRow(DataRowBuilder rb) : base(rb)
            {
                this.tablePieces = (PiecesDataTable)this.Table;
            }

            public string Id
            {
                get => (string)this[this.tablePieces.columnId];
                set => this[this.tablePieces.columnId] = value;
            }

            public string Reference
            {
                get => this.IsReferenceNull() ? string.Empty : (string)this[this.tablePieces.columnReference];
                set => this[this.tablePieces.columnReference] = value;
            }

            public string Description
            {
                get => this.IsDescriptionNull() ? string.Empty : (string)this[this.tablePieces.columnDescription];
                set => this[this.tablePieces.columnDescription] = value;
            }

            public decimal PrixAchatHT
            {
                get => this.IsPrixAchatHTNull() ? 0 : (decimal)this[this.tablePieces.columnPrixAchatHT];
                set => this[this.tablePieces.columnPrixAchatHT] = value;
            }

            public decimal PrixVenteHT
            {
                get => this.IsPrixVenteHTNull() ? 0 : (decimal)this[this.tablePieces.columnPrixVenteHT];
                set => this[this.tablePieces.columnPrixVenteHT] = value;
            }

            public int Stock
            {
                get => this.IsStockNull() ? 0 : (int)this[this.tablePieces.columnStock];
                set => this[this.tablePieces.columnStock] = value;
            }

            public decimal TvaPct
            {
                get => this.IsTvaPctNull() ? 0 : (decimal)this[this.tablePieces.columnTvaPct];
                set => this[this.tablePieces.columnTvaPct] = value;
            }

            public bool IsReferenceNull() => this.IsNull(this.tablePieces.columnReference);
            public void SetReferenceNull() => this[this.tablePieces.columnReference] = Convert.DBNull;

            public bool IsDescriptionNull() => this.IsNull(this.tablePieces.columnDescription);
            public void SetDescriptionNull() => this[this.tablePieces.columnDescription] = Convert.DBNull;

            public bool IsPrixAchatHTNull() => this.IsNull(this.tablePieces.columnPrixAchatHT);
            public void SetPrixAchatHTNull() => this[this.tablePieces.columnPrixAchatHT] = Convert.DBNull;

            public bool IsPrixVenteHTNull() => this.IsNull(this.tablePieces.columnPrixVenteHT);
            public void SetPrixVenteHTNull() => this[this.tablePieces.columnPrixVenteHT] = Convert.DBNull;

            public bool IsStockNull() => this.IsNull(this.tablePieces.columnStock);
            public void SetStockNull() => this[this.tablePieces.columnStock] = Convert.DBNull;

            public bool IsTvaPctNull() => this.IsNull(this.tablePieces.columnTvaPct);
            public void SetTvaPctNull() => this[this.tablePieces.columnTvaPct] = Convert.DBNull;
        }
        #endregion

        #region FacturesVenteDataTable
        public partial class FacturesVenteDataTable : DataTable
        {
            internal DataColumn columnId;
            internal DataColumn columnDate;
            internal DataColumn columnClientId;
            internal DataColumn columnDateEcheance;
            internal DataColumn columnMontantPaye;

            public FacturesVenteDataTable()
            {
                this.TableName = "FacturesVente";
                this.BeginInit();
                this.InitClass();
                this.EndInit();
            }

            internal FacturesVenteDataTable(DataTable table)
            {
                this.TableName = table.TableName;
                this.BeginInit();
                this.InitClass();
                
                if (table.CaseSensitive != table.DataSet.CaseSensitive)
                {
                    this.CaseSensitive = table.CaseSensitive;
                }
                
                if (table.Locale.ToString() != table.DataSet.Locale.ToString())
                {
                    this.Locale = table.Locale;
                }
                
                if (table.Namespace != table.DataSet.Namespace)
                {
                    this.Namespace = table.Namespace;
                }
                
                this.Prefix = table.Prefix;
                this.MinimumCapacity = table.MinimumCapacity;
                
                this.EndInit();
            }

            private void InitClass()
            {
                this.columnId = new DataColumn("Id", typeof(string), null, MappingType.Element);
                this.Columns.Add(this.columnId);
                
                this.columnDate = new DataColumn("Date", typeof(DateTime), null, MappingType.Element);
                this.Columns.Add(this.columnDate);
                
                this.columnClientId = new DataColumn("ClientId", typeof(string), null, MappingType.Element);
                this.Columns.Add(this.columnClientId);
                
                this.columnDateEcheance = new DataColumn("DateEcheance", typeof(DateTime), null, MappingType.Element);
                this.Columns.Add(this.columnDateEcheance);
                
                this.columnMontantPaye = new DataColumn("MontantPaye", typeof(decimal), null, MappingType.Element);
                this.Columns.Add(this.columnMontantPaye);
                
                // Set primary key
                this.Constraints.Add(new UniqueConstraint("Constraint1", new DataColumn[] { this.columnId }, true));
                this.columnId.AllowDBNull = false;
                this.columnId.Unique = true;
            }

            public FacturesVenteRow NewFacturesVenteRow()
            {
                return (FacturesVenteRow)this.NewRow();
            }

            protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
            {
                return new FacturesVenteRow(builder);
            }

            protected override Type GetRowType()
            {
                return typeof(FacturesVenteRow);
            }

            public FacturesVenteRow FindById(string id)
            {
                return (FacturesVenteRow)this.Rows.Find(id);
            }

            public void AddFacturesVenteRow(FacturesVenteRow row)
            {
                this.Rows.Add(row);
            }

            public FacturesVenteRow AddFacturesVenteRow(string id, DateTime date, string clientId, DateTime dateEcheance, decimal montantPaye)
            {
                FacturesVenteRow rowFacturesVenteRow = (FacturesVenteRow)this.NewRow();
                rowFacturesVenteRow.Id = id;
                rowFacturesVenteRow.Date = date;
                rowFacturesVenteRow.ClientId = clientId;
                rowFacturesVenteRow.DateEcheance = dateEcheance;
                rowFacturesVenteRow.MontantPaye = montantPaye;
                this.Rows.Add(rowFacturesVenteRow);
                return rowFacturesVenteRow;
            }
        }

        public partial class FacturesVenteRow : DataRow
        {
            private FacturesVenteDataTable tableFacturesVente;

            internal FacturesVenteRow(DataRowBuilder rb) : base(rb)
            {
                this.tableFacturesVente = (FacturesVenteDataTable)this.Table;
            }

            public string Id
            {
                get => (string)this[this.tableFacturesVente.columnId];
                set => this[this.tableFacturesVente.columnId] = value;
            }

            public DateTime Date
            {
                get => this.IsDateNull() ? DateTime.MinValue : (DateTime)this[this.tableFacturesVente.columnDate];
                set => this[this.tableFacturesVente.columnDate] = value;
            }

            public string ClientId
            {
                get => this.IsClientIdNull() ? string.Empty : (string)this[this.tableFacturesVente.columnClientId];
                set => this[this.tableFacturesVente.columnClientId] = value;
            }

            public DateTime DateEcheance
            {
                get => this.IsDateEcheanceNull() ? DateTime.MinValue : (DateTime)this[this.tableFacturesVente.columnDateEcheance];
                set => this[this.tableFacturesVente.columnDateEcheance] = value;
            }

            public decimal MontantPaye
            {
                get => this.IsMontantPayeNull() ? 0 : (decimal)this[this.tableFacturesVente.columnMontantPaye];
                set => this[this.tableFacturesVente.columnMontantPaye] = value;
            }

            public bool IsDateNull() => this.IsNull(this.tableFacturesVente.columnDate);
            public void SetDateNull() => this[this.tableFacturesVente.columnDate] = Convert.DBNull;

            public bool IsClientIdNull() => this.IsNull(this.tableFacturesVente.columnClientId);
            public void SetClientIdNull() => this[this.tableFacturesVente.columnClientId] = Convert.DBNull;

            public bool IsDateEcheanceNull() => this.IsNull(this.tableFacturesVente.columnDateEcheance);
            public void SetDateEcheanceNull() => this[this.tableFacturesVente.columnDateEcheance] = Convert.DBNull;

            public bool IsMontantPayeNull() => this.IsNull(this.tableFacturesVente.columnMontantPaye);
            public void SetMontantPayeNull() => this[this.tableFacturesVente.columnMontantPaye] = Convert.DBNull;
        }
        #endregion

        #region UsersDataTable
        public partial class UsersDataTable : DataTable
        {
            internal DataColumn columnId;
            internal DataColumn columnUsername;
            internal DataColumn columnNom;
            internal DataColumn columnPrenom;
            internal DataColumn columnRole;
            internal DataColumn columnActif;

            public UsersDataTable()
            {
                this.TableName = "Users";
                this.BeginInit();
                this.InitClass();
                this.EndInit();
            }

            internal UsersDataTable(DataTable table)
            {
                this.TableName = table.TableName;
                this.BeginInit();
                this.InitClass();
                
                if (table.CaseSensitive != table.DataSet.CaseSensitive)
                {
                    this.CaseSensitive = table.CaseSensitive;
                }
                
                if (table.Locale.ToString() != table.DataSet.Locale.ToString())
                {
                    this.Locale = table.Locale;
                }
                
                if (table.Namespace != table.DataSet.Namespace)
                {
                    this.Namespace = table.Namespace;
                }
                
                this.Prefix = table.Prefix;
                this.MinimumCapacity = table.MinimumCapacity;
                
                this.EndInit();
            }

            private void InitClass()
            {
                this.columnId = new DataColumn("Id", typeof(string), null, MappingType.Element);
                this.Columns.Add(this.columnId);
                
                this.columnUsername = new DataColumn("Username", typeof(string), null, MappingType.Element);
                this.Columns.Add(this.columnUsername);
                
                this.columnNom = new DataColumn("Nom", typeof(string), null, MappingType.Element);
                this.Columns.Add(this.columnNom);
                
                this.columnPrenom = new DataColumn("Prenom", typeof(string), null, MappingType.Element);
                this.Columns.Add(this.columnPrenom);
                
                this.columnRole = new DataColumn("Role", typeof(int), null, MappingType.Element);
                this.Columns.Add(this.columnRole);
                
                this.columnActif = new DataColumn("Actif", typeof(bool), null, MappingType.Element);
                this.Columns.Add(this.columnActif);
                
                // Set primary key
                this.Constraints.Add(new UniqueConstraint("Constraint1", new DataColumn[] { this.columnId }, true));
                this.columnId.AllowDBNull = false;
                this.columnId.Unique = true;
            }

            public UsersRow NewUsersRow()
            {
                return (UsersRow)this.NewRow();
            }

            protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
            {
                return new UsersRow(builder);
            }

            protected override Type GetRowType()
            {
                return typeof(UsersRow);
            }

            public UsersRow FindById(string id)
            {
                return (UsersRow)this.Rows.Find(id);
            }

            public void AddUsersRow(UsersRow row)
            {
                this.Rows.Add(row);
            }

            public UsersRow AddUsersRow(string id, string username, string nom, string prenom, int role, bool actif)
            {
                UsersRow rowUsersRow = (UsersRow)this.NewRow();
                rowUsersRow.Id = id;
                rowUsersRow.Username = username;
                rowUsersRow.Nom = nom;
                rowUsersRow.Prenom = prenom;
                rowUsersRow.Role = role;
                rowUsersRow.Actif = actif;
                this.Rows.Add(rowUsersRow);
                return rowUsersRow;
            }
        }

        public partial class UsersRow : DataRow
        {
            private UsersDataTable tableUsers;

            internal UsersRow(DataRowBuilder rb) : base(rb)
            {
                this.tableUsers = (UsersDataTable)this.Table;
            }

            public string Id
            {
                get => (string)this[this.tableUsers.columnId];
                set => this[this.tableUsers.columnId] = value;
            }

            public string Username
            {
                get => this.IsUsernameNull() ? string.Empty : (string)this[this.tableUsers.columnUsername];
                set => this[this.tableUsers.columnUsername] = value;
            }

            public string Nom
            {
                get => this.IsNomNull() ? string.Empty : (string)this[this.tableUsers.columnNom];
                set => this[this.tableUsers.columnNom] = value;
            }

            public string Prenom
            {
                get => this.IsPrenomNull() ? string.Empty : (string)this[this.tableUsers.columnPrenom];
                set => this[this.tableUsers.columnPrenom] = value;
            }

            public int Role
            {
                get => this.IsRoleNull() ? 0 : (int)this[this.tableUsers.columnRole];
                set => this[this.tableUsers.columnRole] = value;
            }

            public bool Actif
            {
                get => this.IsActifNull() ? false : (bool)this[this.tableUsers.columnActif];
                set => this[this.tableUsers.columnActif] = value;
            }

            public bool IsUsernameNull() => this.IsNull(this.tableUsers.columnUsername);
            public void SetUsernameNull() => this[this.tableUsers.columnUsername] = Convert.DBNull;

            public bool IsNomNull() => this.IsNull(this.tableUsers.columnNom);
            public void SetNomNull() => this[this.tableUsers.columnNom] = Convert.DBNull;

            public bool IsPrenomNull() => this.IsNull(this.tableUsers.columnPrenom);
            public void SetPrenomNull() => this[this.tableUsers.columnPrenom] = Convert.DBNull;

            public bool IsRoleNull() => this.IsNull(this.tableUsers.columnRole);
            public void SetRoleNull() => this[this.tableUsers.columnRole] = Convert.DBNull;

            public bool IsActifNull() => this.IsNull(this.tableUsers.columnActif);
            public void SetActifNull() => this[this.tableUsers.columnActif] = Convert.DBNull;
        }
        #endregion
    }
} 