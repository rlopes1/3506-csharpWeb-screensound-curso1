using ScreenSound.Shared.Modelos.Modelos;

namespace ScreenSound.API.Requests;

public record MusicaRequest (string nome, int ArtistaId, int anoLancamento, ICollection<GeneroRequest> Generos = null);

