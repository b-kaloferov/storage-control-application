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
        private readonly IShoeService _shoeService;

        public AddShoesForm(IModelService modelService, IShoeService shoeService)
        {
            _modelService = modelService;
            _shoeService = shoeService;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.txtModelId = new System.Windows.Forms.TextBox();
            this.txtSize = new System.Windows.Forms.TextBox();
            this.txtQuantity = new System.Windows.Forms.TextBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // 
            // txtModelId
            // 
            this.txtModelId.Location = new System.Drawing.Point(50, 50);
            this.txtModelId.Name = "txtModelId";
            this.txtModelId.Size = new System.Drawing.Size(150, 22);
            this.txtModelId.TabIndex = 0;
            this.txtModelId.PlaceholderText = "Model ID";

            // 
            // txtSize
            // 
            this.txtSize.Location = new System.Drawing.Point(50, 100);
            this.txtSize.Name = "txtSize";
            this.txtSize.Size = new System.Drawing.Size(150, 22);
            this.txtSize.TabIndex = 1;
            this.txtSize.PlaceholderText = "Size";

            // 
            // txtQuantity
            // 
            this.txtQuantity.Location = new System.Drawing.Point(50, 150);
            this.txtQuantity.Name = "txtQuantity";
            this.txtQuantity.Size = new System.Drawing.Size(150, 22);
            this.txtQuantity.TabIndex = 2;
            this.txtQuantity.PlaceholderText = "Quantity";

            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(50, 200);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(150, 50);
            this.btnAdd.TabIndex = 3;
            this.btnAdd.Text = "Add Shoes";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);

            // 
            // AddShoesForm
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.txtModelId);
            this.Controls.Add(this.txtSize);
            this.Controls.Add(this.txtQuantity);
            this.Controls.Add(this.btnAdd);
            this.Name = "AddShoesForm";
            this.Text = "Add Shoes";
            this.ResumeLayout(false);
            this.PerformLayout();
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
