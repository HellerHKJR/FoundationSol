using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Permissions;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace IniReadWrite
{
    public partial class Form1 : Form
    {
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal,
                                                        int size, string filePath);
        private DirectoryWatch defaultDirectoryWatcher = null;

        public Form1()
        {
            InitializeComponent();
            InitDirectoryWatcher(true);
        }

        private void btnWrite_Click(object sender, EventArgs e)
        {
            var file = File.Create(Environment.CurrentDirectory + "\\test.SGI" + ".tmp");
            file.Close();
            WritePrivateProfileString("Text_JPN", "MESSAGE", "\"val1\"", Environment.CurrentDirectory + "\\test.SGI" + ".tmp");
            WritePrivateProfileString("Text_ENG", "MESSAGE", "\"val1\"", Environment.CurrentDirectory + "\\test.SGI" + ".tmp");
            WritePrivateProfileString("Text_CHN", "MESSAGE", "\"val1\"", Environment.CurrentDirectory + "\\test.SGI" + ".tmp");
            WritePrivateProfileString("Text_TTT", "MESSAGE", "val1", Environment.CurrentDirectory + "\\test.SGI" + ".tmp");

            File.Delete(Environment.CurrentDirectory + "\\test.SGI");
            File.Move(Environment.CurrentDirectory + "\\test.SGI" + ".tmp", Environment.CurrentDirectory + "\\test.SGI");
        }

        public void InitDirectoryWatcher(bool isStart)
        {
            if (isStart)
            {
                defaultDirectoryWatcher = new DirectoryWatch(Environment.CurrentDirectory, @"^MC.*\.CMD$|^MC.*\.BID$");
                defaultDirectoryWatcher.onDirectoryChangedHandler += new DirectoryWatch.OnDirectoryChangedHandler(SomethingFileChanged);
            }
            else
            {
                if (defaultDirectoryWatcher != null)
                {
                    defaultDirectoryWatcher.onDirectoryChangedHandler -= new DirectoryWatch.OnDirectoryChangedHandler(SomethingFileChanged);
                    defaultDirectoryWatcher.Dispose();
                }
            }
        }

        private void SomethingFileChanged(bool isRenamed, WatcherChangeTypes changeTypes, string name)
        {
            if (changeTypes == WatcherChangeTypes.Changed)
            {
                

            }

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            InitDirectoryWatcher(false);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string[] fileEntries = Directory.GetFiles(Environment.CurrentDirectory, "MC*.CMD");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder(255);
            int ret = GetPrivateProfileString("Text_TTT", "MESSAGE", "nothing", sb, sb.Capacity, Environment.CurrentDirectory + "\\test.SGI");
            ret = GetPrivateProfileString("Text_JPN", "MESSAGE", "nothing", sb, sb.Capacity, Environment.CurrentDirectory + "\\test.SGI");

        }
    }


    #region DirectoryWatch Class Division 
    class DirectoryWatch : IDisposable
    {
        private FileSystemWatcher watcher = null;
        public delegate void OnDirectoryChangedHandler(bool isRenamed, WatcherChangeTypes changeTypes, string name);
        public event OnDirectoryChangedHandler onDirectoryChangedHandler;
        private string sFilter;

        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        public DirectoryWatch(string sDirectory, string sFilter)
        {            
            watcher = new FileSystemWatcher();
            watcher.Path = sDirectory;
            watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName;
            watcher.Filter = "*.*";
            this.sFilter = sFilter;

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

                if (Regex.IsMatch(e.Name, sFilter))
                    onDirectoryChangedHandler?.Invoke(false, e.ChangeType, e.FullPath);
            }
            finally
            {
                watcher.EnableRaisingEvents = true;
            }
        }

        private void OnRenamed(object source, RenamedEventArgs e)
        {
            try
            {
                watcher.EnableRaisingEvents = false;

                if (Regex.IsMatch(e.Name, sFilter))
                    onDirectoryChangedHandler?.Invoke(true, e.ChangeType, e.FullPath);
            }
            finally
            {
                watcher.EnableRaisingEvents = true;
            }
        }

        public void Dispose()
        {
            watcher.EnableRaisingEvents = false;
            watcher.Changed -= new FileSystemEventHandler(OnChanged);
            //watcher.Created -= new FileSystemEventHandler(OnChanged);
            //watcher.Deleted -= new FileSystemEventHandler(OnChanged);
            watcher.Renamed -= new RenamedEventHandler(OnRenamed);

            watcher.Dispose();
            watcher = null;

        }
    }
    #endregion
}
