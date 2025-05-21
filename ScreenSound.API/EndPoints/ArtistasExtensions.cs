using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ScreenSound.API.Requests;
using ScreenSound.API.Responses;
using ScreenSound.Banco;
using ScreenSound.Modelos;

namespace ScreenSound.API.EndPoints;

public static class ArtistasExtensions
{

    public static void AddEndpointsArtistas(this WebApplication app)
    {
        #region EndPoints

        app.MapGet("/Artistas", ([FromServices] DAL<Artista> dal) =>
        {
            var result = EntityListToResponseList(dal.Listar());

            return Results.Ok(result);


        });

        app.MapGet("/Artistas/{nome}", ([FromServices] DAL<Artista> dal, string nome) =>
        {

            var artista = dal.RecuperarPor(a => a.Nome.ToUpper().Equals(nome.ToUpper()));
            if (artista is null)
            {
                return Results.NotFound();
            }

            var artistaResponse = new ArtistaResponse(artista.Id, artista.Nome, artista.Bio, artista.FotoPerfil);

          

            return Results.Ok(artistaResponse);

        });

        app.MapPost("/Artistas", ([FromServices] DAL<Artista> dal, [FromBody] ArtistaRequest artistaRequest) =>
        {

            var artista = new Artista(artistaRequest.nome, artistaRequest.bio);

            dal.Adicionar(artista);
            return Results.Ok(artistaRequest);

        });

        app.MapDelete("/Artistas/{id}", ([FromServices] DAL<Artista> dal, int id) =>
        {
            var artista = dal.RecuperarPor(x => x.Id == id);

            if (artista is null) return Results.NotFound();

            dal.Deletar(artista);
            return Results.NoContent();

        });

        app.MapPut("/Artistas/", ([FromServices] DAL<Artista> dal, [FromBody] ArtistaRequestEdit artistaRequest) =>
        {

            


            var artistaAAtualizar = dal.RecuperarPor(x => x.Id == artistaRequest.id);
            if (artistaAAtualizar is null) return Results.NotFound();

            artistaAAtualizar.Nome = artistaRequest.nome;
            artistaAAtualizar.Bio = artistaRequest.bio;
            

            dal.Atualizar(artistaAAtualizar);
            return Results.Ok();
        });

        #endregion

        

    }



    private static ICollection<ArtistaResponse> EntityListToResponseList(IEnumerable<Artista> listaDeArtistas)
    {

        return listaDeArtistas.Select(a => EntityToResponse(a)).ToList();

    }

    private static ArtistaResponse EntityToResponse(Artista artista)
    {
        return new ArtistaResponse (artista.Id, artista.Nome, artista.Bio, artista.FotoPerfil);
    }


}
