using ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalUserInterface
{
    public partial class AddOrDiscardShoesForm : Form
    {
        private readonly IModelService _modelService;
        private readonly IShoeService _shoeService;

        public AddOrDiscardShoesForm(IModelService modelService, IShoeService shoeService)
        {
            _modelService = modelService;
            _shoeService = shoeService;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            btnAddShoes = new Button();
            btnDiscardShoes = new Button();
            SuspendLayout();
            // 
            // btnAddShoes
            // 
            btnAddShoes.BackColor = Color.DodgerBlue;
            btnAddShoes.Location = new Point(64, 51);
            btnAddShoes.Name = "btnAddShoes";
            btnAddShoes.Size = new Size(150, 50);
            btnAddShoes.TabIndex = 0;
            btnAddShoes.Text = "Add Shoes";
            btnAddShoes.UseVisualStyleBackColor = false;
            btnAddShoes.Click += btnAddShoes_Click;
            // 
            // btnDiscardShoes
            // 
            btnDiscardShoes.BackColor = Color.DodgerBlue;
            btnDiscardShoes.Location = new Point(64, 151);
            btnDiscardShoes.Name = "btnDiscardShoes";
            btnDiscardShoes.Size = new Size(150, 50);
            btnDiscardShoes.TabIndex = 1;
            btnDiscardShoes.Text = "Discard Shoes";
            btnDiscardShoes.UseVisualStyleBackColor = false;
            btnDiscardShoes.Click += btnDiscardShoes_Click;
            // 
            // AddOrDiscardShoesForm
            // 
            BackgroundImage = Properties.Resources.bright_background_image;
            ClientSize = new Size(284, 261);
            Controls.Add(btnAddShoes);
            Controls.Add(btnDiscardShoes);
            Name = "AddOrDiscardShoesForm";
            Text = "Add or Discard Shoes";
            ResumeLayout(false);
        }

        private void btnAddShoes_Click(object sender, EventArgs e)
        {
            var addShoesForm = new AddShoesForm(_modelService, _shoeService);
            addShoesForm.Show();
        }

        private void btnDiscardShoes_Click(object sender, EventArgs e)
        {
            var discardShoesForm = new DiscardShoesForm(_modelService, _shoeService);
            discardShoesForm.Show();
        }

        private Button btnAddShoes;
        private Button btnDiscardShoes;
    }
}
