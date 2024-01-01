using Business.Models.Blog;
using Data.Configuration;

namespace Data.Seed.Auth;

public class CreateInitialCategoriesSeed
{
    private readonly ApplicationDbContext _context;

    public CreateInitialCategoriesSeed(ApplicationDbContext context)
    {
        _context = context;
    }
    public void Create()
    {
        List<string> categories = new List<string>
        {
            "Café da Manhã",
            "Almoço",
            "Jantar",
            "Sobremesa",
            "Lanche",
            "Bebida",
            "Aperitivo",
            "Sopa",
            "Salada",
            "Massa",
            "Frutos do Mar",
            "Vegetariano",
            "Vegano",
            "Sem Lactose",
            "Sem Glúten",
            "Rápido e Fácil",
            "Outros"
        };

        foreach (string category in categories)
        {
            var existingCategory = _context.Categories.FirstOrDefault(c => c.Name == category);

            if (existingCategory == null)
            {
                var newCategory = new Category(category);
                _context.Categories.Add(newCategory);
            }
        }
        _context.SaveChanges();
    }
}
