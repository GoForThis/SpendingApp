using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SpendingApp.Entities;
using SpendingApp.Exceptions;
using SpendingApp.ModelsDTO;

namespace SpendingApp.Services
{
    public interface IExpenseCategoryService
    {
        Task Create(ExpenseCategoryVM expenseCategory);
        Task Update(int id, ExpenseCategoryVM expenseCategory);
        Task Remove(int id);
        Task<ExpenseCategoryDTO> GetById(int id);
        Task<IEnumerable<ExpenseCategoryDTO>> GetAllAsync();
    }
    public class ExpenseCategoryService : IExpenseCategoryService
    {
        private readonly AppDbContext _context;
        private readonly UserContextService _contextService;
        private readonly IMapper _mapper;

        public ExpenseCategoryService(AppDbContext context, UserContextService contextService, IMapper mapper)
        {
            _context = context;
            _contextService = contextService;
            _mapper = mapper;
        }

        public async Task Create(ExpenseCategoryVM expenseCategory)
        {
            var user = _contextService.GetUserId;

            var category = new ExpenseCategory()
            {
                Name = expenseCategory.Name,
            };
            category.UserId = user;

            await _context.ExpenseCategories.AddAsync(category);
            await _context.SaveChangesAsync();
        }

        public async Task Update(int id, ExpenseCategoryVM expenseCategory)
        {
            var dbCategory = await _context.ExpenseCategories.FirstOrDefaultAsync(c => c.Id == id);
            if (dbCategory is null) throw new NotFoundException("Expense category not exist");
            dbCategory.Name = expenseCategory.Name;
            await _context.SaveChangesAsync();
        }

        public async Task Remove(int id)
        {
            var dbCategory = await _context.ExpenseCategories.FirstOrDefaultAsync(c => c.Id == id);

            if (dbCategory == null) throw new NotFoundException("Expense category not exist");
            _context.ExpenseCategories.Remove(dbCategory);
            await _context.SaveChangesAsync();
        }

        public async Task<ExpenseCategoryDTO> GetById(int id)
        {
            var user = _contextService.GetUserId;
            var category = await _context
                .ExpenseCategories
                .FirstOrDefaultAsync(r => r.Id == id && r.User.Id == user);

            if (category is null) throw new NotFoundException("Expense category not exist");

            return _mapper.Map<ExpenseCategoryDTO>(category);
        }

        public async Task<IEnumerable<ExpenseCategoryDTO>> GetAllAsync()
        {
            var user = _contextService.GetUserId;

            var expenseCategories = await _context
                .ExpenseCategories
                .Where(r => r.User.Id == user)
                .Include(x => x.User)
                .ToListAsync();

            var expenseCategoryDtos = _mapper.Map<List<ExpenseCategoryDTO>>(expenseCategories);
            return expenseCategoryDtos;
        }
    }
}
