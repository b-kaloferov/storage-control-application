using ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalUserInterface
{
    internal class ViewShoeModelsForm : Form
    {
        private readonly IModelService _modelService;

        public ViewShoeModelsForm(IModelService modelService)
        {
            _modelService = modelService;
            InitializeComponent();
            ViewShoeModelsForm_Load();
        }

        private async Task ViewShoeModelsForm_Load()
        {
            try
            {
                var availableModels = await _modelService.GetAllModelsAsync();
                dataGridViewShoeModels.DataSource = availableModels;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading the shoe models: {ex.Message}");
            }
        }

        private void InitializeComponent()
        {
            dataGridViewShoeModels = new DataGridView();
            btnClose = new Button();
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)dataGridViewShoeModels).BeginInit();
            SuspendLayout();
            // 
            // dataGridViewShoeModels
            // 
            dataGridViewShoeModels.BackgroundColor = Color.DodgerBlue;
            dataGridViewShoeModels.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewShoeModels.Location = new Point(86, 187);
            dataGridViewShoeModels.Name = "dataGridViewShoeModels";
            dataGridViewShoeModels.RowHeadersWidth = 51;
            dataGridViewShoeModels.Size = new Size(909, 194);
            dataGridViewShoeModels.TabIndex = 0;
            // 
            // btnClose
            // 
            btnClose.Location = new Point(947, 446);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(94, 29);
            btnClose.TabIndex = 1;
            btnClose.Text = "Close";
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += btnClose_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Arial", 28.2F, FontStyle.Bold, GraphicsUnit.Point, 204);
            label1.Location = new Point(200, 60);
            label1.Name = "label1";
            label1.Size = new Size(648, 55);
            label1.TabIndex = 2;
            label1.Text = "View Available Shoe Models";
            // 
            // ViewShoeModelsForm
            // 
            BackgroundImage = Properties.Resources.bright_background_image;
            ClientSize = new Size(1074, 498);
            Controls.Add(label1);
            Controls.Add(btnClose);
            Controls.Add(dataGridViewShoeModels);
            MaximizeBox = false;
            Name = "ViewShoeModelsForm";
            ((System.ComponentModel.ISupportInitialize)dataGridViewShoeModels).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private DataGridView dataGridViewShoeModels;
        private Label label1;
        private Button btnClose;
    }
}
