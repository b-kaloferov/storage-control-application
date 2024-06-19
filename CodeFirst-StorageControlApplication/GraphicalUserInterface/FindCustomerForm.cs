using ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalUserInterface
{
    public partial class FindCustomerForm : Form
    {
        private readonly IClientService _clientService;

        public FindCustomerForm(IClientService clientService)
        {
            _clientService = clientService;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            txtClientId = new TextBox();
            btnFind = new Button();
            txtResult = new TextBox();
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
            // txtResult
            // 
            txtResult.Location = new Point(50, 110);
            txtResult.Multiline = true;
            txtResult.Name = "txtResult";
            txtResult.ReadOnly = true;
            txtResult.Size = new Size(200, 100);
            txtResult.TabIndex = 2;
            // 
            // FindCustomerForm
            // 
            BackgroundImage = Properties.Resources.bright_background_image;
            ClientSize = new Size(1065, 487);
            Controls.Add(txtResult);
            Controls.Add(btnFind);
            Controls.Add(txtClientId);
            Name = "FindCustomerForm";
            Text = "Find Customer by ID";
            ResumeLayout(false);
            PerformLayout();
        }

        private System.Windows.Forms.TextBox txtClientId;
        private System.Windows.Forms.Button btnFind;
        private System.Windows.Forms.TextBox txtResult;

        private async void btnFind_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtClientId.Text, out int clientId))
            {
                var client = await _clientService.GetClientByIdAsync(clientId);
                if (client != null)
                {
                    txtResult.Text = $"Client ID: {client.Id}\nName: {client.Name}\nAddress: {client.Address}\nEmail: {client.Email}\nPhone Number: {client.PhoneNumber}";
                }
                else
                {
                    txtResult.Text = "Customer not found.";
                }
            }
            else
            {
                txtResult.Text = "Invalid customer ID.";
            }
        }
    }
}
