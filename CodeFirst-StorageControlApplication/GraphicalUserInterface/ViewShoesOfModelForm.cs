using ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalUserInterface
{
    public partial class ViewShoesOfModelForm : Form
    {
        private readonly IModelService _modelService;

        public ViewShoesOfModelForm(IModelService modelService)
        {
            _modelService = modelService;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            labelModelId = new Label();
            textBoxModelId = new TextBox();
            buttonFind = new Button();
            labelModelDetails = new Label();
            dataGridViewShoes = new DataGridView();
            btnClose = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridViewShoes).BeginInit();
            SuspendLayout();
            // 
            // labelModelId
            // 
            labelModelId.AutoSize = true;
            labelModelId.BackColor = Color.Transparent;
            labelModelId.Location = new Point(12, 15);
            labelModelId.Name = "labelModelId";
            labelModelId.Size = new Size(74, 20);
            labelModelId.TabIndex = 0;
            labelModelId.Text = "Model ID:";
            // 
            // textBoxModelId
            // 
            textBoxModelId.Location = new Point(98, 12);
            textBoxModelId.Name = "textBoxModelId";
            textBoxModelId.Size = new Size(150, 27);
            textBoxModelId.TabIndex = 1;
            // 
            // buttonFind
            // 
            buttonFind.Location = new Point(254, 10);
            buttonFind.Name = "buttonFind";
            buttonFind.Size = new Size(94, 29);
            buttonFind.TabIndex = 2;
            buttonFind.Text = "Find";
            buttonFind.UseVisualStyleBackColor = true;
            buttonFind.Click += ButtonFind_Click;
            // 
            // labelModelDetails
            // 
            labelModelDetails.AutoSize = true;
            labelModelDetails.BackColor = Color.Transparent;
            labelModelDetails.Location = new Point(12, 50);
            labelModelDetails.Name = "labelModelDetails";
            labelModelDetails.Size = new Size(0, 20);
            labelModelDetails.TabIndex = 3;
            // 
            // dataGridViewShoes
            // 
            dataGridViewShoes.BackgroundColor = Color.WhiteSmoke;
            dataGridViewShoes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewShoes.Location = new Point(20, 227);
            dataGridViewShoes.Name = "dataGridViewShoes";
            dataGridViewShoes.RowHeadersWidth = 51;
            dataGridViewShoes.Size = new Size(679, 188);
            dataGridViewShoes.TabIndex = 4;
            // 
            // btnClose
            // 
            btnClose.Location = new Point(605, 453);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(94, 29);
            btnClose.TabIndex = 5;
            btnClose.Text = "Close";
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += ButtonClose_Click;
            // 
            // ViewShoesOfModelForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.bright_background_image;
            ClientSize = new Size(711, 515);
            Controls.Add(btnClose);
            Controls.Add(dataGridViewShoes);
            Controls.Add(labelModelDetails);
            Controls.Add(buttonFind);
            Controls.Add(textBoxModelId);
            Controls.Add(labelModelId);
            MaximizeBox = false;
            Name = "ViewShoesOfModelForm";
            Text = "View Shoes of Model";
            Load += ViewShoesOfModelForm_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridViewShoes).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private async void ButtonFind_Click(object sender, EventArgs e)
        {
            if (int.TryParse(textBoxModelId.Text, out int modelId))
            {
                var model = await _modelService.GetModelByIdAsync(modelId, true);
                if (model != null)
                {
                    labelModelDetails.Text = $"Model ID: {model.Id}\n" +
                                             $"Brand: {model.Brand}\n" +
                                             $"Code: {model.Code}\n" +
                                             $"Shoe Type: {model.ShoeType}\n" +
                                             $"Price: {model.Price}\n" +
                                             $"Gender Category: {model.GenderCategory}\n" +
                                             $"Description: {model.Description}\n" +
                                             "Shoes:";
                    dataGridViewShoes.DataSource = model.Shoes.Select(shoe => new
                    {
                        shoe.Id,
                        shoe.Size,
                        shoe.Quantity
                    }).ToList();
                }
                else
                {
                    labelModelDetails.Text = "Model not found.";
                    dataGridViewShoes.DataSource = null;
                }
            }
            else
            {
                MessageBox.Show("Invalid model ID.");
            }
        }

        private void ButtonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ViewShoesOfModelForm_Load(object sender, EventArgs e)
        {
            dataGridViewShoes.AutoGenerateColumns = true;
        }

        private Label labelModelId;
        private TextBox textBoxModelId;
        private Button buttonFind;
        private Label labelModelDetails;
        private DataGridView dataGridViewShoes;
        private Button btnClose;
    }
}

