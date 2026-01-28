using FamilyFinance.Api.Core.Interfaces;
using FamilyFinance.Api.Data;
using FamilyFinance.Api.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace FamilyFinance.Api.Infrastructure.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly AppDbContext _context;

        public TransactionService(AppDbContext context)
        {
            _context = context;
        }

        // Retorna todas as transações de uma família
        public async Task<IEnumerable<Transaction>> GetTransactionsByFamilyAsync(Guid familyId)
        {
            return await _context.Transactions
                .Where(t => t.FamilyId == familyId)
                .Include(t => t.Category)
                .Include(t => t.Member)
                .ToListAsync();
        }

        // Cria uma nova transação
        public async Task<Transaction> CreateTransactionAsync(Guid familyId, Transaction transaction)
        {
            transaction.Id = Guid.NewGuid();
            transaction.FamilyId = familyId;

            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();
            return transaction;
        }

        // Retorna transações de um mês/ano específico
        public async Task<IEnumerable<Transaction>> GetMonthlyTransactionsAsync(Guid familyId, int month, int year)
        {
            return await _context.Transactions
                .Where(t => t.FamilyId == familyId &&
                            t.Date.Month == month &&
                            t.Date.Year == year)
                .Include(t => t.Category)
                .Include(t => t.Member)
                .ToListAsync();
        }

        // Retorna o resumo financeiro (income, expense e balance) da família
        public async Task<TransactionSummary> GetSummaryAsync(Guid familyId)
        {
            var income = await _context.Transactions
                .Where(t => t.FamilyId == familyId && t.Type == TransactionType.Income)
                .SumAsync(t => t.Amount);

            var expense = await _context.Transactions
                .Where(t => t.FamilyId == familyId && t.Type == TransactionType.Expense)
                .SumAsync(t => t.Amount);

            return new TransactionSummary
            {
                Income = income,
                Expense = expense
            };
        }
    }
}
