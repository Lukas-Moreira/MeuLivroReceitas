**Visão Geral do Backend**

Este diretório contém a implementação do backend da aplicação MeuLivroReceitas. O backend é composto por projetos separados seguindo a arquitetura em camadas: Application, Domain, Infrastructure e a API. A separação busca manter regras de negócio no domínio, casos de uso na camada de aplicação e persistência na infraestrutura.

**Objetivo**:
- Fornecer uma API REST que permita gerenciar usuários, receitas e outras entidades da aplicação.
- Isolar regras de negócio, persistência e comunicação para facilitar testes e manutenção.

**Estrutura (resumo por projeto)**
- **`MyRecipeBook.Application`**: Camada de aplicação e casos de uso.
  - Contém implementações de serviços, mapeamentos e os UseCases (ex.: pasta `UseCases/User`).
  - Arquivo importante: `DependecyInjectionExtension.cs` — centraliza registro de serviços e casos de uso para injeção de dependência.

- **`MyRecipeBook.Domain`**: Entidades e contratos do domínio.
  - Pastas: `Entities`, `Enums`, `Repositories`.
  - Entidades centrais: `User.cs`, `EntitieBase.cs`.
  - Contratos (interfaces) de repositório e `IUnitOnWork.cs` para abstração de unidade de trabalho.

- **`MyRecipeBook.Infrastructure`**: Implementações de persistência e integrações externas.
  - Implementa `UnitOnWork` em `DataAcess/UnitOnWork.cs` e o `DbContext` em `DataAcess/MyRecipeBookDbContext.cs`.
  - `Repositories/` contém implementações concretas dos repositórios definidos no domínio.
  - `DependecyInjectionExtension.cs` — registra implementações de repositório, unit of work e configurações de infraestrutura no contêiner.
  - Usado pelos projetos da API para conectar a camada de domínio às dependências concretas.

- **`MyRecipeBook.API`** : Projeto que expõe a API.
  - Arquivo chave: `Program.cs` — configuração de host, serviços e pipeline HTTP.
  - `Controllers/` — controladores REST (ex.: `UserController.cs`).
  - `Filters/ExceptionFilter.cs` — filtro global para tratamento de exceções.
  - `Middleware/CultureMiddleware.cs` — middleware para cultura/idioma.
  - Arquivos: `appsettings.json`, `appsettings.Development.json` — configurações de ambiente.

- **`Shared` (fora do backend, porém com implementações compartilhadas)**:
  - `MyRecipeBook.Communication` — modelos de requests/responses que são usados pela API e pela camada de application.
  - `MyRecipeBook.Exceptions` — recursos e mensagens de erro localizadas (ex.: `ResourceMessagesExceptions.pt-BR.resx`) e classes base de exceção.

**Descrição detalhada de cada parte e arquivos relevantes**

- **DependecyInjectionExtension.cs (Application & Infrastructure)**:
  - Objetivo: centralizar o registro de serviços no contêiner de DI (ex.: `IServiceCollection`).
  - Onde adicionar: ao adicionar novos UseCases, serviços ou repositórios, registre-os aqui para exposição na `Program.cs` da API.
  - Boas práticas: registrar tipos por interface, preferir `Scoped` para repositórios/UnitOfWork e `Singleton` somente para serviços sem estado.

- **UseCases (ex.: `UseCases/User/`)**:
  - Contêm a lógica de aplicação para casos de uso (ex.: criação de usuário, autenticação, atualização de perfil).
  - Devem depender apenas de contratos do domínio (`IRepository`, `IUnitOnWork`) e dos DTOs de comunicação.
  - Testabilidade: isolar regras e usar injeção de dependência para fornecer repositório falso em testes.

- **Services (ex.: `Services/Cryptography/`)**:
  - Implementações de utilitários como hash de senha, geração de tokens, etc.
  - Podem ser registradas no `DependecyInjectionExtension.cs` e consumidas pelos UseCases.

- **Mappings**:
  - Perfaz a conversão entre entidades de domínio e DTOs/Responses.
  - Manter consistência nos nomes e centralizar mapeamentos (ex.: AutoMapper ou mapeamentos manuais).

- **Domain Entities (`MyRecipeBook.Domain/Entities`)**:
  - Modelos do domínio com validações e regras invariantes (quando aplicável).
  - `EntitieBase.cs` costuma trazer propriedades comuns como `Id`, `CreatedAt`, `UpdatedAt`.

- **Repositories e `IUnitOnWork`**:
  - `Repositories/` na camada Domain contém interfaces (ex.: `IUserRepository`).
  - Implementações concretas ficam em `MyRecipeBook.Infrastructure/DataAcess/Repositories`.
  - `IUnitOnWork` define commit/rollback; `UnitOnWork` no Infrastructure implementa com `DbContext`.

- **MyRecipeBookDbContext**:
  - Configura `DbSet<T>` para entidades e mapeamentos (fluent API se houver).
  - Local onde configurar conventions, schema e se necessário aplicar seeds.

- **API: Controllers, Filters e Middleware**:
  - `Controllers/` expõem endpoints REST; devem ser finos, delegando lógica para UseCases/Services.
  - `ExceptionFilter` captura exceções não tratadas e transforma em respostas padronizadas.
  - `CultureMiddleware` aplica cultura ao contexto da requisição com base em cabeçalhos ou configurações.

**Variáveis e chaves de configuração**
- `appsettings.Development.json` contem `ConnectionStrings:DatabaseType`.

**Como adicionar um novo caso de uso (resumo de passos)**
- Criar a classe do UseCase na pasta apropriada em `MyRecipeBook.Application/UseCases/`.
- Definir/atualizar interfaces de repositório no `MyRecipeBook.Domain/Repositories` se necessário.
- Implementar repositório concreto em `MyRecipeBook.Infrastructure/DataAcess/Repositories`.
- Registrar o UseCase e/ou serviço no `DependecyInjectionExtension.cs` da camada Application (e implementações na Infrastructure).
- Adicionar endpoint no `Controllers/` da API que injete o UseCase via construtor e delegue a execução.

**Boas práticas e recomendações**
- Manter controllers finos: a lógica deve ficar nos UseCases/Services.
- Seguir modelo de exceptions centralizadas e mensagens localizadas (usar `MyRecipeBook.Exceptions`).
- Testar UseCases com repositórios fake/mocks; testar repositórios com banco em memória quando necessário.
- Preferir `async/await` em chamadas a repositório/DB.
- Registrar e centralizar políticas de retry/circuit-breaker se houver integrações externas.

**Padrões de nomenclatura**
- Classes de domínio: singular e representativas (`User`, `Recipe`).
- Repositórios: `I<User>Repository` e implementação `UserRepository`.
- DTOs/Responses: sufixos `Request/Response` (ex.: `CreateUserRequest`, `UserResponse`).

**Contribuindo / Checklist para PRs**
- Atualizar `DependecyInjectionExtension.cs` ao adicionar novos serviços.

**Arquivos de referência (onde procurar o que foi documentado)**
- `src\Backend\MyRecipeBook.Application\DependecyInjectionExtension.cs`
- `src\Backend\MyRecipeBook.Infrastructure\DependecyInjectionExtension.cs`
- `src\Backend\MyRecipeBook.Infrastructure\DataAcess\MyRecipeBookDbContext.cs`
- `src\Backend\MyRecipeBook.API\Program.cs`
- `src\Backend\MyRecipeBook.API\Controllers\UserController.cs`
- `src\Backend\MyRecipeBook.Domain\Entities\User.cs`
- `src\Backend\MyRecipeBook.Domain\Repositories\IUnitOnWork.cs`
---