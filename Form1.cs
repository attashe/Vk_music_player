using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using VkNet;

//TO DO: изменить надписи на кнопках на иконки

namespace Vk_Music_Player
{
    public partial class Form1 : Form
    {
        static System.Collections.ObjectModel.ReadOnlyCollection<VkNet.Model.Attachments.Audio> AudioList;
        AsyncPlayer myPlayer;

        public Form1()
        {
            InitializeComponent();
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            var playList = AudioListSearch.SelectedIndices;
            if (playList.Count != 0)
            {
                foreach (int i in playList)
                {
                    AudioListPlay.Items.Add(AudioListSearch.Items[i]);
                    AsyncPlayer.EnqueueTask(AudioList[i]);
                }

            }
        }

        private void AudioListSearch_DoubleClick(object sender, EventArgs e)
        {
            //TO DO: добавить воспроизведение выдранной аудиозаписи по двойному клику с приостановкой основного потока воспроизведения
            throw new NotImplementedException("double click play");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog browseFolder = new FolderBrowserDialog();
            browseFolder.ShowDialog(Program.mainForm);
            textBox2.Text = browseFolder.SelectedPath;
            AsyncPlayer.FilePath = textBox2.Text;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            myPlayer = new AsyncPlayer();
        }

        private void AudioListPlay_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            myPlayer.Stop();
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            SearchResult();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SearchResult();
            }
        }

        private void SearchResult()
        {
            AudioListSearch.Items.Clear();
            int audioCount = 0;
            if (textBox1.Text == "")
                return;
            // TO DO: асинхронный поиск аудиозаписей отдельным методом
            AudioList = Vk.vk.Audio.Search(textBox1.Text, out audioCount, true);
            foreach (var audio in AudioList)
            {
                AudioListSearch.Items.Add(audio.Title);
            }
        }
    }
}
