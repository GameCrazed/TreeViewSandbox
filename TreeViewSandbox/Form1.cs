using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using TreeViewSandbox.Data;
using TreeViewSandbox.Dtos;
using TreeViewSandbox.DupesFromArtemis;
using TreeViewSandbox.Entities;

namespace TreeViewSandbox
{
    public partial class Form1 : Form
    {
        private readonly ParentChildTreeViewModel _viewModel = new ParentChildTreeViewModel();

        public Form1()
        {
            InitializeComponent();
            AddToTreeView(_viewModel.PopulateTree());
        }

        private void RefreshButton_Click(object sender, EventArgs e)
        {
            treeView1.Nodes.Clear();
            AddToTreeView(_viewModel.PopulateTree());
        }

        private void AddToTreeView(IEnumerable<TreeParent> rootParents)
        {
            treeView1.Nodes.Clear();
            treeView1.Dock = DockStyle.Fill;

            foreach (var parentRoot in rootParents)
            {
                var nodeRoot =
                    new TreeNode($"{parentRoot.CaseState.CaseStateId} : {parentRoot.CaseState.Name}")
                    {
                        Tag = parentRoot
                    };
                treeView1.Nodes.Add(nodeRoot);

                IterateThroughTreeObject(nodeRoot, parentRoot.Children);
            }
            treeView1.ExpandAll();
        }

        private static void IterateThroughTreeObject(TreeNode treeNode, IReadOnlyCollection<TreeSiblings> tree)
        {
            if (!tree.Any()) return;

            foreach (var child in tree)
            {
                var nodeChild =
                    new TreeNode($"{child.Node.TaskConfigurationId} : {child.Node.TaskConfiguration.Title}")
                    {
                        Tag = child
                    };
                treeNode.Nodes.Add(nodeChild);

                IterateThroughTreeObject(nodeChild, child.Children);
            }
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode.Tag is TreeParent parent)
            {
                CannotDeleteCaseState(parent.CaseState.Name);
                return;
            }
            else
            {
                DeleteSiblingNode((TreeSiblings)treeView1.SelectedNode.Tag);
            }

            RefreshButton_Click(sender, e);
        }

        private void DeleteSiblingNode(TreeSiblings sibling)
        {
            if (sibling.Children.Any())
            {
                if (ChildNodeWarning(sibling.Node.TaskConfiguration.Title) == DialogResult.Yes)
                {
                    _viewModel.DeleteTreeNode(sibling.Node.TaskConfigurationId, sibling.Children);
                }
            }
            else
            {
                _viewModel.DeleteTreeNode(sibling.Node.TaskConfigurationId, new List<TreeSiblings> { sibling });
            }
        }

        private static void CannotDeleteCaseState(string name)
        {
            MessageBox.Show(
                $"Error: Cannot delete a root Case State node. Failed to delete the root node '{name}' please contact a member of the development team to make changes to the Case State root flow.",
                "Cannot Delete Case State",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Error);
            return;
        }

        private static DialogResult ChildNodeWarning(string name)
        {
            return MessageBox.Show(
                $"The Node {name} has child nodes, if {name} is deleted so will the children. Are you sure you wish to continue?",
                "Child Nodes Found",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);
        }



        private const string DevDb = "Server=PRSQ02;Database=DEV_pureData;Trusted_Connection=True;";
        private readonly ITasksDirector _tasks = new TasksDirector(new TaskRepository(new PureConnectionString(DevDb)));

        //Partially ripped from 'Add manual task' method. If these are to be combined then the original method needs extracting 
        //from the ManualTask.Add method.
        private void AddButton_Click(object sender, EventArgs e)
        {
            var rootCaseState = new CaseState();
            var result = new Result();
            if (treeView1.SelectedNode.Tag is TreeParent parent)
            {
                rootCaseState = parent.CaseState;
                result = AddManualTask(rootCaseState.CaseStateId, 1, rootCaseState.CaseStateId) as Result;
            }
            else
            {
                rootCaseState = _viewModel.GetRootParent((TreeSiblings)treeView1.SelectedNode.Tag);
                var taskConfig = (TreeSiblings) treeView1.SelectedNode.Tag;
                result = AddManualTask(taskConfig.Node.TaskConfigurationId, taskConfig.Depth, rootCaseState.CaseStateId) as Result;
            }

            

            if (!result.Success)
            {
                MessageBox.Show("Add Manual Task Error","",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }

            RefreshButton_Click(sender, e);
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button.Equals(MouseButtons.Right))
            {
                treeView1.SelectedNode = treeView1.Nodes[e.Node.Index];
                contextMenuStrip1.Show(MousePosition);
            }
        }





        public IResult AddManualTask(int nodeId, int nodeDepth, int rootCaseStateId)
        {
            try
            {
                const string title = "Task Configuration";
                const string prompt = "Select a Task Configuration to include in the Case Filter.";

                var choices = _tasks.GetManualConfigurations(rootCaseStateId).OrderBy(x => x.Title).ToList();

                if (!choices.Any())
                {
                    var message =
                        $"You have no manual configuration tasks set-up for the CaseState: {_tasks.GetCaseStateName(rootCaseStateId)}";
                    return new ResultFactory().ErrorMessage(message);
                }

                TaskConfiguration chosenConfiguration;
                if (Coercion.TryCoerceChoice(title, prompt, choices, out chosenConfiguration))
                {

                    //Todo:take selected task config and add it to the selected node's children list. If it has no children add it as a row on the groups table before adding to sources.

                    using (var ctx = new PureDataContext(DevDb))
                    {
                        

                        var nodeConfigGroupId = ctx
                            .Set<TaskConfigGroup>().FirstOrDefault(x => x.ParentTaskConfigId == nodeId || x.ParentCaseStateId == nodeId); //This logic doesn't work. What is cs has same id as tc.

                        //If it doesn't exist within the groups table, create it.
                        if (nodeConfigGroupId == null)
                        {
                            var newConfigGroupRow = new TaskConfigGroup()
                            {
                                ParentTaskConfigId = nodeId,
                                Depth = nodeDepth + 1,
                            };

                            ctx.Set<TaskConfigGroup>().Add(newConfigGroupRow);
                            ctx.SaveChanges();

                            //Duplicate nodeConfigGroupId line here when fixed.
                            nodeConfigGroupId = ctx
                                .Set<TaskConfigGroup>().FirstOrDefault(x => x.ParentTaskConfigId == nodeId || x.ParentCaseStateId == nodeId);
                        }

                        var newTaskConfigSource = new TaskConfigSource()
                        {
                            TaskConfigGroupId = nodeConfigGroupId.TaskConfigGroupId,
                            TaskConfigurationId = chosenConfiguration.TaskConfigurationId
                        };

                        ctx.Set<TaskConfigSource>().Add(newTaskConfigSource);
                        ctx.SaveChanges();
                    }
                }
                return new ResultFactory().Ok();
            }
            catch (Exception ex)
            {
                return new ResultFactory().Error(ex);
            }
        }

        private bool DetermineDeferredStatusForManualTasks(DateTime dueDateValue)
        {
            return dueDateValue > DateTime.Now;
        }
    }
}
