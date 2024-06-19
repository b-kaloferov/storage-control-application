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
            this.btnAddCustomer = new System.Windows.Forms.Button();
            this.btnFindCustomerById = new System.Windows.Forms.Button();
            this.btnViewAllCustomers = new System.Windows.Forms.Button();
            this.btnUpdateCustomer = new System.Windows.Forms.Button();
            this.btnRemoveCustomer = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnAddCustomer
            // 
            this.btnAddCustomer.Location = new System.Drawing.Point(50, 30);
            this.btnAddCustomer.Name = "btnAddCustomer";
            this.btnAddCustomer.Size = new System.Drawing.Size(200, 50);
            this.btnAddCustomer.TabIndex = 0;
            this.btnAddCustomer.Text = "Add New Customer";
            this.btnAddCustomer.UseVisualStyleBackColor = true;
            this.btnAddCustomer.Click += new System.EventHandler(this.btnAddCustomer_Click);
            // 
            // btnFindCustomerById
            // 
            this.btnFindCustomerById.Location = new System.Drawing.Point(50, 90);
            this.btnFindCustomerById.Name = "btnFindCustomerById";
            this.btnFindCustomerById.Size = new System.Drawing.Size(200, 50);
            this.btnFindCustomerById.TabIndex = 1;
            this.btnFindCustomerById.Text = "Find Customer by ID";
            this.btnFindCustomerById.UseVisualStyleBackColor = true;
            this.btnFindCustomerById.Click += new System.EventHandler(this.btnFindCustomerById_Click);
            // 
            // btnViewAllCustomers
            // 
            this.btnViewAllCustomers.Location = new System.Drawing.Point(50, 150);
            this.btnViewAllCustomers.Name = "btnViewAllCustomers";
            this.btnViewAllCustomers.Size = new System.Drawing.Size(200, 50);
            this.btnViewAllCustomers.TabIndex = 2;
            this.btnViewAllCustomers.Text = "View All Customers";
            this.btnViewAllCustomers.UseVisualStyleBackColor = true;
            this.btnViewAllCustomers.Click += new System.EventHandler(this.btnViewAllCustomers_Click);
            // 
            // btnUpdateCustomer
            // 
            this.btnUpdateCustomer.Location = new System.Drawing.Point(50, 210);
            this.btnUpdateCustomer.Name = "btnUpdateCustomer";
            this.btnUpdateCustomer.Size = new System.Drawing.Size(200, 50);
            this.btnUpdateCustomer.TabIndex = 3;
            this.btnUpdateCustomer.Text = "Update Customer";
            this.btnUpdateCustomer.UseVisualStyleBackColor = true;
            this.btnUpdateCustomer.Click += new System.EventHandler(this.btnUpdateCustomer_Click);
            // 
            // btnRemoveCustomer
            // 
            this.btnRemoveCustomer.Location = new System.Drawing.Point(50, 270);
            this.btnRemoveCustomer.Name = "btnRemoveCustomer";
            this.btnRemoveCustomer.Size = new System.Drawing.Size(200, 50);
            this.btnRemoveCustomer.TabIndex = 4;
            this.btnRemoveCustomer.Text = "Remove Customer";
            this.btnRemoveCustomer.UseVisualStyleBackColor = true;
            this.btnRemoveCustomer.Click += new System.EventHandler(this.btnRemoveCustomer_Click);
            // 
            // ManageCustomersForm
            // 
            this.ClientSize = new System.Drawing.Size(300, 350);
            this.Controls.Add(this.btnRemoveCustomer);
            this.Controls.Add(this.btnUpdateCustomer);
            this.Controls.Add(this.btnViewAllCustomers);
            this.Controls.Add(this.btnFindCustomerById);
            this.Controls.Add(this.btnAddCustomer);
            this.Name = "ManageCustomersForm";
            this.Text = "Customer Management";
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Button btnAddCustomer;
        private System.Windows.Forms.Button btnFindCustomerById;
        private System.Windows.Forms.Button btnViewAllCustomers;
        private System.Windows.Forms.Button btnUpdateCustomer;
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
