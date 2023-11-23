using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Repository;
using Domain.Interfaces;
using Persistence.Data;
using Persistence.Entities;

namespace Aplication.Repository
{
    public class DetallePedidoRepository : GenericRepository<DetallePedido>, IDetallePedido
    {
        private readonly JardineriaContext _context;
        public DetallePedidoRepository(JardineriaContext context) : base(context)
        {
            _context = context;
        }
    }
}