﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VkNet;

//TO DO: изменить надписи на кнопках на иконки

namespace Vk_Music_Player
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            AudioListSearch.Items.Clear();
            int audioCount = 0;
            var AudioList = Vk.vk.Audio.Search(textBox1.Text, out audioCount, true);
            //AudioListSearch.Items.AddRange();
            foreach (var audio in AudioList)
            {
                AudioListSearch.Items.Add(audio.Title);
            }
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            var playList = AudioListSearch.SelectedIndices;
            if (playList.Count != 0)
            {
                foreach (int i in playList)
                {
                    AudioListPlay.Items.Add(AudioListSearch.Items[i]);
                }

            }
        }

        private void AudioListSearch_DoubleClick(object sender, EventArgs e)
        {
            //TO DO: добавить воспроизведение выдранной аудиозаписи по двойному клику с приостановкой основного потока воспроизведения
            throw new NotImplementedException("double click play");
        }
    }
}
