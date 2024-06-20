using ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalUserInterface
{
    public partial class ManagePurchaseForm : Form
    {
        private IClientService _clientService;
        private IModelService _modelService;
        private IOrderService _orderService;
        private IShoeService _shoeService;

        public ManagePurchaseForm(IClientService clientService, IModelService modelService, IOrderService orderService, IShoeService shoeService)
        {
            InitializeComponent();
            _clientService = clientService;
            _modelService = modelService;
            _orderService = orderService;
            _shoeService = shoeService;
        }

        private void btnCreatePurchase_Click(object sender, EventArgs e)
        {
            var createPurchaseForm = new CreatePurchaseForm(_clientService, _modelService, _orderService, _shoeService);
            createPurchaseForm.ShowDialog();
        }

        private void btnCompletePurchase_Click(object sender, EventArgs e)
        {
            var completePurchaseForm = new CompletePurchaseForm(_orderService, _shoeService);
            completePurchaseForm.ShowDialog();
        }

        private void InitializeComponent()
        {
            this.btnCreatePurchase = new System.Windows.Forms.Button();
            this.btnCompletePurchase = new System.Windows.Forms.Button();

            // 
            // btnCreatePurchase
            // 
            this.btnCreatePurchase.Location = new System.Drawing.Point(30, 30);
            this.btnCreatePurchase.Name = "btnCreatePurchase";
            this.btnCreatePurchase.Size = new System.Drawing.Size(200, 50);
            this.btnCreatePurchase.TabIndex = 0;
            this.btnCreatePurchase.Text = "Create Purchase";
            this.btnCreatePurchase.UseVisualStyleBackColor = true;
            this.btnCreatePurchase.Click += new System.EventHandler(this.btnCreatePurchase_Click);

            // 
            // btnCompletePurchase
            // 
            this.btnCompletePurchase.Location = new System.Drawing.Point(30, 100);
            this.btnCompletePurchase.Name = "btnCompletePurchase";
            this.btnCompletePurchase.Size = new System.Drawing.Size(200, 50);
            this.btnCompletePurchase.TabIndex = 1;
            this.btnCompletePurchase.Text = "Complete Purchase";
            this.btnCompletePurchase.UseVisualStyleBackColor = true;
            this.btnCompletePurchase.Click += new System.EventHandler(this.btnCompletePurchase_Click);

            // 
            // ManagePurchaseForm
            // 
            this.ClientSize = new System.Drawing.Size(284, 181);
            this.Controls.Add(this.btnCompletePurchase);
            this.Controls.Add(this.btnCreatePurchase);
            this.Name = "ManagePurchaseForm";
            this.Text = "Manage Purchase";
            this.ResumeLayout(false);
        }

        private Button btnCreatePurchase;
        private Button btnCompletePurchase;
    }
}
