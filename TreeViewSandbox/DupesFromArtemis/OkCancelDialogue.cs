using System.Windows.Forms;

namespace TreeViewSandbox.DupesFromArtemis
{
    public partial class OkCancelDialogue : Form
    {
        public OkCancelDialogue()
        {
            InitializeComponent();
        }

        public OkCancelDialogue(string title)
        {
            InitializeComponent();
            Text = title;
        }

        public void DockControl(Control control)
        {
            control.Dock = DockStyle.Fill;
            MainContainer.Controls.Clear();
            MainContainer.Controls.Add(control);
        }
    }
}
