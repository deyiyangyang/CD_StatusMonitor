using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data.OleDb;
namespace StatusMonitor
{
    public partial class Database
    {

        public OleDbConnection connection = null;

        public Boolean openDB(string path)
        {
            try
            {
                connection = new OleDbConnection();
                string strconn = "";
                strconn = strconn + System.Configuration.ConfigurationManager.AppSettings["DBConn"].ToString();
                strconn = strconn + "comdesign3652;";
                strconn = strconn + "Data Source=" + path + "\\StatusMonitor.mdb;Persist Security Info=True";
                string connectString = strconn;
                connection.ConnectionString = connectString;
                connection.Open();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public Boolean openDB1(string appPath, string pathFlag)
        {
            try
            {
                string path = "";
                path = System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                path = path + "\\Comdesign";
                if (!System.IO.Directory.Exists(path))
                    System.IO.Directory.CreateDirectory(path);

                path = path + "\\StatusMonitorPre.mdb";
                if (!System.IO.File.Exists(path))
                    System.IO.File.Copy(appPath + "\\StatusMonitorPre.mdb", path);

                System.Threading.Thread.Sleep(200);
                if (!System.IO.File.Exists(path))
                    return false;



                connection = new OleDbConnection();
                string strconn = "";
                strconn = strconn + System.Configuration.ConfigurationManager.AppSettings["DBConn"].ToString();
                strconn = strconn + "comdesign3652;";
                strconn = strconn + "Data Source=" + path + ";Persist Security Info=True";
                string connectString = strconn;
                connection.ConnectionString = connectString;
                connection.Open();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public Boolean openDB(string appPath, string pathFlag)
        {
            try
            {
                string path = "";
                path = System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                path = path + "\\Comdesign";
                if (!System.IO.Directory.Exists(path))
                    System.IO.Directory.CreateDirectory(path);

                path = path + "\\StatusMonitor.mdb";
                if (!System.IO.File.Exists(path))
                    System.IO.File.Copy(appPath + "\\StatusMonitor.mdb", path);

                System.Threading.Thread.Sleep(200);
                if (!System.IO.File.Exists(path))
                    return false;



                connection = new OleDbConnection();
                string strconn = "";
                strconn = strconn + System.Configuration.ConfigurationManager.AppSettings["DBConn"].ToString();
                strconn = strconn + "comdesign3652;";
                strconn = strconn + "Data Source=" + path + ";Persist Security Info=True";
                string connectString = strconn;
                connection.ConnectionString = connectString;
                connection.Open();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public string readDB(string sql)
        {
            OleDbCommand command;
            OleDbDataReader reader;
            int j;
            ArrayList row;
            string intReturn = "";
            try
            {
                row = new ArrayList();
                command = new OleDbCommand(sql, connection);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    if (string.IsNullOrEmpty(reader.GetValue(0).ToString()))
                        intReturn = "";
                    else
                        intReturn = reader.GetValue(0).ToString();

                    break;

                }
                reader.Close();
                return intReturn;
            }
            catch (Exception ex)
            {
                return intReturn;
            }

        }

        public bool readDB(string sql, ArrayList record)
        {
            OleDbCommand command;
            OleDbDataReader reader;
            int j;
            ArrayList row;
            try
            {
                row = new ArrayList();
                command = new OleDbCommand(sql, connection);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    row = new ArrayList();
                    for (j = 0; j < reader.FieldCount; j++)
                    {
                        if (string.IsNullOrEmpty(reader.GetValue(j).ToString()))
                            row.Add("");
                        else
                            row.Add(reader.GetValue(j));


                    }
                    record.Add(row);
                }
                reader.Close();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        private bool IsDBNull(object p)
        {
            throw new Exception("The method or operation is not implemented.");
        }


        public bool excuteSql(string sql)
        {
            int iReturn = 0;
            OleDbCommand command;
            try
            {
                command = new OleDbCommand(sql, connection);
                iReturn = command.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool closeDB()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }
    }
}
