Biblioteca de integração PagSeguro em .NET
==========================================
---
Descrição
---------
---
A biblioteca PagSeguro em .NET é um conjunto de classes de domínio que facilitam, para o desenvolvedor .NET, a utilização das funcionalidades que o PagSeguro oferece na forma de APIs. Com a biblioteca instalada e configurada, você pode facilmente integrar funcionalidades como:

 - Criar [requisições de pagamentos]
 - Consultar [transações por código]
 - Consultar [transações por intervalo de datas]
 - Consultar [transações abandonadas]
 - Receber [notificações]


Requisitos
----------
---
 - [.NET Framework] 2.0+


Instalação
----------
---
 - Baixe o repositório como arquivo zip ou faça um clone;
 - Descompacte os arquivos em seu computador;
 - Dentro do diretório *source* existem dois diretórios, o *Uol.PagSeguro* e o *Examples*. O diretório *Examples* contém exemplos de chamadas utilizando a API e o diretório *Uol.PagSeguro* contém a biblioteca propriamente dita;
 - Inclua o projeto Uol.PagSeguro.csproj dentro de sua solução;
 - Adicione uma referência ao projeto Uol.PagSeguro.csproj em seu projeto.

Configuração
------------
---
Visando garantir a segurança dos seus dados no PagSeguro, é necessário que você informe suas credenciais de acesso ao executar funções da biblioteca que realizam chamadas via API. As credenciais de acesso são formadas pelo e-mail de cadastro no PagSeguro e um token, que funciona como uma senha.

As chamadas via API exigem que você passe uma instância da classe AccountCredentials que é responsável por encapsular suas credenciais.

Mais informações estão disponíveis na [documentação oficial].


Changelog
---------
---
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


[Dúvidas?]
----------
---
Em caso de dúvidas mande um e-mail para desenvolvedores@pagseguro.com.br


Contribuições
-------------
---
Achou e corrigiu um bug ou tem alguma feature em mente e deseja contribuir?

* Faça um fork.
* Adicione sua feature ou correção de bug.
* Envie um pull request no [GitHub].


  [requisições de pagamentos]: https://pagseguro.uol.com.br/v2/guia-de-integracao/api-de-pagamentos.html
  [notificações]: https://pagseguro.uol.com.br/v2/guia-de-integracao/api-de-notificacoes.html
  [transações por código]: https://pagseguro.uol.com.br/v2/guia-de-integracao/consulta-de-transacoes-por-codigo.html
  [transações por intervalo de datas]: https://pagseguro.uol.com.br/v2/guia-de-integracao/consulta-de-transacoes-por-intervalo-de-datas.html
  [transações abandonadas]: https://pagseguro.uol.com.br/v2/guia-de-integracao/consulta-de-transacoes-abandonadas.html
  [Dúvidas?]: https://pagseguro.uol.com.br/desenvolvedor/comunidade.jhtml
  [.NET Framework]: http://www.microsoft.com/net
  [GitHub]: https://github.com/pagseguro/netframework
  [documentação oficial]: https://pagseguro.uol.com.br/v2/guia-de-integracao/documentacao-da-biblioteca-pagseguro-netframework.html
