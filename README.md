Biblioteca de integração PagSeguro em .NET
==========================================
---
Descrição
---------
---
A biblioteca PagSeguro em .NET é um conjunto de classes de domínio que facilitam, para o desenvolvedor .NET, a utilização das funcionalidades que o PagSeguro oferece na forma de APIs. Com a biblioteca instalada e configurada, você pode facilmente integrar funcionalidades como:


 - Criar [requisições de pagamentos]
 - Criar [requisições de assinaturas]
 - Cancelar [assinaturas]
 - Consultar [assinaturas]
 - Consultar [transações por código]
 - Consultar [transações por intervalo de datas]
 - Consultar [transações abandonadas]
 - Receber [notificações]


Requisitos
----------
---
 - [.NET Framework] 4.5+
 - [.NET Core]


Instalação
----------
---
 - Baixe o repositório como arquivo zip ou faça um clone;
 - Descompacte os arquivos em seu computador;
 - Dentro do diretório *source* existem dois diretórios, o *Uol.PagSeguro* e o *Examples*. O diretório *Examples* contém exemplos de chamadas utilizando a API e o diretório *Uol.PagSeguro* contém a biblioteca propriamente dita;
 
 [.Net 4.5+]
 - Inclua o projeto Uol.PagSeguro.csproj dentro de sua solução;
 - Adicione uma referência ao projeto Uol.PagSeguro.csproj em seu projeto.
 
 [.Net Core]
 - Copie o diretório *Uol.PagSeguro* para a pasta da sua solução;
 - Adicione *"Uol.PagSeguro": "1.0.0"* na lista de dependencias do *project.json* de seu projeto.

Configuração
------------
---
Visando garantir a segurança dos seus dados no PagSeguro, é necessário que você informe suas credenciais de acesso ao executar funções da biblioteca que realizam chamadas via API. As credenciais de acesso são formadas pelo e-mail de cadastro no PagSeguro e um token, que funciona como uma senha.

As chamadas via API exigem que você passe uma instância da classe AccountCredentials que é responsável por encapsular suas credenciais.

Mais informações estão disponíveis na [documentação oficial].


Dúvidas?
----------
---
Caso tenha dúvidas ou precise de suporte, acesse nosso [fórum].


Changelog
---------
---

2.5.1

- Possibilidade de definir parcelamento sem juros.

2.5.0

- Integração com serviço de consulta de Assinaturas (PreApproval) por código de notificação.

2.4.0

- Integração com serviço de modelo de aplicações.
- Possibilidade de definir descontos por meio de pagamento durante a requisição do código de checkout - Ver exemplo createPaymentRequest.php
- Ajustes em geral.

2.3.0

- Adicionado classes e métodos para utilização do Checkout Transparente.

2.2.0

- Integração com serviço de solicitação de estorno.
- Integração com serviço de solicitação de cancelamento.
- Integração com serviço de consulta de transações por código de referência.
- Integração com serviço de consulta de transações abandonadas.
- Ajustes em geral.
- Obs.: Algumas das funcionalidades descritas ainda não estão disponíveis comercialmente para todos os vendedores. Em caso de dúvidas acesse nosso [fórum].

2.1.1

- Ajustes diversos

2.1.0

- Implementação do environment Sandbox
- Validação da implementação de Assinaturas (PreApproval). https://github.com/pagseguro/dotnet/pull/4


2.0.6

 - Opção para retornar apenas o código de checkout no método Register.
 - Atualização do exemplo CreatePayment.

2.0.5

 - Correção no TransactionSearchResult.

2.0.4

 - Atualização dos códigos de meios de pagamento.

2.0.3

 - Atualização da arquitetura em diretorios.
 - Alterado método de envio para HTTP.
 - Adicionado possibilidade de envio de SenderCPF, MetaData e Parameter Generics.

2.0.0 - 2.0.2

 - Classes de domínios que representam pagamentos, notificações e transações.
 - Criação de checkouts via API.
 - Controller para processar notificações de pagamento enviadas pelo PagSeguro.
 - Módulo de consulta de transações.


Licença
-------
---
Copyright 2013 PagSeguro Internet LTDA.

Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file except in compliance with the License. You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software distributed under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the License for the specific language governing permissions and limitations under the License.


Notas
-----
---
 - O PagSeguro somente aceita pagamento utilizando a moeda Real brasileiro (BRL).
 - Certifique-se que o email e o token informados estejam relacionados a uma conta que possua o perfil de vendedor ou empresarial.


Contribuições
-------------
---
Achou e corrigiu um bug ou tem alguma feature em mente e deseja contribuir?

* Faça um fork.
* Adicione sua feature ou correção de bug.
* Envie um pull request no [GitHub].

  [requisições de assinaturas]: http://download.uol.com.br/pagseguro/docs/pagseguro-assinatura-automatica.pdf
  [assinaturas]: http://download.uol.com.br/pagseguro/docs/pagseguro-assinatura-automatica.pdf
  [requisições de pagamentos]: https://pagseguro.uol.com.br/v2/guia-de-integracao/api-de-pagamentos.html
  [notificações]: https://pagseguro.uol.com.br/v3/guia-de-integracao/api-de-notificacoes.html
  [Checkout Transparente]: https://pagseguro.uol.com.br/receba-pagamentos.jhtml#checkout-transparent
  [transações por código]: https://pagseguro.uol.com.br/v3/guia-de-integracao/consulta-de-transacoes-por-codigo.html
  [transações por intervalo de datas]: https://pagseguro.uol.com.br/v2/guia-de-integracao/consulta-de-transacoes-por-intervalo-de-datas.html
  [transações abandonadas]: https://pagseguro.uol.com.br/v2/guia-de-integracao/consulta-de-transacoes-abandonadas.html
  [fórum]: http://forum.pagseguro.uol.com.br/
  [.NET Framework]: http://www.microsoft.com/net
  [GitHub]: https://github.com/pagseguro/netframework
  [documentação oficial]: https://pagseguro.uol.com.br/v2/guia-de-integracao/documentacao-da-biblioteca-pagseguro-netframework.html