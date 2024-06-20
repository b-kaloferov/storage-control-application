using ServiceLayer;

namespace GraphicalUserInterface
{
    public partial class Form1 : Form
    {

        private readonly IModelService _modelService;
        private readonly IClientService _clientService;
        private readonly IShoeService _shoeService;
        private readonly IOrderService _orderService;

        public Form1(IModelService modelService, IClientService clientService, IShoeService shoeService, IOrderService orderService)
        {
            _modelService = modelService;
            _clientService = clientService;
            _shoeService = shoeService;
            _orderService = orderService;
            InitializeComponent();
        }
        

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddShoeModelForm addShoeModelForm = new AddShoeModelForm(_modelService);
            addShoeModelForm.ShowDialog();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            ViewShoeModelsForm viewShoeModelsForm = new ViewShoeModelsForm(_modelService);
            viewShoeModelsForm.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            UpdateShoeModelForm updateShoeModelForm = new UpdateShoeModelForm(_modelService);
            updateShoeModelForm.ShowDialog();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            RemoveShoeModelForm removeShoeModelsForm = new RemoveShoeModelForm(_modelService);
            removeShoeModelsForm.ShowDialog();
        }
        private void button5_Click(object sender, EventArgs e)
        {
            AddOrDiscardShoesForm addOrDiscardShoesForm = new AddOrDiscardShoesForm(_modelService, _shoeService);
            addOrDiscardShoesForm.ShowDialog();
        }
        private void button6_Click(object sender, EventArgs e)
        {
            ViewShoesOfModelForm viewShoesOfModelForm = new ViewShoesOfModelForm(_modelService);
            viewShoesOfModelForm.ShowDialog();
        }
        private void button7_Click(object sender, EventArgs e)
        {
            ManageCustomersForm manageCustomersForm = new ManageCustomersForm(_clientService);
            manageCustomersForm.ShowDialog();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            ManagePurchaseForm managePurchaseForm = new ManagePurchaseForm(_clientService, _modelService, _orderService, _shoeService);
            managePurchaseForm.ShowDialog();
        }

        private void button9_Click(object sender, EventArgs e)
        {

        }



    }
}
