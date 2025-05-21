namespace ScreenSound.API.Requests;

public record MusicaRequestEdit(string nome, int id, int ArtistaId, int anoLancamento): MusicaRequest(nome, ArtistaId, anoLancamento);

