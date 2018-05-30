using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Functional.Option;

namespace TreeViewSandbox.DupesFromArtemis
{
    static class Coercion
    {
        public static bool TryCoerceDirectory(
            string description,
            out Option<DirectoryInfo> selectedPath,
            Environment.SpecialFolder rootFolder = Environment.SpecialFolder.MyComputer,
            Environment.SpecialFolder selectedFolder = Environment.SpecialFolder.MyDocuments,
            bool showNewFolderButton = true)
        {
            FolderBrowserDialog selectFolder = new FolderBrowserDialog()
            {
                RootFolder = rootFolder,
                SelectedPath = Environment.GetFolderPath(selectedFolder),
                Description = description,
                ShowNewFolderButton = showNewFolderButton,
            };

            if (selectFolder.ShowDialog() == DialogResult.OK && !string.IsNullOrEmpty(selectFolder.SelectedPath))
            {
                selectedPath = new DirectoryInfo(selectFolder.SelectedPath);
                return true;
            }
            else
            {
                selectedPath = Option.None;
                return false;
            }
        }

        internal static bool TryCoerceChoice<T>(string title, string prompt, List<T> choices, out T instance)
        {
            var selectBox = new SelectBox<T>(title, prompt, choices);
            selectBox.ShowDialog();
            if (selectBox.Chosen.HasValue)
            {
                instance = selectBox.Chosen.Value;
                return true;
            }
            instance = default(T);
            return false;
        }
    }
}
