## TreeView Tables in DB

Two relevant tables:
	- [TaskConfigSources] = The links between each parent-child relation.
	- [TaskConfigGroups]  = A group or sub-group of the tree structure that the Sources fall into.

### TaskConfigGroups Table

TaskConfigGroupId  = Primary Key, Auto-incrementing row identifier.
ParentTaskConfigId = Links in logic to the Ids used in the TaskConfiguration table.
ParentCaseStateId  = Links in logic to the Ids used in the CaseState table.
Depth			   = A record of the depth the group starts at in the tree (root nodes start at depth 1).

There is a condition enforced on the ParentTaskConfigId & ParentCaseStateId columns of type XOR. One column or the other can have a value but not both at once.

### TaskConfigSources Table

TaskConfigGroupId   = Composite Key with Foreign Key relation to the TaskConfigGroups table TaskConfigGroupId column.
TaskConfigurationId = Composite Key with Foreign Key relation to the TaskConfigurations table.
 
## TreeView Code

- The logic for the parent-child relation is held within the tables, the majority of the code queries these tables, assigns them to entities and displays them in a tree view.

### Add Node

- To add a node get the Id of the parent node.
- Look within the TaskConfigGroups table to see if there is already a group for the depth the new node will be inserted at, using the parent ID to find the group.
- If there is no group add one to the table.
- Once group found/created insert the new record into the TaskConfigSources table.
- Refresh UI to display new node.

### Delete Node

- Get the Id of the parent node.
- Get row from TaskConfigGroups table.
- Check if there are any rows in TaskConfigSources for this group.
- If no then delete group & refresh UI.
- If yes then prompt user warning them the delete will be recursive (they could be deleting an entire branch from root if they wanted).
- If they agree then take all records from TaskConfigSources for that group and run them against the TaskConfigGroups table to see if they are parents themselves, iterating all the way down the sub-tree.
- Once all the children are found delete them from the TaskConfigSources table & then delete the empty groups from the TaskConfigGroups table.
- Refresh UI.

## General Notes

- Much of the complexity in this task comes from the parent nodes potentially being two object types (CaseState or TaskConfig).
- Only Case States can be root nodes and only TaskConfigurations can be branch nodes.