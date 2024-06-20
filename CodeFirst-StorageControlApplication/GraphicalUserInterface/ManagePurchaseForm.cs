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
        private Label label1;
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
            btnCreatePurchase = new Button();
            btnCompletePurchase = new Button();
            label1 = new Label();
            SuspendLayout();
            // 
            // btnCreatePurchase
            // 
            btnCreatePurchase.BackColor = Color.DodgerBlue;
            btnCreatePurchase.Location = new Point(68, 98);
            btnCreatePurchase.Name = "btnCreatePurchase";
            btnCreatePurchase.Size = new Size(200, 50);
            btnCreatePurchase.TabIndex = 0;
            btnCreatePurchase.Text = "Create Purchase";
            btnCreatePurchase.UseVisualStyleBackColor = false;
            btnCreatePurchase.Click += btnCreatePurchase_Click;
            // 
            // btnCompletePurchase
            // 
            btnCompletePurchase.BackColor = Color.DodgerBlue;
            btnCompletePurchase.Location = new Point(68, 168);
            btnCompletePurchase.Name = "btnCompletePurchase";
            btnCompletePurchase.Size = new Size(200, 50);
            btnCompletePurchase.TabIndex = 1;
            btnCompletePurchase.Text = "Complete Purchase";
            btnCompletePurchase.UseVisualStyleBackColor = false;
            btnCompletePurchase.Click += btnCompletePurchase_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Arial", 18F, FontStyle.Bold, GraphicsUnit.Point, 204);
            label1.Location = new Point(24, 35);
            label1.Name = "label1";
            label1.Size = new Size(296, 35);
            label1.TabIndex = 2;
            label1.Text = "Manage a Purchase";
            // 
            // ManagePurchaseForm
            // 
            BackgroundImage = Properties.Resources.bright_background_image;
            ClientSize = new Size(341, 265);
            Controls.Add(label1);
            Controls.Add(btnCompletePurchase);
            Controls.Add(btnCreatePurchase);
            Name = "ManagePurchaseForm";
            Text = "Manage Purchase";
            ResumeLayout(false);
            PerformLayout();
        }

        private Button btnCreatePurchase;
        private Button btnCompletePurchase;
    }
}
