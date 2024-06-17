using BusinessLayer;
using ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace GraphicalUserInterface
{
    public partial class AddShoeModelForm : Form
    {
        private readonly IModelService _modelService;

        public AddShoeModelForm(IModelService modelService)
        {
            _modelService = modelService;
            InitializeComponent();
        }

        private async void btnSubmit_Click(object sender, EventArgs e)
        {
            string brand = txtBrand.Text;
            string code = txtCode.Text;
            string shoeType = txtShoeType.Text;
            if (!decimal.TryParse(txtPrice.Text, out decimal price))
            {
                MessageBox.Show("Invalid price. Please enter a valid decimal number.");
                return;
            }
            string genderCategory = txtGenderCategory.Text;
            string description = txtDescription.Text;

            Model newModel;
            if (string.IsNullOrWhiteSpace(description))
            {
                newModel = new Model(brand, code, shoeType, price, genderCategory);
            }
            else
            {
                newModel = new Model(brand, code, shoeType, price, genderCategory, description);
            }

            await _modelService.CreateModelAsync(newModel);
            MessageBox.Show("Shoe model added successfully.");
            this.Close();
        }

        private void InitializeComponent()
        {
            label1 = new Label();
            txtBrand = new TextBox();
            label2 = new Label();
            txtCode = new TextBox();
            label3 = new Label();
            txtShoeType = new TextBox();
            label4 = new Label();
            txtPrice = new TextBox();
            label5 = new Label();
            label6 = new Label();
            txtGenderCategory = new TextBox();
            txtDescription = new TextBox();
            btnSubmit = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(85, 81);
            label1.Name = "label1";
            label1.Size = new Size(48, 20);
            label1.TabIndex = 0;
            label1.Text = "Brand";
            // 
            // txtBrand
            // 
            txtBrand.Location = new Point(199, 86);
            txtBrand.Name = "txtBrand";
            txtBrand.Size = new Size(125, 27);
            txtBrand.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(91, 167);
            label2.Name = "label2";
            label2.Size = new Size(44, 20);
            label2.TabIndex = 2;
            label2.Text = "Code";
            // 
            // txtCode
            // 
            txtCode.Location = new Point(229, 177);
            txtCode.Name = "txtCode";
            txtCode.Size = new Size(125, 27);
            txtCode.TabIndex = 3;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(102, 280);
            label3.Name = "label3";
            label3.Size = new Size(77, 20);
            label3.TabIndex = 4;
            label3.Text = "Shoe Type";
            // 
            // txtShoeType
            // 
            txtShoeType.Location = new Point(267, 281);
            txtShoeType.Name = "txtShoeType";
            txtShoeType.Size = new Size(125, 27);
            txtShoeType.TabIndex = 5;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(108, 402);
            label4.Name = "label4";
            label4.Size = new Size(41, 20);
            label4.TabIndex = 6;
            label4.Text = "Price";
            // 
            // txtPrice
            // 
            txtPrice.Location = new Point(298, 406);
            txtPrice.Name = "txtPrice";
            txtPrice.Size = new Size(125, 27);
            txtPrice.TabIndex = 7;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(586, 94);
            label5.Name = "label5";
            label5.Size = new Size(121, 20);
            label5.TabIndex = 8;
            label5.Text = "Gender Category";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(598, 175);
            label6.Name = "label6";
            label6.Size = new Size(85, 20);
            label6.TabIndex = 9;
            label6.Text = "Description";
            // 
            // txtGenderCategory
            // 
            txtGenderCategory.Location = new Point(817, 92);
            txtGenderCategory.Name = "txtGenderCategory";
            txtGenderCategory.Size = new Size(125, 27);
            txtGenderCategory.TabIndex = 10;
            // 
            // txtDescription
            // 
            txtDescription.Location = new Point(813, 176);
            txtDescription.Name = "txtDescription";
            txtDescription.Size = new Size(125, 27);
            txtDescription.TabIndex = 11;
            // 
            // btnSubmit
            // 
            btnSubmit.Location = new Point(720, 295);
            btnSubmit.Name = "btnSubmit";
            btnSubmit.Size = new Size(94, 29);
            btnSubmit.TabIndex = 12;
            btnSubmit.Text = "Submit";
            btnSubmit.UseVisualStyleBackColor = true;
            btnSubmit.Click += btnSubmit_Click;
            // 
            // AddShoeModelForm
            // 
            BackgroundImage = Properties.Resources.bright_background_image;
            ClientSize = new Size(1047, 476);
            Controls.Add(btnSubmit);
            Controls.Add(txtDescription);
            Controls.Add(txtGenderCategory);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(txtPrice);
            Controls.Add(label4);
            Controls.Add(txtShoeType);
            Controls.Add(label3);
            Controls.Add(txtCode);
            Controls.Add(label2);
            Controls.Add(txtBrand);
            Controls.Add(label1);
            Name = "AddShoeModelForm";
            ResumeLayout(false);
            PerformLayout();
        }

        private Label label1;
        private TextBox txtBrand;
        private Label label2;
        private TextBox txtCode;
        private Label label3;
        private TextBox txtShoeType;
        private Label label4;
        private TextBox txtPrice;
        private Label label5;
        private Label label6;
        private TextBox txtGenderCategory;
        private Button btnSubmit;
        private TextBox txtDescription;
    }
}
