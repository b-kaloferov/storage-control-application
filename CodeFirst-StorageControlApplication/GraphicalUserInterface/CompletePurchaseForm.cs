using ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalUserInterface
{
    public partial class CompletePurchaseForm : Form
    {
        private IOrderService _orderService;
        private IShoeService _shoeService;

        public CompletePurchaseForm(IOrderService orderService, IShoeService shoeService)
        {
            InitializeComponent();
            _orderService = orderService;
            _shoeService = shoeService;
        }

        private async void btnSubmit_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtOrderId.Text, out int orderId))
            {
                MessageBox.Show("Invalid Order ID.");
                return;
            }

            var order = await _orderService.GetPurchaseByIdAsync(orderId, true);
            if (order == null)
            {
                MessageBox.Show($"Order with ID {orderId} not found.");
                return;
            }

            foreach (var detail in order.OrderDetails)
            {
                var shoe = await _shoeService.GetShoeByIdAsync(detail.ShoeId);
                detail.Shoe = shoe;

                if (shoe.Quantity < detail.Quantity)
                {
                    MessageBox.Show($"Not enough quantity for shoe ID {shoe.Id} with size {shoe.Size}. Available: {shoe.Quantity}, Requested: {detail.Quantity}");
                    return;
                }

                shoe.Quantity -= detail.Quantity;
                await _shoeService.UpdateShoeAsync(shoe);
            }

            MessageBox.Show("Purchase completed successfully.");
        }

        private void InitializeComponent()
        {
            this.txtOrderId = new System.Windows.Forms.TextBox();
            this.btnSubmit = new System.Windows.Forms.Button();

            // 
            // txtOrderId
            // 
            this.txtOrderId.Location = new System.Drawing.Point(30, 30);
            this.txtOrderId.Name = "txtOrderId";
            this.txtOrderId.Size = new System.Drawing.Size(200, 20);
            this.txtOrderId.TabIndex = 0;
            this.txtOrderId.PlaceholderText = "Order ID";

            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(30, 60);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(200, 30);
            this.btnSubmit.TabIndex = 1;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);

            // 
            // CompletePurchaseForm
            // 
            this.ClientSize = new System.Drawing.Size(284, 121);
            this.Controls.Add(this.txtOrderId);
            this.Controls.Add(this.btnSubmit);
            this.Name = "CompletePurchaseForm";
            this.Text = "Complete Purchase";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private TextBox txtOrderId;
        private Button btnSubmit;
    }
}
