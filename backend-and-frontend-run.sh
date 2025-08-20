#!/bin/bash

# backend-and-frontend-run.sh
# Script para rodar backend (.NET) e frontend (Angular)

# -------------------------------
# FUNÇÃO PARA CHECAR COMANDO
# -------------------------------
command_exists() {
  command -v "$1" >/dev/null 2>&1
}

# -------------------------------
# BACKEND (.NET)
# -------------------------------

# Verifica se dotnet está instalado
if ! command_exists dotnet; then
  echo ".NET SDK não encontrado. Instale antes de continuar."
  exit 1
fi

# Procura a primeira solução .sln na pasta atual ou subpastas
solucao=$(find . -type f -name "*.sln" | head -n 1)

if [ -z "$solucao" ]; then
  echo "Nenhuma solução (.sln) encontrada."
  exit 1
fi

echo "Solução encontrada: $solucao"

# Limpa a solução
echo "Limpando a solução..."
dotnet clean "$solucao"

# Compila a solução em Release
echo "Compilando a solução (Release)..."
dotnet build "$solucao" -c Release

# Procura o projeto backend específico
projeto=$(find . -type f -name "Ecommerce.Api.csproj" | head -n 1)

if [ -z "$projeto" ]; then
  echo "Projeto backend (.csproj) não encontrado."
  exit 1
fi

echo "Projeto backend encontrado: $projeto"

# Roda o backend em segundo plano
echo "Rodando backend..."
dotnet run --project "$projeto" -c Release &

# -------------------------------
# FRONTEND (Angular)
# -------------------------------

# Verifica se npm está instalado
if ! command_exists npm; then
  echo "npm não encontrado. Instale Node.js e npm antes de continuar."
  exit 1
fi

# Procura a pasta Ecommerce.Client
frontend_dir=$(find . -type d -name "Ecommerce.Client" | head -n 1)

if [ -z "$frontend_dir" ]; then
  echo "Projeto frontend Angular não encontrado."
  exit 1
fi

echo "Projeto frontend encontrado em: $frontend_dir"

# Vai para o diretório do frontend
cd "$frontend_dir" || exit

# Instala dependências do npm
echo "Instalando dependências do frontend..."
npm install

# Roda Angular
echo "Rodando frontend Angular..."
npm start
