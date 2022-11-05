using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WarehouseManagerApi.Domain.Dtos;
using WarehouseManagerApi.Domain.Entities;

namespace WarehouseManagerApi.Application.Features.Product.Validators;
public class AcceptTheGoodCommandValidator : AbstractValidator<AcceptTheGoodCommand>
{
	public AcceptTheGoodCommandValidator()
	{
		RuleFor(x => x.AcceptTheGoodDto.IdProduct)
            .NotNull()
			.NotEmpty()
			.GreaterThan(default(int))
			.WithMessage("Nie podano produktu!");

		RuleFor(x => x.AcceptTheGoodDto.Quantity)
            .NotNull()
            .NotEmpty()
            .GreaterThan(default(uint))
            .WithMessage("Nie podano ilości lub jej wartość jest równa 0!");

		RuleFor(x => x.AcceptTheGoodDto.Weight)
            .NotNull()
            .NotEmpty()
            .GreaterThan(default(decimal))
            .WithMessage("Nie podano wagi lub jej wartość jest równa 0!");
    }
}