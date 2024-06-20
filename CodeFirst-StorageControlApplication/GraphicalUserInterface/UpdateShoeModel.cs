using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GraphicalUserInterface
{
    public partial class UpdateShoeModel : Form
    {
        private ModelService _modelService;

        public UpdateShoeModelForm(ModelService modelService)
        {
            InitializeComponent();
            _modelService = modelService;
        }

        private async void btnSearch_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtModelId.Text, out int modelId))
            {
                var modelToUpdate = await _modelService.GetModelByIdAsync(modelId);

                if (modelToUpdate != null)
                {
                    txtBrand.Text = modelToUpdate.Brand;
                    txtCode.Text = modelToUpdate.Code;
                    txtShoeType.Text = modelToUpdate.ShoeType;
                    txtPrice.Text = modelToUpdate.Price.ToString();
                    txtGenderCategory.Text = modelToUpdate.GenderCategory;
                    txtDescription.Text = modelToUpdate.Description;
                }
                else
                {
                    MessageBox.Show($"Shoe model with ID {modelId} not found.");
                }
            }
            else
            {
                MessageBox.Show("Invalid model ID.");
            }
        }

        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtModelId.Text, out int modelId))
            {
                var modelToUpdate = await _modelService.GetModelByIdAsync(modelId);

                if (modelToUpdate != null)
                {
                    modelToUpdate.Brand = txtBrand.Text;
                    modelToUpdate.Code = txtCode.Text;
                    modelToUpdate.ShoeType = txtShoeType.Text;

                    if (decimal.TryParse(txtPrice.Text, out decimal price))
                    {
                        modelToUpdate.Price = price;
                    }
                    else
                    {
                        MessageBox.Show("Invalid price.");
                        return;
                    }

                    modelToUpdate.GenderCategory = txtGenderCategory.Text;
                    modelToUpdate.Description = txtDescription.Text;

                    await _modelService.UpdateModelAsync(modelToUpdate);
                    MessageBox.Show("Shoe model updated successfully.");
                }
                else
                {
                    MessageBox.Show($"Shoe model with ID {modelId} not found.");
                }
            }
            else
            {
                MessageBox.Show("Invalid model ID.");
            }
        }
    }
}
