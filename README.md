# Segurança de API com KeyCloak

![banner](./docs/img/banner.png)

- Criação do Realm

- Menu: Client
  Criar o Client \* Deixar o "Client Authentication" Marcado

- Menu: Client scopes
  Criar um novo scope \* Deixar o "Include in token scope" marcado

  - Mappers - Configure new mapper
  - Selecionar o "Audience" e depois coloque um nome

- Voltar na tela do "Clients" depois clicar na aba "Client scopes" e depois no botão "Add client scope"

- Menu User
  Criar um novo User
  Na aba Credentials, criar uma senha. Não deixar marcado a aba Temporary

```curl
curl --location --request POST 'http://localhost:8080/realms/<REALM_NAME>/protocol/openid-connect/token' --header 'Content-Type: application/x-www-form-urlencoded' --data-urlencode 'grant_type=password' --data-urlencode 'client_id=' --data-urlencode 'client_secret=' --data-urlencode 'username=' --data-urlencode 'password='
```

```javascript
var jsonData = JSON.parse(responseBody);
postman.setEnvironmentVariable('token', jsonData.access_token);
```
