using MySqlConnector;


namespace ArtistConnect.Context;

public sealed class ArtistConnectContext
{
    public const string ConnectionStringEnvironmentVariable = "ARTISTCONNECT_CONNECTION_STRING";

    public string ConnectionString { get; }

    public ArtistConnectContext(string? connectionString = null)
    {
        ConnectionString = connectionString
            ?? Environment.GetEnvironmentVariable(ConnectionStringEnvironmentVariable)
            ?? throw new InvalidOperationException(
                $"Configure a variável de ambiente {ConnectionStringEnvironmentVariable} com a connection string do MySQL.");
    }

    public MySqlConnection CriarConexao()
    {
        return new MySqlConnection(ConnectionString);
    }

    public async Task<bool> TestarConexaoAsync(CancellationToken cancellationToken = default)
    {
        await using MySqlConnection connection = CriarConexao();
        await connection.OpenAsync(cancellationToken);
        return connection.State == System.Data.ConnectionState.Open;
    }
}
