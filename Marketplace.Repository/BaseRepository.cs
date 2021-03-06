using AutoMapper;
using Marketplace.Contracts.Repository;
using Marketplace.DB;
using Marketplace.DB.Models;
using Marketplace.DTO.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Marketplace.Repository
{
    public abstract class BaseRepository<TTable, TDto, TId> : IBaseRepository<TTable, TDto, TId> where TTable : class, IBaseEntity<TId>
    {
        protected readonly UserManager<User> _userManager;
        protected readonly RoleManager<Role> _roleManager;
        protected readonly DataContext _context;
        protected readonly IMapper _mapper;
        
        public BaseRepository(
            DataContext context, 
            IMapper mapper)
        { 
            _context = context;
            _mapper = mapper;
        }
        
        public BaseRepository(
            DataContext context,
            IMapper mapper,
            UserManager<User> userManager,
            RoleManager<Role> roleManager)
        {
            _context = context;
            _mapper = mapper;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public virtual async Task<ICollection<TDto>> GetAllAsync()
        {
            var dbItems = await _context.Set<TTable>().ToListAsync();
            return _mapper.Map<List<TDto>>(dbItems);
        }

        public virtual async Task<TDto> GetByIdAsync(TId id)
        {
            var dbItem = await _context.Set<TTable>().FindAsync(id);
            return _mapper.Map<TDto>(dbItem);
        }

        public virtual async Task<TId> AddAsync(IEnumerable<TDto> Dto)
        {
            if (Dto == null)
            {
                throw new ArgumentNullException(nameof(Dto));
            }

            var time = _mapper.Map<ICollection<TTable>>(Dto);

            if (time == null)
            {
                throw new ArgumentNullException(nameof(time));
            }

            await _context.Set<TTable>().AddRangeAsync(time);

            await _context.SaveChangesAsync();

            return (time as List<TTable>)[0].Id;
        }

        public virtual async Task<TId> AddAsync(TDto Dto)
        {
            if (Dto == null)
            {
                throw new ArgumentNullException(nameof(Dto));
            }

            var time = _mapper.Map<TTable>(Dto);

            if (time == null)
            {
                throw new ArgumentNullException(nameof(time));
            }

            await _context.Set<TTable>().AddAsync(time);

            await _context.SaveChangesAsync();

            return time.Id;
        }

        public virtual async Task UpdateAsync(TId id, TDto meaning)
        {
            if (meaning == null)
            {
                throw new ArgumentNullException(nameof(meaning));
            }

            var result = _context.Set<TTable>().Find(id);

            if (result == null)
            {
                throw new ArgumentNullException(nameof(result));
            }

            _context.Entry(result)
                .CurrentValues
                .SetValues(_mapper.Map<TTable>(meaning));

            await _context.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(TId id)
        {
            var obj = _context.Set<TTable>().Find(id);
            if (obj != null)
            {
                _context.Entry<TTable>(obj).State = EntityState.Deleted;
                await _context.SaveChangesAsync();
            }

        }

        public virtual async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

    }
}