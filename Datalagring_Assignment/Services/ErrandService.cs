using Datalagring_Assignment.Models.Entities;
using Datalagring_Assignment.Models;
using Datalagring_Assignment.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Datalagring_Assignment.Services
{
    internal class ErrandService
    {
        private static DataContext _context = new DataContext();
        public static async Task SaveAsync(Errand errand)
        {
            var _errandEntity = new ErrandEntity
            {
                Description = errand.Description,
                ErrandDateTime = errand.ErrandDateTime,
                Status = (int)errand.Status
            };

            var _customerEntity = await _context.Customers.FirstOrDefaultAsync(x =>
                x.CustomerName == errand.CustomerName &&
                x.Phone == errand.Phone &&
                x.Email == errand.Email
            );

            if (_customerEntity != null)
            {
                _errandEntity.CustomerId = _customerEntity.Id;
            }
            else
            {
                _customerEntity = new CustomerEntity
                {
                    CustomerName = errand.CustomerName,
                    Phone = errand.Phone,
                    Email = errand.Email
                };

                await _context.Customers.AddAsync(_customerEntity);
                await _context.SaveChangesAsync();

                _errandEntity.CustomerId = _customerEntity.Id;

            }

            _context.Add(_errandEntity);
            await _context.SaveChangesAsync();
        }


        public static async Task<IEnumerable<Errand>> GetAllAsync()
        {
            var _errands = await _context.Errands
                .Include(e => e.Customer)
                .Include(e => e.Comments)
                .ToListAsync();

            return _errands.Select(_errand => new Errand
            {
                Id = _errand.Id,
                Description = _errand.Description,
                ErrandDateTime = _errand.ErrandDateTime,
                CustomerName = _errand.Customer.CustomerName,
                Phone = _errand.Customer.Phone,
                Email = _errand.Customer.Email,
                Status = (ErrandStatus)_errand.Status,
                Comments = _errand.Comments.Select(c => new CommentEntity
                {
                    Id = c.Id,
                    Comment = c.Comment,
                    CommentDateTime = c.CommentDateTime,
                    ErrandId = c.ErrandId
                }).ToList()
            });
        }




        public static async Task<Errand> GetAsync(int id)
        {
            var _errand = await _context.Errands
                .Include(x => x.Customer)
                .Include(x => x.Comments)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (_errand != null)
                return new Errand
                {
                    Id = _errand.Id,
                    Description = _errand.Description,
                    ErrandDateTime = _errand.ErrandDateTime,
                    CustomerName = _errand.Customer.CustomerName,
                    Phone = _errand.Customer.Phone,
                    Email = _errand.Customer.Email,
                    Status = (ErrandStatus)_errand.Status,
                    Comments = _errand.Comments.Select(c => new CommentEntity
                    {
                        Id = c.Id,
                        Comment = c.Comment,
                        CommentDateTime = c.CommentDateTime,
                        ErrandId = c.ErrandId
                    }).ToList()
                };


            else
                return null!;
        }
        public static async Task UpdateAsync(Errand errand)
        {
            var _errandEntity = await _context.Errands.Include(x => x.Customer).FirstOrDefaultAsync(x => x.Id == errand.Id);
            if (_errandEntity != null)
            {
                if (errand.Status >= ErrandStatus.NotStarted && errand.Status <= ErrandStatus.Completed)
                {
                    _errandEntity.Status = (int)errand.Status;
                }

                _context.Update(_errandEntity);
                await _context.SaveChangesAsync();
            }
        }

        public static async Task DeleteAsync(int id)
        {
            var _errand = await _context.Errands.Include(x => x.Customer).FirstOrDefaultAsync(x => x.Id == id && x.Status == 3);
            if (_errand != null)
            {
                _context.Remove(_errand);
                await _context.SaveChangesAsync();
            }
        }




        // ADD COMMENT....
        public static async Task AddCommentAsync(int errandId, string comment)
        {
            var _errandEntity = await _context.Errands.FindAsync(errandId);

            if (_errandEntity != null)
            {
                var _commentEntity = new CommentEntity
                {
                    Comment = comment,
                    CommentDateTime = DateTime.Now,
                    Errand = _errandEntity
                };

                _context.Comments.Add(_commentEntity);
                await _context.SaveChangesAsync();

            }
        }


    }



}
