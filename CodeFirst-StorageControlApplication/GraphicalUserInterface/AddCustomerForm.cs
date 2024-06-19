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
            SuspendLayout();
            // 
            // txtName
            // 
            txtName.Location = new Point(50, 30);
            txtName.Name = "txtName";
            txtName.Size = new Size(200, 27);
            txtName.TabIndex = 0;
            // 
            // txtAddress
            // 
            txtAddress.Location = new Point(50, 70);
            txtAddress.Name = "txtAddress";
            txtAddress.Size = new Size(200, 27);
            txtAddress.TabIndex = 1;
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(50, 110);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(200, 27);
            txtEmail.TabIndex = 2;
            // 
            // txtPhoneNumber
            // 
            txtPhoneNumber.Location = new Point(50, 150);
            txtPhoneNumber.Name = "txtPhoneNumber";
            txtPhoneNumber.Size = new Size(200, 27);
            txtPhoneNumber.TabIndex = 3;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(50, 190);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(200, 30);
            btnSave.TabIndex = 4;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // AddCustomerForm
            // 
            BackgroundImage = Properties.Resources.bright_background_image;
            ClientSize = new Size(1062, 501);
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
