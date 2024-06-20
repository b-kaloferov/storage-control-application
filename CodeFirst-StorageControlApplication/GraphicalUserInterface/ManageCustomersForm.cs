using ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalUserInterface
{
    public partial class ManageCustomersForm : Form
    {
        private readonly IClientService _clientService;

        public ManageCustomersForm(IClientService clientService)
        {
            _clientService = clientService;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            btnAddCustomer = new Button();
            btnFindCustomerById = new Button();
            btnViewAllCustomers = new Button();
            btnUpdateCustomer = new Button();
            btnRemoveCustomer = new Button();
            label1 = new Label();
            SuspendLayout();
            // 
            // btnAddCustomer
            // 
            btnAddCustomer.BackColor = Color.DodgerBlue;
            btnAddCustomer.Location = new Point(70, 155);
            btnAddCustomer.Name = "btnAddCustomer";
            btnAddCustomer.Size = new Size(200, 50);
            btnAddCustomer.TabIndex = 0;
            btnAddCustomer.Text = "Add New Customer";
            btnAddCustomer.UseVisualStyleBackColor = false;
            btnAddCustomer.Click += btnAddCustomer_Click;
            // 
            // btnFindCustomerById
            // 
            btnFindCustomerById.BackColor = Color.DodgerBlue;
            btnFindCustomerById.Location = new Point(70, 215);
            btnFindCustomerById.Name = "btnFindCustomerById";
            btnFindCustomerById.Size = new Size(200, 50);
            btnFindCustomerById.TabIndex = 1;
            btnFindCustomerById.Text = "Find Customer by ID";
            btnFindCustomerById.UseVisualStyleBackColor = false;
            btnFindCustomerById.Click += btnFindCustomerById_Click;
            // 
            // btnViewAllCustomers
            // 
            btnViewAllCustomers.BackColor = Color.DodgerBlue;
            btnViewAllCustomers.Location = new Point(70, 275);
            btnViewAllCustomers.Name = "btnViewAllCustomers";
            btnViewAllCustomers.Size = new Size(200, 50);
            btnViewAllCustomers.TabIndex = 2;
            btnViewAllCustomers.Text = "View All Customers";
            btnViewAllCustomers.UseVisualStyleBackColor = false;
            btnViewAllCustomers.Click += btnViewAllCustomers_Click;
            // 
            // btnUpdateCustomer
            // 
            btnUpdateCustomer.BackColor = Color.DodgerBlue;
            btnUpdateCustomer.Location = new Point(70, 335);
            btnUpdateCustomer.Name = "btnUpdateCustomer";
            btnUpdateCustomer.Size = new Size(200, 50);
            btnUpdateCustomer.TabIndex = 3;
            btnUpdateCustomer.Text = "Update Customer";
            btnUpdateCustomer.UseVisualStyleBackColor = false;
            btnUpdateCustomer.Click += btnUpdateCustomer_Click;
            // 
            // btnRemoveCustomer
            // 
            btnRemoveCustomer.BackColor = Color.DodgerBlue;
            btnRemoveCustomer.Location = new Point(70, 395);
            btnRemoveCustomer.Name = "btnRemoveCustomer";
            btnRemoveCustomer.Size = new Size(200, 50);
            btnRemoveCustomer.TabIndex = 4;
            btnRemoveCustomer.Text = "Remove Customer";
            btnRemoveCustomer.UseVisualStyleBackColor = false;
            btnRemoveCustomer.Click += btnRemoveCustomer_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Arial", 18F, FontStyle.Bold, GraphicsUnit.Point, 204);
            label1.Location = new Point(32, 51);
            label1.Name = "label1";
            label1.Size = new Size(292, 35);
            label1.TabIndex = 5;
            label1.Text = "Manage Customers";
            // 
            // ManageCustomersForm
            // 
            BackgroundImage = Properties.Resources.bright_background_image;
            ClientSize = new Size(347, 513);
            Controls.Add(label1);
            Controls.Add(btnRemoveCustomer);
            Controls.Add(btnUpdateCustomer);
            Controls.Add(btnViewAllCustomers);
            Controls.Add(btnFindCustomerById);
            Controls.Add(btnAddCustomer);
            MaximizeBox = false;
            Name = "ManageCustomersForm";
            Text = "Customer Management";
            ResumeLayout(false);
            PerformLayout();
        }

        private System.Windows.Forms.Button btnAddCustomer;
        private System.Windows.Forms.Button btnFindCustomerById;
        private System.Windows.Forms.Button btnViewAllCustomers;
        private System.Windows.Forms.Button btnUpdateCustomer;
        private Label label1;
        private System.Windows.Forms.Button btnRemoveCustomer;

        private void btnAddCustomer_Click(object sender, EventArgs e)
        {
            var addCustomerForm = new AddCustomerForm(_clientService);
            addCustomerForm.ShowDialog();
        }

        private void btnFindCustomerById_Click(object sender, EventArgs e)
        {
            var findCustomerForm = new FindCustomerForm(_clientService);
            findCustomerForm.ShowDialog();
        }

        private void btnViewAllCustomers_Click(object sender, EventArgs e)
        {
            var viewAllCustomersForm = new ViewAllCustomersForm(_clientService);
            viewAllCustomersForm.ShowDialog();
        }

        private void btnUpdateCustomer_Click(object sender, EventArgs e)
        {
            var updateCustomerForm = new UpdateCustomerForm(_clientService);
            updateCustomerForm.ShowDialog();
        }

        private void btnRemoveCustomer_Click(object sender, EventArgs e)
        {
            var removeCustomerForm = new RemoveCustomerForm(_clientService);
            removeCustomerForm.ShowDialog();
        }
    }
}
