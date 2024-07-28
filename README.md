# Documentação de Arquitetura do Projeto de Gestão de Tarefas

<a name="introducao"></a>
## 1. Introdução

Esta documentação descreve a arquitetura e as decisões de design para o projeto de gestão de tarefas desenvolvido com ASP.NET MVC. O objetivo deste documento é fornecer uma visão clara da estrutura do sistema, justificando as escolhas feitas durante o desenvolvimento.

<a name="objetivos"></a>
## 2. Objetivos

- Desenvolver uma aplicação web para gerenciar tarefas.
- Permitir a criação, edição e exclusão de tarefas.
- Fornecer uma interface amigável para os usuários.

<a name="visao-geral-da-arquitetura"></a>
## 3. Visão Geral da Arquitetura

O projeto segue o padrão MVC (Model-View-Controller), que organiza a aplicação em três componentes principais:

- **Modelos (Models):** Representam os dados da aplicação.
- **Visões (Views):** Responsáveis pela apresentação dos dados.
- **Controladores (Controllers):** Gerenciam a lógica de negócios e o fluxo da aplicação.

### Diagrama de Arquitetura

```plaintext
+-------------------+      +--------------------+      +---------------------+
|                   |      |                    |      |                     |
|       View        | <--> |     Controller     | <--> |     TaskService     |
|                   |      |                    |      |                     |
+-------------------+      +--------------------+      +---------------------+
                                                        |
                                                        |
                                                        v
+-------------------+      +--------------------+      +---------------------+
|                   |      |                    |      |                     |
|     TaskViewModel | <--> | TaskRepository     | <--> |   (Lista de Tarefas)|
|                   |      |                    |      |                     |
+-------------------+      +--------------------+      +---------------------+

```

<a name="decisoes-de-design"></a>
## 4. Decisões de Design

- **Padrão MVC:** Escolhido para separar as preocupações e facilitar a manutenção.
- **Serviços (Services):** Implementados para encapsular a lógica de negócios e interagir com o repositório de dados.
- **Repositórios (Repositories):** Utilizados para abstrair o acesso aos dados.

<a name="componentes-principais"></a>
## 5. Componentes Principais

### Controladores (Controllers)

- `TaskController`: Gerencia as operações de CRUD para as tarefas.

### Serviços (Services)

- `TaskService`: Implementa a lógica de negócios para gerenciar tarefas.

### Repositórios (Repositories)

- `TaskRepository`: Implementa a persistência dos dados das tarefas em uma lista em memória.

### Modelos (Models)

- `TaskViewModel`: Representa os dados de uma tarefa.

<a name="detalhes-de-implementacao"></a>
## 6. Detalhes de Implementação

### TaskController

Responsável por gerenciar as operações de CRUD para as tarefas.

```csharp
public class TaskController : Controller
{
    private readonly ITaskService _serviceTask;

    public TaskController(ITaskService serviceTask)
    {
        _serviceTask = serviceTask;
    }

    public async Task<IActionResult> Index()
    {
        var tasks = await _serviceTask.GetTasks();
        return View(tasks);
    }

    // Outras ações: Create, Edit, Delete
}
```

### TaskService

Implementa a lógica de negócios para gerenciar tarefas.

```csharp
public class TaskService : ITaskService
{
    private readonly ITaskRepository _repository;

    public TaskService(ITaskRepository repository)
    {
        _repository = repository;
    }

    // Métodos para Create, Read, Update, Delete
}
```

### TaskRepository

Implementa a persistência dos dados das tarefas em uma lista em memória.

```csharp
public class TaskRepository : ITaskRepository
{
    private readonly List<TaskViewModel> _tasks = new List<TaskViewModel>();
    private int _nextId = 1;

    // Métodos para GetTaskByIdAsync, AddTask, RemoveTask, UpdateTask, ListTasks
}
```

<a name="seguranca"></a>
## 7. Segurança

- **Tratamento de Exceções:** Cada ação do controlador possui tratamento de exceções para exibir uma página de erro amigável.
- **Validação de Dados:** Utilização do `ModelState.IsValid` para verificar a validade dos dados antes de processar as operações de CRUD.

<a name="consideracoes-finais"></a>
## 8. Considerações Finais

Esta documentação fornece uma visão geral da arquitetura do projeto de gestão de tarefas, explicando os componentes principais e as decisões de design. Futuras melhorias podem incluir a implementação de um banco de dados persistente e a adição de funcionalidades avançadas, como autenticação de usuários e notificações.
