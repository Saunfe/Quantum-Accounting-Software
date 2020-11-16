using Quantum_Accounting_Software.models;
using System.Collections.Generic;

namespace Quantum_Accounting_Software.GlobalVariable
{
    public static class Globalvar
    {
        private static List<Users> user_list = new List<Users>();

        internal static List<Users> User_list { get => user_list; set => user_list = value; }

        private static string vusername;

        public static string Username
        {
            get { return vusername; }
            set { vusername = value; }
        }

    }
}
