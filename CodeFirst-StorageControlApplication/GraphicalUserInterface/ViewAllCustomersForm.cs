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
        private IClientService _clientService;
        private Label label1;
        private DataGridView dataGridView;

        public ViewAllCustomersForm(IClientService clientService)
        {
            InitializeComponent();
            _clientService = clientService;
            LoadClientsAsync();
        }

        private void InitializeComponent()
        {
            dataGridView = new DataGridView();
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)dataGridView).BeginInit();
            SuspendLayout();
            // 
            // dataGridView
            // 
            dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView.Location = new Point(87, 196);
            dataGridView.Name = "dataGridView";
            dataGridView.RowHeadersWidth = 51;
            dataGridView.RowTemplate.Height = 24;
            dataGridView.Size = new Size(469, 208);
            dataGridView.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Arial", 18F, FontStyle.Bold, GraphicsUnit.Point, 204);
            label1.Location = new Point(174, 65);
            label1.Name = "label1";
            label1.Size = new Size(293, 35);
            label1.TabIndex = 1;
            label1.Text = "View All Customers";
            label1.Click += label1_Click;
            // 
            // ViewAllCustomersForm
            // 
            BackgroundImage = Properties.Resources.bright_background_image;
            ClientSize = new Size(640, 461);
            Controls.Add(label1);
            Controls.Add(dataGridView);
            Name = "ViewAllCustomersForm";
            Text = "View All Customers";
            Load += ViewAllCustomersForm_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private async Task LoadClientsAsync()
        {
            var clients = await _clientService.GetAllClientsAsync();

            if (clients.Count > 0)
            {
                var bindingList = new BindingSource(clients, null);
                dataGridView.DataSource = bindingList;
            }
            else
            {
                MessageBox.Show("No customers found.");
            }
        }

        private void ViewAllCustomersForm_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
