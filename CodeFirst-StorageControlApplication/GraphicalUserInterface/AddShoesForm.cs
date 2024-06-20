using BusinessLayer;
using ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalUserInterface
{
    public partial class AddShoesForm : Form
    {
        private readonly IModelService _modelService;
        private Label label1;
        private readonly IShoeService _shoeService;

        public AddShoesForm(IModelService modelService, IShoeService shoeService)
        {
            _modelService = modelService;
            _shoeService = shoeService;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            txtModelId = new TextBox();
            txtSize = new TextBox();
            txtQuantity = new TextBox();
            btnAdd = new Button();
            label1 = new Label();
            SuspendLayout();
            // 
            // txtModelId
            // 
            txtModelId.Location = new Point(92, 111);
            txtModelId.Name = "txtModelId";
            txtModelId.PlaceholderText = "Model ID";
            txtModelId.Size = new Size(150, 27);
            txtModelId.TabIndex = 0;
            // 
            // txtSize
            // 
            txtSize.Location = new Point(92, 161);
            txtSize.Name = "txtSize";
            txtSize.PlaceholderText = "Size";
            txtSize.Size = new Size(150, 27);
            txtSize.TabIndex = 1;
            // 
            // txtQuantity
            // 
            txtQuantity.Location = new Point(92, 211);
            txtQuantity.Name = "txtQuantity";
            txtQuantity.PlaceholderText = "Quantity";
            txtQuantity.Size = new Size(150, 27);
            txtQuantity.TabIndex = 2;
            // 
            // btnAdd
            // 
            btnAdd.BackColor = Color.DodgerBlue;
            btnAdd.Location = new Point(92, 273);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(150, 50);
            btnAdd.TabIndex = 3;
            btnAdd.Text = "Add Shoes";
            btnAdd.UseVisualStyleBackColor = false;
            btnAdd.Click += btnAdd_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Arial", 18F, FontStyle.Bold, GraphicsUnit.Point, 204);
            label1.Location = new Point(82, 35);
            label1.Name = "label1";
            label1.Size = new Size(170, 35);
            label1.TabIndex = 4;
            label1.Text = "Add Shoes";
            // 
            // AddShoesForm
            // 
            BackgroundImage = Properties.Resources.bright_background_image;
            ClientSize = new Size(340, 370);
            Controls.Add(label1);
            Controls.Add(txtModelId);
            Controls.Add(txtSize);
            Controls.Add(txtQuantity);
            Controls.Add(btnAdd);
            Name = "AddShoesForm";
            Text = "Add Shoes";
            ResumeLayout(false);
            PerformLayout();
        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtModelId.Text, out int modelId) && int.TryParse(txtSize.Text, out int size) && int.TryParse(txtQuantity.Text, out int quantity))
            {
                var modelToAddShoes = await _modelService.GetModelByIdAsync(modelId, true);
                if (modelToAddShoes != null)
                {
                    var shoesForModel = await _shoeService.GetShoesByModelIdAsync(modelId);
                    var existingShoe = shoesForModel.FirstOrDefault(s => s.Size == size);

                    if (existingShoe != null)
                    {
                        existingShoe.Quantity += quantity;
                        await _shoeService.UpdateShoeAsync(existingShoe);
                    }
                    else
                    {
                        var newShoe = new Shoe(size, quantity, modelToAddShoes);
                        await _shoeService.CreateShoeAsync(newShoe);
                        modelToAddShoes.Shoes.Add(newShoe);
                    }

                    await _modelService.UpdateModelAsync(modelToAddShoes, true);
                    MessageBox.Show("Shoes added successfully.");
                }
                else
                {
                    MessageBox.Show($"Shoe model with ID {modelId} not found.");
                }
            }
            else
            {
                MessageBox.Show("Invalid input. Please enter valid numbers.");
            }
        }

        private TextBox txtModelId;
        private TextBox txtSize;
        private TextBox txtQuantity;
        private Button btnAdd;
    }
}
