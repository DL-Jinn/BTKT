using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DE01
{
    public partial class Form1 : Form
    {
        private bool isDeleting = false;
        private bool isDataEntered = false;

        public Form1()
        {
            InitializeComponent();
            this.Load += new EventHandler(Form1_Load);
            this.button2.Click += new EventHandler(button2_Click);
            this.button5.Click += new EventHandler(button5_Click);
            this.button6.Click += new EventHandler(button6_Click);
            this.button7.Click += new EventHandler(button7_Click);

        
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.TextChanged += TextBox_TextChanged;
            textBox2.TextChanged += TextBox_TextChanged;
            comboBox1.SelectedIndexChanged += ComboBox_SelectedIndexChanged;

            comboBox1.Items.Add("G1");
            comboBox1.Items.Add("G2");
            comboBox1.Items.Add("G3");
            comboBox1.Items.Add("G4");

            comboBox1.SelectedIndex = 0;


            button4.Enabled = false;
            button5.Enabled = false;
        }

        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            CheckDataEntered();
        }

        private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckDataEntered();
        }

        private void CheckDataEntered()
        {

            isDataEntered = !string.IsNullOrWhiteSpace(textBox1.Text) ||
                            !string.IsNullOrWhiteSpace(textBox2.Text) ||
                            !string.IsNullOrWhiteSpace(textBox3.Text) ||
                            comboBox1.SelectedIndex != -1;


            button4.Enabled = isDataEntered;
            button5.Enabled = isDataEntered;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string inputData1 = textBox1.Text;
            string inputData2 = textBox2.Text;
            string selectedDate = dateTimePicker1.Value.ToString("dd/MM/yyyy");
            string selectedOption = comboBox1.SelectedItem?.ToString();

            if (!string.IsNullOrWhiteSpace(inputData1) &&
                !string.IsNullOrWhiteSpace(inputData2) &&
                comboBox1.SelectedIndex != -1)
            {
                dataGridView1.Rows.Add(inputData1, inputData2, selectedDate, selectedOption);

                textBox1.Clear();
                textBox2.Clear();
                comboBox1.SelectedIndex = 0;
            }
            else
            {
                MessageBox.Show("Vui lòng nhập đầy đủ dữ liệu và lựa chọn!");
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {

                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa dòng đã chọn?",
                                                      "Xác nhận xóa",
                                                      MessageBoxButtons.YesNo,
                                                      MessageBoxIcon.Warning);


                if (result == DialogResult.Yes)
                {

                    foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                    {

                        if (!row.IsNewRow)
                        {
                            dataGridView1.Rows.Remove(row);
                        }
                    }
                    MessageBox.Show("Dòng đã được xóa thành công!");
                }
            }
            else
            {

                MessageBox.Show("Vui lòng chọn dòng để xóa!");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {

                string updatedData1 = textBox1.Text;
                string updatedData2 = textBox2.Text;
                string updatedDate = dateTimePicker1.Value.ToString("dd/MM/yyyy");
                string updatedOption = comboBox1.SelectedItem?.ToString();


                if (!string.IsNullOrWhiteSpace(updatedData1) &&
                    !string.IsNullOrWhiteSpace(updatedData2) &&
                    !string.IsNullOrWhiteSpace(updatedOption))
                {

                    dataGridView1.SelectedRows[0].Cells[0].Value = updatedData1;
                    dataGridView1.SelectedRows[0].Cells[1].Value = updatedData2;
                    dataGridView1.SelectedRows[0].Cells[2].Value = updatedDate;
                    dataGridView1.SelectedRows[0].Cells[3].Value = updatedOption;


                    textBox1.Clear();
                    textBox2.Clear();
                    comboBox1.SelectedIndex = -1;
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ dữ liệu để sửa!");
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn dòng để sửa!");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text File|*.txt";
            saveFileDialog.Title = "Lưu danh sách";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter writer = new StreamWriter(saveFileDialog.FileName))
                {

                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (!row.IsNewRow)
                        {
                            string rowData = string.Format("{0}\t{1}\t{2}\t{3}",
                                row.Cells[0].Value?.ToString(),
                                row.Cells[1].Value?.ToString(),
                                row.Cells[2].Value?.ToString(),
                                row.Cells[3].Value?.ToString());
                            writer.WriteLine(rowData);
                        }
                    }
                }

                MessageBox.Show("Dữ liệu đã được lưu thành công!");
            }
        }


        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {

                textBox1.Text = dataGridView1.SelectedRows[0].Cells[0].Value?.ToString();
                textBox2.Text = dataGridView1.SelectedRows[0].Cells[1].Value?.ToString();
                dateTimePicker1.Value = DateTime.Parse(dataGridView1.SelectedRows[0].Cells[2].Value?.ToString());
                comboBox1.SelectedItem = dataGridView1.SelectedRows[0].Cells[3].Value?.ToString();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            {

                DialogResult result = MessageBox.Show("Bạn có chắc chắn không muốn lưu dữ liệu?",
                                                      "Xác nhận không lưu",
                                                      MessageBoxButtons.YesNo,
                                                      MessageBoxIcon.Question);


                if (result == DialogResult.Yes)
                {

                    this.Close();
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát?",
                                          "Xác nhận thoát",
                                          MessageBoxButtons.YesNo,
                                          MessageBoxIcon.Question);


            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            {
                string searchTerm = textBox3.Text;

                if (string.IsNullOrWhiteSpace(searchTerm))
                {
                    MessageBox.Show("Vui lòng nhập từ khóa tìm kiếm!");
                    return;
                }


                var foundRows = new List<DataGridViewRow>();


                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (!row.IsNewRow)
                    {

                        for (int i = 0; i < row.Cells.Count; i++)
                        {
                            string cellValue = row.Cells[i].Value?.ToString();


                            if (!string.IsNullOrEmpty(cellValue) && cellValue.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0)
                            {
                                foundRows.Add(row);
                                break;
                            }
                        }
                    }
                }


                if (foundRows.Count > 0)
                {

                    dataGridView1.ClearSelection();
                    foreach (var foundRow in foundRows)
                    {
                        foundRow.Selected = true;
                    }
                    MessageBox.Show($"{foundRows.Count} dòng được tìm thấy!");
                }
                else
                {
                    MessageBox.Show("Không tìm thấy dòng nào!");
                }
            }
        }


    }

}