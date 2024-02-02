using Microsoft.VisualBasic.Devices;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace WinFormsApp2
{
    public class DB_work : IDisposable
    {
        private SQLiteConnection conn;
        private Dictionary<string, int> loginAttempts;
        private List<string> randomWords;

        public DB_work(string path)
        {
            conn = new SQLiteConnection("Data Source = " + path);
            conn.Open();
            loginAttempts = new Dictionary<string, int>();

            // Заполните список случайных слов
            randomWords = new List<string>
            {
                "TUSUR",
                "Tomsk",
                "Course work",
                "Cool",
                "Pho Tigers",
                "Programmer",
                "Magic",
                "Bear",
                "Wolf",
                "Strezhevoy",
                "Harvard",
                "Library",
                "Lecture",
                "Exams",
                "Notebooks",
                "Caffeine",
                "Backpack",
                "Parties",
                "Research",
                "Internship"
            };
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // Освобождение управляемых ресурсов
                if (conn != null)
                {
                    if (conn.State == System.Data.ConnectionState.Open)
                    {
                        conn.Dispose();
                    }
                }
            }

            // Освобождение неуправляемых ресурсов
        }

        public bool disconnect()
        {
            if (conn != null)
            {
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Dispose();
                }
            }
            return true;
        }

        public bool add_new_user(string login, string password)
        {
            // Проверка наличия пользователя с таким логином
            if (UserExists(login))
            {
                string suggestedLogin = GenerateSuggestedLogin(login);
                MessageBox.Show($"Пользователь с логином '{login}' уже существует. Выберите другой логин. Предлагаемый новый логин: '{suggestedLogin}'. ", "Ошибка регистрации", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // Проверка ввода пароля
            if (string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Введите пароль.", "Ошибка регистрации", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (conn != null)
            {
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    try
                    {
                        SQLiteCommand cmd = conn.CreateCommand();
                        cmd.CommandText = "INSERT INTO data (login, password) VALUES (@login, @password)";
                        cmd.Parameters.AddWithValue("@login", login);
                        cmd.Parameters.AddWithValue("@password", gethash(password));
                        cmd.ExecuteNonQuery();

                        return true;
                    }
                    catch (Exception exp)
                    {
                        MessageBox.Show(exp.Message);
                    }
                }
            }

            return false;
        }

        public bool auth_user(string login, string password)
        {
            if (conn != null)
            {
                try
                {
                    // Проверка, не заблокирован ли пользователь
                    if (IsAccountLocked(login))
                    {
                        MessageBox.Show("Учетная запись заблокирована. Обратитесь в службу поддержки.");
                        return false;
                    }

                    using (SQLiteCommand command = conn.CreateCommand())
                    {
                        command.CommandText = "SELECT * FROM data WHERE login = @login";
                        command.Parameters.AddWithValue("@login", login);

                        object userExists = command.ExecuteScalar();

                        if (userExists != null)
                        {
                            // Пользователь найден в базе данных
                            command.CommandText = "SELECT * FROM data WHERE login = @login and password = @password";
                            command.Parameters.AddWithValue("@password", gethash(password));

                            object res = command.ExecuteScalar();

                            if (res != null)
                            {
                                // Пользователь успешно аутентифицирован, сбрасываем счетчик неудачных попыток
                                ResetLoginAttempts(login);
                                return true;
                            }
                            else
                            {
                                // Неверный пароль, увеличиваем счетчик неудачных попыток
                                IncrementLoginAttempts(login);
                                MessageBox.Show($"Неверный пароль. Осталось попыток: {GetRemainingAttempts(login)}");
                                return false;
                            }
                        }
                        else
                        {
                            // Пользователь не найден в базе данных
                            MessageBox.Show($"Пользователь с логином '{login}' не существует.", "Ошибка входа", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }
                    }
                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.Message);
                }
            }

            return false;
        }

        private void IncrementLoginAttempts(string login)
        {
            if (loginAttempts.ContainsKey(login))
            {
                loginAttempts[login]++;
            }
            else
            {
                loginAttempts[login] = 1;
            }

            // Если количество неудачных попыток превышает заданный порог (например, 5), блокируем учетную запись
            if (loginAttempts[login] >= 5)
            {
                LockAccount(login);
            }
        }

        private bool IsAccountLocked(string login)
        {
            // Проверка, заблокирована ли учетная запись
            return loginAttempts.ContainsKey(login) && loginAttempts[login] >= 5;
        }

        private void LockAccount(string login)
        {
            // Заблокировать учетную запись (удалить из базы данных)
            try
            {
                using (SQLiteCommand deleteCmd = conn.CreateCommand())
                {
                    // Используем параметризованный запрос, чтобы избежать SQL-инъекций
                    deleteCmd.CommandText = "DELETE FROM data WHERE login = @login";
                    deleteCmd.Parameters.AddWithValue("@login", login);
                    deleteCmd.ExecuteNonQuery();

                    MessageBox.Show("Ваша учётная запись удалена из-за подозрительной активности. Для её восставновления обратитесь по этом адресу электронной почты: shulepov.p.732-1@e.tusur.ru");
                    ResetLoginAttempts(login);
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

        private void ResetLoginAttempts(string login)
        {
            // Сбросить счетчик неудачных попыток
            loginAttempts.Remove(login);
        }

        private int GetRemainingAttempts(string login)
        {
            // Возвращает количество оставшихся попыток входа
            return 5 - (loginAttempts.ContainsKey(login) ? loginAttempts[login] : 0);
        }

        private string gethash(string text)
        {
            SHA256 sha256 = SHA256.Create();
            byte[] raw_text = Encoding.Unicode.GetBytes(text);
            byte[] raw_hash = sha256.ComputeHash(raw_text);
            string hash = Encoding.Unicode.GetString(raw_hash);

            sha256.Clear();
            return hash;
        }

        private string GenerateSuggestedLogin(string originalLogin)
        {
            Random random = new Random();
            string randomWord = randomWords[random.Next(randomWords.Count)];

            string suggestedLogin = $"{originalLogin}_{randomWord}";

            // Проверка уникальности логина в базе данных
            while (UserExists(suggestedLogin))
            {
                randomWord = randomWords[random.Next(randomWords.Count)];
                suggestedLogin = $"{originalLogin}_{randomWord}";
            }

            return suggestedLogin;
        }

        private bool UserExists(string login)
        {
            // Проверка, существует ли пользователь с указанным логином
            using (SQLiteCommand command = conn.CreateCommand())
            {
                command.CommandText = "SELECT COUNT(*) FROM data WHERE login = @login";
                command.Parameters.AddWithValue("@login", login);

                int userCount = Convert.ToInt32(command.ExecuteScalar());

                return userCount > 0;
            }
        }
    }
}
