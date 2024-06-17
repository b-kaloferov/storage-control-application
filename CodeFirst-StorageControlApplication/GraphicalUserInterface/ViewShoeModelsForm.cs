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
        }

        private async Task ViewShoeModelsForm_Load(object sender, EventArgs e)
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
            ((System.ComponentModel.ISupportInitialize)dataGridViewShoeModels).BeginInit();
            SuspendLayout();
            // 
            // dataGridViewShoeModels
            // 
            dataGridViewShoeModels.BackgroundColor = Color.DodgerBlue;
            dataGridViewShoeModels.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewShoeModels.Location = new Point(393, 216);
            dataGridViewShoeModels.Name = "dataGridViewShoeModels";
            dataGridViewShoeModels.RowHeadersWidth = 51;
            dataGridViewShoeModels.Size = new Size(300, 188);
            dataGridViewShoeModels.TabIndex = 0;
            dataGridViewShoeModels.Click += ViewShoeModelsForm_Load;
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
            // ViewShoeModelsForm
            // 
            BackgroundImage = Properties.Resources.bright_background_image;
            ClientSize = new Size(1074, 498);
            Controls.Add(btnClose);
            Controls.Add(dataGridViewShoeModels);
            Name = "ViewShoeModelsForm";
            ((System.ComponentModel.ISupportInitialize)dataGridViewShoeModels).EndInit();
            ResumeLayout(false);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private DataGridView dataGridViewShoeModels;
        private Button btnClose;
    }
}
