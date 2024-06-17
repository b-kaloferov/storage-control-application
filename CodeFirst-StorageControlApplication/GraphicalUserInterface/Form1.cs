using ServiceLayer;

namespace GraphicalUserInterface
{
    public partial class Form1 : Form
    {

        private readonly IModelService _modelService;

        public Form1(IModelService modelService)
        {
            _modelService = modelService;
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

        }
        private void button4_Click(object sender, EventArgs e)
        {

        }
        private void button5_Click(object sender, EventArgs e)
        {

        }
        private void button6_Click(object sender, EventArgs e)
        {

        }
        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {

        }



    }
}
