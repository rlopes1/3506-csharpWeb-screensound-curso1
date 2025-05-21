using Microsoft.AspNetCore.Mvc;
using ScreenSound.API.Requests;
using ScreenSound.API.Responses;
using ScreenSound.Banco;
using ScreenSound.Modelos;
using ScreenSound.Shared.Modelos.Modelos;

namespace ScreenSound.API.EndPoints;

public static class MusicasExtensions
{
    public static void AddEndPointsMusicas(this WebApplication app)
    {
        #region musicas

        app.MapGet("/Musicas", ([FromServices] DAL<Musica> dal) =>
        {
            var result = dal.Listar();
            return Results.Ok(EntityListToResponseList(result));

        });

        app.MapGet("/Musicas/{nome}", ([FromServices] DAL<Musica> dal, string nome) =>
        {
            var result = dal.RecuperarPor(x => x.Nome == nome);

            if (result is null)
            {
                return Results.NotFound();
            }
            return Results.Ok(EntityToResponse(result));

        });

        app.MapPost("/Musicas/", ([FromServices] DAL<Musica> dal, [FromServices] DAL < Genero > dalGenero, [FromBody] MusicaRequest musicaRequest) =>
        {

            var musica = new Musica(musicaRequest.nome)
            {

                AnoLancamento = musicaRequest.anoLancamento,
                ArtistaId = musicaRequest.ArtistaId,
                Generos = musicaRequest.Generos is not null ?
                GeneroRequestConverter(musicaRequest.Generos, dalGenero) :
                new List<Genero>()
            };

            dal.Adicionar(musica);
            return Results.Ok(EntityToResponse(musica));

        });

        app.MapPut("/Musicas/", ([FromServices] DAL<Musica> dal, [FromBody] MusicaRequestEdit musicaRequest) =>
        {
            var musicaResgatada = dal.RecuperarPor(x => x.Id == musicaRequest.id);

            if (musicaResgatada is null) return Results.NotFound();

            musicaResgatada.Nome = musicaRequest.nome;
            musicaResgatada.AnoLancamento = musicaRequest.anoLancamento;
            musicaResgatada.Artista.Id = musicaRequest.ArtistaId;



            dal.Atualizar(musicaResgatada);
            return Results.Ok(musicaResgatada);

        });


        app.MapDelete("/Musicas/{id}", ([FromServices] DAL<Musica> dal, int id) =>
        {
            var musicaResgatada = dal.RecuperarPor(x => x.Id == id);

            if (musicaResgatada is null) return Results.NotFound();

            dal.Deletar(musicaResgatada);
            return Results.NoContent();

        });

        #endregion
    }

    private static ICollection<Genero> GeneroRequestConverter(ICollection<GeneroRequest> generos, DAL<Genero> dalGenero)
    {

        var listaDeGeneros = new List<Genero>();
        foreach (var item in generos)
        {
            var entity = GenerosExtensions.RequestToEntity(item);
            var genero = dalGenero.RecuperarPor(x => x.Nome.ToUpper().Equals(entity.Nome.ToUpper()));

            if (genero is not null)
            {
                listaDeGeneros.Add(genero);
            }
            else
            {
                listaDeGeneros.Add(entity);
            }

        }

        return listaDeGeneros;

    }

   

    private static MusicaResponse EntityToResponse(Musica musica)
    {
        var listaGeneroResponse = musica.Generos.Select(x => GenerosExtensions.EntityToResponse(x)).ToList();

        return new MusicaResponse(musica.Id, musica.Nome, musica.Artista.Id, musica.Artista.Nome, listaGeneroResponse);
    }

    private static ICollection<MusicaResponse> EntityListToResponseList(IEnumerable<Musica> listaDeMusicas)
    {
        return listaDeMusicas.Select(x => EntityToResponse(x)).ToList();
    }

  

}
