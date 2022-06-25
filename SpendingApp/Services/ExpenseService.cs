using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using SpendingApp.Entities;
using SpendingApp.Exceptions;
using SpendingApp.ModelsDTO;

namespace SpendingApp.Services
{
    public interface IExpenseService
    {
        Task Create(CreateExpenseVM createExpenseVM);
        Task Update(int id, CreateExpenseVM createExpenseVM);
        Task Remove(int id);
        Task<ExpenseDTO> GetById(int id);
        Task<IEnumerable<ExpenseDTO>> GetAll(string name);
    }
    public class ExpenseService : IExpenseService
    {
        private readonly AppDbContext _context;
        private readonly IUserContextService _contextService;
        private readonly IMapper _mapper;

        public ExpenseService(AppDbContext context, IUserContextService contextService, IMapper mapper)
        {
            _context = context;
            _contextService = contextService;
            _mapper = mapper;
        }

        public async Task Create(CreateExpenseVM createExpenseVM)
        {
            var currentUser = _contextService.GetUserId;

            var newExpense = new Expense()
            {
                Name = createExpenseVM.Name,
                PaymentMethod = createExpenseVM.PaymentMethod,
                Amount = createExpenseVM.Amount,
                Date = createExpenseVM.Date,
                ExpenseCategoryId = createExpenseVM.ExpenseCategoryId
            };
            newExpense.UserId = currentUser;
            await _context.Expenses.AddAsync(newExpense);
            await _context.SaveChangesAsync();
        }

        public async Task Update(int id, CreateExpenseVM createExpenseVM)
        {
            var newExpense = await _context.Expenses.FirstOrDefaultAsync(t => t.Id == id);

            if (newExpense == null) throw new NotFoundException("Expense not exist");

            newExpense.Name = createExpenseVM.Name;
            newExpense.PaymentMethod = createExpenseVM.PaymentMethod;
            newExpense.Amount = createExpenseVM.Amount;
            newExpense.Date = createExpenseVM.Date;
            newExpense.ExpenseCategoryId = createExpenseVM.ExpenseCategoryId;

            await _context.SaveChangesAsync();
        }

        public async Task Remove(int id)
        {
            var expense = await _context.Expenses.FirstOrDefaultAsync(t => t.Id == id);

            if (expense == null) throw new NotFoundException("Expense not exist");

            _context.Expenses.Remove(expense);

            await _context.SaveChangesAsync();
        }

        public async Task<ExpenseDTO> GetById(int id)
        {
            var currentUser = _contextService.GetUserId;

            var expense = await _context.Expenses.FirstOrDefaultAsync(t => t.User.Id == currentUser && t.Id == id);

            if (expense == null) throw new NotFoundException("Expense not exist");

            return _mapper.Map<ExpenseDTO>(expense);
        }

        public async Task<IEnumerable<ExpenseDTO>> GetAll(string name)
        {
            var currentUser = _contextService.GetUserId;

            IQueryable<Expense> expensesQuery = _context.Expenses.Include(x => x.ExpenseCategory);

            if (expensesQuery == null) throw new NotFoundException("Expenses not exist");

            expensesQuery = expensesQuery
                .Where(t => t.User.Id == currentUser);

            if (!string.IsNullOrEmpty(name))
            {
                expensesQuery = expensesQuery.Where(x => x.Name.Contains(name));
            }

            var expenses = await expensesQuery.ToListAsync();
            var expensesDTO = _mapper.Map<IEnumerable<ExpenseDTO>>(expenses);
            return expensesDTO;
        }
    }
}
