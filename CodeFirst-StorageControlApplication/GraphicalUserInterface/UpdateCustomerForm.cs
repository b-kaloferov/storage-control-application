using BusinessLayer;
using ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalUserInterface
{
    public partial class UpdateCustomerForm :  Form
    {
        private readonly IClientService _clientService;

        public UpdateCustomerForm(IClientService clientService)
        {
            _clientService = clientService;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            txtClientId = new TextBox();
            btnFind = new Button();
            txtName = new TextBox();
            txtAddress = new TextBox();
            txtEmail = new TextBox();
            txtPhoneNumber = new TextBox();
            btnSave = new Button();
            SuspendLayout();
            // 
            // txtClientId
            // 
            txtClientId.Location = new Point(50, 30);
            txtClientId.Name = "txtClientId";
            txtClientId.PlaceholderText = "Enter Customer ID";
            txtClientId.Size = new Size(200, 27);
            txtClientId.TabIndex = 0;
            // 
            // btnFind
            // 
            btnFind.Location = new Point(50, 70);
            btnFind.Name = "btnFind";
            btnFind.Size = new Size(200, 30);
            btnFind.TabIndex = 1;
            btnFind.Text = "Find";
            btnFind.UseVisualStyleBackColor = true;
            btnFind.Click += btnFind_Click;
            // 
            // txtName
            // 
            txtName.Location = new Point(50, 110);
            txtName.Name = "txtName";
            txtName.PlaceholderText = "Enter new name";
            txtName.Size = new Size(200, 27);
            txtName.TabIndex = 2;
            // 
            // txtAddress
            // 
            txtAddress.Location = new Point(50, 150);
            txtAddress.Name = "txtAddress";
            txtAddress.PlaceholderText = "Enter new address";
            txtAddress.Size = new Size(200, 27);
            txtAddress.TabIndex = 3;
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(50, 190);
            txtEmail.Name = "txtEmail";
            txtEmail.PlaceholderText = "Enter new email";
            txtEmail.Size = new Size(200, 27);
            txtEmail.TabIndex = 4;
            // 
            // txtPhoneNumber
            // 
            txtPhoneNumber.Location = new Point(50, 230);
            txtPhoneNumber.Name = "txtPhoneNumber";
            txtPhoneNumber.PlaceholderText = "Enter new phone number";
            txtPhoneNumber.Size = new Size(200, 27);
            txtPhoneNumber.TabIndex = 5;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(50, 270);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(200, 30);
            btnSave.TabIndex = 6;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // UpdateCustomerForm
            // 
            BackgroundImage = Properties.Resources.bright_background_image;
            ClientSize = new Size(1065, 495);
            Controls.Add(btnSave);
            Controls.Add(txtPhoneNumber);
            Controls.Add(txtEmail);
            Controls.Add(txtAddress);
            Controls.Add(txtName);
            Controls.Add(btnFind);
            Controls.Add(txtClientId);
            Name = "UpdateCustomerForm";
            Text = "Update Customer";
            ResumeLayout(false);
            PerformLayout();
        }

        private System.Windows.Forms.TextBox txtClientId;
        private System.Windows.Forms.Button btnFind;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TextBox txtPhoneNumber;
        private System.Windows.Forms.Button btnSave;
        private Client _client;

        private async void btnFind_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtClientId.Text, out int clientId))
            {
                _client = await _clientService.GetClientByIdAsync(clientId);
                if (_client != null)
                {
                    txtName.Text = _client.Name;
                    txtAddress.Text = _client.Address;
                    txtEmail.Text = _client.Email;
                    txtPhoneNumber.Text = _client.PhoneNumber;
                }
                else
                {
                    MessageBox.Show("Customer not found.");
                }
            }
            else
            {
                MessageBox.Show("Invalid customer ID.");
            }
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (_client != null)
            {
                _client.Name = string.IsNullOrEmpty(txtName.Text) ? _client.Name : txtName.Text;
                _client.Address = string.IsNullOrEmpty(txtAddress.Text) ? _client.Address : txtAddress.Text;
                _client.Email = string.IsNullOrEmpty(txtEmail.Text) ? _client.Email : txtEmail.Text;
                _client.PhoneNumber = string.IsNullOrEmpty(txtPhoneNumber.Text) ? _client.PhoneNumber : txtPhoneNumber.Text;

                await _clientService.UpdateClientAsync(_client);
                MessageBox.Show("Customer updated successfully.");
            }
        }
    }
}
