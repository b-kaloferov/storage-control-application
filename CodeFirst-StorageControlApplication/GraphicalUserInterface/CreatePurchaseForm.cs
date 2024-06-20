using BusinessLayer;
using ServiceLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GraphicalUserInterface
{
    public partial class CreatePurchaseForm : Form
    {
        private IClientService _clientService;
        private IModelService _modelService;
        private IOrderService _orderService;
        private Label label1;
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
            txtClientId = new TextBox();
            txtOrderDate = new TextBox();
            rdoUseCurrentDate = new RadioButton();
            rdoEnterDate = new RadioButton();
            txtModelId = new TextBox();
            txtShoeSize = new TextBox();
            txtQuantity = new TextBox();
            btnSubmit = new Button();
            label1 = new Label();
            SuspendLayout();
            // 
            // txtClientId
            // 
            txtClientId.Location = new Point(63, 96);
            txtClientId.Name = "txtClientId";
            txtClientId.PlaceholderText = "Client ID";
            txtClientId.Size = new Size(225, 27);
            txtClientId.TabIndex = 0;
            // 
            // txtOrderDate
            // 
            txtOrderDate.Location = new Point(63, 186);
            txtOrderDate.Name = "txtOrderDate";
            txtOrderDate.PlaceholderText = "yyyy-MM-dd HH:mm";
            txtOrderDate.Size = new Size(225, 27);
            txtOrderDate.TabIndex = 3;
            // 
            // rdoUseCurrentDate
            // 
            rdoUseCurrentDate.BackColor = Color.Transparent;
            rdoUseCurrentDate.Location = new Point(63, 126);
            rdoUseCurrentDate.Name = "rdoUseCurrentDate";
            rdoUseCurrentDate.Size = new Size(225, 27);
            rdoUseCurrentDate.TabIndex = 1;
            rdoUseCurrentDate.Text = "Use current date and time";
            rdoUseCurrentDate.UseVisualStyleBackColor = false;
            rdoUseCurrentDate.CheckedChanged += RadioButton_CheckedChanged;
            // 
            // rdoEnterDate
            // 
            rdoEnterDate.BackColor = Color.Transparent;
            rdoEnterDate.Location = new Point(63, 156);
            rdoEnterDate.Name = "rdoEnterDate";
            rdoEnterDate.Size = new Size(225, 27);
            rdoEnterDate.TabIndex = 2;
            rdoEnterDate.Text = "Enter desired date and time";
            rdoEnterDate.UseVisualStyleBackColor = false;
            rdoEnterDate.CheckedChanged += RadioButton_CheckedChanged;
            // 
            // txtModelId
            // 
            txtModelId.Location = new Point(63, 216);
            txtModelId.Name = "txtModelId";
            txtModelId.PlaceholderText = "Model ID";
            txtModelId.Size = new Size(225, 27);
            txtModelId.TabIndex = 4;
            // 
            // txtShoeSize
            // 
            txtShoeSize.Location = new Point(63, 246);
            txtShoeSize.Name = "txtShoeSize";
            txtShoeSize.PlaceholderText = "Shoe Size";
            txtShoeSize.Size = new Size(225, 27);
            txtShoeSize.TabIndex = 5;
            // 
            // txtQuantity
            // 
            txtQuantity.Location = new Point(63, 276);
            txtQuantity.Name = "txtQuantity";
            txtQuantity.PlaceholderText = "Quantity";
            txtQuantity.Size = new Size(225, 27);
            txtQuantity.TabIndex = 6;
            // 
            // btnSubmit
            // 
            btnSubmit.BackColor = Color.DodgerBlue;
            btnSubmit.Location = new Point(63, 306);
            btnSubmit.Name = "btnSubmit";
            btnSubmit.Size = new Size(225, 37);
            btnSubmit.TabIndex = 7;
            btnSubmit.Text = "Submit";
            btnSubmit.UseVisualStyleBackColor = false;
            btnSubmit.Click += btnSubmit_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Arial", 18F, FontStyle.Bold, GraphicsUnit.Point, 204);
            label1.Location = new Point(44, 33);
            label1.Name = "label1";
            label1.Size = new Size(279, 35);
            label1.TabIndex = 8;
            label1.Text = "Create a Purchase";
            // 
            // CreatePurchaseForm
            // 
            BackgroundImage = Properties.Resources.bright_background_image;
            ClientSize = new Size(363, 401);
            Controls.Add(label1);
            Controls.Add(txtClientId);
            Controls.Add(txtOrderDate);
            Controls.Add(rdoUseCurrentDate);
            Controls.Add(rdoEnterDate);
            Controls.Add(txtModelId);
            Controls.Add(txtShoeSize);
            Controls.Add(txtQuantity);
            Controls.Add(btnSubmit);
            Name = "CreatePurchaseForm";
            Text = "Create Purchase";
            Load += CreatePurchaseForm_Load;
            ResumeLayout(false);
            PerformLayout();
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

        private void CreatePurchaseForm_Load(object sender, EventArgs e)
        {

        }
    }
}

