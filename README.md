# Teste Técnico - KPMG

Este projeto foi desenvolvido para atender a solicitação de uma empresa que opera vários servidores de jogos, onde cada jogo resulta em ganho ou perda de pontos para o Jogador. 

O projeto consiste na construção de uma API, onde contém dois endpoins, o primeiro está relacionado a persistência dos dados, em que foi feito o processo para armazenar os dados na memória do servidor e periodicamente são migrados para o Banco de Dados. O segundo endpoint poderá ser utilizado pelo website para obter a classificação dos 100 melhores jogares, conforme a classficação de pontos. 

# Pré-Requisitos

Para executar a API é necessário ter um banco de dados SQL SERVER armazenado Localmente. O arquivo de configuração para criação do banco de dados está disponivel em config/CREATE_DATABASE.sql;

# Estrutura do Projeto

## DDD
A Estrutura do projeto está seguindo a modelagem DDD (design orientado a domínio) que basicamente defende uma modelagem com base na regra de negócio da empresa, sendo estruturado da seguinte forma:

    Points.Domain
    Points.UnitTests
    Points.Infrastructure
    Points.API
    Points.BackgroundTasks

Utilizou-se dessa modelagem para que o código do projeto fique mais organizado e alinhado com a realidade do negócio. Além de facilmente podemos implementar como um microserviço.

## CQRS

O padrão CQRS foi utilizado para garantir um controle avançado sobre a leitura e a escrita dos dados. Além de ser uma ótima opção para domínios em que vários usuários acessam os mesmos dados em paralelo, o CQRS ajudar a separar o desempenho das consultas com o desempenho das gravações, especialmente em um cenário que o número de leituras é muito maior que o número de gravações.


## Tecnologias

### .NET Core

Para desenvolvimento da solução utilizou-se a tencologia Net Core, por fornecer um suporte multiplataforma, além de garantir uma alta performance no tratamento de dados via API.

### EntityFrameWork Core

Biblioteca utilizada para integração com o banco de dados. 

### IHostedService 

No cenário em que os dados deveram ser migrados periodicamente para o banco, pensou em utiliza-se a interface IHostedService, que permite que tarefas sejam executadas em segundo plano como um microserviço. 

Sempre que o EndPoint /GameResult for chamado será registrado um arquivo txt contendo o JSON da requisição na mémoria do servidor e em tempos o serviço contido em Points.BackgroundTasks será chamado para obter os dados desses arquivos e adiciona no banco de uma só vez.

Para configurar a periodicidade do serviço basta acessar o arquivo de configuração da API /appsettings.json e atualizar o parâmetro CheckUpdateTimeService incluido a quantidade de milisegundos necessário para o tempo de atualização. 

### BD SQL SERVER

UTilizou do banco de dados SQL Server por garantir um alto desempenho no tratamento de dados, além de fornece um ótimo suporte para recuperação de dados em caso de dados corrompidos.


# Teste da API

Para realizar testes na API é possivel executar através do arquivo /build/Points.API.exe ou executar o projeto em si.


## EndPoint 1 /GameResult

O teste pode ser realiado através da ferramenta Postamn adicionando o seguinte código:

curl --location --request POST 'https://localhost:5001/api/v1/GameResults' \
--header 'Content-Type: application/json' \
--data-raw '{
    "PlayerId":6,
    "GameId":3,
    "Win":-5,
    "TimeStamp":"2021-04-18T18:00:55.1565614-03:00"
}'


Após o envio da requisição será possível visualizar o chave identificadora do registro:
{
    "identityGuid": "9139749f-bc82-4b14-985a-846590896c35"
}

## EndPoint 2 / Leaderboard

curl --location --request GET 'https://localhost:5001/api/v1/Leaderboard'


