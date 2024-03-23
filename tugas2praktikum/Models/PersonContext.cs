using Npgsql;
using System;
using System.Collections.Generic;
using tugas2praktikum.Helpers;

namespace tugas2praktikum.Models
{
    public class PersonContext
    {
        private string __constr;
        private string __ErrorMsg;

        public PersonContext(string pConstr)
        {
            __constr = pConstr;
        }

        // Read (Select)
        public List<Person> ListPerson()
        {
            List<Person> list1 = new List<Person>();
            string query = string.Format(@"SELECT id_murid, nama, umur, asal_kota FROM users.murid;");
            sqlDBHelper dB = new sqlDBHelper(this.__constr);
            try
            {
                NpgsqlCommand cmd = dB.getNpgsqlCommand(query);
                NpgsqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list1.Add(new Person()
                    {
                        id_murid = int.Parse(reader["id_murid"].ToString()),
                        nama = reader["nama"].ToString(),
                        umur = reader["umur"].ToString(),
                        asal_kota = reader["asal_kota"].ToString()
                    });
                }
                cmd.Dispose();
                dB.closeConnection();
            }
            catch (Exception ex)
            {
                __ErrorMsg = ex.Message;
            }
            return list1;
        }

        // Create (Insert)
        public void AddPerson(Person person)
        {
            string query = string.Format(@"INSERT INTO users.murid (id_murid, nama, umur, asal_kota) VALUES (@id_murid, @nama, @umur, @asal_kota);");
            // Ini merupakan salah satu querry yang digunakan untuk mengisi kolom pada tabel yang sebelumnya kosong.
            sqlDBHelper dB = new sqlDBHelper(this.__constr);
            try
            {
                NpgsqlCommand cmd = dB.getNpgsqlCommand(query);
                cmd.Parameters.AddWithValue("@nama", person.nama);
                cmd.Parameters.AddWithValue("@umur", person.umur);
                cmd.Parameters.AddWithValue("@asal_kota", person.asal_kota);
                cmd.Parameters.AddWithValue("@id_murid", person.id_murid);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                dB.closeConnection();
            }
            catch (Exception ex)
            {
                __ErrorMsg = ex.Message;
            }
        }

        // Update
        public void UpdatePerson(Person person)
        {
            string query = "UPDATE users.murid SET nama = @nama, umur = @umur, asal_kota = @asal_kota WHERE id_murid = @id_murid;";
            sqlDBHelper dB = new sqlDBHelper(this.__constr);
            try
            {
                NpgsqlCommand cmd = dB.getNpgsqlCommand(query);
                cmd.Parameters.AddWithValue("@nama", person.nama);
                cmd.Parameters.AddWithValue("@umur", person.umur);
                cmd.Parameters.AddWithValue("@asal_kota", person.asal_kota);
                cmd.Parameters.AddWithValue("@id_murid", person.id_murid);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                dB.closeConnection();
            }
            catch (Exception ex)
            {
                __ErrorMsg = ex.Message;
                throw; // Melemparkan pengecualian untuk debugging
            }
        }

        // Delete
        public void DeletePerson(int id_murid)
        {
            string query = string.Format(@"DELETE FROM users.murid WHERE id_murid = @id_murid;");
            sqlDBHelper dB = new sqlDBHelper(this.__constr);
            try
            {
                NpgsqlCommand cmd = dB.getNpgsqlCommand(query);
                cmd.Parameters.AddWithValue("@id_murid", id_murid);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                dB.closeConnection();
            }
            catch (Exception ex)
            {
                __ErrorMsg = ex.Message;
            }
        }
    }
}