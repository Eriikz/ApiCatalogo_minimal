using ApiCatalago.Context;
using ApiCatalago.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalago.ApiEndpoints
{
    public static class CategoriasEndpoints
    {
        public static void MapCategoriasEndpoints(this WebApplication app)
        {
            app.MapPost("/categorias", async (Categoria categoria, AppDbContext db)
=>
            {
                db.Categoria.Add(categoria);
                await db.SaveChangesAsync();

                return Results.Created($"/categorias/{categoria.CategoriaId}", categoria);
            });

            app.MapGet("/categorias", async (AppDbContext db) => await db.Categoria.ToListAsync()).WithTags("Categorias").RequireAuthorization();

            app.MapGet("/categorias/{id:int}", async (int id, AppDbContext db)
                =>
            {
                return await db.Categoria.FindAsync(id)
                is Categoria categoria
                ? Results.Ok(categoria)
                : Results.NotFound();
            });

            app.MapPut("/categorias/{id:int}", async (int id, Categoria categoria, AppDbContext db) =>
            {
                if (categoria.CategoriaId != id)
                {
                    return Results.BadRequest();
                }

                var categoriaDB = await db.Categoria.FindAsync(id);

                if (categoriaDB is null) return Results.NotFound();

                categoriaDB.Nome = categoria.Nome;
                categoriaDB.Descricao = categoria.Descricao;

                await db.SaveChangesAsync();

                return Results.Ok(categoriaDB);
            });

            app.MapDelete("/categoria/{id:int}", async (int id, AppDbContext db) =>
            {
                var categoria = await db.Categoria.FindAsync(id);

                if (categoria is null)
                {
                    return Results.NotFound();
                }

                db.Categoria.Remove(categoria);
                await db.SaveChangesAsync();

                return Results.NoContent();
            });
        }
    }
}
