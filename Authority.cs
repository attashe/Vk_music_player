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
using VkNet.Enums.Filters;

namespace Vk_Music_Player
{
        public partial class Authority : Form
        {
            public Authority()
            {
                InitializeComponent();
            }

            private void button1_Click(object sender, EventArgs e)
            {
                //TO DO: окно ожидания авторизации
                if (Auth(Login.Text, Password.Text)) //TO DO: шифрование пользовательских данных
                {
                    //Application.Run(new Form1());
                    //ActiveForm.Close();
                    Form mainForm = new Form1();
                    try
                    {
                        ActiveForm.Visible = false;// TO DO: исправить ошибку когда ни одна из форм приложения не является активный(пользователь за время авторизации убрфл фокус с приложения)
                        mainForm.ShowDialog(ActiveForm);
                    }
                    finally
                    {
                        Application.Exit();
                    }
                }
                else
                {
                    Password.Text = "";
                }
            }

            public bool Auth(string login, string pass)
            {
                int appID = 4576470;
                Settings scope = Settings.All; // TO DO: сделать выбор разрешений для программы пользвателем

                try
                {
                    Vk.vk.Authorize(appID, login, pass, scope); // TO DO: Сделать возможным сохранения пары логин/пароль
                }
                catch (VkNet.Exception.VkApiAuthorizationException e)
                {
                    //TO DO: Вынести строковую константу из кода, для локализируемости
                    MessageBox.Show(e.Email +
                                    "\nВозможно данный логин не существует, либо пароль набран не верно.\nПовторите ввод.");
                    return false;
                }
                return true; // TO DO: проверка успешности аутентификации
            }
        }

        public static class Vk
        {
            public static VkApi vk = new VkApi();
        }
}
