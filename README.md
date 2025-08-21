### Configurando o âmbiente
📋 Requisitos
- Microsoft Sql Server ou MySql intalado

⚙️Configurando projeto
- Navegue ate o arquivo Ecommerce/Ecommerce.Api/appsettings.json e defina o "Provider" de acordo com o banco escolhido
```json
{
  "Database": {
    "Provider": "MySql" ou "SqlServer"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}

```
- Na pasta Ecommerce/Ecommerce.Api crie a pasta Secrets e adicione o arquivo secrets.json
- Dentro do secrets.json cole a seguinte estrutura
```json
{
  "SqlServer": {
    "DataSource": "127.0.0.1, 1433",  // endereço do banco
    "User": "usuario-do-banco",
    "Password": "senha-do-banco",
    "DatabaseName": "nome-do-banco" // Nome da base de dadod ex. Ecommerce
  },
  "MySql": {
    "Server": "127.0.0.1", // endereço do banco
    "User": "usuario-do-banco",
    "Password": "senha-do-banco",
    "DatabaseName": "nome-do-banco" // Nome da base de dadod ex. Ecommerce
  },
  "Jwt": {
    "Issuer": "Ecommerce.Api",
    "Audience": "Ecommerce.Client",
    "Key": "chave-privada-para-criptografia",
    "ExpiresMinutes": 120 // tempo de duração do token
  }
}

```

### Script de Execução para Backend (.NET) e Frontend (Angular)
Este script automatiza a execução de uma aplicação full-stack composta por um backend em .NET e um frontend em Angular.
Obs.: Certifique-se de que o banco está em execução antes de rodar o script

📋 Requisitos
1. Sistema Operacional
- Linux (Ubuntu/Debian recomendados)
- macOS
- Windows (com WSL - Windows Subsystem for Linux recomendado)

2. Backend (.NET)
- .NET SDK 6.0 ou superior
- Verifique a instalação:
  - dotnet --version
3. Frontend (Angular)
- Node.js (versão 18 ou superior)
- npm (normalmente incluído com Node.js)
- Angular CLI (opcional, mas recomendado)
- Verifique as instalações:
```bash
node --version
npm --version
ng version  # se Angular CLI estiver instalado
```
4. Dependências do Projeto

🚀 Instalação das Dependências
Para Ubuntu/Debian:

```
# Instalar .NET SDK
wget https://dot.net/v1/dotnet-install.sh -O dotnet-install.sh
chmod +x ./dotnet-install.sh
./dotnet-install.sh --channel LTS

# Adicionar ao PATH (adicione ao seu ~/.bashrc ou ~/.zshrc)
export DOTNET_ROOT=$HOME/.dotnet
export PATH=$PATH:$HOME/.dotnet:$HOME/.dotnet/tools

# Instalar Node.js
curl -fsSL https://deb.nodesource.com/setup_18.x | sudo -E bash -
sudo apt-get install -y nodejs

# Verificar instalações
dotnet --version
node --version
npm --version
```
Para Windows:
- Instale o .NET SDK
- Instale o Node.js
- Opcional: Instale o Angular CLI globalmente:

```bash
npm install -g @angular/cli
```

🛠️ Como Usar
Clone o repositório do projeto
Navegue até o diretório raiz do projeto
Torne o script executável (Linux/macOS):
```bash
chmod +x backend-and-frontend-run.sh
```
Execute o script:
```bash
./backend-and-frontend-run.sh
```
⚙️ Comportamento do Script
- Verifica a existência dos comandos necessários (dotnet, npm)
- Limpa e compila a solução .NET em modo Release
- Executa o backend em segundo plano
- Navega para o diretório do frontend
- Instala as dependências npm (se necessário)
- Inicia o servidor de desenvolvimento do Angular

🔧 Solução de Problemas
Erro: "dotnet não encontrado"
- Verifique se o .NET SDK está instalado
- Confirme se o PATH está configurado corretamente

Erro: "npm não encontrado"
- Verifique se o Node.js está instalado
- Reinicie o terminal após a instalação

Erro: "Projeto não encontrado"
- Verifique se a estrutura de diretórios corresponde à esperada
- Ajuste o script se seus nomes de projeto forem diferentes

Portas em uso
- O backend .NET geralmente roda na porta 5000 ou 7000
- O frontend Angular geralmente roda na porta 4200
- Certifique-se de que estas portas estão livres

📝 Notas
- O script assume que você está executando a partir do diretório raiz do projeto
- O backend é executado em segundo plano
- Para parar a execução, use Ctrl+C e termine os processos manualmente se necessário

📄 Licença
- Este script é destinado a uso em ambientes de desenvolvimento.
