﻿using GeekStore.Core.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GeekStore.Clientes.Api.Models
{
    public interface IClienteRepository : IRepository<Cliente>
    {
        void Adicionar(Cliente cliente);
        Task<IEnumerable<Cliente>> ObterTodos();
        Task<Cliente> ObterPorCpf(string cpf);
        void AdicionarEndereco(Endereco endereco);
        Task<Endereco> ObterEnderecoPorId(Guid id);
    }
}
