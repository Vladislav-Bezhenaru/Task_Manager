using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Reflection;
using System.IO;

namespace Task_Manager
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            foreach (Process pr in Process.GetProcesses())
            {
                DGProcess.Rows.Add(pr.ProcessName, pr.Id, pr.VirtualMemorySize64 / 8 / 1024);
            }
        }

        private void btUpdate_Click(object sender, EventArgs e)
        {
            DGProcess.Rows.Clear();
            foreach (Process pr in Process.GetProcesses())
            {
                DGProcess.Rows.Add(pr.ProcessName, pr.Id, pr.VirtualMemorySize64 / 8 / 1024);
            }
        }

        private void DGProcess_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            fSecond form = new fSecond();
            try
            {
                string ProcName = DGProcess[0, e.RowIndex].Value.ToString();
                Process proc = Process.GetProcessesByName(ProcName)[0];
                ProcessThreadCollection processThreads = proc.Threads;
                ProcessModuleCollection modules = proc.Modules;
                MessageBox.Show(proc.ToString());
                foreach (ProcessThread thread in processThreads)
                {
                    form.dgvThreads.Rows.Add(thread.Id, thread.PriorityLevel);
                }
                //foreach (ProcessModule module in modules)
                //{
                //    form.dgvModules.Rows.Add(module.ModuleName, module.FileName, module.ModuleMemorySize / 8);
                //}
            }
            catch
            {
                MessageBox.Show("Повторите попытку");
            }
            form.Show();
        }
    }
}
