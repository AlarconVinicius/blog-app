<hr>

# Projeto API Blog de Receitas - Documentação

O projeto Blog de Receitas é uma aplicação full stack para um blog de receitas. Esta documentação detalha as rotas disponíveis na API para manipular categorias, receitas, usuários e autenticação.

## Padrão de Resposta

A API segue um padrão de resposta comum para diferentes operações:

### Códigos de Resposta

* **200 (OK):**
```bash
{
  "success": true,
  "data": []
}
```

* **204 (No Content)**

* **400 (Bad Request):**
```bash
{
  "success": false,
   "errors": {
    "Mensagens": []
   }
}
```

## Rotas da API

### Auth
* **POST /api/v1/auth/create**
  * **Descrição:** Cria um usuário

* **POST /api/v1/auth/login**
  * **Descrição:** Faz login

### Category
* **POST /api/v1/categories**
  * **Descrição:** Adiciona uma categoria

* **GET /api/v1/categories**
  * **Descrição:** Retorna todas as categorias

* **GET /api/v1/categories/{id}**
  * **Descrição:** Retorna uma categoria

* **PUT /api/v1/categories/{id}**
  * **Descrição:** Atualiza uma categoria

* **DELETE /api/v1/categories/{id}**
  * **Descrição:** Deleta uma categoria

### Recipe
* **POST /api/v1/recipes**
  * **Descrição:** Adiciona uma receita

* **GET /api/v1/recipes ou /api/v1/recipes?userId={userId}**
  * **Descrição:** Retorna todas as receitas ou do usuário com o ID **correspondente**

* **GET /api/v1/recipes/{id} ou /api/v1/recipes/{id}?userId={userId}**
  * **Descrição:** Retorna uma receita pelo ID ou do usuário com o ID **correspondente**

* **GET /api/v1/recipes/url/{url} ou /api/v1/recipes/url/{url}?userId={userId}**
  * **Descrição:** Retorna uma receita pela URL ou do usuário com o ID **correspondente**

* **GET /api/v1/recipes/search/{search} ou /api/v1/recipes/search/{search}?userId={userId}**
  * **Descrição:** Retorna uma receita com base na busca ou do usuário com o ID **correspondente**

* **GET /api/v1/recipes/category/{category} ou /api/v1/recipes/category/{category}?userId={userId}**
  * **Descrição:** Retorna uma receita com base na categoria ou do usuário com o ID **correspondente**

* **PUT /api/v1/recipes/{id}**
  * **Descrição:** Atualiza uma receita

* **DELETE /api/v1/recipes/{id}**
  * **Descrição:** Deleta uma receita

### User
* **GET /api/v1/users**
  * **Descrição:** Retorna o usuário logado

* **PUT /api/v1/users**
  * **Descrição:** Atualiza o usuário logado

* **PUT /api/v1/users/change-password**
  * **Descrição:** Atualiza a senha do usuário logado

* **POST /api/v1/users/favorite-recipes/{recipeId}**
  * **Descrição:** Adiciona uma receita à lista de favoritas do usuário logado

* **GET /api/v1/users/favorite-recipes**
  * **Descrição:** Retorna todas as receitas favoritas do usuário logado

> ***Para mais detalhes sobre os parâmetros e estrutura das requisições, consulte a documentação interativa [Swagger](http://localhost:8081/swagger/index.html) após executar a aplicação seguindo o passo a passo da [documentação completa da aplicação](https://github.com/AlarconVinicius/blog-app).***
>
> ***Esta documentação detalha as rotas disponíveis, os métodos HTTP correspondentes e suas respectivas descrições na API do Projeto Blog de Receitas.***

## Links para Documentação

* **Documentação Completa:** [GitHub - Blog APP](https://github.com/AlarconVinicius/blog-app)
* **Documentação do SPA:** [GitHub - Blog SPA](https://github.com/AlarconVinicius/blog-app/tree/main/src/Web/WebSPA/Blog)

## Informações de Contato

- **Portfólio**: [Link para o seu Portfólio](https://github.com/AlarconVinicius/)
- **LinkedIn**: [Link para o seu LinkedIn](https://www.linkedin.com/in/vin%C3%ADcius-alarcon-52a8a820a/)

<hr>