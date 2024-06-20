using BusinessLayer;
using ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalUserInterface
{
    public partial class CreatePurchaseForm : Form
    {
        private IClientService _clientService;
        private IModelService _modelService;
        private IOrderService _orderService;
        private IShoeService _shoeService;

        public CreatePurchaseForm(IClientService clientService, IModelService modelService, IOrderService orderService, IShoeService shoeService)
        {
            InitializeComponent();
            _clientService = clientService;
            _modelService = modelService;
            _orderService = orderService;
            _shoeService = shoeService;
        }

        private async void btnSubmit_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtClientId.Text, out int clientId))
            {
                MessageBox.Show("Invalid Client ID.");
                return;
            }

            var client = await _clientService.GetClientByIdAsync(clientId);
            if (client == null)
            {
                MessageBox.Show("Client not found.");
                return;
            }

            DateTime orderDate;
            if (rdoUseCurrentDate.Checked)
            {
                orderDate = DateTime.Now;
            }
            else if (!DateTime.TryParse(txtOrderDate.Text, out orderDate))
            {
                MessageBox.Show("Invalid date format. Using current date and time.");
                orderDate = DateTime.Now;
            }

            var order = new Order(orderDate, client);
            await _orderService.MakePurchaseAsync(order);

            List<OrderDetail> orderDetails = new List<OrderDetail>();
            bool continueAddingDetails = true;

            while (continueAddingDetails)
            {
                if (!int.TryParse(txtModelId.Text, out int modelId))
                {
                    MessageBox.Show("Invalid Shoe Model ID.");
                    continue;
                }

                var model = await _modelService.GetModelByIdAsync(modelId);
                if (model == null)
                {
                    MessageBox.Show($"Shoe model with ID {modelId} not found.");
                    continue;
                }

                if (!double.TryParse(txtShoeSize.Text, out double size))
                {
                    MessageBox.Show("Invalid size. Please enter a valid number.");
                    continue;
                }

                if (!int.TryParse(txtQuantity.Text, out int quantity) || quantity <= 0)
                {
                    MessageBox.Show("Invalid quantity. Please enter a positive integer.");
                    continue;
                }

                var shoesForModel = await _shoeService.GetShoesByModelIdAsync(modelId);
                var existingShoe = shoesForModel.FirstOrDefault(s => s.Size == size);
                if (existingShoe == null)
                {
                    MessageBox.Show($"No shoe found with size {size} for model ID {modelId}.");
                    continue;
                }

                var orderDetail = new OrderDetail(quantity, existingShoe, order);
                orderDetails.Add(orderDetail);

                var result = MessageBox.Show("Do you want to add another shoe to the order?", "Add another?", MessageBoxButtons.YesNo);
                continueAddingDetails = result == DialogResult.Yes;
            }

            if (orderDetails.Count == 0)
            {
                MessageBox.Show("No order details added.");
                return;
            }

            order.OrderDetails = orderDetails;
            await _orderService.UpdateOrderAsync(order);
            MessageBox.Show("Order created successfully.");
        }

        private void InitializeComponent()
        {
            this.txtClientId = new System.Windows.Forms.TextBox();
            this.txtOrderDate = new System.Windows.Forms.TextBox();
            this.rdoUseCurrentDate = new System.Windows.Forms.RadioButton();
            this.rdoEnterDate = new System.Windows.Forms.RadioButton();
            this.txtModelId = new System.Windows.Forms.TextBox();
            this.txtShoeSize = new System.Windows.Forms.TextBox();
            this.txtQuantity = new System.Windows.Forms.TextBox();
            this.btnSubmit = new System.Windows.Forms.Button();

            // 
            // txtClientId
            // 
            this.txtClientId.Location = new System.Drawing.Point(30, 30);
            this.txtClientId.Name = "txtClientId";
            this.txtClientId.Size = new System.Drawing.Size(200, 20);
            this.txtClientId.TabIndex = 0;
            this.txtClientId.PlaceholderText = "Client ID";

            // 
            // rdoUseCurrentDate
            // 
            this.rdoUseCurrentDate.Location = new System.Drawing.Point(30, 60);
            this.rdoUseCurrentDate.Name = "rdoUseCurrentDate";
            this.rdoUseCurrentDate.Size = new System.Drawing.Size(200, 20);
            this.rdoUseCurrentDate.TabIndex = 1;
            this.rdoUseCurrentDate.Text = "Use current date and time";
            this.rdoUseCurrentDate.CheckedChanged += new EventHandler(RadioButton_CheckedChanged);

            // 
            // rdoEnterDate
            // 
            this.rdoEnterDate.Location = new System.Drawing.Point(30, 90);
            this.rdoEnterDate.Name = "rdoEnterDate";
            this.rdoEnterDate.Size = new System.Drawing.Size(200, 20);
            this.rdoEnterDate.TabIndex = 2;
            this.rdoEnterDate.Text = "Enter desired date and time";
            this.rdoEnterDate.CheckedChanged += new EventHandler(RadioButton_CheckedChanged);

            // 
            // txtOrderDate
            // 
            this.txtOrderDate.Location = new System.Drawing.Point(30, 120);
            this.txtOrderDate.Name = "txtOrderDate";
            this.txtOrderDate.Size = new System.Drawing.Size(200, 20);
            this.txtOrderDate.TabIndex = 3;
            this.txtOrderDate.PlaceholderText = "yyyy-MM-dd HH:mm";

            // 
            // txtModelId
            // 
            this.txtModelId.Location = new System.Drawing.Point(30, 150);
            this.txtModelId.Name = "txtModelId";
            this.txtModelId.Size = new System.Drawing.Size(200, 20);
            this.txtModelId.TabIndex = 4;
            this.txtModelId.PlaceholderText = "Model ID";

            // 
            // txtShoeSize
            // 
            this.txtShoeSize.Location = new System.Drawing.Point(30, 180);
            this.txtShoeSize.Name = "txtShoeSize";
            this.txtShoeSize.Size = new System.Drawing.Size(200, 20);
            this.txtShoeSize.TabIndex = 5;
            this.txtShoeSize.PlaceholderText = "Shoe Size";

            // 
            // txtQuantity
            // 
            this.txtQuantity.Location = new System.Drawing.Point(30, 210);
            this.txtQuantity.Name = "txtQuantity";
            this.txtQuantity.Size = new System.Drawing.Size(200, 20);
            this.txtQuantity.TabIndex = 6;
            this.txtQuantity.PlaceholderText = "Quantity";

            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(30, 240);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(200, 30);
            this.btnSubmit.TabIndex = 7;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);

            // 
            // CreatePurchaseForm
            // 
            this.ClientSize = new System.Drawing.Size(284, 291);
            this.Controls.Add(this.txtClientId);
            this.Controls.Add(this.txtOrderDate);
            this.Controls.Add(this.rdoUseCurrentDate);
            this.Controls.Add(this.rdoEnterDate);
            this.Controls.Add(this.txtModelId);
            this.Controls.Add(this.txtShoeSize);
            this.Controls.Add(this.txtQuantity);
            this.Controls.Add(this.btnSubmit);
            this.Name = "CreatePurchaseForm";
            this.Text = "Create Purchase";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            txtOrderDate.Enabled = rdoEnterDate.Checked;
        }

        private TextBox txtClientId;
        private TextBox txtOrderDate;
        private RadioButton rdoUseCurrentDate;
        private RadioButton rdoEnterDate;
        private TextBox txtModelId;
        private TextBox txtShoeSize;
        private TextBox txtQuantity;
        private Button btnSubmit;
    }
}
