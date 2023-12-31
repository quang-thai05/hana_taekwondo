﻿using StudentManagement.DTOs.Output;
using StudentManagement.Models;
using StudentManagement.Repositories.Interfaces;

namespace StudentManagement.Repositories;

public class SpendingRepository : Repository<Spending>, ISpendingRepository
{
    private readonly hana_taekwondoContext _context;

    public SpendingRepository(hana_taekwondoContext context) : base(context)
    {
        _context = context;
    }

    public SpendingValueOutput GetSpendingValue(int month, int year)
    {
        var monthlyTotals = _context.Spendings
            .Where(x => x.PaidDate.Year == year && x.PaidDate.Month == month)
            .GroupBy(x => x.PaidDate.Month)
            .Select(group => new SpendingItemValueOutput
            {
                Month = group.Key,
                ElectricSpending = group.Sum(x => x.Electric),
                WaterSpending = group.Sum(x => x.Water),
                RentSpending = group.Sum(x => x.Rent),
                SalarySpending = group.Sum(x => x.Salary),
                EatingSpending = group.Sum(x => x.Eating),
                AnotherSpending = group.Sum(x => x.Another),
                PaidDate = $"{year}-{group.Key:00}",
                Total = group.Sum(x => x.Water + x.Electric + x.Rent + x.Salary + x.Eating + x.Another)
            })
            .FirstOrDefault();

        var annual = _context.Spendings
            .Where(x => x.PaidDate.Year == year)
            .Sum(x => x.Water + x.Electric + x.Rent + x.Salary + x.Eating + x.Another);

        var allMonths = Enumerable.Range(1, 12);
        var spendingData = allMonths
            .GroupJoin(
                _context.Spendings.Where(x => x.PaidDate.Year == year),
                month => month,
                tuition => tuition.PaidDate.Month,
                (month, tuitions) => new
                {
                    Month = month,
                    TotalSpendings = tuitions.Sum(x => x.Water + x.Electric + x.Rent + x.Salary + x.Eating + x.Another)
                }
            )
            .OrderBy(x => x.Month)
            .ToList();

        var spendingValues = spendingData.Select(item => item.TotalSpendings).ToList();

        monthlyTotals ??= new SpendingItemValueOutput
        {
            Month = month,
            ElectricSpending = 0,
            WaterSpending = 0,
            RentSpending = 0,
            SalarySpending = 0,
            EatingSpending = 0,
            AnotherSpending = 0,
            PaidDate = $"{year}-{month}",
            Total = 0
        };
        return new SpendingValueOutput
        {
            Monthly = monthlyTotals,
            SpendingData = spendingValues,
            SpendingAnnual = annual
        };
    }

    public IEnumerable<Spending> GetListSpending()
    {
        var result = _context.Spendings.ToList();
        return result;
    }

    public Spending GetSpendingById(int spendingId)
    {
        var spending = _context.Spendings.FirstOrDefault(x => x.Id == spendingId);
        return spending;
    }

    public override async Task Add(Spending entity)
    {
        _context.Spendings.Add(entity);
        await _context.SaveChangesAsync();
    }

    public override async Task Update(Spending entity)
    {
        _context.Spendings.Update(entity);
        await _context.SaveChangesAsync();
    }

    public override async Task Delete(Spending entity)
    {
        _context.Spendings.Remove(entity);
        await _context.SaveChangesAsync();
    }
}