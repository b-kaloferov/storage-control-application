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
        private Label label1;
        private readonly IShoeService _shoeService;

        public DiscardShoesForm(IModelService modelService, IShoeService shoeService)
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
            btnDiscard = new Button();
            label1 = new Label();
            SuspendLayout();
            // 
            // txtModelId
            // 
            txtModelId.Location = new Point(96, 98);
            txtModelId.Name = "txtModelId";
            txtModelId.PlaceholderText = "Model ID";
            txtModelId.Size = new Size(150, 27);
            txtModelId.TabIndex = 0;
            // 
            // txtSize
            // 
            txtSize.Location = new Point(96, 148);
            txtSize.Name = "txtSize";
            txtSize.PlaceholderText = "Size";
            txtSize.Size = new Size(150, 27);
            txtSize.TabIndex = 1;
            // 
            // txtQuantity
            // 
            txtQuantity.Location = new Point(96, 198);
            txtQuantity.Name = "txtQuantity";
            txtQuantity.PlaceholderText = "Quantity";
            txtQuantity.Size = new Size(150, 27);
            txtQuantity.TabIndex = 2;
            // 
            // btnDiscard
            // 
            btnDiscard.BackColor = Color.DodgerBlue;
            btnDiscard.Location = new Point(96, 248);
            btnDiscard.Name = "btnDiscard";
            btnDiscard.Size = new Size(150, 49);
            btnDiscard.TabIndex = 3;
            btnDiscard.Text = "Discard Shoes";
            btnDiscard.UseVisualStyleBackColor = false;
            btnDiscard.Click += btnDiscard_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Arial", 18F, FontStyle.Bold, GraphicsUnit.Point, 204);
            label1.Location = new Point(63, 41);
            label1.Name = "label1";
            label1.Size = new Size(224, 35);
            label1.TabIndex = 4;
            label1.Text = "Discard Shoes";
            // 
            // DiscardShoesForm
            // 
            BackgroundImage = Properties.Resources.bright_background_image;
            ClientSize = new Size(350, 341);
            Controls.Add(label1);
            Controls.Add(txtModelId);
            Controls.Add(txtSize);
            Controls.Add(txtQuantity);
            Controls.Add(btnDiscard);
            Name = "DiscardShoesForm";
            Text = "Discard Shoes";
            ResumeLayout(false);
            PerformLayout();
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
