// See https://aka.ms/new-console-template for more information

using CsvHelper;
using CsvHelper.Configuration;
using MySql.Data.MySqlClient;
using Newtonsoft.Json.Linq;
using System.Globalization;
using TesteConversaoCSV;

var config = new CsvConfiguration(CultureInfo.InvariantCulture)
{
    Delimiter = ";",
    HasHeaderRecord = true
};

string json = File.ReadAllText("../../../config.json");
JObject configJson = JObject.Parse(json);
string connectionStringJson = (string)configJson["ConnectionString"];

string connectionString = connectionStringJson;

try
{
    MySqlConnection connection = new MySqlConnection(connectionString);

    connection.Open();

    using (var reader = new StreamReader("../../../arquivo_lai_PREV_5_202303.csv"))
    using (var csv = new CsvReader(reader, config))
    {
        csv.Context.RegisterClassMap<PGFNMap>();
        csv.Read();
        csv.ReadHeader();

        // COLUNAS DA TABELA
        string[] columnNames = csv.HeaderRecord;

        // Verifique se a tabela já existe no banco de dados
        string tableName = "Estabelecimentos";
        bool tableExists = CheckIfTableExists(connection, tableName);

        if (!tableExists)
        {
            // A tabela não existe, então crie-a
            string createTableQuery = $"CREATE TABLE {tableName} (";

            foreach (string columnName in columnNames)
            {
                createTableQuery += $"{columnName} VARCHAR(255), ";
            }

            createTableQuery = createTableQuery.TrimEnd(',', ' ');
            createTableQuery += ")";

            MySqlCommand createTableCommand = new MySqlCommand(createTableQuery, connection);
            createTableCommand.ExecuteNonQuery();
            Console.WriteLine("Tabela criada com sucesso!");
        }
        else
        {

            var dados = csv.GetRecords<PGFN>().ToList();

            string createData = $"INSERT INTO {tableName} ({string.Join(", ", columnNames)}) VALUES ";

            foreach (var dado in dados.Take(5))
            {
                createData += $"('{dado.CpfCnpj}', '{dado.TipoPessoa}', '{dado.TipoDevedor}', " +
                    $"'{dado.NomeDevedor}', '{dado.UfDevedor}', '{dado.UnidadeResponsável}', " +
                    $"'{dado.NumeroInscricao}', '{dado.TipoSituacaoInscricao}', " +
                    $"'{dado.SituacaoInscricao}', '{dado.TipoCredito}', " +
                    $"'{dado.DataInscricao}', '{dado.IndicadorAjuizado}', '{dado.ValorConsolidado}'), ";
            }

            createData = createData.TrimEnd(',', ' '); // Remover a vírgula extra no final

            MySqlCommand createTableCommand = new MySqlCommand(createData, connection);
            createTableCommand.ExecuteNonQuery();


            Console.WriteLine("Dados inserido com sucesso!");
        }
    }


    connection.Close();

    Console.WriteLine("Pressiona alguma tecla para continuar...");
    Console.ReadLine();
}
catch (Exception ex)
{
    Console.WriteLine("Erro ao conectar ao banco de dados: " + ex.Message);
    Console.ReadLine();
}

// Função para verificar se a tabela existe no banco de dados
static bool CheckIfTableExists(MySqlConnection connection, string tableName)
{
    using (var command = new MySqlCommand())
    {
        command.Connection = connection;
        command.CommandText = $"SHOW TABLES LIKE '{tableName}'";
        return command.ExecuteScalar() != null;
    }
}