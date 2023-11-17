using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BucolPad
{
    public partial class Form1 : Form
    {
        private bool modifyDocument = false;

        public Form1()
        {
            InitializeComponent();
            this.Text = "Untitled - BucolPad";
        }

        private void NewDocument()
        {
            if (modifyDocument)
            {
                var result = MessageBox.Show("Do you want to save changes to the current document?", "Save Changes", MessageBoxButtons.YesNoCancel);

                if (result == DialogResult.Yes)
                {
                    SaveDocument();
                }
                else if (result == DialogResult.Cancel)
                {
                    return;
                }
            }
            richTextBox1.Clear();
            this.Text = "Untitled - BucolPad";
            modifyDocument = false;
        }

        private void SaveDocument()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Save";
            saveFileDialog.Filter = "Text Document(*.txt)|*.txt| All Files(*.*)|*.*";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.SaveFile(saveFileDialog.FileName, RichTextBoxStreamType.PlainText);
                this.Text = saveFileDialog.FileName;
                modifyDocument = false;
            }
            this.Text = "Untitled - BucolPad";
            modifyDocument = false;
        }

        private void ConfirmDocument()
        {
            if (modifyDocument)
            {
                var result = MessageBox.Show("Do you want to save changes to the current document?", "Save Changes", MessageBoxButtons.YesNoCancel);

                if (result == DialogResult.Yes)
                {
                    SaveDocument();
                }
                else if (result == DialogResult.No)
                {
                    Application.Exit();
                }
                else
                {
                    return;
                }
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewDocument();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (modifyDocument)
            {
                var result = MessageBox.Show("Do you want to save changes to the current document?", "Save Changes", MessageBoxButtons.YesNoCancel);

                if (result == DialogResult.Yes)
                {
                    SaveDocument();
                }
                else if (result == DialogResult.Cancel)
                {
                    return;
                }
            }

            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Open";
            op.Filter = "Text Document(*.txt)|*.txt| All Files(*.*)|*.*";

            if (op.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.LoadFile(op.FileName, RichTextBoxStreamType.PlainText);
                this.Text = op.FileName;
                modifyDocument = false;
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveDocument();
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Undo();
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Redo();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Copy();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Paste();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Cut();
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectAll();
        }

        private void dateTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = System.DateTime.Now.ToString();
        }

        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog fontDialog = new FontDialog();
            if (fontDialog.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.Font = fontDialog.Font;
            }
        }

        private void colorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.ForeColor = colorDialog.Color;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            richTextBox1.WordWrap = wordWarpToolStripMenuItem.Checked;
            statusBarToolStripMenuItem.Enabled = !wordWarpToolStripMenuItem.Checked;
            statusBarToolStripMenuItem.Checked = statusStrip1.Visible = statusBarToolStripMenuItem.Enabled;
        }

        private void wordWarpToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            richTextBox1.WordWrap = wordWarpToolStripMenuItem.Checked;
            statusBarToolStripMenuItem.Enabled = !wordWarpToolStripMenuItem.Checked;
            statusBarToolStripMenuItem.Checked = statusStrip1.Visible = statusBarToolStripMenuItem.Enabled;
        }

        private void statusBarToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            statusStrip1.Visible = statusBarToolStripMenuItem.Checked;
        }

        private void richTextBox1_SelectionChanged(object sender, EventArgs e)
        {
            StatusChange();
        }

        private void StatusChange()
        {
            int area = richTextBox1.SelectionStart;
            int line = richTextBox1.GetLineFromCharIndex(area) + 1;
            int col = area - richTextBox1.GetFirstCharIndexOfCurrentLine() + 1;

            status.Text = "Ln " + line + ", Col " + col;
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            modifyDocument = true;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            ConfirmDocument();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConfirmDocument();
        }
    }
}
