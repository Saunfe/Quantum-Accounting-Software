using Microsoft.Data.Sqlite;
using Quantum_Accounting_Software.GlobalVariable;
using Quantum_Accounting_Software.models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace Quantum_Accounting_Software.db
{
    class dbmanager
    {
        public async static void InitializeDatabase()
        {
            await ApplicationData.Current.LocalFolder.CreateFileAsync("qubit.db", CreationCollisionOption.OpenIfExists);
            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "qubit.db");
            using (SqliteConnection db =
               new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                //create table user
                String table_user = "CREATE TABLE IF NOT EXISTS Users(" +
                    " username TEXT PRIMARY KEY, " +
                    "password TEXT NOT NULL)";
                SqliteCommand createTable_users = new SqliteCommand(table_user, db);
                createTable_users.ExecuteReader();


                //create table account charts
                String table_acc_chart = "CREATE TABLE IF NOT EXISTS Acc_chart (" +
                    "acc_type TEXT PRIMARY KEY, " +
                    "acc_det_type TEXT )";
                SqliteCommand createTable_acc_chart = new SqliteCommand(table_acc_chart, db);
                createTable_acc_chart.ExecuteReader();


                //create table account charts details description
                String table_acc_chart_desc = "CREATE TABLE IF NOT EXISTS Acc_chart_desc (" +
                    "acc_det_type TEXT PRIMARY KEY, " +
                    "desc TEXT )";
                SqliteCommand createTable_acc_chart_desc = new SqliteCommand(table_acc_chart_desc, db);
                createTable_acc_chart_desc.ExecuteReader();

                //create table accounts
                String table_accounts = "CREATE TABLE IF NOT EXISTS Accounts (" +
                    "acc_type TEXT PRIMARY KEY, " +
                    "acc_det_type TEXT, " +
                    "desc TEXT, " +
                    "name TEXT )";
                SqliteCommand createTable_accounts = new SqliteCommand(table_accounts, db);
                createTable_accounts.ExecuteReader();


                //create table journals 
                String table_journals = "CREATE TABLE IF NOT EXISTS Journals (" +
                    "Desc TEXT PRIMARY KEY, " +
                    "acc_det_type TEXT, " +
                    "dr TEXT, " +
                    "cr TEXT )";
                SqliteCommand createTable_journals = new SqliteCommand(table_journals, db);
                createTable_journals.ExecuteReader();

            }

        }

        //return users list
        public static List<Users> get_users()
        {
            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "qubit.db");
            using (SqliteConnection db =  new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                SqliteCommand selectCommand = new SqliteCommand
                    ("SELECT * from Users", db);

                SqliteDataReader query = selectCommand.ExecuteReader();

                while (query.Read())
                {
                    Users u = new Users();
                    u.Username = query.GetString(0);
                    u.Password = query.GetString(1);
                    Globalvar.User_list.Add(u);
                }

                db.Close();
            }

            return Globalvar.User_list;
        }

    }
}
