#language: pt-BR
Funcionalidade: Encurtar uma url
	Como engenheiro
	Eu quero mostrar que Reqnroll também suporta outras linguagens
	A fim de facilitar o processo de desenvolvimento para times de qualquer nacionalidade. 

Cenario: Uma url long é encurtada
	Dada a url "https://www.google.com/search?q=how+to+bdd"
	Quando eu solicito uma url encurtada
	Então eu recebo uma url encurtada de volta