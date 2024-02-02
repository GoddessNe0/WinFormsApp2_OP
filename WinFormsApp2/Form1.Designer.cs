namespace WinFormsApp2
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
            btnFindRoute = new Button();
            txtRowCount = new TextBox();
            dataGridView1 = new DataGridView();
            txtColumnCount = new TextBox();
            btnCreateMatrix = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            linkLabel1 = new LinkLabel();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // btnFindRoute
            // 
            btnFindRoute.Location = new Point(286, 72);
            btnFindRoute.Name = "btnFindRoute";
            btnFindRoute.Size = new Size(142, 23);
            btnFindRoute.TabIndex = 0;
            btnFindRoute.Text = "Применить алгоритм";
            btnFindRoute.UseVisualStyleBackColor = true;
            btnFindRoute.Click += btnFindOptimalRoute_Click;
            // 
            // txtRowCount
            // 
            txtRowCount.Location = new Point(150, 31);
            txtRowCount.Name = "txtRowCount";
            txtRowCount.Size = new Size(121, 23);
            txtRowCount.TabIndex = 1;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(25, 132);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(429, 296);
            dataGridView1.TabIndex = 2;
            // 
            // txtColumnCount
            // 
            txtColumnCount.Location = new Point(150, 73);
            txtColumnCount.Name = "txtColumnCount";
            txtColumnCount.Size = new Size(121, 23);
            txtColumnCount.TabIndex = 3;
            // 
            // btnCreateMatrix
            // 
            btnCreateMatrix.Location = new Point(286, 31);
            btnCreateMatrix.Name = "btnCreateMatrix";
            btnCreateMatrix.Size = new Size(142, 23);
            btnCreateMatrix.TabIndex = 4;
            btnCreateMatrix.Text = "Создать матрицу";
            btnCreateMatrix.UseVisualStyleBackColor = true;
            btnCreateMatrix.Click += btnCreateMatrix_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(124, 114);
            label1.Name = "label1";
            label1.Size = new Size(233, 15);
            label1.TabIndex = 5;
            label1.Text = "Здесь будет отображаться ваша матрица";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(64, 34);
            label2.Name = "label2";
            label2.Size = new Size(80, 15);
            label2.TabIndex = 6;
            label2.Text = "Кол-во строк";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(43, 76);
            label3.Name = "label3";
            label3.Size = new Size(101, 15);
            label3.TabIndex = 7;
            label3.Text = "Кол-во столбцов";
            // 
            // linkLabel1
            // 
            linkLabel1.AutoSize = true;
            linkLabel1.Location = new Point(136, 450);
            linkLabel1.Name = "linkLabel1";
            linkLabel1.Size = new Size(190, 15);
            linkLabel1.TabIndex = 8;
            linkLabel1.TabStop = true;
            linkLabel1.Text = "Как пользоваться приложением?";
            linkLabel1.LinkClicked += linkLabel1_LinkClicked;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(486, 487);
            Controls.Add(linkLabel1);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnCreateMatrix);
            Controls.Add(btnFindRoute);
            Controls.Add(txtColumnCount);
            Controls.Add(dataGridView1);
            Controls.Add(txtRowCount);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MinimizeBox = false;
            Name = "Form1";
            Text = "Program";
            FormClosing += Form1_FormClosing;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnFindRoute;
        private TextBox txtRowCount;
        private DataGridView dataGridView1;
        private TextBox txtColumnCount;
        private Button btnCreateMatrix;
        private Label label1;
        private Label label2;
        private Label label3;
        private LinkLabel linkLabel1;
    }
}
