using ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalUserInterface
{
    public partial class ViewAllCustomersForm : Form
    {
        private readonly IClientService _clientService;

        public ViewAllCustomersForm(IClientService clientService)
        {
            _clientService = clientService;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            txtResult = new TextBox();
            SuspendLayout();
            // 
            // txtResult
            // 
            txtResult.Location = new Point(12, 12);
            txtResult.Multiline = true;
            txtResult.Name = "txtResult";
            txtResult.ReadOnly = true;
            txtResult.ScrollBars = ScrollBars.Vertical;
            txtResult.Size = new Size(400, 300);
            txtResult.TabIndex = 0;
            // 
            // ViewAllCustomersForm
            // 
            BackgroundImage = Properties.Resources.bright_background_image;
            ClientSize = new Size(1066, 502);
            Controls.Add(txtResult);
            Name = "ViewAllCustomersForm";
            Text = "View All Customers";
            Load += ViewAllCustomersForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        private System.Windows.Forms.TextBox txtResult;

        private async void ViewAllCustomersForm_Load(object sender, EventArgs e)
        {
            var clients = await _clientService.GetAllClientsAsync();
            if (clients.Count > 0)
            {
                foreach (var client in clients)
                {
                    txtResult.AppendText($"Client ID: {client.Id}\nName: {client.Name}\nAddress: {client.Address}\nEmail: {client.Email}\nPhone Number: {client.PhoneNumber}\n\n");
                }
            }
            else
            {
                txtResult.Text = "No customers found.";
            }
        }
    }
}
