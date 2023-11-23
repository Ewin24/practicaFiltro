using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aplication.Repository;
using Domain.Interfaces;
using Persistence.Data;
using Persistence.Entities;

namespace Infrastructure.UnityOfWork;

public class UnityOfWork : IUnitOfWork, IDisposable
{
    private readonly JardineriaContext _context;
    public ICliente _clientes;
    public IEmpleado _empleados;
    public IGamaProducto _gamaProdcuto;
    public IOficina _oficinas;
    public IPago _pagos;
    public IProducto _productos;
    public IPedido _pedidos;
    public IDetallePedido _detallePedidos;
    public ICliente Clientes
    {
        get
        {
            if (_clientes == null)
            {
                _clientes = new ClienteRepository(_context);
            }
            return _clientes;
        }
    }
    public IDetallePedido DetallePedido
    {
        get
        {
            if (_detallePedidos == null)
            {
                _detallePedidos = new DetallePedidoRepository(_context);
            }
            return _detallePedidos;
        }
    }
    public IEmpleado Empleado
    {
        get
        {
            if (_empleados == null)
            {
                _empleados = new EmpleadoRepository(_context);
            }
            return _empleados;
        }
    }
    public IGamaProducto GamaProducto
    {
        get
        {
            if (_gamaProdcuto == null)
            {
                _gamaProdcuto = new GamaProductoReposiroty(_context);
            }
            return _gamaProdcuto;
        }
    }

    public void Dispose()
    {
        _context.Dispose();
    }

    public async Task<int> SaveAsync()
    {
        return await _context.SaveChangesAsync();
    }
}
