using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security.Permissions;

namespace DirectoryWatchApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnSelDir_Click(object sender, EventArgs e)
        {

            if (folderBrowserDialog1.ShowDialog(this) == DialogResult.OK)
            {
                if (defaultDirectoryWatcher != null) defaultDirectoryWatcher.Dispose();

                InitDirectoryWatcher(folderBrowserDialog1.SelectedPath, "job");                
            }
        }

        private static DirectoryWatch defaultDirectoryWatcher = null;

        public static void InitDirectoryWatcher(string sDirectory, string sExtention)
        {
            defaultDirectoryWatcher = new DirectoryWatch(sDirectory, sExtention);
            defaultDirectoryWatcher.onDirectoryChangedHandler += new DirectoryWatch.OnDirectoryChangedHandler(SomethingFileChanged);
        }

        private static void SomethingFileChanged(bool isRenamed, WatcherChangeTypes changeTypes, string name)
        {
            //Sample Event
            Console.WriteLine("{0} {1} {2}", isRenamed, changeTypes, name);
        }
    }

    #region DirectoryWatch Class Division 
    class DirectoryWatch : IDisposable
    {
        private FileSystemWatcher watcher = null;
        public delegate void OnDirectoryChangedHandler(bool isRenamed, WatcherChangeTypes changeTypes, string name);
        public event OnDirectoryChangedHandler onDirectoryChangedHandler;

        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        public DirectoryWatch(string sDirectory, string sExtention)
        {
            watcher = new FileSystemWatcher();
            watcher.Path = sDirectory;
            watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName;
            watcher.Filter = "*." + sExtention;

            watcher.Changed += new FileSystemEventHandler(OnChanged);
            //watcher.Created += new FileSystemEventHandler(OnChanged);
            //watcher.Deleted += new FileSystemEventHandler(OnChanged);
            watcher.Renamed += new RenamedEventHandler(OnRenamed);

            watcher.EnableRaisingEvents = true;
        }

        private void OnChanged(object source, FileSystemEventArgs e)
        {
            try
            {
                watcher.EnableRaisingEvents = false;
                onDirectoryChangedHandler?.Invoke(false, e.ChangeType, e.Name);
            }
            finally
            {
                watcher.EnableRaisingEvents = true;
            }
        }

        private void OnRenamed(object source, RenamedEventArgs e)
        {
            onDirectoryChangedHandler?.Invoke(true, e.ChangeType, e.Name);
        }

        public void Dispose()
        {
            watcher.EnableRaisingEvents = false;
            watcher.Changed -= new FileSystemEventHandler(OnChanged);
            watcher.Created -= new FileSystemEventHandler(OnChanged);
            watcher.Deleted -= new FileSystemEventHandler(OnChanged);
            watcher.Renamed -= new RenamedEventHandler(OnRenamed);

            watcher.Dispose();
            watcher = null;
            
        }
    }
    #endregion
}
