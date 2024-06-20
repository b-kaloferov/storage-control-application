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
            txtModelId = new TextBox();
            txtBrand = new TextBox();
            txtCode = new TextBox();
            txtShoeType = new TextBox();
            txtPrice = new TextBox();
            txtGenderCategory = new TextBox();
            txtDescription = new TextBox();
            btnSearch = new Button();
            btnUpdate = new Button();
            btnClose = new Button();
            label3 = new Label();
            SuspendLayout();
            // 
            // txtModelId
            // 
            txtModelId.Location = new Point(73, 123);
            txtModelId.Name = "txtModelId";
            txtModelId.PlaceholderText = "Enter Shoe Model Id";
            txtModelId.Size = new Size(200, 27);
            txtModelId.TabIndex = 0;
            // 
            // txtBrand
            // 
            txtBrand.Location = new Point(120, 189);
            txtBrand.Name = "txtBrand";
            txtBrand.Size = new Size(200, 27);
            txtBrand.TabIndex = 1;
            // 
            // txtCode
            // 
            txtCode.Location = new Point(120, 229);
            txtCode.Name = "txtCode";
            txtCode.Size = new Size(200, 27);
            txtCode.TabIndex = 2;
            // 
            // txtShoeType
            // 
            txtShoeType.Location = new Point(120, 269);
            txtShoeType.Name = "txtShoeType";
            txtShoeType.Size = new Size(200, 27);
            txtShoeType.TabIndex = 3;
            // 
            // txtPrice
            // 
            txtPrice.Location = new Point(120, 309);
            txtPrice.Name = "txtPrice";
            txtPrice.Size = new Size(200, 27);
            txtPrice.TabIndex = 4;
            // 
            // txtGenderCategory
            // 
            txtGenderCategory.Location = new Point(120, 349);
            txtGenderCategory.Name = "txtGenderCategory";
            txtGenderCategory.Size = new Size(200, 27);
            txtGenderCategory.TabIndex = 5;
            // 
            // txtDescription
            // 
            txtDescription.Location = new Point(120, 389);
            txtDescription.Name = "txtDescription";
            txtDescription.Size = new Size(200, 27);
            txtDescription.TabIndex = 6;
            // 
            // btnSearch
            // 
            btnSearch.BackColor = Color.DodgerBlue;
            btnSearch.Location = new Point(283, 123);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(94, 27);
            btnSearch.TabIndex = 7;
            btnSearch.Text = "Search";
            btnSearch.UseVisualStyleBackColor = false;
            btnSearch.Click += btnSearch_Click;
            // 
            // btnUpdate
            // 
            btnUpdate.BackColor = Color.DodgerBlue;
            btnUpdate.Location = new Point(89, 444);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(77, 31);
            btnUpdate.TabIndex = 8;
            btnUpdate.Text = "Update";
            btnUpdate.UseVisualStyleBackColor = false;
            btnUpdate.Click += btnUpdate_Click;
            // 
            // btnClose
            // 
            btnClose.BackColor = Color.DodgerBlue;
            btnClose.Location = new Point(294, 445);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(94, 29);
            btnClose.TabIndex = 9;
            btnClose.Text = "Close";
            btnClose.UseVisualStyleBackColor = false;
            btnClose.Click += this.btnClose_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.Transparent;
            label3.Font = new Font("Arial", 18F, FontStyle.Bold, GraphicsUnit.Point, 204);
            label3.Location = new Point(71, 46);
            label3.Name = "label3";
            label3.Size = new Size(317, 35);
            label3.TabIndex = 10;
            label3.Text = "Update a Shoe Model";
            // 
            // UpdateShoeModelForm
            // 
            BackgroundImage = Properties.Resources.bright_background_image;
            ClientSize = new Size(431, 501);
            Controls.Add(label3);
            Controls.Add(btnClose);
            Controls.Add(txtModelId);
            Controls.Add(txtBrand);
            Controls.Add(txtCode);
            Controls.Add(txtShoeType);
            Controls.Add(txtPrice);
            Controls.Add(txtGenderCategory);
            Controls.Add(txtDescription);
            Controls.Add(btnSearch);
            Controls.Add(btnUpdate);
            Name = "UpdateShoeModelForm";
            Text = "Update Shoe Model";
            ResumeLayout(false);
            PerformLayout();
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

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
       
    }
}
