﻿using Microsoft.EntityFrameworkCore;
using StudentManagement.Models;
using StudentManagement.Repositories.Interfaces;

namespace StudentManagement.Repositories;

public class StudentRepository : Repository<Student>, IStudentRepository
{
    private readonly hana_taekwondoContext _context;

    public StudentRepository(hana_taekwondoContext context) : base(context)
    {
        _context = context;
    }

    public override Task<List<Student>> GetAll()
    {
        var results = _context.Students.ToListAsync();
        return results;
    }

    public List<Student> GetAllStudents()
    {
        var result = _context.Students.ToList();
        return result;
    }

    public Student GetStudentInfoByStudentId(int studentId)
    {
        var student = _context.Students.FirstOrDefault(s => s.Id == studentId);
        return student;
    }

    public int AddNewStudent(Student student)
    {
        _context.Students.Add(student);
        var result = _context.SaveChanges();
        return result;
    }

    public int UpdateStudent(Student student)
    {
        _context.Students.Update(student);
        var result = _context.SaveChanges();
        return result;
    }
}