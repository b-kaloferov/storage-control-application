using BusinessLayer;
using ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalUserInterface
{
    public partial class AddCustomerForm : Form
    {
        private readonly IClientService _clientService;

        public AddCustomerForm(IClientService clientService)
        {
            _clientService = clientService;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            txtName = new TextBox();
            txtAddress = new TextBox();
            txtEmail = new TextBox();
            txtPhoneNumber = new TextBox();
            btnSave = new Button();
            label1 = new Label();
            SuspendLayout();
            // 
            // txtName
            // 
            txtName.Location = new Point(72, 123);
            txtName.Name = "txtName";
            txtName.PlaceholderText = "Enter name";
            txtName.Size = new Size(200, 27);
            txtName.TabIndex = 0;
            // 
            // txtAddress
            // 
            txtAddress.Location = new Point(72, 163);
            txtAddress.Name = "txtAddress";
            txtAddress.PlaceholderText = "Enter address";
            txtAddress.Size = new Size(200, 27);
            txtAddress.TabIndex = 1;
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(72, 203);
            txtEmail.Name = "txtEmail";
            txtEmail.PlaceholderText = "Enter email";
            txtEmail.Size = new Size(200, 27);
            txtEmail.TabIndex = 2;
            // 
            // txtPhoneNumber
            // 
            txtPhoneNumber.Location = new Point(72, 243);
            txtPhoneNumber.Name = "txtPhoneNumber";
            txtPhoneNumber.PlaceholderText = "Enter phone number";
            txtPhoneNumber.Size = new Size(200, 27);
            txtPhoneNumber.TabIndex = 3;
            // 
            // btnSave
            // 
            btnSave.BackColor = Color.DodgerBlue;
            btnSave.Location = new Point(72, 283);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(200, 30);
            btnSave.TabIndex = 4;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += btnSave_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Arial", 18F, FontStyle.Bold, GraphicsUnit.Point, 204);
            label1.Location = new Point(32, 41);
            label1.Name = "label1";
            label1.Size = new Size(292, 35);
            label1.TabIndex = 5;
            label1.Text = "Add New Customer";
            // 
            // AddCustomerForm
            // 
            BackgroundImage = Properties.Resources.bright_background_image;
            ClientSize = new Size(350, 387);
            Controls.Add(label1);
            Controls.Add(btnSave);
            Controls.Add(txtPhoneNumber);
            Controls.Add(txtEmail);
            Controls.Add(txtAddress);
            Controls.Add(txtName);
            Name = "AddCustomerForm";
            Text = "Add Customer";
            ResumeLayout(false);
            PerformLayout();
        }

        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TextBox txtPhoneNumber;
        private Label label1;
        private System.Windows.Forms.Button btnSave;

        private async void btnSave_Click(object sender, EventArgs e)
        {
            var name = txtName.Text;
            var address = txtAddress.Text;
            var email = txtEmail.Text;
            var phoneNumber = txtPhoneNumber.Text;

            var newClient = new Client(name, address, email, phoneNumber);
            await _clientService.CreateClientAsync(newClient);
            MessageBox.Show("Customer added successfully.");
            this.Close();
        }
    }
}
