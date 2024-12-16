# Apresentação: Conceitos Avançados de Arquitetura de Software

## 1. Persistência e Event Sourcing

### Definição de Persistência

A persistência refere-se ao armazenamento durável de dados em sistemas, garantindo que informações importantes sejam mantidas além da duração do processo ou aplicação.

- **Persistência Tradicional:** Dados armazenados em tabelas relacionais com atributos representando o estado atual.
- **Event Sourcing:** Em vez de salvar diretamente o estado, armazena eventos que descrevem mudanças no sistema.

#### Exemplo Comparativo:

- **Modelo Tradicional:**

  ```
  Tabela Conta:
  +---------+-------+
  | ContaID | Saldo |
  +---------+-------+
  |   123   |  50   |
  +---------+-------+
  ```

  - O estado é alterado diretamente (e.g., de 50 para 100 após um depósito).

- **Event Sourcing:**

  ```
  Eventos:
  1. Depósito de 50.
  2. Saque de 20.
  3. Depósito de 70.

  Saldo é reconstituído como 100 ao reproduzir os eventos.
  ```

#### Benefícios do Event Sourcing

- **Histórico completo:** Rastreia todas as mudanças ocorridas no sistema.
- **Recuperação de erros:** É possível corrigir erros reproduzindo eventos corretos.
- **Escalabilidade:** Processamento de eventos em fluxos distribuídos.

---

## 2. CQRS (Command Query Responsibility Segregation)

### O que é CQRS?

- Padrão arquitetural que separa responsabilidades de **comandos** (operações de escrita) e **consultas** (operações de leitura).
- Cada lado pode ter seus próprios modelos e banco de dados otimizado.

#### Estrutura do CQRS:

- **Command Model:** Focado em alterar o estado do sistema.
- **Query Model:** Focado em buscar dados e responder rapidamente a consultas.

#### Exemplo Prático:

- **Comando:**
  ```java
  class CriarPedido {
      private String clienteId;
      private List<Item> itens;
      // Lógica de validação e salvamento
  }
  ```
- **Consulta:**
  ```sql
  SELECT * FROM Pedidos WHERE status = 'PENDENTE';
  ```

#### Benefícios:

- **Escalabilidade:** Leitura e escrita podem ser escaladas separadamente.
- **Simplicidade:** Modelos dedicados tornam cada operação mais simples.
- **Otimização:** Banco de dados de leitura pode ser otimizado para consultas.

#### Considerações:

- Pode ser mais complexo de implementar.
- Requer sincronização entre os modelos de leitura e escrita.

---

## 3. Integração de Contextos Delimitados

### O que são Contextos Delimitados (Bounded Contexts)?

No **Domain-Driven Design (DDD)**, um contexto delimitado é uma área específica do modelo de negócios que possui:

- **Vocabulário próprio:** Cada equipe ou área tem seu próprio "idioma."
- **Responsabilidade clara:** Limitações e interfaces bem definidas.

### Integração de Contextos

#### Estratégias de Integração:

1. **Mensagens/Eventos:** Contextos comunicam-se através de eventos publicados em um barramento.

   - Exemplo:
     - Contexto 'Vendas' publica `PedidoCriado`.
     - Contexto 'Estoque' consome esse evento para reservar itens.

2. **APIs Rest:** Um contexto expõe funcionalidades específicas que outros podem consumir.

   - Exemplo:
     - Contexto 'Pagamentos' expõe `POST /api/pagamentos` para processar cobranças.

3. **Compartilhamento de Dados:** Um banco de dados compartilhado entre contextos (menos recomendado).

#### Benefícios:

- **Desacoplamento:** Cada contexto pode evoluir de forma independente.
- **Resiliência:** Falhas em um contexto não afetam diretamente outros.

#### Desafios:

- **Complexidade:** Requer sincronização e monitoramento de eventos.
- **Latência:** Integração pode introduzir atraso em operações.

---

## Conclusão

- **Persistência e Event Sourcing:** Fornece histórico completo do sistema.
- **CQRS:** Permite escalabilidade e separação de preocupações.
- **Integração de Contextos Delimitados:** Melhora a modularidade e o desacoplamento.

Esses padrões combinados ajudam a criar sistemas escaláveis, resilientes e alinhados às necessidades de negócio.

