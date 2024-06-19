using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalUserInterface
{
    public partial class RemoveShoeModelForm : Form
    {
        private readonly IModelService _modelService;

        public RemoveShoeModelForm(IModelService modelService)
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
            buttonDelete = new Button();
            buttonCancel = new Button();
            SuspendLayout();
            // 
            // labelModelId
            // 
            labelModelId.AutoSize = true;
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
            labelModelDetails.Location = new Point(12, 50);
            labelModelDetails.Name = "labelModelDetails";
            labelModelDetails.Size = new Size(0, 20);
            labelModelDetails.TabIndex = 3;
            // 
            // buttonDelete
            // 
            buttonDelete.Location = new Point(30, 455);
            buttonDelete.Name = "buttonDelete";
            buttonDelete.Size = new Size(94, 29);
            buttonDelete.TabIndex = 4;
            buttonDelete.Text = "Delete";
            buttonDelete.UseVisualStyleBackColor = true;
            buttonDelete.Click += ButtonDelete_Click;
            // 
            // buttonCancel
            // 
            buttonCancel.Location = new Point(927, 444);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new Size(94, 29);
            buttonCancel.TabIndex = 5;
            buttonCancel.Text = "Cancel";
            buttonCancel.UseVisualStyleBackColor = true;
            buttonCancel.Click += ButtonCancel_Click;
            // 
            // RemoveShoeModelForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.bright_background_image;
            ClientSize = new Size(1058, 496);
            Controls.Add(buttonCancel);
            Controls.Add(buttonDelete);
            Controls.Add(labelModelDetails);
            Controls.Add(buttonFind);
            Controls.Add(textBoxModelId);
            Controls.Add(labelModelId);
            Name = "RemoveShoeModelForm";
            Text = "Remove Shoe Model";
            ResumeLayout(false);
            PerformLayout();
        }

        private async void ButtonFind_Click(object sender, EventArgs e)
        {
            if (int.TryParse(textBoxModelId.Text, out int modelId))
            {
                var modelToDelete = await _modelService.GetModelByIdAsync(modelId);
                if (modelToDelete != null)
                {
                    labelModelDetails.Text = $"ID: {modelToDelete.Id}\n" +
                                             $"Brand: {modelToDelete.Brand}\n" +
                                             $"Code: {modelToDelete.Code}\n" +
                                             $"Shoe Type: {modelToDelete.ShoeType}\n" +
                                             $"Price: {modelToDelete.Price}\n" +
                                             $"Gender Category: {modelToDelete.GenderCategory}\n" +
                                             $"Description: {modelToDelete.Description}";
                    buttonDelete.Enabled = true;
                }
                else
                {
                    labelModelDetails.Text = "Shoe model not found.";
                    buttonDelete.Enabled = false;
                }
            }
            else
            {
                MessageBox.Show("Invalid model ID.");
            }
        }

        private async void ButtonDelete_Click(object sender, EventArgs e)
        {
            if (int.TryParse(textBoxModelId.Text, out int modelId))
            {
                var confirmation = MessageBox.Show("Are you sure you want to delete this model?", "Confirm Deletion", MessageBoxButtons.YesNo);
                if (confirmation == DialogResult.Yes)
                {
                    await _modelService.DeleteModelAsync(modelId);
                    MessageBox.Show("Shoe model deleted successfully.");
                    labelModelDetails.Text = string.Empty;
                    textBoxModelId.Text = string.Empty;
                    buttonDelete.Enabled = false;
                }
            }
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private Label labelModelId;
        private TextBox textBoxModelId;
        private Button buttonFind;
        private Label labelModelDetails;
        private Button buttonDelete;
        private Button buttonCancel;
    }
}
