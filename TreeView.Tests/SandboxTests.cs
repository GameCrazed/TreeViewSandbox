using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using NUnit.Framework;
using TreeViewSandbox.Data;
using TreeViewSandbox.Dtos;
using TreeViewSandbox.Entities;

namespace TreeView.Tests
{
    [TestFixture]
    public class SandboxTests
    {
        private List<TaskConfigGroup> _allGroups;
        private List<TaskConfigSource> _allSources;
        private const string DevDb = "Server=PRSQ02;Database=DEV_pureData;Trusted_Connection=True;";

        [OneTimeSetUp]
        public void Setup()
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
        }

        [Test]
        public void BuildCaseState1OfferBranch()
        {
            var offer = _allGroups.SingleOrDefault(x => x.ParentCaseStateId == 1 && x.ParentTaskConfigId == null);
            var rootParent = new TreeParent
            {
                CaseState = offer?.CaseState,
                Children = new List<TreeSiblings>()
            };

            foreach (var source in offer.TaskConfigSources.ToList())
            {
                var lvl1Children = new TreeSiblings
                {
                    Depth = offer.Depth,
                    CaseParent = rootParent,
                    Node = source,
                    Children = new List<TreeSiblings>()
                };

                rootParent.Children.Add(lvl1Children);
            }

            var immediateChildIds = new List<int>();

            //1. find the ids off the immediate children from the parent case group.
            foreach (var taskParent in rootParent.Children)
            {
                immediateChildIds.AddRange(GetAllChildren(taskParent.Node.TaskConfigurationId));

                if (immediateChildIds.Any())
                {
                    var children = _allSources.Where(x => x.TaskConfigGroupId == immediateChildIds.First()).ToList();
                    //2. Use the ids to gather a list of the next layer of children.
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
            }

            foreach (var taskSuperParent in rootParent.Children)
            {
                foreach (var taskParent in taskSuperParent.Children)
                {
                    immediateChildIds.AddRange(GetAllChildren(taskParent.Node.TaskConfigurationId));

                    if (immediateChildIds.Any())
                    {
                        var children = _allSources.Where(x => x.TaskConfigGroupId == immediateChildIds.First()).ToList();
                        //2. Use the ids to gather a list of the next layer of children.
                        foreach (var child in children)
                        {
                            var lvl3Children = new TreeSiblings
                            {
                                Depth = taskParent.Depth + 1,
                                TaskParent = taskParent,
                                Children = new List<TreeSiblings>(),
                                Node = child
                            };


                            taskParent.Children.Add(lvl3Children);
                        }
                    }

                    immediateChildIds.Clear();
                }
            }
        }

        [Test]
        public void BuildCaseState2OfferBranch()
        {
            var offer = _allGroups.SingleOrDefault(x => x.ParentCaseStateId == 2 && x.ParentTaskConfigId == null);
            var lvl1Children = offer?.TaskConfigSources;
            var lvl2Children = new List<TaskConfigSource>();
            var lvl3Children = new List<TaskConfigSource>();
            var lvl4Children = new List<TaskConfigSource>();

            //1. find the ids off the immediate children from the parent case group.
            var immediateChildIds = new List<int>();
            foreach (var child in lvl1Children)
            {
                immediateChildIds.AddRange(GetAllChildren(child.TaskConfigurationId));
            }
            //2. Use the ids to gather a list of the next layer of children.
            foreach (var childId in immediateChildIds)
            {
                lvl2Children.AddRange(_allSources.Where(x => x.TaskConfigGroupId == childId).ToList());
            }


            immediateChildIds.Clear();
            foreach (var child in lvl2Children)
            {
                immediateChildIds.AddRange(GetAllChildren(child.TaskConfigurationId));
            }
            foreach (var childId in immediateChildIds)
            {
                lvl3Children.AddRange(_allSources.Where(x => x.TaskConfigGroupId == childId).ToList());
            }


            immediateChildIds.Clear();
            foreach (var child in lvl3Children)
            {
                immediateChildIds.AddRange(GetAllChildren(child.TaskConfigurationId));
            }
            foreach (var childId in immediateChildIds)
            {
                lvl4Children.AddRange(_allSources.Where(x => x.TaskConfigGroupId == childId).ToList());
            }
        }

        [Test]
        public void BuildCaseState3OfferBranch()
        {
            var offer = _allGroups.SingleOrDefault(x => x.ParentCaseStateId == 3 && x.ParentTaskConfigId == null);
            var lvl1Children = offer?.TaskConfigSources;
            var lvl2Children = new List<TaskConfigSource>();
            var lvl3Children = new List<TaskConfigSource>();
            var lvl4Children = new List<TaskConfigSource>();

            //1. find the ids off the immediate children from the parent case group.
            var immediateChildIds = new List<int>();
            foreach (var child in lvl1Children)
            {
                immediateChildIds.AddRange(GetAllChildren(child.TaskConfigurationId));
            }
            //2. Use the ids to gather a list of the next layer of children.
            foreach (var childId in immediateChildIds)
            {
                lvl2Children.AddRange(_allSources.Where(x => x.TaskConfigGroupId == childId).ToList());
            }


            immediateChildIds.Clear();
            foreach (var child in lvl2Children)
            {
                immediateChildIds.AddRange(GetAllChildren(child.TaskConfigurationId));
            }
            foreach (var childId in immediateChildIds)
            {
                lvl3Children.AddRange(_allSources.Where(x => x.TaskConfigGroupId == childId).ToList());
            }


            immediateChildIds.Clear();
            foreach (var child in lvl3Children)
            {
                immediateChildIds.AddRange(GetAllChildren(child.TaskConfigurationId));
            }
            foreach (var childId in immediateChildIds)
            {
                lvl4Children.AddRange(_allSources.Where(x => x.TaskConfigGroupId == childId).ToList());
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
    }
}
