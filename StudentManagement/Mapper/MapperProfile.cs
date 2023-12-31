﻿using AutoMapper;
using StudentManagement.DTOs.Input;
using StudentManagement.DTOs.Output;
using StudentManagement.Models;

namespace StudentManagement.Mapper;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<TimetableInput, Timetable>()
            .ForMember(des => des.Id,
                opt => opt.MapFrom(src => src.TimetableId));

        CreateMap<TuitionInput, Tuition>()
            .ForMember(des => des.CreatedAt,
                opt => opt.MapFrom(src => DateTime.Now))
            .ForMember(des => des.ModifiedAt,
                opt => opt.MapFrom(src => DateTime.Now)); ;

        CreateMap<NewStudentInput, Student>()
            .ForMember(des => des.CreatedAt,
                opt => opt.MapFrom(src => DateTime.Now))
            .ForMember(des => des.ModifiedAt,
                opt => opt.MapFrom(src => DateTime.Now));

        CreateMap<Student, StudentInfoOutput>()
            .ForMember(des => des.Dob,
                opt => opt.MapFrom(src => $"{src.Dob:yyyy-MM-dd}"))
            .ForMember(des => des.Gender,
                opt => opt.MapFrom(src => src.Gender ? "Male" : "Female"));

        CreateMap<Student, StudentOutput>()
            .ForMember(des => des.Dob,
                opt => opt.MapFrom(src => $"{src.Dob:yyyy-MM-dd}"))
            .ForMember(des => des.Gender,
                opt => opt.MapFrom(src => src.Gender ? "Male" : "Female"))
            .ForMember(des => des.TotalTuitions,
                opt => opt.MapFrom(src => src.Tuitions.Sum(x => x.ActualAmount)));

        CreateMap<Class, ClassInfoOutput>()
            .ForMember(des => des.Id,
             opt => opt.MapFrom(src => src.Id))
            .ForMember(des => des.Name,
             opt => opt.MapFrom(src => src.Name))
            .ForMember(des => des.StartDate,
             opt => opt.MapFrom(src => $"{src.StartDate:yyyy-MM-dd}"))
            .ForMember(des => des.DueDate,
             opt => opt.MapFrom(src => $"{src.DueDate:yyyy-MM-dd}"));

        CreateMap<SpendingInput, Spending>()
            .ForMember(des => des.CreatedAt,
                opt => opt.MapFrom(src => DateTime.Now))
            .ForMember(des => des.ModifiedAt,
                opt => opt.MapFrom(src => DateTime.Now));
        
        CreateMap<Spending, SpendingItemListOutput>()
            .ForMember(des => des.PaidDate,
                opt => opt.MapFrom(src => $"{src.PaidDate:yyyy-MM-dd}"));

        CreateMap<Student, DeadlineTutionOutput>()
            .ForMember(des => des.StudentId,
             opt => opt.MapFrom(src => src.Id))
            .ForMember(des => des.FullName,
             opt => opt.MapFrom(src => src.FullName));

        CreateMap<Tuition, DeadlineTutionOutput>()
           .ForMember(des => des.DueDate,
            opt => opt.MapFrom(src => $"{src.DueDate:yyyy-MM-dd}"));
    }
}