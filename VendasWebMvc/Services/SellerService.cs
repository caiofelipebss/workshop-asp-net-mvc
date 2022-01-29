using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VendasWebMvc.Data;
using VendasWebMvc.Models;
using VendasWebMvc.Services.Exceptions;

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

        public Seller FindById(int id)
        {
            return _context.Seller.Include(obj => obj.Department).FirstOrDefault(obj => obj.Id == id); // eager loading
        }

        public void Remove(int id)
        {
            var obj = _context.Seller.Find(id);
            _context.Seller.Remove(obj);
            _context.SaveChanges();
        }

        public void Update(Seller obj)
        {
            if (!_context.Seller.Any(x => x.Id == obj.Id)) //Vê se não existe no banco de dados algum vendededor X com o ID igual ao ID.obj
            {
                throw new NotFoundException("Id not found");
            }
            try
            {
            _context.Update(obj);
            _context.SaveChanges();
            }

            //Se uma exceção de nível de acesso a dados acontecer, lançará uma exceção da camada de serviços. Assim o controlador só conversará apenas com a camada de serviço.
            catch (DbUpdateConcurrencyException e) 
            {
                throw new DbConcurrencyException(e.Message);
            }
        }
    }
}
