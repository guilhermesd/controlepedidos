Feature: Cadastro de Cliente
  Como um usuário do sistema
  Eu quero cadastrar um novo cliente
  Para que o cliente seja salvo e eu possa recuperá-lo pelo CPF

  Scenario: Cadastrar cliente com sucesso
    Given que um cliente válido foi informado
    When o cliente for cadastrado
    Then o cliente deve ser retornado com status 201