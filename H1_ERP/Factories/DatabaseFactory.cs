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
        private static readonly SqlConnection sqlServer;
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
                File.WriteAllText(".\\settings.ini", result);
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
            SqlCommand command = new("SELECT Navn, Salgspris, Købspris,Lagerplads, ID, Lagerantal  from Varer", sqlServer);
            SqlDataReader dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                tempList.Add(new Item(dataReader.GetString(0), dataReader.GetDouble(1), dataReader.GetDouble(2), dataReader.GetInt32(3), dataReader.GetInt32(4), dataReader.GetInt32(5)));
            }
            return tempList;
            
        }
        public static List<Bestillinger> AddEverythingToBestillingList()
        {
            List<Bestillinger> tempList = new();
            SqlCommand command = new("SELECT KundeID,Lokation,Antal, VareID, OrdreID, Dato from ordre o left join ordrelinjer ol on o.ID = ol.OrdreID WHERE NOT Status = 'Delivered'", sqlServer);
            SqlDataReader dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                tempList.Add(new Bestillinger(GetVareNavn(dataReader.GetInt32(3)),dataReader.GetInt32(0), dataReader.GetString(1), dataReader.GetInt32(2), dataReader.GetInt32(3), dataReader.GetInt32(4), dataReader.GetDateTime(5)));
            }
            return tempList;
            //breaker hvis der er en ordre uden ordrelinjer
        }
        public static void AddKundeToDatabase(Kunde kunde)
        {
            SqlCommand command = new($"INSERT INTO Kunder (Fornavn, Efternavn, Postnummer,[By], Vejnavn, Husnummer, Email, Telefonnummer, Kontaktinfo, PersonID) VALUES('{kunde.Fornavn}', '{kunde.Efternavn}', {kunde.Adresse.PostNummer},'{kunde.Adresse.By}','{kunde.Adresse.Vejnavn}','{kunde.Adresse.HusNummer}','{kunde.Kontakt.Email}',{kunde.Kontakt.TelefonNummer}, '{kunde.Kontakt.Tekst}',{kunde.PersonId}) ", sqlServer);
            command.ExecuteNonQuery();
            WriteLineCommands.WritelineWaitForKeyPress($"{kunde.Fornavn} {kunde.Efternavn} has been added to the database!");
        }
        public static void EditKunde(Kunde kunde)
        {
            SqlCommand command = new($"UPDATE Kunder Set Fornavn = '{kunde.Fornavn}', Efternavn = '{kunde.Efternavn}', Postnummer = {kunde.Adresse.PostNummer}, [By] = '{kunde.Adresse.By}', Vejnavn = '{kunde.Adresse.Vejnavn}', Husnummer = '{kunde.Adresse.HusNummer}', Email = '{kunde.Kontakt.Email}', Telefonnummer = {kunde.Kontakt.TelefonNummer}, Kontaktinfo = '{kunde.Kontakt.Tekst}', PersonID = {kunde.PersonId} WHERE ID={kunde.Kundenummer}", sqlServer);
            command.ExecuteNonQuery();
            WriteLineCommands.WritelineWaitForKeyPress($"{kunde.Fornavn} {kunde.Efternavn} has been updated!");
        }
        public static void RemoveKunde(Kunde kunde)
        {
            SqlCommand command = new($"DELETE FROM Kunde WHERE ID={kunde.Kundenummer}", sqlServer);
            command.ExecuteNonQuery();
            WriteLineCommands.WritelineWaitForKeyPress($"{kunde.Fornavn} {kunde.Efternavn} has been removed from the database.");
        }
        public static void AddVare(Item item)
        {
            SqlCommand command = new($"INSERT INTO Varer (Navn, Lagerantal, Lagerplads, Salgspris,Købspris) VALUES('{item.ItemName}',{item.Quantity},{item.StorageCapacity} ,{item.ItemSalesPrice},{item.ItemBuyPrice})", sqlServer);
            command.ExecuteNonQuery();
            WriteLineCommands.WritelineWaitForKeyPress($"{item.ItemName} has been added to the database!");
        }
        public static void EditVare(Item item)
        {
            SqlCommand command = new($"UPDATE Varer set Navn = '{item.ItemName}', Salgspris = {item.ItemSalesPrice}, Købspris = {item.ItemBuyPrice} WHERE ID = {item.ItemID}", sqlServer);
            command.ExecuteNonQuery();
            WriteLineCommands.WritelineWaitForKeyPress($"{item.ItemName} has been updated!");
        }
        public static void RemoveVare(Item item)
        {
            SqlCommand command = new($"DELETE FROM Varer WHERE ID={item.ItemID}", sqlServer);
            command.ExecuteNonQuery();
            WriteLineCommands.WritelineWaitForKeyPress($"{item.ItemName} has been removed from the database.");
        }
        public static Item SelectVareByID(int id)
        {
            SqlCommand command = new($"SELECT Navn, Salgspris, Købspris, ID, Lagerantal, Lagerplads FROM Varer WHERE ID={id}", sqlServer);
            SqlDataReader dataReader = command.ExecuteReader();
            Item item = null;
            while (dataReader.Read())
            {
                item = new(dataReader.GetString(0), dataReader.GetDouble(1), dataReader.GetDouble(2), dataReader.GetInt32(3), dataReader.GetInt32(4), dataReader.GetInt32(5));
            }
            return item;
        }
        public static string GetVareNavn(int id)
        {
            SqlCommand command = new($"SELECT Navn from Varer WHERE ID={id}", sqlServer);
            SqlDataReader dataReader = command.ExecuteReader();
            string s = "noname";
            while (dataReader.Read())
            {
                s = dataReader.GetString(0);
            }
            return s;
        }
        public static void AddVareToBestillingList(int vareID, int antal, int kundeid, int ordreid = 0, string lokation = "")
        {
            SqlCommand command;
            if(ordreid == 0)
            {
                command = new($"INSERT INTO Ordrelinjer (VareID, OrdreID, Antal) VALUES({vareID}, {CreateOrdre(kundeid, lokation)}, {antal})", sqlServer);

            }
            else
            {
                command = new($"INSERT INTO Ordrelinjer (VareID, OrdreID, Antal) VALUES({vareID}, {ordreid}, {antal})", sqlServer);
            }
            command.ExecuteNonQuery();
            WriteLineCommands.WritelineWaitForKeyPress($"{GetVareNavn(vareID)} order has been added to the database");
        }
        public static int CreateOrdre(int kundeID, string lokation)
        {
            SqlCommand command = new($"INSERT INTO Ordre (KundeID,Dato,Lokation, Status) VALUES({kundeID}, '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}', '{lokation}', 'Pending'); SELECT SCOPE_IDENTITY()", sqlServer);
            var output = command.ExecuteScalar();
            int ordreID = Convert.ToInt32(output);
            return ordreID;
        }
        public static void DeliverItems(Bestillinger bestilling)
        {
            SqlCommand command = new($"UPDATE Ordre SET Status = 'Delivered' WHERE ID = {bestilling.OrderID}", sqlServer);
            SqlCommand command2 = new($"UPDATE Varer SET Lagerantal += { bestilling.Antal } WHERE ID = { bestilling.VareID }", sqlServer);
            command.ExecuteNonQuery();
            command2.ExecuteNonQuery();
        }
        public static DateTime GetLatestKundeOrderDato(int id)
        {
            DateTime date = new();
            SqlCommand command = new($"SELECT Dato from Ordre where KundeID = {id} order by Dato asc", sqlServer);
            SqlDataReader dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                date = dataReader.GetDateTime(0);
            }
            return date;
        }
        



    }
}
