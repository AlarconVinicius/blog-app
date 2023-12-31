<hr>

# Projeto Blog de Receitas - Documentação

O projeto Blog de Receitas consiste em uma aplicação full stack para um blog de receitas. É dividido em dois principais componentes: o Projeto de API e o Projeto SPA, e ambos são containerizados usando Docker.

## Tecnologias Utilizadas

- **.NET 7:** Utilizado para o desenvolvimento da API.
- **Angular 15:** Framework utilizado para a criação do SPA.
- **SQLServer:** Banco de dados utilizado na aplicação.
- **Docker:** Utilizado para containerizar os diferentes componentes da aplicação.

## Pré-requisito

Antes de iniciar, é necessário ter o Docker Desktop instalado no seu sistema. Você pode baixá-lo [aqui](https://www.docker.com/products/docker-desktop/).

## Passo a Passo para Executar a Aplicação

1. **Clone o Repositório:**
   Abra o terminal (Git Bash) no diretório desejado e execute o seguinte comando:

```bash
git clone https://github.com/AlarconVinicius/blog-app.git
```

2. **Acesse os Diretórios:**
   Vá para o diretório onde o repositório foi clonado e acesse a pasta src:

```bash 
cd blog-app/src/
```

3. **Configure os Arquivos de Configuração:**
   Faça cópias dos arquivos de configuração de desenvolvimento:

* No diretório **/Services/Blog/1-Api/Api/**, copie **appsettings.Development.json** para **appsettings.json**
* No diretório **/Web/WebSPA/Blog/src/environments/**, copie **environment.development** para **environment.prod**

```bash 
cd Services/Blog/1-Api/Api/
cp appsettings.Development.json appsettings.json
cd ../../../../
cd Web/WebSPA/Blog/src/environments/
cp environment.development environment.prod
cd ../../../../../../
```
> ***Antes de prosseguir, é recomendável verificar e ajustar as configurações dos arquivos recém-criados (appsettings.json e environment.prod) para garantir que as chaves e configurações estejam corretas para o ambiente de produção. É crucial assegurar que todas as variáveis e configurações necessárias estejam presentes e funcionem conforme esperado. Certifique-se de revisar e modificar esses arquivos, se necessário, para refletir as configurações adequadas ao ambiente de produção.***

3. **Execute os Contêineres Docker:**
   Utilize o Docker Compose para subir os contêineres:

```bash 
docker compose -f "docker-compose.yml" -p blog-app up -d --build
```

## Acesso à Aplicação

Após executar os contêineres, você pode acessar as diferentes partes da aplicação:

* **Banco de Dados:** Está na porta 8080 e pode ser acessado usando as seguintes credenciais:

  * **Server Name:** localhost\8080
  * **Login:** SA
  * **Senha:** Senha@123

* **API:** Está na porta 8081 e sua documentação pode ser acessada através do Swagger:
  * **Acesse em:** [http://localhost:8081/swagger/index.html](http://localhost:8081/swagger/index.html)

* **SPA:** Está na porta 8082 e pode ser acessada através do navegador:

  * **Acesse em:** [http://localhost:8082](http://localhost:8081/swagger/index.html)

### Acesso à Área Administrativa

Para acessar a área administrativa, utilize as seguintes credenciais:

- **Email:** blog@admin.com
- **Senha:** Admin@123

## Links para Documentação

* **Documentação da API:** [GitHub - Blog API](https://github.com/AlarconVinicius/blog-app/tree/main/src/Services/Blog)
* **Documentação do SPA:** [GitHub - Blog SPA](https://github.com/AlarconVinicius/blog-app/tree/main/src/Web/WebSPA/Blog)

## Informações de Contato

- **Portfólio**: [Link para o seu Portfólio](https://github.com/AlarconVinicius/)
- **LinkedIn**: [Link para o seu LinkedIn](https://www.linkedin.com/in/vin%C3%ADcius-alarcon-52a8a820a/)

<hr>