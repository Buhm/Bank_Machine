using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankMachine
{
    class DBClass
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;

        //Constructor
        public DBClass()
        {
            Initialize();
        }


        private void Initialize()
        {
            server = "localhost";
            database = "bankaccounts";
            uid = "root";
            password = "testicles";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

            connection = new MySqlConnection(connectionString);
        }


        private bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {

                //The two most common error numbers when connecting are:
                //0: Cannot connect to server.
                //1045: Invalid user name and/or password.
                switch (ex.Number)
                {
                    case 0:
                        MessageBox.Show("Cannot connect to server.  Contact administrator");
                        break;

                    case 1045:
                        MessageBox.Show("Invalid username/password, please try again");
                        break;
                }
                return false;
            }

        }


        private bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }


        public void Insert(string UserId, string Amount, bool IsAmountPositive)
        {
            int intUserId;
            int.TryParse(UserId, out intUserId);
            int intAmount;
            int.TryParse(Amount, out intAmount);

            //Console.WriteLine("INSERT INTO transfers (user_id, amount, isamountpositive) VALUES( " + UserId + ", " + Amount + ", " + IsAmountPositive + ")");
            string query = "INSERT INTO transfers (user_id, amount, isamountpositive) VALUES( " + intUserId + ", " + intAmount + ", " + IsAmountPositive + ")";


            //open connection
            if (this.OpenConnection() == true)
            {
                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(query, connection);

                //Execute command
                cmd.ExecuteNonQuery();

                //close connection
                this.CloseConnection();
            }

        }


        public void Update(int userId, float NewBalance)
        {
            //variable to hold the changed balance amount

            string query = "UPDATE users SET balance=" + NewBalance + " WHERE id=" + 1 + "";
            //Console.WriteLine("query: {0}", query);

            //Open connection
            if (this.OpenConnection() == true)
            {
                //create mysql command
                MySqlCommand cmd = new MySqlCommand();
                //Assign the query using CommandText
                cmd.CommandText = query;
                //Assign the connection using Connection
                cmd.Connection = connection;

                //Execute query
                cmd.ExecuteNonQuery();

                //close connection
                this.CloseConnection();
            }

        }


        public void Delete()
        {
        }


        public List<string>[] SelectList(string query)
        {

            //Create a list to store the result
            List<string>[] list = new List<string>[4];
            list[0] = new List<string>();
            list[1] = new List<string>();
            list[2] = new List<string>();
            list[3] = new List<string>();

            //Open connection
            if (this.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    list[0].Add(dataReader["id"] + "");
                    list[1].Add(dataReader["BankAccountNumber"] + "");
                    list[2].Add(dataReader["UserName"] + "");
                    list[3].Add(dataReader["Balance"] + "");
                }


                dataReader.Close();


                this.CloseConnection();

                return list;

            }
            else
            {

                return list;

            }

        }


        public string SelectSingle(string Userquery)
        {
            //base value in case no value is returned through SQL query
            string NoResultFound = "No Result Found";

            //Open connection
            if (this.OpenConnection() == true)
            {
                string query = "SELECT " + Userquery + " FROM users WHERE id = 1";
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                string foundResult;
                //Read the data and store them in the list
                dataReader.Read();

                foundResult = dataReader.GetString(Userquery);


                dataReader.Close();

                this.CloseConnection();

                return foundResult;

            }
            else
            {

                Console.WriteLine("DBClass connect failed");
                return NoResultFound;

            }

        }


        public int Count()
        {
            string query = "SELECT Count(*) FROM users";
            int Count = -1;

            //Open Connection
            if (this.OpenConnection() == true)
            {
                //Create Mysql Command
                MySqlCommand cmd = new MySqlCommand(query, connection);

                //ExecuteScalar will return one value
                Count = int.Parse(cmd.ExecuteScalar() + "");

                //close Connection
                this.CloseConnection();

                return Count;
            }
            else
            {
                return Count;
            }
        }


        public void Backup()
        {
        }


        public void Restore()
        {
        }
    }
}
