# ArtistConnect.WebApi

API REST para artistas e músicas. Todos os endpoints de `/api/artistas` e
`/api/musicas` exigem `Authorization: Bearer <token>`. A API apenas valida
tokens JWT assinados pelo próprio ArtistConnect. As contas são armazenadas no
banco `ArtistConnect` e as senhas são persistidas como hashes.

## Autenticação

Em Development, crie uma conta de teste:

```http
POST /api/auth/register
Content-Type: application/json

{
  "nome": "Usuário de Teste",
  "email": "teste@artistconnect.local",
  "senha": "teste12345"
}
```

O cadastro de conta fica desabilitado fora de Development. Faça login para
receber um JWT Bearer:

```http
POST /api/auth/login
Content-Type: application/json

{
  "email": "teste@artistconnect.local",
  "senha": "teste12345"
}
```

Use o campo `accessToken` retornado no header
`Authorization: Bearer <accessToken>` para as rotas de artistas e músicas. O
token segue o padrão do DiarioSenac (claims `sub`, `email` e `name`, assinatura
HS256, emissor e público configurados), mas usa a tabela de usuários do
ArtistConnect.

## Configuração

Defina a conexão MySQL e os parâmetros do JWT em User Secrets:

```powershell
dotnet user-secrets set "ConnectionStrings:ArtistConnect" "Server=localhost;Port=3306;Database=ArtistConnect;User=SEU_USUARIO;Password=SUA_SENHA;" --project ArtistConnect.WebApi
dotnet user-secrets set "Jwt:Issuer" "issuer-do-token" --project ArtistConnect.WebApi
dotnet user-secrets set "Jwt:Audience" "ArtistConnect.Api" --project ArtistConnect.WebApi
dotnet user-secrets set "Jwt:SigningKey" "<segredo-aleatorio-com-pelo-menos-32-bytes>" --project ArtistConnect.WebApi
```

O projeto está configurado para carregar User Secrets quando executado em
`Development` (incluindo o perfil de `launchSettings.json`). Os secrets ficam
fora do repositório. Para listar ou remover um valor configurado:

```powershell
dotnet user-secrets list --project ArtistConnect.WebApi
dotnet user-secrets remove "ConnectionStrings:ArtistConnect" --project ArtistConnect.WebApi
```

Use uma chave aleatória com pelo menos 32 bytes e não reutilize a chave de
outro projeto. Não armazene a chave no repositório. Login e validação usam
HS256 e verificam assinatura, emissor, público e expiração.

Inicie a API com `dotnet run --project ArtistConnect.WebApi`. Em ambiente de
desenvolvimento, a especificação OpenAPI fica em `/openapi/v1.json`.

## Migrations

As migrations ficam em `ArtistConnect.Data/Migrations`. Com a connection string
configurada no ambiente:

```powershell
dotnet ef database update --project ArtistConnect.Data --startup-project ArtistConnect.WebApi
```

Para criar uma migration:

```powershell
dotnet ef migrations add NomeDaMigration --project ArtistConnect.Data --startup-project ArtistConnect.WebApi --output-dir Migrations
```

Os exemplos de chamadas CRUD e o cabeçalho de autorização estão em
`ArtistConnect.WebApi.http`.
