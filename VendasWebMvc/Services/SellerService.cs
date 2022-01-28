using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VendasWebMvc.Data;
using VendasWebMvc.Models;

namespace VendasWebMvc.Services
{
    public class SellerService 
    {
        private readonly VendasWebMvcContext _context; 

        public SellerService(VendasWebMvcContext context)
        {
            _context = context;
        }

        // Retorna uma lista com todos os vendedores do BD
        public List<Seller> FindAll() 
        {
            return _context.Seller.ToList();
        }

        public void Insert(Seller obj)
        {
            _context.Add(obj);
            _context.SaveChanges();
        }
    }
}
