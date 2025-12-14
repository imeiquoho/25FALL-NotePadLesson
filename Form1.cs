using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _25FALL_NotePadLesson
{
    public partial class Form1 : Form
    {
        string filePath;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TextBlock.SelectAll();
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TextBlock.Undo();
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TextBlock.Redo();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (TextBlock.SelectedText != "")
            {
                Clipboard.SetText(TextBlock.SelectedText);
                TextBlock.SelectedText = "";
            }
            
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TextBlock.Paste();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //this.Close();
            Application.Exit();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
          if (TextBlock.Text != string.Empty)
            {
                string message = "Do you want to clear your document?";
                string caption = "New Document";
                DialogResult result = MessageBox.Show(message,caption,MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {
                    TextBlock.Text = "";
                }

            }

        }

        private async void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "Text Document | *.txt" }) 
                {
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        using (StreamWriter sw = new StreamWriter(sfd.FileName))
                        {
                            await sw.WriteAsync(TextBlock.Text);
                        }
                        filePath = sfd.FileName;
                    }
                }
            }
            else
            {
                using (StreamWriter sw = new StreamWriter(filePath))
                {
                    await sw.WriteAsync(TextBlock.Text);
                }
            }

        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    using (StreamReader sr = new StreamReader(ofd.FileName))
                    {
                        filePath = ofd.FileName;
                        string text = sr.ReadToEnd();
                        TextBlock.Text = text;
                    }
                }
            }
        }
    }
}
