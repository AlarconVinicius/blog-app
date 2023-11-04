﻿using FluentValidation;
using Business.Models.Blog.Dtos;

namespace Business.Validators;

public class RecipePostAddValidator : AbstractValidator<RecipePostAddDto>
{
    public RecipePostAddValidator()
    {
        RuleFor(dto => dto.Title)
            .NotEmpty().WithMessage("O título é obrigatório.")
            .MaximumLength(100).WithMessage("O título deve ter no máximo 100 caracteres.");

        RuleFor(dto => dto.Content)
            .NotEmpty().WithMessage("O conteúdo é obrigatório.");

        RuleFor(dto => dto.BlogId)
            .NotEmpty().WithMessage("O ID do blog é obrigatório.");

        RuleFor(dto => dto.CategoryId)
            .NotEmpty().WithMessage("O ID da categoria é obrigatório.");

        RuleFor(dto => dto.Difficulty)
            .IsInEnum().WithMessage("A dificuldade deve ser um valor válido.");

        RuleFor(dto => dto.PreparationTime)
            .NotEmpty().WithMessage("O tempo de preparo é obrigatório.");

        RuleFor(dto => dto.Servings)
            .GreaterThanOrEqualTo(0).WithMessage("O número de porções deve ser maior ou igual a zero.");

        RuleFor(dto => dto.Ingredients)
            .NotNull().WithMessage("Os ingredientes são obrigatórios.");

        RuleFor(dto => dto.Ingredients.Ingredients)
            .NotEmpty().WithMessage("A lista de ingredientes não pode estar vazia.");
    }
}