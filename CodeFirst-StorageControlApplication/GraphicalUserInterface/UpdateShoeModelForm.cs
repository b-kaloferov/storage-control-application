using ServiceLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ServiceLayer;

namespace GraphicalUserInterface
{
    public partial class UpdateShoeModelForm : Form
    {
        private IModelService _modelService;

        private TextBox txtModelId;
        private TextBox txtBrand;
        private TextBox txtCode;
        private TextBox txtShoeType;
        private TextBox txtPrice;
        private TextBox txtGenderCategory;
        private TextBox txtDescription;
        private Button btnSearch;
        private Button btnUpdate;

        public UpdateShoeModelForm(IModelService modelService)
        {
            InitializeComponent();
            _modelService = modelService;
        }

        private void InitializeComponent()
        {
            this.txtModelId = new TextBox();
            this.txtBrand = new TextBox();
            this.txtCode = new TextBox();
            this.txtShoeType = new TextBox();
            this.txtPrice = new TextBox();
            this.txtGenderCategory = new TextBox();
            this.txtDescription = new TextBox();
            this.btnSearch = new Button();
            this.btnUpdate = new Button();

            // txtModelId
            this.txtModelId.Location = new System.Drawing.Point(120, 30);
            this.txtModelId.Name = "txtModelId";
            this.txtModelId.Size = new System.Drawing.Size(200, 22);

            // txtBrand
            this.txtBrand.Location = new System.Drawing.Point(120, 70);
            this.txtBrand.Name = "txtBrand";
            this.txtBrand.Size = new System.Drawing.Size(200, 22);

            // txtCode
            this.txtCode.Location = new System.Drawing.Point(120, 110);
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(200, 22);

            // txtShoeType
            this.txtShoeType.Location = new System.Drawing.Point(120, 150);
            this.txtShoeType.Name = "txtShoeType";
            this.txtShoeType.Size = new System.Drawing.Size(200, 22);

            // txtPrice
            this.txtPrice.Location = new System.Drawing.Point(120, 190);
            this.txtPrice.Name = "txtPrice";
            this.txtPrice.Size = new System.Drawing.Size(200, 22);

            // txtGenderCategory
            this.txtGenderCategory.Location = new System.Drawing.Point(120, 230);
            this.txtGenderCategory.Name = "txtGenderCategory";
            this.txtGenderCategory.Size = new System.Drawing.Size(200, 22);

            // txtDescription
            this.txtDescription.Location = new System.Drawing.Point(120, 270);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(200, 22);

            // btnSearch
            this.btnSearch.Location = new System.Drawing.Point(330, 30);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.Text = "Search";
            this.btnSearch.Click += new EventHandler(this.btnSearch_Click);

            // btnUpdate
            this.btnUpdate.Location = new System.Drawing.Point(120, 310);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 23);
            this.btnUpdate.Text = "Update";
            this.btnUpdate.Click += new EventHandler(this.btnUpdate_Click);

            // UpdateShoeModelForm
            this.ClientSize = new System.Drawing.Size(434, 361);
            this.Controls.Add(this.txtModelId);
            this.Controls.Add(this.txtBrand);
            this.Controls.Add(this.txtCode);
            this.Controls.Add(this.txtShoeType);
            this.Controls.Add(this.txtPrice);
            this.Controls.Add(this.txtGenderCategory);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.btnUpdate);
            this.Text = "Update Shoe Model";
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
