// ===============================
// Configuração do builder e serviços
// ===============================
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors();
var app = builder.Build();

// ===============================
// Mock de dados (produtos)
// ===============================
var produtos = new List<Produto>
{
	new Produto { Id = 1, Nome = "Teclado", Preco = 99.90m },
	new Produto { Id = 2, Nome = "Mouse", Preco = 49.90m },
	new Produto { Id = 3, Nome = "Monitor", Preco = 899.00m },
	new Produto { Id = 4, Nome = "Teclado", Preco = 120.00m },
	new Produto { Id = 5, Nome = "Mouse", Preco = 55.00m },
	new Produto { Id = 6, Nome = "Monitor", Preco = 950.00m },
	new Produto { Id = 7, Nome = "Headset", Preco = 199.90m },
	new Produto { Id = 8, Nome = "Webcam", Preco = 299.90m },
	new Produto { Id = 9, Nome = "Impressora", Preco = 499.90m },
	new Produto { Id = 10, Nome = "Teclado", Preco = 89.90m },
	new Produto { Id = 11, Nome = "Mouse", Preco = 39.90m },
	new Produto { Id = 12, Nome = "Monitor", Preco = 799.00m },
	new Produto { Id = 13, Nome = "Headset", Preco = 149.90m },
	new Produto { Id = 14, Nome = "Webcam", Preco = 199.90m },
	new Produto { Id = 15, Nome = "Impressora", Preco = 599.90m },
	new Produto { Id = 16, Nome = "Teclado", Preco = 110.00m },
	new Produto { Id = 17, Nome = "Mouse", Preco = 65.00m },
	new Produto { Id = 18, Nome = "Monitor", Preco = 999.00m },
	new Produto { Id = 19, Nome = "Headset", Preco = 179.90m },
	new Produto { Id = 20, Nome = "Webcam", Preco = 249.90m }
};

// Endpoint para retornar os produtos mockados
// ===============================
// Endpoints da API
// ===============================

// Endpoint raiz da API: mensagem simples (Hello World)
app.MapGet("/", () => "Hello World!");


// Endpoint para retornar todos os produtos
app.MapGet("/produtos", () => produtos);

// Endpoint para cadastrar novo produto
app.MapPost("/produtos", (Produto novoProduto) =>
{
	// Gera novo Id automaticamente
	novoProduto.Id = produtos.Count > 0 ? produtos.Max(p => p.Id) + 1 : 1;
	produtos.Add(novoProduto);
	return Results.Created($"/produtos/{novoProduto.Id}", novoProduto);
});

// Endpoint HTML para visualizar a lista de produtos: redireciona para arquivo estático
app.MapGet("/produtos/html", () => Results.Redirect("/produtos.html"));

// Endpoint HTML para visualizar um produto (detalhe): redireciona para arquivo estático com query string
app.MapGet("/produtos/{id}/html", (int id) => Results.Redirect($"/produto.html?id={id}"));

// Endpoint para retornar um produto por Id
app.MapGet("/produtos/{id}", (int id) =>
{
	var produto = produtos.FirstOrDefault(p => p.Id == id);
	return produto is not null ? Results.Ok(produto) : Results.NotFound();
});

// ===============================
// Configuração de arquivos estáticos e CORS
// ===============================
app.UseDefaultFiles(); // Permite servir index.html automaticamente
app.UseStaticFiles();  // Permite servir arquivos da pasta wwwroot
app.UseCors(policy =>
	policy.AllowAnyOrigin()
		  .AllowAnyMethod()
		  .AllowAnyHeader()); // Libera requisições de qualquer origem

// ===============================
// Inicialização da aplicação
// ===============================
app.Run();
