using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using StockApp.Data.Entities;
using StockApp.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace StockApp.PieceForms
{
    public partial class PieceManagementForm : UserControl
    {
        private readonly IPieceRepository _pieceRepository;
        private List<Piece> _pieces = new List<Piece>();
        
        public PieceManagementForm()
        {
            InitializeComponent();
            
            // Get the repository from DI
            _pieceRepository = Program.ServiceProvider.GetRequiredService<IPieceRepository>();
            
            // Load data from database asynchronously
            LoadDataAsync();
        }
        
        private async void LoadDataAsync()
        {
            // Load pieces from database in a fire-and-forget manner
            await LoadPiecesAsync();
        }
        
        private async Task LoadPiecesAsync()
        {
            try
            {
                // Get pieces from the database
                var pieces = await _pieceRepository.GetAllAsync();
                _pieces = pieces.ToList();
                
                // Refresh the DataGridView
                RefreshPiecesGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors du chargement des pièces: {ex.Message}", 
                    "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private async void AddButton_Click(object sender, EventArgs e)
        {
            var detailsForm = new PieceDetailsForm();
            
            if (detailsForm.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Add the new piece to the database
                    await _pieceRepository.AddAsync(detailsForm.Piece);
                    
                    // Reload pieces from the database
                    await LoadPiecesAsync();
                    
                    MessageBox.Show("Pièce ajoutée avec succès!", "Succès", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erreur lors de l'ajout de la pièce: {ex.Message}", 
                        "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        
        private async void EditButton_Click(object sender, EventArgs e)
        {
            if (this.piecesDataGridView.SelectedRows.Count > 0)
            {
                // Get the selected piece
                var selectedPiece = this.piecesDataGridView.SelectedRows[0].DataBoundItem as Piece;
                
                if (selectedPiece != null)
                {
                    // Create a copy of the piece for editing
                    var pieceCopy = new Piece
                    {
                        Id = selectedPiece.Id,
                        Marque = selectedPiece.Marque,
                        Reference = selectedPiece.Reference,
                        PrixAchatHT = selectedPiece.PrixAchatHT,
                        PrixVenteHT = selectedPiece.PrixVenteHT,
                        Stock = selectedPiece.Stock,
                        SeuilAlerte = selectedPiece.SeuilAlerte,
                        TvaPct = selectedPiece.TvaPct
                    };
                    
                    var detailsForm = new PieceDetailsForm(pieceCopy);
                    
                    if (detailsForm.ShowDialog() == DialogResult.OK)
                    {
                        try
                        {
                            // Update the piece in the database
                            await _pieceRepository.UpdateAsync(detailsForm.Piece);
                            
                            // Reload pieces from the database
                            await LoadPiecesAsync();
                            
                            MessageBox.Show("Pièce modifiée avec succès!", "Succès", 
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Erreur lors de la modification de la pièce: {ex.Message}", 
                                "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner une pièce à modifier.", "Aucune sélection", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        
        private async void DeleteButton_Click(object sender, EventArgs e)
        {
            if (this.piecesDataGridView.SelectedRows.Count > 0)
            {
                var selectedPiece = this.piecesDataGridView.SelectedRows[0].DataBoundItem as Piece;
                
                if (selectedPiece != null)
                {
                    var result = MessageBox.Show($"Êtes-vous sûr de vouloir supprimer la pièce {selectedPiece.Marque} {selectedPiece.Reference}?", 
                        "Confirmation de suppression", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    
                    if (result == DialogResult.Yes)
                    {
                        try
                        {
                            // Delete from the database
                            await _pieceRepository.DeleteAsync(selectedPiece.Id);
                            
                            // Reload pieces from the database
                            await LoadPiecesAsync();
                            
                            MessageBox.Show("Pièce supprimée avec succès!", "Succès", 
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Erreur lors de la suppression de la pièce: {ex.Message}", 
                                "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner une pièce à supprimer.", "Aucune sélection", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        
        private void RefreshPiecesGrid()
        {
            // Update the DataSource to refresh the grid
            this.piecesDataGridView.DataSource = null;
            this.piecesDataGridView.DataSource = _pieces;
        }
    }
} 