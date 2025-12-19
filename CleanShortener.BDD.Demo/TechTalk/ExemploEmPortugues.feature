#language: pt-BR
Funcionalidade: Deleção de URLs encurtadas
	Sendo um usuário do sistema
	Eu quero verificar que o sistema deleta corretamente uma URL
	De forma que satisfaça os meus requisitos de negócio

Cenario: URL deletada do sistema não é mais acessível
	Sendo um usuário do sistema
	Eu quero deletar uma URL que eu criei
	De forma que tentativas de acessá-la após deleção não sejam bem-sucedidas.

	Dada a URL "www.google.com"
	Quando eu solicito uma URL encurtada
	Então eu recebo uma resposta contendo uma URL encurtada

	Quando eu solicito a deleção da URL encurtada
	Então eu recebo uma resposta indicando que o recurso foi corretamente deletado

	Dada a URL encurtada que criei anteriormente
	Quando eu tento acessar a URL original por meio da URL encurtada
	Então eu me deparo com um erro