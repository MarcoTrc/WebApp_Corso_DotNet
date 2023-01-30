using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

namespace WebApp.Models.Services.Infrastructure
{
    public class SqliteDatabaseAccessor : IDatabaseAccessor
    // 5. implementando l' interfaccia dovremo definire la logica 
    // del metodo 'DataSet Query(string query)' di cui proprio 
    // l'interfaccia è proprietaria

    // Sarà necessario registrare i nuovi servizi nel metodo ConfigureServices
    // della classe Startup (6.)
    {
        public async Task<DataSet> QueryAsync(FormattableString formattableQuery)
        {

            //Creiamo dei SqliteParameter a partire dalla FormattableString
            var queryArguments = formattableQuery.GetArguments();
            var sqliteParameters = new List<SqliteParameter>();
            for (var i = 0; i < queryArguments.Length; i++)
            {
                var parameter = new SqliteParameter(i.ToString(), queryArguments[i]);
                sqliteParameters.Add(parameter);
                queryArguments[i] = "@" + i;
            }
            string query = formattableQuery.ToString();

            // Per prima cosa creiamo l'oggetto SqliteConnection che prende in input
            // la stringa indicante il Db che si vuole raggiungere.

            // NB è buona prassi utilizzare dei blocchi 'using' quando
            // utilizziamo oggetti che implementano IDisposable provvisti del metodo Dispose(), questo
            // per assicurarci di terminare correttamente l'utilizzo dell'oggetto
            // qualsiasi cosa succeda in fase di esecuzione. 

            using (var conn = new SqliteConnection("Data source=Data/WebApp.db"))
            {
                // Invochiamo il metodo Open() di SqliteConnection per stabilire
                // la connessione. In questo modo chiederemo al connection pool
                // di fornirci una connessione già preparata e pronta all'uso.

                await conn.OpenAsync();

                // generiamo un oggetto di tipo SqliteCommand per inviare una query al DB
                // fra i suoi costruttori ne abbiamo uno che ci permette di indicare
                // sia il testo della query, argomento del metodo (Query(query)),
                // che la connessione  

                using (var cmd = new SqliteCommand(query, conn))
                {
                    cmd.Parameters.AddRange(sqliteParameters);

                    // invochiamo il metodo ExecuteReader sull'oggetto cmd, il quale ci 
                    // permetterà di inviare una query al Db che ritornerà dei dati 
                    // sotto forma di SqliteDatareader (ossia l'oggeto che ci permette di 
                    // leggere dei risultati una riga alla volta) 
                    // che assegnamo alla variabile reader

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {

                        // Ora finalizziamo l'implementazione del metodo Query che deve restituire
                        // un DataSet (oggetto in grado di conservare in memoria una o più tabelle di risultati
                        // che arrivano da un db relazionale, così che portremmo passarli dal nostro
                        // servizio infrastrutturale al nostro servizio applicativo)
                        var dataSet = new DataSet();

                        do
                        {
                            //un DataSet è una collezione di DataTable, per cui creeremo anche un oggetto
                            // DataTable
                            var dataTable = new DataTable();

                            // aggiungiamo il DataTable al DataSet
                            dataSet.Tables.Add(dataTable);

                            //A questo punto, tramite il metodo Load() di DataTable ptremmo prendere i dati passando in imput il SqliteDataReader

                            dataTable.Load(reader);

                        } while (!reader.IsClosed);

                        //e ritornando il DataSet
                        return dataSet;
                    }
                }
            }
            throw new NotImplementedException();
        }
    }
}