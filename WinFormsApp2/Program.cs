namespace WinFormsApp2
{
    internal static class Program
    {
        private static DB_work dbwork = new DB_work("D:\\Data Base for TUSUR\\users.db");
        private static Form1 form1 = new Form1();
        private static Instruction form2 = new Instruction();
        private static Form3 form3 = new Form3(form1, dbwork);
        

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            form3.Show();
            Application.Run(form1);


        }
    }
}