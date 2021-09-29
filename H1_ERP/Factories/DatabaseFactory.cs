using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.IO;
using H1_ERP.Models;
using H1_ERP.ConsoleCommands;
namespace H1_ERP.Factories
{
    public static class DatabaseFactory
    {
        static SqlConnection sqlServer;
        static DatabaseFactory()
        {
            sqlServer = new(ReadConnectionString());
            sqlServer.Open();
            
        }
        private static string ReadConnectionString()
        {
            string result = "Server=LAPTOP-D3QE2FUO;Database=ERPSystem;Trusted_Connection=True;MultipleActiveResultSets=true";
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
        
        public static List<Kunde> AddEverythingToKundeLists()
        {
            
            List<Kunde> tempList = new();
            SqlCommand command = new("SELECT Fornavn, Efternavn, PersonID, Email, Telefonnummer, Kontaktinfo, Vejnavn, Husnummer, Postnummer, [By], ID from Kunder", sqlServer);
            SqlDataReader dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                tempList.Add(new Kunde(dataReader.GetString(0), dataReader.GetString(1), dataReader.GetInt32(2), dataReader.GetString(3),
                    dataReader.GetInt32(4), dataReader.GetString(5), dataReader.GetString(6), dataReader.GetString(7), dataReader.GetInt32(8),
                    dataReader.GetString(9), dataReader.GetInt32(10)));
            }
            return tempList;
        }
        public static List<Item> AddEverythingToVareLists()
        {
            
            List<Item> tempList = new();
            SqlCommand command = new("SELECT Navn, Salgspris, Købspris, ID, Lagerantal from Varer", sqlServer);
            SqlDataReader dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                tempList.Add(new Item(dataReader.GetString(0), dataReader.GetDouble(1), dataReader.GetDouble(2), dataReader.GetInt32(3), dataReader.GetInt32(4)));
            }
            return tempList;
            
        }
        public static void AddKundeToDatabase(Kunde kunde)
        {
            SqlCommand command = new($"INSERT INTO Kunder (Fornavn, Efternavn, Postnummer,[By], Vejnavn, Husnummer, Email, Telefonnummer, Kontaktinfo, PersonID) VALUES('{kunde.Fornavn}', '{kunde.Efternavn}', {kunde.Adresse.PostNummer},'{kunde.Adresse.By}','{kunde.Adresse.Vejnavn}','{kunde.Adresse.HusNummer}','{kunde.Kontakt.Email}',{kunde.Kontakt.TelefonNummer}, '{kunde.Kontakt.Tekst}',{kunde.PersonId}) ", sqlServer);

            SqlDataReader dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                WriteLineCommands.WriteLineMessage("Uploading to database...");
            }
            WriteLineCommands.WritelineWaitForKeyPress("Success!");
        }
        
    }
}
