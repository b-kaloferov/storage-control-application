namespace GraphicalUserInterface
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            button1 = new Button();
            label1 = new Label();
            button2 = new Button();
            button3 = new Button();
            button4 = new Button();
            button5 = new Button();
            button6 = new Button();
            button7 = new Button();
            button8 = new Button();
            button9 = new Button();
            SuspendLayout();
            // 
            // button1
            // 
            button1.BackColor = Color.DodgerBlue;
            button1.Font = new Font("Arial", 9F);
            button1.Location = new Point(122, 130);
            button1.Name = "button1";
            button1.Size = new Size(274, 62);
            button1.TabIndex = 0;
            button1.Text = "Add New Shoe Model";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Arial", 36F, FontStyle.Bold, GraphicsUnit.Point, 204);
            label1.Location = new Point(122, 9);
            label1.Name = "label1";
            label1.Size = new Size(825, 70);
            label1.TabIndex = 1;
            label1.Text = "Storage Control Application";
            label1.Click += label1_Click;
            // 
            // button2
            // 
            button2.BackColor = Color.DodgerBlue;
            button2.Font = new Font("Arial", 9F);
            button2.Location = new Point(670, 130);
            button2.Name = "button2";
            button2.Size = new Size(274, 62);
            button2.TabIndex = 2;
            button2.Text = "View Available Shoe Models";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.BackColor = Color.DodgerBlue;
            button3.Font = new Font("Arial", 9F);
            button3.Location = new Point(122, 200);
            button3.Name = "button3";
            button3.Size = new Size(274, 62);
            button3.TabIndex = 3;
            button3.Text = "Update a Shoe Model";
            button3.UseVisualStyleBackColor = false;
            button3.Click += button3_Click;
            // 
            // button4
            // 
            button4.BackColor = Color.DodgerBlue;
            button4.Font = new Font("Arial", 9F);
            button4.Location = new Point(670, 200);
            button4.Name = "button4";
            button4.Size = new Size(274, 62);
            button4.TabIndex = 4;
            button4.Text = "Remove a Shoe Model";
            button4.UseVisualStyleBackColor = false;
            button4.Click += button4_Click;
            // 
            // button5
            // 
            button5.BackColor = Color.DodgerBlue;
            button5.Font = new Font("Arial", 9F);
            button5.Location = new Point(122, 270);
            button5.Name = "button5";
            button5.Size = new Size(274, 62);
            button5.TabIndex = 5;
            button5.Text = "Add/Discard Shoes";
            button5.UseVisualStyleBackColor = false;
            button5.Click += button5_Click;
            // 
            // button6
            // 
            button6.BackColor = Color.DodgerBlue;
            button6.Font = new Font("Arial", 9F);
            button6.Location = new Point(670, 270);
            button6.Name = "button6";
            button6.Size = new Size(274, 62);
            button6.TabIndex = 6;
            button6.Text = "View Shoes of a Particular Model";
            button6.UseVisualStyleBackColor = false;
            button6.Click += button6_Click;
            // 
            // button7
            // 
            button7.BackColor = Color.DodgerBlue;
            button7.Font = new Font("Arial", 9F);
            button7.Location = new Point(122, 340);
            button7.Name = "button7";
            button7.Size = new Size(274, 62);
            button7.TabIndex = 7;
            button7.Text = "Manage Customers";
            button7.UseVisualStyleBackColor = false;
            button7.Click += button7_Click;
            // 
            // button8
            // 
            button8.BackColor = Color.DodgerBlue;
            button8.Font = new Font("Arial", 9F);
            button8.Location = new Point(670, 340);
            button8.Name = "button8";
            button8.Size = new Size(274, 62);
            button8.TabIndex = 8;
            button8.Text = "Manage a Purchase";
            button8.UseVisualStyleBackColor = false;
            button8.Click += button8_Click;
            // 
            // button9
            // 
            button9.BackColor = Color.DodgerBlue;
            button9.Font = new Font("Arial", 9F);
            button9.Location = new Point(395, 410);
            button9.Name = "button9";
            button9.Size = new Size(274, 62);
            button9.TabIndex = 9;
            button9.Text = "View Purchase History";
            button9.UseVisualStyleBackColor = false;
            button9.Click += button9_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1046, 522);
            Controls.Add(button9);
            Controls.Add(button8);
            Controls.Add(button7);
            Controls.Add(button6);
            Controls.Add(button5);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(label1);
            Controls.Add(button1);
            DoubleBuffered = true;
            MaximizeBox = false;
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private Label label1;
        private Button button2;
        private Button button3;
        private Button button4;
        private Button button5;
        private Button button6;
        private Button button7;
        private Button button8;
        private Button button9;
    }
}
