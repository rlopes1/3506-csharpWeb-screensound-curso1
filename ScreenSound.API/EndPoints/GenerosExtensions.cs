using Microsoft.AspNetCore.Mvc;
using ScreenSound.API.Requests;
using ScreenSound.API.Responses;
using ScreenSound.Banco;
using ScreenSound.Shared.Modelos.Modelos;
using System.Runtime.CompilerServices;

namespace ScreenSound.API.EndPoints;

public static class GenerosExtensions
{

    public static void AddEndpointsGeneros(this WebApplication app)
    {


        app.MapGet("/Generos", ([FromServices] DAL<Genero> dal) =>
        {

            var result = dal.Listar();



            return Results.Ok(EntityListToResponseList(result));


        });

        app.MapGet("/Generos/{nome}", (DAL<Genero> dal, string nome) =>
        {

            var retorno = dal.RecuperarPor(x => x.Nome.ToUpper().Equals(nome.ToUpper()));

            if (retorno is null) return Results.NotFound();

            return Results.Ok(EntityToResponse(retorno));

            

        });

        app.MapPost("/Generos", ([FromServices] DAL<Genero> dal, [FromBody] GeneroRequest generoRequest) =>
        {

            var entity = RequestToEntity(generoRequest);

            dal.Adicionar(entity);

            var result = EntityToResponse(entity);

            return Results.Ok(result);


        });


        app.MapPut("/Generos/", ([FromServices] DAL<Genero> dal, [FromBody] GeneroRequestEdit generoRequest ) =>
        {

            var generoRecuperado = dal.RecuperarPor(x => x.Id == generoRequest.Id);

            if (generoRecuperado is null) return Results.NotFound();


            generoRecuperado.Descricao = generoRequest.Descricao;
            generoRecuperado.Nome = generoRequest.Nome;


            dal.Atualizar(generoRecuperado);


            return Results.Ok(EntityToResponse(generoRecuperado));


        });

        app.MapDelete("/Generos/{id}", ([FromServices] DAL<Genero> dal, int id) =>
        {
            var generoRecuperado = dal.RecuperarPor(x => x.Id == id);

            if (generoRecuperado is null) return Results.NotFound();

            dal.Deletar(generoRecuperado);

            return Results.Ok();


        });
        
    }





    public static Genero RequestToEntity(GeneroRequest genero)
    {
        return new Genero() { Nome = genero.Nome, Descricao = genero.Descricao};
    }

    public static GeneroResponse EntityToResponse(Genero genero)
    {
        return new GeneroResponse(genero.Id, genero.Nome, genero.Descricao);
    }

    public static ICollection<GeneroResponse> EntityListToResponseList(IEnumerable<Genero> generos)
    {

        return generos.Select(x => EntityToResponse(x)).ToList();

    }
}
