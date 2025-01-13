# Segurança de API com KeyCloak

![banner](./docs/img/banner.png)

> [!NOTE]
> Docker
>
> Docker compose
>
> .NET 9 SDK

> [!WARNING]
> testado


> [![SonarQube Cloud](https://sonarcloud.io/images/project_badges/sonarcloud-light.svg)](https://sonarcloud.io/summary/new_code?id=felipementel_PoC.KeyCloak.v1)

## Passo a passo

### 1 Criação do Realm

### 2 Menu: Clients

    2.1 - General Settings - Preencher o campo "Client ID"

    2.2 - Capability config = habilitar flag "Client Authentication"

    2.3 - Login settings = n/a

  SAVE

- Menu: Client scopes
    Create client scope
      1 - Preencher o campo *Name* e Deixar o ***Include in token scope*** marcado
    SAVE
  - Mappers - Configure a new mapper
      1 - Selecionar o "Audience" e depois coloque um nome

-  Menu: Clients
  - Selecione o usuário recem criado.
  Clicar na aba "Client scopes" e depois no botão "Add client scope", selecionar o scope criado, e depois clique em Add / Default

- Menu User

  - Create new user
    1 - Preencher o campo "Username"
    2 - Clicar no user criado, aba Credentials, criar uma senha. Não deixar marcado a aba Temporary


# Teste via cURL

[!NOTE]
> Para obter o client_id e client_secret, acesse o menu Clients, selecione o client e copie o client_id da aba de Settings e client_secret da aba Credentials

```curl
curl --location --request POST 'http://localhost:8087/realms/<REALM_NAME>/protocol/openid-connect/token' --header 'Content-Type: application/x-www-form-urlencoded' --data-urlencode 'grant_type=password' --data-urlencode 'client_id=' --data-urlencode 'client_secret=' --data-urlencode 'username=' --data-urlencode 'password='
```

```curl
curl --location --request POST 'http://localhost:8087/realms/Canal-DEPLOY/protocol/openid-connect/token' --header 'Content-Type: application/x-www-form-urlencoded' --data-urlencode 'grant_type=password' --data-urlencode 'client_id=client-api' --data-urlencode 'client_secret=f1iW2gAjZJs7aIYZdexebxwzUHO1NbDZ' --data-urlencode 'username=user-api' --data-urlencode 'password=abcd1234'
```

curl --location --request POST 'http://localhost:8087/realms/CanalDEPLOY/protocol/openid-connect/token' --header 'Content-Type: application/x-www-form-urlencoded' --data-urlencode 'grant_type=password' --data-urlencode 'client_id=system-1' --data-urlencode 'client_secret=' --data-urlencode 'username=felipementel' --data-urlencode 'password='

```javascript
var jsonData = JSON.parse(responseBody);
postman.setEnvironmentVariable('token', jsonData.access_token);
```

# Docker

### imagens

````
https://hub.docker.com/_/postgres/
````

````
https://www.pgadmin.org/download/pgadmin-4-container/
````

````
https://hub.docker.com/r/keycloak/keycloak
````


# Docs

- [Dotnet Ports](https://learn.microsoft.com/en-us/dotnet/core/compatibility/containers/8.0/aspnet-port)

- https://www.keycloak.org/server/all-config

- https://www.pgadmin.org/docs/pgadmin4/development/container_deployment.html

# Fork

Create a secrets

SONAR_TOKEN
SONAR_ORGANIZATION
SONAR_PROJECT_KEY
SONAR_PROJECT_NAME

GITLEAKS_LICENSE
