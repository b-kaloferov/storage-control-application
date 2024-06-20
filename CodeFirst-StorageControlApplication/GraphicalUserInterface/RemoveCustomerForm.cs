using ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalUserInterface
{
    public partial class RemoveCustomerForm : Form
    {
        private readonly IClientService _clientService;

        public RemoveCustomerForm(IClientService clientService)
        {
            _clientService = clientService;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            txtClientId = new TextBox();
            btnRemove = new Button();
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
            // btnRemove
            // 
            btnRemove.Location = new Point(50, 70);
            btnRemove.Name = "btnRemove";
            btnRemove.Size = new Size(200, 30);
            btnRemove.TabIndex = 1;
            btnRemove.Text = "Remove";
            btnRemove.UseVisualStyleBackColor = true;
            btnRemove.Click += btnRemove_Click;
            // 
            // RemoveCustomerForm
            // 
            BackgroundImage = Properties.Resources.bright_background_image;
            ClientSize = new Size(506, 427);
            Controls.Add(btnRemove);
            Controls.Add(txtClientId);
            Name = "RemoveCustomerForm";
            Text = "Remove Customer";
            ResumeLayout(false);
            PerformLayout();
        }

        private System.Windows.Forms.TextBox txtClientId;
        private System.Windows.Forms.Button btnRemove;

        private async void btnRemove_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtClientId.Text, out int clientId))
            {
                var confirmation = MessageBox.Show("Are you sure you want to delete this client?", "Confirm Deletion", MessageBoxButtons.YesNo);
                if (confirmation == DialogResult.Yes)
                {
                    await _clientService.DeleteClientAsync(clientId);
                    MessageBox.Show("Customer removed successfully.");
                    txtClientId.Text = string.Empty;
                }
            }
            else
            {
                MessageBox.Show("Invalid customer ID.");
            }
        }
    }
}
