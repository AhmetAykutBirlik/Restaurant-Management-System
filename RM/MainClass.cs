using DevExpress.Xpo.DB.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RM
{
    class MainClass
    {

        public static readonly string con_string = "Data Source=.;Initial Catalog = AAB_RestaurantManagementSystem; Integrated Security = True; Encrypt=False";
        //"Data Source=.;Initial Catalog=AAB_RestaurantManagementSystem;Persist security Info-True;User ID=sa; Password=123"
        public static SqlConnection con = new SqlConnection(con_string);

        //Check user validation

        public static bool IsValidUser(string user, string pass)
        {
            bool isValid = false;
            string qry = @"Select * from users where username   = '" + user + "' and upass = '" + pass + "' ";
            SqlCommand cmd = new SqlCommand(qry, con);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                isValid = true;
                USER = dt.Rows[0]["uName"].ToString();
            }
            return isValid;
        }
        // Create property for username
        public static string user;
        public static string USER
        {
            get { return user; }
            private set { user = value; }
        }

        // Methord for curd operation

        public static int SQl(string qry, Hashtable ht)
            {
            int res = 0 ;
            try
            {
                SqlCommand cmd = new SqlCommand(qry, con);
                cmd.CommandType = CommandType.Text;

                foreach (DictionaryEntry item in ht)
                {
                    cmd.Parameters.AddWithValue(item.Key.ToString(), item.Value);
                }
                if (con.State == ConnectionState.Closed) { con.Open(); }
                res = cmd.ExecuteNonQuery();
                if (con.State == ConnectionState.Open) { con.Close(); }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                con.Close();
            }
            return res ;
            }
        // For Loading data from database

        public static void LoadData(string qry, DataGridView gv, ListBox lb)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(qry, con);
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                for (int i = 0; i < lb.Items.Count; i++)
                {
                    string colNam1 = ((DataGridViewColumn)lb.Items[i]).Name;
                    gv.Columns[colNam1].DataPropertyName = dt.Columns[i].ToString();

                }
                gv.DataSource = dt;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
                con.Close();
            }
        }
    }
}
