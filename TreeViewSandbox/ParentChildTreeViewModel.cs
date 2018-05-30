using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using TreeViewSandbox.Data;
using TreeViewSandbox.Dtos;
using TreeViewSandbox.Entities;

namespace TreeViewSandbox
{
    public class ParentChildTreeViewModel
    {
        private const string DevDb = "Server=PRSQ02;Database=DEV_pureData;Trusted_Connection=True;";
        private List<TaskConfigGroup> _allGroups;
        private List<TaskConfigSource> _allSources;

        public List<TreeParent> PopulateTree()
        {
            using (var ctx = new PureDataContext(DevDb))
            {
                _allGroups = ctx.Set<TaskConfigGroup>()
                    .Include(x => x.CaseState)
                    .Include(x => x.TaskConfigSources)
                    .ToList();

                _allSources = ctx.Set<TaskConfigSource>()
                    .Include(x => x.TaskConfigGroup)
                    .Include(x => x.TaskConfiguration)
                    .ToList();
            }

            var rootParentConfigGroups = _allGroups.Where(x => x.ParentTaskConfigId == null).ToList();
            var rootParents = new List<TreeParent>();

            foreach (var rootParent in rootParentConfigGroups)
            {
                var caseState = new TreeParent
                {
                    CaseState = rootParent.CaseState,
                    Children = new List<TreeSiblings>()
                };


                foreach (var source in rootParent.TaskConfigSources.ToList())
                {
                    var lvl1Children = new TreeSiblings
                    {
                        Depth = rootParent.Depth,
                        CaseParent = caseState,
                        Node = source,
                        Children = new List<TreeSiblings>()
                    };

                    caseState.Children.Add(lvl1Children);
                }

                GenerateNodeChildren(caseState.Children);
                rootParents.Add(caseState);
            }

            return rootParents;
        }

        private void GenerateNodeChildren(IEnumerable<TreeSiblings> siblings)
        {
            var immediateChildIds = new List<int>();

            foreach (var taskParent in siblings)
            {
                immediateChildIds.AddRange(GetAllChildren(taskParent.Node.TaskConfigurationId));

                if (immediateChildIds.Any())
                {
                    var children = _allSources.Where(x => x.TaskConfigGroupId == immediateChildIds.First()).ToList();
                    foreach (var child in children)
                    {
                        var lvl2Children = new TreeSiblings
                        {
                            Depth = taskParent.Depth + 1,
                            TaskParent = taskParent,
                            Children = new List<TreeSiblings>(),
                            Node = child
                        };

                        taskParent.Children.Add(lvl2Children);
                    }
                }

                immediateChildIds.Clear();
                GenerateNodeChildren(taskParent.Children);
            }
        }

        private IEnumerable<int> GetAllChildren(int parentTaskId)
        {
            foreach (var child in _allGroups.Where(x => x.ParentTaskConfigId == parentTaskId))
            {
                Debug.WriteLine($"{child.ParentTaskConfigId} // {child.TaskConfigGroupId}");
            }

            return _allGroups.Where(x => x.ParentTaskConfigId == parentTaskId)
                .Select(x => x.TaskConfigGroupId)
                .Distinct()
                .ToList();
        }

        private readonly List<int> _taskConfigIdList = new List<int>();

        //Todo:
        //Not sure if this works as it pulls through the parent item too even if it isn't selected for removal.
        //Try doing it in the tests first.
        public void DeleteTreeNode(int nodeId, List<TreeSiblings> children)
        {
            using (var ctx = new PureDataContext(DevDb))
            {
                _taskConfigIdList.Clear();
                _taskConfigIdList.Add(nodeId);
                GetAllChildIds(children);

                var groupRowsToDelete = ctx.Set<TaskConfigGroup>()
                    .Where(x => _taskConfigIdList.Contains(x.ParentTaskConfigId.Value)).ToList();

                var sourceRowsToDelete = ctx.Set<TaskConfigSource>()
                    .Where(x => _taskConfigIdList.Contains(x.TaskConfigurationId)).ToList();

                if (!groupRowsToDelete.Any() || !sourceRowsToDelete.Any()) return;

                //ctx.Set<TaskConfigGroup>().RemoveRange(groupRowsToDelete);
                //ctx.Set<TaskConfigSource>().RemoveRange(sourceRowsToDelete);
                //ctx.SaveChanges();
            }
        }

        //Needs to check if there are any children not deleted???
        private void GetAllChildIds(IReadOnlyCollection<TreeSiblings> children)
        {
            if (!children.Any()) return;

            _taskConfigIdList.Add(children.First().Node.TaskConfigurationId);

            foreach (var child in children)
            {
                GetAllChildIds(child.Children);
            }
        }

        public CaseState GetRootParent(TreeSiblings sibling)
        {
            return sibling.TaskParent != null ? GetRootParent(sibling.TaskParent) : sibling.CaseParent.CaseState;
        }
    }
}