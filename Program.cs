using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vk_Music_Player
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        //TO DO: Добавить глобальный обработчик ошибок и мьютекс, не позволяющий запускать два экзэмпляра приложения одновременно
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            authForm = new Authority();
            mainForm = new Form1();
            Application.Run(authForm);
        }
        static public Form authForm;
        static public Form mainForm;
    }
}
