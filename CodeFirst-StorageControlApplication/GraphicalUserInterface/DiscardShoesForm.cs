using ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalUserInterface
{
    public partial class DiscardShoesForm : Form
    {
        private readonly IModelService _modelService;
        private readonly IShoeService _shoeService;

        public DiscardShoesForm(IModelService modelService, IShoeService shoeService)
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
            this.btnDiscard = new System.Windows.Forms.Button();
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
            // btnDiscard
            // 
            this.btnDiscard.Location = new System.Drawing.Point(50, 200);
            this.btnDiscard.Name = "btnDiscard";
            this.btnDiscard.Size = new System.Drawing.Size(150, 50);
            this.btnDiscard.TabIndex = 3;
            this.btnDiscard.Text = "Discard Shoes";
            this.btnDiscard.UseVisualStyleBackColor = true;
            this.btnDiscard.Click += new System.EventHandler(this.btnDiscard_Click);

            // 
            // DiscardShoesForm
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.txtModelId);
            this.Controls.Add(this.txtSize);
            this.Controls.Add(this.txtQuantity);
            this.Controls.Add(this.btnDiscard);
            this.Name = "DiscardShoesForm";
            this.Text = "Discard Shoes";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private async void btnDiscard_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtModelId.Text, out int modelId) && double.TryParse(txtSize.Text, out double size) && int.TryParse(txtQuantity.Text, out int quantity))
            {
                var model = await _modelService.GetModelByIdAsync(modelId);
                if (model != null)
                {
                    var shoesForModel = await _shoeService.GetShoesByModelIdAsync(modelId);
                    var existingShoe = shoesForModel.FirstOrDefault(s => s.Size == size);

                    if (existingShoe != null)
                    {
                        if (existingShoe.Quantity >= quantity)
                        {
                            existingShoe.Quantity -= quantity;
                            await _shoeService.UpdateShoeAsync(existingShoe);
                            MessageBox.Show($"Discarded {quantity} of shoe ID {existingShoe.Id} with size {size}. Remaining quantity: {existingShoe.Quantity}");
                        }
                        else
                        {
                            MessageBox.Show($"Not enough quantity. Available: {existingShoe.Quantity}, Requested: {quantity}");
                        }
                    }
                    else
                    {
                        MessageBox.Show($"No shoe found with size {size} for model ID {modelId}.");
                    }
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
        private Button btnDiscard;
    }
}
