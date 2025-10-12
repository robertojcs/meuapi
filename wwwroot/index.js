// ===============================
// Script principal da página Index
// ===============================

const apiUrl = window.location.hostname ? `http://${window.location.hostname}:5217/produtos` : '/produtos';
let produtos = [];
let produtosFiltrados = [];
let ordemNomeAsc = true;
let ordemPrecoAsc = true;
const tbody = document.querySelector('#produtos-table tbody');

// Função para renderizar a tabela de produtos
function renderProdutos(lista) {
    tbody.innerHTML = '';
    lista.forEach(produto => {
        const tr = document.createElement('tr');
        tr.innerHTML = `<td>${produto.id}</td><td>${produto.nome}</td><td>R$ ${produto.preco.toFixed(2)}</td>`;
        tbody.appendChild(tr);
    });
}

// Requisição dos dados da API
fetch(apiUrl)
    .then(response => response.json())
    .then(data => {
        produtos = data;
        produtosFiltrados = [...produtos];
        renderProdutos(produtosFiltrados);
    })
    .catch(() => {
        document.body.innerHTML += '<p style="color:red">Erro ao carregar produtos.</p>';
    });

// Filtro por nome do produto
const filtroInput = document.getElementById('filtro');
filtroInput.addEventListener('input', function() {
    const termo = this.value.toLowerCase();
    produtosFiltrados = produtos.filter(p => p.nome.toLowerCase().includes(termo));
    renderProdutos(produtosFiltrados);
});

// Ordenação por nome
const ordenarNomeBtn = document.getElementById('ordenar-nome');
ordenarNomeBtn.addEventListener('click', function() {
    produtosFiltrados.sort((a, b) => {
        if (a.nome.toLowerCase() < b.nome.toLowerCase()) return ordemNomeAsc ? -1 : 1;
        if (a.nome.toLowerCase() > b.nome.toLowerCase()) return ordemNomeAsc ? 1 : -1;
        return 0;
    });
    ordemNomeAsc = !ordemNomeAsc;
    renderProdutos(produtosFiltrados);
});

// Ordenação por preço
const ordenarPrecoBtn = document.getElementById('ordenar-preco');
ordenarPrecoBtn.addEventListener('click', function() {
    produtosFiltrados.sort((a, b) => ordemPrecoAsc ? a.preco - b.preco : b.preco - a.preco);
    ordemPrecoAsc = !ordemPrecoAsc;
    renderProdutos(produtosFiltrados);
});
