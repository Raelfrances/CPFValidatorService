# Microsserviço de Validação de CPF

Este projeto implementa um microsserviço para validação de CPFs utilizando arquitetura serverless na Azure. A aplicação é construída com base em princípios modernos de computação em nuvem, garantindo alta disponibilidade, baixo custo operacional e facilidade de manutenção.

## Objetivo

Desenvolver um microsserviço eficiente, escalável e econômico para validação de CPFs.

## Tecnologias Utilizadas

- **Azure Functions**: Para criar funções serverless.
- **C#**: Linguagem de programação utilizada para implementação da validação.
- **Azure CLI**: Ferramenta de linha de comando para gerenciar recursos da Azure.
- **Visual Studio Code**: Editor de código utilizado para desenvolvimento.

    - ![Azure](https://upload.wikimedia.org/wikipedia/commons/thumb/a/a8/Microsoft_Azure_Logo.svg/120px-Microsoft_Azure_Logo.svg.png) ![C#](https://upload.wikimedia.org/wikipedia/commons/thumb/4/4f/Csharp_Logo.png/120px-Csharp_Logo.png)  ![Visual Studio Code](https://upload.wikimedia.org/wikipedia/commons/thumb/9/9a/Visual_Studio_Code_1.35_icon.svg/120px-Visual_Studio_Code_1.35_icon.svg.png)
  
## Estrutura do Projeto
```
- Projeto Azure Function: ValidateCPF
  - |-- host.json
  - |-- local.settings.json
  - |-- .vscode
      - |-- settings.json
  - |-- ValidateCPF
      - |-- function.json
      - |-- fnvalidacpf.csx
  - |-- bin
  - |-- obj
```

O projeto é composto pelos seguintes arquivos:

- `fnvalidacpf.csx`: Contém a função Azure para validação de CPF.
- `local.settings.json`: Arquivo de configuração local para rodar a função.

## Como Configurar o Ambiente

1. Crie uma conta no [Azure](https://azure.microsoft.com/).
2. Instale o [Azure CLI](https://docs.microsoft.com/cli/azure/install-azure-cli).
3. Instale o [Visual Studio Code](https://code.visualstudio.com/) com a extensão Azure Functions.

## Criação da Função Azure

1. Crie um novo projeto no Visual Studio Code.
2. Adicione uma nova função:
   - Selecione o ícone do Azure na barra lateral.
   - Clique em "Criar Nova Função".
   - Escolha a linguagem "C#".
   - Selecione o modelo "HTTP trigger".
   - Nomeie a função como "ValidateCPF".


## Implementação da Função de Validação de CPF

Crie um  arquivo.csx ex:  `fnvalidacpf.csx`, e insira o seguinte código:

```csharp
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

public static async Task<IActionResult> Run(HttpRequest req, ILogger log)
{
    log.LogInformation("C# HTTP trigger function processed a request.");

    string cpf = req.Query["cpf"];

    if (string.IsNullOrEmpty(cpf))
    {
        return new BadRequestObjectResult("Por favor, forneça um CPF.");
    }

    bool isValid = IsValidCPF(cpf);

    return (ActionResult)new OkObjectResult(new { isValid = isValid });
}

public static bool IsValidCPF(string cpf)
{
    if (cpf.Length != 11 || cpf.All(c => c == cpf[0]))
        return false;

    var numbers = cpf.Select(c => int.Parse(c.ToString())).ToArray();

    for (int j = 0; j < 2; j++)
    {
        int sum = 0;
        for (int i = 0; i < 9 + j; i++)
            sum += (10 + j - i) * numbers[i];

        int result = sum % 11;
        if (result < 2 ? 0 : 11 - result != numbers[9 + j])
            return false;
    }

    return true;
}
```

## Publicação no Azure

1. No Visual Studio Code, selecione o ícone do Azure.
2. Clique com o botão direito no projeto e selecione "Deploy to Function App".
3. Siga as instruções para criar uma nova Function App no Azure.

## Testes e Manutenção

- Testes: Utilize ferramentas como o Postman para testar as requisições HTTP ao seu serviço.
