using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using StockApp.Data;
using StockApp.Data.Entities;
using StockApp.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace StockApp.PieceForms
{
    public partial class PieceManagementForm : UserControl
    {
        private readonly IPieceRepository _pieceRepository;
        private readonly StockDataAdapter _dataAdapter;
        private BindingSource _piecesBindingSource;
        
        public PieceManagementForm()
        {
            InitializeComponent();
            
            // Get the repository from DI
            _pieceRepository = Program.ServiceProvider.GetRequiredService<IPieceRepository>();
            
            // Initialize the data adapter
            _dataAdapter = new StockDataAdapter();
            
            // Set up binding source
            _piecesBindingSource = new BindingSource();
            _piecesBindingSource.DataSource = _dataAdapter.GetDataSet();
            _piecesBindingSource.DataMember = "Pieces";
            
            // Set up data binding
            piecesDataGridView.DataSource = _piecesBindingSource;
            
            // Configure columns to match the dataset fields
            ConfigureColumns();
            
            // Load data from database
            LoadDataAsync();
        }
        
        private void ConfigureColumns()
        {
            piecesDataGridView.AutoGenerateColumns = false;
            
            // Clear existing columns
            piecesDataGridView.Columns.Clear();
            
            // Add columns matching the dataset schema
            var idColumn = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Id",
                HeaderText = "ID",
                Visible = false
            };
            
            var referenceColumn = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Reference",
                HeaderText = "Référence",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            };
            
            var descriptionColumn = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Description",
                HeaderText = "Description",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                Width = 200
            };
            
            var prixAchatColumn = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "PrixAchatHT",
                HeaderText = "Prix Achat HT",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "C2" }
            };
            
            var prixVenteColumn = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "PrixVenteHT",
                HeaderText = "Prix Vente HT",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "C2" }
            };
            
            var stockColumn = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Stock",
                HeaderText = "Stock",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            };
            
            var tvaColumn = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TvaPct",
                HeaderText = "TVA %",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "P1" }
            };
            
            // Add columns to DataGridView
            piecesDataGridView.Columns.AddRange(new DataGridViewColumn[] {
                idColumn,
                referenceColumn,
                descriptionColumn,
                prixAchatColumn,
                prixVenteColumn,
                stockColumn,
                tvaColumn
            });
        }
        
        private async void LoadDataAsync()
        {
            try
            {
                // Fill the pieces table in the dataset
                _dataAdapter.FillPiecesTable();
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
                    
                    // Refresh the dataset
                    _dataAdapter.FillPiecesTable();
                    
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
                // Get the selected piece ID from the DataRowView
                var selectedRow = piecesDataGridView.SelectedRows[0].DataBoundItem as DataRowView;
                if (selectedRow != null)
                {
                    var pieceId = selectedRow["Id"].ToString();
                    
                    // Fetch the actual piece entity from the repository
                    var piece = await _pieceRepository.GetByIdAsync(pieceId);
                    
                    if (piece != null)
                    {
                        var detailsForm = new PieceDetailsForm(piece);
                        
                        if (detailsForm.ShowDialog() == DialogResult.OK)
                        {
                            try
                            {
                                // Update the piece in the database
                                await _pieceRepository.UpdateAsync(detailsForm.Piece);
                                
                                // Refresh the dataset
                                _dataAdapter.FillPiecesTable();
                                
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
                // Get the selected piece ID from the DataRowView
                var selectedRow = piecesDataGridView.SelectedRows[0].DataBoundItem as DataRowView;
                if (selectedRow != null)
                {
                    var pieceId = selectedRow["Id"].ToString();
                    var reference = selectedRow["Reference"].ToString();
                    
                    var result = MessageBox.Show($"Êtes-vous sûr de vouloir supprimer la pièce {reference}?", 
                        "Confirmation de suppression", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    
                    if (result == DialogResult.Yes)
                    {
                        try
                        {
                            // Delete from the database
                            await _pieceRepository.DeleteAsync(pieceId);
                            
                            // Refresh the dataset
                            _dataAdapter.FillPiecesTable();
                            
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
    }
} 