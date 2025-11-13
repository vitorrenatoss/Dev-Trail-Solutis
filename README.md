tive um erro um pouco antes da aula de hoje (13/11) começar.
deu um erro (imagem 'erro_terminal' na pasta banksystem). 
tentei aplicar os comandos de dotnet ef migrations após mexer nas camadas das classes conta e cliente.
por isso, não apliquei ainda as alterações relacionadas a testes no projeto banksystem

pendências no bankSytem:
* terminar Crud cliente (falta atualizar e deletar com regras de negócio)
* inserir a propriedade Transação na classe Conta
* Criar classe Transação com enum TipoOperracao {Depósito, Saque, Pagamento (ou pix), TransferênciaEnviada, TRansferência resolvida}  // ou só transferência, se não quiser exibir contas origem/destino no extrato
* Criar/atualizar métodos de saque, depósito, pagamento, transferência (talvez criar um menu pra selecionar a operação e depois dá-lhe
* Criar método 'Extrato' (se for usar os dois tipos diferentes de transação no enum, colocar conta origem/destino. Provavelmente usando um if pra verificar o tipo do enum e modificar o print no loop de exibir/ a adição de linhas na string de resultado)
