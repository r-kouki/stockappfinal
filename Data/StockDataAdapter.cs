using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Extensions.DependencyInjection;
using StockApp.Data.Entities;
using StockApp.Data.Repositories;

namespace StockApp.Data
{
    public class StockDataAdapter
    {
        private readonly StockDataSet _stockDataSet;
        private readonly IServiceProvider _serviceProvider;

        public StockDataAdapter()
        {
            _stockDataSet = new StockDataSet();
            _serviceProvider = Program.ServiceProvider;
        }

        public StockDataSet GetDataSet()
        {
            return _stockDataSet;
        }

        public void FillClientsTable()
        {
            try
            {
                // Temporarily disable constraints
                bool previousEnforceConstraints = _stockDataSet.EnforceConstraints;
                _stockDataSet.EnforceConstraints = false;
                
                _stockDataSet.Clients.Clear();
                
                var repository = _serviceProvider.GetRequiredService<IClientRepository>();
                var clients = repository.GetAllAsync().GetAwaiter().GetResult();
                
                foreach (var client in clients)
                {
                    var newRow = _stockDataSet.Clients.NewClientsRow();
                    newRow.Id = client.Id;
                    newRow.Nom = $"{client.Nom} {client.Prenom}".Trim();
                    newRow.Adresse = client.Adresse ?? string.Empty;
                    newRow.Telephone = client.Telephone ?? string.Empty;
                    newRow.Email = client.MatFiscal ?? string.Empty;
                    
                    _stockDataSet.Clients.AddClientsRow(newRow);
                }
                
                _stockDataSet.AcceptChanges();
                
                // Re-enable constraints
                _stockDataSet.EnforceConstraints = previousEnforceConstraints;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error filling clients table: {ex.Message}");
                throw;
            }
        }

        public void FillFournisseursTable()
        {
            try
            {
                // Temporarily disable constraints
                bool previousEnforceConstraints = _stockDataSet.EnforceConstraints;
                _stockDataSet.EnforceConstraints = false;
                
                _stockDataSet.Fournisseurs.Clear();
                
                var repository = _serviceProvider.GetRequiredService<IFournisseurRepository>();
                var fournisseurs = repository.GetAllAsync().GetAwaiter().GetResult();
                
                foreach (var fournisseur in fournisseurs)
                {
                    var newRow = _stockDataSet.Fournisseurs.NewFournisseursRow();
                    newRow.Id = fournisseur.Id;
                    newRow.Nom = $"{fournisseur.Nom} {fournisseur.Prenom}".Trim();
                    newRow.Adresse = fournisseur.Adresse ?? string.Empty;
                    newRow.Telephone = fournisseur.Telephone ?? string.Empty;
                    newRow.Email = fournisseur.MatFiscal ?? string.Empty;
                    
                    _stockDataSet.Fournisseurs.AddFournisseursRow(newRow);
                }
                
                _stockDataSet.AcceptChanges();
                
                // Re-enable constraints
                _stockDataSet.EnforceConstraints = previousEnforceConstraints;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error filling fournisseurs table: {ex.Message}");
                throw;
            }
        }

        public void FillPiecesTable()
        {
            try
            {
                // Temporarily disable constraints
                bool previousEnforceConstraints = _stockDataSet.EnforceConstraints;
                _stockDataSet.EnforceConstraints = false;
                
                _stockDataSet.Pieces.Clear();
                
                var repository = _serviceProvider.GetRequiredService<IPieceRepository>();
                var pieces = repository.GetAllAsync().GetAwaiter().GetResult();
                
                foreach (var piece in pieces)
                {
                    var newRow = _stockDataSet.Pieces.NewPiecesRow();
                    newRow.Id = piece.Id;
                    newRow.Reference = piece.Reference ?? string.Empty;
                    newRow.Description = piece.Marque ?? string.Empty;
                    newRow.PrixAchatHT = piece.PrixAchatHT;
                    newRow.PrixVenteHT = piece.PrixVenteHT;
                    newRow.Stock = piece.Stock;
                    newRow.TvaPct = piece.TvaPct;
                    
                    _stockDataSet.Pieces.AddPiecesRow(newRow);
                }
                
                _stockDataSet.AcceptChanges();
                
                // Re-enable constraints
                _stockDataSet.EnforceConstraints = previousEnforceConstraints;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error filling pieces table: {ex.Message}");
                throw;
            }
        }

        public void FillFacturesVenteTable()
        {
            try
            {
                // Temporairement désactiver les contraintes pendant le chargement
                bool previousEnforceConstraints = _stockDataSet.EnforceConstraints;
                _stockDataSet.EnforceConstraints = false;
                
                // Vider la table avant de la remplir à nouveau
                _stockDataSet.FacturesVente.Clear();
                
                // Obtenir toutes les factures depuis le repository
                var repository = Program.ServiceProvider.GetRequiredService<IFactureVenteRepository>();
                var factures = repository.GetAllWithDetailsAsync().Result;
                
                // Ajouter chaque facture au DataSet
                foreach (var facture in factures)
                {
                    try
                    {
                        var newRow = _stockDataSet.FacturesVente.NewFacturesVenteRow();
                        newRow.Id = facture.Id;
                        newRow.Date = facture.Date;
                        newRow.ClientId = facture.ClientId;
                        
                        if (facture.DateEcheance.HasValue)
                        {
                            newRow.DateEcheance = facture.DateEcheance.Value;
                        }
                        
                        newRow.MontantPaye = facture.MontantPaye;
                        
                        _stockDataSet.FacturesVente.AddFacturesVenteRow(newRow);
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"Erreur lors de l'ajout de la facture {facture.Id} au DataSet: {ex.Message}");
                        // Continuer avec la facture suivante plutôt que d'échouer complètement
                    }
                }
                
                // Appliquer les changements au DataSet
                _stockDataSet.AcceptChanges();
                
                // Rétablir l'état original des contraintes
                _stockDataSet.EnforceConstraints = previousEnforceConstraints;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Erreur lors du remplissage de la table FacturesVente: {ex.Message}");
                throw;
            }
        }

        public void FillAllTables()
        {
            try 
            {
                // Temporarily disable constraints during full data reload
                bool previousEnforceConstraints = _stockDataSet.EnforceConstraints;
                _stockDataSet.EnforceConstraints = false;
                
                // Fill all tables in appropriate order
                FillClientsTable();
                FillFournisseursTable();
                FillPiecesTable();
                FillFacturesVenteTable();
                FillUsersTable();
                
                // Re-enable constraints
                _stockDataSet.EnforceConstraints = previousEnforceConstraints;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error filling all tables: {ex.Message}");
                throw;
            }
        }

        public void FillUsersTable()
        {
            try
            {
                // Temporarily disable constraints
                bool previousEnforceConstraints = _stockDataSet.EnforceConstraints;
                _stockDataSet.EnforceConstraints = false;
                
                _stockDataSet.Users.Clear();
                
                var repository = _serviceProvider.GetRequiredService<IUserRepository>();
                var users = repository.GetAllAsync().GetAwaiter().GetResult();
                
                foreach (var user in users)
                {
                    var newRow = _stockDataSet.Users.NewUsersRow();
                    newRow.Id = user.Id;
                    newRow.Username = user.Username ?? string.Empty;
                    newRow.Nom = user.Nom ?? string.Empty;
                    newRow.Prenom = user.Prenom ?? string.Empty;
                    newRow.Role = (int)user.Role;
                    newRow.Actif = user.Actif;
                    
                    _stockDataSet.Users.AddUsersRow(newRow);
                }
                
                _stockDataSet.AcceptChanges();
                
                // Re-enable constraints
                _stockDataSet.EnforceConstraints = previousEnforceConstraints;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error filling users table: {ex.Message}");
                throw;
            }
        }
    }
} 