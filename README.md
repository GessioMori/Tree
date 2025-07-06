# TreeTraversal

**Árvore de componentes para impressão, monitoramento e persistência, com design extensível.**

---

## Visão Geral

Este projeto demonstra a implementação de:

- Estruturas de árvore usando *composite pattern*
- Várias estratégias de percurso (*pre-order, DFS, BFS*)
- Estratégias de impressão personalizadas (*print strategy pattern*)
- Persistência com serialização JSON via *repository pattern*
- Mapeamento de DTOs para facilitar exportação/importação
- Projeto de testes automatizados com xUnit
- Docker-ready

---

## Estrutura de projetos

- **TreeTraversal.Core**  
  Contém a estrutura de domínio, percursos, estratégias de impressão e monitoramento.

- **TreeTraversal.Tests**  
  Contém testes unitários com xUnit para validação dos percursos e monitoramentos.

- **Tree.Persistence**  
  Contém repositórios para serialização (JSON) e DTOs de transporte.

- **Tree.Console**  
  Exemplo de aplicação de console para executar a árvore e visualizar percursos.

---

## Recursos

✅ Árvores heterogêneas (nós e folhas)  
✅ Impressão extensível (via `IPrintStrategy`)  
✅ Monitoramento independente (via `IMonitor`)  
✅ Vários tipos de percurso (pre-order, depth-first, breadth-first)  
✅ Builder para montagem fluida da árvore  
✅ Serialização / desserialização em JSON  
✅ Suporte a Docker, com parâmetro `--nowait` para execução sem bloqueio

---

## Executando

**Local**

```bash
dotnet run --project Tree.Console
```

Isso executará o traversal no console e aguardará uma tecla ao final.

## Docker

```bash
docker build -t treeconsole .
docker run --rm treeconsole --nowait
```

## Testes
Execute todos os testes do projeto:
```bash
dotnet test
```

## Persistência
Para salvar e restaurar a árvore de forma simples:
```csharp
ITreeRepository repository = new JsonTreeRepository();
await repository.SaveAsync(tree, "tree.json");

var restored = await repository.LoadAsync("tree.json");
```
