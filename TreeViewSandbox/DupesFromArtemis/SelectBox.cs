using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Functional.Option;

namespace TreeViewSandbox.DupesFromArtemis
{
    public partial class SelectBox<T> : Form
    {
        private readonly List<T> _items;
        public SelectBox(string title, string prompt, List<T> items)
        {
            InitializeComponent();

            _items = items;
            Text = title;
            ThePrompt.Text = prompt;
            Chosen = Option<T>.None;
        }

        public Option<T> Chosen { get; private set; }

        private void SelectBox_Load(object sender, EventArgs e)
        {
            foreach (var item in _items)
            {
                TheComboBox.Items.Add(item.ToString());
            }
        }

        private void TheComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Chosen = _items[TheComboBox.SelectedIndex];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Chosen = Option<T>.None;
            Close();
        }

        private void SelectBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                DialogResult = DialogResult.Cancel;
                Chosen = Option<T>.None;
                Close();
            }
        }
    }
}
