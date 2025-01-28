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
