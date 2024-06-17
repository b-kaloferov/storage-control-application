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
            btnSubmit = new Button();
            label7 = new Label();
            txtDescription = new RichTextBox();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Arial", 12F);
            label1.Location = new Point(75, 119);
            label1.Name = "label1";
            label1.Size = new Size(62, 23);
            label1.TabIndex = 0;
            label1.Text = "Brand";
            // 
            // txtBrand
            // 
            txtBrand.Location = new Point(209, 115);
            txtBrand.Name = "txtBrand";
            txtBrand.Size = new Size(160, 27);
            txtBrand.TabIndex = 1;
            txtBrand.TextChanged += txtBrand_TextChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Arial", 12F);
            label2.Location = new Point(75, 197);
            label2.Name = "label2";
            label2.Size = new Size(57, 23);
            label2.TabIndex = 2;
            label2.Text = "Code";
            // 
            // txtCode
            // 
            txtCode.Location = new Point(209, 197);
            txtCode.Name = "txtCode";
            txtCode.Size = new Size(160, 27);
            txtCode.TabIndex = 3;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.Transparent;
            label3.Font = new Font("Arial", 12F);
            label3.Location = new Point(75, 305);
            label3.Name = "label3";
            label3.Size = new Size(104, 23);
            label3.TabIndex = 4;
            label3.Text = "Shoe Type";
            // 
            // txtShoeType
            // 
            txtShoeType.Location = new Point(209, 305);
            txtShoeType.Name = "txtShoeType";
            txtShoeType.Size = new Size(160, 27);
            txtShoeType.TabIndex = 5;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.Transparent;
            label4.Font = new Font("Arial", 12F);
            label4.Location = new Point(710, 190);
            label4.Name = "label4";
            label4.Size = new Size(55, 23);
            label4.TabIndex = 6;
            label4.Text = "Price";
            // 
            // txtPrice
            // 
            txtPrice.Location = new Point(795, 191);
            txtPrice.Name = "txtPrice";
            txtPrice.Size = new Size(125, 27);
            txtPrice.TabIndex = 7;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = Color.Transparent;
            label5.Font = new Font("Arial", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label5.Location = new Point(615, 137);
            label5.Name = "label5";
            label5.Size = new Size(163, 23);
            label5.TabIndex = 8;
            label5.Text = "Gender Category";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.BackColor = Color.Transparent;
            label6.Font = new Font("Arial", 12F);
            label6.Location = new Point(542, 305);
            label6.Name = "label6";
            label6.Size = new Size(108, 23);
            label6.TabIndex = 9;
            label6.Text = "Description";
            // 
            // txtGenderCategory
            // 
            txtGenderCategory.Location = new Point(795, 133);
            txtGenderCategory.Name = "txtGenderCategory";
            txtGenderCategory.Size = new Size(125, 27);
            txtGenderCategory.TabIndex = 10;
            // 
            // btnSubmit
            // 
            btnSubmit.BackColor = Color.DodgerBlue;
            btnSubmit.Location = new Point(786, 406);
            btnSubmit.Name = "btnSubmit";
            btnSubmit.Size = new Size(156, 46);
            btnSubmit.TabIndex = 12;
            btnSubmit.Text = "Submit";
            btnSubmit.UseVisualStyleBackColor = false;
            btnSubmit.Click += btnSubmit_Click;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.BackColor = Color.Transparent;
            label7.Font = new Font("Arial Narrow", 36F, FontStyle.Bold, GraphicsUnit.Point, 204);
            label7.ForeColor = Color.Black;
            label7.Location = new Point(327, 20);
            label7.Name = "label7";
            label7.Size = new Size(416, 69);
            label7.TabIndex = 13;
            label7.Text = "Add Shoe Model";
            // 
            // txtDescription
            // 
            txtDescription.Location = new Point(679, 259);
            txtDescription.Name = "txtDescription";
            txtDescription.Size = new Size(263, 120);
            txtDescription.TabIndex = 14;
            txtDescription.Text = "";
            // 
            // AddShoeModelForm
            // 
            BackgroundImage = Properties.Resources.bright_background_image;
            ClientSize = new Size(1047, 476);
            Controls.Add(txtDescription);
            Controls.Add(label7);
            Controls.Add(btnSubmit);
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
            MaximizeBox = false;
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
        private RichTextBox txtDescription;
        private Label label7;

        private void txtBrand_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
