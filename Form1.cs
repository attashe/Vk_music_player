using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VkNet;

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
            int audioCount = 0;
            var AudioList = Vk.vk.Audio.Search("Веня Д'ркин", out audioCount, true);
            //AudioListSearch.Items.AddRange();
            foreach (var audio in AudioList)
            {
                AudioListSearch.Items.Add(audio);
            }
        }
    }
}
