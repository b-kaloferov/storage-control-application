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
            this.btnAddShoes = new System.Windows.Forms.Button();
            this.btnDiscardShoes = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // 
            // btnAddShoes
            // 
            this.btnAddShoes.Location = new System.Drawing.Point(50, 50);
            this.btnAddShoes.Name = "btnAddShoes";
            this.btnAddShoes.Size = new System.Drawing.Size(150, 50);
            this.btnAddShoes.TabIndex = 0;
            this.btnAddShoes.Text = "Add Shoes";
            this.btnAddShoes.UseVisualStyleBackColor = true;
            this.btnAddShoes.Click += new System.EventHandler(this.btnAddShoes_Click);

            // 
            // btnDiscardShoes
            // 
            this.btnDiscardShoes.Location = new System.Drawing.Point(50, 150);
            this.btnDiscardShoes.Name = "btnDiscardShoes";
            this.btnDiscardShoes.Size = new System.Drawing.Size(150, 50);
            this.btnDiscardShoes.TabIndex = 1;
            this.btnDiscardShoes.Text = "Discard Shoes";
            this.btnDiscardShoes.UseVisualStyleBackColor = true;
            this.btnDiscardShoes.Click += new System.EventHandler(this.btnDiscardShoes_Click);

            // 
            // AddOrDiscardShoesForm
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.btnAddShoes);
            this.Controls.Add(this.btnDiscardShoes);
            this.Name = "AddOrDiscardShoesForm";
            this.Text = "Add or Discard Shoes";
            this.ResumeLayout(false);
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
