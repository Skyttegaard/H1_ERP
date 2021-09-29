using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.IO;
using H1_ERP.Models;
namespace H1_ERP.Factories
{
    public static class DatabaseFactory
    {
        static DatabaseFactory()
        {
            SqlConnection sqlServer = new SqlConnection(ReadConnectionString());
            sqlServer.Open();
            AddEverythingToVareLists(sqlServer);
            AddEverythingToKundeLists(sqlServer);
        }
        private static string ReadConnectionString()
        {
            string result = "Server=localhost;Database=ERPSystem;Trusted_Connection=True;MultipleActiveResultSets=true";
            if (!File.Exists(".\\settings.ini"))
            {
                File.AppendAllText(".\\settings.ini", result);
            }
            else
            {
                result = File.ReadAllText(".\\settings.ini");
            }
            
            return result;
        }
        
        private static void AddEverythingToKundeLists(SqlConnection server)
        {
            List<Kunde> tempList = new();
            SqlCommand command = new("SELECT Fornavn, Efternavn, PersonID, Email, Telefonnummer, Kontaktinfo, Vejnavn, Husnummer, Postnummer, [By], ID from Kunder", server);
            SqlDataReader dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                tempList.Add(new Kunde(dataReader.GetString(0), dataReader.GetString(1), dataReader.GetInt32(2), dataReader.GetString(3),
                    dataReader.GetInt32(4), dataReader.GetString(5), dataReader.GetString(6), dataReader.GetString(7), dataReader.GetInt32(8),
                    dataReader.GetString(9), dataReader.GetInt32(10)));
            }
        }
        private static void AddEverythingToVareLists(SqlConnection server)
        {
            List<Item> tempList = new();
            SqlCommand command = new("SELECT Navn, Salgspris, Købspris, ID, Lagerantal from Varer", server);
            SqlDataReader dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                tempList.Add(new Item(dataReader.GetString(0), dataReader.GetDouble(1), dataReader.GetDouble(2), dataReader.GetInt32(3), dataReader.GetInt32(4)));
            }
            
        }
        
    }
}
