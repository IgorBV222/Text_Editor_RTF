using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Text_Editor_RTF;

namespace Text_Editor_RTF
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 newMDIChild = new Form2();            
            newMDIChild.MdiParent = this;            
            newMDIChild.Show();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Determine the active child form.  
            Form activeChild = this.ActiveMdiChild;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "RTF files(*.rtf)|*.rtf|All files(*.*)|*.*";
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }
            // получаем выбранный файл
            string filename = openFileDialog1.FileName;
            MessageBox.Show($"Открыт файл:\n{filename}\n");            
            if (activeChild != null)
            {
                try
                {
                    RichTextBox theBox = (RichTextBox)activeChild.ActiveControl;
                    if (theBox != null)
                    {                        
                        theBox.LoadFile(filename);
                    }
                }
                catch
                {
                    MessageBox.Show("You need to select a RichTextBox.");
                }
            }
        }
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form activeChild = this.ActiveMdiChild;
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "RTF files(*.rtf)|*.rtf|All files(*.*)|*.*";
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }
            // получаем выбранный файл
            string filename = saveFileDialog1.FileName;
            // сохраняем текст в файл            
            MessageBox.Show($"Результат сохранен в файл:\n{filename}\n");
            if (activeChild != null)
            {
                try
                {
                    RichTextBox theBox = (RichTextBox)activeChild.ActiveControl;
                    if (theBox != null)
                    {
                        theBox.SaveFile(filename);                        
                    }
                }
                catch
                {
                    MessageBox.Show("Вам нужно выбрать RichTextBox.");
                }
            }
        }
        private void backgroundColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form activeChild = this.ActiveMdiChild;
            // расширенное окно для выбора цвета
            colorDialog1.FullOpen = true;
            // установка начального цвета для colorDialog
            colorDialog1.Color = this.BackColor;
            if (colorDialog1.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }
            if (activeChild != null)
            {
                try
                {
                    RichTextBox theBox = (RichTextBox)activeChild.ActiveControl;
                    if (theBox != null)
                    {
                        // установка цвета формы
                        theBox.BackColor = colorDialog1.Color;
                    }
                }
                catch
                {
                    MessageBox.Show("Вам нужно выбрать RichTextBox.");
                }
            }
        }
        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form activeChild = this.ActiveMdiChild;
            // добавляем возможность выбора цвета шрифта
            fontDialog1.ShowColor = true;
            if (fontDialog1.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }
            if (colorDialog1.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }
            if (activeChild != null)
            {
                try
                {
                    RichTextBox theBox = (RichTextBox)activeChild.ActiveControl;
                    if (theBox != null)
                    {
                        // установка шрифта
                        theBox.SelectionFont = fontDialog1.Font;
                        // установка цвета шрифта
                        theBox.SelectionColor = fontDialog1.Color;
                    }
                }
                catch
                {
                    MessageBox.Show("Вам нужно выбрать RichTextBox.");
                }
            }            
        }
        private void insertingAnImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form activeChild = this.ActiveMdiChild;
            openFileDialog1.Filter = "Images (*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF|All files (*.*)|*.*";
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }
            string filename = openFileDialog1.FileName;
            Bitmap myBitmap = new Bitmap(filename);
            Clipboard.SetDataObject(myBitmap);
            DataFormats.Format myFormat = DataFormats.GetFormat(DataFormats.Bitmap);

            if (activeChild != null)
            {
                try
                {
                    RichTextBox theBox = (RichTextBox)activeChild.ActiveControl;
                    if (theBox != null)
                    {
                        if (theBox.CanPaste(myFormat))
                        {
                            theBox.Paste(myFormat);
                            return;
                        }
                        else
                        {
                            MessageBox.Show("Формат данных, который вы пытались вставить, не поддерживается этим элементом управления.");
                            return;
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("Вам нужно выбрать RichTextBox.");
                }
            }
        }
        private void independentInterfaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WinForm winForm = new WinForm();
            winForm.Size = this.Size;
            winForm.Text = $"Независимый интерфейс";
            winForm.Show();
        }
        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}