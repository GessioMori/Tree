# TreeTraversal

**Árvore de componentes para impressão, monitoramento e persistência, com design extensível.**

---

## Visão Geral

Este projeto demonstra a implementação de:

- Estruturas de árvore usando *composite pattern*
- Estratégias de impressão desacopladas (*print strategy pattern*)
- Monitoramento de execução (*observer pattern*)
- Diferentes percursos (*pre-order, depth-first, breadth-first*)
- Serialização JSON com *repository pattern*
- Mapeamento de DTOs para exportação/importação
- Projeto de testes automatizados (xUnit)
- Pronto para execução via Docker

---

## Estrutura de projetos

- **TreeTraversal.Core**  
  Contém o modelo de domínio, estratégias de impressão, monitoramento e percursos.

- **TreeTraversal.Tests**  
  Contém os testes automatizados para as regras de negócio.

- **Tree.Persistence**  
  Responsável pela serialização JSON, DTOs e repositórios.

- **Tree.Console**  
  Aplicação console para executar os exemplos e ler arquivos persistidos.

---

## Recursos

- Árvores heterogêneas (nós e folhas)  
- Impressão extensível via `IPrintStrategy`  
- Monitoramento desacoplado via `IMonitor`  
- Percursos variados (pre-order, depth-first, breadth-first)  
- Builder fluido para montagem de árvores  
- Serialização / desserialização JSON  
- Suporte a Docker com volumes  
- Argumentos flexíveis para indicar diretório e modo sem bloqueio

---

## Executando

### Com Docker Compose

```bash
docker compose up --build
````

* Cria o volume `tree_storage` automaticamente (se não existir)
* Monta o volume em `/app/storage` dentro do container
* Passa o argumento `--nowait` para evitar bloqueio com `Console.ReadKey()`
* Todos arquivos JSON persistem no volume entre execuções

### Com Docker manual

```bash
docker build -t treeconsole .
docker run --rm -v tree_storage:/app/storage treeconsole --nowait
```

Você pode adicionar argumentos extras, por exemplo caminho do storage:

```bash
docker run --rm -v tree_storage:/app/storage treeconsole "/app/storage" --nowait
```

---

## Execução em ambiente de desenvolvimento (Visual Studio)

* Sem argumentos → usa o caminho padrão `/app/storage`
* Sem `--nowait` → aguarda pressionar uma tecla após a execução, facilitando o debug

Para passar argumentos no debug, configure o `launchSettings.json`:

```json
{
  "profiles": {
    "Tree.Console": {
      "commandName": "Project",
      "commandLineArgs": "--nowait"
    }
  }
}
```

---

## Como funcionam os volumes

* O volume `tree_storage` armazena os arquivos JSON
* Mesmo ao destruir o container, o volume persiste
* Para inspecionar manualmente:

  ```bash
  docker run --rm -it -v tree_storage:/app/storage alpine sh
  ```

  dentro do shell, navegue para `/app/storage`.

---

## Persistência

Para salvar e restaurar a árvore de forma programática:

```csharp
ITreeRepository repository = new JsonTreeRepository();
await repository.SaveAsync(tree, "tree.json");

var restored = await repository.LoadAsync("tree.json");
```

---

## Testes

Execute todos os testes:

```bash
dotnet test
```
