using ScreenSound.Modelos;
using ScreenSound.Shared.Modelos.Modelos;

namespace ScreenSound.API.Responses;

public record MusicaResponse(int id, string nome, int? ArtistaId, string nomeArtista, ICollection<GeneroResponse> Generos);
