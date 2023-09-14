using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Text_Editor_RTF
{
    public partial class WinForm : Form
    {
        public WinForm()
        {
            InitializeComponent();
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {            
            OpenFileDialog openFileDialogWinForm = new OpenFileDialog();
            openFileDialogWinForm.Filter = "RTF files(*.rtf)|*.rtf|All files(*.*)|*.*";
            if (openFileDialogWinForm.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }
            // получаем выбранный файл
            string filename = openFileDialogWinForm.FileName;
            // читаем файл в строку
            richTextBoxWinForm.LoadFile(filename);     
            MessageBox.Show($"Открыт файл:\n{filename}\n");
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialogWinForm = new SaveFileDialog();
            saveFileDialogWinForm.Filter = "RTF files(*.rtf)|*.rtf|All files(*.*)|*.*";
            if (saveFileDialogWinForm.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }
            // получаем выбранный файл
            string filename = saveFileDialogWinForm.FileName;
            // сохраняем текст в файл
            richTextBoxWinForm.SaveFile(filename);   
            MessageBox.Show($"Результат сохранен в файл:\n{filename}\n");
        }

        private void backgroundColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // расширенное окно для выбора цвета
            colorDialogWinForm.FullOpen = true;
            // установка начального цвета для colorDialog
            colorDialogWinForm.Color = this.BackColor;
            if (colorDialogWinForm.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }
            // установка цвета формы
            richTextBoxWinForm.BackColor = colorDialogWinForm.Color;
        }
        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {            
            // добавляем возможность выбора цвета шрифта
            fontDialogWinForm.ShowColor = true;
            if (fontDialogWinForm.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }
            // установка шрифта
            richTextBoxWinForm.SelectionFont = fontDialogWinForm.Font;
            // установка цвета шрифта
            richTextBoxWinForm.SelectionColor = fontDialogWinForm.Color;
        }
        private void insertingAnImageToolStripMenuItemWinForm_Click(object sender, EventArgs e)
        {
            openFileDialogWinForm.Filter = "Images (*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF|All files (*.*)|*.*";
            if (openFileDialogWinForm.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }
            string filename = openFileDialogWinForm.FileName;
            Bitmap myBitmap = new Bitmap(filename);
            Clipboard.SetDataObject(myBitmap);
            DataFormats.Format myFormat = DataFormats.GetFormat(DataFormats.Bitmap);
            if (richTextBoxWinForm.CanPaste(myFormat))
            {
                richTextBoxWinForm.Paste(myFormat);
                return;
            }
            else
            {
                MessageBox.Show("The data format that you attempted to paste is not supported by this control.");
                return;
            }
        }
    }
}
