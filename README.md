# Documentos API

> Um microserviço para gerenciar documentos de clientes.

### Pré-Requisitos dotnet

É necessário ter o `.NET 5` SDK instalado em sua máquina, caso não tenha siga as instruções contidas no link abaixo:

* [.NET 5](https://dotnet.microsoft.com/download/dotnet/5.0)

### Pré-Requisitos localmente

Este projeto depende de algumas ferramentas para ser executado. O ideal é que se utilize `docker` para tornar estas depedências disponíveis de forma local, para isso o docker pode ser baixado através dos seguintes links: 

* [Windows](https://docs.docker.com/windows/started)
* [OS X](https://docs.docker.com/mac/started/)
* [Linux](https://docs.docker.com/linux/started/)

Após ter o docker instalado em sua máquina, será necessário levantar as depedências utilizando o `docker-compose`, para isso, utilize o código abaixo na raiz do repositório:

```shell
docker-compose up
```
### Pré-Requisitos AWS

Caso possua credenciais de uma infraestrutura AWS, é possível levantar as depedências utilizando o terraform como `infra-as-code` e para utilizá-lo são necessário algumas depedências. Para instalação siga as instruções contidas nos links abaixos:

* [AWS CLI](https://aws.amazon.com/cli/)
* [Terraform](https://www.terraform.io/downloads.html)

#### Configurando e Subindo infraestrutura na AWS

A aplicação tem como depedência alguns serviços que podem ser configurados na AWS, para fazer isso é necessário seguir os passos abaixos:

1. Precisamos ter as credenciais aws salvas, para criá-las utilize o link [AWS Credentials](https://aws.amazon.com/free/?trk=ps_a134p000003yHrnAAE&trkCampaign=acq_paid_search_brand&sc_channel=PS&sc_campaign=acquisition_MY&sc_publisher=Google&sc_category=Core&sc_country=MY&sc_geo=APAC&sc_outcome=acq&sc_detail=%2Baws%20%2Baccount&sc_content=Account_bmm&sc_segment=444351555789&sc_medium=ACQ-P|PS-GO|Brand|Desktop|SU|AWS|Core|MY|EN|Text&s_kwcid=AL!4422!3!444351555789!b!!g!!%2Baws%20%2Baccount&ef_id=Cj0KCQiAnb79BRDgARIsAOVbhRpTdbVEw6q2GhWhOsQS-AfLEUC04_VYhKfpHa28oKyBwdQf1J7fn2saAu1hEALw_wcB:G:s&s_kwcid=AL!4422!3!444351555789!b!!g!!%2Baws%20%2Baccount&all-free-tier.sort-by=item.additionalFields.SortRank&all-free-tier.sort-order=asc)
2. Para facilitar o deploy de sua aplicação, iremos criar um repositório na AWS, assim a imagem da aplicação pode ser usada para ser disponibilizada, para isso, execute o comando (para exemplo iremos utilizar o repositório chamado document_api):
```shell
aws ecr create-repository --repository-name document_api
```
3. O comando acima irá retornar uma url referente ao repositório criado, lembre-se ele é importante para os próximos passos. Agora iremos construir e subir a imagem do `dockerfile` da aplicação. Segue o comando:
```shell
aws ecr get-login-password | docker login --username AWS --password-stdin "{url-repo}"
docker build --rm --pull -f src/GS.Document.API/Dockerfile -t document_api .
docker tag document_api:latest {url-repo}:latest
docker push {url-repo}:latest
```
4. A imagem já está disponível para uso. Para conferir basta acessar o console da AWS. Agora iremos aplicar o `terraform`, primeiro vamos iniciá-lo:
```shell
terraform init infra
```
5. O comando acima fará a instalação das depedências do terraform para execução. Agora iremos executar o comando de planejamento, este comando valida os arquivos `.tf` afim de evitar erros na execução:
```shell
terraform plan infra
```
6. Agora só aplicar:
```shell
terraform apply -auto-approve infra
```
7. Pronto, ao fim da execução os serviços necessários já foram criados e estão disponíveis. Para testar, basta usar a url disponível no output da cli e chamar a rota swagger para testar a aplicação. Caso você queira reverter, segue o comando:
```shell
terraform destroy -auto-approve infra
```
### Estrutura do projeto

Este projeto é orientado ao DDD (Domain-Driven-Design), segue a sua estrutura e responsabilidades.

    ├── src                    
    │   ├── GS.Document.API               # Projeto web api, responsável por disponibilizar os endpoints
    │   ├── GS.Document.Application       # Camada de aplicação
    │   └── GS.Document.Domain            # Camada de domínio
    │   └── GS.Document.Infra.Database    # Camada de abstração da infraestrutura do banco de dados
    │   └── GS.Document.Infra.Kafka       # Camada de abstração da infraestrutura do kafka
    │   └── GS.Document.Infra.S3          # Camada de abstração da infraestrutura do Bucket S3
    ├── test                                 # Testes unitários
    │   ├── GS.Document.Domain.Tests         # Testes unitários da camada de domínio
    │   ├── GS.Document.Infra.Kafka.Tests    # Testes de integração para os serviços do Kafka
    │   └── GS.Document.Infra.S3.Tests       # Testes de integração para os serviços do Bucket S3



