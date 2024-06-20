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
            label1 = new Label();
            SuspendLayout();
            // 
            // txtClientId
            // 
            txtClientId.Location = new Point(79, 149);
            txtClientId.Name = "txtClientId";
            txtClientId.PlaceholderText = "Enter Customer ID";
            txtClientId.Size = new Size(200, 27);
            txtClientId.TabIndex = 0;
            txtClientId.TextChanged += txtClientId_TextChanged;
            // 
            // btnFind
            // 
            btnFind.BackColor = Color.DodgerBlue;
            btnFind.Location = new Point(79, 189);
            btnFind.Name = "btnFind";
            btnFind.Size = new Size(200, 30);
            btnFind.TabIndex = 1;
            btnFind.Text = "Find";
            btnFind.UseVisualStyleBackColor = false;
            btnFind.Click += btnFind_Click;
            // 
            // txtResult
            // 
            txtResult.Location = new Point(79, 229);
            txtResult.Multiline = true;
            txtResult.Name = "txtResult";
            txtResult.ReadOnly = true;
            txtResult.Size = new Size(200, 100);
            txtResult.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Arial", 18F, FontStyle.Bold, GraphicsUnit.Point, 204);
            label1.Location = new Point(68, 50);
            label1.Name = "label1";
            label1.Size = new Size(225, 35);
            label1.TabIndex = 3;
            label1.Text = "Find Customer";
            // 
            // FindCustomerForm
            // 
            BackgroundImage = Properties.Resources.bright_background_image;
            ClientSize = new Size(367, 385);
            Controls.Add(label1);
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
        private Label label1;
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

        private void txtClientId_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
