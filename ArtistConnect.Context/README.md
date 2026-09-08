# ArtistConnect.Context

Este projeto concentra a conexão com o banco de dados MySQL do ArtistConnect.

## Configuração local

A classe `ArtistConnectContext` lê a connection string da variável de ambiente:

```text
ARTISTCONNECT_CONNECTION_STRING
```

Exemplo de formato para desenvolvimento local:

```text
Server=localhost;Port=3306;Database=ArtistConnect;User ID=seu_usuario;Password=sua_senha;
```

Configure os valores de usuário e senha somente no ambiente local. Não coloque credenciais reais no código-fonte ou em arquivos versionados.

No PowerShell, a variável pode ser definida para a sessão atual com:

```powershell
$env:ARTISTCONNECT_CONNECTION_STRING = "Server=localhost;Port=3306;Database=ArtistConnect;User ID=seu_usuario;Password=sua_senha;"
```

O banco deve ser criado executando o script `ArtistConnect.BD/BDARTISTCONNECT.sql` no MySQL.
