using System.Diagnostics;
using System.Text;

namespace WinFormsApp2
{
    public partial class Form1 : Form
    {
        private int rowCount;
        private int columnCount;
        private bool is_aut = false;
        public Form1()
        {
            InitializeComponent();
            Opacity = 0;
            ShowInTaskbar = false;
        }

        public void UserAuthed()
        {
            Opacity = 100;
            ShowInTaskbar = true;
            is_aut = true;
            this.Activate();
        }

        public void btnCreateMatrix_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtRowCount.Text, out rowCount) && int.TryParse(txtColumnCount.Text, out columnCount))
            {
                if (rowCount > 0 && columnCount > 0)
                {
                    // �������� �������
                    InitializeDataGridView(rowCount, columnCount);
                }
                else
                {
                    MessageBox.Show("���������� ����� � �������� ������ ���� ������ ����.", "������");
                }
            }
            else
            {
                MessageBox.Show("������� ���������� ���������� ����� � �������� (����� �����).", "������");
            }
        }

        public void InitializeDataGridView(int rows, int columns)
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();

            for (int i = 0; i < columns; i++)
            {
                dataGridView1.Columns.Add("", "");
            }

            for (int i = 0; i < rows; i++)
            {
                dataGridView1.Rows.Add();
            }
        }

        public void btnFindOptimalRoute_Click(object sender, EventArgs e)
        {
            if (rowCount > 0 && columnCount > 0)
            {
                int[,] graph = new int[rowCount, columnCount];

                // ���������� ����� ���� �� DataGridView
                for (int i = 0; i < rowCount; i++)
                {
                    for (int j = 0; j < columnCount; j++)
                    {
                        graph[i, j] = Convert.ToInt32(dataGridView1.Rows[i].Cells[j].Value);
                    }
                }

                int[] optimalPath = FindOptimalRoute(graph, rowCount);

                // ����� ������ ��������� �������
                int startIndex = Array.IndexOf(optimalPath, 0);

                // ������������ ������ ����
                StringBuilder pathBuilder = new StringBuilder();
                for (int i = 0; i < rowCount; i++)
                {
                    int currentVertex = (optimalPath[i] + startIndex) % rowCount;
                    pathBuilder.Append(currentVertex + 1); // ��������� 1 ��� �������� � ������ ��������� �������
                    if (i < rowCount - 1)
                        pathBuilder.Append(" -> ");
                }

                string result = "����������� �������: " + pathBuilder.ToString() + " -> " + (startIndex + 1); // �������� ����
                MessageBox.Show(result, "Optimal Route");
            }
            else
            {
                MessageBox.Show("������� �������� �������.", "������");
            }
        }

        public int[] FindOptimalRoute(int[,] graph, int numberOfVertices)
        {
            int[] cities = Enumerable.Range(0, numberOfVertices).ToArray();
            int[] optimalPath = null;
            int minCost = int.MaxValue;

            // ��������� ���� ��������� ������������ �������
            var permutations = Permutations(cities);

            foreach (var permutation in permutations)
            {
                int currentCost = CalculatePathCost(permutation, graph);

                if (currentCost < minCost)
                {
                    minCost = currentCost;
                    optimalPath = permutation.ToArray();
                }
            }

            // ����� ������ ��������� �������
            int startIndex = optimalPath.ToList().IndexOf(0);
            // ������������� ������� � ������, ������� � ���������� �������
            optimalPath = optimalPath.Select(index => (index + startIndex) % numberOfVertices).ToArray();

            return optimalPath;
        }

        public int CalculatePathCost(IEnumerable<int> path, int[,] graph)
        {
            int cost = 0;
            int prevCity = path.First();

            foreach (int city in path.Skip(1))
            {
                cost += graph[prevCity, city];
                prevCity = city;
            }

            cost += graph[prevCity, path.First()]; // �������� ����

            return cost;
        }

        public IEnumerable<IEnumerable<T>> Permutations<T>(IEnumerable<T> items)
        {
            if (items.Count() == 1)
                return new List<IEnumerable<T>> { items };

            return items.SelectMany(item =>
                Permutations(items.Where(x => !x.Equals(item))),
                (item, permutation) => new List<T> { item }.Concat(permutation));
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // ������� ����� ����� ����������
            Instruction instructionForm = new Instruction();

            // ��������� ����� ���������� ��� ��������� ����
            instructionForm.ShowDialog();
        }
        
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (is_aut)
            {
                DialogResult result = MessageBox.Show("�������, ��� ������ �������?", "�� �������?", MessageBoxButtons.YesNo);

                bool needClose = result == DialogResult.Yes;
                {
                    if (!needClose)
                    {
                        e.Cancel = true;
                    }
                }
            }
        }

        
    }
}
