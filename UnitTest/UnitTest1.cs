using WinFormsApp2;
using System.Data.SQLite;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestOptimalRouteWith3Vertices()
        {
            // Arrange
            Form1 form = new Form1();
            int rowCount = 3;
            int columnCount = 3;

            form.InitializeDataGridView(rowCount, columnCount);

            // Матрица для тестирования
            int[,] graph = new int[,]
            {
                {0, 1, 2},
                {1, 0, 3},
                {2, 3, 0}
            };

            // Act
            int[] optimalPath = form.FindOptimalRoute(graph, rowCount);

            // Assert
            Assert.IsNotNull(optimalPath);
            Assert.AreEqual(rowCount, optimalPath.Length);

            // Проверка, что путь содержит все вершины от 0 до rowCount-1
            for (int i = 0; i < rowCount; i++)
            {
                Assert.IsTrue(optimalPath.Contains(i), $"В пути отсутствует вершина {i}");
            }
        }

        [TestMethod]
        public void TestOptimalRouteWith4Vertices()
        {
            // Arrange
            Form1 form = new Form1();
            int rowCount = 4;
            int columnCount = 4;

            form.InitializeDataGridView(rowCount, columnCount);

            // Матрица для тестирования
            int[,] graph = new int[,]
            {
                {0, 1, 2, 3},
                {1, 0, 3, 4},
                {2, 3, 0, 1},
                {4, 2, 1, 0}
            };

            // Act
            int[] optimalPath = form.FindOptimalRoute(graph, rowCount);

            // Assert
            Assert.IsNotNull(optimalPath);
            Assert.AreEqual(rowCount, optimalPath.Length);

            // Проверка, что первый элемент равен 0
            Assert.AreEqual(0, optimalPath[0]);

            // Проверка, что последний элемент равен 1
            Assert.AreEqual(1, optimalPath[rowCount - 1]);

            // Проверка, что путь содержит все вершины от 0 до rowCount-1
            for (int i = 0; i < rowCount; i++)
            {
                Assert.IsTrue(optimalPath.Contains(i), $"В пути отсутствует вершина {i}");
            }
        }

        [TestMethod]
        public void TestOptimalRouteWith5Vertices()
        {
            // Arrange
            Form1 form = new Form1();
            int rowCount = 5;
            int columnCount = 5;

            form.InitializeDataGridView(rowCount, columnCount);

            // Матрица для тестирования
            int[,] graph = new int[,]
            {
                {0, 1, 2, 3, 4},
                {1, 0, 3, 4, 5},
                {2, 3, 0, 1, 2},
                {4, 2, 1, 0, 3},
                {3, 4, 5, 2, 0}
            };

            // Act
            int[] optimalPath = form.FindOptimalRoute(graph, rowCount);

            // Assert
            Assert.IsNotNull(optimalPath);
            Assert.AreEqual(rowCount, optimalPath.Length);

            // Проверка, что первый элемент равен 0
            Assert.AreEqual(0, optimalPath[0]);

            // Проверка, что последний элемент равен 1
            Assert.AreEqual(1, optimalPath[rowCount - 1]);

            // Проверка, что путь содержит все вершины от 0 до rowCount-1
            for (int i = 0; i < rowCount; i++)
            {
                Assert.IsTrue(optimalPath.Contains(i), $"В пути отсутствует вершина {i}");
            }
        }



        
        private const string DatabasePath = "D:\\Data Base for TUSUR\\users.db";
        private const string TestLogin = "test_admin";
        private const string TestPassword = "test_password";
        [TestMethod]
        public void AuthenticationSuccessful()
        {
            using (DB_work db = new DB_work(DatabasePath))
            {
                // Добавление тестового пользователя в базу данных
                db.add_new_user(TestLogin, TestPassword);

                // Попытка аутентификации тестового пользователя
                bool isAuthenticated = db.auth_user(TestLogin, TestPassword);

                // Проверка, что аутентификация прошла успешно
                Assert.IsTrue(isAuthenticated);
            }
        }

        [TestMethod]
        public void AuthenticationUnsuccessful()
        {
            using (DB_work db = new DB_work(DatabasePath))
            {
                // Попытка аутентификации с неправильными учетными данными
                bool isAuthenticated = db.auth_user(TestLogin, "WrongPassword");

                // Assert that authentication is unsuccessful
                Assert.IsFalse(isAuthenticated);
            }
        }

        [TestMethod]
        public void RegistrationSuccessful()
        {
            using (DB_work db = new DB_work(DatabasePath))
            {
                // Attempt to add a new user
                bool isRegistered = db.add_new_user("NewUser", "NewPassword");

                // Assert that registration is successful
                Assert.IsTrue(isRegistered);
            }
        }

        [TestMethod]
        public void RegistrationUnsuccessful()
        {
            using (DB_work db = new DB_work(DatabasePath))
            {
                // Add a test user to the database
                db.add_new_user(TestLogin, TestPassword);

                // Attempt to add a user with an existing login
                bool isRegistered = db.add_new_user(TestLogin, "NewPassword");

                // Assert that registration is unsuccessful
                Assert.IsFalse(isRegistered);
            }
        }
        

    }
}