using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Threading.Tasks;
namespace Database_Console
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            //Чтобы всё повторялось
            while (true)
            {
                Console.WriteLine("Выберите действие \n1) Посмотреть все записи\r\n2) Добавить нового пользователя\r\n3) Обновить существующего пользователя\r\n4) Удалить существующего пользователя\r\n5) Авторизоваться в системе");
                int a = Convert.ToInt32(Console.ReadLine());

                string connectionString = "Server = sql.bsite.net\\MSSQL2016; Database = vetshot_my; User Id = vetshot_my; Password = my;TrustServerCertificate=true";

                string sqlExpression = "SELECT * FROM usersCon";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    //Выбор задания

                    //Просмотр пользователей
                    if (a == 1)
                    {
                        SqlCommand command = new SqlCommand(sqlExpression, connection);
                        SqlDataReader reader = await command.ExecuteReaderAsync();
                        if (reader.HasRows) // если есть данные
                        {
                            // выводим названия столбцов
                            string columnName0 = reader.GetName(0);
                            string columnName1 = reader.GetName(1);
                            string columnName2 = reader.GetName(2);
                            string columnName3 = reader.GetName(3);
                            string columnName4 = reader.GetName(4);

                            Console.WriteLine($"{columnName0} \t{columnName1} \t \t{columnName2} \t\t\t\t{columnName3}    {columnName4}");

                            // Console.WriteLine("{0,0}{1,10}{2,10}{3,20}{4,30}", columnName1, columnName2, columnName3, columnName4, columnName5);

                            while (await reader.ReadAsync())
                            {
                                int id = reader.GetInt32(0);
                                string course_id = reader.GetString(1);
                                object name = reader[2];
                                string description = reader.GetString(3);
                                object video_url = reader[4];

                                //course_id = course_id.Length > 15 ? course_id.Substring(0, Math.Min(course_id.Length, 15)) + "..." : course_id;
                                //description = description.Length > 15 ? description.Substring(0, Math.Min(description.Length, 15)) + "..." : description;


                                Console.WriteLine($"{id} \t{course_id} \t{name} {description} {video_url}");
                                //Console.WriteLine("{0,1}{2,10}{2,10}{3,20}{4,30}", id, course_id, name, description, video_url);

                            }


                        }
                        await reader.CloseAsync();
                    }
                    //Добавление пользователей
                    if (a == 2)
                    {
                        Console.WriteLine("Введите имя");
                        string name = (Console.ReadLine());
                        Console.WriteLine("Введите возраст");
                        int age = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Придумайте логин");
                        string username = (Console.ReadLine());
                        Console.WriteLine("Придумайте Пароль");
                        string password = (Console.ReadLine());

                        string addUsers = $"INSERT INTO usersCon (firstname, age, username, password) VALUES ('{name}', {age}, '{username}', '{password}')";
                        SqlCommand command = new SqlCommand(addUsers, connection);
                        await command.ExecuteNonQueryAsync();

                    }
                    //Обновление пользователей
                    if (a == 3)
                    {
                        Console.WriteLine("Введите ID пользователя, у которого хотите обновить данные");
                        string Up = (Console.ReadLine());
                        Console.WriteLine("Что вы хотите изменить? 1. Имя 2. Логин 3. Возраст");
                        int Choose = Convert.ToInt32(Console.ReadLine());
                        //Что хотите поменять
                        if (Choose == 1)
                        {
                            Console.WriteLine("На какое имя вы хотите изменить");
                            string name = (Console.ReadLine());
                            string updateUsers = $"UPDATE usersCon SET firstname='{name}' WHERE Id='{Up}'";

                            SqlCommand command = new SqlCommand(updateUsers, connection);
                            int number = await command.ExecuteNonQueryAsync();
                            Console.WriteLine($"Обновлено объектов: {number}");
                        }

                        if (Choose == 2)
                        {
                            Console.WriteLine("Какой логин задать");
                            string username = (Console.ReadLine());
                            string updateUsers = $"UPDATE usersCon SET username='{username}' WHERE Id='{Up}'";

                            SqlCommand command = new SqlCommand(updateUsers, connection);
                            int number = await command.ExecuteNonQueryAsync();
                            Console.WriteLine($"Обновлено объектов: {number}");
                        }
                        if (Choose == 3)
                        {

                            Console.WriteLine("Введите новый возраст");
                            int age = Convert.ToInt32(Console.ReadLine());
                            string updateUsers = $"UPDATE usersCon SET age='{age}' WHERE Id='{Up}'";

                            SqlCommand command = new SqlCommand(updateUsers, connection);
                            int number = await command.ExecuteNonQueryAsync();
                            Console.WriteLine($"Обновлено объектов: {number}");
                        }
                    }
                    //Удаление пользователей
                    if (a == 4)
                    {
                        Console.WriteLine("Введите ID пользователя, которого хотите удалить");
                        int id = Convert.ToInt32(Console.ReadLine());
                        string Delete = $"DELETE  FROM usersCon WHERE Id='{id}'";

                        SqlCommand command = new SqlCommand(Delete, connection);
                        int number = await command.ExecuteNonQueryAsync();
                        Console.WriteLine($"Удалено объектов: {number}");
                    }
                    if (a == 5)
                    {

                    }
                }
            }
            Console.Read();
        }
    }
}

