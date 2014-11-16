using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

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
            Application.ThreadException += UnknownException;//как бы глобальный перехватчик каких-то необработанных исключений
            Application.Run(authForm);
        }
        static public Form authForm;
        static public Form mainForm;
        static void UnknownException(object sender, ThreadExceptionEventArgs e)
        {
            MessageBox.Show(e.ToString());
        }
    }
}
