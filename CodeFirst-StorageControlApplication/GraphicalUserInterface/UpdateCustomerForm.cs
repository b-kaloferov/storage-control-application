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
            label1 = new Label();
            SuspendLayout();
            // 
            // txtClientId
            // 
            txtClientId.Location = new Point(92, 170);
            txtClientId.Name = "txtClientId";
            txtClientId.PlaceholderText = "Enter Customer ID";
            txtClientId.Size = new Size(200, 27);
            txtClientId.TabIndex = 0;
            // 
            // btnFind
            // 
            btnFind.BackColor = Color.DodgerBlue;
            btnFind.Location = new Point(92, 210);
            btnFind.Name = "btnFind";
            btnFind.Size = new Size(200, 30);
            btnFind.TabIndex = 1;
            btnFind.Text = "Find";
            btnFind.UseVisualStyleBackColor = false;
            btnFind.Click += btnFind_Click;
            // 
            // txtName
            // 
            txtName.Location = new Point(92, 250);
            txtName.Name = "txtName";
            txtName.PlaceholderText = "Enter new name";
            txtName.Size = new Size(200, 27);
            txtName.TabIndex = 2;
            // 
            // txtAddress
            // 
            txtAddress.Location = new Point(92, 290);
            txtAddress.Name = "txtAddress";
            txtAddress.PlaceholderText = "Enter new address";
            txtAddress.Size = new Size(200, 27);
            txtAddress.TabIndex = 3;
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(92, 330);
            txtEmail.Name = "txtEmail";
            txtEmail.PlaceholderText = "Enter new email";
            txtEmail.Size = new Size(200, 27);
            txtEmail.TabIndex = 4;
            // 
            // txtPhoneNumber
            // 
            txtPhoneNumber.Location = new Point(92, 370);
            txtPhoneNumber.Name = "txtPhoneNumber";
            txtPhoneNumber.PlaceholderText = "Enter new phone number";
            txtPhoneNumber.Size = new Size(200, 27);
            txtPhoneNumber.TabIndex = 5;
            // 
            // btnSave
            // 
            btnSave.BackColor = Color.DodgerBlue;
            btnSave.Location = new Point(92, 410);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(200, 30);
            btnSave.TabIndex = 6;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += btnSave_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Arial", 18F, FontStyle.Bold, GraphicsUnit.Point, 204);
            label1.Location = new Point(64, 61);
            label1.Name = "label1";
            label1.Size = new Size(265, 35);
            label1.TabIndex = 7;
            label1.Text = "Update Customer";
            // 
            // UpdateCustomerForm
            // 
            BackgroundImage = Properties.Resources.bright_background_image;
            ClientSize = new Size(392, 495);
            Controls.Add(label1);
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
        private Label label1;
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
