# Segurança de API com KeyCloak

![banner](./docs/img/banner.png)

> [!NOTE]
> Docker
>
> Docker compose
>
> .NET 9 SDK

> [!WARNING]
> [![SonarQube Cloud](https://sonarcloud.io/images/project_badges/sonarcloud-light.svg)](https://sonarcloud.io/summary/new_code?id=felipementel_PoC.KeyCloak.v1)

## Passo a passo

### 1. Criação do Realm
1. Crie um novo Realm no Keycloak.

### 2. Configuração de Clientes

#### Menu: **Clients**
1. Acesse o menu **Clients** e realize as configurações abaixo:
   - **General Settings**: Preencha o campo **Client ID**.
   - **Capability Config**: Habilite a flag **Client Authentication**.
   - **Login Settings**: Não é necessário realizar configurações adicionais (**n/a**).

#### Menu: **Client Scopes**
1. Crie um novo **Client Scope**:
   - Preencha o campo **Name**.
   - Marque a opção **Include in token scope**.
   - Clique em **Save**.
2. Configure um novo Mapper:
   - Selecione o tipo **Audience**.
   - Preencha o campo **Name**.
   - No campo **Include Client Audience**, insira o nome do **client** criado no passo 2 (Menu: **Clients**).

#### Menu: **Clients**
1. Selecione o cliente recém-criado.
2. Vá até a aba **Client Scopes**:
   - Clique no botão **Add client scope**.
   - Selecione o scope criado anteriormente.
   - Clique em **Add / Default** para finalizar.

### 3. Configuração de Usuários

#### Menu: **Users**
1. Crie um novo usuário:
   - Preencha o campo **Username**, **Email** e **First Name** e **Last Name**.
2. Após criar o usuário:
   - Acesse a aba **Credentials**.
   - Defina uma senha para o usuário.
   - Certifique-se de **não marcar** a opção **Temporary**.

# Teste via cURL

> [!NOTE]
> Para obter o client_id e client_secret, acesse o menu Clients, selecione o client e copie o client_id da aba de Settings e client_secret da aba Credentials

```curl
curl --location --request POST 'http://localhost:8087/realms/<REALM_NAME>/protocol/openid-connect/token' --header 'Content-Type: application/x-www-form-urlencoded' --data-urlencode 'grant_type=password' --data-urlencode 'client_id=' --data-urlencode 'client_secret=' --data-urlencode 'username=' --data-urlencode 'password='
```

```curl
curl --location --request POST 'http://localhost:8087/realms/Canal-DEPLOY/protocol/openid-connect/token' --header 'Content-Type: application/x-www-form-urlencoded' --data-urlencode 'grant_type=password' --data-urlencode 'client_id=client-api' --data-urlencode 'client_secret=' --data-urlencode 'username=user-api' --data-urlencode 'password='
```

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

### Create a secrets

> SONAR_TOKEN
> 
> SONAR_ORGANIZATION
> 
> SONAR_PROJECT_KEY
> 
> SONAR_PROJECT_NAME
> 
> GITLEAKS_LICENSE
> 
> SNYK_TOKEN
